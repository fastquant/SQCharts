// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.FinChart
{
    public abstract class BSView : SeriesView
    {
        public string ToolTipDateTimeFormat { get; set; }

        public BSView(Pad pad)
            : base(pad)
        {
        }

        public override PadRange GetPadRangeY(Pad Pad)
        {
            double max = MainSeries.GetMax(this.firstDate, this.lastDate);
            double min = MainSeries.GetMin(this.firstDate, this.lastDate);
            if (max >= min)
            {
                double num = max / 10.0;
                max -= num;
                min += num;
            }
            return new PadRange(max, min);
        }

        public override Distance Distance(int x, double y)
        {
            Distance distance = new Distance();
            Bar bar = (this.MainSeries as BarSeries)[this.pad.GetDateTime(x), IndexOption.Null];
            distance.DX = 0.0;
            if (y >= bar.Low && y <= bar.High)
                distance.DY = 0.0;
            if (distance.DX == double.MaxValue || distance.DY == double.MaxValue)
                return   null;

            distance.ToolTipText = string.Format(this.toolTipFormat, (object) this.MainSeries.Name, (object) this.MainSeries.Description, (object) this.ToolTipDateTimeFormat, (object) bar.High, (object) bar.Low, (object) bar.Open, (object) bar.Close, (object) bar.Volume);
            return distance;

        }
    }
}

