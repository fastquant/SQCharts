using System;
using Gtk;
using System.ComponentModel;

namespace Compatibility.Gtk
{
    class TooltipWindow : Window
    {
        internal Label Label;

        public TooltipWindow()
            : base(WindowType.Popup)
        {
            SkipPagerHint = true;
            SkipTaskbarHint = true;
            Decorated = false;
            BorderWidth = 3;
            TypeHint = Gdk.WindowTypeHint.Tooltip;
            AllowShrink = false;
            AllowGrow = false;
            Title = "tooltip"; 
            Name = "gtk-tooltip"; // fake widget name for stupid theme engines
            this.Label = new Label();
            Add(Label);
        }

        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            int width, height;
            GetSize(out width, out height);
            Style.PaintFlatBox(Style, GdkWindow, StateType.Normal, ShadowType.Out, evnt.Area, this, "tooltip", 0, 0, width, height);
            foreach (var child in Children)
                PropagateExpose(child, evnt);
            return false;
        }
    }

    public class ToolTip : Component
    {
        TooltipWindow window;

        private UserControl parent;

        public ToolTip(UserControl parent)
        {
            this.parent = parent;
            window = new TooltipWindow();
        }

        public ToolTip(IContainer container, UserControl parent)
            : this(parent)
        {
            container.Add(this);
        }

        public void SetToolTip(UserControl control, string caption)
        {
            this.window.Label.Text = caption;
        }

        public bool Active
        {
            get
            {
                return window.Visible;
            }
            set
            { 
                if (value)
                {
                    if (parent.Cursor != null)
                        window.Move(parent.Cursor.Position.X, parent.Cursor.Position.Y);

                    window.ShowAll();
                }
                else
                    window.Hide();
            }
        }

        public new void Dispose()
        {   
            window.Destroy();
            base.Dispose();
        }
    }

}

