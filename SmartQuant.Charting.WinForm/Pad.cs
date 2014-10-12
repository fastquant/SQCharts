// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;
using MouseButtons = System.Windows.Forms.MouseButtons;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using PointF = System.Drawing.PointF;
#if XWT
using Xwt;
using Xwt.Drawing;
#else
using System.Drawing;
#endif
namespace SmartQuant.Charting
{
    public class Pad
    {
        [Browsable(false)]
        public bool Grid3D;
        protected int fX1;
        protected int fX2;
        protected int fY1;
        protected int fY2;
        protected double fCanvasX1;
        protected double fCanvasX2;
        protected double fCanvasY1;
        protected double fCanvasY2;
        protected int fClientX;
        protected int fClientY;
        protected int fClientWidth;
        protected int fClientHeight;
        protected double fXMin;
        protected double fXMax;
        protected double fYMin;
        protected double fYMax;
        protected int fMarginLeft;
        protected int fMarginRight;
        protected int fMarginTop;
        protected int fMarginBottom;
        protected int fWidth;
        protected int fHeight;
        [NonSerialized]
        protected Chart fChart;
        [NonSerialized]
        protected Graphics fGraphics;
        protected ArrayList fPrimitives;
        protected Color fBackColor;
        protected Color fForeColor;
        protected string fName;
        protected TTitle fTitle;
        protected bool fTitleEnabled;
        protected int fTitleOffsetX;
        protected int fTitleOffsetY;
        protected Axis fAxisLeft;
        protected Axis fAxisRight;
        protected Axis fAxisTop;
        protected Axis fAxisBottom;
        protected TLegend fLegend;
        protected bool fLegendEnabled;
        protected ELegendPosition fLegendPosition;
        protected int fLegendOffsetX;
        protected int fLegendOffsetY;
        protected bool fBorderEnabled;
        protected Color fBorderColor;
        protected int fBorderWidth;
        protected IDrawable fSelectedPrimitive;
        protected TDistance fSelectedPrimitiveDistance;
        protected bool fOnAxis;
        protected bool fOnPrimitive;
        protected bool fMouseDown;
        protected int fMouseDownX;
        protected int fMouseDownY;
        [NonSerialized]
        protected MouseButtons fMouseDownButton;
        protected bool fOutlineEnabled;
        protected Rectangle fOutlineRectangle;
        protected bool fMouseZoomEnabled;
        protected bool fMouseZoomXAxisEnabled;
        protected bool fMouseZoomYAxisEnabled;
        protected bool fMouseUnzoomEnabled;
        protected bool fMouseUnzoomXAxisEnabled;
        protected bool fMouseUnzoomYAxisEnabled;
        protected bool fMouseMoveContentEnabled;
        protected bool fMouseMovePrimitiveEnabled;
        protected bool fMouseDeletePrimitiveEnabled;
        protected bool fMousePadPropertiesEnabled;
        protected bool fMousePrimitivePropertiesEnabled;
        protected bool fMouseContextMenuEnabled;
        protected bool fMouseWheelEnabled;
        protected double fMouseWheelSensitivity;
        protected EMouseWheelMode fMouseWheelMode;
        protected int fWindowSize;
        protected bool fMonitored;
        protected bool fUpdating;
        protected int fLastTickTime;
        protected int fUpdateInterval;
        protected DateTime fLastUpdateDateTime;
        protected ETransformationType fTransformationType;
        protected IChartTransformation fTransformation;
        protected Color fSessionGridColor;

//        [Browsable(false)]
//        public bool For3D
//        {
//            get
//            {
//                return this.Features3D.Active;
//            }
//            set
//            {
//                this.Features3D.Active = value;
//            }
//        }
//
//        [Browsable(false)]
//        public object View3D
//        {
//            get
//            {
//                return this.Features3D.View;
//            }
//            set
//            {
//                this.Features3D.View = value;
//            }
//        }
//
//        [Browsable(false)]
//        public Axis[] Axes3D
//        {
//            get
//            {
//                return this.Features3D.Axes;
//            }
//        }
//
//        [Browsable(false)]
//        public Axis AxisX3D
//        {
//            get
//            {
//                return this.Features3D.Axes[0];
//            }
//        }
//
//        [Browsable(false)]
//        public Axis AxisY3D
//        {
//            get
//            {
//                return this.Features3D.Axes[1];
//            }
//        }
//
//        [Browsable(false)]
//        public Axis AxisZ3D
//        {
//            get
//            {
//                return this.Features3D.Axes[2];
//            }
//        }

        [Browsable(false)]
        public Chart Chart
        {
            get
            {
                return this.fChart;
            }
            set
            {
                this.fChart = value;
            }
        }

        [Description("Enable or disable double buffering")]
        [Category("Appearance")]
        public bool DoubleBufferingEnabled
        {
            get
            {
                return Chart.DoubleBufferingEnabled;
            }
            set
            {
                Chart.DoubleBufferingEnabled = value;
            }
        }

        [Description("Enable or disable smoothing")]
        [Category("Appearance")]
        public bool SmoothingEnabled
        {
            get
            {
                return Chart.SmoothingEnabled;
            }
            set
            {
                Chart.SmoothingEnabled = value;
            }
        }

        [Description("Enable or disable antialiasing")]
        [Category("Appearance")]
        public bool AntiAliasingEnabled
        {
            get
            {
                return Chart.AntiAliasingEnabled;
            }
            set
            {
                Chart.AntiAliasingEnabled = value;
            }
        }

        [Category("Position")]
        [Description("")]
        public double CanvasX1
        {
            get
            {
                return this.fCanvasX1;
            }
            set
            {
                this.fCanvasX1 = value;
            }
        }

        [Category("Position")]
        [Description("")]
        public double CanvasX2
        {
            get
            {
                return this.fCanvasX2;
            }
            set
            {
                this.fCanvasX2 = value;
            }
        }

        [Description("")]
        [Category("Position")]
        public double CanvasY1
        {
            get
            {
                return this.fCanvasY1;
            }
            set
            {
                this.fCanvasY1 = value;
            }
        }

        [Category("Position")]
        [Description("")]
        public double CanvasY2
        {
            get
            {
                return this.fCanvasY2;
            }
            set
            {
                this.fCanvasY2 = value;
            }
        }

        [Browsable(false)]
        public double CanvasWidth
        {
            get
            {
                return Math.Abs(CanvasX2 - CanvasX1);
            }
        }

        [Browsable(false)]
        public double CanvasHeight
        {
            get
            {
                return Math.Abs(CanvasY2 - CanvasY1);
            }
        }

        [Browsable(false)]
        public virtual int X1
        {
            get
            {
                return this.fX1;
            }
            set
            {
                this.fX1 = value;
                this.fWidth = this.fX2 - this.fX1;
            }
        }

        [Browsable(false)]
        public virtual int X2
        {
            get
            {
                return this.fX2;
            }
            set
            {
                this.fX2 = value;
                this.fWidth = this.fX2 - this.fX1;
            }
        }

        [Browsable(false)]
        public int Y1
        {
            get
            {
                return this.fY1;
            }
            set
            {
                this.fY1 = value;
                this.fHeight = this.fY2 - this.fY1;
            }
        }

        [Browsable(false)]
        public int Y2
        {
            get
            {
                return this.fY2;
            }
            set
            {
                this.fY2 = value;
                this.fHeight = this.fY2 - this.fY1;
            }
        }

        [Browsable(false)]
        public int Width
        {
            get
            {
                return this.fWidth;
            }
            set
            {
                this.fWidth = value;
                this.fX2 = this.fX1 + this.fWidth;
            }
        }

        [Browsable(false)]
        public int Height
        {
            get
            {
                return this.fHeight;
            }
            set
            {
                this.fHeight = value;
                this.fY2 = this.fY1 + this.fHeight;
            }
        }

        [Browsable(false)]
        public double XMin
        {
            get
            {
                return this.fAxisBottom.Enabled && this.fAxisBottom.Zoomed ? this.fAxisBottom.Min : this.fXMin;
            }
            set
            {
                this.fXMin = value;
            }
        }

        [Browsable(false)]
        public double XMax
        {
            get
            {
                return this.fAxisBottom.Enabled && this.fAxisBottom.Zoomed ?  this.fAxisBottom.Max : this.fXMax; 
            }
            set
            {
                this.fXMax = value;
            }
        }

        [Browsable(false)]
        public double YMin
        {
            get
            {
                return this.fAxisLeft.Enabled && this.fAxisLeft.Zoomed ? this.fAxisLeft.Min :  this.fYMin;
            }
            set
            {
                this.fYMin = value;
            }
        }

        [Browsable(false)]
        public double YMax
        {
            get
            {
                return this.fAxisLeft.Enabled && this.fAxisLeft.Zoomed ? this.fAxisLeft.Max : this.fYMax;
            }
            set
            {
                this.fYMax = value;
            }
        }

        [Browsable(false)]
        public double XRangeMin
        {
            get
            {
                return this.fXMin;
            }
            set
            {
                this.fXMin = value;
            }
        }

        [Browsable(false)]
        public double XRangeMax
        {
            get
            {
                return this.fXMax;
            }
            set
            {
                this.fXMax = value;
            }
        }

        [Browsable(false)]
        public double YRangeMin
        {
            get
            {
                return this.fYMin;
            }
            set
            {
                this.fYMin = value;
            }
        }

        [Browsable(false)]
        public double YRangeMax
        {
            get
            {
                return this.fYMax;
            }
            set
            {
                this.fYMax = value;
            }
        }

        [Category("Margin")]
        [Description("")]
        public int MarginLeft
        {
            get
            {
                return this.fMarginLeft;
            }
            set
            {
                this.fMarginLeft = value;
            }
        }

        [Category("Margin")]
        [Description("")]
        public int MarginRight
        {
            get
            {
                return this.fMarginRight;
            }
            set
            {
                this.fMarginRight = value;
            }
        }

        [Category("Margin")]
        [Description("")]
        public int MarginTop
        {
            get
            {
                return this.fMarginTop;
            }
            set
            {
                this.fMarginTop = value;
            }
        }

        [Category("Margin")]
        [Description("")]
        public int MarginBottom
        {
            get
            {
                return this.fMarginBottom;
            }
            set
            {
                this.fMarginBottom = value;
            }
        }

        public string Name
        {
            get
            {
                return this.fName;
            }
            set
            {
                this.fName = value;
            }
        }

        [Browsable(false)]
        public TTitle Title
        {
            get
            {
                return this.fTitle;
            }
            set
            {
                this.fTitle = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public bool TitleEnabled
        {
            get
            {
                return this.fTitleEnabled;
            }
            set
            {
                this.fTitleEnabled = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public ArrayList TitleItems
        {
            get
            {
                return this.fTitle.Items;
            }
        }

        [Description("")]
        [Category("Title")]
        public bool TitleItemsEnabled
        {
            get
            {
                return this.fTitle.ItemsEnabled;
            }
            set
            {
                this.fTitle.ItemsEnabled = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public string TitleText
        {
            get
            {
                return this.fTitle.Text;
            }
            set
            {
                this.fTitle.Text = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public Font TitleFont
        {
            get
            {
                return this.fTitle.Font;
            }
            set
            {
                this.fTitle.Font = value;
            }
        }

        [Category("Title")]
        [Description("")]
        public Color TitleColor
        {
            get
            {
                return this.fTitle.Color;
            }
            set
            {
                this.fTitle.Color = value;
            }
        }

        [Description("Title offset alone X axis")]
        [Category("Title")]
        public int TitleOffsetX
        {
            get
            {
                return this.fTitleOffsetX;
            }
            set
            {
                this.fTitleOffsetX = value;
            }
        }

        [Category("Title")]
        [Description("Title offset alone Y axis")]
        public int TitleOffsetY
        {
            get
            {
                return this.fTitleOffsetY;
            }
            set
            {
                this.fTitleOffsetY = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public ETitlePosition TitlePosition
        {
            get
            {
                return this.fTitle.Position;
            }
            set
            {
                this.fTitle.Position = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public ETitleStrategy TitleStrategy
        {
            get
            {
                return this.fTitle.Strategy;
            }
            set
            {
                this.fTitle.Strategy = value;
            }
        }

        [Category("Color")]
        [Description("")]
        public Color BackColor
        {
            get
            {
                return this.fBackColor;
            }
            set
            {
                this.fBackColor = value;
            }
        }

        [Description("")]
        [Category("Color")]
        public Color ForeColor
        {
            get
            {
                return this.fForeColor;
            }
            set
            {
                this.fForeColor = value;
            }
        }

        [Browsable(false)]
        public ArrayList Primitives
        {
            get
            {
                return this.fPrimitives;
            }
            set
            {
                this.fPrimitives = value;
            }
        }

        [Browsable(false)]
        public Graphics Graphics
        {
            get
            {
                return this.fGraphics;
            }
            set
            {
                this.fGraphics = value;
            }
        }

        [Browsable(false)]
        public Axis AxisLeft
        {
            get
            {
                return this.fAxisLeft;
            }
        }

        [Browsable(false)]
        public Axis AxisRight
        {
            get
            {
                return this.fAxisRight;
            }
        }

        [Browsable(false)]
        public Axis AxisTop
        {
            get
            {
                return this.fAxisTop;
            }
        }

        [Browsable(false)]
        public Axis AxisBottom
        {
            get
            {
                return this.fAxisBottom;
            }
        }

        [Description("")]
        [Category("Grid")]
        public bool XGridEnabled
        {
            get
            {
                return this.fAxisLeft.GridEnabled;
            }
            set
            {
                this.fAxisLeft.GridEnabled = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public bool YGridEnabled
        {
            get
            {
                return this.fAxisBottom.GridEnabled;
            }
            set
            {
                this.fAxisBottom.GridEnabled = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public float XGridWidth
        {
            get
            {
                return this.fAxisLeft.GridWidth;
            }
            set
            {
                this.fAxisLeft.GridWidth = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public float YGridWidth
        {
            get
            {
                return this.fAxisBottom.GridWidth;
            }
            set
            {
                this.fAxisBottom.GridWidth = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public Color XGridColor
        {
            get
            {
                return this.fAxisLeft.GridColor;
            }
            set
            {
                this.fAxisLeft.GridColor = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public Color YGridColor
        {
            get
            {
                return this.fAxisBottom.GridColor;
            }
            set
            {
                this.fAxisBottom.GridColor = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public DashStyle XGridDashStyle
        {
            get
            {
                return this.fAxisLeft.GridDashStyle;
            }
            set
            {
                this.fAxisLeft.GridDashStyle = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public DashStyle YGridDashStyle
        {
            get
            {
                return this.fAxisBottom.GridDashStyle;
            }
            set
            {
                this.fAxisBottom.GridDashStyle = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisType XAxisType
        {
            get
            {
                return this.fAxisBottom.Type;
            }
            set
            {
                this.fAxisBottom.Type = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public EAxisPosition XAxisPosition
        {
            get
            {
                return this.fAxisBottom.Position;
            }
            set
            {
                this.fAxisBottom.Position = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public bool XAxisMajorTicksEnabled
        {
            get
            {
                return this.fAxisBottom.MajorTicksEnabled;
            }
            set
            {
                this.fAxisBottom.MajorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public bool XAxisMinorTicksEnabled
        {
            get
            {
                return this.fAxisBottom.MinorTicksEnabled;
            }
            set
            {
                this.fAxisBottom.MinorTicksEnabled = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public bool XAxisTitleEnabled
        {
            get
            {
                return this.fAxisBottom.TitleEnabled;
            }
            set
            {
                this.fAxisBottom.TitleEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public string XAxisTitle
        {
            get
            {
                return this.fAxisBottom.Title;
            }
            set
            {
                this.fAxisBottom.Title = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public Font XAxisTitleFont
        {
            get
            {
                return this.fAxisBottom.TitleFont;
            }
            set
            {
                this.fAxisBottom.TitleFont = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public Color XAxisTitleColor
        {
            get
            {
                return this.fAxisBottom.TitleColor;
            }
            set
            {
                this.fAxisBottom.TitleColor = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public int XAxisTitleOffset
        {
            get
            {
                return this.fAxisBottom.TitleOffset;
            }
            set
            {
                this.fAxisBottom.TitleOffset = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisTitlePosition XAxisTitlePosition
        {
            get
            {
                return this.fAxisBottom.TitlePosition;
            }
            set
            {
                this.fAxisBottom.TitlePosition = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public bool XAxisLabelEnabled
        {
            get
            {
                return this.fAxisBottom.LabelEnabled;
            }
            set
            {
                this.fAxisBottom.LabelEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public Font XAxisLabelFont
        {
            get
            {
                return this.fAxisBottom.LabelFont;
            }
            set
            {
                this.fAxisBottom.LabelFont = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public Color XAxisLabelColor
        {
            get
            {
                return this.fAxisBottom.LabelColor;
            }
            set
            {
                this.fAxisBottom.LabelColor = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public int XAxisLabelOffset
        {
            get
            {
                return this.fAxisBottom.LabelOffset;
            }
            set
            {
                this.fAxisBottom.LabelOffset = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public string XAxisLabelFormat
        {
            get
            {
                return this.fAxisBottom.LabelFormat;
            }
            set
            {
                this.fAxisBottom.LabelFormat = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisLabelAlignment XAxisLabelAlignment
        {
            get
            {
                return this.fAxisBottom.LabelAlignment;
            }
            set
            {
                this.fAxisBottom.LabelAlignment = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public EAxisType YAxisType
        {
            get
            {
                return this.fAxisLeft.Type;
            }
            set
            {
                this.fAxisLeft.Type = value;
                this.fAxisRight.Type = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public EAxisPosition YAxisPosition
        {
            get
            {
                return this.fAxisLeft.Position;
            }
            set
            {
                this.fAxisLeft.Position = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public bool YAxisMajorTicksEnabled
        {
            get
            {
                return this.fAxisLeft.MajorTicksEnabled;
            }
            set
            {
                this.fAxisLeft.MajorTicksEnabled = value;
                this.fAxisRight.MajorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisMinorTicksEnabled
        {
            get
            {
                return this.fAxisLeft.MinorTicksEnabled;
            }
            set
            {
                this.fAxisLeft.MinorTicksEnabled = value;
                this.fAxisRight.MinorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisTitleEnabled
        {
            get
            {
                return this.fAxisLeft.TitleEnabled;
            }
            set
            {
                this.fAxisLeft.TitleEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public string YAxisTitle
        {
            get
            {
                return this.fAxisLeft.Title;
            }
            set
            {
                this.fAxisLeft.Title = value;
                this.fAxisRight.Title = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public Font YAxisTitleFont
        {
            get
            {
                return this.fAxisLeft.TitleFont;
            }
            set
            {
                this.fAxisLeft.TitleFont = value;
                this.fAxisRight.TitleFont = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public Color YAxisTitleColor
        {
            get
            {
                return this.fAxisLeft.TitleColor;
            }
            set
            {
                this.fAxisLeft.TitleColor = value;
                this.fAxisRight.TitleColor = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public int YAxisTitleOffset
        {
            get
            {
                return this.fAxisLeft.TitleOffset;
            }
            set
            {
                this.fAxisLeft.TitleOffset = value;
                this.fAxisRight.TitleOffset = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public EAxisTitlePosition YAxisTitlePosition
        {
            get
            {
                return this.fAxisLeft.TitlePosition;
            }
            set
            {
                this.fAxisLeft.TitlePosition = value;
                this.fAxisRight.TitlePosition = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisLabelEnabled
        {
            get
            {
                return this.fAxisLeft.LabelEnabled;
            }
            set
            {
                this.fAxisLeft.LabelEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public Font YAxisLabelFont
        {
            get
            {
                return this.fAxisLeft.LabelFont;
            }
            set
            {
                this.fAxisLeft.LabelFont = value;
                this.fAxisRight.LabelFont = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public Color YAxisLabelColor
        {
            get
            {
                return this.fAxisLeft.LabelColor;
            }
            set
            {
                this.fAxisLeft.LabelColor = value;
                this.fAxisRight.LabelColor = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public int YAxisLabelOffset
        {
            get
            {
                return this.fAxisLeft.LabelOffset;
            }
            set
            {
                this.fAxisLeft.LabelOffset = value;
                this.fAxisRight.LabelOffset = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public string YAxisLabelFormat
        {
            get
            {
                return this.fAxisLeft.LabelFormat;
            }
            set
            {
                this.fAxisLeft.LabelFormat = value;
                this.fAxisRight.LabelFormat = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public EAxisLabelAlignment YAxisLabelAlignment
        {
            get
            {
                return this.fAxisLeft.LabelAlignment;
            }
            set
            {
                this.fAxisLeft.LabelAlignment = value;
                this.fAxisRight.LabelAlignment = value;
            }
        }

        [Browsable(false)]
        public TLegend Legend
        {
            get
            {
                return this.fLegend;
            }
        }

        [Category("Legend")]
        [Description("")]
        public bool LegendEnabled
        {
            get
            {
                return this.fLegendEnabled;
            }
            set
            {
                this.fLegendEnabled = value;
            }
        }

        [Category("Legend")]
        [Description("")]
        public ELegendPosition LegendPosition
        {
            get
            {
                return this.fLegendPosition;
            }
            set
            {
                this.fLegendPosition = value;
            }
        }

        [Description("")]
        [Category("Legend")]
        public int LegendOffsetX
        {
            get
            {
                return this.fLegendOffsetX;
            }
            set
            {
                this.fLegendOffsetX = value;
            }
        }

        [Category("Legend")]
        [Description("")]
        public int LegendOffsetY
        {
            get
            {
                return this.fLegendOffsetY;
            }
            set
            {
                this.fLegendOffsetY = value;
            }
        }

        [Description("")]
        [Category("Legend")]
        public bool LegendBorderEnabled
        {
            get
            {
                return this.fLegend.BorderEnabled;
            }
            set
            {
                this.fLegend.BorderEnabled = value;
            }
        }

        [Category("Legend")]
        [Description("")]
        public Color LegendBorderColor
        {
            get
            {
                return this.fLegend.BorderColor;
            }
            set
            {
                this.fLegend.BorderColor = value;
            }
        }

        [Description("")]
        [Category("Legend")]
        public Color LegendBackColor
        {
            get
            {
                return this.fLegend.BackColor;
            }
            set
            {
                this.fLegend.BackColor = value;
            }
        }

        [Category("Border")]
        [Description("")]
        public bool BorderEnabled
        {
            get
            {
                return this.fBorderEnabled;
            }
            set
            {
                this.fBorderEnabled = value;
            }
        }

        [Category("Border")]
        [Description("")]
        public Color BorderColor
        {
            get
            {
                return this.fBorderColor;
            }
            set
            {
                this.fBorderColor = value;
            }
        }

        [Description("")]
        [Category("Border")]
        public int BorderWidth
        {
            get
            {
                return this.fBorderWidth;
            }
            set
            {
                this.fBorderWidth = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseZoomEnabled
        {
            get
            {
                return this.fMouseZoomEnabled;
            }
            set
            {
                this.fMouseZoomEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseZoomXAxisEnabled
        {
            get
            {
                return this.fMouseZoomXAxisEnabled;
            }
            set
            {
                this.fMouseZoomXAxisEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("")]
        public bool MouseZoomYAxisEnabled
        {
            get
            {
                return this.fMouseZoomYAxisEnabled;
            }
            set
            {
                this.fMouseZoomYAxisEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("")]
        public bool MouseUnzoomEnabled
        {
            get
            {
                return this.fMouseUnzoomEnabled;
            }
            set
            {
                this.fMouseUnzoomEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseUnzoomXAxisEnabled
        {
            get
            {
                return this.fMouseUnzoomXAxisEnabled;
            }
            set
            {
                this.fMouseUnzoomXAxisEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("")]
        public bool MouseUnzoomYAxisEnabled
        {
            get
            {
                return this.fMouseUnzoomYAxisEnabled;
            }
            set
            {
                this.fMouseUnzoomYAxisEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseMoveContentEnabled
        {
            get
            {
                return this.fMouseMoveContentEnabled;
            }
            set
            {
                this.fMouseMoveContentEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseMovePrimitiveEnabled
        {
            get
            {
                return this.fMouseMovePrimitiveEnabled;
            }
            set
            {
                this.fMouseMovePrimitiveEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseDeletePrimitiveEnabled
        {
            get
            {
                return this.fMouseDeletePrimitiveEnabled;
            }
            set
            {
                this.fMouseDeletePrimitiveEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("")]
        public bool MousePadPropertiesEnabled
        {
            get
            {
                return this.fMousePadPropertiesEnabled;
            }
            set
            {
                this.fMousePadPropertiesEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("")]
        public bool MousePrimitivePropertiesEnabled
        {
            get
            {
                return this.fMousePrimitivePropertiesEnabled;
            }
            set
            {
                this.fMousePrimitivePropertiesEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public bool MouseContextMenuEnabled
        {
            get
            {
                return this.fMouseContextMenuEnabled;
            }
            set
            {
                this.fMouseContextMenuEnabled = value;
            }
        }

        [Category("Mouse")]
        [Description("Enable or disable mouse wheel")]
        public bool MouseWheelEnabled
        {
            get
            {
                return this.fMouseWheelEnabled;
            }
            set
            {
                this.fMouseWheelEnabled = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public double MouseWheelSensitivity
        {
            get
            {
                return this.fMouseWheelSensitivity;
            }
            set
            {
                this.fMouseWheelSensitivity = value;
            }
        }

        [Description("")]
        [Category("Mouse")]
        public EMouseWheelMode MouseWheelMode
        {
            get
            {
                return this.fMouseWheelMode;
            }
            set
            {
                this.fMouseWheelMode = value;
            }
        }

        [Browsable(false)]
        public IChartTransformation Transformation
        {
            get
            {
                return this.fTransformation;
            }
        }

        [Category("Transformation")]
        [Description("")]
        public ETransformationType TransformationType
        {
            get
            {
                return this.fTransformationType;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Category("Transformation")]
        [Description("")]
        public bool SessionGridEnabled
        {
            get
            {
                if (this.fTransformationType == ETransformationType.Intraday)
                    return ((TIntradayTransformation) this.Transformation).SessionGridEnabled;
                else
                    return false;
            }
            set
            {
                if (this.fTransformationType != ETransformationType.Intraday)
                    return;
                ((TIntradayTransformation) this.Transformation).SessionGridEnabled = value;
            }
        }

        [Description("")]
        [Category("Transformation")]
        public Color SessionGridColor
        {
            get
            {
                return this.fSessionGridColor;
            }
            set
            {
                this.fSessionGridColor = value;
            }
        }

        [Category("Transformation")]
        [Description("")]
        public TimeSpan SessionStart
        {
            get
            {
                if (this.fTransformationType == ETransformationType.Intraday)
                    return new TimeSpan(((TIntradayTransformation) this.fTransformation).FirstSessionTick);
                else
                    return new TimeSpan(0, 0, 0, 0);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Description("")]
        [Category("Transformation")]
        public TimeSpan SessionEnd
        {
            get
            {
                if (this.fTransformationType == ETransformationType.Intraday)
                    return new TimeSpan(((TIntradayTransformation) this.fTransformation).LastSessionTick);
                else
                    return new TimeSpan(0, 24, 0, 0);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public bool Monitored
        {
            get
            {
                return this.fMonitored;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public int WindowSize
        {
            get
            {
                return this.fWindowSize;
            }
            set
            {
                this.fWindowSize = value;
            }
        }

        [Browsable(false)]
        public int UpdateInterval
        {
            get
            {
                return this.fUpdateInterval;
            }
            set
            {
                this.fUpdateInterval = value;
            }
        }

        public static event NewTickEventHandler NewTick;

        public event ZoomEventHandler Zoom;

        public Pad()
        {
            this.Init();
        }

        public Pad(Chart Chart)
        {
            this.fChart = Chart;
            this.Init();
        }

        public Pad(Chart Chart, double X1, double Y1, double X2, double Y2)
        {
            this.fChart = Chart;
            this.fCanvasX1 = X1;
            this.fCanvasX2 = X2;
            this.fCanvasY1 = Y1;
            this.fCanvasY2 = Y2;
            this.Init();
        }

        private Viewer GetViewer(object obj)
        {
            throw new NotImplementedException();
        }

        public void RegisterViewer(Viewer viewer)
        {
            throw new NotImplementedException();
        }

        public void Set(object obj, string name, object value)
        {
            throw new NotImplementedException();
        }

        public void ResetLastTickTime()
        {
            this.fLastTickTime = 0;
        }

        public void Init()
        {
            throw new NotImplementedException();

        }

        private void InitContextMenu()
        {
            throw new NotImplementedException();
        }

        public virtual void SetCanvas(double x1, double y1, double x2, double y2, int width, int height)
        {
            this.SetCanvas(x1, y1, x2, y2);
            this.SetCanvas(width, height);
        }

        public virtual void SetCanvas(double x1, double y1, double x2, double y2)
        {
            this.fCanvasX1 = x1;
            this.fCanvasX2 = x2;
            this.fCanvasY1 = y1;
            this.fCanvasY2 = y2;
        }

        public virtual void SetCanvas(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SetRangeX(double xMin, double xMax)
        {
            throw new NotImplementedException();
        }

        public void SetRangeX(DateTime xMin, DateTime xMax)
        {
            this.SetRangeX(xMin.Ticks, xMax.Ticks);
        }

        public void SetRangeY(double yMin, double yMax)
        {
            throw new NotImplementedException();
        }

        public void SetRange(double xMin, double xMax, double yMin, double yMax)
        {
            throw new NotImplementedException();
        }

        public void SetRange(DateTime xMin, DateTime xMax, double yMin, double yMax)
        {
            this.SetRange(xMin.Ticks, xMax.Ticks, yMin, yMax);
        }

        public void SetRange(string xMin, string xMax, double yMin, double yMax)
        {
            this.SetRange(DateTime.Parse(xMin).Ticks, DateTime.Parse(xMax).Ticks, yMin, yMax);
        }

        public bool IsInRange(double x, double y)
        {
            return x >= this.XMin && x <= this.XMin + this.CalculateRealQuantityOfTicks_Right(this.XMin, this.XMax) && (y >= this.YMin && y <= this.YMax);
        }

        public void UnZoomX()
        {
            throw new NotImplementedException();
        }

        public void UnZoomY()
        {
            throw new NotImplementedException();
        }

        public void UnZoom()
        {
            throw new NotImplementedException();
        }

        public double GetNextGridDivision(double firstTick, double prevMajor, int majorCount, EGridSize gridSize)
        {
            return this.fTransformation.GetNextGridDivision(firstTick, prevMajor, majorCount, gridSize);
        }

        public double CalculateRealQuantityOfTicks_Right(double x, double y)
        {
            return this.fTransformation.CalculateRealQuantityOfTicks_Right(x, y);
        }

        public double CalculateRealQuantityOfTicks_Left(double x, double y)
        {
            return this.fTransformation.CalculateRealQuantityOfTicks_Left(x, y);
        }

        public void GetFirstGridDivision(ref EGridSize gridSize, ref double min, ref double max, ref DateTime firstDateTime)
        {
            this.fTransformation.GetFirstGridDivision(ref gridSize, ref min, ref max, ref firstDateTime);
        }

        public double CalculateNotInSessionTicks(double x, double y)
        {
            return this.fTransformation.CalculateNotInSessionTicks(x, y);
        }

        public int ClientX(double worldX)
        {
            return (int) ((double) this.fClientX + (worldX - this.XMin - this.CalculateNotInSessionTicks(this.XMin, worldX)) * ((double) this.fClientWidth / (this.XMax - this.XMin)));
        }

        public int ClientY(double worldY)
        {
            return (int) ((double) this.fClientY + (double) this.fClientHeight * (1.0 - (worldY - this.YMin) / (this.YMax - this.YMin)));
        }

        public int ClientX()
        {
            return this.fClientX;
        }

        public int ClientY()
        {
            return this.fClientY;
        }

        public int ClientHeight()
        {
            return this.fClientHeight;
        }

        public int ClientWidth()
        {
            return this.fClientWidth;
        }

        public double WorldX(int clientX)
        {
            return this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.XMin + (double) (clientX - this.fClientX) / (double) this.fClientWidth * (this.XMax - this.XMin));
        }

        public double WorldY(int clientY)
        {
            return this.YMin + (1.0 - (double) (clientY - this.fClientY) / (double) this.fClientHeight) * (this.YMax - this.YMin);
        }

        public Viewer Add(object obj)
        {
            throw new NotImplementedException();
        }

        public Viewer Insert(int index, object obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(object obj)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public static Graphics GetGraphics()
        {
            return Chart.Pad != null ? Chart.Pad.Graphics : null;
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public void PaintAll(Graphics graphics)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Pen pen, double x1, double y1, double x2, double y2, bool doTransform)
        {
            throw new NotImplementedException();
        }

        public void DrawVerticalTick(Pen pen, double x, double y, int length)
        {
        }

        public void DrawHorizontalTick(Pen pen, double x, double y, int length)
        {
            throw new NotImplementedException();
        }

        public void DrawVerticalGrid(Pen pen, double x)
        {
            this.fGraphics.DrawLine(pen, this.ClientX(x), this.fClientY, this.ClientX(x), this.fClientY + this.fClientHeight);
        }

        public void DrawHorizontalGrid(Pen pen, double y)
        {
            this.fGraphics.DrawLine(pen, this.fClientX, this.ClientY(y), this.fClientX + this.fClientWidth, this.ClientY(y));
        }

        public void DrawLine(Pen pen, double x1, double y1, double x2, double y2)
        {
            this.DrawLine(pen, x1, y1, x2, y2, true);
        }

        public void DrawRectangle(Pen pen, double x, double y, int width, int height)
        {
            this.fGraphics.DrawRectangle(pen, this.ClientX(x), this.ClientY(y), width, height);
        }

        public void DrawEllipse(Pen pen, double x, double y, int width, int height)
        {
            this.fGraphics.DrawEllipse(pen, this.ClientX(x), this.ClientY(y), width, height);
        }

        public void DrawBeziers(Pen pen, PointF[] points)
        {
            throw new NotImplementedException();
        }

        public void DrawText(string text, Font font, Brush brush, int x, int y)
        {
            this.fGraphics.DrawString(text, font, brush, x, y);
        }

        private bool IsInsideClient(int x, int y)
        {
            return x > this.fClientX && x < this.fClientX + this.fClientWidth && y > this.fClientY && y < this.fClientY + this.fClientHeight;
        }

        public virtual void MouseMove(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void MouseWheel(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual void DoubleClick(int x, int y)
        {
            throw new NotImplementedException();
        }

        public static void EmitNewTick(DateTime datetime)
        {
            if (NewTick != null)
                NewTick(null, new NewTickEventArgs(datetime));
        }
            
        public void EmitZoom(bool zoom)
        {
            if (this.Zoom != null)
            this.Zoom(null, new ZoomEventArgs(this.XMin, this.XMax, this.YMin, this.YMax, zoom));
        }
    }
}
