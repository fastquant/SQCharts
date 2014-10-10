using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Histogram : IDrawable
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public bool ToolTipEnabled { get; set; }

        public string ToolTipFormat { get; set; }

        public Color LineColor { get; set; }

        public Color FillColor { get; set; }

        public Histogram(string name, string title, int nBins, double xMin, double xMax)
        {
            Name = name;
            Title = title;
        }

        public Histogram(string name, int nBins, double xMin, double xMax)
            : this(name, null, nBins, xMin, xMax)
        {
        }

        public Histogram(int nBins, double xMin, double xMax)
            : this(null, null, nBins, xMin, xMax)
        {
        }

        public void Add(double X)
        {
            throw new NotImplementedException();
        }

        public void Add(double X, double Value)
        {
            throw new NotImplementedException();
        }

        public double GetBinSize()
        {
            throw new NotImplementedException();
        }

        public double GetBinMin(int Index)
        {
            throw new NotImplementedException();
        }

        public double GetBinMax(int Index)
        {
            throw new NotImplementedException();
        }

        public double GetBinCentre(int Index)
        {
            throw new NotImplementedException();  
        }

        public double[] GetIntegral()
        {
            throw new NotImplementedException();
        }

        public double GetSum()
        {
            throw new NotImplementedException();
        }

        public double GetMean()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public virtual void Draw()
        {
            Draw("");
        }

        public virtual void Draw(string Option)
        {
            throw new NotImplementedException();
        }

        public virtual void Paint(Pad Pad, double XMin, double XMax, double YMin, double YMax)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double X, double Y)
        {
            return null;
        }
    }
}

