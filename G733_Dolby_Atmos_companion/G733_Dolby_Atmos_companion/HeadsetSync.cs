using System;
using System.Linq;
using System.Threading;
using HidLibrary;
using Microsoft.Win32;

namespace G733_Companion
{
    // Owns the HID link to the G733 receiver and keeps the headset matching the saved settings.
    //
    // The headset forgets lighting/sidetone when it powers off, sleeps, or the receiver re-enumerates
    // (e.g. after the PC resumes), so instead of sending settings once we run a loop that:
    //  - reopens the receiver whenever the handle is lost,
    //  - polls the battery to detect the headset going offline/online, and re-applies on every reconnect,
    //  - reads the current light effect and sidetone back from the headset and re-applies on any mismatch,
    //  - re-applies everything periodically as a safety net,
    //  - waits for each command's acknowledgement and retries if it doesn't arrive.
    // All HID writes go through this one thread so commands never interleave.
    public class HeadsetSync
    {
        // HID++ 2.0 feature indexes on the G733 (from Root.GetFeature)
        private const byte feature_led = 0x04;      // 0x8070 Color LED effects
        private const byte feature_sidetone = 0x07; // 0x8300 Sidetone
        private const byte feature_battery = 0x08;  // 0x1F20 ADC measurement (battery voltage)

        private const byte fn_led_set_zone_effect = 0x3;
        private const byte fn_led_get_zone_effect = 0xE;
        private const byte fn_sidetone_get = 0x0;
        private const byte fn_sidetone_set = 0x1;
        private const byte fn_battery_get = 0x0;

        private const byte software_id = 0x0d;

        private const byte mode_off = 0x00;
        private const byte mode_fixed = 0x01;
        private const byte mode_breathing = 0x02;
        private const byte mode_cycling = 0x03;

        private const int poll_interval_ms = 5000;
        private const int reconnect_interval_ms = 1000;
        private const int debounce_ms = 250;
        private const int reply_timeout_ms = 500;
        private const int request_attempts = 3;
        private static readonly TimeSpan full_reapply_interval = TimeSpan.FromMinutes(2);

        public event Action<byte[]> FrameReceived;
        public event Action<bool> HeadsetOnlineChanged;

        private HidDevice device;
        private Thread worker;
        private volatile bool running;
        private volatile bool dirty = true;
        private volatile bool reopen_requested;
        private volatile bool device_lost;
        private bool headset_online;
        private DateTime last_full_apply = DateTime.MinValue;
        private readonly AutoResetEvent wake = new AutoResetEvent(false);

        // Single outstanding request, matched against incoming frames by the reader thread
        private readonly object pending_lock = new object();
        private byte pending_feature;
        private byte pending_function;
        private byte[] pending_reply;
        private bool pending_error;
        private readonly ManualResetEventSlim reply_received = new ManualResetEventSlim(false);

        public void Start()
        {
            if (running) return;
            running = true;
            SystemEvents.PowerModeChanged += power_mode_changed;
            SystemEvents.SessionSwitch += session_switch;
            worker = new Thread(run) { IsBackground = true, Name = "HeadsetSync" };
            worker.Start();
        }

        public void Stop()
        {
            running = false;
            SystemEvents.PowerModeChanged -= power_mode_changed;
            SystemEvents.SessionSwitch -= session_switch;
            wake.Set();
        }

        // Call whenever a setting changes (or the user asks to re-apply); the worker pushes it out shortly after.
        public void RequestApply()
        {
            dirty = true;
            wake.Set();
        }

        private void power_mode_changed(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.Resume) return;
            Console.WriteLine("System resumed, reopening headset.");
            reopen_requested = true;
            RequestApply();
        }

        private void session_switch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionUnlock || e.Reason == SessionSwitchReason.SessionLogon)
                RequestApply();
        }

        private void run()
        {
            while (running)
            {
                int wait = poll_interval_ms;
                try
                {
                    if (reopen_requested || device_lost)
                    {
                        reopen_requested = false;
                        close_device();
                    }

                    if (device == null && !open_device())
                    {
                        wait = reconnect_interval_ms;
                    }
                    else
                    {
                        sync_once();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Headset sync error: " + ex.Message);
                    close_device();
                    wait = reconnect_interval_ms;
                }

                if (wake.WaitOne(wait) && running)
                {
                    // Let a burst of UI changes (e.g. dragging a slider) settle before sending
                    Thread.Sleep(debounce_ms);
                }
            }
            close_device();
        }

        private void sync_once()
        {
            bool online = query_battery();
            if (online != headset_online)
            {
                headset_online = online;
                Console.WriteLine(online ? "Headset online, applying settings." : "Headset offline.");
                HeadsetOnlineChanged?.Invoke(online);
                if (online) dirty = true;
            }
            if (!online) return;

            if (dirty || DateTime.Now - last_full_apply > full_reapply_interval)
            {
                apply_all();
            }
            else if (!headset_matches_settings())
            {
                Console.WriteLine("Headset drifted from saved settings, re-applying.");
                apply_all();
            }
        }

        private void apply_all()
        {
            dirty = false; // cleared first so changes made while applying trigger another pass
            bool ok = set_lights();
            ok &= set_sidetone();
            if (ok) last_full_apply = DateTime.Now;
            else dirty = true;
        }

        private bool query_battery()
        {
            byte[] reply = request(feature_battery, fn_battery_get);
            if (reply == null) return false;
            int voltage = (reply[4] << 8) | reply[5];
            return voltage != 0;
        }

        private bool headset_matches_settings()
        {
            foreach (byte zone in LightZones.all)
            {
                byte mode = LightZones.GetMode(zone);
                byte[] reply = request(feature_led, fn_led_get_zone_effect, zone);
                if (reply == null) return false;
                if (reply[4] != zone || reply[5] != mode)
                {
                    Console.WriteLine("Zone " + zone + " effect is " + reply[5] + ", expected " + mode);
                    return false;
                }
            }

            byte[] sidetone = request(feature_sidetone, fn_sidetone_get);
            if (sidetone == null) return false;
            if (sidetone[4] != Properties.Settings.Default.sidetone)
            {
                Console.WriteLine("Sidetone is " + sidetone[4] + ", expected " + Properties.Settings.Default.sidetone);
                return false;
            }
            return true;
        }

        private bool set_lights()
        {
            bool ok = true;
            foreach (byte zone in LightZones.all)
            {
                ok &= set_zone_light(zone);
            }
            return ok;
        }

        private bool set_zone_light(byte zone)
        {
            byte mode = LightZones.GetMode(zone);
            int duration = LightZones.GetDuration(zone);
            byte duration_msb = (byte)((duration & 0xff00) >> 8);
            byte duration_lsb = (byte)(duration & 0xff);
            byte brightness = LightZones.GetBrightness(zone);

            // Gamma-ish correction so the LEDs look like the picked color
            System.Drawing.Color color = LightZones.GetColor(zone);
            byte r = correct_color(color.R);
            byte g = correct_color(color.G);
            byte b = correct_color(color.B);

            // args[0] = zone, args[1] = effect, rest = effect parameters (frame bytes 4..)
            byte[] args = new byte[12];
            args[0] = zone;
            args[1] = mode;

            switch (mode)
            {
                case mode_off:
                    break;
                case mode_fixed:
                    args[2] = r; args[3] = g; args[4] = b;
                    args[5] = 0x02;
                    break;
                case mode_breathing:
                    args[2] = r; args[3] = g; args[4] = b;
                    args[5] = duration_msb; args[6] = duration_lsb;
                    args[8] = brightness;
                    break;
                case mode_cycling:
                    args[7] = duration_msb; args[8] = duration_lsb;
                    args[9] = brightness;
                    break;
                default:
                    return true;
            }

            bool sent = request(feature_led, fn_led_set_zone_effect, args) != null;
            Console.WriteLine("Light zone " + zone + " mode " + mode + " applied: " + sent);
            return sent;
        }

        private static byte correct_color(int c)
        {
            return (byte)(int)(-c * 0.0086 + c * c * 0.0028 + c * c * c * 0.000004);
        }

        private bool set_sidetone()
        {
            bool sent = request(feature_sidetone, fn_sidetone_set, Properties.Settings.Default.sidetone) != null;
            Console.WriteLine("Sidetone " + Properties.Settings.Default.sidetone + " applied: " + sent);
            return sent;
        }

        // Sends a HID++ long report and waits for the matching reply. Returns null on error or timeout.
        private byte[] request(byte feature, byte function, params byte[] args)
        {
            HidDevice dev = device;
            if (dev == null) return null;

            byte[] frame = new byte[dev.Capabilities.OutputReportByteLength];
            frame[0] = 0x11;
            frame[1] = 0xff;
            frame[2] = feature;
            frame[3] = (byte)((function << 4) | software_id);
            Array.Copy(args, 0, frame, 4, Math.Min(args.Length, frame.Length - 4));

            for (int attempt = 0; attempt < request_attempts; attempt++)
            {
                lock (pending_lock)
                {
                    pending_feature = frame[2];
                    pending_function = frame[3];
                    pending_reply = null;
                    pending_error = false;
                    reply_received.Reset();
                }

                if (!dev.Write(frame))
                {
                    Console.WriteLine("HID write failed, receiver lost.");
                    device_lost = true;
                    return null;
                }

                if (reply_received.Wait(reply_timeout_ms))
                {
                    lock (pending_lock)
                    {
                        // An error reply (e.g. headset switched off) won't change on retry
                        return pending_error ? null : pending_reply;
                    }
                }
            }
            Console.WriteLine("No reply to " + BitConverter.ToString(frame, 0, 6));
            return null;
        }

        private bool open_device()
        {
            HidDevice found = null;
            // The original receiver uses PID 0x0AB5. Newer G733 receivers may enumerate as 0x0B1F.
            // Select the HID++ long-report collection (20-byte input and output reports).
            ushort[] receiver_pids = { 0x0ab5, 0x0b1f };
            foreach (ushort pid in receiver_pids)
            {
                foreach (HidDevice candidate in HidDevices.Enumerate(0x046d, pid))
                {
                    if (candidate.Capabilities.OutputReportByteLength == 20 &&
                        candidate.Capabilities.InputReportByteLength == 20)
                    {
                        found = candidate;
                        break;
                    }
                }
                if (found != null) break;
            }
            if (found == null) return false;

            found.OpenDevice();
            if (!found.IsOpen) return false;

            Console.WriteLine("Receiver opened: " + found.DevicePath);
            device = found;
            device_lost = false;
            dirty = true;
            new Thread(() => read_loop(found)) { IsBackground = true, Name = "HeadsetRead" }.Start();
            return true;
        }

        private void close_device()
        {
            HidDevice dev = device;
            device = null;
            if (dev != null)
            {
                try { dev.CloseDevice(); } catch { }
            }
            if (headset_online)
            {
                headset_online = false;
                HeadsetOnlineChanged?.Invoke(false);
            }
        }

        private void read_loop(HidDevice dev)
        {
            while (running && device == dev)
            {
                HidDeviceData report;
                try { report = dev.Read(); }
                catch { break; }

                if (report.Status != HidDeviceData.ReadStatus.Success)
                {
                    if (device != dev) break;
                    if (report.Status == HidDeviceData.ReadStatus.NotConnected || !dev.IsConnected)
                    {
                        device_lost = true;
                        wake.Set();
                        break;
                    }
                    Thread.Sleep(100);
                    continue;
                }
                on_frame(report.Data);
            }
        }

        private void on_frame(byte[] data)
        {
            if (data == null || data.Length < 6) return;
            Console.WriteLine("Received hid frame: " + BitConverter.ToString(data));

            lock (pending_lock)
            {
                if (!reply_received.IsSet)
                {
                    if (data[2] == pending_feature && data[3] == pending_function)
                    {
                        pending_reply = data;
                        reply_received.Set();
                    }
                    else if (data[2] == 0xff && data[3] == pending_feature && data[4] == pending_function)
                    {
                        Console.WriteLine("Headset returned error " + data[5]);
                        pending_error = true;
                        reply_received.Set();
                    }
                }
            }

            FrameReceived?.Invoke(data);
        }
    }
}
