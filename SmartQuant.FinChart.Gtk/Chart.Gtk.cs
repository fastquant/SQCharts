using System;
using System.ComponentModel;
using System.Drawing;
using Compatibility.Gtk;
using Gtk;

namespace SmartQuant.FinChart
{
    public partial class Chart : ScrolledUserControl
    {
        protected internal double scrollbarOldValue;
        internal ToolTip ToolTip;

        private void InitializeComponent()
        {
            this.components =  new System.ComponentModel.Container();
            this.ToolTip = new ToolTip(this.components);
            AutoScroll = true;
            Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
            this.scrollbar.ValueChanged += OnScrollBarScroll;
        }

        protected override void OnMouseDown(Compatibility.Gtk.MouseEventArgs e)
        {
            Chart_MouseDown(this, e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Chart_MouseLeave(this, e);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Chart_MouseUp(this, e);
            base.OnMouseUp(e);
        }

        private void OnScrollBarScroll(object sender, EventArgs e)
        {
            double newValue = this.scrollbar.Value;
            if (newValue == scrollbarOldValue)
                return;
            int delta = (int)(newValue- scrollbarOldValue);
            SetIndexInterval(this.firstIndex + delta, this.lastIndex + delta);
            Invalidate();
            scrollbarOldValue = newValue;
        }
    }
}

