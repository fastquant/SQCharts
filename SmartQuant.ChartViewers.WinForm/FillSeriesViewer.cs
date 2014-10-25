using System;
using SmartQuant;
using SmartQuant.Charting;
using System.Drawing;

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
            BuyColor = Color.Blue;
            SellColor = Color.Red;
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

