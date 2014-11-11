using System;
using System.Drawing;

namespace SmartQuant.Charting
{
    public class TTextBoxItem
    {
        public string Text { get; set; }

        public Color Color { get; set; }

        public Font Font { get; set; }

        public TTextBoxItem(string text, Color color, Font font)
        {
            Text = text;
            Color = color;
            Font = font;
        }

        public TTextBoxItem(string text, Color color)
            : this(text, color, new Font("Arial", 8f))
        {
        }
    }
}