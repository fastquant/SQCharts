using System;
using Pango;

namespace Compatibility.Gtk
{
    public class Font
    {
        public Font(string family, float size)
        {
            throw new NotImplementedException();
        }

        public FontDescription FontDescription { get; private set; }
    }

    public class Fonts
    {
        public static Font SystemFont()
        {
            return new Font("aaaa", 8);
        }
    }
}

