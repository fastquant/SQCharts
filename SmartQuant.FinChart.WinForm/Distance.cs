// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

namespace SmartQuant.FinChart
{
    public class Distance
    {
        public double DX { get; set; }

        public double DY { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string ToolTipText { get; set; }

        public Distance()
        {
            DX = DY = double.MaxValue;
            ToolTipText = null;
        }
    }
}
