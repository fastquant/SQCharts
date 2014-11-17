using System;
using Gtk;

namespace Compatibility.Gtk
{
    public class NoPaintUserControl : EventBox
    {
        public NoPaintUserControl()
            : base()
        {
            Events = Gdk.EventMask.AllEventsMask;
        }
    }
}

