// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using SmartQuant;
using SmartQuant.FinChart;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace SmartQuant.FinChart.Objects
{
    public class LineView : IChartDrawable, IZoomable
    {
        private DrawingLine line;

        protected bool toolTipEnabled = true;
        protected string toolTipFormat = "";
        protected Pad pad;
        protected DateTime firstDate;
        protected DateTime lastDate;
        protected bool selected;
        protected DateTime chartFirstDate;
        protected DateTime chartLastDate;

        [Description("Enable or disable tooltip appearance for this marker.")]
        [Category("ToolTip")]
        public bool ToolTipEnabled
        {
            get
            {
                return this.toolTipEnabled;
            }
            set
            {
                this.toolTipEnabled = value;
            }
        }

        [Category("ToolTip")]
        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        public string ToolTipFormat
        {
            get
            {
                return this.toolTipFormat;
            }
            set
            {
                this.toolTipFormat = value;
            }
        }

        public LineView(DrawingLine line, Pad pad)
        {
            this.line = line;
            this.pad = pad;
            this.toolTipEnabled = true;
            this.toolTipFormat = "{0} {1} {2} - {3:F6}";
            this.chartFirstDate = new DateTime(Math.Min(line.X1.Ticks, line.X2.Ticks));
            this.chartLastDate = new DateTime(Math.Max(line.X1.Ticks, line.X2.Ticks));
        }

        public void Paint()
        {
            throw new NotImplementedException();
        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            this.firstDate = minDate;
            this.lastDate = maxDate;
        }

        public Distance Distance(int x, double y)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
        }

        public void UnSelect()
        {
        }

        public PadRange GetPadRangeY(Pad pad)
        {
            throw new NotImplementedException();
        }
    }
}
