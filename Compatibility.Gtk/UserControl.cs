using System;

using Gdk;
using Gtk;
using MouseButtons = System.Windows.Forms.MouseButtons;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
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
        
    public class Form : Gdk.Window
    {
        public Form()
            : base(null, Gdk.WindowAttr.Zero, 0)
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

        //        public static implicit operator MouseEventArgs(MouseMovedEventArgs args)
        //        {
        //            return new MouseEventArgs((int)args.X, (int)args.Y);
        //        }
        //
        //        public static implicit operator MouseEventArgs(MouseScrolledEventArgs args)
        //        {
        //            return new MouseEventArgs((int)args.X, (int)args.Y);
        //        }
        //
        //        public static implicit operator MouseEventArgs(ButtonEventArgs args)
        //        {
        //            var me = new MouseEventArgs((int)args.X, (int)args.Y);
        //            me.Clicks = args.MultiplePress;
        //            return me;
        //        }
    }

    public class PrintPageEventArgs : EventArgs
    {
    }

    public class KeyPressEventArgs : EventArgs
    {
        //            public static implicit operator KeyPressEventArgs(KeyEventArgs args)
        //            {
        //                throw new NotImplementedException();
        //            }
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

        public UserControl()
        {
            Events = EventMask.AllEventsMask;
        }

        protected override void OnSizeAllocated(Gdk.Rectangle allocation)
        {
            Console.WriteLine("HandleSizeAllocated:{0}", allocation);
            base.OnSizeAllocated(allocation);
        }

        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            var g = global::Gtk.DotNet.Graphics.FromDrawable(GdkWindow);
            var pe = new PaintEventArgs(g, evnt.Area.ToStandardRectangle());
            this.OnPaintBackground(pe);
            this.OnPaint(pe);
            return base.OnExposeEvent(evnt);
        }

        protected override bool OnButtonPressEvent(Gdk.EventButton evnt)
        {
            Console.WriteLine("OnButtonPressed");
            return base.OnButtonPressEvent(evnt);
        }

        #region Native Events Dispatch

        protected override bool OnEnterNotifyEvent(Gdk.EventCrossing evnt)
        {
            Console.WriteLine("OnEnterNotifyEvent");
            return true;
        }

        protected override bool OnLeaveNotifyEvent(Gdk.EventCrossing evnt)
        {
            Console.WriteLine("OnLeaveNotifyEvent");
            return base.OnLeaveNotifyEvent(evnt);
        }

        protected override bool OnMotionNotifyEvent(Gdk.EventMotion evnt)
        {
            Console.WriteLine("OnMotionNotifyEvent");
            return base.OnMotionNotifyEvent(evnt);
        }

//        protected override bool OnButtonPressEvent(Gdk.EventButton evnt)
//        {
//            Console.WriteLine("OnButtonPressEvent");
//            return base.OnButtonPressEvent(evnt);
//
//        }

        protected override bool OnButtonReleaseEvent(Gdk.EventButton evnt)
        {
//            Console.WriteLine("OnButtonReleaseEvent");
            return base.OnButtonReleaseEvent(evnt);
        }

        protected override bool OnScrollEvent(Gdk.EventScroll evnt)
        {
            Console.WriteLine("OnScrollEvent");
            return base.OnScrollEvent(evnt);
        }

        protected override bool OnKeyPressEvent(Gdk.EventKey evnt)
        {
//            Console.WriteLine("OnKeyPressEvent");
            return base.OnKeyPressEvent(evnt);
        }

        #endregion

        public object Invoke(Delegate method)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaint(PaintEventArgs pe)
        {
            //            Console.WriteLine("OnPaint");
        }

        public void Invalidate()
        {
            QueueDraw();
        }

        protected virtual void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseMove");
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseWheel");
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseDown");
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseUp");
        }

        protected virtual void OnMouseLeave(EventArgs e)
        {
            //            Console.WriteLine("OnMouseLeave");
        }

        protected virtual void OnDoubleClick(EventArgs e)
        {
            //            Console.WriteLine("OnDoubleClick");
        }

        protected virtual void OnResize(EventArgs e)
        {
            //            Console.WriteLine("ScreenBounds:{0}", this.ScreenBounds);
            //            Console.WriteLine("Bounds:{0}", this.Bounds);
        }

        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            Console.WriteLine("OnKeyPress");
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
