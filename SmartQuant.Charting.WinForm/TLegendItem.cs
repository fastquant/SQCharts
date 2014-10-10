using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class TLegendItem
    {
        public string Text { get; set; }

        public Color Color { get; set; }

        public Font Font { get; set; }

        public TLegendItem(string text, Color color, Font font)
        {
            Text = text;
            Color = color;
            Font = font;
        }

        public TLegendItem(string text, Color color)
            : this(text, color, Fonts.SystemFont())
        {
        }
    }
}
