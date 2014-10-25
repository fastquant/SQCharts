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
    public class ChartToolStrip : UserControl
    {
        private Chart chart;
        private IContainer components;

        public ChartToolStrip()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}

