// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

namespace SmartQuant.Charting
{
    public interface IDrawable
    {
        bool ToolTipEnabled { get; set; }

        string ToolTipFormat { get; set; }

        void Draw();

        void Paint(Pad Pad, double MinX, double MaxX, double MinY, double MaxY);

        TDistance Distance(double X, double Y);
    }
}
