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
            this.ToolTip = new ToolTip(this.components, this);
//            AutoScroll = true;
//            Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
            this.scrollbar.Adjustment = new Adjustment(0, 0, 100, 1, 10, 10);
            this.scrollbar.ValueChanged += OnScrollBarScroll;
        }

        protected override void OnMouseDown(Compatibility.Gtk.MouseEventArgs e)
        {
            OnChartMouseDown(this, e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            OnChartMouseLeave(this, e);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnChartMouseUp(this, e);
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            OnChartMouseWheel(this, e);
            base.OnMouseWheel(e);
        }

        private void OnScrollBarScroll(object sender, EventArgs e)
        {
//            Console.WriteLine("scrollvalue:{0} min:{1} max:{2}", this.scrollbar.Value,this.scrollbar.Adjustment.Lower,this.scrollbar.Adjustment.Upper );

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

