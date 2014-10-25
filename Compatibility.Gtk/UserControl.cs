using System;

using Gdk;
using Gtk;
using Point = Gdk.Point;
using EmfType = System.Drawing.Imaging.EmfType;
using MouseButtons = System.Windows.Forms.MouseButtons;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using SmoothingMode = System.Drawing.Drawing2D.SmoothingMode;
using TextRenderingHint = System.Drawing.Text.TextRenderingHint;
using Image = Gdk.Image;

namespace Compatibility.Gtk
{
    public sealed class ImageFormat
    {
    }

    public class Pen
    {
        public Pen() : this(Color.Zero, 1)
        {
        }
        public Pen(Color color) : this(color, 1)
        {
        }

        public Color Color { get; set; }

        public float Width { get; set; }

        public Pen(Color color, float width)
        {
            Color = color;
            Width = width;
        }
    }

    public class Brush
    {
        public Color Color { get; set; }
    }

    public class SolidBrush : Brush
    {

        public SolidBrush(Color color)
        {
            Color = color;
        }
    }

    public class Bitmap
    {
        public Bitmap(Metafile metafile)
        {
            throw new NotImplementedException();
        }
    }

    public class Metafile
    {
    }

    public class ToolTip
    {
    }

    public class PrintDocument
    {
        public void Print()
        {
            throw new NotImplementedException();
        }
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

        public Point Location { get; private set; }

        public int X { get { return (int)Location.X; } }

        public int Y { get { return (int)Location.Y; } }

        public MouseEventArgs(int x, int y)
        {
            Button = MouseButtons.None;
            Clicks = 1;
            Delta = 1;
            Location = new Point(x, y);
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

    public sealed class GraphicsPath
    {
        public void AddLine(Point pt1, Point pt2)
        {
            this.AddLine(pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public void AddLine(int x1, int y1, int x2, int y2)
        {
        }
    }

    public struct PointF
    {
        public static implicit operator PointF(Point p)
        {
            return new PointF();
        }
    }
    public struct SizeF
    {
        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }
    }

    public class Graphics
    {
        private Rectangle rect;
        private Widget widget;
        private Cairo.Context ctx;

        public Graphics(Widget widget, Cairo.Context ctx, Rectangle rect)
        {
            this.widget = widget;
            this.ctx = ctx;
            this.rect = rect;
        }

        public TextRenderingHint TextRenderingHint
        {
            get;
            set;
        }

        public SmoothingMode SmoothingMode
        {
            get;
            set;
        }

        public void Clear(Color color)
        {
            ctx.Rectangle (0, 0, rect.Width, rect.Height);
            CairoHelper.SetSourceColor(ctx, color);
            ctx.Fill();           
        }

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float heigth)
        {
            this.ctx.Rectangle(x, y, width, heigth);
            this.ctx.Fill();
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.ctx.LineWidth = pen.Width;
            this.ctx.Save();
            this.ctx.MoveTo(x1, y1);
            this.ctx.LineTo(x2, y2);
            this.ctx.Restore();
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image image, float x, float y)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Pen pen, PointF[] points)
        {
            throw new NotImplementedException();
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString (string text, Font font)
        {
            return new SizeF();
        }
    }

    public class PaintEventArgs : EventArgs, IDisposable
    {
        public void Dispose()
        {
        }

        public Rectangle ClipRectangle { get; private set; }

        public Graphics Graphics { get; private set; }

        public PaintEventArgs(Graphics graphics, Rectangle clipRect)
        {
            Graphics = graphics;
            ClipRectangle = clipRect;
        }
    }

    public delegate void PaintEventHandler(object sender,PaintEventArgs e);

    public class UserControl : ScrolledWindow
    {
        public string Text { get; set; }

        public Size Size
        { 
            get
            { 
                return Allocation.Size; 
            }
        }

        public Size ClientSize 
        {
            get
            {
                return ClientRectangle.Size;
            }
        }

        public Rectangle ClientRectangle 
        { 
            get
            {
                return Allocation;
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

        public Color BackColor { get; private set; }

        private DrawingArea drawingArea;
        private Tooltip tooltip;

        public UserControl()
        {
            ShadowType = ShadowType.None;
            Viewport vp = new Viewport();
            vp.ShadowType = ShadowType.None;
            this.drawingArea = new DrawingArea();
            this.drawingArea.Events = EventMask.AllEventsMask;
            var eventBox = new EventBox();
            eventBox.Add(this.drawingArea);
            vp.Add(eventBox);
            this.Add(vp);
            if (this.Child != null)
                this.Child.ShowAll();
            this.drawingArea.ExposeEvent += HandleExposeEvent;
//            this.ButtonPressEvent += OnButtonPressed;
            this.drawingArea.EnterNotifyEvent += HandleEnterNotifyEvent;
            this.drawingArea.TooltipText = "ddddd";
//            this.drawingArea..
            //tooltip = new Tooltip();
            Events = EventMask.AllEventsMask;
            this.SizeAllocated += HandleSizeAllocated;
            this.Hide();
        }

        private void HandleSizeAllocated(object o, SizeAllocatedArgs args)
        {
//            Console.WriteLine("HandleSizeAllocated:{0}", args.Allocation);
        }

        private void HandleEnterNotifyEvent(object o, EnterNotifyEventArgs args)
        {
            //  Console.WriteLine("HandleEnterNotifyEvent");
        }

        private void HandleExposeEvent(object sender, ExposeEventArgs args)
        {
            var obj = sender as DrawingArea;
            using (var cr = Gdk.CairoHelper.Create(args.Event.Window))
            {
                var area = args.Event.Area;
                var pe = new PaintEventArgs(new Graphics(obj, cr, area), area);
                this.OnPaintBackground(pe);
                this.OnPaint(pe);
            }
        }

        protected void OnButtonPressed(object sender, ButtonPressEventArgs a)
        {
//            Console.WriteLine("OnButtonPressed");
        }

        #region Native Events Dispatch

        protected override bool OnEnterNotifyEvent(EventCrossing evnt)
        {
//            Console.WriteLine("OnEnterNotifyEvent");
            return true;
        }

        protected override bool OnLeaveNotifyEvent(EventCrossing evnt)
        {
//            Console.WriteLine("OnLeaveNotifyEvent");
            return base.OnLeaveNotifyEvent(evnt);
        }

        protected override bool OnMotionNotifyEvent(EventMotion evnt)
        {
//            Console.WriteLine("OnMotionNotifyEvent");
            return true;
        }

        protected override bool OnButtonPressEvent(EventButton evnt)
        {
//            Console.WriteLine("OnButtonPressEvent");
            return true;

        }

        protected override bool OnButtonReleaseEvent(EventButton evnt)
        {
//            Console.WriteLine("OnButtonReleaseEvent");
            return true;

        }

        protected override bool OnScrollEvent(EventScroll evnt)
        {
//            Console.WriteLine("OnScrollEvent");
            return true;
        }

        protected override bool OnKeyPressEvent(EventKey evnt)
        {
//            Console.WriteLine("OnKeyPressEvent");
            return base.OnKeyPressEvent(evnt);
        }

        //     protected override

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
