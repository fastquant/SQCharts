using SmartQuant;
using SmartQuant.Charting;
using System;
using System.Collections.Generic;
using System.Drawing;

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
            Color = Color.Black;
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
            TimeSeries timeSeries = obj as TimeSeries;
            if (timeSeries.Count == 0)
                return;
            double xmin = pad.XMin;
            double xmax = pad.XMax;
            double ymin = pad.YMin;
            double ymax = pad.YMax;
            List<Viewer.Property> list = (List<Viewer.Property>)null;
            if (this.metadata.TryGetValue(obj, out list))
            {
                foreach (Viewer.Property property in list)
                {
                    if (property.Name == "Color")
                        this.Color = (Color)property.Value;
                    if (property.Name == "Width")
                        this.DrawWidth = (int)property.Value;
                    if (property.Name == "Style")
                    {
                        if ((string)property.Value == "Line")
                            this.DrawStyle = DrawStyle.Line;
                        if ((string)property.Value == "Bar")
                            this.DrawStyle = DrawStyle.Bar;
                        if ((string)property.Value == "Circle")
                            this.DrawStyle = DrawStyle.Circle;
                    }
                }
            }
            Pen pen = new Pen(this.Color, (float)this.DrawWidth);
            int num1 = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            long num4 = 0L;
            long num5 = 0L;
            int num6 = 0;
            int num7 = 0;
            long num8 = 0L;
            long num9 = 0L;
            int num10 = 0;
            int num11 = 0;
            DateTime datetime1 = new DateTime((long)xmin);
            DateTime datetime2 = new DateTime((long)xmax);
            int num12 = !(datetime1 < timeSeries.FirstDateTime) ? timeSeries.GetIndex(datetime1, IndexOption.Prev) : 0;
            int num13 = !(datetime2 > timeSeries.LastDateTime) ? timeSeries.GetIndex(datetime2, IndexOption.Next) : timeSeries.Count - 1;
            if (num12 == -1 || num13 == -1)
                return;
            for (int index = num12; index <= num13; ++index)
            {
                TimeSeriesItem timeSeriesItem = timeSeries.GetItem(index);
                double num14 = (double)timeSeriesItem.DateTime.Ticks;
                pad.ClientX(num14);
                double num15 = timeSeriesItem.Value;
                if (this.DrawStyle == DrawStyle.Line)
                {
                    if (num1 != 0)
                    {
                        num4 = (long)pad.ClientX(num2);
                        num6 = pad.ClientY(num3);
                        num5 = (long)pad.ClientX(num14);
                        num7 = pad.ClientY(num15);
                        if ((pad.IsInRange(num14, num15) || pad.IsInRange(num2, num3)) && (num4 != num8 || num5 != num9 || (num6 != num10 || num7 != num11)))
                            pad.Graphics.DrawLine(pen, (float)num4, (float)num6, (float)num5, (float)num7);
                    }
                    num8 = num4;
                    num10 = num6;
                    num9 = num5;
                    num11 = num7;
                    num2 = num14;
                    num3 = num15;
                    ++num1;
                }
                if (this.DrawStyle == DrawStyle.Bar)
                {
                    if (num15 > 0.0)
                        pad.Graphics.FillRectangle((Brush)new SolidBrush(this.Color), pad.ClientX(num14) - (this.DrawWidth + 1) / 2, pad.ClientY(num15), this.DrawWidth + 1, pad.ClientY(0.0) - pad.ClientY(num15));
                    else
                        pad.Graphics.FillRectangle((Brush)new SolidBrush(this.Color), pad.ClientX(num14) - (this.DrawWidth + 1) / 2, pad.ClientY(0.0), this.DrawWidth + 1, pad.ClientY(num15) - pad.ClientY(0.0));
                }
                if (this.DrawStyle == DrawStyle.Circle)
                {
                    SolidBrush solidBrush = new SolidBrush(this.Color);
                    pad.Graphics.FillEllipse((Brush)solidBrush, pad.ClientX(num14) - this.DrawWidth / 2, pad.ClientY(num15) - this.DrawWidth / 2, this.DrawWidth, this.DrawWidth);
                }
            }
        }
    }
}
