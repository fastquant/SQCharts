// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using SmartQuant;
using SmoothingMode = System.Drawing.Drawing2D.SmoothingMode;
#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.FinChart
{
    public class DSView : SeriesView
    {
        private SmoothingMode smoothing;
        private ISeries mainSeries;

        public SearchOption Option { get; private set; }

        public SimpleDSStyle Style { get; set; }

        public override ISeries MainSeries { get { return mainSeries; } }


        public override Color Color { get; set; }

        public int DrawWidth { get; set; }

        public override double LastValue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DSView(Pad pad, TimeSeries series)
            : this(pad, series, SearchOption.ExactFirst)
        {
        }

        public DSView(Pad pad, TimeSeries series, Color color)
            : this(pad, series, color, SearchOption.ExactFirst, SmoothingMode.AntiAlias)
        {
        }

        public DSView(Pad pad, TimeSeries series, SearchOption option)
            : this(pad, series, Colors.White, option, SmoothingMode.AntiAlias)
        {
        }

        public DSView(Pad pad, TimeSeries series, Color color, SearchOption option, SmoothingMode smoothing)
            : base(pad)
        {
            this.mainSeries = series;
            Option = option;
            Color = color;
            this.smoothing = smoothing;
            var format = "{0}\n{2} - {3:F*}";
            ToolTipFormat = format.Replace("*", pad.Chart.LabelDigitsCount.ToString());
        }

        public override PadRange GetPadRangeY(Pad Pad)
        {
            throw new NotImplementedException();
        }

        public override void Paint()
        {
            throw new NotImplementedException();
        }

        public override Distance Distance(int x, double y)
        {
            throw new NotImplementedException();
        }
    }
}

