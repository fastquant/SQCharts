using System;
using SmartQuant.Charting.Draw3D;

#if XWT
using Xwt.Drawing;

#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Histogram2D : IDrawable
    {
        public const double epsilon = 1E-09;

        protected string fName;
        protected string fTitle;
        protected int fNBinsX;
        protected int fNBinsY;
        protected double fXMin;
        protected double fXMax;
        protected double fYMin;
        protected double fYMax;
        protected double[,] fBins;
        protected double fBinSizeX;
        protected double fBinSizeY;
        protected double fBinMin;
        protected double fBinMax;
        protected double Kx;
        protected double Ky;
        protected double fShowMaxZ;
        protected int fNColors;
        protected Color[] fPalette;
        public ESmoothing Smoothing;
        public bool Multicolor3D;

        public string Name{ get; set; }

        public string Title{ get; set; }

        public bool ToolTipEnabled { get; set; }

        public string ToolTipFormat { get; set; }

        public double dX
        {
            get
            {
                return (this.fXMax - this.fXMin) / (double)this.fNBinsX;
            }
        }

        public double dY
        {
            get
            {
                return (this.fYMax - this.fYMin) / (double)this.fNBinsY;
            }
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void Paint(Pad pad, double minX, double maxX, double minY, double maxY)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double X, double Y)
        {
            throw new NotImplementedException();
        }
    }
}

