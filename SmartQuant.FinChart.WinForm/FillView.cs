// Decompiled with JetBrains decompiler
// Type: SmartQuant.FinChart.FillView
// Assembly: SmartQuant.FinChart, Version=1.0.5424.18125, Culture=neutral, PublicKeyToken=null
// MVID: 8A140279-ED2D-4F5A-B7EB-2662861D60C1
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.FinChart.dll

using SmartQuant;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace SmartQuant.FinChart
{
    public class FillView : IChartDrawable, IDateDrawable
    {
        private Color buyColor = Color.Blue;
        private Color sellColor = Color.Red;
        private Color sellShortColor = Color.Yellow;
        private bool textEnabled = true;
        protected bool toolTipEnabled = true;
        protected string toolTipFormat = "";
        private Fill fill;
        protected Pad pad;
        protected DateTime firstDate;
        protected DateTime lastDate;
        protected bool selected;

        internal bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color BuyColor
        {
            get
            {
                return this.buyColor;
            }
            set
            {
                this.buyColor = value;
            }
        }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color SellColor
        {
            get
            {
                return this.sellColor;
            }
            set
            {
                this.sellColor = value;
            }
        }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color SellShortColor
        {
            get
            {
                return this.sellShortColor;
            }
            set
            {
                this.sellShortColor = value;
            }
        }

        [Browsable(false)]
        [Category("Drawing Style")]
        public bool TextEnabled
        {
            get
            {
                return this.textEnabled;
            }
            set
            {
                this.textEnabled = value;
            }
        }

        [Category("ToolTip")]
        [Description("Enable or disable tooltip appearance for this marker.")]
        public bool ToolTipEnabled
        {
            get
            {
                return this.toolTipEnabled;
            }
            set
            {
                this.toolTipEnabled = value;
            }
        }

        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        [Category("ToolTip")]
        public string ToolTipFormat
        {
            get
            {
                return this.toolTipFormat;
            }
            set
            {
                this.toolTipFormat = value;
            }
        }

        public DateTime DateTime
        {
            get
            {
                if (!(this.pad.Series is BarSeries))
                    return this.fill.DateTime;
                int index = this.pad.Series.GetIndex(this.fill.DateTime, IndexOption.Prev);
                Bar bar = index >= 0 ? (this.pad.Series as BarSeries)[index] : (Bar) null;
                if (bar != null)
                    return bar.DateTime;
                else
                    return DateTime.MinValue;
            }
        }

        public FillView(Fill fill, Pad pad)
        {
            this.fill = fill;
            this.pad = pad;
            this.toolTipEnabled = true;
            this.toolTipFormat = "{0} {2} {1} @ {3} {4} {5}";
        }

        public void Paint()
        {
            int index = this.pad.Series.GetIndex(this.fill.DateTime, IndexOption.Prev);
            int x = this.pad.ClientX(this.pad.Series.GetDateTime(index));
            int num1 = this.pad.ClientY(this.fill.Price);
            if (this.pad.Series is BarSeries)
            {
                Bar bar = (this.pad.Series as BarSeries)[index];
                if (bar.OpenDateTime != bar.CloseDateTime)
                {
                    int num2 = Math.Max(2, (int) this.pad.IntervalWidth);
                    x = x - num2 / 2 + (int) ((double) num2 * ((double) (this.fill.DateTime - bar.OpenDateTime).Ticks / (double) (bar.CloseDateTime - bar.OpenDateTime).Ticks));
                }
            }
            float num3 = 8f;
            string str = ((object) this.fill.Side).ToString() + (object) " " + (string) (object) this.fill.Qty + " @ " + this.fill.Price.ToString();
            Font font = !this.selected ? new Font("Arial", 7f) : new Font("Arial", 9f);
            int y = this.fill.Side != OrderSide.Buy ? num1 - 5 : num1 + 5;
            switch (this.fill.Side)
            {
                case OrderSide.Buy:
                    Color color1 = this.buyColor;
                    PointF[] points1 = new PointF[3]
                    {
                        (PointF) new Point(x, y),
                        (PointF) new Point((int) ((double) x - (double) num3 / 2.0), (int) ((double) y + (double) num3 / 2.0)),
                        (PointF) new Point((int) ((double) x + (double) num3 / 2.0), (int) ((double) y + (double) num3 / 2.0))
                    };
                    this.pad.Graphics.DrawPolygon(new Pen(Color.LightGray), points1);
                    this.pad.Graphics.DrawRectangle(new Pen(Color.LightGray), (float) x - num3 / 4f, (float) y + num3 / 2f, num3 / 2f, num3 / 2f);
                    this.pad.Graphics.FillPolygon((Brush) new SolidBrush(color1), points1);
                    this.pad.Graphics.FillRectangle((Brush) new SolidBrush(color1), (float) x - num3 / 4f, (float) ((double) y + (double) num3 / 2.0 - 1.0), num3 / 2f, (float) ((double) num3 / 2.0 + 1.0));
                    break;
                case OrderSide.Sell:
                    Color color2 = this.sellColor;
                    Point[] points2 = new Point[3]
                    {
                        new Point(x, y),
                        new Point((int) ((double) x - (double) num3 / 2.0), (int) ((double) y - (double) num3 / 2.0)),
                        new Point((int) ((double) x + (double) num3 / 2.0), (int) ((double) y - (double) num3 / 2.0))
                    };
                    this.pad.Graphics.DrawPolygon(new Pen(Color.LightGray), points2);
                    this.pad.Graphics.DrawRectangle(new Pen(Color.LightGray), (float) x - num3 / 4f, (float) y - num3, num3 / 2f, (float) ((double) num3 / 2.0 + 1.0));
                    this.pad.Graphics.FillPolygon((Brush) new SolidBrush(color2), points2);
                    this.pad.Graphics.FillRectangle((Brush) new SolidBrush(color2), (float) x - num3 / 4f, (float) y - num3, num3 / 2f, (float) ((double) num3 / 2.0 + 1.0));
                    break;
            }
            if (!this.textEnabled)
                return;
            int num4 = (int) this.pad.Graphics.MeasureString(str, font).Width;
            int num5 = (int) this.pad.Graphics.MeasureString(str, font).Height;
            Color color3 = this.pad.Chart.ItemTextColor;
            if (this.selected)
                color3 = this.pad.Chart.SelectedItemTextColor;
            switch (this.fill.Side)
            {
                case OrderSide.Buy:
                    this.pad.Graphics.DrawString(str, font, (Brush) new SolidBrush(color3), (float) (x - num4 / 2), (float) ((double) y + (double) num3 + 2.0));
                    break;
                case OrderSide.Sell:
                    this.pad.Graphics.DrawString(str, font, (Brush) new SolidBrush(color3), (float) (x - num4 / 2), (float) ((double) y - (double) num3 - 2.0) - (float) num5);
                    break;
            }
        }

        public bool Compare(object obj)
        {
            return obj == this.fill;
        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            this.firstDate = minDate;
            this.lastDate = maxDate;
        }

        public Distance Distance(int x, double y)
        {
            Distance distance = new Distance();
            int index = this.pad.Series.GetIndex(this.fill.DateTime, IndexOption.Prev);
            distance.X = (double) this.pad.ClientX(this.pad.Series.GetDateTime(index));
            distance.Y = this.fill.Price;
            distance.DX = Math.Abs((double) x - distance.X);
            distance.DY = Math.Abs(y - distance.Y);
            StringBuilder stringBuilder = new StringBuilder();
            if (this.fill.DateTime.Second != 0 || this.fill.DateTime.Minute != 0 || this.fill.DateTime.Hour != 0)
                stringBuilder.AppendFormat(this.toolTipFormat, (object) ((object) this.fill.Side).ToString(), (object) this.fill.Instrument.Symbol, (object) this.fill.Qty, (object) this.fill.Price.ToString(), (object) this.fill.DateTime, (object) this.fill.Text);
            else
                stringBuilder.AppendFormat(this.toolTipFormat, (object) ((object) this.fill.Side).ToString(), (object) this.fill.Instrument.Symbol, (object) this.fill.Qty, (object) this.fill.Price.ToString(), (object) this.fill.DateTime.ToShortDateString(), (object) this.fill.Text);
            distance.ToolTipText = ((object) stringBuilder).ToString();
            return distance;
        }

        public void Select()
        {
        }

        public void UnSelect()
        {
        }
    }
}
