#if XWT
using Xwt.Drawing;

namespace SmartQuant.Charting
{
    static class ColorUtils
    {
        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromBytes((byte)red, (byte)green, (byte)blue);
        }

        public static Color Empty()
        {
            return Colors.Red;
        }
    }
}

#else
using System.Drawing;

namespace SmartQuant.Charting
{
    static class ColorUtils
    {
        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        public static Color Empty()
        {
            return Color.Empty;
        }
    }
}
#endif

