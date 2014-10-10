// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using SmartQuant.FinChart;
using System;
using System.ComponentModel;
using System.Drawing;

namespace SmartQuant.FinChart.Objects
{
    public class EllipseView : IChartDrawable, IZoomable
    {
        protected bool toolTipEnabled = true;
        protected string toolTipFormat = "";
        private DrawingEllipse rect;
        protected DateTime firstDate;
        protected DateTime lastDate;
        protected bool selected;
        protected DateTime chartFirstDate;
        protected DateTime chartLastDate;

        public Pad Pad { get; set; }

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

        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        [Category("ToolTip")]
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

        public EllipseView(DrawingEllipse rect, Pad pad)
        {
            this.rect = rect;
            this.Pad = pad;
            this.toolTipEnabled = true;
            this.toolTipFormat = "{0} {1} {2} - {3:F6}";
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
            return null;
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
