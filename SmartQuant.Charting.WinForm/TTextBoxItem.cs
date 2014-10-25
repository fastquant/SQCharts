using System;
#if XWT
using Xwt.Drawing;
using Compatibility.Xwt;
#elif GTK
using Gdk;
using Compatibility.Gtk;
using Font = Compatibility.Gtk.Font;
#else
using System.Drawing;
using Compatibility.WinForm;
#endif

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
            : this(text, color, Fonts.SystemFont())
        {
        }
    }
}