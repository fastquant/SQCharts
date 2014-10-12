using System;
using System.Collections.Generic;
using SmartQuant;
using SmartQuant.Charting;
using SmartQuant.ChartViewers;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartQuant.Controls.BarChart
{
    public class BarChart : FrameworkControl, IGroupListener
    {
        private IContainer components;
        private Chart chart;
        private ComboBox cbxSelector;

        public PermanentQueue<Event> Queue { get; private set; }

        public BarChart()
        {
            this.InitializeComponent();
        }

        protected override void OnInit()
        {
            throw new NotImplementedException();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            throw new NotImplementedException();
        }

        public bool OnNewGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void OnNewGroupEvent(GroupEvent groupEvent)
        {
            throw new NotImplementedException();
        }

        private int GetViewerIndex(Group group, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void OnNewGroupUpdate(GroupUpdate groupUpdate)
        {
            throw new NotImplementedException();
        }

        private void EnsurePadExists(int newPad, string labelFormat)
        {
            throw new NotImplementedException();
        }

        private Pad AddPad()
        {
            throw new NotImplementedException();

        }

        public void UpdateGUI()
        {
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

        private delegate void GroupUpdateDelegate(GroupUpdate groupUpdate);
    }
}

