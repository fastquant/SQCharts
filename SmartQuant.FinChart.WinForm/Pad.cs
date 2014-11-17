using SmartQuant;
using System;
using System.Collections;
using System.Drawing;

#if GTK
using Compatibility.Gtk;

#else
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public class Pad
    {
        private Chart chart;
        private ArrayList simplePrimitives;
        private SortedRangeList rangeList;
        private SortedRangeList intervalLeftList;
        private SortedRangeList intervalRightList;
        private object selectedObject;
        private int width;
        private int height;
        private int marginLeft;
        private int marginRight;
        private bool onPrimitive;
        private bool outlineEnabled;
        private Rectangle outlineRectangle;
        private string axisLabelFormat;

        public AxisRight Axis { get; private set; }

        internal int AxisGap { get; private set; }

        internal Chart Chart
        {
            get
            {
                return this.chart;
            }
        }

        public bool DrawItems { get; set; }

        public string AxisLabelFormat
        {
            get
            {
                return this.axisLabelFormat != null ? this.axisLabelFormat : string.Format("F{0}", chart.LabelDigitsCount);
            }
            set
            {
                this.axisLabelFormat = value;
            }
        }

        internal IChartDrawable SelectedPrimitive { get; set; }

        public bool DrawGrid { get; set; }

        public PadScaleStyle ScaleStyle { get; set; }

        public int X1 { get; private set; }

        public int X2 { get; private set; }

        public int Y1 { get; private set; }

        public int Y2 { get; private set; }

        public double MaxValue { get; private set; }

        public double MinValue { get; private set; }

        internal int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                X2 = X1 + this.width;
                Axis.SetBounds(X2, Y1, Y2);
            }
        }

        public ISeries Series
        {
            get
            {
                return this.chart.Series;
            }
        }

        public ISeries MainSeries
        {
            get
            {
                return this.chart.MainSeries;
            }
        }

        public double IntervalWidth
        {
            get
            {
                return this.chart.IntervalWidth;
            }
        }

        public int FirstIndex
        {
            get
            {
                return this.chart.FirstIndex;
            }
        }

        public int LastIndex
        {
            get
            {
                return this.chart.LastIndex;
            }
        }

        public Graphics Graphics { get; private set; }

        public ArrayList Primitives { get; private set; }

        public Pad(Chart chart, int x1, int x2, int y1, int y2)
        {
            this.chart = chart;
            this.marginLeft = this.marginRight = 0;
            this.onPrimitive = false;
            this.outlineEnabled = false;
            this.outlineRectangle = Rectangle.Empty;
            DrawGrid = true;
            SetCanvas(x1, x2, y1, y2);
            Primitives = ArrayList.Synchronized(new ArrayList());
            this.simplePrimitives = new ArrayList();
            this.rangeList = new SortedRangeList();
            this.intervalLeftList = new SortedRangeList();
            this.intervalRightList = new SortedRangeList(true);
        }

        public void SetCanvas(int x1, int x2, int y1, int y2)
        {
            X1 = x1 + this.marginLeft;
            X2 = x2 - this.marginRight;
            Y1 = y1;
            Y2 = y2;
            this.width = X2 - X1;
            this.height = Y2 - Y1;
            if (Axis == null)
                Axis = new AxisRight(this.chart, this, x2, y1, y2);
            else
                Axis.SetBounds(x2, y1, y2);
        }

        public void AddPrimitive(IChartDrawable primitive)
        {
            Primitives.Add(primitive);
            if (primitive is IDateDrawable)
                this.rangeList.Add(primitive as IDateDrawable);
            else
                this.simplePrimitives.Add(primitive);
        }

        public void RemovePrimitive(IChartDrawable primitive)
        {
            Primitives.Remove(primitive);
            if (primitive is IDateDrawable)
                this.rangeList[(primitive as IDateDrawable).DateTime].Remove(primitive);
            else
                this.simplePrimitives.Remove(primitive);
        }

        public void ClearPrimitives()
        {
            Primitives.Clear();
            this.rangeList.Clear();
            this.simplePrimitives.Clear();
        }

        public void SetSelectedObject(object obj)
        {
            this.selectedObject = obj;
        }

        internal void Reset()
        {
            ClearPrimitives();
        }

        public int ClientX(DateTime dateTime)
        {
            double num = (double)Width / (double)(this.LastIndex - this.FirstIndex + 1);
            return X1 + (int)((double)(MainSeries.GetIndex(dateTime, IndexOption.Null) - FirstIndex) * num + num / 2.0);
        }

        public int ClientY(double worldY)
        {
            if (ScaleStyle == PadScaleStyle.Log)
                return Y1 + (int)((1.0 - (Math.Log10(worldY) - Math.Log10(MinValue)) / (Math.Log10(MaxValue) - Math.Log10(MinValue))) * (double)this.height);
            else
                return Y1 + (int)((1.0 - (worldY - MinValue) / (MaxValue - MinValue)) * (double)this.height);
        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            foreach (IChartDrawable drawable in Primitives)
                drawable.SetInterval(minDate, maxDate);
        }

        public void DrawHorizontalGrid(Pen pen, double y)
        {
            if (DrawGrid)
                Graphics.DrawLine(pen, X1, this.ClientY(y), X2, this.ClientY(y));
        }

        public void DrawHorizontalTick(Pen pen, double x, double y, int length)
        {
            Graphics.DrawLine(pen, (int)x, this.ClientY(y), (int)x + length, this.ClientY(y));
        }

        internal void PrepareForUpdate()
        {
            AxisGap = -1;
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0)
                return;
            MaxValue = double.MinValue;
            MinValue = double.MaxValue;
            ArrayList arrayList;
            lock (Primitives.SyncRoot)
                arrayList = new ArrayList(Primitives);
            foreach (IChartDrawable chartDrawable in arrayList)
            {
                if ((DrawItems || chartDrawable is SeriesView) && chartDrawable is IZoomable)
                {
                    PadRange padRangeY = (chartDrawable as IZoomable).GetPadRangeY(this);
                    if (padRangeY.IsValid)
                    {
                        if (MaxValue < padRangeY.Max)
                            MaxValue = padRangeY.Max;
                        if (MinValue > padRangeY.Min)
                            MinValue = padRangeY.Min;
                    }
                }
            }
            if (MinValue != double.MaxValue && MaxValue != double.MinValue)
            {
                MinValue -= (MaxValue - MinValue) / 10.0;
                MaxValue += (MaxValue - MinValue) / 10.0;
                AxisGap = Axis.GetAxisGap();
            }
            else
                AxisGap = -1;
        }

        internal void Update(Graphics g)
        {
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0)
                return;
            Graphics = g;
            if (MinValue != double.MaxValue && MaxValue != double.MinValue)
            {
                Axis.Paint();
                g.SetClip(new Rectangle(X1, Y1, this.width, this.height));
                foreach (IChartDrawable drawable in this.simplePrimitives)
                {
                    if (DrawItems || drawable is SeriesView)
                        drawable.Paint();
                }
                if (DrawItems)
                {
                    int nextIndex = this.rangeList.GetNextIndex(this.chart.MainSeries.GetDateTime(this.chart.FirstIndex));
                    int prevIndex = this.rangeList.GetPrevIndex(this.chart.MainSeries.GetDateTime(this.chart.LastIndex));
                    if (nextIndex != -1 && prevIndex != -1)
                    {
                        for (int index = nextIndex; index <= prevIndex; ++index)
                        {
                            foreach (IChartDrawable drawable in this.rangeList[index])
                                drawable.Paint();
                        }
                    }
                }
                g.ResetClip();
            }
            bool flag = true;
            float num = (float)(X1 + 2);
            foreach (IChartDrawable drawable in this.simplePrimitives)
            {
                if (drawable is SeriesView)
                {
                    SeriesView seriesView = drawable as SeriesView;
                    if (seriesView.DisplayNameEnabled)
                    {
                        string str;
                        if (flag)
                        {
                            str = seriesView.DisplayName;
                            flag = false;
                        }
                        else
                            str = " " + seriesView.DisplayName;
                        SizeF sizeF = g.MeasureString(str, this.chart.Font);
                        g.FillRectangle(new SolidBrush(this.chart.CanvasColor), num + 2f, (float)(Y1 + 2), sizeF.Width, sizeF.Height);
                        g.DrawString(str, this.chart.Font, new SolidBrush(seriesView.Color), num + 2f, (float)(Y1 + 2));
                        num += sizeF.Width;
                    }
                }
            }
            if (this.outlineEnabled)
                g.DrawRectangle(new Pen(Color.Green), this.outlineRectangle);
        }

        public DateTime GetDateTime(int x)
        {
            return this.chart.GetDateTime(x);
        }

        public double WorldY(int y)
        {
            if (ScaleStyle == PadScaleStyle.Log)
                return Math.Pow(10.0, (double)(Y2 - y) / (double)(Y2 - Y1) * (Math.Log10(MaxValue) - Math.Log10(MinValue))) * MinValue;
            else
                return MinValue + (MaxValue - MinValue) * (double)(Y2 - y) / (double)(Y2 - Y1);
        }

        public virtual void MouseDown(MouseEventArgs Event)
        {
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0 || (Event.X <= X1 || Event.X >= X2))
                return;
            double num1 = 10.0;
            double num2 = (MaxValue - MinValue) / 20.0;
            int x = Event.X;
            double y = this.WorldY(Event.Y);
            foreach (IChartDrawable primitive in this.simplePrimitives)
            {
                if (primitive is DSView)
                {
                    Distance distance = primitive.Distance(x, y);
                    if (distance != null)
                    {
                        this.chart.UnSelectAll();
                        if (distance.DX < num1 && distance.DY < num2)
                        {
                            primitive.Select();
                            this.chart.ContentUpdated = true;
                            this.chart.Invalidate();
                            this.chart.ShowProperties(primitive as DSView, this, false);
                            SelectedPrimitive = primitive;
                            if (!this.chart.ContextMenuEnabled || Event.Button != System.Windows.Forms.MouseButtons.Right)
                                break;
                            #if GTK
                            #else
                            InitContextMenu(primitive).Show(this.chart, new Point(Event.X, Event.Y));
                            #endif
                            break;
                        }
                    }
                }
            }
        }

        public virtual void MouseUp(MouseEventArgs Event)
        {
            this.chart.ContentUpdated = true;
            this.chart.Invalidate();
        }

        public virtual void MouseMove(MouseEventArgs Event)
        {
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0 || (Event.X <= X1 || Event.X >= X2))
                return;
            double num1 = 10.0;
            double num2 = (MaxValue - MinValue) / 20.0;
            int x = Event.X;
            double y = this.WorldY(Event.Y);
            bool flag = false;
            string caption = "";
            this.onPrimitive = false;
            foreach (IChartDrawable chartDrawable in this.simplePrimitives)
            {
                if (DrawItems || chartDrawable is SeriesView)
                {
                    Distance distance = chartDrawable.Distance(x, y);
                    if (distance != null && distance.DX < num1 && distance.DY < num2)
                    {
                        if (chartDrawable.ToolTipEnabled)
                        {
                            if (caption != "")
                                caption = caption + "\n\n";
                            caption = caption + distance.ToolTipText;
                            flag = true;
                        }
                        this.onPrimitive = true;
                        #if GTK
                        #else
                        if (Cursor.Current != Cursors.Hand)
                            Cursor.Current = Cursors.Hand;
                        #endif
                    }
                }
            }
            if (DrawItems)
            {
                int num3 = 0;
                int index1 = this.chart.MainSeries.GetIndex(this.GetDateTime(Event.X), IndexOption.Null);
                DateTime dateTime1 = this.chart.MainSeries.GetDateTime(index1);
                if (index1 != 0)
                {
                    DateTime dateTime2 = this.chart.MainSeries.GetDateTime(index1 - 1);
                    num3 = this.rangeList.GetNextIndex(dateTime2);
                    if (this.rangeList.Contains(dateTime2))
                        ++num3;
                }
                int prevIndex = this.rangeList.GetPrevIndex(dateTime1);
                if (num3 != -1 && prevIndex != -1)
                {
                    for (int index2 = num3; index2 <= prevIndex; ++index2)
                    {
                        foreach (IChartDrawable chartDrawable in this.rangeList[index2])
                        {
                            Distance distance = chartDrawable.Distance(x, y);
                            if (distance != null && distance.DX < num1 && distance.DY < num2)
                            {
                                if (chartDrawable.ToolTipEnabled)
                                {
                                    if (caption != "")
                                        caption = caption + "\n\n";
                                    caption = caption + distance.ToolTipText;
                                    flag = true;
                                }
                                this.onPrimitive = true;
                                #if GTK
                                #else
                                if (Cursor.Current != Cursors.Hand)
                                    Cursor.Current = Cursors.Hand;
                                #endif
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                this.chart.ToolTip.SetToolTip(this.chart, caption);
                this.chart.ToolTip.Active = true;
            }
            else
                this.chart.ToolTip.Active = false;
        }

        #if GTK
        #else
        private ContextMenuStrip InitContextMenu(IChartDrawable primitive)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
            ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
            toolStripMenuItem1.Text = "Delete " + (primitive as DSView).DisplayName;
            toolStripMenuItem1.Click += new EventHandler(this.DeleteMenuItem_Click);
            toolStripMenuItem1.Image = this.chart.PrimitiveDeleteImage;
            toolStripMenuItem2.Text = "Properties " + (primitive as DSView).DisplayName;
            toolStripMenuItem2.Click += new EventHandler(this.PropertiesMenuItem_Click);
            toolStripMenuItem2.Image = this.chart.PrimitivePropertiesImage;
            contextMenuStrip.Items.Add((ToolStripItem)toolStripMenuItem1);
            contextMenuStrip.Items.Add((ToolStripItem)toolStripSeparator);
            contextMenuStrip.Items.Add((ToolStripItem)toolStripMenuItem2);
            return contextMenuStrip;
        }
        #endif

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            this.chart.ShowProperties(SelectedPrimitive as DSView, this, true);
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            Primitives.Remove(SelectedPrimitive);
            this.simplePrimitives.Remove(SelectedPrimitive);
            this.chart.ContentUpdated = true;
            this.chart.Invalidate();
        }

        public bool IsInRange(double x, double y)
        {
            return X1 <= x && x <= X2 && Y1 <= y && y <= Y2;
        }
    }
}
