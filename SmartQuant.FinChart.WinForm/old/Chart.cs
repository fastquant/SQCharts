// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
using SmartQuant.FinChart.Objects;
using System.Drawing.Drawing2D;


#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

using System.Drawing;

namespace SmartQuant.FinChart
{
    public partial class Chart : UserControl
    {
        private IContainer components;
        private object lockObject = new object();

        protected SmoothingMode TimeSeriesSmoothingMode;
        protected PadList pads;
        protected int minCountOfBars;
        protected int canvasLeftOffset;
        protected int canvasTopOffset;
        protected int canvasRightOffset;
        protected int canvasBottomOffset;
        protected Color canvasColor;
        protected Color sessionGridColor;
        protected ArrayList padsHeightArray;
        protected int padSplitIndex;
        protected bool contentUpdated;
        protected ChartUpdateStyle updateStyle;
        protected int minAxisGap;
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

        private Color selectedItemTextColor;
        private Color itemTextColor;
        private Color chartBackColor;
        private Color selectedFillHighlightColor;
        private Color rightAxisGridColor;
        private Color rightAxisTextColor;
        private Color rightAxisMinorTicksColor;
        private Color rightAxisMajorTicksColor;
        private Color dateTipRectangleColor;
        private Color dateTipTextColor;
        private Color valTipRectangleColor;
        private Color valTipTextColor;
        private Color crossColor;
        private Color borderColor;
        private Color splitterColor;

        public ChartActionType ActionType { get; set; }

        public bool ContextMenuEnabled { get; set; }

        public int RightAxesFontSize { get; set; }

        public int LabelDigitsCount { get; set; }

        public Image PrimitiveDeleteImage { get; set; }

        public Image PrimitivePropertiesImage { get; set; }

        [Browsable(false)]
        public bool DrawItems { get; set; }

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
                this.barSeriesStyle = value;
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
                return Pads.Count;
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
                this.chartBackColor = value;
                this.contentUpdated = true;
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
                this.axisBottom.GridColor = value;
                this.contentUpdated = true;
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
                this.axisBottom.LabelColor = value;
                this.contentUpdated = true;
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
                foreach (Pad pad in this.pads)
                    pad.Axis.GridColor = value;
                this.rightAxisGridColor = value;
                this.contentUpdated = true;
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

                foreach (Pad pad in this.pads)
                    pad.Axis.LabelColor = value;
                this.rightAxisTextColor = value;
                this.contentUpdated = true;
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

                foreach (Pad pad in this.pads)
                    pad.Axis.MinorTicksColor = value;
                this.rightAxisMinorTicksColor = value;
                this.contentUpdated = true;
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
                foreach (Pad pad in this.pads)
                    pad.Axis.MajorTicksColor = value;
                this.rightAxisMajorTicksColor = value;
                this.contentUpdated = true;
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
                this.dateTipRectangleColor = value;
                this.contentUpdated = true;
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
                this.dateTipTextColor = value;
                this.contentUpdated = true;
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
                this.valTipRectangleColor = value;
                this.contentUpdated = true;
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
                this.valTipTextColor = value;
                this.contentUpdated = true;
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
                this.crossColor = value;
                this.contentUpdated = true;
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
                this.borderColor = value;
                this.contentUpdated = true;
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
                this.splitterColor = value;
                this.contentUpdated = true;
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
//                this.EmitScaleStyleChanged();
            }
        }

        public event EventHandler UpdateStyleChanged;

        public event EventHandler VolumeVisibleChanged;

        public event EventHandler ActionTypeChanged;

        public event EventHandler BarSeriesStyleChanged;

        public event EventHandler ScaleStyleChanged;

        public Chart()
        {
            this.components = new System.ComponentModel.Container();
            InitializeComponent();
            #if XWT || GTK           
            #else
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            #endif
            this.pads = new PadList();
            this.canvasColor = Color.MidnightBlue;
            this.padsHeightArray = new ArrayList();

            this.canvasLeftOffset = 40; // 10
            this.canvasTopOffset = 40; // 10
            this.canvasRightOffset = 40;
            this.canvasBottomOffset = 40;

            this.chartBackColor = Color.MidnightBlue;
            bool contentUpdated = true;
            // Should have a default pad.
            AddPad();
            this.axisBottom = new AxisBottom(this, this.canvasLeftOffset, this.Width - this.canvasRightOffset, this.Height - this.canvasTopOffset);
            this.mouseX = this.mouseY = -1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        public void ZoomIn()
        {
            this.ZoomIn((LastIndex - FirstIndex) / 5);
        }

        public void ZoomOut()
        {
            throw new NotImplementedException();
        }

        private void ZoomIn(int delta)
        {
            this.Invalidate();
        }

        private void ZoomOut(int delta)
        {
            this.Invalidate();
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color)
        {
            DrawSeries(series, padNumber, color, SearchOption.ExactFirst);
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SearchOption option)
        {
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style)
        {
            DrawSeries(series, padNumber, color, style, SearchOption.ExactFirst, this.TimeSeriesSmoothingMode);
        }

        public void DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style, SmoothingMode smoothingMode)
        {
            DrawSeries(series, padNumber, color, style, SearchOption.ExactFirst, smoothingMode);
        }

        public DSView DrawSeries(TimeSeries series, int padNumber, Color color, SimpleDSStyle style, SearchOption option, SmoothingMode smoothingMode)
        {
            throw new NotImplementedException();
        }

        public void DrawFill(Fill fill, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(DrawingLine line, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(DrawingEllipse circle, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(DrawingRectangle rect, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void DrawPath(SmartQuant.FinChart.Objects.DrawingPath path, int padNumber)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(SmartQuant.FinChart.Objects.DrawingImage image, int padNumber)
        {
            throw new NotImplementedException();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.ChartMouseDown(this, e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.ChartMouseUp(this, e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.ChartMouseLeave(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var prevMouseX = this.mouseX;
            var prevMouseY = this.mouseY;
            this.mouseX = e.X;
            this.mouseY = e.Y;
//            if (this.prevMouseX != this.mouseX || this.prevMouseY != this.mouseY)
//            {
//                if (e.X > this.canvasLeftOffset && e.X < this.Width - this.canvasRightOffset && (e.Y > this.canvasTopOffset && e.Y < this.Height - this.canvasBottomOffset))
//                {
//                    this.isMouseOverCanvas = true;
//                    if (this.actionType == ChartActionType.Cross)
//                        this.Cursor = Cursors.Cross;
//                }
//                else
//                {
//                    this.isMouseOverCanvas = false;
//                    if (this.actionType == ChartActionType.Cross)
//                        this.Invalidate();
//                    this.Cursor = Cursors.Default;
//                }
//
//                if (this.padSplit && this.padSplitIndex != 0)
//                {
//                    Pad pad1 = this.pads[this.padSplitIndex];
//                    Pad pad2 = this.pads[this.padSplitIndex - 1];
//                    int num1 = e.Y;
//                    if (pad1.Y2 - e.Y < 20)
//                        num1 = pad1.Y2 - 20;
//                    if (e.Y - pad2.Y1 < 20)
//                        num1 = pad2.Y1 + 20;
//                    if (pad1.Y2 - num1 >= 20 && num1 - pad2.Y1 >= 20)
//                    {
//                        int num2 = pad1.Y2 - num1;
//                        int num3 = num1 - pad2.Y1;
//                        this.padsHeightArray[this.padSplitIndex] = (object) ((double) num2 / (double) (this.Height - this.canvasTopOffset - this.canvasBottomOffset));
//                        this.padsHeightArray[this.padSplitIndex - 1] = (object) ((double) num3 / (double) (this.Height - this.canvasTopOffset - this.canvasBottomOffset));
//                        pad1.SetCanvas(pad1.X1, pad1.X2, num1, pad1.Y2);
//                        pad2.SetCanvas(pad2.X1, pad2.X2, pad2.Y1, num1);
//                    }
//                    this.contentUpdated = true;
//                    this.Invalidate();
//                }
//                foreach (Pad pad in this.pads)
//                {
//                    if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1 && (this.pads.IndexOf(pad) != 0 && Cursor.Current != Cursors.HSplit))
//                        Cursor.Current = Cursors.HSplit;
//                }
//
//                foreach (Pad pad in Pads)
//                    if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
//                        pad.MouseMove(e);
//                if (this.isMouseOverCanvas && this.actionType == ChartActionType.Cross)
//                    this.Invalidate();
//            }
//            this.prevMouseX = this.mouseX;
//            this.prevMouseY = this.mouseY;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.ChartMouseWheel(this, e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        //        protected override void OnKeyPress(KeyPressEventArgs e)
        //        {
        //        }
        //

        #region Mouse Handler

        private void ChartMouseLeave(object sender, EventArgs e)
        {
        }

        private void ChartMouseDown(object sender, MouseEventArgs e)
        {

            if (this.isMouseOverCanvas)
            {
                foreach (Pad pad in Pads)
                {
                    if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                    {
                        this.padSplit = true;
                        this.padSplitIndex = this.pads.IndexOf(pad);
                        return;
                    }
                }
            }
            foreach (Pad pad in Pads)
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseDown(e);
        }

        private void ChartMouseUp(object sender, MouseEventArgs e)
        {
            if (this.padSplit)
                this.padSplit = false;
            foreach (Pad pad in Pads)
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseUp(e);
            this.Invalidate();
        }

        private void ChartMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                this.ZoomIn(e.Delta / 20);
            else
                this.ZoomOut(-e.Delta / 20);
            this.Invalidate();
        }

        #endregion

        public void UnSelectAll()
        {
            foreach (Pad pad in Pads)
            {
            }
        }

        public virtual void ShowProperties(DSView view, Pad pad, bool forceShowProperties)
        {
        }

        public void AddPad()
        {
            this.pads.Add(new Pad(this, this.canvasLeftOffset, Width - this.canvasRightOffset, this.canvasTopOffset, Height - this.canvasBottomOffset));
        }

        public void ShowVolumePad()
        {
        }

        public void HideVolumePad()
        {
        }

        public int ClientX(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int x)
        {
            throw new NotImplementedException();

        }

        public void Reset()
        {
        }

        private void EmitUpdateStyleChanged()
        {
            if (UpdateStyleChanged != null)
                UpdateStyleChanged(this, EventArgs.Empty);
        }

        private void EmitVolumeVisibleChanged()
        {
            if (VolumeVisibleChanged != null)
                VolumeVisibleChanged(this, EventArgs.Empty);
        }

        private void EmitBarSeriesStyleChanged()
        {
            if (BarSeriesStyleChanged != null)
                BarSeriesStyleChanged(this, EventArgs.Empty);
        }

        private void EmitActionTypeChanged()
        {
            if (ActionTypeChanged != null)
                ActionTypeChanged(this, EventArgs.Empty);
        }

        private void EmitScaleStyleChanged()
        {
            if (ScaleStyleChanged != null)
                ScaleStyleChanged(this, EventArgs.Empty);
        }

        public void SetMainSeries(ISeries mainSeries)
        {
            SetMainSeries(mainSeries, false, Color.Black);
        }

        public void SetMainSeries(ISeries mainSeries, bool showVolumePad, Color color)
        {
        }

        public void OnItemAdedd(DateTime dateTime)
        {
        }

        public void EnsureVisible(Fill fill)
        {
        }

        public int GetPadNumber(Point point)
        {
            int y = (int)point.Y;
            for (int i = 0; i < Pads.Count; ++i)
                if (Pads[i].Y1 <= y && y <= Pads[i].Y2)
                    return i;
            return -1;    
        }

        private void Update(Graphics graphics)
        {
            graphics.Clear(ChartBackColor);
            foreach (Pad pad in Pads)
            {
            }

            // Draw background
            graphics.FillRectangle(new SolidBrush(CanvasColor), this.canvasLeftOffset, this.canvasTopOffset, this.Width - this.canvasRightOffset - this.canvasLeftOffset, this.Height - this.canvasBottomOffset - this.canvasLeftOffset);
        
            // Draw pads
            foreach (Pad pad in Pads)
                pad.Update(graphics);
        }
    }
}
