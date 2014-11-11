// Decompiled with JetBrains decompiler
// Type: SmartQuant.Charting.Histogram
// Assembly: SmartQuant.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=23953e483e363d68
// MVID: F3B55EE9-4DBA-4875-B18A-7BD8DFCF4D88
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.Charting.dll

using System;
using System.Drawing;

namespace SmartQuant.Charting
{
  [Serializable]
  public class Histogram : IDrawable
  {
    private string fName;
    private string fTitle;
    protected int fNBins;
    protected double[] fBins;
    protected double fBinSize;
    protected double fXMin;
    protected double fXMax;
    protected double fYMin;
    protected double fYMax;
    protected double[] fIntegral;
    protected bool fIntegralChanged;
    private Color fLineColor;
    private Color fFillColor;
    [NonSerialized]
    private Brush fFillBrush;
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

    public Color FillColor
    {
      get
      {
        return this.fFillColor;
      }
      set
      {
        this.fFillColor = value;
      }
    }

    public Histogram(string Name, string Title, int NBins, double XMin, double XMax)
    {
      this.fName = Name;
      this.fTitle = Title;
      this.Init(NBins, XMin, XMax);
    }

    public Histogram(string Name, int NBins, double XMin, double XMax)
    {
      this.fName = Name;
      this.Init(NBins, XMin, XMax);
    }

    public Histogram(int NBins, double XMin, double XMax)
    {
      this.Init(NBins, XMin, XMax);
    }

    private void Init(int NBins, double XMin, double XMax)
    {
      this.fNBins = NBins;
      this.fBins = new double[this.fNBins];
      this.fBinSize = Math.Abs(XMax - XMin) / (double) NBins;
      for (int index = 0; index < this.fNBins; ++index)
        this.fBins[index] = 0.0;
      if (XMin < XMax)
      {
        this.fXMin = XMin;
        this.fXMax = XMax;
      }
      else
      {
        this.fXMin = XMax;
        this.fXMax = XMin;
      }
      this.fYMin = 0.0;
      this.fYMax = 0.0;
      this.fLineColor = Color.Black;
      this.fFillColor = Color.Blue;
      this.fFillBrush = (Brush) null;
      this.fIntegral = new double[this.fNBins];
      this.fIntegralChanged = false;
    }

    public void Add(double X)
    {
      if (X < this.fXMin || X >= this.fXMax)
        return;
      int index = (int) ((double) this.fNBins * (X - this.fXMin) / (this.fXMax - this.fXMin));
      ++this.fBins[index];
      if (this.fBins[index] > this.fYMax)
        this.fYMax = this.fBins[index];
      this.fIntegralChanged = true;
    }

    public void Add(double X, double Value)
    {
      if (X < this.fXMin || X >= this.fXMax)
        return;
      int index = (int) ((double) this.fNBins * (X - this.fXMin) / (this.fXMax - this.fXMin));
      this.fBins[index] = Value;
      if (this.fBins[index] > this.fYMax)
        this.fYMax = this.fBins[index];
      this.fIntegralChanged = true;
    }

    public double GetBinSize()
    {
      return this.fBinSize;
    }

    public double GetBinMin(int Index)
    {
      return this.fXMin + this.fBinSize * (double) Index;
    }

    public double GetBinMax(int Index)
    {
      return this.fXMin + this.fBinSize * (double) (Index + 1);
    }

    public double GetBinCentre(int Index)
    {
      return this.fXMin + this.fBinSize * ((double) Index + 0.5);
    }

    public double[] GetIntegral()
    {
      if (this.fIntegralChanged)
      {
        for (int index = 0; index < this.fNBins; ++index)
          this.fIntegral[index] = index != 0 ? this.fIntegral[index - 1] + this.fBins[index] : this.fBins[index];
        if (this.fIntegral[this.fNBins - 1] == 0.0)
        {
          Console.WriteLine("Error in THistogram::GetIntegral, Integral = 0");
          return (double[]) null;
        }
        else
        {
          for (int index = 0; index < this.fNBins; ++index)
            this.fIntegral[index] /= this.fIntegral[this.fNBins - 1];
        }
      }
      return this.fIntegral;
    }

    public double GetSum()
    {
      double num = 0.0;
      for (int index = 0; index < this.fNBins; ++index)
        num += this.fBins[index];
      return num;
    }

    public double GetMean()
    {
      double num1 = 0.0;
      double num2 = 0.0;
      for (int Index = 0; Index < this.fNBins; ++Index)
      {
        num1 += this.fBins[Index];
        num2 += this.GetBinCentre(Index) * this.fBins[Index];
      }
      if (num1 != 0.0)
        return num2 / num1;
      else
        return 0.0;
    }

    public void Print()
    {
      for (int Index = 0; Index < this.fNBins; ++Index)
        Console.WriteLine((string) (object) Index + (object) " - [" + (string) (object) this.GetBinMin(Index) + " " + (string) (object) this.GetBinCentre(Index) + " " + (string) (object) this.GetBinMax(Index) + "] : " + this.fBins[Index].ToString("F2"));
    }

    public virtual void Draw()
    {
      this.Draw("");
    }

    public virtual void Draw(string Option)
    {
      if (Chart.Pad == null)
      {
        Canvas canvas = new Canvas("Canvas", "Canvas");
      }
      Chart.Pad.Add((object) this);
      Chart.Pad.Title.Add(this.fName, this.fFillColor);
      Chart.Pad.Legend.Add(this.fName, this.fFillColor);
      if (Option.ToLower().IndexOf("s") >= 0)
        return;
      Chart.Pad.SetRange(this.fXMin, this.fXMax, this.fYMin - (this.fYMax - this.fYMin) / 10.0, this.fYMax + (this.fYMax - this.fYMin) / 10.0);
    }

    public virtual void Paint(Pad Pad, double XMin, double XMax, double YMin, double YMax)
    {
      Pen Pen = new Pen(this.fLineColor);
      Brush brush = this.fFillBrush != null ? this.fFillBrush : (Brush) new SolidBrush(this.fFillColor);
      for (int Index = 0; Index < this.fNBins; ++Index)
      {
        Pad.Graphics.FillRectangle(brush, Pad.ClientX(this.GetBinMin(Index)), Pad.ClientY(this.fBins[Index]), Math.Abs(Pad.ClientX(this.GetBinMax(Index)) - Pad.ClientX(this.GetBinMin(Index))), Math.Abs(Pad.ClientY(this.fBins[Index]) - Pad.ClientY(0.0)));
        Pad.DrawLine(Pen, this.GetBinMin(Index), 0.0, this.GetBinMin(Index), this.fBins[Index]);
        Pad.DrawLine(Pen, this.GetBinMin(Index), this.fBins[Index], this.GetBinMax(Index), this.fBins[Index]);
        Pad.DrawLine(Pen, this.GetBinMax(Index), this.fBins[Index], this.GetBinMax(Index), 0.0);
      }
    }

    public TDistance Distance(double X, double Y)
    {
      return (TDistance) null;
    }
  }
}
