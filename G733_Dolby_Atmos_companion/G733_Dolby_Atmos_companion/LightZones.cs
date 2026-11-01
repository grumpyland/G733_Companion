using System;
using System.Drawing;
using System.Linq;

namespace G733_Companion
{
    // Per-side light settings: top and bottom each keep their own mode, color, duration and brightness.
    internal static class LightZones
    {
        public const byte bottom = 0x00;
        public const byte top = 0x01;
        public const byte both = 0x02;
        public static readonly byte[] all = { bottom, top };

        private static Properties.Settings settings => Properties.Settings.Default;

        // The zones a "Light side to apply" choice refers to
        public static byte[] Selected(byte side)
        {
            return side == both ? all : new[] { side };
        }

        // True when every zone has the same value; value is the first zone's either way
        public static bool Same<T>(byte[] zones, Func<byte, T> get, out T value)
        {
            T first = get(zones[0]);
            value = first;
            return zones.All(z => get(z).Equals(first));
        }

        // Earlier versions had one set of light settings for both sides; copy it to each side once
        public static void Migrate()
        {
            if (settings.light_zones_initialized) return;
            foreach (byte zone in all)
            {
                SetMode(zone, settings.light_mode);
                SetColor(zone, Color.FromArgb(settings.color_R, settings.color_G, settings.color_B));
                SetDuration(zone, settings.light_duration);
                SetBrightness(zone, settings.light_brightness);
            }
            settings.light_zones_initialized = true;
            settings.Save();
        }

        public static byte GetMode(byte zone)
        {
            return zone == top ? settings.top_light_mode : settings.bottom_light_mode;
        }

        public static void SetMode(byte zone, byte mode)
        {
            if (zone == top) settings.top_light_mode = mode;
            else settings.bottom_light_mode = mode;
        }

        public static Color GetColor(byte zone)
        {
            int rgb = zone == top ? settings.top_color : settings.bottom_color;
            return Color.FromArgb((rgb >> 16) & 0xff, (rgb >> 8) & 0xff, rgb & 0xff);
        }

        public static void SetColor(byte zone, Color color)
        {
            int rgb = (color.R << 16) | (color.G << 8) | color.B;
            if (zone == top) settings.top_color = rgb;
            else settings.bottom_color = rgb;
        }

        public static int GetDuration(byte zone)
        {
            return zone == top ? settings.top_light_duration : settings.bottom_light_duration;
        }

        public static void SetDuration(byte zone, int duration)
        {
            if (zone == top) settings.top_light_duration = duration;
            else settings.bottom_light_duration = duration;
        }

        public static byte GetBrightness(byte zone)
        {
            return zone == top ? settings.top_light_brightness : settings.bottom_light_brightness;
        }

        public static void SetBrightness(byte zone, byte brightness)
        {
            if (zone == top) settings.top_light_brightness = brightness;
            else settings.bottom_light_brightness = brightness;
        }
    }
}
