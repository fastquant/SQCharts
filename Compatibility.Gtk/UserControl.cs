using System;
using Gtk;
using MouseButtons = System.Windows.Forms.MouseButtons;
using Image = Gdk.Image;
using Gtk.DotNet;
using System.Drawing;
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
            BorderWidth = 2;
            TypeHint = Gdk.WindowTypeHint.Tooltip;
            AllowShrink = false;
            AllowGrow = false;
            Title = "tooltip"; 
            //fake widget name for stupid theme engines
            Name = "gtk-tooltip";
            this.Label = new Label();
            Add(Label);
        }

        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            int winWidth, winHeight;
            GetSize(out winWidth, out winHeight);
            Style.PaintFlatBox(Style, GdkWindow, StateType.Normal, ShadowType.Out, evnt.Area, this, "tooltip", 0, 0, winWidth, winHeight);
            foreach (var child in Children)
                PropagateExpose(child, evnt);
            return false;
        }

        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
//            if (NudgeHorizontal || NudgeVertical) {
//                int x, y;
//                this.GetPosition (out x, out y);
//                int oldY = y, oldX = x;
//                const int edgeGap = 2;
//
////                Gdk.Rectangle geometry = DesktopService.GetUsableMonitorGeometry (Screen, Screen.GetMonitorAtPoint (x, y));
////                if (NudgeHorizontal) {
////                    if (allocation.Width <= geometry.Width && x + allocation.Width >= geometry.Left + geometry.Width - edgeGap)
////                        x = geometry.Left + (geometry.Width - allocation.Width - edgeGap);
////                    if (x <= geometry.Left + edgeGap)
////                        x = geometry.Left + edgeGap;
////                }
////
////                if (NudgeVertical) {
////                    if (allocation.Height <= geometry.Height && y + allocation.Height >= geometry.Top + geometry.Height - edgeGap)
////                        y = geometry.Top + (geometry.Height - allocation.Height - edgeGap);
////                    if (y <= geometry.Top + edgeGap)
////                        y = geometry.Top + edgeGap;
////                }
////
////                if (y != oldY || x != oldX)
////                    Move (x, y);
//            }
//
            base.OnSizeAllocated(allocation);
        }
    }

    public class ToolTip : Component
    {
        TooltipWindow window;

        public ToolTip()
        {
            window = new TooltipWindow();
        }

        public ToolTip(IContainer container):this()
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
                    window.ShowAll();
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

    public class MouseEventArgs : EventArgs
    {
        public MouseButtons Button { get; private set; }

        public int Clicks { get; private set; }

        public int Delta { get; private set; }

        public System.Drawing.Point Location { get; private set; }

        public int X { get { return (int)Location.X; } }

        public int Y { get { return (int)Location.Y; } }

        public MouseEventArgs(int x, int y)
        {
            Button = MouseButtons.None;
            Clicks = 1;
            Delta = 1;
            Location = new System.Drawing.Point(x, y);
        }

        public static implicit operator MouseEventArgs(global::Gtk.MotionNotifyEventArgs args)
        {
            return new MouseEventArgs((int)args.Event.X, (int)args.Event.Y);
        }

        public static implicit operator MouseEventArgs(global::Gtk.EnterNotifyEventArgs args)
        {
            return new MouseEventArgs((int)args.Event.X, (int)args.Event.Y);
        }

        public static implicit operator MouseEventArgs(global::Gtk.LeaveNotifyEventArgs args)
        {
            return new MouseEventArgs((int)args.Event.X, (int)args.Event.Y);
        }

        public static implicit operator MouseEventArgs(global::Gtk.ScrollEventArgs args)
        {
            return new MouseEventArgs((int)args.Event.X, (int)args.Event.Y);
        }

        public static implicit operator MouseEventArgs(global::Gtk.ButtonPressEventArgs args)
        {
            return FromEventButton(args.Event);
        }

        public static implicit operator MouseEventArgs(global::Gtk.ButtonReleaseEventArgs args)
        {
            return FromEventButton(args.Event);
        }

        public static MouseEventArgs FromEventButton(global::Gdk.EventButton evt)
        {
            return new MouseEventArgs((int)evt.X, (int)evt.Y);
        }
    }

    public class KeyPressEventArgs : EventArgs
    {
        public static implicit operator KeyPressEventArgs(global::Gtk.KeyPressEventArgs args)
        {
            return new KeyPressEventArgs();
        }

        public static implicit operator KeyPressEventArgs(global::Gdk.EventKey evnt)
        {
            return new KeyPressEventArgs();
        }
    }

    public class PaintEventArgs : EventArgs, IDisposable
    {
        public void Dispose()
        {
        }

        public System.Drawing.Rectangle ClipRectangle { get; private set; }

        public System.Drawing.Graphics Graphics { get; private set; }

        public PaintEventArgs(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipRect)
        {
            Graphics = graphics;
            ClipRectangle = clipRect;
        }
    }

    public class UserControl : EventBox
    {
        public string Text { get; set; }

        public virtual System.Drawing.Font Font { get; set; }

        public System.Drawing.Size Size
        { 
            get
            { 
                return Allocation.Size.ToStandardSize(); 
            }
        }

        public System.Drawing.Size ClientSize
        {
            get
            {
                return ClientRectangle.Size;
            }
        }

        public System.Drawing.Rectangle ClientRectangle
        { 
            get
            {
                return Allocation.ToStandardRectangle();
            }
        }

        public int Width
        { 
            get
            { 
                return (int)this.Size.Width; 
            } 
        }

        public int Height
        {
            get
            { 
                return (int)this.Size.Height; 
            } 
        }

        public bool AutoScroll { get; set; }

        public bool Disposing
        {
            get
            {
                return false;
            }
        }

        public System.Drawing.Color BackColor { get; private set; }

        private Tooltip tooltip;
        protected internal DrawingArea drawingArea;

        public UserControl()
        {
            Font = SystemFonts.DefaultFont;
            this.drawingArea = new DrawingArea();
            this.drawingArea.Events = Gdk.EventMask.AllEventsMask;
            Add(drawingArea);
            ShowAll();

            this.drawingArea.ExposeEvent += (o, args) =>
            {
                var da = o as DrawingArea;
                var g = global::Gtk.DotNet.Graphics.FromDrawable(da.GdkWindow);
                var pe = new PaintEventArgs(g, args.Event.Area.ToStandardRectangle());
                this.OnPaintBackground(pe);
                this.OnPaint(pe);
            };

            this.drawingArea.EnterNotifyEvent += (o, args) =>
            {
                this.OnMouseEnter(args);
            };

            this.drawingArea.LeaveNotifyEvent += (o, args) =>
            {
                this.OnMouseLeave(args);
            };

            this.drawingArea.MotionNotifyEvent += (o, args) =>
            {
                this.OnMouseMove(args);
            };

            this.drawingArea.ScrollEvent += (o, args) =>
            {
                this.OnMouseWheel(args);
            };

            this.drawingArea.ButtonPressEvent += (o, args) =>
            {
                var evt = args.Event;
                if (evt.Type == Gdk.EventType.TwoButtonPress)
                    this.OnDoubleClick(args);
                else
                    this.OnMouseDown(args);
            };

            this.drawingArea.ButtonReleaseEvent += (o, args) =>
            {
                this.OnMouseUp(args);
            };

            this.drawingArea.SizeAllocated += (o, args) =>
            {
                this.OnResize(args);
            };

            this.drawingArea.KeyPressEvent += (o, args) =>
            {
                this.OnKeyPress(args);
            };
        }

        public void Invalidate()
        {
            QueueDraw();
        }

        protected override bool OnKeyPressEvent(Gdk.EventKey evnt)
        {
            this.OnKeyPress(evnt);
            return base.OnKeyPressEvent(evnt);
        }

        //TODO: fixme
        public System.Drawing.Graphics CreateGraphics()
        {
            return null;
        }

        #region UserControl Events

        protected virtual void OnPaint(PaintEventArgs pe)
        {
        }

        protected virtual void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        protected virtual void OnMouseEnter(EventArgs e)
        {
        }

        protected virtual void OnMouseLeave(EventArgs e)
        {
        }

        protected virtual void OnDoubleClick(EventArgs e)
        {
        }

        protected virtual void OnResize(EventArgs e)
        {
        }

        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    public class ScrolledUserControl : UserControl
    {
        protected internal HScrollbar scrollbar;

        public ScrolledUserControl()
            : base()
        {   
            Remove(this.drawingArea);
            this.scrollbar = new HScrollbar(new Adjustment(0, 0, 100, 1, 20, 20));
            var vb = new VBox();
            vb.PackStart(this.drawingArea, true, true, 0);
            vb.PackStart(this.scrollbar, false, true, 0);
            Add(vb);
            ShowAll();
        }
    }
}
