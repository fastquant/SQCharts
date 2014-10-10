// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;
using System.ComponentModel;

#if XWT
using Xwt.Drawing;

#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class TText : IDrawable
    {
        protected double fX;
        protected double fY;
        protected double fZ;
        protected string fText;
        protected ETextPosition fPosition;
        protected Font fFont;
        protected Color fColor;

        [Category("ToolTip")]
        [Description("")]
        public bool ToolTipEnabled { get; set; }

        [Category("ToolTip")]
        [Description("")]
        public string ToolTipFormat { get; set; }

        [Description("")]
        [Category("Position")]
        public double X
        {
            get
            {
                return this.fX;
            }
            set
            {
                this.fX = value;
            }
        }

        [Category("Position")]
        [Description("")]
        public double Y
        {
            get
            {
                return this.fY;
            }
            set
            {
                this.fY = value;
            }
        }

        [Browsable(false)]
        public double Z
        {
            get
            {
                return this.fZ;
            }
            set
            {
                this.fZ = value;
            }
        }

        [Category("Text")]
        [Description("")]
        public string Text
        {
            get
            {
                return this.fText;
            }
            set
            {
                this.fText = value;
            }
        }

        [Description("")]
        [Category("Text")]
        public ETextPosition Position
        {
            get
            {
                return this.fPosition;
            }
            set
            {
                this.fPosition = value;
            }
        }

        [Category("Text")]
        [Description("")]
        public Font Font
        {
            get
            {
                return this.fFont;
            }
            set
            {
                this.fFont = value;
            }
        }

        [Description("")]
        [Category("Text")]
        public Color Color
        {
            get
            {
                return this.fColor;
            }
            set
            {
                this.fColor = value;
            }
        }

        public TText(string text, double x, double y)
            : this(text, x, y, Colors.Black)
        {
        }

        public TText(string text, double x, double y, Color color)
        {
            X = x;
            Y = y;
            Z = 0.0;
            Text = text;
            Color = color;
            Position = ETextPosition.RightBottom;
            Font = Fonts.SystemFont();
        }

        public TText(string text, DateTime x, double y)
            : this(text, x.Ticks, y, Colors.Black)
        {
        }

        public TText(string text, DateTime x, double y, Color color)
            : this(text, x.Ticks, y, color)
        {
        }

        public virtual void Draw()
        {
            throw new NotImplementedException();
        }

        public void Paint(Pad pad, double minX, double maxX, double minY, double maxY)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double X, double Y)
        {
            return null;
        }
    }
}
