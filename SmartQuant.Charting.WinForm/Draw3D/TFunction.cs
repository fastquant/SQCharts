// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using SmartQuant.Charting;

namespace SmartQuant.Charting.Draw3D
{
    public abstract class TFunction
    {
        protected double MaxX;
        protected double MaxY;
        protected double MinX;
        protected double MinY;
        protected int nx;
        protected int ny;

        public EChartLook Look;
        public TSurface Surface;
        public bool Grid;
        public bool LevelLines;

        private bool BitmapWriteOnly = true;

        public TFunction()
        {
            MaxX = 1.0;
            MaxY = 1.0;
            Surface = new TSurface();
            Surface.Diffuse = 0.59 * new TColor(0.5, 0.7, 1.0);
        }

        public abstract double f(double x, double y);

        public virtual TColor color0(double x, double y)
        {
            return this.Surface.Diffuse;
        }

        public unsafe void Paint(Pad pad)
        {
        }
    }
}

