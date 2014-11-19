using System;
using Gtk;

namespace Compatibility.Gtk
{
    public class Form : Window
    {
        public Form()
            : base(WindowType.Toplevel)
        {
        }

        public string Text { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public void Update()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }

}

