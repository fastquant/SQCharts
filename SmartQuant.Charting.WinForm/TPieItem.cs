// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
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
