using SmartQuant;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace SmartQuant.FinChart
{
    public class SimpleBSView : SeriesView
    {
        private Color upColor = Color.Black;
        private Color downColor = Color.Lime;
        private BarSeries series;
        private SimpleBSStyle style;

        [Category("Drawing Style")]
        public Color UpColor
        {
            get
            {
                return this.upColor;
            }
            set
            {
                this.upColor = value;
            }
        }

        [Category("Drawing Style")]
        public Color DownColor
        {
            get
            {
                return this.downColor;
            }
            set
            {
                this.downColor = value;
            }
        }

        public SimpleBSStyle Style
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

        [Browsable(false)]
        public override Color Color
        {
            get
            {
                return this.downColor;
            }
            set
            {
            }
        }

        public override double LastValue
        {
            get
            {
                return this.series[this.lastDate, IndexOption.Prev].Close;
            }
        }

        public SimpleBSView(Pad pad, BarSeries series)
            : base(pad)
        {
            this.series = series;
        }

        public override PadRange GetPadRangeY(Pad Pad)
        {
            double min = this.series.LowestLow(this.firstDate, this.lastDate);
            double max = this.series.HighestHigh(this.firstDate, this.lastDate);
            if (min >= max)
            {
                double num = min / 10.0;
                min -= num;
                max += num;
            }
            return new PadRange(min, max);
        }

        public override void Paint()
        {
            Color color = this.downColor;
            Pen pen1 = new Pen(color);
            Pen pen2 = new Pen(color);
            Pen pen3 = new Pen(color);
            Brush brush1 = (Brush) new SolidBrush(this.downColor);
            Brush brush2 = (Brush) new SolidBrush(this.upColor);
            long num1 = -1L;
            long num2 = -1L;
            int index1 = this.series.GetIndex(this.firstDate, IndexOption.Null);
            int index2 = this.series.GetIndex(this.lastDate, IndexOption.Null);
            if (index1 == -1 || index2 == -1)
                return;
            int width = (int) Math.Max(2.0, (double) (int) this.pad.IntervalWidth / 1.4);
            int num3 = 0;
            for (int index3 = index1; index3 <= index2; ++index3)
            {
                int num4 = this.pad.ClientX(this.series[index3].DateTime);
                Bar bar = this.series[index3];
                double high = bar.High;
                double low = bar.Low;
                double open = bar.Open;
                double close = bar.Close;
                if (this.style == SimpleBSStyle.Candle)
                {
                    this.pad.Graphics.DrawLine(pen1, num4, this.pad.ClientY(low), num4, this.pad.ClientY(high));
                    if (open != 0.0 && close != 0.0)
                    {
                        if (open > close)
                        {
                            int height = this.pad.ClientY(close) - this.pad.ClientY(open);
                            if (height == 0)
                                height = 1;
                            this.pad.Graphics.FillRectangle(brush1, num4 - width / 2, this.pad.ClientY(open), width, height);
                        }
                        else
                        {
                            int height = this.pad.ClientY(open) - this.pad.ClientY(close);
                            if (height == 0)
                                height = 1;
                            this.pad.Graphics.DrawRectangle(pen1, num4 - width / 2, this.pad.ClientY(close), width, height);
                            this.pad.Graphics.FillRectangle(brush2, num4 - width / 2 + 1, this.pad.ClientY(close) + 1, width - 1, height - 1);
                        }
                    }
                }
                if (this.style == SimpleBSStyle.Bar)
                {
                    this.pad.Graphics.DrawLine(pen1, num4, this.pad.ClientY(low), num4, this.pad.ClientY(high));
                    this.pad.Graphics.DrawLine(pen1, num4 - width / 2, this.pad.ClientY(open), num4, this.pad.ClientY(open));
                    this.pad.Graphics.DrawLine(pen1, num4 + width / 2, this.pad.ClientY(close), num4, this.pad.ClientY(close));
                }
                if (this.style == SimpleBSStyle.Line)
                {
                    long num5 = (long) num4;
                    int num6 = this.pad.ClientY(bar.Close);
                    if (num1 != -1L && num2 != -1L)
                        this.pad.Graphics.DrawLine(pen1, (float) num5, (float) num6, (float) num1, (float) num2);
                    num1 = num5;
                    num2 = (long) num6;
                    ++num3;
                }
            }
        }

        public override Distance Distance(int x, double y)
        {
            Distance distance = new Distance();
            Bar bar = this.series[this.pad.GetDateTime(x), IndexOption.Null];
            distance.DX = 0.0;
            if (y >= bar.Low && y <= bar.High)
                distance.DY = 0.0;
            if (distance.DX == double.MaxValue || distance.DY == double.MaxValue)
                return (Distance) null;
            this.toolTipFormat = "{0} {2}\n\nH : {3:F*}\nL : {4:F*}\nO : {5:F*}\nC : {6:F*}\nV : {7}";
            this.toolTipFormat = this.toolTipFormat.Replace("*", this.pad.Chart.LabelDigitsCount.ToString());
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(this.toolTipFormat, (object) this.series.Name, (object) this.series.Description, (object) bar.DateTime, (object) bar.High, (object) bar.Low, (object) bar.Open, (object) bar.Close, (object) bar.Volume);
            distance.ToolTipText = ((object) stringBuilder).ToString();
            return distance;
        }
    }
}
