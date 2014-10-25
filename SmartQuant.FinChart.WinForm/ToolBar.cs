using System;
using System.ComponentModel;
#if XWT
using Compatibility.Xwt;
#elif GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public class ToolBar : UserControl
    {
        private Chart chart;

        private IContainer components;

        public Chart Chart
        {
            get
            {
                return this.chart;
            }
            set
            {
                this.chart = value;
            }
        }

        public ToolBar()
        {
            this.InitializeComponent();
        }

        public ToolBar(Chart chart)
            : this()
        {
            Chart = chart;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
        }
    }
}

