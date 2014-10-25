// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MouseButtons = System.Windows.Forms.MouseButtons;
#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    class ViewerList
    {
    }

    public class Pad
    {
        private Dictionary<System.Type, Viewer> viewers = new Dictionary<System.Type, Viewer>();

        public bool Grid3D;

        protected Chart fChart;
        protected Graphics fGraphics;
        protected ArrayList fPrimitives;

        // geometry
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

        // appearance
        protected Color fBackColor;
        protected Color fForeColor;

        // title
        protected string fName;
        protected bool fTitleEnabled;
        protected TTitle fTitle;
        protected int fTitleOffsetX;
        protected int fTitleOffsetY;

        // axis
        protected Axis fAxisLeft;
        protected Axis fAxisRight;
        protected Axis fAxisTop;
        protected Axis fAxisBottom;
        protected bool fLegendEnabled;

        // legend
        protected TLegend fLegend;
        protected ELegendPosition fLegendPosition;
        protected int fLegendOffsetX;
        protected int fLegendOffsetY;

        // border
        protected bool fBorderEnabled;
        protected Color fBorderColor;
        protected int fBorderWidth;

        // outline
        protected bool fOutlineEnabled;
        protected Rectangle fOutlineRectangle;

        // selection
        protected IDrawable fSelectedPrimitive;
        protected TDistance fSelectedPrimitiveDistance;

        // zooming
        protected bool fMouseZoomEnabled;
        protected bool fMouseZoomXAxisEnabled;
        protected bool fMouseZoomYAxisEnabled;
        protected bool fMouseUnzoomEnabled;
        protected bool fMouseUnzoomXAxisEnabled;
        protected bool fMouseUnzoomYAxisEnabled;

        // mouse
        protected bool fOnAxis;
        protected bool fOnPrimitive;
        protected bool fMouseDown;
        protected int fMouseDownX;
        protected int fMouseDownY;
        protected MouseButtons fMouseDownButton;
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

        // grid
        protected Color fSessionGridColor;

        public bool For3D
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object View3D
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Axis[] Axes3D
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Axis AxisX3D
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Axis AxisY3D
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Axis AxisZ3D
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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

        public double CanvasWidth
        {
            get
            {
                return Math.Abs(CanvasX2 - CanvasX1);
            }
        }

        public double CanvasHeight
        {
            get
            {
                return Math.Abs(CanvasY2 - CanvasY1);
            }
        }

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

        public double XMin
        {
            get
            {
                return AxisBottom.Enabled && AxisBottom.Zoomed ? AxisBottom.Min : this.fXMin;
            }
            set
            {
                this.fXMin = value;
            }
        }

        public double XMax
        {
            get
            {
                return AxisBottom.Enabled && AxisBottom.Zoomed ? AxisBottom.Max : this.fXMax; 
            }
            set
            {
                this.fXMax = value;
            }
        }

        public double YMin
        {
            get
            {
                return AxisLeft.Enabled && AxisLeft.Zoomed ? AxisLeft.Min : this.fYMin;
            }
            set
            {
                this.fYMin = value;
            }
        }

        public double YMax
        {
            get
            {
                return AxisLeft.Enabled && AxisLeft.Zoomed ? AxisLeft.Max : this.fYMax;
            }
            set
            {
                this.fYMax = value;
            }
        }

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
                return Title.Items;
            }
        }

        [Description("")]
        [Category("Title")]
        public bool TitleItemsEnabled
        {
            get
            {
                return Title.ItemsEnabled;
            }
            set
            {
                Title.ItemsEnabled = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public string TitleText
        {
            get
            {
                return Title.Text;
            }
            set
            {
                Title.Text = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public Font TitleFont
        {
            get
            {
                return Title.Font;
            }
            set
            {
                Title.Font = value;
            }
        }

        [Category("Title")]
        [Description("")]
        public Color TitleColor
        {
            get
            {
                return Title.Color;
            }
            set
            {
                Title.Color = value;
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
                return Title.Position;
            }
            set
            {
                Title.Position = value;
            }
        }

        [Description("")]
        [Category("Title")]
        public ETitleStrategy TitleStrategy
        {
            get
            {
                return Title.Strategy;
            }
            set
            {
                Title.Strategy = value;
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

        public Axis AxisLeft
        {
            get
            {
                return this.fAxisLeft;
            }
            private set
            {
                this.fAxisLeft = value;
            }
        }

        public Axis AxisRight
        {
            get
            {
                return this.fAxisRight;
            }
            private set
            {
                this.fAxisRight = value;
            }
        }

        public Axis AxisTop
        {
            get
            {
                return this.fAxisTop;
            }
            private set
            {
                this.fAxisTop = value;
            }
        }

        public Axis AxisBottom
        {
            get
            {
                return this.fAxisBottom;
            }
            private set
            {
                this.fAxisBottom = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public bool XGridEnabled
        {
            get
            {
                return AxisLeft.GridEnabled;
            }
            set
            {
                AxisLeft.GridEnabled = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public bool YGridEnabled
        {
            get
            {
                return AxisBottom.GridEnabled;
            }
            set
            {
                AxisBottom.GridEnabled = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public float XGridWidth
        {
            get
            {
                return AxisLeft.GridWidth;
            }
            set
            {
                AxisLeft.GridWidth = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public float YGridWidth
        {
            get
            {
                return AxisBottom.GridWidth;
            }
            set
            {
                AxisBottom.GridWidth = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public Color XGridColor
        {
            get
            {
                return AxisLeft.GridColor;
            }
            set
            {
                AxisLeft.GridColor = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public Color YGridColor
        {
            get
            {
                return AxisBottom.GridColor;
            }
            set
            {
                AxisBottom.GridColor = value;
            }
        }

        [Description("")]
        [Category("Grid")]
        public DashStyle XGridDashStyle
        {
            get
            {
                return AxisLeft.GridDashStyle;
            }
            set
            {
                AxisLeft.GridDashStyle = value;
            }
        }

        [Category("Grid")]
        [Description("")]
        public DashStyle YGridDashStyle
        {
            get
            {
                return AxisBottom.GridDashStyle;
            }
            set
            {
                AxisBottom.GridDashStyle = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisType XAxisType
        {
            get
            {
                return AxisBottom.Type;
            }
            set
            {
                AxisBottom.Type = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public EAxisPosition XAxisPosition
        {
            get
            {
                return AxisBottom.Position;
            }
            set
            {
                AxisBottom.Position = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public bool XAxisMajorTicksEnabled
        {
            get
            {
                return AxisBottom.MajorTicksEnabled;
            }
            set
            {
                AxisBottom.MajorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public bool XAxisMinorTicksEnabled
        {
            get
            {
                return AxisBottom.MinorTicksEnabled;
            }
            set
            {
                AxisBottom.MinorTicksEnabled = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public bool XAxisTitleEnabled
        {
            get
            {
                return AxisBottom.TitleEnabled;
            }
            set
            {
                AxisBottom.TitleEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public string XAxisTitle
        {
            get
            {
                return AxisBottom.Title;
            }
            set
            {
                AxisBottom.Title = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public Font XAxisTitleFont
        {
            get
            {
                return AxisBottom.TitleFont;
            }
            set
            {
                AxisBottom.TitleFont = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public Color XAxisTitleColor
        {
            get
            {
                return AxisBottom.TitleColor;
            }
            set
            {
                AxisBottom.TitleColor = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public int XAxisTitleOffset
        {
            get
            {
                return AxisBottom.TitleOffset;
            }
            set
            {
                AxisBottom.TitleOffset = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisTitlePosition XAxisTitlePosition
        {
            get
            {
                return AxisBottom.TitlePosition;
            }
            set
            {
                AxisBottom.TitlePosition = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public bool XAxisLabelEnabled
        {
            get
            {
                return AxisBottom.LabelEnabled;
            }
            set
            {
                AxisBottom.LabelEnabled = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public Font XAxisLabelFont
        {
            get
            {
                return AxisBottom.LabelFont;
            }
            set
            {
                AxisBottom.LabelFont = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public Color XAxisLabelColor
        {
            get
            {
                return AxisBottom.LabelColor;
            }
            set
            {
                AxisBottom.LabelColor = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public int XAxisLabelOffset
        {
            get
            {
                return AxisBottom.LabelOffset;
            }
            set
            {
                AxisBottom.LabelOffset = value;
            }
        }

        [Description("")]
        [Category("XAxis")]
        public string XAxisLabelFormat
        {
            get
            {
                return AxisBottom.LabelFormat;
            }
            set
            {
                AxisBottom.LabelFormat = value;
            }
        }

        [Category("XAxis")]
        [Description("")]
        public EAxisLabelAlignment XAxisLabelAlignment
        {
            get
            {
                return AxisBottom.LabelAlignment;
            }
            set
            {
                AxisBottom.LabelAlignment = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public EAxisType YAxisType
        {
            get
            {
                return AxisLeft.Type;
            }
            set
            {
                AxisLeft.Type = value;
                AxisRight.Type = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public EAxisPosition YAxisPosition
        {
            get
            {
                return AxisLeft.Position;
            }
            set
            {
                AxisLeft.Position = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public bool YAxisMajorTicksEnabled
        {
            get
            {
                return AxisLeft.MajorTicksEnabled;
            }
            set
            {
                AxisLeft.MajorTicksEnabled = value;
                AxisRight.MajorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisMinorTicksEnabled
        {
            get
            {
                return AxisLeft.MinorTicksEnabled;
            }
            set
            {
                AxisLeft.MinorTicksEnabled = value;
                AxisRight.MinorTicksEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisTitleEnabled
        {
            get
            {
                return AxisLeft.TitleEnabled;
            }
            set
            {
                AxisLeft.TitleEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public string YAxisTitle
        {
            get
            {
                return AxisLeft.Title;
            }
            set
            {
                AxisLeft.Title = value;
                AxisRight.Title = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public Font YAxisTitleFont
        {
            get
            {
                return AxisLeft.TitleFont;
            }
            set
            {
                AxisLeft.TitleFont = value;
                AxisRight.TitleFont = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public Color YAxisTitleColor
        {
            get
            {
                return AxisLeft.TitleColor;
            }
            set
            {
                AxisLeft.TitleColor = value;
                AxisRight.TitleColor = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public int YAxisTitleOffset
        {
            get
            {
                return AxisLeft.TitleOffset;
            }
            set
            {
                AxisLeft.TitleOffset = value;
                this.fAxisRight.TitleOffset = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public EAxisTitlePosition YAxisTitlePosition
        {
            get
            {
                return AxisLeft.TitlePosition;
            }
            set
            {
                AxisLeft.TitlePosition = value;
                AxisRight.TitlePosition = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public bool YAxisLabelEnabled
        {
            get
            {
                return AxisLeft.LabelEnabled;
            }
            set
            {
                AxisLeft.LabelEnabled = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public Font YAxisLabelFont
        {
            get
            {
                return AxisLeft.LabelFont;
            }
            set
            {
                AxisLeft.LabelFont = value;
                AxisRight.LabelFont = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public Color YAxisLabelColor
        {
            get
            {
                return AxisLeft.LabelColor;
            }
            set
            {
                AxisLeft.LabelColor = value;
                AxisRight.LabelColor = value;
            }
        }

        [Category("YAxis")]
        [Description("")]
        public int YAxisLabelOffset
        {
            get
            {
                return AxisLeft.LabelOffset;
            }
            set
            {
                AxisLeft.LabelOffset = value;
                AxisRight.LabelOffset = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public string YAxisLabelFormat
        {
            get
            {
                return AxisLeft.LabelFormat;
            }
            set
            {
                AxisLeft.LabelFormat = value;
                AxisRight.LabelFormat = value;
            }
        }

        [Description("")]
        [Category("YAxis")]
        public EAxisLabelAlignment YAxisLabelAlignment
        {
            get
            {
                return AxisLeft.LabelAlignment;
            }
            set
            {
                AxisLeft.LabelAlignment = value;
                AxisRight.LabelAlignment = value;
            }
        }

        [Browsable(false)]
        public TLegend Legend
        {
            get
            {
                return this.fLegend;
            }
            private set
            {
                this.fLegend = value;
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
                return Legend.BorderEnabled;
            }
            set
            {
                Legend.BorderEnabled = value;
            }
        }

        [Category("Legend")]
        [Description("")]
        public Color LegendBorderColor
        {
            get
            {
                return Legend.BorderColor;
            }
            set
            {
                Legend.BorderColor = value;
            }
        }

        [Description("")]
        [Category("Legend")]
        public Color LegendBackColor
        {
            get
            {
                return Legend.BackColor;
            }
            set
            {
                Legend.BackColor = value;
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
            private set
            {
                this.fTransformation = value;
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
                return this.fTransformationType == ETransformationType.Intraday ? ((TIntradayTransformation)this.Transformation).SessionGridEnabled : false;
            }
            set
            {
                if (this.fTransformationType == ETransformationType.Intraday)
                    ((TIntradayTransformation)this.Transformation).SessionGridEnabled = value;
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
                return this.fTransformationType == ETransformationType.Intraday ? new TimeSpan(((TIntradayTransformation)this.fTransformation).FirstSessionTick) : new TimeSpan(0);
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
                return this.fTransformationType == ETransformationType.Intraday ? new TimeSpan(((TIntradayTransformation)this.fTransformation).LastSessionTick) : new TimeSpan(1, 0, 0, 0);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Monitored
        {
            get
            {
                return this.fMonitored;
            }
            set
            {
                this.fMonitored = value;
                if (this.fMonitored)
                    NewTick += new NewTickEventHandler(this.OnNewTick);
                else
                    NewTick -= new NewTickEventHandler(this.OnNewTick); 
            }
        }

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
            : this(null)
        {
        }

        public Pad(Chart chart)
            : this(chart, 0, 0, 0, 0)
        {
        }

        public Pad(Chart chart, double x1, double y1, double x2, double y2)
        {
            this.fChart = chart;
            SetCanvas(x1, y1, x2, y2);
            this.Init();
        }

        public void Init()
        {
            this.fPrimitives = new ArrayList();
            Chart.Pad = this;
            //            this.Features3D = new Pad.TFeatures3D(this);

            this.fX1 = this.fY1 = 0;
            this.fX2 = this.fY2 = 1; 
            this.fWidth = Chart.ClientSize.Width;
            this.fHeight = Chart.ClientSize.Height;
            this.fClientX = 10;
            this.fClientY = 10;
            this.fClientWidth = 0;
            this.fClientHeight = 0;
            BackColor = Color.LightGray;
            ForeColor = Color.White;
            MarginLeft = 10;
            MarginRight = 20;
            MarginTop = 10;
            MarginBottom = 10;
            TitleEnabled = true;
            Title = new TTitle(this, "");
            TitleOffsetX = TitleOffsetY = 5;
            Transformation = (IChartTransformation)new TIntradayTransformation();
            this.fTransformationType = ETransformationType.Empty;
            SessionGridColor = Color.Blue;
            AxisLeft = new Axis(this, EAxisPosition.Left);
            AxisRight = new Axis(this, EAxisPosition.Right);
            AxisTop = new Axis(this, EAxisPosition.Top);
            AxisBottom = new Axis(this, EAxisPosition.Bottom);
            AxisRight.LabelEnabled = false;
            AxisRight.TitleEnabled = false;
            AxisTop.LabelEnabled = false;
            AxisTop.TitleEnabled = false;
            Legend = new TLegend(this);
            LegendEnabled = false;
            LegendPosition = ELegendPosition.TopRight;
            LegendOffsetX = 5;
            LegendOffsetY = 5;
            BorderEnabled = true;
            BorderColor = Color.Black;
            BorderWidth = 1;
            SetRange(0.0, 100.0, 0.0, 100.0);
            Graphics = null;

            this.fOnAxis = false;
            this.fOnPrimitive = false;
            this.fMouseDown = false;
            this.fMouseDownX = 0;
            this.fMouseDownY = 0;
            this.fOutlineEnabled = false;
            WindowSize = 600;
            this.fLastTickTime = 0;
            UpdateInterval = 1;
            this.fLastUpdateDateTime = DateTime.Now;
            Monitored = false;
            this.fUpdating = false;
            MouseZoomEnabled = true;
            MouseZoomXAxisEnabled = true;
            MouseZoomYAxisEnabled = true;
            MouseUnzoomEnabled = true;
            MouseUnzoomXAxisEnabled = true;
            MouseUnzoomYAxisEnabled = true;
            MouseMoveContentEnabled = true;
            MouseMovePrimitiveEnabled = true;
            MouseDeletePrimitiveEnabled = true;
            MousePadPropertiesEnabled = true;
            MousePrimitivePropertiesEnabled = true;
            MouseContextMenuEnabled = true;
            MouseWheelEnabled = true;
            MouseWheelSensitivity = 0.1;
            MouseWheelMode = EMouseWheelMode.ZoomX;
        }

        public void RegisterViewer(Viewer viewer)
        {
            this.viewers.Add(viewer.Type, viewer);
        }

        public void Set(object obj, string name, object value)
        {
            throw new NotImplementedException();
        }

        public void ResetLastTickTime()
        {
            this.fLastTickTime = 0;
        }
            
        public virtual void SetCanvas(double x1, double y1, double x2, double y2, int width, int height)
        {
            SetCanvas(x1, y1, x2, y2);
            SetCanvas(width, height);
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
            this.fX1 = (int)(width * this.fCanvasX1);
            this.fX2 = (int)(width * this.fCanvasX2);
            this.fY1 = (int)(height * this.fCanvasY1);
            this.fY2 = (int)(height * this.fCanvasY2);
            this.fWidth = this.fX2 - this.fX1;
            this.fHeight = this.fY2 - this.fY1;    
        }

        public void SetRangeX(double xMin, double xMax)
        {
            this.fXMin = xMin;
            this.fXMax = xMax - CalculateNotInSessionTicks(xMin, xMax);
            this.fAxisBottom.SetRange(xMin, xMax);
            this.fAxisTop.SetRange(xMin, xMax);
        }

        public void SetRangeX(DateTime xMin, DateTime xMax)
        {
            SetRangeX(xMin.Ticks, xMax.Ticks);
        }

        public void SetRangeY(double yMin, double yMax)
        {
            this.fYMin = yMin;
            this.fYMax = yMax;
            this.fAxisLeft.SetRange(yMin, yMax);
            this.fAxisRight.SetRange(yMin, yMax);
        }

        public void SetRange(double xMin, double xMax, double yMin, double yMax)
        {
            SetRangeX(xMin, xMax);
            SetRangeY(yMin, yMax);
        }

        public void SetRange(DateTime xMin, DateTime xMax, double yMin, double yMax)
        {
            SetRange(xMin.Ticks, xMax.Ticks, yMin, yMax);
        }

        public void SetRange(string xMin, string xMax, double yMin, double yMax)
        {
            SetRange(DateTime.Parse(xMin).Ticks, DateTime.Parse(xMax).Ticks, yMin, yMax);
        }

        public bool IsInRange(double x, double y)
        {
            return XMin <= x && x <= XMin + this.CalculateRealQuantityOfTicks_Right(XMin, XMax) && YMin <= y && y <= YMax;
        }

        public void UnZoomX()
        {
            AxisBottom.UnZoom();
            AxisTop.UnZoom();
        }

        public void UnZoomY()
        {
            AxisLeft.UnZoom();
            AxisRight.UnZoom(); 
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
            return (int)((double)this.fClientX + (worldX - this.XMin - this.CalculateNotInSessionTicks(this.XMin, worldX)) * ((double)this.fClientWidth / (this.XMax - this.XMin)));
        }

        public int ClientY(double worldY)
        {
            return (int)((double)this.fClientY + (double)this.fClientHeight * (1.0 - (worldY - this.YMin) / (this.YMax - this.YMin)));
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
            return this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, XMin + (double)(clientX - this.fClientX) / (double)this.fClientWidth * (XMax - XMin));
        }

        public double WorldY(int clientY)
        {
            return YMin + (1.0 - (double)(clientY - this.fClientY) / (double)this.fClientHeight) * (YMax - YMin);
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
            this.PaintAll(graphics);
        }

        public void PaintAll(Graphics graphics)
        {
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
            this.Graphics.DrawLine(pen, this.ClientX(x), this.fClientY, this.ClientX(x), this.fClientY + this.fClientHeight);
        }

        public void DrawHorizontalGrid(Pen pen, double y)
        {
            this.Graphics.DrawLine(pen, this.fClientX, this.ClientY(y), this.fClientX + this.fClientWidth, this.ClientY(y));
        }

        public void DrawLine(Pen pen, double x1, double y1, double x2, double y2)
        {
            this.DrawLine(pen, x1, y1, x2, y2, true);
        }

        public void DrawRectangle(Pen pen, double x, double y, int width, int height)
        {
            this.Graphics.DrawRectangle(pen, this.ClientX(x), this.ClientY(y), width, height);
        }

        public void DrawEllipse(Pen pen, double x, double y, int width, int height)
        {
            this.Graphics.DrawEllipse(pen, this.ClientX(x), this.ClientY(y), width, height);
        }

        public void DrawBeziers(Pen pen, PointF[] points)
        {
            throw new NotImplementedException();
        }

        public void DrawText(string text, Font font, Brush brush, int x, int y)
        {
            Graphics.DrawString(text, font, brush, x, y);
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
            //throw new NotImplementedException();
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
            if (Zoom != null)
                Zoom(null, new ZoomEventArgs(this.XMin, this.XMax, this.YMin, this.YMax, zoom));
        }

        private void OnNewTick(object sender, NewTickEventArgs args)
        {
        }
    }
}
