using System;

using Xwt;
using Xwt.Drawing;
using EmfType = System.Drawing.Imaging.EmfType;
using MouseButtons = System.Windows.Forms.MouseButtons;

namespace Compatibility.Xwt
{
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

        public static implicit operator MouseEventArgs(MouseMovedEventArgs args)
        {
            return new MouseEventArgs((int)args.X, (int)args.Y);
        }

        public static implicit operator MouseEventArgs(MouseScrolledEventArgs args)
        {
            return new MouseEventArgs((int)args.X, (int)args.Y);
        }

        public static implicit operator MouseEventArgs(ButtonEventArgs args)
        {
            var me = new MouseEventArgs((int)args.X, (int)args.Y);
            me.Clicks = args.MultiplePress;
            return me;
        }
    }

    public class PrintPageEventArgs : EventArgs
    {
    }

    public class KeyPressEventArgs : EventArgs
    {
        public static implicit operator KeyPressEventArgs(KeyEventArgs args)
        {
            throw new NotImplementedException();
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

    public delegate void PaintEventHandler(object sender, PaintEventArgs e);

    class ScrollableCanvas : Canvas
    {
        private ScrollAdjustment hscroll;
        private ScrollAdjustment vscroll;

        public event PaintEventHandler PaintBackground;
        public event PaintEventHandler Paint;

        public ScrollableCanvas()
            : base()
        {
        }

        protected override void OnDraw(Context ctx, Rectangle dirtyRect)
        {
            var pe = new PaintEventArgs(new Graphics(ctx, dirtyRect), dirtyRect);

            if (PaintBackground != null)
                PaintBackground(this, pe);
            if (Paint != null)
                Paint(this, pe);

//            ctx.Save ();
//            ctx.Translate (-hscroll.Value, -vscroll.Value);
//            ctx.Rectangle (new Rectangle (0, 0, imageSize, imageSize));
//            ctx.SetColor (Xwt.Drawing.Colors.White);
//            ctx.Fill ();
//            ctx.Arc (imageSize / 2, imageSize / 2, imageSize / 2 - 20, 0, 360);
//            ctx.SetColor (new Color (0,0,1));
//            ctx.Fill ();
//            ctx.Restore ();
//
//            ctx.Rectangle (0, 0, Bounds.Width, 30);
//            ctx.SetColor (new Color (1, 0, 0, 0.5));
//            ctx.Fill ();
        }

        protected override bool SupportsCustomScrolling { get { return true; } }

        protected override void SetScrollAdjustments(ScrollAdjustment horizontal, ScrollAdjustment vertical)
        {
            hscroll = horizontal;
            vscroll = vertical;

//            hscroll.UpperValue = imageSize;
//            hscroll.PageIncrement = Bounds.Width;
//            hscroll.PageSize = Bounds.Width;
//            hscroll.ValueChanged += delegate {
//                QueueDraw ();
//            };
//
//            vscroll.UpperValue = imageSize;
//            vscroll.PageIncrement = Bounds.Height;
//            vscroll.PageSize = Bounds.Height;
//            vscroll.ValueChanged += delegate {
//                QueueDraw ();
//            };
        }

        protected override void OnBoundsChanged()
        {
//            if (vscroll == null)
//                return;
//            vscroll.PageSize = vscroll.PageIncrement = Bounds.Height;
//            hscroll.PageSize = hscroll.PageIncrement = Bounds.Width;
        }
    }


    public class UserControl : VBox
    {
        private ScrollableCanvas canvas;

        public string Text { get; set; }

        public int Width { get { return (int)this.Size.Width; } }

        public int Height { get { return (int)this.Size.Height; } }

        public bool AutoScroll { get; set; }

        public bool Disposing
        {
            get
            {
                return false;
            }
        }

        public Rectangle ClientRectangle { get { return new Rectangle(new Point(0, 0), this.Size); } }

        public bool InvokeRequired { get; set; }

        public Color BackColor { get; private set; }

        public UserControl()
        {
            ScrollView sv = new ScrollView();
            PackStart(sv, fill: true, expand: true);
            canvas = new ScrollableCanvas();
            sv.Content = canvas;
            sv.VerticalScrollPolicy = ScrollPolicy.Never;
            sv.HorizontalScrollPolicy = ScrollPolicy.Always;
            canvas.Paint += (object sender, PaintEventArgs e) => this.OnPaint(e);
            canvas.PaintBackground += (object sender, PaintEventArgs e) => this.OnPaintBackground(e);

            this.AutoScroll = false;
            this.MouseMoved += (sender, args) => this.OnMouseMove(args);
            this.MouseScrolled += (sender, args) => this.OnMouseWheel(args);
            this.ButtonPressed += (sender, args) =>
            {
                var be = args as ButtonEventArgs;
                this.OnMouseDown(args);
                if (be.MultiplePress == 2)
                    this.OnDoubleClick(args);
            };
            this.ButtonReleased += (sender, args) => this.OnMouseUp(args);
            this.KeyPressed += (sender, args) => this.OnKeyPress(args);
            this.BoundsChanged += (sender, args) => this.OnResize(args);
        }

        public object Invoke(Delegate method)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaint(PaintEventArgs pe)
        {
        }

        public void Invalidate()
        {
            canvas.QueueDraw();
        }

        protected virtual void OnPaintBackground(PaintEventArgs e)
        {
//            Console.WriteLine("OnPaintBackground");
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

        protected override void OnMouseExited(EventArgs args)
        {
            OnMouseLeave(args);
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
//            Console.WriteLine("OnKeyPress");
        }
    }
}
