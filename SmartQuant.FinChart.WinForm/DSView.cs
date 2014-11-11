using SmartQuant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace SmartQuant.FinChart
{
    public class DSView : SeriesView
    {
        private Color color = Color.White;
        private SearchOption option = SearchOption.ExactFirst;
        private SmoothingMode smoothing = SmoothingMode.AntiAlias;
        private TimeSeries series;
        private SimpleDSStyle style;

        public SearchOption Option
        {
            get
            {
                return this.option;
            }
        }

        public SimpleDSStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        public override ISeries MainSeries
        {
            get
            {
                return (ISeries) this.series;
            }
        }

        public override Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public int DrawWidth { get; set; }

        public override double LastValue
        {
            get
            {
                if (this.series.Count == 0 || this.lastDate < this.series.FirstDateTime)
                    return double.NaN;
                if (this.option == SearchOption.ExactFirst)
                    return this.series[this.lastDate, SearchOption.Prev];
                if (this.option == SearchOption.Next)
                    return this.series[this.lastDate.AddTicks(1L), SearchOption.Next];
                else
                    return -1.0;
            }
        }

        public DSView(Pad pad, TimeSeries series)
            : this(pad, series, SearchOption.ExactFirst)
        {
            this.series = series;
            this.toolTipFormat = "{0}\n{2} - {3:F*}";
            this.toolTipFormat = this.toolTipFormat.Replace("*", pad.Chart.LabelDigitsCount.ToString());
        }

        public DSView(Pad pad, TimeSeries series, Color color)
            : this(pad, series, color, SearchOption.ExactFirst, SmoothingMode.AntiAlias)
        {
            this.series = series;
            this.color = color;
            this.toolTipFormat = "{0}\n{2} - {3:F*}";
            this.toolTipFormat = this.toolTipFormat.Replace("*", pad.Chart.LabelDigitsCount.ToString());
        }

        public DSView(Pad pad, TimeSeries series, SearchOption option)
            : base(pad)
        {
            this.series = series;
            this.option = option;
            this.toolTipFormat = "{0}\n{2} - {3:F*}";
            this.toolTipFormat = this.toolTipFormat.Replace("*", pad.Chart.LabelDigitsCount.ToString());
        }

        public DSView(Pad pad, TimeSeries series, Color color, SearchOption option, SmoothingMode smoothing)
            : base(pad)
        {
            this.series = series;
            this.option = option;
            this.color = color;
            this.smoothing = smoothing;
            this.toolTipFormat = "{0}\n{2} - {3:F*}";
            this.toolTipFormat = this.toolTipFormat.Replace("*", pad.Chart.LabelDigitsCount.ToString());
        }

        public override PadRange GetPadRangeY(Pad Pad)
        {
            DateTime datetime1;
            DateTime datetime2;
            if (this.option == SearchOption.ExactFirst)
            {
                datetime1 = this.firstDate;
                datetime2 = this.lastDate;
            }
            else
            {
                int index1 = this.series.GetIndex(this.firstDate.AddTicks(1L), IndexOption.Null);
                int index2 = this.series.GetIndex(this.lastDate.AddTicks(1L), IndexOption.Next);
                if (index1 == -1 || index2 == -1)
                    return new PadRange(0.0, 0.0);
                datetime1 = this.series.GetDateTime(index1);
                datetime2 = this.series.GetDateTime(index2);
            }
            if (this.series.Count == 0 || !(this.series.LastDateTime >= datetime1) || !(this.series.FirstDateTime <= datetime2))
                return new PadRange(0.0, 0.0);
            int index3 = this.series.GetIndex(datetime1, IndexOption.Next);
            int index4 = this.series.GetIndex(datetime2, IndexOption.Prev);
            double min = this.series.GetMin(Math.Min(index3, index4), Math.Max(index3, index4));
            double max = this.series.GetMax(Math.Min(index3, index4), Math.Max(index3, index4));
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
            Pen pen = new Pen(this.Color, (float) this.DrawWidth);
            int num1 = 0;
            GraphicsPath path = new GraphicsPath();
            List<Point> list = new List<Point>();
            double worldY1 = 0.0;
            long ticks1 = 0L;
            double worldY2 = 0.0;
            int x1 = 0;
            int x2 = 0;
            int num2 = 0;
            int y2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int index1 = this.pad.MainSeries.GetIndex(this.firstDate, IndexOption.Null);
            int index2 = this.pad.MainSeries.GetIndex(this.lastDate, IndexOption.Null);
            ArrayList arrayList = (ArrayList) null;
            int val2_1 = this.pad.ClientY(0.0);
            int val2_2 = this.pad.ClientY(this.pad.MaxValue);
            int val2_3 = this.pad.ClientY(this.pad.MinValue);
            if (this.selected)
                arrayList = new ArrayList();
            int num7 = (int) Math.Max(2.0, (double) (int) this.pad.IntervalWidth / 1.2);
            for (int index3 = index1; index3 <= index2; ++index3)
            {
                DateTime dateTime = this.pad.MainSeries.GetDateTime(index3);
                if (this.selected)
                    arrayList.Add((object) dateTime);
                long ticks2 = dateTime.Ticks;
                if (this.option == SearchOption.ExactFirst)
                {
                    if (this.series.Contains(dateTime))
                        worldY1 = this.series[dateTime, SearchOption.Next];
                    else
                        continue;
                }
                if (this.option == SearchOption.Next)
                {
                    if (this.series.Contains(dateTime.AddTicks(1L)))
                        worldY1 = this.series[dateTime.AddTicks(1L), 0, SearchOption.ExactFirst];
                    else if (dateTime.AddTicks(1L) >= this.series.FirstDateTime)
                        worldY1 = this.series[dateTime.AddTicks(1L), SearchOption.Next];
                    else
                        continue;
                }
                if (this.style == SimpleDSStyle.Line)
                {
                    if (num1 != 0)
                    {
                        x1 = this.pad.ClientX(new DateTime(ticks1));
                        num2 = this.pad.ClientY(worldY2);
                        x2 = this.pad.ClientX(new DateTime(ticks2));
                        y2 = this.pad.ClientY(worldY1);
                        if (x1 != num3 || x2 != num4 || (num2 != num5 || y2 != num6))
                            path.AddLine(x1, num2, x2, y2);
                    }
                    num3 = x1;
                    num5 = num2;
                    num4 = x2;
                    num6 = y2;
                    ticks1 = ticks2;
                    worldY2 = worldY1;
                    list.Add(new Point(this.pad.ClientX(new DateTime(ticks1)), this.pad.ClientY(worldY2)));
                }
                if (this.style == SimpleDSStyle.Bar)
                {
                    x1 = this.pad.ClientX(new DateTime(ticks2));
                    num2 = this.pad.ClientY(worldY1);
                    float y = (float) Math.Max(Math.Min(num2, val2_1), val2_2);
                    float num8 = (float) Math.Min(Math.Max(num2, val2_1), val2_3);
                    this.pad.Graphics.FillRectangle((Brush) new SolidBrush(this.Color), (float) (x1 - num7 / 2), y, (float) num7, Math.Abs(y - num8));
                }
                if (this.style == SimpleDSStyle.Circle)
                {
                    x1 = this.pad.ClientX(new DateTime(ticks2));
                    num2 = this.pad.ClientY(worldY1);
                    Math.Max(Math.Min(num2, val2_1), val2_2);
                    Math.Min(Math.Max(num2, val2_1), val2_3);
                    this.pad.Graphics.FillEllipse((Brush) new SolidBrush(this.Color), x1 - this.DrawWidth, num2 - this.DrawWidth, this.DrawWidth * 2, this.DrawWidth * 2);
                }
                ++num1;
            }
            if (this.selected)
            {
                int num8 = Math.Max(1, (int) Math.Round((double) arrayList.Count / 8.0));
                int index3 = 1;
                while (index3 < arrayList.Count)
                {
                    int num9 = this.pad.ClientX(new DateTime(((DateTime) arrayList[index3]).Ticks));
                    if (this.series.Contains((DateTime) arrayList[index3]))
                    {
                        int num10 = this.pad.ClientY(this.series[(DateTime) arrayList[index3], 0, SearchOption.ExactFirst]);
                        Color midnightBlue = Color.MidnightBlue;
                        this.pad.Graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb((int) midnightBlue.R ^ (int) byte.MaxValue, (int) midnightBlue.G ^ (int) byte.MaxValue, (int) midnightBlue.B ^ (int) byte.MaxValue)), num9 - 2, num10 - 2, 4, 4);
                    }
                    index3 += num8;
                }
            }
            if (this.style != SimpleDSStyle.Line)
                return;
            SmoothingMode smoothingMode = this.pad.Graphics.SmoothingMode;
            this.pad.Graphics.SmoothingMode = this.smoothing;
            this.pad.Graphics.DrawPath(pen, path);
            this.pad.Graphics.SmoothingMode = smoothingMode;
        }

        public override Distance Distance(int x, double y)
        {
            Distance distance = new Distance();
            DateTime dateTime = this.pad.GetDateTime(x);
            double num = 0.0;
            if (this.option == SearchOption.ExactFirst)
            {
                if (!this.series.Contains(dateTime))
                    return (Distance) null;
                num = this.series[dateTime, SearchOption.ExactFirst];
            }
            if (this.option == SearchOption.Next)
            {
                if (this.series.LastDateTime < dateTime.AddTicks(1L))
                    return (Distance) null;
                num = this.series[dateTime.AddTicks(1L), SearchOption.Next];
            }
            distance.X = (double) x;
            distance.Y = num;
            distance.DX = 0.0;
            distance.DY = Math.Abs(y - num);
            if (distance.DX == double.MaxValue || distance.DY == double.MaxValue)
                return (Distance) null;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(this.toolTipFormat, (object) this.series.Name, (object) this.series.Description, (object) dateTime.ToString(), (object) distance.Y);
            distance.ToolTipText = ((object) stringBuilder).ToString();
            return distance;
        }
    }
}
