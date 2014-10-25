using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace SmartQuant.FinChart
{
	public partial class Chart
	{
        private HScrollBar scrollBar;
        private ToolTip Tooltip;

        private void InitializeComponent()
        {
            this.components = new Container();
            this.scrollBar = new HScrollBar();
            this.Tooltip = new ToolTip(this.components);
            this.SuspendLayout();
            this.scrollBar.Dock = DockStyle.Bottom;
            this.scrollBar.TabIndex = 0;
           // this.scrollBar.Scroll += new ScrollEventHandler(this.WhenScrollBarScroll);
            this.Controls.Add(this.scrollBar);
           // this.MouseDown += new MouseEventHandler(this.OnMouseDown);
            //this.MouseLeave += new EventHandler(this.Chart_MouseLeave);
            //this.MouseUp += new MouseEventHandler(this.Chart_MouseUp);
            this.ResumeLayout(false);
        }
            
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
        }

	}
}

