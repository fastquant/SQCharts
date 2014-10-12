// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;
using MouseButtons = System.Windows.Forms.MouseButtons;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif
namespace SmartQuant.Charting
{
    public class Pen
    {
    }

    public class Brush
    {
    }

    public class Bitmap
    {
        public Bitmap(Metafile metafile)
        {
            throw new NotImplementedException();
        }
    }

    public class Metafile
    {
    }
    public class Graphics
	{
        public void DrawEllipse(Pen pen, int i, int i2, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string text, Font font, Brush brush, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Pen pen, int i, int i2, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Pen pen, int i, int fClientY, int i2, int i3)
        {
            throw new NotImplementedException();
        }
	}
}
