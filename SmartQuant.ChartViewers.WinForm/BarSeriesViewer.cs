using System;
using SmartQuant;
using SmartQuant.Charting;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
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
            this.Type = typeof(BarSeries);
        }

        public override PadRange GetPadRangeX(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }

        public override PadRange GetPadRangeY(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }

        public override void Paint(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }
    }
}

