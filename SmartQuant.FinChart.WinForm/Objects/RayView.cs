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
    public class RayView : IChartDrawable, IZoomable
    {
        private DrawingRay ray;

        protected bool toolTipEnabled = true;
        protected string toolTipFormat = "";
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

        public RayView(DrawingRay ray, Pad pad)
        {
            this.ray = ray;
            Pad = pad;
            this.toolTipEnabled = true;
            this.toolTipFormat = "{0} {1} {2} - {3:F6}";
            this.chartFirstDate =DateTime.MaxValue;
            this.chartLastDate = DateTime.MaxValue;
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
            Distance distance = new Distance();
            DateTime dateTime = this.Pad.GetDateTime(x);

            distance.X = x;
            distance.Y = this.ray.Y;
            int num = this.Pad.ClientX(this.chartFirstDate);
            int x2 = this.Pad.X2;
            distance.DX = num > x || x2 < x ? double.MaxValue : 0.0;
            distance.DY = Math.Abs(y - this.ray.Y);

            if (distance.DX == double.MaxValue || distance.DY == double.MaxValue)
                return null;

            distance.ToolTipText = string.Format(ToolTipFormat, "Ray", this.ray.Name, dateTime, this.ray.Y);

            return distance;
        }

        public void Select()
        {
        }

        public void UnSelect()
        {
        }

        public PadRange GetPadRangeY(Pad pad)
        {
            return new PadRange(this.ray.Y * 0.999, this.ray.Y * 1.0001);
        }
    }
}
