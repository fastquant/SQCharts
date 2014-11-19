using System;
using Gtk;
using MouseButtons = System.Windows.Forms.MouseButtons;
using Image = Gdk.Image;
using Gtk.DotNet;
using System.Drawing;
using System.ComponentModel;

namespace Compatibility.Gtk
{
    public class MouseEventArgs : EventArgs
    {
        public Gdk.EventButton GdkEventButton { get; set; }

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
            var evt = args.Event;
            var me = new MouseEventArgs((int)evt.X, (int)evt.Y);
            me.Delta = evt.Direction == Gdk.ScrollDirection.Up ? 120 : -120;
            return me;
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
            var me = new MouseEventArgs((int)evt.X, (int)evt.Y) { GdkEventButton = evt };
            if (evt.Button == 1)
                me.Button = System.Windows.Forms.MouseButtons.Left;
            if (evt.Button == 3)
                me.Button = System.Windows.Forms.MouseButtons.Right;
             return me;
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

    public sealed class Cursor
    {
        public Point Position { get; set; }

        public Size Size { get; set; }

        public Cursor()
        {
            Position = Point.Empty;
            Size = Size.Empty;
        }
    }

    public class UserControl : EventBox
    {
        public static Gdk.Cursor CrossCursor = new Gdk.Cursor(Gdk.CursorType.Cross);
        public static Gdk.Cursor VSplitterCursor = new Gdk.Cursor(Gdk.CursorType.SbVDoubleArrow);
        public static Gdk.Cursor HandCursor = new Gdk.Cursor(Gdk.CursorType.Hand1);

        public Cursor Cursor { get; set; }

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
            Cursor = new Cursor();
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
                var e = args.Event;
                Cursor.Position = new Point((int)e.XRoot, (int)e.YRoot + 16);
                this.OnMouseMove(args);
            };

            this.drawingArea.ScrollEvent += (o, args) =>
            {
                this.OnMouseWheel(args);
            };

            this.drawingArea.ButtonPressEvent += (o, args) =>
            {
                var evt = args.Event;
                if (evt.Button == 1 && evt.Type == Gdk.EventType.TwoButtonPress)
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
