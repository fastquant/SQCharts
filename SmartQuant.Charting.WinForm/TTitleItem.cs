using System;
#if XWT
using Xwt.Drawing;
#elif GTK
using Gdk;
using Compatibility.Gtk;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
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
