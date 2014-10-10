// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    [Serializable]
    public class TIntradayTransformation : IChartTransformation
    {
        public long FirstSessionTick { get; set; }

        public long LastSessionTick { get; set; }

        public long Session { get; private set; }

        public bool SessionGridEnabled { get; set; }

        public TIntradayTransformation() : this(0, 864000000000)
        {
        }

        public TIntradayTransformation(long firstSessionTick, long lastSessionTick)
        {
            SessionGridEnabled = true;
            this.SetSessionBounds(firstSessionTick, lastSessionTick);
        }

        public void SetSessionBounds(long firstSessionTick, long lastSessionTick)
        {
            throw new NotImplementedException();
        }

        public void GetFirstGridDivision(ref EGridSize gridSize, ref double min, ref double max, ref DateTime firstDateTime)
        {
            throw new NotImplementedException();
        }

        public double GetNextGridDivision(double firstTick, double prevMajor, int majorCount, EGridSize gridSize)
        {
            throw new NotImplementedException();
        }
            
        public double CalculateRealQuantityOfTicks_Right(double x, double y)
        {
            throw new NotImplementedException();
        }

        public double CalculateRealQuantityOfTicks_Left(double x, double y)
        {
            throw new NotImplementedException();
        }

        private long CalculateJumpGap(long x, long fGridSize)
        {
            throw new NotImplementedException();

        }

        public double CalculateNotInSessionTicks(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
