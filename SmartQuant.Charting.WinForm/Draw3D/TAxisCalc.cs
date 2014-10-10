// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public class TAxisCalc
    {
        public struct TTick
        {
            public double Value;
            public TVec3 Position;
        }

        public int nTicks { get; private set; }

        public TAxisCalc(TVec3 origin, TVec3 end, double valO, double valEnd, int nTicks)
        {
            throw new NotImplementedException();
        }

        public double TickVal(int i)
        {
            throw new NotImplementedException();
        }

        public TVec3 TickPos(int i)
        {
            throw new NotImplementedException();
        }

        public bool TickPassed(ref TAxisCalc.TTick Tick, double Val)
        {
            throw new NotImplementedException();
        }

        public bool TickPassed(double Val)
        {
            throw new NotImplementedException();
        }

        public static double Round(double x)
        {
            return Math.Pow(10.0, Math.Round(Math.Log10(x)));
        }

        public static double Ceiling(double x, double d)
        {
            return x < 0 ? d * Math.Floor(x / d) : d * Math.Ceiling(x / d);
        }
    }
}

