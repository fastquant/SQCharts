using System;
using Gtk;
using System.Collections.Generic;

namespace Compatibility.Gtk
{
    public static class  Extensions
    {
        public static System.Drawing.Rectangle ToStandardRectangle(this Gdk.Rectangle rect)
        {
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static System.Drawing.Size ToStandardSize(this Gdk.Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        public static List<string> GetTexts(this ComboBox cbx)
        {
            var texts = new List<string>();
            var store = cbx.Model as ListStore;
            foreach (object[] row in store)
                texts.Add(row[0].ToString());
            return texts;
        }

        public static bool ContainsText(this ComboBox cbx, string text)
        {
            var texts = cbx.GetTexts();
            return texts.Contains(text);
        }

        public static void ClearTexts(this ComboBox cbx)
        {
            for (int i = 0; i < cbx.Model.IterNChildren(); ++i)
                cbx.RemoveText(0);
        }
    }
}

