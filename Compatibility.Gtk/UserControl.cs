using System;

//using Gdk;
using Gtk;
using MouseButtons = System.Windows.Forms.MouseButtons;
using Image = Gdk.Image;
using Gtk.DotNet;
using System.Drawing;

namespace Compatibility.Gtk
{
    public static class GraphicsExtention
    {
        public static System.Drawing.Rectangle ToStandardRectangle(this Gdk.Rectangle rect)
        {
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static System.Drawing.Size ToStandardSize(this Gdk.Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }
    }

    public class ToolTip
    {
    }

    public class Form : Window
    {
        public Form()
            : base(WindowType.Toplevel)
        {
        }

        public string Name { get; set; }

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

    public class UserControl : ScrolledWindow
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

        public bool InvokeRequired { get; set; }

        public System.Drawing.Color BackColor { get; private set; }

        private Tooltip tooltip;
        private DrawingArea drawingArea;

        public UserControl()
        {
            this.drawingArea = new DrawingArea();
            this.drawingArea.Events = Gdk.EventMask.AllEventsMask;
            var eb = new EventBox();
            eb.Add(this.drawingArea);
            var vp = new Viewport();
            vp.ShadowType = ShadowType.None;
            vp.Add(eb);
            this.Add(vp);
            Child.ShowAll();

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

//            this.drawingArea.KeyPressEvent += (o, args) =>
//            {
//                Console.WriteLine("KeyPressEvent");
//                this.OnKeyPress(args);
//            };

            Hide();
        }

        public object Invoke(Delegate method, params object[] args)
        {
            //Gtk.Application
            return null;
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
}
