// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Threading.Tasks;

namespace SmartQuant.Charting.Draw3D
{
    public class TAxisCalc
    {
        public struct TTick
        {
            public double Value;
            public TVec3 Position;
        }

        private TVec3 origin;
        private TVec3 end;
        private double valO;
        private double valEnd;
        private TTick[] ticks;

        private double lastVal;

        public int nTicks { get; private set; }

        public TAxisCalc(TVec3 origin, TVec3 end, double valO, double valEnd, int nTicks)
        {
            this.origin = origin;
            this.end = end;
            this.valO = valO;
            this.valEnd = valEnd;
            this.nTicks = nTicks;

            // Set ticks
            double d = Round(Math.Abs(this.valEnd - this.valO) / this.nTicks);
            double num1 = Ceiling(this.valO, d);
            this.nTicks = (int)(Math.Abs(this.valEnd - num1) / d) + 1;
            if (this.nTicks < 3)
                this.nTicks = 3;
            this.ticks = new TTick[this.nTicks];
            if (this.nTicks == 3)
            {
                this.ticks[0].Value = this.valO;
                this.ticks[1].Value = (this.valO + this.valEnd) / 2;
                this.ticks[2].Value = this.valEnd;
            }
            else
            {
                d = this.valEnd < this.valO ? -d : d;
                for (int i = 0; i < this.nTicks; ++i)
                    this.ticks[i].Value = num1 + i * d;
            }

            // Set tick positions
            Parallel.ForEach(this.ticks, tick => tick.Position = this.origin + (this.end - this.origin) * (tick.Value - this.valO) / (this.valEnd - this.valO));
        }

        public double TickVal(int i)
        {
            return this.ticks[i].Value;
        }

        public TVec3 TickPos(int i)
        {
            return this.ticks[i].Position;
        }

        // TODO: Need reviewed!
        public bool TickPassed(ref TTick tick, double val)
        {
            foreach (var ttick in this.ticks)
            {
                if (val == ttick.Value || (val - ttick.Value) * (this.lastVal - ttick.Value) < 0.0)
                {
                    tick = ttick;
                    this.lastVal = val;
                    return true;
                }
            }
            this.lastVal = val;
            return false;
        }

        public bool TickPassed(double val)
        {
            var tick = new TTick();
            return TickPassed(ref tick, val);
        }

        public static double Round(double x)
        {
            return Math.Pow(10, Math.Round(Math.Log10(x)));
        }

        public static double Ceiling(double x, double d)
        {
            return x < 0 ? d * Math.Floor(x / d) : d * Math.Ceiling(x / d);
        }
    }
}
