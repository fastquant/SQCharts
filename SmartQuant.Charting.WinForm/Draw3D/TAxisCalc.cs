// Decompiled with JetBrains decompiler
// Type: SmartQuant.Charting.Draw3D.TAxisCalc
// Assembly: SmartQuant.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=23953e483e363d68
// MVID: F3B55EE9-4DBA-4875-B18A-7BD8DFCF4D88
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.Charting.dll

using System;

namespace SmartQuant.Charting.Draw3D
{
    public class TAxisCalc
    {
        private TAxisCalc.TTick[] ticks = new TAxisCalc.TTick[0];
        private TVec3 Origin;
        private TVec3 End;
        private double ValO;
        private double ValEnd;
        private int n;
        private double lastVal;

        public int nTicks
        {
            get
            {
                return this.n;
            }
        }

        public TAxisCalc(TVec3 Origin, TVec3 End, double ValO, double ValEnd, int nTicks)
        {
            this.Origin = Origin;
            this.End = End;
            this.ValO = ValO;
            this.ValEnd = ValEnd;
            this.n = nTicks;
            this.SetTicks();
            this.SetTickPositions();
        }

        public double TickVal(int i)
        {
            return this.ticks[i].Value;
        }

        public TVec3 TickPos(int i)
        {
            return new TVec3(this.ticks[i].Position);
        }

        public bool TickPassed(ref TAxisCalc.TTick Tick, double Val)
        {
            foreach (TAxisCalc.TTick ttick in this.ticks)
            {
                if (Val == ttick.Value || (Val - ttick.Value) * (this.lastVal - ttick.Value) < 0.0)
                {
                    Tick = ttick;
                    this.lastVal = Val;
                    return true;
                }
            }
            this.lastVal = Val;
            return false;
        }

        public bool TickPassed(double Val)
        {
            foreach (TAxisCalc.TTick ttick in this.ticks)
            {
                if (Val == ttick.Value || (Val - ttick.Value) * (this.lastVal - ttick.Value) < 0.0)
                {
                    this.lastVal = Val;
                    return true;
                }
            }
            this.lastVal = Val;
            return false;
        }

        public static double Round(double x)
        {
            return Math.Pow(10.0, Math.Round(Math.Log10(x)));
        }

        public static double Ceiling(double x, double d)
        {
            if (x < 0.0)
                return d * Math.Floor(x / d);
            else
                return d * Math.Ceiling(x / d);
        }

        private void SetTicks()
        {
            double d = TAxisCalc.Round(Math.Abs(this.ValEnd - this.ValO) / (double) this.n);
            double num1 = TAxisCalc.Ceiling(this.ValO, d);
            this.n = (int) (Math.Abs(this.ValEnd - num1) / d) + 1;
            if (this.n < 3)
                this.n = 3;
            this.ticks = new TAxisCalc.TTick[this.n];
            if (this.n == 3)
            {
                this.ticks[0].Value = this.ValO;
                this.ticks[1].Value = 0.5 * (this.ValO + this.ValEnd);
                this.ticks[2].Value = this.ValEnd;
            }
            else
            {
                double num2 = num1;
                if (this.ValEnd < this.ValO)
                    d = -d;
                int index = 0;
                while (index < this.n)
                {
                    this.ticks[index].Value = num2;
                    ++index;
                    num2 += d;
                }
            }
        }

        private void SetTickPositions()
        {
            for (int index = 0; index < this.ticks.Length; ++index)
                this.ticks[index].Position = this.Origin + (this.End - this.Origin) * (this.ticks[index].Value - this.ValO) / (this.ValEnd - this.ValO);
        }

        public struct TTick
        {
            public double Value;
            public TVec3 Position;
        }
    }
}
