// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;
using MouseButtons = System.Windows.Forms.MouseButtons;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using SmoothingMode = System.Drawing.Drawing2D.SmoothingMode;
using TextRenderingHint = System.Drawing.Text.TextRenderingHint;

using Xwt.Drawing;
using Xwt;

namespace Compatibility.Xwt
{
    public class Pen
    {
        public Pen(Color color):this(color, 8)
        {
        }

        public Color Color { get; set; }

        public float Width { get; set; }

        public Pen(Color color, float width)
        {
            Color = color;
            Width = width;
        }
    }

    public class Brush
    {
    }

    public class SolidBrush : Brush
    {
        public Color Color { get; set; }

        public SolidBrush(Color color)
        {
            Color = color;
        }
    }

    public class Bitmap
    {
        public Bitmap(Metafile metafile)
        {
            throw new NotImplementedException();
        }
    }

    public class GraphicsPath
    {
        public void AddLine(Point pt1, Point pt2)
        {
            throw new NotImplementedException();
        }
    }

    public struct PointF
    {
        public static implicit operator PointF(Point p)
        {
            return new PointF();
        }
    }
    public struct SizeF
    {
        public float Width
        {
            get;
            set;
        }
        public float Height
        {
            get;
            set;
        }
    }
    public class Metafile
    {
    }

    public class Graphics
    {
        private Context ctx;
        private Rectangle rect;

        public TextRenderingHint TextRenderingHint
        {
            get;
            set;
        }

        public SmoothingMode SmoothingMode
        {
            get;
            set;
        }

        public Graphics(Context ctx, Rectangle rect)
        {
            this.ctx = ctx;
            this.rect = rect; 
        }

        public void Clear(Color color)
        {
            this.ctx.Rectangle(this.rect);
            this.ctx.SetColor(color);
            this.ctx.Fill();
        }

        public void DrawEllipse(Pen pen, float i, float i2, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string text, Font font, Brush brush, float x, float y)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Pen pen, float i, float i2, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Pen pen, float i, float fClientY, float i2, float i3)
        {
            throw new NotImplementedException();
        }

        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image image, int i, int i2)
        {
            throw new NotImplementedException();
        }

        public void DrawPath(Pen pen, GraphicsPath gPath)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Brush brush, float x, float y, float i, float i2)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Pen pen, PointF[] points1)
        {
            throw new NotImplementedException();
        }

        public void FillPolygon(Brush brush, PointF[] points1)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString(string str, Font font)
        {
            throw new NotImplementedException();
        }
    }
}
