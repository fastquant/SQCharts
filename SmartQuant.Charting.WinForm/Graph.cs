using System;
using System.ComponentModel;
using System.Collections;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Graph : IDrawable, IZoomable, IMovable
    {
//        private string fName;
//        private string fTitle;
//        private ArrayList fPoints;
//        private EGraphStyle fStyle;
//        private EGraphMoveStyle fMoveStyle;
//        private bool fMarkerEnabled;
//        private EMarkerStyle fMarkerStyle;
//        private int fMarkerSize;
//        private Color fMarkerColor;
//        private bool fLineEnabled;
//        private System.Drawing.Drawing2D.DashStyle fLineDashStyle;
//        private Color fLineColor;
//        private double fXMin;
//        private double fXMax;
//        private double fYMin;
//        private double fMaxY;
//        private int fBarWidth;
//        private bool fToolTipEnabled;
//        private string fToolTipFormat;

        public string Name { get; set; }

        public string Title { get; set; }

        [Description("")]
        [Category("ToolTip")]
        public bool ToolTipEnabled { get; set; }
        [Description("")]
        [Category("ToolTip")]
        public string ToolTipFormat { get; set; }

        [Description("")]
        [Category("Style")]
        public EGraphStyle Style { get; set; }

        [Category("Style")]
        [Description("")]
        public EGraphMoveStyle MoveStyle { get; set; }

        [Description("")]
        [Category("Marker")]
        public bool MarkerEnabled { get; set; }

//        [Description("")]
//        [Category("Marker")]
//        public EMarkerStyle MarkerStyle
//        {
//            get
//            {
//                return this.fMarkerStyle;
//            }
//            set
//            {
//                this.fMarkerStyle = value;
//                foreach (TMarker tmarker in this.fPoints)
//                    tmarker.Style = this.fMarkerStyle;
//            }
//        }
//
//        [Category("Marker")]
//        [Description("")]
//        public int MarkerSize
//        {
//            get
//            {
//                return this.fMarkerSize;
//            }
//            set
//            {
//                this.fMarkerSize = value;
//                foreach (TMarker tmarker in this.fPoints)
//                    tmarker.Size = this.fMarkerSize;
//            }
//        }
//
//        [Description("")]
//        [Category("Marker")]
//        public Color MarkerColor
//        {
//            get
//            {
//                return this.fMarkerColor;
//            }
//            set
//            {
//                this.fMarkerColor = value;
//                foreach (TMarker tmarker in this.fPoints)
//                    tmarker.Color = this.fMarkerColor;
//            }
//        }

        [Description("")]
        [Category("Bar")]
        public int BarWidth { get; set; }

        [Category("Line")]
        [Description("")]
        public bool LineEnabled { get; set; }
        [Category("Line")]
        [Description("")]
        public DashStyle LineDashStyle { get; set; }

        [Description("")]
        [Category("Line")]
        public Color LineColor { get; set; }

        [Browsable(false)]
        public ArrayList Points { get; private set; }
        [Browsable(false)]
        public double MinX { get; private set; }

        [Browsable(false)]
        public double MinY { get; private set; }

        [Browsable(false)]
        public double MaxX { get; private set; }

        [Browsable(false)]
        public double MaxY { get; private set; }

        public Graph() : this(null)
        {
        }

        public Graph(string name) : this(name, null)
        {
        }

        public Graph(string name, string title)
        {
            Name = name;
            Title = title;
        }

        private void MinMax(double x, double y)
        {
            MinX = Math.Min(MinX, x);
            MinY = Math.Min(MinY, y);
            MaxX = Math.Max(MaxX, x);
            MaxY = Math.Max(MaxY, y);
        }

        public void Add(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void Add(double x, double y, Color color)
        {
            throw new NotImplementedException();
        }

        public void Add(double x, double y, string text)
        {
            throw new NotImplementedException();
        }

        public void Add(double x, double y, string text, Color markerColor)
        {
            throw new NotImplementedException();
        }

        public void Add(double x, double y, string text, Color markerColor, Color textColor)
        {   
            throw new NotImplementedException();
        }

        public void Add(TMarker marker)
        {
            throw new NotImplementedException();
        }

        public void Add(TLabel label)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsPadRangeX()
        {
            return false;
        }

        public virtual bool IsPadRangeY()
        {
            return false;
        }

        public virtual PadRange GetPadRangeX(Pad pad)
        {
            return null;
        }

        public virtual PadRange GetPadRangeY(Pad pad)
        {
            return null;
        }

        public virtual void Draw(string option)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw()
        {
            Draw("");
        }

        public virtual void Paint(Pad pad, double xMin, double xMax, double yMin, double yMax)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void Move(double x, double y, double dX, double dY)
        {
            throw new NotImplementedException();
        }
    }
}

