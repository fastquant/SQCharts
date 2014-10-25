// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
#if XWT
using Xwt.Drawing;
#elif GTK
using Gdk;
using Compatibility.Gtk;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class TLine : IDrawable
    {
        public bool ToolTipEnabled { get; set; }

        public string ToolTipFormat { get; set; }

        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public DashStyle DashStyle { get; set; }

        public Color Color { get; set; }

        public int Width { get; set; }

        public TLine(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Color = Colors.Black;
            DashStyle = DashStyle.Solid;
            Width = 1;
        }

        public TLine(DateTime X1, double Y1, DateTime X2, double Y2)
            : this(X1.Ticks, Y1, X2.Ticks, Y2)
        {
        }

        public virtual void Draw()
        {
            throw new NotImplementedException();
        }

        public virtual void Paint(Pad Pad, double XMin, double XMax, double YMin, double YMax)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double X, double Y)
        {
            return null;
        }
    }
}
