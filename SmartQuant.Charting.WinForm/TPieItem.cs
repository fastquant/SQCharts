// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Drawing;

namespace SmartQuant.Charting
{
    class TPieItem
    {
        public double Weight { get; set; }

        public Color Color { get; set; }

        public string Text { get; set; }

        public TPieItem(double weight, string text, Color color)
        {
            Weight = weight;
            Text = text;
            Color = color;
        }
    }
}
