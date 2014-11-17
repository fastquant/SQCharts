using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace SmartQuant.FinChart
{
    public partial class Chart : UserControl
	{
        private HScrollBar scrollBar;
        internal ToolTip ToolTip;

        private void InitializeComponent()
        {
            this.components = new Container();
            this.scrollBar = new HScrollBar();
            this.ToolTip = new ToolTip(this.components);
            this.SuspendLayout();
            this.scrollBar.Dock = DockStyle.Bottom;
            this.scrollBar.TabIndex = 0;
            this.scrollBar.Scroll += new ScrollEventHandler(OnScrollBarScroll);
            this.AutoScroll = true;
            this.Controls.Add((Control) this.scrollBar);
            this.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
            this.Name = "Chart";
            this.MouseDown += new MouseEventHandler(this.Chart_MouseDown);
            this.MouseLeave += new EventHandler(this.Chart_MouseLeave);
            this.MouseUp += new MouseEventHandler(this.Chart_MouseUp);
            this.ResumeLayout(false);
        }
	}
}

