using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HidLibrary;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Management;

namespace G733_Companion
{
    public partial class Form1 : Form
    {
        private readonly HeadsetSync headset = new HeadsetSync();

        private const byte top = 0x01;
        private const byte bottom = 0x00;
        private const byte both = 0x02;
        private const byte off = 0x00;
        private const byte fix = 0x01;
        private const byte breathing = 0x02;
        private const byte cycling = 0x03;

        private int voltage = 0;
        private bool is_headset_plugged = false;
        private bool is_headset_connected = false;

        Graphics canvas;
        Bitmap iconBitmap = new Bitmap(32, 32);
        Color pink = Color.FromArgb(255, 0, 76);
        Color green = Color.FromArgb(0, 200, 0);
        StringFormat format = new StringFormat();
        int percent = 0;
        double estim = 0;

        public Form1()
        {
            InitializeComponent();
           
            colorDialog1.AllowFullOpen = true;
            pictureBox1.BackColor = colorDialog1.Color;
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "Disconnected";

            label1.Text = trackBar1.Value.ToString() + " ms";
            label2.Text = trackBar_brightness.Value.ToString() + " %";
            label6.Text = trackBar2.Value.ToString() + " %";
            label11.Text = "";

            trackBar1.Value = Properties.Settings.Default.light_duration;
            trackBar2.Value = Properties.Settings.Default.sidetone;
            trackBar_brightness.Value = Properties.Settings.Default.light_brightness;

            checkBox1.Checked = Properties.Settings.Default.startup_launch;

            Color backup = Color.FromArgb(Properties.Settings.Default.color_R, Properties.Settings.Default.color_G, Properties.Settings.Default.color_B);

            pictureBox1.BackColor = backup;

            progressBar1.Visible = false;

            switch (Properties.Settings.Default.light_mode)
            {
                case off: radioButton_off.Checked = true;
                    break;
                case fix: radioButton_fixed.Checked = true;
                    break;
                case breathing: radioButton_breathing.Checked = true;
                    break;
                case cycling: radioButton_cycling.Checked = true;
                    break;
                default: break;
            }
            switch (Properties.Settings.Default.light_side)
            {
                case both: radioButton_both.Checked = true;
                    break;
                case top: radioButton_top.Checked = true;
                    break;
                case bottom: radioButton_down.Checked = true;
                    break;
                default: break;
            }

            canvas = Graphics.FromImage(iconBitmap);
            format.Alignment = StringAlignment.Center;

            notify_percentage();
            TrayMenuContext();

            headset.FrameReceived += process_frame;
            headset.HeadsetOnlineChanged += headset_online_changed;
            this.FormClosed += (s, e) => headset.Stop();
            headset.Start();
            HID_read_timer.Enabled = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void TrayMenuContext()
        {
            this.notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, this.MenuExit_Click);
            //this.notify_icon.ContextMenuStrip.Items.Add("Test2", null, this.MenuTest2_Click);
            //this.notify_icon.ContextMenuStrip.Items.Add("Exit", null, this.MenuExit_Click);
        }

        void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonApplySettings_Click(object sender, EventArgs e)
        {
            headset.RequestApply();
        }

        private void headset_online_changed(bool online)
        {
            if (online) return; // battery frames mark us connected once a voltage arrives
            voltage = 0;
            is_headset_connected = false;
        }

        private void headset_connected_check_Tick(object sender, EventArgs e)
        {
            notify_percentage();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Value = (trackBar1.Value / 100) * 100;
        }

        private void trackBar_brightness_Scroll(object sender, EventArgs e)
        {
            trackBar_brightness.Value = (trackBar_brightness.Value / 5) * 5;     
        }

        private void radioButton_fixed_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_fixed.Checked) return;
            Properties.Settings.Default.light_mode = fix;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_breathing_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_breathing.Checked) return;
            Properties.Settings.Default.light_mode = breathing;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_cycling_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_cycling.Checked) return;
            Properties.Settings.Default.light_mode = cycling;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_off_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_off.Checked) return;
            Properties.Settings.Default.light_mode = off;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_both_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_both.Checked) return;
            Properties.Settings.Default.light_side = both;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_top_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_top.Checked) return;
            Properties.Settings.Default.light_side = top;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void radioButton_down_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton_down.Checked) return;
            Properties.Settings.Default.light_side = bottom;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void buttonPickcolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;
            Console.WriteLine(colorDialog1.Color);
            pictureBox1.BackColor = colorDialog1.Color;
            Properties.Settings.Default.color_R = colorDialog1.Color.R;
            Properties.Settings.Default.color_G = colorDialog1.Color.G;
            Properties.Settings.Default.color_B = colorDialog1.Color.B;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
        }

        private void HID_read_timer_Tick(object sender, EventArgs e)
        {
            if (voltage == 0) label11.Text = "Disconnected";
            else label11.Text = voltage.ToString() + " mV";

            if (is_headset_connected == true)
            {
                pictureBox2.BackColor = Color.Green;
            }
            else
            {
                pictureBox2.BackColor = Color.Red;
                notifyIcon1.Text = "Disconnected";
            }

            if( is_headset_plugged == true)
            {
                //progressBar1.SetState(2);
                customProgressBar1.ForeColor = Color.FromArgb(155, 155, 0);
                customProgressBar1.BackColor = Color.FromArgb(200, 200, 0);
                //ProgressBarColor.SetState(progressBar1, 2);

            }
            else
            {
                //progressBar1.SetState(1);
                //ProgressBarColor.SetState(progressBar1, 1);
                customProgressBar1.ForeColor = Color.FromArgb(0, 120, 0);
                customProgressBar1.BackColor = Color.FromArgb(0, 172, 0);
            }
        }

        // Runs on the HID read thread for every frame from the headset; settings are pushed by HeadsetSync.
        private void process_frame(byte[] data)
        {
            if (data[2] == 0x08 && ((data[3] & 0xf0) == 0x00))
            {
                voltage = data[4] << 8;
                voltage |= data[5];

                switch(data[6])
                {
                    case 0x01: is_headset_plugged = false;
                        break;
                    case 0x03:
                    case 0x07: is_headset_plugged = true;
                        //voltage -= 370;
                        break;
                    default: break;
                }
                
                Console.WriteLine("Battery voltage: " + voltage);

                is_headset_connected = voltage != 0;
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label6.Text = trackBar2.Value.ToString() + " %";
            Properties.Settings.Default.sidetone = (byte)trackBar2.Value;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString() + " ms";
            Properties.Settings.Default.light_duration = trackBar1.Value;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void trackBar_brightness_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = trackBar_brightness.Value.ToString() + " %";
            Properties.Settings.Default.light_brightness = (byte)trackBar_brightness.Value;
            Properties.Settings.Default.Save();
            headset.RequestApply();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notify_percentage()
        { 
            if (voltage == 0)
            {
                notifyIcon1.Icon = G733_Companion.Properties.Resources.icon;
                progressBar1.Value = 0;
                customProgressBar1.Value = 0;
                label10.Text = "Approx -- hours";
                label9.Text = "--%";
            }
            else
            {   
                // alternate percentage calculation methods?
                // percent = (int)(3451.853 - 2.885 * voltage + 0.00078188 * voltage * voltage - 0.000000067828 * voltage * voltage * voltage);
                // percent = (int)(0.2 * voltage - 740);
                percent = (int)(0.1667 * voltage - 608.33);
                if (percent >= 60)
                {
                    try
                    {
                        if (is_headset_plugged == true && percent >= 80) notifyIcon1.Icon = G733_Companion.Properties.Resources.green_charging;
                        else if (is_headset_plugged == true) notifyIcon1.Icon = G733_Companion.Properties.Resources.orange_charging;
                        else notifyIcon1.Icon = G733_Companion.Properties.Resources.green;
                    }
                    catch
                    {
                        Console.WriteLine("Exception catched, icon related. I still don't know what happens....");
                    }
                }
                else if(percent >= 20)
                {
                    try
                    {
                        if (is_headset_plugged == true && percent >= 40) notifyIcon1.Icon = G733_Companion.Properties.Resources.orange_charging;
                        else if (is_headset_plugged == true) notifyIcon1.Icon = G733_Companion.Properties.Resources.red_charging;
                        else notifyIcon1.Icon = G733_Companion.Properties.Resources.orange;
                    }
                    catch
                    {
                        Console.WriteLine("Exception catched, icon related. I still don't know what happens....");
                    }
                }
                else
                {
                    try
                    {
                        if (is_headset_plugged == true) notifyIcon1.Icon = G733_Companion.Properties.Resources.red_charging;
                        else notifyIcon1.Icon = G733_Companion.Properties.Resources.red;
                    }
                    catch
                    {
                        Console.WriteLine("Exception catched, icon related. I still don't know what happens....");
                    }
                }
                
                estim = (29 * percent / 100);
                //progressBar1.Value = Math.Min(percent, 100);
                percent = Math.Max(0, percent);
                customProgressBar1.Value = Math.Min(percent, 100);
                label9.Text = Math.Min(percent, 100) + "%";
                label10.Text = "Approx " + (int)estim + " hours";

                //Something must be wrong with their ADC ref voltage while charging .... I've measured a ~370mV offset while charging
                if (is_headset_plugged == true && voltage >= 4270)
                {
                    label8.Text = "Battery - Charged";
                    notifyIcon1.Text = voltage + " mV / " + percent + "% / Charged !";
                }
                else if (is_headset_plugged == true)
                {
                    label8.Text = "Battery - Charging";
                    notifyIcon1.Text = voltage + " mV / " + percent + "% / Charging...";
                }
                else
                {
                    label8.Text = "Battery";
                    notifyIcon1.Text = voltage + " mV / " + percent + "% / Approx " + (int)estim + " hours";
                }
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            else if (e.Button == MouseButtons.Right)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                Properties.Settings.Default.startup_launch = true;
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("G733_Dolby_Companion", Application.ExecutablePath);
            }
            else
            {
                Properties.Settings.Default.startup_launch = false;
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("G733_Dolby_Companion", false);
            }
            Properties.Settings.Default.Save();
        }
    }
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            double scaleFactor = (((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum));
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);
            rec.Width = (int)((rec.Width * scaleFactor) - 4);
            if (rec.Width == 0) rec.Width = 1;
            rec.Height -= 4;
            if (rec.Height == 0) rec.Height = 1;
            LinearGradientBrush brush = new LinearGradientBrush(rec, this.ForeColor, this.BackColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
        }
    }
}
