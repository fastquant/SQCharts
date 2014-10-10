using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class TTitleItem
    {
        public string Text { get; set; }

        public Color Color { get; set; }

        public TTitleItem() : this("", Colors.Black)
        {
        }

        public TTitleItem(string text): this(text, Colors.Black)
        {
        }

        public TTitleItem(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
