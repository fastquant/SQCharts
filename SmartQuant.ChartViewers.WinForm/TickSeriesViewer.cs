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
    public class TickSeriesViewer : Viewer
    {
        public Pad Pad { get; set; }

        public Color Color { get; set; }

        public int DrawWidth { get; set; }

        public override bool IsZoomable
        {
            get
            {
                return true;
            }
        }

        public TickSeriesViewer()
        {
            Type = typeof(TickSeries);
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

