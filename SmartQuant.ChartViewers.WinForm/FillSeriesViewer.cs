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
    public class FillSeriesViewer : Viewer
    {
        public Color BuyColor { get; set; }

        public Color SellColor { get; set; }

        public bool TextEnabled { get; set; }

        public override bool IsZoomable
        {
            get
            {
                return true;
            }
        }

        public FillSeriesViewer()
        {
            this.Type = typeof(FillSeries);
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

