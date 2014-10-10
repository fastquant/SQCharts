// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif
using System.Text;

namespace SmartQuant.Charting
{
    public class TImage : IDrawable, IMovable
    {
        private Image Image { get; set; }

        [Category("Position")]
        [Description("X position of this image on the pad. (World coordinate system)")]
        public double X { get; set; }

        [Category("Position")]
        [Description("Y position of this image on the pad. (World coordinate system)")]
        public double Y { get; set; }

        [Description("Enable or disable tooltip appearance for this image.")]
        [Category("ToolTip")]
        public bool ToolTipEnabled { get; set; }

        [Category("ToolTip")]
        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        public string ToolTipFormat { get; set; }

        public TImage(Image image, double x, double y)
        {
            Image = image;
            X = x;
            Y = y;
            ToolTipEnabled = true;
            ToolTipFormat = "X = {0:F2} Y = {1:F2}";
        }

        public TImage(Image image, DateTime x, double y)
            : this(image, x.Ticks, y)
        {
        }

        public TImage(string fileName, double x, double y)
            : this(Images.FromFile(fileName), x, y)
        {
        }

        public TImage(string fileName, DateTime x, double y)
            : this(Images.FromFile(fileName), x.Ticks, y)
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

        public TDistance Distance(double x, double y)
        {
            TDistance td = new TDistance();
            td.X = X;
            td.Y = Y;
            td.dX = Math.Abs(x - X);
            td.dY = Math.Abs(y - Y);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(ToolTipFormat, X, Y);
            td.ToolTipText = sb.ToString();
            return td;
        }

        public void Move(double x, double y, double dx, double dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
