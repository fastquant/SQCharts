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

            throw new NotImplementedException();
        }

        public override Distance Distance(int x, double y)
        {
            throw new NotImplementedException();
        }
    }
}

