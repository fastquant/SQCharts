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
            throw new NotImplementedException();
        }

        public static implicit operator MouseEventArgs(ButtonEventArgs args)
        {
            throw new NotImplementedException();
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

    public class PaintEventArgs : EventArgs
    {
        private Graphics g;
        private Rectangle r;

        public PaintEventArgs(Graphics g, Rectangle r)
        {
            this.g = g;
            this.r = r;
        }
    }

    public class UserControl : Canvas
    {
        private Graphics graphics;

        public string Text { get; set; }

        public bool InvokeRequired { get; set; }

        public UserControl()
        {
            this.MouseMoved += (sender, args) => this.OnMouseMove(args);
            this.MouseScrolled += (sender, args) => this.OnMouseWheel(args);
            this.ButtonPressed += (sender, args) => this.OnMouseDown(args);
            this.ButtonReleased += (sender, args) => this.OnMouseUp(args);
            this.KeyPressed +=  (sender, args) => this.OnKeyPress(args);
            this.BoundsChanged += (sender, args) => this.OnResize(args);
        }

        public object Invoke(Delegate method)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaint(PaintEventArgs pe)
        {
            throw new NotImplementedException();
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

        protected virtual void OnDoubleClick(EventArgs e)
        {
        }

        protected virtual void OnResize(EventArgs e)
        {
        }

        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
        }

        protected override void OnDraw(Context ctx, Rectangle rect)
        {
            var pe = new PaintEventArgs(new Graphics(ctx), rect);
            OnPaintBackground(pe);
            OnPaint(pe);
        }
    }
}
