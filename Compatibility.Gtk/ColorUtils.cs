using System;
using Gdk;

namespace Compatibility.Gtk
{
    public static class ColorUtils
    {
        public static Cairo.Color ToCairoColor(this Color color)
        {
            return new Cairo.Color((double)color.Red / ushort.MaxValue, (double)color.Green / ushort.MaxValue, (double)color.Blue / ushort.MaxValue);
        }

        public static Color FromBytes(byte red, byte green, byte blue, byte alpha)
        {
            var mul = ushort.MaxValue / byte.MaxValue;
            return new Color() { Red = (ushort)(red * mul), Green = (ushort)(green * mul), Blue = (ushort)(blue * mul) };
        }

        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            throw new NotImplementedException();
        }

        public static Color FromArgb(int red, int green, int blue)
        {
            return FromArgb(255, red, green, blue);
        }

        public static Color FromArgb(int alpha, Color baseColor)
        {
            return new Color() { Red = (ushort)baseColor.Red, Green = (ushort)baseColor.Green, Blue = (ushort)baseColor.Blue };
        }

        public static Color Empty()
        {
            return Color.Zero;
        }
    }
}

