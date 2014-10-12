using System;

using Xwt;
using Xwt.Drawing;
using EmfType = System.Drawing.Imaging.EmfType;
using PrintPageEventArgs = System.Drawing.Printing.PrintPageEventArgs;
using PaintEventArgs = System.Windows.Forms.PaintEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Compatibility.Xwt
{
    public class UserControl : Widget
    {
        public string Text { get; set; }

        public bool InvokeRequired { get; set; }

        public object Invoke(Delegate method)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaint(PaintEventArgs pe)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected virtual void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDoubleClick(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }

    public class ToolTip
    {
    }

    public class PrintDocument
    {
    }
}
