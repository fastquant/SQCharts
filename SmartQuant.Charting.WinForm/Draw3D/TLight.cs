// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

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
        public TLight.TSource[] ParallelBeams;
        public TLight.TSource[] NearSources;

        public TLight()
        {
            throw new NotImplementedException();
        }

        public void SetSfumatoDay()
        {
            throw new NotImplementedException();
        }

        public void SetNormalDay()
        {
            throw new NotImplementedException();
        }

        public void SetVeryBrightDay()
        {
            throw new NotImplementedException();
        }

        public void SetShadowSources(double K)
        {
            throw new NotImplementedException();
        }

        public virtual TColor Result(TVec3 r, TVec3 n, TColor diffuse)
        {
            throw new NotImplementedException();

        }
    }
}

