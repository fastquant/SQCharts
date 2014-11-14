using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace SmartQuant.Charting
{
    [Serializable]
    public class Graph : IDrawable, IZoomable, IMovable
    {
        private EMarkerStyle markerStyle;
        private int markerSize;
        private Color markerColor;

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
        public EGraphStyle Style  { get; set; }

        [Category("Style")]
        [Description("")]
        public EGraphMoveStyle MoveStyle { get; set; }

        [Description("")]
        [Category("Marker")]
        public bool MarkerEnabled { get; set; }

        [Description("")]
        [Category("Marker")]
        public EMarkerStyle MarkerStyle
        {
            get
            {
                return this.markerStyle;
            }
            set
            {
                this.markerStyle = value;
                foreach (TMarker marker in Points)
                    marker.Style = this.markerStyle;
            }
        }

        [Category("Marker")]
        [Description("")]
        public int MarkerSize
        {
            get
            {
                return this.markerSize;
            }
            set
            {
                this.markerSize = value;
                foreach (TMarker marker in Points)
                    marker.Size = this.markerSize;
            }
        }

        [Description("")]
        [Category("Marker")]
        public Color MarkerColor
        {
            get
            {
                return this.markerColor;
            }
            set
            {
                this.markerColor = value;
                foreach (TMarker marker in Points)
                    marker.Color = this.markerColor;
            }
        }

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

        public Graph()
            : this(null, null)
        {
        }

        public Graph(string name)
            : this(name, null)
        {
        }

        public Graph(string name, string title)
        {
            Name = name;
            Title = title;
            Style = EGraphStyle.Line;
            MoveStyle = EGraphMoveStyle.Point;
            Points = new ArrayList();
            MinX = double.MaxValue;
            MinY = double.MaxValue;
            MaxX = double.MinValue;
            MaxY = double.MinValue;
            MarkerEnabled = true;
            this.markerStyle = EMarkerStyle.Rectangle;
            this.markerSize = 5;
            this.markerColor = Color.Black;
            LineEnabled = true;
            LineDashStyle = DashStyle.Solid;
            LineColor = Color.Black;
            BarWidth = 20;
            ToolTipEnabled = true;
            ToolTipFormat = "{0}\nX = {2:F2} Y = {3:F2}";
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
            Add(x, y, this.markerColor);
        }

        public void Add(double x, double y, Color color)
        {
            Points.Add(new TMarker(x, y) { Style = MarkerStyle, Size = MarkerSize, Color = color });
            MinMax(x, y);
        }

        public void Add(double x, double y, string text)
        {
            Add(x, y, text, MarkerColor);
        }

        public void Add(double x, double y, string text, Color markerColor)
        {
            TLabel tlabel = new TLabel(text, x, y, markerColor);
            tlabel.Style = this.markerStyle;
            tlabel.Size = this.markerSize;
            Points.Add(tlabel);
            MinMax(x, y);
        }

        public void Add(double x, double y, string text, Color markerColor, Color textColor)
        {
            TLabel tlabel = new TLabel(text, x, y, markerColor, textColor);
            tlabel.Style = this.markerStyle;
            tlabel.Size = this.markerSize;
            Points.Add(tlabel);
            MinMax(x, y);
        }

        public void Add(TMarker marker)
        {
            Points.Add(marker);
            MinMax(marker.X, marker.Y);
        }

        public void Add(TLabel label)
        {
            Points.Add(label);
            MinMax(label.X, label.Y);
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
            if (Chart.Pad == null)
            {
                var canvas = new Canvas("Canvas", "Canvas");
            }
            Chart.Pad.Add(this);
            Chart.Pad.Title.Add(Name, LineColor);
            Chart.Pad.Legend.Add(Name, LineColor);
            if (option.ToLower().IndexOf("s") >= 0)
                return;
            Chart.Pad.SetRange(MinX - (MaxX - MinX) / 10.0, MaxX + (MaxX - MinX) / 10.0, MinY - (MaxY - MinY) / 10.0, MaxY + (MaxY - MinY) / 10.0);
        }

        public virtual void Draw()
        {
            Draw("");
        }

        public virtual void Paint(Pad pad, double xMin, double xMax, double yMin, double yMax)
        {
            if (Style == EGraphStyle.Line && LineEnabled)
            {
                Pen Pen = new Pen(LineColor);
                Pen.DashStyle = LineDashStyle;
                double X1 = 0.0;
                double Y1 = 0.0;
                bool flag = true;
                foreach (TMarker tmarker in Points)
                {
                    if (!flag)
                        pad.DrawLine(Pen, X1, Y1, tmarker.X, tmarker.Y);
                    else
                        flag = false;
                    X1 = tmarker.X;
                    Y1 = tmarker.Y;
                }
            }
            if ((Style == EGraphStyle.Line || Style == EGraphStyle.Scatter) && MarkerEnabled)
            {
                foreach (TMarker tmarker in Points)
                    tmarker.Paint(pad, xMin, xMax, yMin, yMax);
            }
            if (Style != EGraphStyle.Bar)
                return;
            foreach (TMarker tmarker in Points)
            {
                if (tmarker.Y > 0.0)
                    pad.Graphics.FillRectangle(new SolidBrush(Color.Black), pad.ClientX(tmarker.X) - BarWidth / 2, pad.ClientY(tmarker.Y), BarWidth, pad.ClientY(0.0) - pad.ClientY(tmarker.Y));
                else
                    pad.Graphics.FillRectangle(new SolidBrush(Color.Black), pad.ClientX(tmarker.X) - BarWidth / 2, pad.ClientY(0.0), BarWidth, pad.ClientY(tmarker.Y) - pad.ClientY(0.0));
            }
        }

        public TDistance Distance(double x, double y)
        {
            var d = new TDistance();
            foreach (TMarker marker in Points)
            {
                var d2 = marker.Distance(x, y);
                if (d2.dX < d.dX && d2.dY < d.dY)
                    d = d2;
            }
            if (d != null)
                d.ToolTipText = string.Format(ToolTipFormat, Name, Title, d.X, d.Y);
            return d;
        }

        public void Move(double x, double y, double dX, double dY)
        {
            switch (MoveStyle)
            {
                case EGraphMoveStyle.Graph:
                    IEnumerator enumerator1 = Points.GetEnumerator();
                    try
                    {
                        while (enumerator1.MoveNext())
                        {
                            TMarker tmarker = (TMarker)enumerator1.Current;
                            tmarker.X += dX;
                            tmarker.Y += dY;
                        }
                        break;
                    }
                    finally
                    {
                        IDisposable disposable = enumerator1 as IDisposable;
                        if (disposable != null)
                            disposable.Dispose();
                    }
                case EGraphMoveStyle.Point:
                    IEnumerator enumerator2 = Points.GetEnumerator();
                    try
                    {
                        while (enumerator2.MoveNext())
                        {
                            TMarker tmarker = (TMarker)enumerator2.Current;
                            if (tmarker.X == x && tmarker.Y == y)
                            {
                                tmarker.X += dX;
                                tmarker.Y += dY;
                                break;
                            }
                        }
                        break;
                    }
                    finally
                    {
                        IDisposable disposable = enumerator2 as IDisposable;
                        if (disposable != null)
                            disposable.Dispose();
                    }
            }
        }
    }
}
