// Decompiled with JetBrains decompiler
// Type: SmartQuant.Charting.Graph
// Assembly: SmartQuant.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=23953e483e363d68
// MVID: F3B55EE9-4DBA-4875-B18A-7BD8DFCF4D88
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.Charting.dll

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
    private string fName;
    private string fTitle;
    private ArrayList fPoints;
    private EGraphStyle fStyle;
    private EGraphMoveStyle fMoveStyle;
    private bool fMarkerEnabled;
    private EMarkerStyle fMarkerStyle;
    private int fMarkerSize;
    private Color fMarkerColor;
    private bool fLineEnabled;
    private DashStyle fLineDashStyle;
    private Color fLineColor;
    private double fXMin;
    private double fXMax;
    private double fYMin;
    private double fMaxY;
    private int fBarWidth;
    private bool fToolTipEnabled;
    private string fToolTipFormat;

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

    public string Title
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
    [Category("ToolTip")]
    public bool ToolTipEnabled
    {
      get
      {
        return this.fToolTipEnabled;
      }
      set
      {
        this.fToolTipEnabled = value;
      }
    }

    [Description("")]
    [Category("ToolTip")]
    public string ToolTipFormat
    {
      get
      {
        return this.fToolTipFormat;
      }
      set
      {
        this.fToolTipFormat = value;
      }
    }

    [Description("")]
    [Category("Style")]
    public EGraphStyle Style
    {
      get
      {
        return this.fStyle;
      }
      set
      {
        this.fStyle = value;
      }
    }

    [Category("Style")]
    [Description("")]
    public EGraphMoveStyle MoveStyle
    {
      get
      {
        return this.fMoveStyle;
      }
      set
      {
        this.fMoveStyle = value;
      }
    }

    [Description("")]
    [Category("Marker")]
    public bool MarkerEnabled
    {
      get
      {
        return this.fMarkerEnabled;
      }
      set
      {
        this.fMarkerEnabled = value;
      }
    }

    [Description("")]
    [Category("Marker")]
    public EMarkerStyle MarkerStyle
    {
      get
      {
        return this.fMarkerStyle;
      }
      set
      {
        this.fMarkerStyle = value;
        foreach (TMarker tmarker in this.fPoints)
          tmarker.Style = this.fMarkerStyle;
      }
    }

    [Category("Marker")]
    [Description("")]
    public int MarkerSize
    {
      get
      {
        return this.fMarkerSize;
      }
      set
      {
        this.fMarkerSize = value;
        foreach (TMarker tmarker in this.fPoints)
          tmarker.Size = this.fMarkerSize;
      }
    }

    [Description("")]
    [Category("Marker")]
    public Color MarkerColor
    {
      get
      {
        return this.fMarkerColor;
      }
      set
      {
        this.fMarkerColor = value;
        foreach (TMarker tmarker in this.fPoints)
          tmarker.Color = this.fMarkerColor;
      }
    }

    [Description("")]
    [Category("Bar")]
    public int BarWidth
    {
      get
      {
        return this.fBarWidth;
      }
      set
      {
        this.fBarWidth = value;
      }
    }

    [Category("Line")]
    [Description("")]
    public bool LineEnabled
    {
      get
      {
        return this.fLineEnabled;
      }
      set
      {
        this.fLineEnabled = value;
      }
    }

    [Category("Line")]
    [Description("")]
    public DashStyle LineDashStyle
    {
      get
      {
        return this.fLineDashStyle;
      }
      set
      {
        this.fLineDashStyle = value;
      }
    }

    [Description("")]
    [Category("Line")]
    public Color LineColor
    {
      get
      {
        return this.fLineColor;
      }
      set
      {
        this.fLineColor = value;
      }
    }

    [Browsable(false)]
    public ArrayList Points
    {
      get
      {
        return this.fPoints;
      }
    }

    [Browsable(false)]
    public double MinX
    {
      get
      {
        return this.fXMin;
      }
    }

    [Browsable(false)]
    public double MinY
    {
      get
      {
        return this.fYMin;
      }
    }

    [Browsable(false)]
    public double MaxX
    {
      get
      {
        return this.fXMax;
      }
    }

    [Browsable(false)]
    public double MaxY
    {
      get
      {
        return this.fMaxY;
      }
    }

    public Graph()
    {
      this.InitGraph();
    }

    public Graph(string Name)
    {
      this.fName = Name;
      this.InitGraph();
    }

    public Graph(string Name, string Title)
    {
      this.fName = Name;
      this.fTitle = Title;
      this.InitGraph();
    }

    private void MinMax(double X, double Y)
    {
      this.fXMin = Math.Min(this.fXMin, X);
      this.fYMin = Math.Min(this.fYMin, Y);
      this.fXMax = Math.Max(this.fXMax, X);
      this.fMaxY = Math.Max(this.fMaxY, Y);
    }

    private void InitGraph()
    {
      this.fStyle = EGraphStyle.Line;
      this.fMoveStyle = EGraphMoveStyle.Point;
      this.fPoints = new ArrayList();
      this.fXMin = double.MaxValue;
      this.fYMin = double.MaxValue;
      this.fXMax = double.MinValue;
      this.fMaxY = double.MinValue;
      this.fMarkerEnabled = true;
      this.fMarkerStyle = EMarkerStyle.Rectangle;
      this.fMarkerSize = 5;
      this.fMarkerColor = Color.Black;
      this.fLineEnabled = true;
      this.fLineDashStyle = DashStyle.Solid;
      this.fLineColor = Color.Black;
      this.fBarWidth = 20;
      this.fToolTipEnabled = true;
      this.fToolTipFormat = "{0}\nX = {2:F2} Y = {3:F2}";
    }

    public void Add(double X, double Y)
    {
      this.fPoints.Add((object) new TMarker(X, Y)
      {
        Style = this.fMarkerStyle,
        Size = this.fMarkerSize,
        Color = this.fMarkerColor
      });
      this.MinMax(X, Y);
    }

    public void Add(double X, double Y, Color Color)
    {
      this.fPoints.Add((object) new TMarker(X, Y)
      {
        Style = this.fMarkerStyle,
        Size = this.fMarkerSize,
        Color = Color
      });
      this.MinMax(X, Y);
    }

    public void Add(double X, double Y, string Text)
    {
      TLabel tlabel = new TLabel(Text, X, Y);
      tlabel.Style = this.fMarkerStyle;
      tlabel.Size = this.fMarkerSize;
      tlabel.Color = this.fMarkerColor;
      this.fPoints.Add((object) tlabel);
      this.MinMax(X, Y);
    }

    public void Add(double X, double Y, string Text, Color MarkerColor)
    {
      TLabel tlabel = new TLabel(Text, X, Y, MarkerColor);
      tlabel.Style = this.fMarkerStyle;
      tlabel.Size = this.fMarkerSize;
      this.fPoints.Add((object) tlabel);
      this.MinMax(X, Y);
    }

    public void Add(double X, double Y, string Text, Color MarkerColor, Color TextColor)
    {
      TLabel tlabel = new TLabel(Text, X, Y, MarkerColor, TextColor);
      tlabel.Style = this.fMarkerStyle;
      tlabel.Size = this.fMarkerSize;
      this.fPoints.Add((object) tlabel);
      this.MinMax(X, Y);
    }

    public void Add(TMarker Marker)
    {
      this.fPoints.Add((object) Marker);
      this.MinMax(Marker.X, Marker.Y);
    }

    public void Add(TLabel Label)
    {
      this.fPoints.Add((object) Label);
      this.MinMax(Label.X, Label.Y);
    }

    public virtual bool IsPadRangeX()
    {
      return false;
    }

    public virtual bool IsPadRangeY()
    {
      return false;
    }

    public virtual PadRange GetPadRangeX(Pad Pad)
    {
      return (PadRange) null;
    }

    public virtual PadRange GetPadRangeY(Pad Pad)
    {
      return (PadRange) null;
    }

    public virtual void Draw(string Option)
    {
      if (Chart.Pad == null)
      {
        Canvas canvas = new Canvas("Canvas", "Canvas");
      }
      Chart.Pad.Add((object) this);
      Chart.Pad.Title.Add(this.fName, this.fLineColor);
      Chart.Pad.Legend.Add(this.fName, this.fLineColor);
      if (Option.ToLower().IndexOf("s") >= 0)
        return;
      Chart.Pad.SetRange(this.fXMin - (this.fXMax - this.fXMin) / 10.0, this.fXMax + (this.fXMax - this.fXMin) / 10.0, this.fYMin - (this.fMaxY - this.fYMin) / 10.0, this.fMaxY + (this.fMaxY - this.fYMin) / 10.0);
    }

    public virtual void Draw()
    {
      this.Draw("");
    }

    public virtual void Paint(Pad Pad, double XMin, double XMax, double YMin, double YMax)
    {
      if (this.fStyle == EGraphStyle.Line && this.fLineEnabled)
      {
        Pen Pen = new Pen(this.fLineColor);
        Pen.DashStyle = this.fLineDashStyle;
        double X1 = 0.0;
        double Y1 = 0.0;
        bool flag = true;
        foreach (TMarker tmarker in this.fPoints)
        {
          if (!flag)
            Pad.DrawLine(Pen, X1, Y1, tmarker.X, tmarker.Y);
          else
            flag = false;
          X1 = tmarker.X;
          Y1 = tmarker.Y;
        }
      }
      if ((this.fStyle == EGraphStyle.Line || this.fStyle == EGraphStyle.Scatter) && this.fMarkerEnabled)
      {
        foreach (TMarker tmarker in this.fPoints)
          tmarker.Paint(Pad, XMin, XMax, YMin, YMax);
      }
      if (this.fStyle != EGraphStyle.Bar)
        return;
      foreach (TMarker tmarker in this.fPoints)
      {
        if (tmarker.Y > 0.0)
          Pad.Graphics.FillRectangle((Brush) new SolidBrush(Color.Black), Pad.ClientX(tmarker.X) - this.fBarWidth / 2, Pad.ClientY(tmarker.Y), this.fBarWidth, Pad.ClientY(0.0) - Pad.ClientY(tmarker.Y));
        else
          Pad.Graphics.FillRectangle((Brush) new SolidBrush(Color.Black), Pad.ClientX(tmarker.X) - this.fBarWidth / 2, Pad.ClientY(0.0), this.fBarWidth, Pad.ClientY(tmarker.Y) - Pad.ClientY(0.0));
      }
    }

    public TDistance Distance(double X, double Y)
    {
      TDistance tdistance1 = new TDistance();
      foreach (TMarker tmarker in this.fPoints)
      {
        TDistance tdistance2 = tmarker.Distance(X, Y);
        if (tdistance2.dX < tdistance1.dX && tdistance2.dY < tdistance1.dY)
          tdistance1 = tdistance2;
      }
      if (tdistance1 != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(this.fToolTipFormat, (object) this.fName, (object) this.fTitle, (object) tdistance1.X, (object) tdistance1.Y);
        tdistance1.ToolTipText = ((object) stringBuilder).ToString();
      }
      return tdistance1;
    }

    public void Move(double X, double Y, double dX, double dY)
    {
      switch (this.fMoveStyle)
      {
        case EGraphMoveStyle.Graph:
          IEnumerator enumerator1 = this.fPoints.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
            {
              TMarker tmarker = (TMarker) enumerator1.Current;
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
          IEnumerator enumerator2 = this.fPoints.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              TMarker tmarker = (TMarker) enumerator2.Current;
              if (tmarker.X == X && tmarker.Y == Y)
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
