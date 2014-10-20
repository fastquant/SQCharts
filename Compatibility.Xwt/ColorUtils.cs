using System;
using Xwt.Drawing;

namespace Compatibility.Xwt
{
    public static class ColorUtils
    {
        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromBytes((byte)red, (byte)green, (byte)blue);
        }

        public static Color FromArgb(int alpha, Color baseColor)
        {
            return Color.FromBytes((byte)(baseColor.Red * byte.MaxValue), (byte)(baseColor.Green * byte.MaxValue), (byte)(baseColor.Blue * byte.MaxValue), (byte)(baseColor.Alpha * byte.MaxValue));
        }

        public static Color Empty()
        {
            return Colors.Red;
        }
    }
}

