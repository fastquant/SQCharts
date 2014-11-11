using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
#if GTK
using MouseButtons = System.Windows.Forms.MouseButtons;
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class Pad
    {
        private Dictionary<System.Type, Viewer> viewers = new Dictionary<System.Type, Viewer>();
        private List<Pad.ObjectViewer> objectViewers = new List<Pad.ObjectViewer>();
        private Pad.TFeatures3D Features3D;
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
        #if GTK
        #else
        [NonSerialized]
        private ContextMenu PrimitiveContextMenu;
        [NonSerialized]
        private MenuItem DeleteMenuItem;
        [NonSerialized]
        private MenuItem PropertiesMenuItem;
        #endif
        private int TitleHeight;
        private int AxisBottomHeight;
        private int AxisTopHeight;
        private int AxisRightWidth;
        private int AxisLeftWidth;

        [Browsable(false)]
        public bool For3D
        {
            get
            {
                return this.Features3D.Active;
            }
            set
            {
                this.Features3D.Active = value;
            }
        }

        [Browsable(false)]
        public object View3D
        {
            get
            {
                return this.Features3D.View;
            }
            set
            {
                this.Features3D.View = value;
            }
        }

        [Browsable(false)]
        public Axis[] Axes3D
        {
            get
            {
                return this.Features3D.Axes;
            }
        }

        [Browsable(false)]
        public Axis AxisX3D
        {
            get
            {
                return this.Features3D.Axes[0];
            }
        }

        [Browsable(false)]
        public Axis AxisY3D
        {
            get
            {
                return this.Features3D.Axes[1];
            }
        }

        [Browsable(false)]
        public Axis AxisZ3D
        {
            get
            {
                return this.Features3D.Axes[2];
            }
        }

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
                return this.fChart.DoubleBufferingEnabled;
            }
            set
            {
                this.fChart.DoubleBufferingEnabled = value;
            }
        }

        [Description("Enable or disable smoothing")]
        [Category("Appearance")]
        public bool SmoothingEnabled
        {
            get
            {
                return this.fChart.SmoothingEnabled;
            }
            set
            {
                this.fChart.SmoothingEnabled = value;
            }
        }

        [Description("Enable or disable antialiasing")]
        [Category("Appearance")]
        public bool AntiAliasingEnabled
        {
            get
            {
                return this.fChart.AntiAliasingEnabled;
            }
            set
            {
                this.fChart.AntiAliasingEnabled = value;
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
                return Math.Abs(this.fCanvasX2 - this.fCanvasX1);
            }
        }

        [Browsable(false)]
        public double CanvasHeight
        {
            get
            {
                return Math.Abs(this.fCanvasY2 - this.fCanvasY1);
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
                if (this.fAxisBottom.Enabled && this.fAxisBottom.Zoomed)
                    return this.fAxisBottom.Min;
                else
                    return this.fXMin;
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
                if (this.fAxisBottom.Enabled && this.fAxisBottom.Zoomed)
                    return this.fAxisBottom.Max;
                else
                    return this.fXMax;
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
                if (this.fAxisLeft.Enabled && this.fAxisLeft.Zoomed)
                    return this.fAxisLeft.Min;
                else
                    return this.fYMin;
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
                if (this.fAxisLeft.Enabled && this.fAxisLeft.Zoomed)
                    return this.fAxisLeft.Max;
                else
                    return this.fYMax;
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
                this.fTransformationType = value;
                double Y1 = this.fXMin + this.CalculateRealQuantityOfTicks_Right(this.fXMin, this.fXMax);
                double Y2 = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                if (this.fTransformationType == ETransformationType.Empty)
                    this.fTransformation = (IChartTransformation) new TEmptyTransformation();
                if (this.fTransformationType == ETransformationType.Intraday)
                    this.fTransformation = (IChartTransformation) new TIntradayTransformation();
                this.fXMax = Y1 - this.CalculateNotInSessionTicks(this.fXMin, Y1);
                this.fAxisBottom.Max = Y2 - this.CalculateNotInSessionTicks(this.fAxisBottom.Min, Y2);
                this.Update();
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
                double Y1 = this.fXMin + this.CalculateRealQuantityOfTicks_Right(this.fXMin, this.fXMax);
                double Y2 = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                if (this.fTransformationType == ETransformationType.Intraday)
                    ((TIntradayTransformation) this.fTransformation).FirstSessionTick = value.Ticks;
                this.fXMax = Y1 - this.CalculateNotInSessionTicks(this.fXMin, Y1);
                this.fAxisBottom.Max = Y2 - this.CalculateNotInSessionTicks(this.fAxisBottom.Min, Y2);
                this.Update();
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
                double Y1 = this.fXMin + this.CalculateRealQuantityOfTicks_Right(this.fXMin, this.fXMax);
                double Y2 = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                if (this.fTransformationType == ETransformationType.Intraday)
                    ((TIntradayTransformation) this.fTransformation).LastSessionTick = value.Ticks;
                this.fXMax = Y1 - this.CalculateNotInSessionTicks(this.fXMin, Y1);
                this.fAxisBottom.Max = Y2 - this.CalculateNotInSessionTicks(this.fAxisBottom.Min, Y2);
                this.Update();
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
                this.fMonitored = value;
                if (this.fMonitored)
                    Pad.NewTick += new NewTickEventHandler(this.OnNewTick);
                else
                    Pad.NewTick -= new NewTickEventHandler(this.OnNewTick);
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
            System.Type key = obj.GetType();
            Viewer viewer = (Viewer) null;
            for (; key != (System.Type) null; key = key.BaseType)
            {
                if (this.viewers.TryGetValue(key, out viewer))
                    return Activator.CreateInstance(viewer.GetType()) as Viewer;
            }
            Console.WriteLine("No viewer exists for object with type " + (object) obj.GetType());
            return (Viewer) null;
        }

        public void RegisterViewer(Viewer viewer)
        {
            try
            {
                this.viewers.Add(viewer.Type, viewer);
            }
            catch
            {
            }
        }

        public void Set(object obj, string name, object value)
        {
            Viewer viewer = this.GetViewer(obj);
            if (viewer == null)
                return;
            viewer.Set(obj, name, value);
        }

        public void ResetLastTickTime()
        {
            this.fLastTickTime = 0;
        }

        public void Init()
        {
            this.fPrimitives = new ArrayList();
            Chart.Pad = this;
            this.Features3D = new Pad.TFeatures3D(this);
            this.fBackColor = Color.LightGray;
            this.fForeColor = Color.White;
            this.fX1 = 0;
            this.fX2 = 1;
            this.fY1 = 0;
            this.fY2 = 1;
            this.fWidth = this.fChart.ClientSize.Width;
            this.fHeight = this.fChart.ClientSize.Height;
            this.fClientX = 10;
            this.fClientY = 10;
            this.fClientWidth = 0;
            this.fClientHeight = 0;
            this.fMarginLeft = 10;
            this.fMarginRight = 20;
            this.fMarginTop = 10;
            this.fMarginBottom = 10;
            this.fTitle = new TTitle(this, "");
            this.fTitleEnabled = true;
            this.fTitleOffsetX = 5;
            this.fTitleOffsetY = 5;
            this.fTransformation = (IChartTransformation) new TIntradayTransformation();
            this.fTransformationType = ETransformationType.Empty;
            this.fSessionGridColor = Color.Blue;
            this.fAxisLeft = new Axis(this, EAxisPosition.Left);
            this.fAxisRight = new Axis(this, EAxisPosition.Right);
            this.fAxisTop = new Axis(this, EAxisPosition.Top);
            this.fAxisBottom = new Axis(this, EAxisPosition.Bottom);
            this.fAxisRight.LabelEnabled = false;
            this.fAxisRight.TitleEnabled = false;
            this.fAxisTop.LabelEnabled = false;
            this.fAxisTop.TitleEnabled = false;
            this.fLegend = new TLegend(this);
            this.fLegendEnabled = false;
            this.fLegendPosition = ELegendPosition.TopRight;
            this.fLegendOffsetX = 5;
            this.fLegendOffsetY = 5;
            this.fBorderEnabled = true;
            this.fBorderColor = Color.Black;
            this.fBorderWidth = 1;
            this.SetRange(0.0, 100.0, 0.0, 100.0);
            this.fGraphics = (Graphics) null;
            this.fOnAxis = false;
            this.fOnPrimitive = false;
            this.fMouseDown = false;
            this.fMouseDownX = 0;
            this.fMouseDownY = 0;
            this.fOutlineEnabled = false;
            this.fWindowSize = 600;
            this.fLastTickTime = 0;
            this.fUpdateInterval = 1;
            this.fLastUpdateDateTime = DateTime.Now;
            this.Monitored = false;
            this.fUpdating = false;
            this.fMouseZoomEnabled = true;
            this.fMouseZoomXAxisEnabled = true;
            this.fMouseZoomYAxisEnabled = true;
            this.fMouseUnzoomEnabled = true;
            this.fMouseUnzoomXAxisEnabled = true;
            this.fMouseUnzoomYAxisEnabled = true;
            this.fMouseMoveContentEnabled = true;
            this.fMouseMovePrimitiveEnabled = true;
            this.fMouseDeletePrimitiveEnabled = true;
            this.fMousePadPropertiesEnabled = true;
            this.fMousePrimitivePropertiesEnabled = true;
            this.fMouseContextMenuEnabled = true;
            this.fMouseWheelEnabled = true;
            this.fMouseWheelSensitivity = 0.1;
            this.fMouseWheelMode = EMouseWheelMode.ZoomX;
        }

        private void InitContextMenu()
        {
            #if GTK
            #else
            if (this.PrimitiveContextMenu != null)
                return;
            this.PrimitiveContextMenu = new ContextMenu();
            this.DeleteMenuItem = new MenuItem();
            this.PropertiesMenuItem = new MenuItem();
            MenuItem menuItem = new MenuItem();
            this.PrimitiveContextMenu.MenuItems.AddRange(new MenuItem[3]
            {
                this.DeleteMenuItem,
                menuItem,
                this.PropertiesMenuItem
            });
            this.DeleteMenuItem.Index = 0;
            this.DeleteMenuItem.Text = "Delete";
            this.DeleteMenuItem.Click += new EventHandler(this.DeleteMenuItem_Click);
            menuItem.Index = 1;
            menuItem.Text = "-";
            this.PropertiesMenuItem.Index = 2;
            this.PropertiesMenuItem.Text = "Properties";
            this.PropertiesMenuItem.Click += new EventHandler(this.PropertiesMenuItem_Click);
            #endif
        }

        public virtual void SetCanvas(double X1, double Y1, double X2, double Y2, int Width, int Height)
        {
            this.fCanvasX1 = X1;
            this.fCanvasX2 = X2;
            this.fCanvasY1 = Y1;
            this.fCanvasY2 = Y2;
            this.SetCanvas(Width, Height);
        }

        public virtual void SetCanvas(double X1, double Y1, double X2, double Y2)
        {
            this.fCanvasX1 = X1;
            this.fCanvasX2 = X2;
            this.fCanvasY1 = Y1;
            this.fCanvasY2 = Y2;
        }

        public virtual void SetCanvas(int Width, int Height)
        {
            this.fX1 = (int) ((double) Width * this.fCanvasX1);
            this.fX2 = (int) ((double) Width * this.fCanvasX2);
            this.fY1 = (int) ((double) Height * this.fCanvasY1);
            this.fY2 = (int) ((double) Height * this.fCanvasY2);
            this.fWidth = this.fX2 - this.fX1;
            this.fHeight = this.fY2 - this.fY1;
        }

        public void SetRangeX(double XMin, double XMax)
        {
            this.fXMin = XMin;
            this.fXMax = XMax - this.CalculateNotInSessionTicks(XMin, XMax);
            this.fAxisBottom.SetRange(this.fXMin, this.fXMax);
            this.fAxisTop.SetRange(this.fXMin, this.fXMax);
            this.Features3D.SetRangeX(this.fXMin, this.fXMax);
        }

        public void SetRangeX(DateTime XMin, DateTime XMax)
        {
            this.SetRangeX((double) XMin.Ticks, (double) XMax.Ticks);
        }

        public void SetRangeY(double YMin, double YMax)
        {
            this.fYMin = YMin;
            this.fYMax = YMax;
            this.fAxisLeft.SetRange(this.fYMin, this.fYMax);
            this.fAxisRight.SetRange(this.fYMin, this.fYMax);
            this.Features3D.SetRangeY(this.fYMin, this.fYMax);
        }

        public void SetRange(double XMin, double XMax, double YMin, double YMax)
        {
            this.fXMin = XMin;
            this.fXMax = XMax - this.CalculateNotInSessionTicks(XMin, XMax);
            this.fYMin = YMin;
            this.fYMax = YMax;
            this.fAxisBottom.SetRange(this.fXMin, this.fXMax);
            this.fAxisTop.SetRange(this.fXMin, this.fXMax);
            this.fAxisLeft.SetRange(this.fYMin, this.fYMax);
            this.fAxisRight.SetRange(this.fYMin, this.fYMax);
            this.Features3D.SetRange(this.fXMin, this.fXMax, this.fYMin, this.fYMax);
        }

        public void SetRange(DateTime XMin, DateTime XMax, double YMin, double YMax)
        {
            this.SetRange((double) XMin.Ticks, (double) XMax.Ticks, YMin, YMax);
        }

        public void SetRange(string XMin, string XMax, double YMin, double YMax)
        {
            this.SetRange((double) DateTime.Parse(XMin).Ticks, (double) DateTime.Parse(XMax).Ticks, YMin, YMax);
        }

        public bool IsInRange(double X, double Y)
        {
            return X >= this.XMin && X <= this.XMin + this.CalculateRealQuantityOfTicks_Right(this.XMin, this.XMax) && (Y >= this.YMin && Y <= this.YMax);
        }

        public void UnZoomX()
        {
            this.fAxisBottom.UnZoom();
            this.fAxisTop.UnZoom();
        }

        public void UnZoomY()
        {
            this.fAxisLeft.UnZoom();
            this.fAxisRight.UnZoom();
        }

        public void UnZoom()
        {
            this.fAxisBottom.SetRange(this.fXMin, this.fXMax);
            this.fAxisTop.SetRange(this.fXMin, this.fXMax);
            this.fAxisLeft.SetRange(this.fYMin, this.fYMax);
            this.fAxisRight.SetRange(this.fYMin, this.fYMax);
            this.fAxisBottom.Zoomed = false;
            this.fAxisTop.Zoomed = false;
            this.fAxisLeft.Zoomed = false;
            this.fAxisRight.Zoomed = false;
            if (this.fChart.GroupZoomEnabled)
                return;
            this.Update();
        }

        public double GetNextGridDivision(double FirstTick, double PrevMajor, int MajorCount, EGridSize GridSize)
        {
            return this.fTransformation.GetNextGridDivision(FirstTick, PrevMajor, MajorCount, GridSize);
        }

        public double CalculateRealQuantityOfTicks_Right(double X, double Y)
        {
            return this.fTransformation.CalculateRealQuantityOfTicks_Right(X, Y);
        }

        public double CalculateRealQuantityOfTicks_Left(double X, double Y)
        {
            return this.fTransformation.CalculateRealQuantityOfTicks_Left(X, Y);
        }

        public void GetFirstGridDivision(ref EGridSize GridSize, ref double Min, ref double Max, ref DateTime FirstDateTime)
        {
            this.fTransformation.GetFirstGridDivision(ref GridSize, ref Min, ref Max, ref FirstDateTime);
        }

        public double CalculateNotInSessionTicks(double X, double Y)
        {
            return this.fTransformation.CalculateNotInSessionTicks(X, Y);
        }

        public int ClientX(double WorldX)
        {
            return (int) ((double) this.fClientX + (WorldX - this.XMin - this.CalculateNotInSessionTicks(this.XMin, WorldX)) * ((double) this.fClientWidth / (this.XMax - this.XMin)));
        }

        public int ClientY(double WorldY)
        {
            return (int) ((double) this.fClientY + (double) this.fClientHeight * (1.0 - (WorldY - this.YMin) / (this.YMax - this.YMin)));
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

        public double WorldX(int ClientX)
        {
            return this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.XMin + (double) (ClientX - this.fClientX) / (double) this.fClientWidth * (this.XMax - this.XMin));
        }

        public double WorldY(int ClientY)
        {
            return this.YMin + (1.0 - (double) (ClientY - this.fClientY) / (double) this.fClientHeight) * (this.YMax - this.YMin);
        }

        public Viewer Add(object obj)
        {
            if (obj is IDrawable)
            {
                this.fPrimitives.Add((object) (obj as IDrawable));
                return (Viewer) null;
            }
            else
            {
                Viewer viewer = this.GetViewer(obj);
                if (viewer == null)
                    throw new Exception("There is no viewer for " + (object) obj.GetType());
                this.objectViewers.Add(new Pad.ObjectViewer(obj, viewer));
                return viewer;
            }
        }

        public Viewer Insert(int index, object obj)
        {
            if (obj is IDrawable)
            {
                this.fPrimitives.Add((object) (obj as IDrawable));
                return (Viewer) null;
            }
            else
            {
                Viewer viewer = this.GetViewer(obj);
                if (viewer == null)
                    throw new Exception("There is no viewer for " + (object) obj.GetType());
                this.objectViewers.Insert(index, new Pad.ObjectViewer(obj, viewer));
                return viewer;
            }
        }

        public void Remove(object obj)
        {
            if (obj is IDrawable)
            {
                this.fPrimitives.Remove(obj);
            }
            else
            {
                foreach (Pad.ObjectViewer objectViewer in this.objectViewers)
                {
                    if (objectViewer.Object == obj)
                    {
                        this.objectViewers.Remove(objectViewer);
                        break;
                    }
                }
            }
        }

        public void Clear()
        {
            this.fPrimitives.Clear();
            this.fLegend.Items.Clear();
            this.objectViewers.Clear();
        }

        public static Graphics GetGraphics()
        {
            if (Chart.Pad != null)
                return Chart.Pad.Graphics;
            else
                return (Graphics) null;
        }

        public virtual void Update()
        {
            if (this.fUpdating)
                return;
            this.fUpdating = true;
            this.fChart.UpdatePads();
            this.fUpdating = false;
        }

        public virtual void Update(Graphics Graphics)
        {
            double val1_1 = double.MaxValue;
            double val1_2 = double.MinValue;
            double val1_3 = double.MaxValue;
            double val1_4 = double.MinValue;
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            try
            {
                foreach (IDrawable drawable in this.fPrimitives)
                {
                    if (drawable is Histogram)
                        flag3 = true;
                    if (drawable is IZoomable)
                    {
                        IZoomable zoomable = (IZoomable) drawable;
                        if (zoomable.IsPadRangeX())
                        {
                            PadRange padRangeX = zoomable.GetPadRangeX(this);
                            if (padRangeX.IsValid)
                            {
                                val1_1 = Math.Min(val1_1, padRangeX.Min);
                                val1_2 = Math.Max(val1_2, padRangeX.Max);
                                flag1 = true;
                            }
                        }
                        if (zoomable.IsPadRangeY())
                        {
                            double max = this.fAxisBottom.Max;
                            double num = this.fXMax;
                            this.fAxisBottom.Max = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                            this.fXMax = this.fAxisBottom.Max;
                            PadRange padRangeY = zoomable.GetPadRangeY(this);
                            if (padRangeY.IsValid)
                            {
                                val1_3 = Math.Min(val1_3, padRangeY.Min);
                                val1_4 = Math.Max(val1_4, padRangeY.Max);
                                flag2 = true;
                            }
                            this.fAxisBottom.Max = max;
                            this.fXMax = num;
                        }
                    }
                }
                foreach (Pad.ObjectViewer objectViewer in this.objectViewers)
                {
                    if (objectViewer.Viewer.IsZoomable)
                    {
                        PadRange padRangeX = objectViewer.Viewer.GetPadRangeX(objectViewer.Object, this);
                        if (padRangeX != null && padRangeX.IsValid)
                        {
                            val1_1 = Math.Min(val1_1, padRangeX.Min);
                            val1_2 = Math.Max(val1_2, padRangeX.Max);
                            flag1 = true;
                        }
                        PadRange padRangeY = objectViewer.Viewer.GetPadRangeY(objectViewer.Object, this);
                        if (padRangeY != null)
                        {
                            double max = this.fAxisBottom.Max;
                            double num = this.fXMax;
                            this.fAxisBottom.Max = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                            this.fXMax = this.fAxisBottom.Max;
                            if (padRangeY.IsValid)
                            {
                                val1_3 = Math.Min(val1_3, padRangeY.Min);
                                val1_4 = Math.Max(val1_4, padRangeY.Max);
                                if (Math.Round(val1_3, 6) == 0.0 && Math.Round(val1_4, 6) == 0.0)
                                {
                                    val1_3 = -1.0;
                                    val1_4 = 1.0;
                                }
                                flag2 = true;
                            }
                            this.fAxisBottom.Max = max;
                            this.fXMax = num;
                        }
                    }
                }
            }
            catch
            {
            }
            if (!flag2 && !flag3)
            {
                flag2 = true;
                val1_4 = 1.0;
                val1_3 = -1.0;
            }
            if (flag1)
                this.SetRangeX(val1_1 - (val1_2 - val1_1) / 20.0, val1_2 + (val1_2 - val1_1) / 20.0);
            if (flag2)
                this.SetRangeY(val1_3 - (val1_4 - val1_3) / 20.0, val1_4 + (val1_4 - val1_3) / 20.0);
            this.fGraphics = Graphics;
            this.TitleHeight = 0;
            this.AxisBottomHeight = 0;
            this.AxisTopHeight = 0;
            this.AxisRightWidth = 0;
            this.AxisLeftWidth = 0;
            if (this.fTitleEnabled)
            {
                switch (this.fTitle.Position)
                {
                    case ETitlePosition.Left:
                        this.TitleHeight = this.Title.Height + this.fTitleOffsetY;
                        break;
                    case ETitlePosition.Right:
                        this.TitleHeight = this.Title.Height + this.fTitleOffsetY;
                        break;
                    case ETitlePosition.Centre:
                        this.TitleHeight = this.Title.Height + this.fTitleOffsetY;
                        break;
                    case ETitlePosition.InsideLeft:
                        this.TitleHeight = 0;
                        break;
                    case ETitlePosition.InsideRight:
                        this.TitleHeight = 0;
                        break;
                    case ETitlePosition.InsideCentre:
                        this.TitleHeight = 0;
                        break;
                }
            }
            if (this.fAxisBottom.Enabled)
                this.AxisBottomHeight = this.fAxisBottom.Height;
            if (this.fAxisTop.Enabled)
                this.AxisTopHeight = this.fAxisTop.Height;
            if (this.fAxisRight.Enabled)
                this.AxisRightWidth = this.fAxisRight.Width;
            if (this.fAxisLeft.Enabled)
                this.AxisLeftWidth = this.fAxisLeft.Width;
            this.PaintAll(Graphics);
        }

        public void PaintAll(Graphics Graphics)
        {
            this.fGraphics = Graphics;
            this.fGraphics.Clip = new Region(new Rectangle(this.fX1, this.fY1, this.fWidth + 1, this.fHeight + 1));
            this.fGraphics.FillRectangle((Brush) new SolidBrush(this.fBackColor), this.fX1, this.fY1, this.fWidth, this.fHeight);
            if (this.fBorderEnabled)
            {
                int height = this.fHeight;
                int width = this.fWidth;
                int num1 = this.fChart.ClientRectangle.Height - this.fY1 - 1;
                int num2 = this.fChart.ClientRectangle.Width - this.fX1 - 1;
                if (this.fHeight > num1)
                    height = num1;
                if (this.fWidth > num2)
                    width = num2;
                this.fGraphics.DrawRectangle(new Pen(this.fBorderColor)
                {
                    Width = (float) this.fBorderWidth
                }, this.fX1, this.fY1, width, height);
            }
            this.fClientX = this.fX1 + this.AxisLeftWidth + this.fMarginLeft;
            this.fClientY = this.fY1 + this.TitleHeight + this.AxisTopHeight + this.fMarginTop;
            this.fClientWidth = this.fWidth - this.AxisLeftWidth - this.AxisRightWidth - this.fMarginLeft - this.fMarginRight;
            this.fClientHeight = this.fHeight - this.TitleHeight - this.AxisTopHeight - this.AxisBottomHeight - this.fMarginTop - this.fMarginBottom;
            if (this.fClientWidth != 0 && this.fClientHeight != 0)
                this.fGraphics.FillRectangle((Brush) new LinearGradientBrush(new RectangleF((float) this.fClientX, (float) this.fClientY, (float) this.fClientWidth, (float) this.fClientHeight), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(200, 200, 200), LinearGradientMode.Vertical), this.fClientX, this.fClientY, this.fClientWidth, this.fClientHeight);
            if (this.fAxisBottom.Enabled)
            {
                this.fAxisBottom.SetLocation((double) this.fClientX, (double) (this.fClientY + this.fClientHeight), (double) (this.fClientX + this.fClientWidth), (double) (this.fClientY + this.fClientHeight));
                this.fAxisBottom.Paint();
            }
            if (this.fAxisLeft.Enabled)
            {
                this.fGraphics.Clip = new Region(new Rectangle(this.fX1, this.fY1, this.fWidth, this.fHeight));
                this.fAxisLeft.SetLocation((double) this.fClientX, (double) this.fClientY, (double) this.fClientX, (double) (this.fClientY + this.fClientHeight));
                this.fAxisLeft.Paint();
            }
            if (this.fAxisTop.Enabled)
            {
                this.fAxisTop.SetLocation((double) this.fClientX, (double) this.fClientY, (double) (this.fClientX + this.fClientWidth), (double) this.fClientY);
                this.fAxisTop.Paint();
            }
            if (this.fAxisRight.Enabled)
            {
                this.fAxisRight.SetLocation((double) (this.fClientX + this.fClientWidth), (double) this.fClientY, (double) (this.fClientX + this.fClientWidth), (double) (this.fClientY + this.fClientHeight));
                this.fAxisRight.Paint();
            }
            this.fGraphics.Clip = new Region(new Rectangle(this.fClientX + 1, this.fClientY + 1, this.fClientWidth - 1, this.fClientHeight - 1));
            try
            {
                foreach (IDrawable drawable in this.fPrimitives)
                    drawable.Paint(this, this.XMin, this.XMin + this.CalculateRealQuantityOfTicks_Right(this.XMin, this.XMax), this.YMin, this.YMax);
            }
            catch
            {
            }
            foreach (Pad.ObjectViewer objectViewer in this.objectViewers)
                objectViewer.Viewer.Paint(objectViewer.Object, this);
            if (this.fOutlineEnabled)
                this.fGraphics.DrawRectangle(new Pen(Color.Green), this.fOutlineRectangle);
            if (this.fTitleEnabled)
            {
                switch (this.fTitle.Position)
                {
                    case ETitlePosition.Left:
                        this.fGraphics.Clip = new Region(new Rectangle(this.fX1, this.fY1, this.fWidth, this.fHeight));
                        this.fTitle.Y = this.fY1 + this.fMarginTop;
                        this.fTitle.X = this.fClientX + this.fTitleOffsetX;
                        break;
                    case ETitlePosition.Right:
                        this.fGraphics.Clip = new Region(new Rectangle(this.fX1, this.fY1, this.fWidth, this.fHeight));
                        this.fTitle.Y = this.fY1 + this.fMarginTop;
                        this.fTitle.X = this.fClientX + this.fClientWidth - this.fTitle.Width - this.fTitleOffsetX;
                        break;
                    case ETitlePosition.Centre:
                        this.fGraphics.Clip = new Region(new Rectangle(this.fX1, this.fY1, this.fWidth, this.fHeight));
                        this.fTitle.Y = this.fY1 + this.fMarginTop;
                        this.fTitle.X = this.fClientX + this.fClientWidth / 2 - this.fTitle.Width / 2 + this.fTitleOffsetX;
                        break;
                    case ETitlePosition.InsideLeft:
                        this.fTitle.Y = this.fClientY + this.fTitleOffsetY;
                        this.fTitle.X = this.fClientX + this.fTitleOffsetX;
                        this.fGraphics.FillRectangle((Brush) new SolidBrush(this.fForeColor), this.fTitle.X, this.fTitle.Y, this.fTitle.Width, this.fTitle.Height);
                        break;
                    case ETitlePosition.InsideRight:
                        this.fTitle.Y = this.fClientY + this.fTitleOffsetY;
                        this.fTitle.X = this.fClientX + this.fClientWidth - this.fTitle.Width - this.fTitleOffsetX;
                        this.fGraphics.FillRectangle((Brush) new SolidBrush(this.fForeColor), this.fTitle.X, this.fTitle.Y, this.fTitle.Width, this.fTitle.Height);
                        break;
                    case ETitlePosition.InsideCentre:
                        this.fTitle.Y = this.fClientY + this.fTitleOffsetY;
                        this.fTitle.X = this.fClientX + this.fClientWidth / 2 - this.fTitle.Width / 2 + this.fTitleOffsetX;
                        this.fGraphics.FillRectangle((Brush) new SolidBrush(this.fForeColor), this.fTitle.X, this.fTitle.Y, this.fTitle.Width, this.fTitle.Height);
                        break;
                }
                this.fTitle.Paint();
            }
            if (!this.fLegendEnabled)
                return;
            switch (this.fLegendPosition)
            {
                case ELegendPosition.TopRight:
                    this.fLegend.X = this.fClientX + this.fClientWidth - this.fLegendOffsetX - this.fLegend.Width;
                    this.fLegend.Y = this.fClientY + this.fLegendOffsetY;
                    break;
                case ELegendPosition.TopLeft:
                    this.fLegend.X = this.fClientX + this.fLegendOffsetX;
                    this.fLegend.Y = this.fClientY + this.fLegendOffsetY;
                    break;
                case ELegendPosition.BottomRight:
                    this.fLegend.X = this.fClientX + this.fClientWidth - this.fLegendOffsetX - this.fLegend.Width;
                    this.fLegend.Y = this.fClientY + this.fClientHeight - this.fLegendOffsetY - this.fLegend.Height;
                    break;
                case ELegendPosition.BottomLeft:
                    this.fLegend.X = this.fClientX + this.fLegendOffsetX;
                    this.fLegend.Y = this.fClientY + this.fClientHeight - this.fLegendOffsetY - this.fLegend.Height;
                    break;
            }
            this.fLegend.Paint();
        }

        public void DrawLine(Pen Pen, double X1, double Y1, double X2, double Y2, bool DoTransform)
        {
            if (DoTransform)
                this.fGraphics.DrawLine(Pen, this.ClientX(X1), this.ClientY(Y1), this.ClientX(X2), this.ClientY(Y2));
            else
                this.fGraphics.DrawLine(Pen, (int) X1, (int) Y1, (int) X2, (int) Y2);
        }

        public void DrawVerticalTick(Pen Pen, double X, double Y, int Length)
        {
        }

        public void DrawHorizontalTick(Pen Pen, double X, double Y, int Length)
        {
            try
            {
                this.fGraphics.DrawLine(Pen, (int) X, this.ClientY(Y), (int) X + Length, this.ClientY(Y));
            }
            catch
            {
            }
        }

        public void DrawVerticalGrid(Pen Pen, double X)
        {
            this.fGraphics.DrawLine(Pen, this.ClientX(X), this.fClientY, this.ClientX(X), this.fClientY + this.fClientHeight);
        }

        public void DrawHorizontalGrid(Pen Pen, double Y)
        {
            this.fGraphics.DrawLine(Pen, this.fClientX, this.ClientY(Y), this.fClientX + this.fClientWidth, this.ClientY(Y));
        }

        public void DrawLine(Pen Pen, double X1, double Y1, double X2, double Y2)
        {
            this.DrawLine(Pen, X1, Y1, X2, Y2, true);
        }

        public void DrawRectangle(Pen Pen, double X, double Y, int Width, int Height)
        {
            this.fGraphics.DrawRectangle(Pen, this.ClientX(X), this.ClientY(Y), Width, Height);
        }

        public void DrawEllipse(Pen Pen, double X, double Y, int Width, int Height)
        {
            this.fGraphics.DrawEllipse(Pen, this.ClientX(X), this.ClientY(Y), Width, Height);
        }

        public void DrawBeziers(Pen Pen, PointF[] Points)
        {
            Point[] points = new Point[Points.Length];
            for (int index = 0; index < Points.Length; ++index)
            {
                PointF pointF = Points[index];
                points[index] = new Point(this.ClientX((double) pointF.X), this.ClientY((double) pointF.Y));
            }
            this.fGraphics.DrawBeziers(Pen, points);
        }

        public void DrawText(string Text, Font Font, Brush Brush, int X, int Y)
        {
            this.fGraphics.DrawString(Text, Font, Brush, (float) X, (float) Y);
        }

        private bool IsInsideClient(int X, int Y)
        {
            if (X > this.fClientX && X < this.fClientX + this.fClientWidth && Y > this.fClientY)
                return Y < this.fClientY + this.fClientHeight;
            else
                return false;
        }

        public virtual void MouseMove(MouseEventArgs Event)
        {
            try
            {
                if (!this.fMouseDown)
                {
                    double num1 = (this.fXMax - this.fXMin) / 100.0;
                    double num2 = (this.fYMax - this.fYMin) / 100.0;
                    double X = this.WorldX(Event.X);
                    double Y = this.WorldY(Event.Y);
                    string str = "";
                    this.fSelectedPrimitive = (IDrawable) null;
                    this.fSelectedPrimitiveDistance = (TDistance) null;
                    this.fOnPrimitive = false;
                    foreach (IDrawable drawable in this.fPrimitives)
                    {
                        TDistance tdistance = drawable.Distance(X, Y);
                        if (tdistance != null && tdistance.dX < num1 && tdistance.dY < num2)
                        {
                            if (drawable.ToolTipEnabled)
                            {
                                if (str != "")
                                    str = str + "\n\n";
                                str = str + tdistance.ToolTipText;
                            }
                            this.fOnPrimitive = true;
                            this.fSelectedPrimitive = drawable;
                            this.fSelectedPrimitiveDistance = tdistance;
                        }
                    }
                }
                if (this.fMouseMovePrimitiveEnabled && this.fMouseDown && (this.fMouseDownButton == MouseButtons.Left && this.fOnPrimitive) && this.fSelectedPrimitive is IMovable)
                {
                    double num1 = this.WorldX(Event.X);
                    double num2 = this.WorldY(Event.Y);
                    ((IMovable) this.fSelectedPrimitive).Move(this.fSelectedPrimitiveDistance.X, this.fSelectedPrimitiveDistance.Y, num1 - this.fSelectedPrimitiveDistance.X, num2 - this.fSelectedPrimitiveDistance.Y);
                    this.fSelectedPrimitiveDistance.X = num1;
                    this.fSelectedPrimitiveDistance.Y = num2;
                    this.fOnPrimitive = true;
                    this.Update();
                }
                if (this.fMouseZoomEnabled && this.fMouseDown && (this.fMouseDownButton == MouseButtons.Left && !this.fOnPrimitive))
                {
                    int num1 = Math.Abs(this.fMouseDownX - Event.X);
                    int num2 = Math.Abs(this.fMouseDownY - Event.Y);
                    int num3 = this.fMouseDownX >= Event.X ? Event.X : this.fMouseDownX;
                    int num4 = this.fMouseDownY >= Event.Y ? Event.Y : this.fMouseDownY;
                    this.fOutlineRectangle.X = num3;
                    this.fOutlineRectangle.Y = num4;
                    this.fOutlineRectangle.Width = num1;
                    this.fOutlineRectangle.Height = num2;
                    this.Update();
                }
                if (this.fMouseMoveContentEnabled && this.fMouseDown && this.fMouseDownButton == MouseButtons.Right)
                {
                    double num1 = (double) (this.fMouseDownX - Event.X) / (double) this.fClientWidth * (this.XMax - this.XMin);
                    double num2 = this.WorldY(this.fMouseDownY) - this.WorldY(Event.Y);
                    double num3 = num1 <= 0.0 ? this.CalculateRealQuantityOfTicks_Left(this.fAxisBottom.Min, this.fAxisBottom.Min + num1) : this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Min + num1);
                    this.fMouseDownX = Event.X;
                    this.fMouseDownY = Event.Y;
                    this.fAxisBottom.SetRange(this.fAxisBottom.Min + num3, this.fAxisBottom.Max + num3);
                    this.fAxisTop.SetRange(this.fAxisTop.Min + num3, this.fAxisTop.Max + num3);
                    this.fAxisLeft.SetRange(this.fAxisLeft.Min + num2, this.fAxisLeft.Max + num2);
                    this.fAxisRight.SetRange(this.fAxisRight.Min + num2, this.fAxisRight.Max + num2);
                    this.fAxisBottom.Zoomed = true;
                    this.fAxisTop.Zoomed = true;
                    this.fAxisLeft.Zoomed = true;
                    this.fAxisRight.Zoomed = true;
                    if (!this.fChart.GroupZoomEnabled)
                        this.Update();
                    this.EmitZoom(true);
                }
                else
                {
                    this.fOnAxis = false;
                    this.fAxisLeft.MouseMove(Event);
                    this.fAxisBottom.MouseMove(Event);
                    if (this.fAxisLeft.X1 - 10.0 <= (double) Event.X && this.fAxisLeft.X1 >= (double) Event.X && (this.fAxisLeft.Y1 <= (double) Event.Y && this.fAxisLeft.Y2 >= (double) Event.Y))
                        this.fOnAxis = true;
                    if (this.fAxisBottom.X1 <= (double) Event.X && this.fAxisBottom.X2 >= (double) Event.X && (this.fAxisBottom.Y1 <= (double) Event.Y && this.fAxisBottom.Y1 + 10.0 >= (double) Event.Y))
                        this.fOnAxis = true;
                    if (this.fOnAxis || this.fOnPrimitive)
                    {
                        #if GTK
                        #else
                        if (!(Cursor.Current != Cursors.Hand))
                            return;
                        Cursor.Current = Cursors.Hand;
                        #endif
                    }
                    else
                    {
                        #if GTK
                        #else
                        if (!(Cursor.Current != Cursors.Default))
                            return;
                        Cursor.Current = Cursors.Default;
                        #endif
                    }
                }
            }
            catch
            {
            }
        }

        public virtual void MouseWheel(MouseEventArgs Event)
        {
            if (!this.fMouseWheelEnabled)
                return;
            double min = this.fAxisBottom.Min;
            double max = this.fAxisBottom.Max;
            switch (this.fMouseWheelMode)
            {
                case EMouseWheelMode.MoveX:
                    double num1 = (double) Event.Delta / 120.0 * (this.fAxisBottom.Max - this.fAxisBottom.Min) * this.fMouseWheelSensitivity;
                    double num2 = num1 <= 0.0 ? this.CalculateRealQuantityOfTicks_Left(this.fAxisBottom.Min, this.fAxisBottom.Min + num1) : this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Min + num1);
                    this.fAxisBottom.SetRange(this.fAxisBottom.Min + num2, this.fAxisBottom.Max + num2);
                    this.fAxisTop.SetRange(this.fAxisTop.Min + num2, this.fAxisTop.Max + num2);
                    this.fAxisBottom.Zoomed = true;
                    this.fAxisTop.Zoomed = true;
                    this.EmitZoom(true);
                    break;
                case EMouseWheelMode.MoveY:
                    double num3 = (double) Event.Delta / 120.0 * (this.fYMax - this.fYMin) * this.fMouseWheelSensitivity;
                    this.fAxisLeft.SetRange(this.fAxisLeft.Min + num3, this.fAxisLeft.Max + num3);
                    this.fAxisRight.SetRange(this.fAxisRight.Min + num3, this.fAxisRight.Max + num3);
                    this.fAxisLeft.Zoomed = true;
                    this.fAxisRight.Zoomed = true;
                    this.EmitZoom(true);
                    break;
                case EMouseWheelMode.ZoomX:
                    double num4 = (double) Event.Delta / 120.0 * (this.fAxisBottom.Max - this.fAxisBottom.Min) * this.fMouseWheelSensitivity;
                    double num5 = num4 <= 0.0 ? this.CalculateRealQuantityOfTicks_Left(this.fAxisBottom.Min, this.fAxisBottom.Min + num4) : this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Min + num4);
                    double num6 = num5 - num4;
                    this.fAxisBottom.SetRange(this.fAxisBottom.Min + num5, this.fAxisBottom.Max + num6);
                    this.fAxisTop.SetRange(this.fAxisTop.Min + num5, this.fAxisTop.Max + num6);
                    this.fAxisBottom.Zoomed = true;
                    this.fAxisTop.Zoomed = true;
                    this.EmitZoom(true);
                    break;
                case EMouseWheelMode.ZoomY:
                    double num7 = (double) Event.Delta / 120.0 * (this.fYMax - this.fYMin) * this.fMouseWheelSensitivity;
                    this.fAxisLeft.SetRange(this.fAxisLeft.Min + num7, this.fAxisLeft.Max);
                    this.fAxisRight.SetRange(this.fAxisRight.Min + num7, this.fAxisRight.Max);
                    this.fAxisLeft.Zoomed = true;
                    this.fAxisRight.Zoomed = true;
                    this.EmitZoom(true);
                    break;
                case EMouseWheelMode.Zoom:
                    double num8 = this.fAxisBottom.Min + this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Max);
                    double num9 = (double) Event.Delta / 120.0 * (num8 - this.fAxisBottom.Min) * this.fMouseWheelSensitivity;
                    double num10 = (double) Event.Delta / 120.0 * (this.fYMax - this.fYMin) * this.fMouseWheelSensitivity;
                    double num11 = this.WorldX(Event.X);
                    double num12 = this.WorldY(Event.Y);
                    double num13 = (num11 - this.fAxisBottom.Min) / (num8 - this.fAxisBottom.Min) * num9;
                    double num14 = (num8 - num11) / (num8 - this.fAxisBottom.Min) * num9;
                    double num15 = (num12 - this.fYMin) / (this.fYMax - this.fYMin) * num10;
                    double num16 = (this.fYMax - num12) / (this.fYMax - this.fYMin) * num10;
                    double num17 = num13 <= 0.0 ? this.CalculateRealQuantityOfTicks_Left(this.fAxisBottom.Min, this.fAxisBottom.Min + num13) : this.CalculateRealQuantityOfTicks_Right(this.fAxisBottom.Min, this.fAxisBottom.Min + num13);
                    double num18 = -num17 + num13 + num14;
                    this.fAxisBottom.SetRange(this.fAxisBottom.Min + num17, this.fAxisBottom.Max - num18);
                    this.fAxisTop.SetRange(this.fAxisTop.Min + num17, this.fAxisTop.Max - num18);
                    this.fAxisLeft.SetRange(this.fAxisLeft.Min + num15, this.fAxisLeft.Max - num16);
                    this.fAxisRight.SetRange(this.fAxisRight.Min + num15, this.fAxisRight.Max - num16);
                    this.fAxisBottom.Zoomed = true;
                    this.fAxisTop.Zoomed = true;
                    this.fAxisLeft.Zoomed = true;
                    this.fAxisRight.Zoomed = true;
                    this.EmitZoom(true);
                    break;
            }
            if (this.fChart.GroupZoomEnabled)
                return;
            this.Update();
        }

        public virtual void MouseDown(MouseEventArgs Event)
        {
            if (this.IsInsideClient(Event.X, Event.Y))
            {
                this.fMouseDown = true;
                this.fMouseDownX = Event.X;
                this.fMouseDownY = Event.Y;
                this.fMouseDownButton = Event.Button;
                if (this.fMouseZoomEnabled && this.fMouseDownButton == MouseButtons.Left && this.fSelectedPrimitive == null)
                    this.fOutlineEnabled = true;
                if (this.fMouseContextMenuEnabled && this.fMouseDownButton == MouseButtons.Right && this.fOnPrimitive)
                {
                    this.InitContextMenu();
                    #if !GTK
                    this.DeleteMenuItem.Text = "Delete " + this.fSelectedPrimitive.GetType().Name;
                    #endif
                }
            }
            this.fAxisLeft.MouseDown(Event);
            this.fAxisBottom.MouseDown(Event);
        }

        public virtual void MouseUp(MouseEventArgs Event)
        {
            if (this.fMouseZoomEnabled && this.fMouseDown && (this.fMouseDownButton == MouseButtons.Left && !this.fOnPrimitive))
            {
                this.fOutlineEnabled = false;
                if (Math.Abs(this.fMouseDownX - Event.X) > 2 && Math.Abs(this.fMouseDownY - Event.Y) > 2)
                {
                    double num1 = this.WorldX(this.fMouseDownX);
                    double num2 = this.WorldX(Event.X);
                    double num3 = this.WorldY(this.fMouseDownY);
                    double num4 = this.WorldY(Event.Y);
                    double num5;
                    double Y;
                    if (num1 < num2)
                    {
                        num5 = num1;
                        Y = num2;
                    }
                    else
                    {
                        num5 = num2;
                        Y = num1;
                    }
                    double Min;
                    double Max1;
                    if (num3 < num4)
                    {
                        Min = num3;
                        Max1 = num4;
                    }
                    else
                    {
                        Min = num4;
                        Max1 = num3;
                    }
                    double Max2 = Y - this.CalculateNotInSessionTicks(num5, Y);
                    this.fAxisBottom.SetRange(num5, Max2);
                    this.fAxisTop.SetRange(num5, Max2);
                    this.fAxisLeft.SetRange(Min, Max1);
                    this.fAxisRight.SetRange(Min, Max1);
                    this.fAxisBottom.Zoomed = true;
                    this.fAxisTop.Zoomed = true;
                    this.fAxisLeft.Zoomed = true;
                    this.fAxisRight.Zoomed = true;
                    if (!this.fChart.GroupZoomEnabled)
                        this.Update();
                    this.EmitZoom(true);
                }
                this.fMouseDown = false;
            }
            else
            {
                this.fAxisLeft.MouseUp(Event);
                this.fAxisBottom.MouseUp(Event);
                this.fMouseDown = false;
            }
        }

        public virtual void DoubleClick(int X, int Y)
        {
            if (this.IsInsideClient(X, Y))
            {
                if (this.fOnPrimitive)
                {
                    if (!this.fMousePrimitivePropertiesEnabled)
                        return;
                    int num = (int) new PadProperyForm((object) this.fSelectedPrimitive, this).ShowDialog();
                }
                else
                {
                    if (!this.fMouseUnzoomEnabled || !this.AxisLeft.Zoomed && !this.AxisBottom.Zoomed)
                        return;
                    this.fOutlineEnabled = false;
                    if (!this.fChart.GroupZoomEnabled)
                        this.UnZoom();
                    this.EmitZoom(false);
                }
            }
            else
            {
                if (!this.fMousePadPropertiesEnabled)
                    return;
                int num = (int) new PadProperyForm((object) this, this).ShowDialog();
            }
        }

        public static void EmitNewTick(DateTime datetime)
        {
            if (Pad.NewTick == null)
                return;
            Pad.NewTick((object) null, new NewTickEventArgs(datetime));
        }

        private void OnNewTick(object sender, NewTickEventArgs EventArgs)
        {
            if (!this.fMonitored)
                return;
            int num1 = EventArgs.DateTime.Hour * 60 * 60 + EventArgs.DateTime.Minute * 60 + EventArgs.DateTime.Second;
            if (num1 - this.fLastTickTime < this.fUpdateInterval)
                return;
            DateTime dateTime = EventArgs.DateTime;
            double XMin = (double) dateTime.AddSeconds((double) -this.fWindowSize).Ticks;
            double num2 = (double) dateTime.Ticks;
            this.SetRangeX(XMin, num2 + (num2 - XMin) / 20.0);
            if ((DateTime.Now.Ticks - this.fLastUpdateDateTime.Ticks) / 1000000L > 1L)
            {
                if (!this.fChart.GroupZoomEnabled)
                    this.Update();
                this.EmitZoom(true);
                this.fLastUpdateDateTime = DateTime.Now;
            }
            this.fLastTickTime = num1;
        }

        public void EmitZoom(bool zoom)
        {
            if (this.Zoom == null)
                return;
            this.Zoom((object) null, new ZoomEventArgs(this.XMin, this.XMax, this.YMin, this.YMax, zoom));
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            this.fPrimitives.Remove((object) this.fSelectedPrimitive);
            this.Update();
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int) new PadProperyForm((object) this.fSelectedPrimitive, this).ShowDialog();
        }

        private class ObjectViewer
        {
            public object Object { get; set; }

            public Viewer Viewer { get; set; }

            public ObjectViewer(object obj, Viewer viewer)
            {
                this.Object = obj;
                this.Viewer = viewer;
            }
        }

        [Serializable]
        private class TFeatures3D
        {
            private Pad Pad;
            private Pad.TFeatures3D.TAxes2D Axes2D;
            public Axis[] Axes;
            private bool fActive;
            public object View;

            public bool Active
            {
                get
                {
                    return this.fActive;
                }
                set
                {
                    this.fActive = value;
                    if (value)
                    {
                        this.Axes2D.SetFor3D();
                        this.Pad.ForeColor = this.Pad.BackColor;
                        this.Pad.AntiAliasingEnabled = true;
                    }
                    else
                        this.Axes2D.Restore();
                }
            }

            public TFeatures3D(Pad Pad)
            {
                this.Pad = Pad;
                this.Axes2D = new Pad.TFeatures3D.TAxes2D(Pad);
                this.Axes = new Axis[3]
                {
                    new Axis(Pad),
                    new Axis(Pad),
                    new Axis(Pad)
                };
                for (int index = 0; index < this.Axes.Length; ++index)
                {
                    this.Axes[index].Max = 1.0;
                    this.Axes[index].Min = 0.0;
                }
            }

            public void SetRangeX(double XMin, double XMax)
            {
                this.Axes[0].SetRange(XMin, XMax);
            }

            public void SetRangeY(double YMin, double YMax)
            {
                this.Axes[1].SetRange(YMin, YMax);
            }

            public void SetRangeZ(double ZMin, double ZMax)
            {
                this.Axes[2].SetRange(ZMin, ZMax);
            }

            public void SetRange(double XMin, double XMax, double YMin, double YMax)
            {
                this.SetRangeX(XMin, XMax);
                this.SetRangeY(YMin, YMax);
            }

            [Serializable]
            private class TAxes2D
            {
                private Pad Pad;
                private bool Saved;
                private Axis Top;
                private Axis Bottom;
                private Axis Left;
                private Axis Right;

                public TAxes2D(Pad Pad)
                {
                    this.Pad = Pad;
                    this.Top = new Axis(Pad);
                    this.Bottom = new Axis(Pad);
                    this.Left = new Axis(Pad);
                    this.Right = new Axis(Pad);
                }

                private void Copy(Axis dst, Axis src)
                {
                    dst.LabelEnabled = src.LabelEnabled;
                    dst.MajorTicksEnabled = src.MajorTicksEnabled;
                    dst.MinorTicksEnabled = src.MinorTicksEnabled;
                    dst.GridEnabled = src.GridEnabled;
                    dst.MinorGridEnabled = src.MinorGridEnabled;
                    dst.SetRange(src.Min, src.Max);
                    dst.Enabled = src.Enabled;
                }

                private void SetFor3D(Axis a)
                {
                    a.LabelEnabled = false;
                    a.MajorTicksEnabled = false;
                    a.MinorTicksEnabled = false;
                    a.GridEnabled = false;
                    a.MinorGridEnabled = false;
                    a.SetRange(0.0, 1.0);
                    a.Enabled = false;
                }

                public void Save()
                {
                    this.Copy(this.Top, this.Pad.fAxisTop);
                    this.Copy(this.Bottom, this.Pad.fAxisBottom);
                    this.Copy(this.Left, this.Pad.fAxisLeft);
                    this.Copy(this.Right, this.Pad.fAxisRight);
                    this.Saved = true;
                }

                public void SetFor3D()
                {
                    this.Save();
                    this.SetFor3D(this.Pad.AxisTop);
                    this.SetFor3D(this.Pad.AxisBottom);
                    this.SetFor3D(this.Pad.AxisLeft);
                    this.SetFor3D(this.Pad.AxisRight);
                }

                public void Restore()
                {
                    if (!this.Saved)
                        return;
                    this.Copy(this.Pad.fAxisTop, this.Top);
                    this.Copy(this.Pad.fAxisBottom, this.Bottom);
                    this.Copy(this.Pad.fAxisLeft, this.Left);
                    this.Copy(this.Pad.fAxisRight, this.Right);
                }
            }
        }
    }
}
