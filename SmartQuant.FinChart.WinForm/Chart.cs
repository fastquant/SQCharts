// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;
using SmoothingMode = System.Drawing.Drawing2D.SmoothingMode;
using PrintPageEventArgs = System.Drawing.Printing.PrintPageEventArgs;
using System.ComponentModel;
using SmartQuant.FinChart.Objects;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
using Xwt;

#else
using Compatibility.WinForm;
using System.Drawing;
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public partial class Chart : UserControl
    {
        private IComponent components;
        protected SmoothingMode TimeSeriesSmoothingMode = SmoothingMode.HighSpeed;
        protected PadList pads = new PadList();
        protected int minCountOfBars = 125;
        protected int canvasLeftOffset = 20;
        protected int canvasTopOffset = 20;
        protected int canvasRightOffset = 20;
        protected int canvasBottomOffset = 30;
        protected Color canvasColor = Colors.MidnightBlue;
        protected ArrayList padsHeightArray = new ArrayList();
        protected int padSplitIndex = -1;
        protected bool contentUpdated = true;
        protected ChartUpdateStyle updateStyle = ChartUpdateStyle.Trailing;
        protected int minAxisGap = 50;

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
                this.chartBackColor = value;
                this.contentUpdated = true;
            }
        }

        //        public ChartActionType ActionType
        //        {
        //            get
        //            {
        //                return this.actionType;
        //            }
        //            set
        //            {
        //                if (this.actionType == value)
        //                    return;
        //                this.actionType = value;
        //                this.EmitActionTypeChanged();
        //                this.Invalidate();
        //            }
        //        }

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
                this.selectedFillHighlightColor = ColorUtils.FromArgb(100, value);
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


        public void ZoomIn()
        {
            throw new NotImplementedException();
        }

        public void ZoomOut()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            throw new NotImplementedException();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
        }

        protected override void OnResize(EventArgs e)
        {
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
        }

        public void UnSelectAll()
        {
//            foreach (Pad pad in this.pads)
//            {
//                if (pad.SelectedPrimitive != null)
//                {
//                    pad.SelectedPrimitive.UnSelect();
//                    pad.SelectedPrimitive = null;
//                }
//            }
        }

        public virtual void ShowProperties(DSView view, Pad pad, bool forceShowProperties)
        {
        }

        public void AddPad()
        {
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

        public void SetMainSeries(ISeries mainSeries)
        {
            this.SetMainSeries(mainSeries, false, Colors.Black);
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
            throw new NotImplementedException();

        }

        void EmitUpdateStyleChanged()
        {
            if (UpdateStyleChanged != null)
                UpdateStyleChanged(this, EventArgs.Empty);
        }
    }
}

