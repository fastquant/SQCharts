using System;
using System.Drawing;

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
            : this(text, color,  new Font("Arial", 8f))
        {
        }
    }
}
