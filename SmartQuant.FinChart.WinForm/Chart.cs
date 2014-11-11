using SmartQuant;
using SmartQuant.FinChart.Objects;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public partial class Chart : UserControl
    {
        private int prevMouseX = -1;
        private int prevMouseY = -1;
        protected SmoothingMode TimeSeriesSmoothingMode = SmoothingMode.HighSpeed;
        protected PadList pads = new PadList();
        protected int minCountOfBars = 125;
        protected int canvasLeftOffset = 20;
        protected int canvasTopOffset = 20;
        protected int canvasRightOffset = 20;
        protected int canvasBottomOffset = 30;
        protected Color canvasColor = Color.MidnightBlue;
        protected ArrayList padsHeightArray = new ArrayList();
        protected int padSplitIndex = -1;
        protected bool contentUpdated = true;
        private ChartActionType actionType = ChartActionType.None;
        protected ChartUpdateStyle updateStyle = ChartUpdateStyle.Trailing;
        protected int minAxisGap = 50;
        private bool contextMenuEnabled = true;
        private int labelDigitsCount = 2;
        private int rightAxesFontSize = 7;
        private Color dateTipRectangleColor = Color.LightGray;
        private Color dateTipTextColor = Color.Black;
        private Color valTipRectangleColor = Color.LightGray;
        private Color valTipTextColor = Color.Black;
        private Color crossColor = Color.DarkGray;
        private Color borderColor = Color.Gray;
        private Color splitterColor = Color.LightGray;
        private Color candleUpColor = Color.Black;
        private Color candleDownColor = Color.Lime;
        private Color volumeColor = Color.SteelBlue;
        private Color rightAxisGridColor = Color.DimGray;
        private Color rightAxisTextColor = Color.LightGray;
        private Color rightAxisMinorTicksColor = Color.LightGray;
        private Color rightAxisMajorTicksColor = Color.LightGray;
        private Color itemTextColor = Color.LightGray;
        private Color selectedItemTextColor = Color.Yellow;
        private Color selectedFillHighlightColor = Color.LightBlue;
        private Color activeStopColor = Color.Yellow;
        private Color executedStopColor = Color.MediumSeaGreen;
        private Color canceledStopColor = Color.Gray;
        private object lockObject = new object();
        private DateTime lastDate = DateTime.MaxValue;
        private DateTime polosaDate = DateTime.MinValue;
        private IContainer components;
        private Image primitiveDeleteImage;
        private Image primitivePropertiesImage;
        protected Color sessionGridColor;
        protected TimeSpan sessionStart;
        protected TimeSpan sessionEnd;
        protected bool sessionGridEnabled;
        protected SmoothingMode smoothingMode;
        protected BSStyle barSeriesStyle;
        protected SeriesView mainSeriesView;
        protected ISeries mainSeries;
        protected ISeries series;
        protected int firstIndex;
        protected int lastIndex;
        protected Graphics graphics;
        protected double intervalWidth;
        protected AxisBottom axisBottom;
        protected int mouseX;
        protected int mouseY;
        protected bool padSplit;
        protected bool isMouseOverCanvas;
        protected Bitmap bitmap;
        protected DateTime leftDateTime;
        protected DateTime rightDateTime;
        protected bool volumePadShown;
        protected PadScaleStyle scaleStyle;
        private bool drawItems;
        internal Font RightAxesFont;
        private Color chartBackColor;

        public bool ContextMenuEnabled
        {
            get
            {
                return this.contextMenuEnabled;
            }
            set
            {
                this.contextMenuEnabled = value;
            }
        }

        public int RightAxesFontSize
        {
            get
            {
                return this.rightAxesFontSize;
            }
            set
            {
                this.rightAxesFontSize = value;
                this.RightAxesFont = new Font(this.Font.FontFamily, (float) this.rightAxesFontSize);
            }
        }

        public int LabelDigitsCount
        {
            get
            {
                return this.labelDigitsCount;
            }
            set
            {
                this.labelDigitsCount = value;
            }
        }

        public Image PrimitiveDeleteImage
        {
            get
            {
                return this.primitiveDeleteImage;
            }
            set
            {
                this.primitiveDeleteImage = value;
            }
        }

        public Image PrimitivePropertiesImage
        {
            get
            {
                return this.primitivePropertiesImage;
            }
            set
            {
                this.primitivePropertiesImage = value;
            }
        }

        [Browsable(false)]
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

        [Browsable(false)]
        public bool VolumePadVisible
        {
            get
            {
                return this.volumePadShown;
            }
            set
            {
                if (value)
                    this.ShowVolumePad();
                else
                    this.HideVolumePad();
            }
        }

        public ChartUpdateStyle UpdateStyle
        {
            get
            {
                return this.updateStyle;
            }
            set
            {
                this.updateStyle = value;
                this.EmitUpdateStyleChanged();
            }
        }

        public BSStyle BarSeriesStyle
        {
            get
            {
                return this.barSeriesStyle;
            }
            set
            {
                this.drawItems = true;
                if (this.barSeriesStyle == value)
                    return;
                lock (this.lockObject)
                {
                    this.barSeriesStyle = value;
                    if (this.mainSeries != null)
                    {
                        bool local_0 = this.SetBarSeriesStyle(this.barSeriesStyle, false);
                        if (this.volumePadShown)
                        {
                            int temp_55 = local_0 ? 1 : 0;
                        }
                        if (local_0)
                        {
                            this.firstIndex = Math.Max(0, this.mainSeries.Count - this.minCountOfBars);
                            this.lastIndex = this.mainSeries.Count - 1;
                            if (this.mainSeries.Count == 0)
                                this.firstIndex = -1;
                            if (this.lastIndex >= 0)
                                this.SetIndexInterval(this.firstIndex, this.lastIndex);
                        }
                        this.contentUpdated = true;
                    }
                    this.EmitBarSeriesStyleChanged();
                    this.Invalidate();
                }
            }
        }

        [Category("Transformation")]
        [Description("")]
        public bool SessionGridEnabled
        {
            get
            {
                return this.sessionGridEnabled;
            }
            set
            {
                this.sessionGridEnabled = value;
            }
        }

        [Description("")]
        [Category("Transformation")]
        public Color SessionGridColor
        {
            get
            {
                return this.sessionGridColor;
            }
            set
            {
                this.sessionGridColor = value;
            }
        }

        [Description("")]
        [Category("Transformation")]
        public TimeSpan SessionStart
        {
            get
            {
                return this.sessionStart;
            }
            set
            {
                this.sessionStart = value;
            }
        }

        [Category("Transformation")]
        [Description("")]
        public TimeSpan SessionEnd
        {
            get
            {
                return this.sessionEnd;
            }
            set
            {
                this.sessionEnd = value;
            }
        }

        public double IntervalWidth
        {
            get
            {
                return this.intervalWidth;
            }
        }

        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        public SmoothingMode SmoothingMode
        {
            get
            {
                return this.smoothingMode;
            }
            set
            {
                this.smoothingMode = value;
            }
        }

        internal ISeries Series
        {
            get
            {
                return this.series;
            }
        }

        public ISeries MainSeries
        {
            get
            {
                return this.mainSeries;
            }
        }

        public int FirstIndex
        {
            get
            {
                return this.firstIndex;
            }
        }

        public int LastIndex
        {
            get
            {
                return this.lastIndex;
            }
        }

        public int PadCount
        {
            get
            {
                return this.pads.Count;
            }
        }

        public Color CanvasColor
        {
            get
            {
                return this.canvasColor;
            }
            set
            {
                this.contentUpdated = true;
                this.canvasColor = value;
            }
        }

        public Color ChartBackColor
        {
            get
            {
                return this.chartBackColor;
            }
            set
            {
                this.contentUpdated = true;
                this.chartBackColor = value;
            }
        }

        public ChartActionType ActionType
        {
            get
            {
                return this.actionType;
            }
            set
            {
                if (this.actionType == value)
                    return;
                this.actionType = value;
                this.EmitActionTypeChanged();
                this.Invalidate();
            }
        }

        public int MinNumberOfBars
        {
            get
            {
                return this.minCountOfBars;
            }
            set
            {
                this.minCountOfBars = value;
            }
        }

        internal bool ContentUpdated
        {
            get
            {
                return this.contentUpdated;
            }
            set
            {
                this.contentUpdated = value;
            }
        }

        public PadList Pads
        {
            get
            {
                return this.pads;
            }
        }

        public Color SelectedFillHighlightColor
        {
            get
            {
                return this.selectedFillHighlightColor;
            }
            set
            {
                this.selectedFillHighlightColor = Color.FromArgb(100, value);
                this.contentUpdated = true;
            }
        }

        public Color ItemTextColor
        {
            get
            {
                return this.itemTextColor;
            }
            set
            {
                this.itemTextColor = value;
                this.contentUpdated = true;
            }
        }

        public Color SelectedItemTextColor
        {
            get
            {
                return this.selectedItemTextColor;
            }
            set
            {
                this.selectedItemTextColor = value;
                this.contentUpdated = true;
            }
        }

        public Color BottomAxisGridColor
        {
            get
            {
                return this.axisBottom.GridColor;
            }
            set
            {
                this.contentUpdated = true;
                this.axisBottom.GridColor = value;
            }
        }

        public Color BottomAxisLabelColor
        {
            get
            {
                return this.axisBottom.LabelColor;
            }
            set
            {
                this.contentUpdated = true;
                this.axisBottom.LabelColor = value;
            }
        }

        public Color RightAxisGridColor
        {
            get
            {
                return this.rightAxisGridColor;
            }
            set
            {
                this.contentUpdated = true;
                foreach (Pad pad in this.pads)
                    pad.Axis.GridColor = value;
                this.rightAxisGridColor = value;
            }
        }

        public Color RightAxisTextColor
        {
            get
            {
                return this.rightAxisTextColor;
            }
            set
            {
                this.contentUpdated = true;
                foreach (Pad pad in this.pads)
                    pad.Axis.LabelColor = value;
                this.rightAxisTextColor = value;
            }
        }

        public Color RightAxisMinorTicksColor
        {
            get
            {
                return this.rightAxisMinorTicksColor;
            }
            set
            {
                this.contentUpdated = true;
                foreach (Pad pad in this.pads)
                    pad.Axis.MinorTicksColor = value;
                this.rightAxisMinorTicksColor = value;
            }
        }

        public Color RightAxisMajorTicksColor
        {
            get
            {
                return this.rightAxisMajorTicksColor;
            }
            set
            {
                this.contentUpdated = true;
                foreach (Pad pad in this.pads)
                    pad.Axis.MajorTicksColor = value;
                this.rightAxisMajorTicksColor = value;
            }
        }

        public Color DateTipRectangleColor
        {
            get
            {
                return this.dateTipRectangleColor;
            }
            set
            {
                this.contentUpdated = true;
                this.dateTipRectangleColor = value;
            }
        }

        public Color DateTipTextColor
        {
            get
            {
                return this.dateTipTextColor;
            }
            set
            {
                this.contentUpdated = true;
                this.dateTipTextColor = value;
            }
        }

        public Color ValTipRectangleColor
        {
            get
            {
                return this.valTipRectangleColor;
            }
            set
            {
                this.contentUpdated = true;
                this.valTipRectangleColor = value;
            }
        }

        public Color ValTipTextColor
        {
            get
            {
                return this.valTipTextColor;
            }
            set
            {
                this.contentUpdated = true;
                this.valTipTextColor = value;
            }
        }

        public Color CrossColor
        {
            get
            {
                return this.crossColor;
            }
            set
            {
                this.contentUpdated = true;
                this.crossColor = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.contentUpdated = true;
                this.borderColor = value;
            }
        }

        public Color SplitterColor
        {
            get
            {
                return this.splitterColor;
            }
            set
            {
                this.contentUpdated = true;
                this.splitterColor = value;
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
                this.pads[0].ScaleStyle = value;
                this.contentUpdated = true;
                this.Invalidate();
                this.EmitScaleStyleChanged();
            }
        }

        public event EventHandler UpdateStyleChanged;

        public event EventHandler VolumeVisibleChanged;

        public event EventHandler ActionTypeChanged;

        public event EventHandler BarSeriesStyleChanged;

        public event EventHandler ScaleStyleChanged;

        public Chart()
        {
            InitializeComponent();
            #if GTK
            #else
            this.RightAxesFont = new Font(this.Font.FontFamily, (float) this.rightAxesFontSize);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.canvasLeftOffset = 10;
            this.canvasTopOffset = 10;
            this.canvasRightOffset = 40;
            this.canvasBottomOffset = 40;
            this.MouseWheel += new MouseEventHandler(this.Chart_MouseWheel);
            this.AddPad();
            this.axisBottom = new AxisBottom(this, this.canvasLeftOffset, this.Width - this.canvasRightOffset, this.Height - this.canvasTopOffset);
            this.scrollBar.Minimum = 0;
            this.chartBackColor = Color.MidnightBlue;
            this.firstIndex = -1;
            this.lastIndex = -1;
            #endif
        }

        public Chart(TimeSeries mainSeries)
            : this()
        {
            this.SetMainSeries((ISeries) mainSeries);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }


        internal void DrawVerticalTick(Pen Pen, long X, int Length)
        {
            this.graphics.DrawLine(Pen, this.ClientX(new DateTime(X)), this.canvasTopOffset + this.Height - (this.canvasBottomOffset + this.canvasTopOffset), this.ClientX(new DateTime(X)), this.canvasTopOffset + this.Height - (this.canvasBottomOffset + this.canvasTopOffset) + Length);
        }

        internal void DrawVerticalGrid(Pen Pen, long X)
        {
            int x1 = this.ClientX(new DateTime(X));
            this.graphics.DrawLine(Pen, x1, this.canvasTopOffset, this.ClientX(new DateTime(X)), this.canvasTopOffset + this.Height - (this.canvasBottomOffset + this.canvasTopOffset));
        }

        internal void DrawSessionGrid(Pen Pen, long X)
        {
            this.graphics.DrawLine(Pen, (int) ((double) this.ClientX(new DateTime(X)) - this.intervalWidth / 2.0), this.canvasTopOffset, (int) ((double) this.ClientX(new DateTime(X)) - this.intervalWidth / 2.0), this.canvasTopOffset + this.Height - (this.canvasBottomOffset + this.canvasTopOffset));
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color)
        {
            this.DrawSeries(series, padNumber, color, SearchOption.ExactFirst);
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SearchOption option)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                DSView local_0 = new DSView(this.pads[padNumber], series, color, option, this.TimeSeriesSmoothingMode);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style)
        {
            this.DrawSeries(series, padNumber, color, style, SearchOption.ExactFirst, this.TimeSeriesSmoothingMode);
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style, SmoothingMode smoothingMode)
        {
            this.DrawSeries(series, padNumber, color, style, SearchOption.ExactFirst, smoothingMode);
        }

        public DSView DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style, SearchOption option, SmoothingMode smoothingMode)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                DSView local_0 = new DSView(this.pads[padNumber], series, color, option, smoothingMode);
                local_0.Style = style;
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
                return local_0;
            }
        }

        public void DrawFill(Fill fill, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                FillView local_0 = new FillView(fill, this.pads[padNumber]);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
            }
        }

        private void primitive_Updated(object sender, EventArgs e)
        {
            this.contentUpdated = true;
            this.Invalidate();
        }

        public void DrawLine(DrawingLine line, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                LineView local_0 = new LineView(line, this.pads[padNumber]);
                line.Updated += new EventHandler(this.primitive_Updated);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        public void DrawEllipse(DrawingEllipse circle, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                EllipseView local_0 = new EllipseView(circle, this.pads[padNumber]);
                circle.Updated += new EventHandler(this.primitive_Updated);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        public void DrawRectangle(DrawingRectangle rect, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                RectangleView local_0 = new RectangleView(rect, this.pads[padNumber]);
                rect.Updated += new EventHandler(this.primitive_Updated);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        public void DrawPath(DrawingPath path, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                PathView local_0 = new PathView(path, this.pads[padNumber]);
                path.Updated += new EventHandler(this.primitive_Updated);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        public void DrawImage(DrawingImage image, int padNumber)
        {
            lock (this.lockObject)
            {
                if (!this.volumePadShown && padNumber > 1)
                    --padNumber;
                ImageView local_0 = new ImageView(image, this.pads[padNumber]);
                image.Updated += new EventHandler(this.primitive_Updated);
                this.pads[padNumber].AddPrimitive((IChartDrawable) local_0);
                local_0.SetInterval(this.leftDateTime, this.rightDateTime);
                this.contentUpdated = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (this.lockObject)
            {
                try
                {
                    this.Update(e.Graphics);
                    if (this.lastIndex <= 0 || this.firstIndex < 0)
                        return;
                    #if GTK
                    #else
                    if (this.scrollBar.Maximum != this.mainSeries.Count - (this.lastIndex - this.firstIndex + 1) + this.scrollBar.LargeChange - 1)
                        this.scrollBar.Maximum = this.mainSeries.Count - (this.lastIndex - this.firstIndex + 1) + this.scrollBar.LargeChange - 1;
                    if (this.scrollBar.Value == this.firstIndex)
                        return;
                    this.scrollBar.Value = this.firstIndex;
                    #endif
                }
                catch (Exception exception_0)
                {
                }
            }
        }

        private void Update(Graphics graphics)
        {
            if (this.lastIndex - this.firstIndex + 1 == 0)
                return;
            int num1 = this.Width - this.canvasLeftOffset - this.canvasRightOffset;
            int height = this.Height;
            this.intervalWidth = (double) (num1 / (this.lastIndex - this.firstIndex + 1));
            if (this.contentUpdated)
            {
                if (this.bitmap != null)
                    this.bitmap.Dispose();
                this.bitmap = new Bitmap(this.Width, this.Height);
                Graphics graphics1 = Graphics.FromImage((Image) this.bitmap);
                graphics1.SmoothingMode = this.smoothingMode;
                graphics1.Clear(this.chartBackColor);
                this.graphics = graphics1;
                int val1 = int.MinValue;
                foreach (Pad pad in this.pads)
                {
                    pad.PrepareForUpdate();
                    if (val1 < pad.AxisGap + 2)
                        val1 = pad.AxisGap + 2;
                }
                this.canvasRightOffset = Math.Max(val1, this.minAxisGap);
                foreach (Pad pad in this.pads)
                {
                    pad.DrawItems = this.drawItems;
                    pad.Width = this.Width - this.canvasRightOffset - this.canvasLeftOffset;
                }
                graphics1.FillRectangle((Brush) new SolidBrush(this.canvasColor), this.canvasLeftOffset, this.canvasTopOffset, this.Width - this.canvasRightOffset - this.canvasLeftOffset, this.Height - this.canvasBottomOffset - this.canvasLeftOffset);
                if (this.polosaDate != DateTime.MinValue)
                {
                    int num2 = this.ClientX(this.polosaDate);
                    if (num2 > this.canvasLeftOffset && num2 < this.Width - this.canvasRightOffset)
                        graphics1.FillRectangle((Brush) new SolidBrush(this.selectedFillHighlightColor), (float) num2 - (float) this.intervalWidth / 2f, (float) this.canvasTopOffset, (float) this.intervalWidth, (float) (this.Height - this.canvasBottomOffset - this.canvasLeftOffset));
                }
                graphics1.DrawRectangle(new Pen(this.borderColor), this.canvasLeftOffset, this.canvasTopOffset, this.Width - this.canvasRightOffset - this.canvasLeftOffset, this.Height - this.canvasBottomOffset - this.canvasLeftOffset);
                if (this.mainSeries != null && this.mainSeries.Count != 0)
                    this.axisBottom.PaintWithDates(this.mainSeries.GetDateTime(this.firstIndex), this.mainSeries.GetDateTime(this.lastIndex));
                foreach (Pad pad in this.pads)
                    pad.Update(graphics1);
                for (int index = 1; index < this.pads.Count; ++index)
                    graphics1.DrawLine(new Pen(this.splitterColor), this.pads[index].X1, this.pads[index].Y1, this.pads[index].X2, this.pads[index].Y1);
                graphics1.Dispose();
                this.contentUpdated = false;
            }
            if (this.mainSeries != null && this.mainSeries.Count != 0 && (this.actionType == ChartActionType.Cross && this.isMouseOverCanvas) && this.bitmap != null)
            {
                graphics.DrawImage((Image) this.bitmap, 0, 0);
                graphics.SmoothingMode = this.smoothingMode;
                #if GTK
                var point = new Point(0, 0);
                #else
                Point point = this.PointToClient(Cursor.Position);
                #endif
                this.mouseX = point.X;
                this.mouseY = point.Y;
                graphics.DrawLine(new Pen(this.crossColor, 0.5f), this.canvasLeftOffset, this.mouseY, this.mouseX - 10, this.mouseY);
                graphics.DrawLine(new Pen(this.crossColor, 0.5f), this.mouseX + 10, this.mouseY, this.Width - this.canvasRightOffset, this.mouseY);
                graphics.DrawLine(new Pen(this.crossColor, 0.5f), this.mouseX, this.canvasTopOffset, this.mouseX, this.mouseY - 10);
                graphics.DrawLine(new Pen(this.crossColor, 0.5f), this.mouseX, this.mouseY + 10, this.mouseX, this.Height - this.canvasBottomOffset);
                string str1 = this.GetDateTime(this.mouseX).ToString();
                SizeF sizeF1 = graphics.MeasureString(str1, this.Font);
                graphics.FillRectangle((Brush) new SolidBrush(this.dateTipRectangleColor), (float) ((double) this.mouseX - (double) sizeF1.Width / 2.0 - 2.0), (float) (this.Height - this.canvasBottomOffset), sizeF1.Width, sizeF1.Height + 2f);
                graphics.DrawString(str1, this.Font, (Brush) new SolidBrush(this.dateTipTextColor), (float) ((double) this.mouseX - (double) sizeF1.Width / 2.0 - 1.0), (float) (this.Height - this.canvasBottomOffset + 2));
                double num2 = 0.0;
                for (int index = 0; index < this.pads.Count; ++index)
                {
                    Pad pad = this.pads[index];
                    if (pad.Y2 > this.mouseY && pad.Y1 < this.mouseY)
                    {
                        num2 = pad.WorldY(this.mouseY);
                        break;
                    }
                }
                string str2 = num2.ToString("F" + (object) this.labelDigitsCount);
                SizeF sizeF2 = graphics.MeasureString(str2, this.Font);
                graphics.FillRectangle((Brush) new SolidBrush(this.valTipRectangleColor), (float) (this.Width - this.canvasRightOffset), (float) ((double) this.mouseY - (double) sizeF2.Height / 2.0 - 2.0), sizeF2.Width, sizeF2.Height + 2f);
                graphics.DrawString(str2, this.Font, (Brush) new SolidBrush(this.valTipTextColor), (float) (this.Width - this.canvasRightOffset + 2), (float) ((double) this.mouseY - (double) sizeF2.Height / 2.0 - 1.0));
            }
            else
            {
                if (this.bitmap == null)
                    return;
                graphics.DrawImage((Image) this.bitmap, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.isMouseOverCanvas)
                {
                    foreach (Pad pad in this.pads)
                    {
                        if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                        {
                            this.padSplit = true;
                            this.padSplitIndex = this.pads.IndexOf(pad);
                            return;
                        }
                    }
                }
                foreach (Pad pad in this.pads)
                {
                    if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                        pad.MouseDown(e);
                }
            }
            catch
            {
            }
        }

        private void Chart_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.padSplit)
                    this.padSplit = false;
                foreach (Pad pad in this.pads)
                {
                    if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                        pad.MouseUp(e);
                }
                this.Invalidate();
            }
            catch
            {
            }
        }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                this.ZoomIn(e.Delta / 20);
            else
                this.ZoomOut(-e.Delta / 20);
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                this.mouseX = e.X;
                this.mouseY = e.Y;
                if (this.prevMouseX != this.mouseX || this.prevMouseY != this.mouseY)
                {
                    if (e.X > this.canvasLeftOffset && e.X < this.Width - this.canvasRightOffset && (e.Y > this.canvasTopOffset && e.Y < this.Height - this.canvasBottomOffset))
                    {
                        this.isMouseOverCanvas = true;
                        #if GTK
                        #else
                        if (this.actionType == ChartActionType.Cross)
                            this.Cursor = Cursors.Cross;
                        #endif
                    }
                    else
                    {
                        this.isMouseOverCanvas = false;
                        if (this.actionType == ChartActionType.Cross)
                            this.Invalidate();
                        #if GTK
                        #else
                        this.Cursor = Cursors.Default;
                        #endif
                    }
                    if (this.padSplit && this.padSplitIndex != 0)
                    {
                        Pad pad1 = this.pads[this.padSplitIndex];
                        Pad pad2 = this.pads[this.padSplitIndex - 1];
                        int num1 = e.Y;
                        if (pad1.Y2 - e.Y < 20)
                            num1 = pad1.Y2 - 20;
                        if (e.Y - pad2.Y1 < 20)
                            num1 = pad2.Y1 + 20;
                        if (pad1.Y2 - num1 >= 20 && num1 - pad2.Y1 >= 20)
                        {
                            int num2 = pad1.Y2 - num1;
                            int num3 = num1 - pad2.Y1;
                            this.padsHeightArray[this.padSplitIndex] = (object) ((double) num2 / (double) (this.Height - this.canvasTopOffset - this.canvasBottomOffset));
                            this.padsHeightArray[this.padSplitIndex - 1] = (object) ((double) num3 / (double) (this.Height - this.canvasTopOffset - this.canvasBottomOffset));
                            pad1.SetCanvas(pad1.X1, pad1.X2, num1, pad1.Y2);
                            pad2.SetCanvas(pad2.X1, pad2.X2, pad2.Y1, num1);
                        }
                        this.contentUpdated = true;
                        this.Invalidate();
                    }
                    foreach (Pad pad in this.pads)
                    {
                        #if GTK
                        #else
                        if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1 && (this.pads.IndexOf(pad) != 0 && Cursor.Current != Cursors.HSplit))
                            Cursor.Current = Cursors.HSplit;
                        #endif
                    }
                    foreach (Pad pad in this.pads)
                    {
                        if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                            pad.MouseMove(e);
                    }
                    if (this.isMouseOverCanvas && this.actionType == ChartActionType.Cross)
                        this.Invalidate();
                }
                this.prevMouseX = this.mouseX;
                this.prevMouseY = this.mouseY;
            }
            catch
            {
            }
        }

        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            this.isMouseOverCanvas = false;
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetPadSizes();
            this.contentUpdated = true;
            if (this.axisBottom != null)
                this.axisBottom.SetBounds(this.canvasLeftOffset, this.Width - this.canvasRightOffset, this.Height - this.canvasBottomOffset);
            this.Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
        }

        private void ZoomIn(int delta)
        {
            this.SetIndexInterval(Math.Min(this.firstIndex + delta, this.lastIndex - 1 + 1), this.lastIndex);
            this.Invalidate();
        }

        private void ZoomOut(int delta)
        {
            if (this.mainSeries == null || this.mainSeries.Count == 0)
                return;
            this.SetIndexInterval(Math.Max(0, this.firstIndex - delta), this.lastIndex);
            this.Invalidate();
        }

        public void ZoomIn()
        {
            this.ZoomIn((this.lastIndex - this.firstIndex) / 5);
        }

        public void ZoomOut()
        {
            this.ZoomOut((this.lastIndex - this.firstIndex) / 10 + 1);
        }

        public void UnSelectAll()
        {
            foreach (Pad pad in this.pads)
            {
                if (pad.SelectedPrimitive != null)
                {
                    pad.SelectedPrimitive.UnSelect();
                    pad.SelectedPrimitive = (IChartDrawable) null;
                }
            }
        }

        public virtual void ShowProperties(DSView view, Pad pad, bool forceShowProperties)
        {
        }

        public void AddPad()
        {
            lock (this.lockObject)
            {
                this.FillPadsHeightArray();
                this.pads.Add(new Pad(this, this.canvasLeftOffset, this.Width - this.canvasRightOffset, this.canvasTopOffset, this.Height - this.canvasBottomOffset));
                this.SetPadSizes();
                this.contentUpdated = true;
            }
        }

        private void SetPadSizes()
        {
            int y1 = this.canvasTopOffset;
            int num1 = this.Height - this.canvasBottomOffset - this.canvasTopOffset;
            int index = 0;
            double num2 = 0.0;
            foreach (Pad pad in this.pads)
            {
                num2 += (double) this.padsHeightArray[index];
                int y2 = (int) ((double) this.canvasTopOffset + (double) num1 * num2);
                pad.SetCanvas(this.canvasLeftOffset, this.Width - this.canvasRightOffset, y1, y2);
                ++index;
                y1 = y2;
            }
        }

        private void FillPadsHeightArray()
        {
            if (this.padsHeightArray.Count == 0)
            {
                this.padsHeightArray.Add((object) 1.0);
            }
            else
            {
                this.padsHeightArray.Add((object) 0);
                int count = this.padsHeightArray.Count;
                if (this.volumePadShown)
                    --count;
                this.padsHeightArray[0] = (object) (3.0 / (double) (count + 2));
                for (int index = 1; index < this.padsHeightArray.Count; ++index)
                {
                    if (this.volumePadShown && index == 1)
                    {
                        this.padsHeightArray[1] = (object) ((double) this.padsHeightArray[0] / 6.0);
                        this.padsHeightArray[0] = (object) ((double) this.padsHeightArray[1] * 5.0);
                    }
                    else
                        this.padsHeightArray[index] = (object) (1.0 / (double) (count + 2));
                }
            }
        }

        public void ShowVolumePad()
        {
        }

        public void HideVolumePad()
        {
        }

        public int ClientX(DateTime dateTime)
        {
            double num = (double) (this.Width - this.canvasLeftOffset - this.canvasRightOffset) / (double) (this.lastIndex - this.firstIndex + 1);
            return this.canvasLeftOffset + (int) ((double) (this.mainSeries.GetIndex(dateTime, IndexOption.Null) - this.firstIndex) * num + num / 2.0);
        }

        public DateTime GetDateTime(int x)
        {
            double num = (double) (this.Width - this.canvasLeftOffset - this.canvasRightOffset) / (double) (this.lastIndex - this.firstIndex + 1);
            return this.mainSeries.GetDateTime((int) Math.Floor((double) (x - this.canvasLeftOffset) / num) + this.firstIndex);
        }

        public void Reset()
        {
            lock (this.lockObject)
            {
                foreach (Pad item_1 in this.pads)
                {
                    item_1.Reset();
                    foreach (object item_0 in item_1.Primitives)
                    {
                        if (item_0 is IUpdatable)
                            (item_0 as IUpdatable).Updated -= new EventHandler(this.primitive_Updated);
                    }
                }
                this.pads.Clear();
                this.padsHeightArray.Clear();
                this.volumePadShown = false;
                this.AddPad();
                this.firstIndex = -1;
                this.lastIndex = -1;
                this.mainSeries = (ISeries) null;
                this.polosaDate = DateTime.MinValue;
                this.contentUpdated = true;
                if (this.updateStyle == ChartUpdateStyle.Fixed)
                    this.UpdateStyle = ChartUpdateStyle.Trailing;
                this.BarSeriesStyle = BSStyle.Candle;
            }
        }

        public void SetMainSeries(ISeries mainSeries)
        {
            this.SetMainSeries(mainSeries, false, Color.Black);
        }

        public void SetMainSeries(ISeries mainSeries, bool showVolumePad, Color color)
        {
            lock (this.lockObject)
            {
                ISeries temp_5 = this.mainSeries;
                this.series = mainSeries;
                if (mainSeries is BarSeries)
                {
                    this.SetBarSeriesStyle(this.barSeriesStyle, true);
                }
                else
                {
                    this.mainSeries = this.series;
                    this.mainSeriesView = (SeriesView) new DSView(this.pads[0], mainSeries as TimeSeries, color, SearchOption.ExactFirst, SmoothingMode.HighSpeed);
                    this.pads[0].AddPrimitive((IChartDrawable) this.mainSeriesView);
                }
                this.pads[0].ScaleStyle = this.scaleStyle;
                if (showVolumePad)
                    this.ShowVolumePad();
                this.firstIndex = this.updateStyle != ChartUpdateStyle.WholeRange ? Math.Max(0, mainSeries.Count - this.minCountOfBars) : 0;
                this.lastIndex = mainSeries.Count - 1;
                if (mainSeries.Count == 0)
                    this.firstIndex = -1;
                if (this.lastIndex >= 0)
                    this.SetIndexInterval(this.firstIndex, this.lastIndex);
                this.contentUpdated = true;
                this.Invalidate();
            }
        }

        private void SetIndexInterval(int firstIndex, int lastIndex)
        {
            if (this.mainSeries == null || firstIndex < 0 || lastIndex > this.mainSeries.Count - 1)
                return;
            this.firstIndex = firstIndex;
            this.lastIndex = lastIndex;
            this.leftDateTime = firstIndex >= 0 ? this.mainSeries.GetDateTime(this.firstIndex) : DateTime.MaxValue;
            this.rightDateTime = lastIndex < 0 || lastIndex > this.mainSeries.Count - 1 ? DateTime.MinValue : this.mainSeries.GetDateTime(this.lastIndex);
            foreach (Pad pad in this.pads)
                pad.SetInterval(this.leftDateTime, this.rightDateTime);
            this.contentUpdated = true;
        }

        private void SetDateInterval(DateTime firstDateTime, DateTime lastDateTime)
        {
            this.SetIndexInterval(this.MainSeries.GetIndex(firstDateTime, IndexOption.Next), this.MainSeries.GetIndex(lastDateTime, IndexOption.Prev));
        }

        #if !GTK
        private void OnScrollBarScroll(object sender, ScrollEventArgs e)
        {
            if (this.scrollBar.Value == e.NewValue)
                return;
            int num = e.NewValue - this.scrollBar.Value;
            this.SetIndexInterval(this.firstIndex + num, this.lastIndex + num);
            this.Invalidate();
        }
        #endif

        public void OnItemAdedd(DateTime dateTime)
        {
            bool flag = false;
            lock (this.lockObject)
            {
                this.contentUpdated = true;
                if (this.firstIndex == -1)
                    this.firstIndex = 0;
                switch (this.updateStyle)
                {
                    case ChartUpdateStyle.WholeRange:
                        try
                        {
                            this.SetIndexInterval(0, this.mainSeries.Count - 1);
                            flag = true;
                            break;
                        }
                        catch (Exception exception_0)
                        {
                            break;
                        }
                    case ChartUpdateStyle.Trailing:
                        if (this.lastIndex - this.firstIndex + 1 < this.minCountOfBars)
                            this.SetIndexInterval(this.firstIndex, this.lastIndex + 1);
                        else
                            this.SetIndexInterval(this.firstIndex + 1, this.lastIndex + 1);
                        flag = true;
                        break;
                }
            }
            if (flag)
                this.Invalidate();
            #if !GTK
            Application.DoEvents();
            #endif
        }

        private void mainSeries_Cleared(object sender, EventArgs e)
        {
            this.firstIndex = -1;
            this.lastIndex = -1;
        }

        private bool SetBarSeriesStyle(BSStyle barSeriesStyle, bool force)
        {
            bool flag = true;
            if (barSeriesStyle == BSStyle.Candle || barSeriesStyle == BSStyle.Bar || barSeriesStyle == BSStyle.Line)
            {
                if (!(this.mainSeriesView is SimpleBSView) || force)
                {
                    this.pads[0].RemovePrimitive((IChartDrawable) this.mainSeriesView);
                    this.mainSeriesView = (SeriesView) new SimpleBSView(this.pads[0], this.series as BarSeries);
                    (this.mainSeriesView as SimpleBSView).UpColor = this.candleUpColor;
                    (this.mainSeriesView as SimpleBSView).DownColor = this.candleDownColor;
                    this.mainSeries = this.mainSeriesView.MainSeries;
                    this.pads[0].AddPrimitive((IChartDrawable) this.mainSeriesView);
                }
                else
                    flag = false;
                if (barSeriesStyle == BSStyle.Candle)
                    (this.mainSeriesView as SimpleBSView).Style = SimpleBSStyle.Candle;
                if (barSeriesStyle == BSStyle.Bar)
                    (this.mainSeriesView as SimpleBSView).Style = SimpleBSStyle.Bar;
                if (barSeriesStyle == BSStyle.Line)
                    (this.mainSeriesView as SimpleBSView).Style = SimpleBSStyle.Line;
            }
            return flag;
        }

        private void EmitUpdateStyleChanged()
        {
            if (this.UpdateStyleChanged == null)
                return;
            this.UpdateStyleChanged((object) this, EventArgs.Empty);
        }

        private void EmitVolumeVisibleChanged()
        {
            if (this.VolumeVisibleChanged == null)
                return;
            this.VolumeVisibleChanged((object) this, EventArgs.Empty);
        }

        private void EmitBarSeriesStyleChanged()
        {
            if (this.BarSeriesStyleChanged == null)
                return;
            this.BarSeriesStyleChanged((object) this, EventArgs.Empty);
        }

        private void EmitActionTypeChanged()
        {
            if (this.ActionTypeChanged == null)
                return;
            this.ActionTypeChanged((object) this, EventArgs.Empty);
        }

        private void EmitScaleStyleChanged()
        {
            if (this.ScaleStyleChanged == null)
                return;
            this.ScaleStyleChanged((object) this, EventArgs.Empty);
        }

        public void EnsureVisible(Fill fill)
        {
            if (fill.DateTime < this.mainSeries.FirstDateTime)
                return;
            int num = Math.Max(this.mainSeries.GetIndex(fill.DateTime, IndexOption.Prev), 0);
            int val2 = this.lastIndex - this.firstIndex + 1;
            int lastIndex = Math.Max(Math.Min(this.mainSeries.Count - 1, num + val2 / 5), val2);
            this.SetIndexInterval(lastIndex - val2 + 1, lastIndex);
            this.pads[0].SetSelectedObject((object) fill);
            this.polosaDate = this.mainSeries.GetDateTime(this.mainSeries.GetIndex(fill.DateTime, IndexOption.Prev));
            this.contentUpdated = true;
            this.Invalidate();
        }

        public int GetPadNumber(Point point)
        {
            int y = point.Y;
            for (int index = 0; index < this.pads.Count; ++index)
            {
                if (this.pads[index].Y1 <= y && this.pads[index].Y2 >= y)
                    return index;
            }
            return -1;
        }

        private delegate void SetIndexIntervalHandler(int firstIndex, int lastIndex);
    }
}
