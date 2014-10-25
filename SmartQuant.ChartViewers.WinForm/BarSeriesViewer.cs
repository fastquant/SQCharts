using System;
using SmartQuant;
using SmartQuant.Charting;
using System.Drawing;

#if GTK
using Compatibility.Gtk;
#endif

namespace SmartQuant.ChartViewers
{
    public class BarSeriesViewer : Viewer
    {
        public Pad Pad { get; set; }

        public ChartStyle ChartStyle { get; set; }

        public Color BarColor { get; set; }

        public Color CandleColor { get; set; }

        public Color CandleBorderColor { get; set; }

        public Color CandleWhiteColor { get; set; }

        public Color CandleBlackColor { get; set; }

        public EWidthStyle CandleWidthStyle { get; set; }

        public EWidthStyle BarWidthStyle { get; set; }

        public int BarWidth { get; set; }

        public int CandleWidth { get; set; }

        public Color Color { get; set; }

        public int DrawWidth { get; set; }

        public override bool IsZoomable
        {
            get
            {
                return true;
            }
        }

        public BarSeriesViewer()
        {
            Type = typeof (BarSeries);
            Color = Color.Black;
            DrawWidth = 1;
            ChartStyle = ChartStyle.Candle;
            BarWidthStyle = EWidthStyle.Auto;
            CandleWidthStyle = EWidthStyle.Auto;
            CandleBlackColor = Color.Black;
            CandleWhiteColor = Color.White;
            CandleBorderColor = Color.Black;
            CandleColor = Color.Black;
            BarColor = Color.Black;
        }

        public override PadRange GetPadRangeX(object obj, Pad pad)
        {
            return null;
        }

        public override PadRange GetPadRangeY(object obj, Pad pad)
        {
            var bs = obj as BarSeries;
            if (bs == null || bs.Count == 0)
                return null;
            var dt1 = new DateTime((long) pad.XMin);
            var dt2 = new DateTime((long) pad.XMax);
            var lowest = bs.LowestLowBar(dt1, dt2);
            double min = lowest != null ? lowest.Low : 0;
            var highest = bs.HighestHighBar(dt1, dt2);
            double max = highest != null ? highest.High : 0;
            return new PadRange(min, max);
        }

        public override void Paint(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }
    }
}

