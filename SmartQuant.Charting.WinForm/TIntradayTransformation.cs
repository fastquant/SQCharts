// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public class TIntradayTransformation : IChartTransformation
    {
        private long firstSessionTick;
        private long lastSessionTick;

        public long FirstSessionTick
        { 
            get
            {
                return firstSessionTick;
            }
            set
            {
            }
        }

        public long LastSessionTick
        { 
            get
            {
                return lastSessionTick;
            }
            set
            {
            }
        }

        public long Session { get; private set; }

        public bool SessionGridEnabled { get; set; }

        public TIntradayTransformation()
            : this(0, TimeSpan.TicksPerDay)
        {
        }

        public TIntradayTransformation(long firstSessionTick, long lastSessionTick)
        {
            SessionGridEnabled = true;
            SetSessionBounds(firstSessionTick, lastSessionTick);
        }

        public void SetSessionBounds(long firstSessionTick, long lastSessionTick)
        {
            this.firstSessionTick = FirstSessionTick;
            this.lastSessionTick = LastSessionTick;
            Session = this.lastSessionTick - this.firstSessionTick;
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

        public double CalculateNotInSessionTicks(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
