using System;
using SmartQuant;
using SmartQuant.Charting;
using System.Collections.Generic;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.ChartViewers
{
    public class TimeSeriesViewer : Viewer
    {
        public Pad Pad { get; set; }

        public Color Color { get; set; }

        public int DrawWidth { get; set; }

        public DrawStyle DrawStyle { get; set; }

        public override bool IsZoomable
        {
            get
            {
                return true;
            }
        }

        public TimeSeriesViewer()
        {
            Type = typeof(TimeSeries);
            Color = Colors.Black;
            DrawWidth = 1;
            DrawStyle = DrawStyle.Line;
        }

        public override PadRange GetPadRangeX(object obj, Pad pad)
        {
            return null;
        }

        public override PadRange GetPadRangeY(object obj, Pad pad)
        {
            var ts = obj as TimeSeries;
            if (ts == null || ts.Count == 0)
                return null;
            var dt1 = new DateTime((long)pad.XMin);
            var dt2 = new DateTime((long)pad.XMax);
            var min = ts.GetMin(dt1, dt2);
            min = double.IsNaN(min) ? 0 : min;
            var max = ts.GetMax(dt1, dt2);
            max = double.IsNaN(max) ? 0 : min;
            return new PadRange(min, max);
        }

        public override void Paint(object obj, Pad pad)
        {
            var ts = obj as TimeSeries;
            if (ts == null || ts.Count == 0)
                return;
            var xmin = pad.XMin;
            var xmax = pad.XMax;
            var ymin = pad.YMin;
            var ymax = pad.YMax;
            List<Property> props;
            if (this.metadata.TryGetValue(obj, out props))
            {
                foreach (var property in props)
                {
                }
            }
            Pen pen = new Pen(this.Color, this.DrawWidth);
            var dt1 = new DateTime((long)xmin);
            var dt2 = new DateTime((long)xmax);
            int idx1 = dt1 < ts.FirstDateTime ? 0 : ts.GetIndex(dt1, IndexOption.Prev);
            int idx2 = dt2 > ts.LastDateTime ? ts.Count - 1 : ts.GetIndex(dt2, IndexOption.Next);
            if (idx1 == -1 || idx2 == -1)
                return;
            for (int i = idx1; i <= idx2; ++i)
            {
                var item = ts.GetItem(i);
                var ticks = item.DateTime.Ticks;
                var value = item.Value;
                if (this.DrawStyle == DrawStyle.Line)
                {
                }
                if (this.DrawStyle == DrawStyle.Bar)
                {
                }
                if (this.DrawStyle == DrawStyle.Circle)
                {
                    SolidBrush brush = new SolidBrush(this.Color);
                    pad.Graphics.FillEllipse(brush, pad.ClientX(ticks) - this.DrawWidth / 2, pad.ClientY(value) - this.DrawWidth / 2, this.DrawWidth, this.DrawWidth);
                }
            }
        }
    }
}

