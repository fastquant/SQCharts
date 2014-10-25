// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using SmartQuant;
using SmoothingMode = System.Drawing.Drawing2D.SmoothingMode;
#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#elif GTK
using Gdk;
using Compatibility.Gtk;
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
//                if (MainSeries.Count == 0 || this.lastDate < MainSeries.FirstDateTime)
//                    return double.NaN;
//                if (Option == SearchOption.ExactFirst)
//                    return MainSeries[this.lastDate, SearchOption.Prev];
//                if (Option == SearchOption.Next)
//                    return MainSeries[this.lastDate.AddTicks(1), SearchOption.Next];
//                else
                    return -1.0;
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
            DateTime datetime1;
            DateTime datetime2;
            if (Option == SearchOption.ExactFirst)
            {
                datetime1 = this.firstDate;
                datetime2 = this.lastDate;
            }
            else
            {
                int index1 = MainSeries.GetIndex(this.firstDate.AddTicks(1L), IndexOption.Null);
                int index2 = MainSeries.GetIndex(this.lastDate.AddTicks(1L), IndexOption.Next);
                if (index1 == -1 || index2 == -1)
                    return new PadRange(0.0, 0.0);
                datetime1 = MainSeries.GetDateTime(index1);
                datetime2 = MainSeries.GetDateTime(index2);
            }
            if (MainSeries.Count == 0 || !(MainSeries.LastDateTime >= datetime1) || !(MainSeries.FirstDateTime <= datetime2))
                return new PadRange(0.0, 0.0);
            int index3 = MainSeries.GetIndex(datetime1, IndexOption.Next);
            int index4 = MainSeries.GetIndex(datetime2, IndexOption.Prev);
//            double min = MainSeries.GetMin(Math.Min(index3, index4), Math.Max(index3, index4));
//            double max = MainSeries.GetMax(Math.Min(index3, index4), Math.Max(index3, index4));
            double min = 0;
            double max = 0;
            if (min >= max)
            {
                double num = Math.Abs(min) / 1000.0;
                min -= num;
                max += num;
            }
            return new PadRange(min, max);
        }

        public override void Paint()
        {
            throw new NotImplementedException();
        }

        public override Distance Distance(int x, double y)
        {
            Distance distance = new Distance();
            DateTime dateTime = this.pad.GetDateTime(x);
            double num = 0.0;
//            if (Option == SearchOption.ExactFirst)
//            {
//                if (!MainSeries.Contains(dateTime))
//                    return null;
//                num = MainSeries[dateTime, SearchOption.ExactFirst];
//            }
//            if (Option == SearchOption.Next)
//            {
//                if (MainSeries.LastDateTime < dateTime.AddTicks(1L))
//                    return (Distance) null;
//                num = MainSeries[dateTime.AddTicks(1), SearchOption.Next];
//            }
            distance.X = (double) x;
            distance.Y = num;
            distance.DX = 0.0;
            distance.DY = Math.Abs(y - num);
            if (distance.DX == double.MaxValue || distance.DY == double.MaxValue)
                return null;
            distance.ToolTipText = string.Format(this.toolTipFormat, (object) MainSeries.Name, MainSeries.Description, (object) dateTime.ToString(), (object) distance.Y);
            return distance;
        }
    }
}

