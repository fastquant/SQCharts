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
        private bool drawGrid = true;
        private Chart chart;
        private AxisRight axis;
        private ArrayList primitives;
        private ArrayList simplePrimitives;
        private SortedRangeList rangeList;
        private SortedRangeList intervalLeftList;
        private SortedRangeList intervalRightList;
        private int axisGap;
        private object selectedObject;
        private int x1;
        private int x2;
        private int y1;
        private int y2;
        private int width;
        private int height;
        private int marginLeft;
        private int marginRight;
        private double maxValue;
        private double minValue;
        private Graphics graphics;
        private IChartDrawable selectedPrimitive;
        private bool onPrimitive;
        private bool outlineEnabled;
        private Rectangle outlineRectangle;
        private PadScaleStyle scaleStyle;
        private string axisLabelFormat;
        private bool drawItems;

        public AxisRight Axis
        {
            get
            {
                return this.axis;
            }
        }

        internal int AxisGap
        {
            get
            {
                return this.axisGap;
            }
        }

        internal Chart Chart
        {
            get
            {
                return this.chart;
            }
        }

        public bool DrawItems
        {
            get
            {
                return this.drawItems;
            }
            set
            {
                this.drawItems = value;
            }
        }

        public string AxisLabelFormat
        {
            get
            {
                if (this.axisLabelFormat == null)
                    return "F" + this.chart.LabelDigitsCount.ToString();
                else
                    return this.axisLabelFormat;
            }
            set
            {
                this.axisLabelFormat = value;
            }
        }

        internal IChartDrawable SelectedPrimitive
        {
            get
            {
                return this.selectedPrimitive;
            }
            set
            {
                this.selectedPrimitive = value;
            }
        }

        public bool DrawGrid
        {
            get
            {
                return this.drawGrid;
            }
            set
            {
                this.drawGrid = value;
            }
        }

        public PadScaleStyle ScaleStyle
        {
            get
            {
                return this.scaleStyle;
            }
            set
            {
                this.scaleStyle = value;
            }
        }

        public int X1
        {
            get
            {
                return this.x1;
            }
        }

        public int X2
        {
            get
            {
                return this.x2;
            }
        }

        public int Y1
        {
            get
            {
                return this.y1;
            }
        }

        public int Y2
        {
            get
            {
                return this.y2;
            }
        }

        public double MaxValue
        {
            get
            {
                return this.maxValue;
            }
        }

        public double MinValue
        {
            get
            {
                return this.minValue;
            }
        }

        internal int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                this.x2 = this.x1 + this.width;
                this.axis.SetBounds(this.x2, this.y1, this.y2);
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

        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        public ArrayList Primitives
        {
            get
            {
                return this.primitives;
            }
        }

        public Pad(Chart chart, int x1, int x2, int y1, int y2)
        {
            this.chart = chart;
            this.SetCanvas(x1, x2, y1, y2);
            this.primitives = ArrayList.Synchronized(new ArrayList());
            this.simplePrimitives = new ArrayList();
            this.rangeList = new SortedRangeList();
            this.intervalLeftList = new SortedRangeList();
            this.intervalRightList = new SortedRangeList(true);
        }

        public void SetCanvas(int x1, int x2, int y1, int y2)
        {
            this.y1 = y1;
            this.y2 = y2;
            this.x1 = x1 + this.marginLeft;
            this.x2 = x2 - this.marginRight;
            this.width = this.x2 - this.x1;
            this.height = this.y2 - this.y1;
            if (this.axis == null)
                this.axis = new AxisRight(this.chart, this, x2, y1, y2);
            else
                this.axis.SetBounds(x2, y1, y2);
        }

        public void AddPrimitive(IChartDrawable primitive)
        {
            this.primitives.Add((object)primitive);
            if (primitive is IDateDrawable)
                this.rangeList.Add(primitive as IDateDrawable);
            else
                this.simplePrimitives.Add((object)primitive);
        }

        public void RemovePrimitive(IChartDrawable primitive)
        {
            this.primitives.Remove((object)primitive);
            if (primitive is IDateDrawable)
                this.rangeList[(primitive as IDateDrawable).DateTime].Remove((object)primitive);
            else
                this.simplePrimitives.Remove((object)primitive);
        }

        public void ClearPrimitives()
        {
            this.primitives.Clear();
            this.rangeList.Clear();
            this.simplePrimitives.Clear();
        }

        public void SetSelectedObject(object obj)
        {
            this.selectedObject = obj;
        }

        internal void Reset()
        {
            this.ClearPrimitives();
        }

        public int ClientX(DateTime dateTime)
        {
            double num = (double)this.width / (double)(this.LastIndex - this.FirstIndex + 1);
            return this.x1 + (int)((double)(this.MainSeries.GetIndex(dateTime, IndexOption.Null) - this.FirstIndex) * num + num / 2.0);
        }

        public int ClientY(double worldY)
        {
            if (this.scaleStyle == PadScaleStyle.Log)
                return this.y1 + (int)((1.0 - (Math.Log10(worldY) - Math.Log10(this.minValue)) / (Math.Log10(this.maxValue) - Math.Log10(this.minValue))) * (double)this.height);
            else
                return this.y1 + (int)((1.0 - (worldY - this.minValue) / (this.maxValue - this.minValue)) * (double)this.height);
        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            foreach (IChartDrawable chartDrawable in this.primitives)
                chartDrawable.SetInterval(minDate, maxDate);
        }

        public void DrawHorizontalGrid(Pen pen, double y)
        {
            if (!this.drawGrid)
                return;
            this.graphics.DrawLine(pen, this.x1, this.ClientY(y), this.x2, this.ClientY(y));
        }

        public void DrawHorizontalTick(Pen pen, double x, double y, int length)
        {
            this.graphics.DrawLine(pen, (int)x, this.ClientY(y), (int)x + length, this.ClientY(y));
        }

        internal void PrepareForUpdate()
        {
            this.axisGap = -1;
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0)
                return;
            this.maxValue = double.MinValue;
            this.minValue = double.MaxValue;
            ArrayList arrayList;
            lock (this.primitives.SyncRoot)
                arrayList = new ArrayList((ICollection)this.primitives);
            foreach (IChartDrawable chartDrawable in arrayList)
            {
                if ((this.drawItems || chartDrawable is SeriesView) && chartDrawable is IZoomable)
                {
                    PadRange padRangeY = (chartDrawable as IZoomable).GetPadRangeY(this);
                    if (padRangeY.IsValid)
                    {
                        if (this.maxValue < padRangeY.Max)
                            this.maxValue = padRangeY.Max;
                        if (this.minValue > padRangeY.Min)
                            this.minValue = padRangeY.Min;
                    }
                }
            }
            if (this.minValue != double.MaxValue && this.maxValue != double.MinValue)
            {
                this.minValue -= (this.maxValue - this.minValue) / 10.0;
                this.maxValue += (this.maxValue - this.minValue) / 10.0;
                this.axisGap = this.axis.GetAxisGap();
            }
            else
                this.axisGap = -1;
        }

        internal void Update(Graphics graphics)
        {
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0)
                return;
            this.graphics = graphics;
            if (this.minValue != double.MaxValue && this.maxValue != double.MinValue)
            {
                this.axis.Paint();
                graphics.SetClip(new Rectangle(this.x1, this.y1, this.width, this.height));
                foreach (IChartDrawable chartDrawable in this.simplePrimitives)
                {
                    if (this.drawItems || chartDrawable is SeriesView)
                        chartDrawable.Paint();
                }
                if (this.drawItems)
                {
                    int nextIndex = this.rangeList.GetNextIndex(this.chart.MainSeries.GetDateTime(this.chart.FirstIndex));
                    int prevIndex = this.rangeList.GetPrevIndex(this.chart.MainSeries.GetDateTime(this.chart.LastIndex));
                    if (nextIndex != -1 && prevIndex != -1)
                    {
                        for (int index = nextIndex; index <= prevIndex; ++index)
                        {
                            foreach (IChartDrawable chartDrawable in this.rangeList[index])
                                chartDrawable.Paint();
                        }
                    }
                }
                graphics.ResetClip();
            }
            bool flag = true;
            float num = (float)(this.x1 + 2);
            foreach (IChartDrawable chartDrawable in this.simplePrimitives)
            {
                if (chartDrawable is SeriesView)
                {
                    SeriesView seriesView = chartDrawable as SeriesView;
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
                        SizeF sizeF = graphics.MeasureString(str, this.chart.Font);
                        graphics.FillRectangle((Brush)new SolidBrush(this.chart.CanvasColor), num + 2f, (float)(this.y1 + 2), sizeF.Width, sizeF.Height);
                        graphics.DrawString(str, this.chart.Font, (Brush)new SolidBrush(seriesView.Color), num + 2f, (float)(this.y1 + 2));
                        num += sizeF.Width;
                    }
                }
            }
            if (!this.outlineEnabled)
                return;
            graphics.DrawRectangle(new Pen(Color.Green), this.outlineRectangle);
        }

        public DateTime GetDateTime(int x)
        {
            return this.chart.GetDateTime(x);
        }

        public double WorldY(int y)
        {
            if (this.scaleStyle == PadScaleStyle.Log)
                return Math.Pow(10.0, (double)(this.y2 - y) / (double)(this.y2 - this.y1) * (Math.Log10(this.maxValue) - Math.Log10(this.minValue))) * this.minValue;
            else
                return this.minValue + (this.maxValue - this.minValue) * (double)(this.y2 - y) / (double)(this.y2 - this.y1);
        }

        public virtual void MouseDown(MouseEventArgs Event)
        {
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0 || (Event.X <= this.x1 || Event.X >= this.x2))
                return;
            double num1 = 10.0;
            double num2 = (this.maxValue - this.minValue) / 20.0;
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
                            this.selectedPrimitive = primitive;
                            if (!this.chart.ContextMenuEnabled || Event.Button != System.Windows.Forms.MouseButtons.Right)
                                break;
                            #if GTK
                            #else
                            this.InitContextMenu(primitive).Show((Control)this.chart, new Point(Event.X, Event.Y));
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
            if (this.chart.MainSeries == null || this.chart.MainSeries.Count == 0 || (Event.X <= this.x1 || Event.X >= this.x2))
                return;
            double num1 = 10.0;
            double num2 = (this.maxValue - this.minValue) / 20.0;
            int x = Event.X;
            double y = this.WorldY(Event.Y);
            bool flag = false;
            string caption = "";
            this.onPrimitive = false;
            foreach (IChartDrawable chartDrawable in this.simplePrimitives)
            {
                if (this.drawItems || chartDrawable is SeriesView)
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
            if (this.drawItems)
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
            #if GTK
            #else
            if (flag)
            {
                this.chart.ToolTip.SetToolTip(this.chart, caption);
                this.chart.ToolTip.Active = true;
            }
            else
                this.chart.ToolTip.Active = false;
            #endif
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
            this.chart.ShowProperties(this.selectedPrimitive as DSView, this, true);
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            this.primitives.Remove((object)this.selectedPrimitive);
            this.simplePrimitives.Remove((object)this.selectedPrimitive);
            this.chart.ContentUpdated = true;
            this.chart.Invalidate();
        }

        public bool IsInRange(double x, double y)
        {
            return x >= (double)this.x1 && x <= (double)this.x2 && (y >= (double)this.y1 && y <= (double)this.y2);
        }
    }
}
