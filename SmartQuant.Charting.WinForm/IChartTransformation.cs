// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public interface IChartTransformation
    {
        double CalculateNotInSessionTicks(double X, double Y);

        double CalculateRealQuantityOfTicks_Right(double X, double Y);

        double CalculateRealQuantityOfTicks_Left(double X, double Y);

        void GetFirstGridDivision(ref EGridSize GridSize, ref double Min, ref double Max, ref DateTime FirstDateTime);

        double GetNextGridDivision(double FirstTick, double PrevMajor, int MajorCount, EGridSize GridSize);
    }
}
