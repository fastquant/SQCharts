using System;
using System.Collections.Generic;
using System.ComponentModel;
using SmartQuant.FinChart;

namespace SmartQuant.Controls
{
    public class BarChart2 : FrameworkControl, IGroupListener
    {
        private IContainer components;
        private Chart chart;

        public PermanentQueue<Event> Queue { get; private set; }

        public BarChart2()
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

        public void OnNewGroupUpdate(GroupUpdate groupUpdate)
        {
        }

        public void OnNewGroupEvent(GroupEvent groupEvent)
        {
            throw new NotImplementedException();
        }

        public void UpdateGUI()
        {
            throw new NotImplementedException();
        }

        public void Crosshair(bool isChecked)
        {
            this.chart.ActionType = isChecked ? ChartActionType.None : ChartActionType.Cross;
        }

        public void ZoomIn()
        {
            this.chart.ZoomIn();
        }

        public void ZoomOut()
        {
            this.chart.ZoomOut();
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

