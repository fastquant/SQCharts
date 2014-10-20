using System;
using System.Drawing;

namespace Compatibility.WinForm
{
    public static class ColorUtils
    {
        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        public static Color FromArgb(int alpha, Color baseColor)
        {
            return Color.FromArgb(alpha, baseColor);
        }

        public static Color Empty()
        {
            return Color.Empty;
        }
    }
}

