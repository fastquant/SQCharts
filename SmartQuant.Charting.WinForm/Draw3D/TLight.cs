// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Drawing;

namespace SmartQuant.Charting.Draw3D
{
    public class TLight
    {
        public struct TSource
        {
            public TVec3 o;
            public TColor c;

            public TSource(TVec3 o, TColor c)
            {
                this.o = o;
                this.c = c;
            }
        }

        public TColor Ambient;
        public TSource[] ParallelBeams;
        public TSource[] NearSources;

        public TLight()
        {
            Ambient = new TColor(Color.PaleTurquoise);
            ParallelBeams = new TSource[1]
            {
                new TSource(new TVec3(3.0, -2.0, 2.0), (TColor)Color.LightYellow)
            };
            NearSources = new TSource[0];

            this.SetSfumatoDay();
            this.SetShadowSources(0.25);
        }

        public void SetSfumatoDay()
        {
            this.Ambient = new TColor(0.55, 0.55, 0.7);
            this.ParallelBeams[0].c = 2.05 * new TColor(1.0, 1.0, 0.55);
        }

        public void SetNormalDay()
        {
            this.SetSfumatoDay();
            this.Ambient *= 1.1;
            this.ParallelBeams[0].c *= 1.1;
        }

        public void SetVeryBrightDay()
        {
            this.SetSfumatoDay();
            this.Ambient *= 1.2;
            this.ParallelBeams[0].c *= 1.2;
        }

        public void SetShadowSources(double k)
        {
            var sources = new TSource[2 * this.ParallelBeams.Length];
            for (int i = 0; i < this.ParallelBeams.Length; ++i)
            {
                int index2 = 2 * i;
                sources[index2] = this.ParallelBeams[i];
                sources[index2 + 1].o = -sources[index2].o;
                sources[index2 + 1].c = -k * sources[index2].c;
            }
            this.ParallelBeams = sources;
        }

        public virtual TColor Result(TVec3 r, TVec3 n, TColor diffuse)
        {
            TColor tcolor = this.Ambient;
            foreach (TLight.TSource tsource in this.ParallelBeams)
            {
                double num1 = n * tsource.o;
                if (num1 >= 0.0)
                {
                    double num2 = num1 * num1 / (n * n * tsource.o * tsource.o);
                    tcolor += num2 * tsource.c;
                }
            }
            foreach (TLight.TSource tsource in this.NearSources)
            {
                TVec3 tvec3 = tsource.o - r;
                double num1 = n * tvec3;
                double num2 = tvec3 * tvec3;
                if (num1 >= 0.0)
                {
                    double num3 = num1 * num1 / (n * n * num2 * num2);
                    tcolor += num3 * tsource.c;
                }
            }
            return diffuse * tcolor;
        }
    }
}

