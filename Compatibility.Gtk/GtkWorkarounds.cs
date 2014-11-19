using System;
using Gtk;

namespace Compatibility.Gtk
{
    public static class GtkWorkarounds
    {
        /// <summary>Shows a context menu.</summary>
        /// <param name='menu'>The menu.</param>
        /// <param name='parent'>The parent widget.</param>
        /// <param name='evt'>The mouse event. May be null if triggered by keyboard.</param>
        /// <param name='caret'>The caret/selection position within the parent, if the EventButton is null.</param>
        public static void ShowContextMenu (Menu menu, Widget parent, Gdk.EventButton evt, Gdk.Rectangle caret)
        {
            MenuPositionFunc posFunc = null;

            if (parent != null) {
                menu.AttachToWidget (parent, null);
                menu.Hidden += (sender, e) => {
                    if (menu.AttachWidget != null)
                        menu.Detach ();
                };
                posFunc = delegate (Menu m, out int x, out int y, out bool pushIn) {
                    Gdk.Window window = evt != null? evt.Window : parent.GdkWindow;
                    window.GetOrigin (out x, out y);
                    var alloc = parent.Allocation;
                    if (evt != null) {
                        x += (int) evt.X;
                        y += (int) evt.Y;
                    } else if (caret.X >= alloc.X && caret.Y >= alloc.Y) {
                        x += caret.X;
                        y += caret.Y + caret.Height;
                    } else {
                        x += alloc.X;
                        y += alloc.Y;
                    }
                    Requisition request = m.SizeRequest ();
                    var screen = parent.Screen;
                    Gdk.Rectangle geometry = screen.GetMonitorGeometry(screen.GetMonitorAtPoint (x, y));

                    //whether to push or flip menus that would extend offscreen
                    //FIXME: this is the correct behaviour for mac, check other platforms
                    bool flip_left = true;
                    bool flip_up   = false;

                    if (x + request.Width > geometry.X + geometry.Width) {
                        if (flip_left) {
                            x -= request.Width;
                        } else {
                            x = geometry.X + geometry.Width - request.Width;
                        }

                        if (x < geometry.Left)
                            x = geometry.Left;
                    }

                    if (y + request.Height > geometry.Y + geometry.Height) {
                        if (flip_up) {
                            y -= request.Height;
                        } else {
                            y = geometry.Y + geometry.Height - request.Height;
                        }

                        if (y < geometry.Top)
                            y = geometry.Top;
                    }

                    pushIn = false;
                };
            }

            uint time;
            uint button;

            if (evt == null) {
                time = Global.CurrentEventTime;
                button = 0;
            } else {
                time = evt.Time;
                button = evt.Button;
            }

            //HACK: work around GTK menu issues on mac when passing button to menu.Popup
            //some menus appear and immediately hide, and submenus don't activate
//            if (Platform.IsMac) {
//                button = 0;
//            }

            menu.Popup (null, null, posFunc, button, time);
        }

    }
}

