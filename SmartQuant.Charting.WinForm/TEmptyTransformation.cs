// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    [Serializable]
    public class TEmptyTransformation : IChartTransformation
    {
        public double CalculateNotInSessionTicks(double x, double y)
        {
            return 0.0;
        }

        public double CalculateRealQuantityOfTicks_Right(double x, double y)
        {
            return y - x;
        }

        public double CalculateRealQuantityOfTicks_Left(double x, double y)
        {
            return y - x;
        }

        public void GetFirstGridDivision(ref EGridSize gridSize, ref double min, ref double max, ref DateTime firstDateTime)
        {
            throw new NotImplementedException();
        }

        public double GetNextGridDivision(double firstTick, double prevMajor, int majorCount, EGridSize gridSize)
        {
            throw new NotImplementedException();
        }
    }
}
