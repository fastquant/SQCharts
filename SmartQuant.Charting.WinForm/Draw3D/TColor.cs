// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting.Draw3D
{
    public struct TColor
    {
        private double r;
        private double g;
        private double b;

        public Color Color
        {
            get
            {
                return ColorUtils.FromArgb((int) ((double) byte.MaxValue * this.r), (int) ((double) byte.MaxValue * this.g), (int) ((double) byte.MaxValue * this.b));
            }
            set
            {
                this = new TColor(value);
            }
        }

        public TColor(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public TColor(double gray)
        {
            this.r = this.g = this.b = gray;
        }

        public TColor(Color c)
        {
            #if XWT
            double r = c.Red;
            double g = c.Green;
            double b = c.Blue;
            #else
            double r = c.R;
            double g = c.G;
            double b = c.B;
            #endif
            this.r = 1.0 / (double) byte.MaxValue * r;
            this.g = 1.0 / (double) byte.MaxValue * g;
            this.b = 1.0 / (double) byte.MaxValue * b;
        }

        public static implicit operator TColor(Color c)
        {
            return new TColor(c);
        }

        public static TColor operator +(TColor a, TColor b)
        {
            return new TColor(a.r + b.r, a.g + b.g, a.b + b.b);
        }

        public static TColor operator *(TColor a, TColor b)
        {
            return new TColor(a.r * b.r, a.g * b.g, a.b * b.b);
        }

        public static TColor operator *(double k, TColor c)
        {
            return new TColor(k * c.r, k * c.g, k * c.b);
        }

        public static TColor operator *(TColor c, double k)
        {
            return k * c;
        }

        public int Get888()
        {
            return ((int) ((double) byte.MaxValue * this.r) << 16) + ((int) ((double) byte.MaxValue * this.g) << 8) + (int) ((double) byte.MaxValue * this.b);
        }

        private void Clip(ref double x)
        {
            if (x < 1.0 / 254.0)
                x = 1.0 / 254.0;
            if (x <= 1.0)
                return;
            x = 1.0;
        }

        public void Clip()
        {
            this.Clip(ref this.r);
            this.Clip(ref this.g);
            this.Clip(ref this.b);
        }

        public static TColor Clip(TColor c)
        {
            TColor tcolor = c;
            tcolor.Clip();
            return tcolor;
        }
    }
}
