// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public class ZoomEventArgs : EventArgs
    {
        public double XMin { get; set; }

        public double XMax { get; set; }

        public double YMin { get; set; }

        public double YMax { get; set; }

        public bool ZoomUnzoom { get; set; }

        public ZoomEventArgs(double xMin, double xMax, double yMin, double yMax, bool zoomUnzoom)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            ZoomUnzoom = zoomUnzoom;
        }
    }
}
