using System;
using SmartQuant;
using SmartQuant.Charting;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;

#else
using Compatibility.WinForm;
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
            Type = typeof(FillSeries);
            BuyColor = Colors.Blue;
            SellColor = Colors.Red;
            TextEnabled = true;
        }

        public override PadRange GetPadRangeX(object obj, Pad pad)
        {
            return null;
        }

        public override PadRange GetPadRangeY(object obj, Pad pad)
        {
            return null;
        }

        public override void Paint(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }
    }
}

