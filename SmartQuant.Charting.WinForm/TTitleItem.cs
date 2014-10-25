using System;
#if GTK
using Compatibility.Gtk;
#else
using Compatibility.WinForm;
#endif
using System.Drawing;

namespace SmartQuant.Charting
{
    public class TTitleItem
    {
        public string Text { get; set; }

        public Color Color { get; set; }

        public TTitleItem() : this("", Color.Black)
        {
        }

        public TTitleItem(string text): this(text, Color.Black)
        {
        }

        public TTitleItem(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
