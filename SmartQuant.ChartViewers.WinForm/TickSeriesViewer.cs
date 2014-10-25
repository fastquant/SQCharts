using System;
using SmartQuant;
using SmartQuant.Charting;
using System.Drawing;

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
            Color = Color.Black;
            DrawWidth = 1;
        }

        public override PadRange GetPadRangeX(object obj, Pad pad)
        {
            return null;
        }

        public override PadRange GetPadRangeY(object obj, Pad pad)
        {
            var ts = obj as TickSeries;
            if (ts == null || ts.Count == 0)
                return null;
            var dt1 = new DateTime((long) pad.XMin);
            var dt2 = new DateTime((long) pad.XMax);
            var min = ts.GetMin(dt1, dt2);
            var minPx = min == null ? 0.0 : min.Price;
            var max = ts.GetMax(dt1, dt2);
            var maxPx = max == null ? 0.0 : max.Price;
            return new PadRange(minPx, maxPx);   
        }

        public override void Paint(object obj, Pad pad)
        {
            throw new NotImplementedException();
        }
    }
}

