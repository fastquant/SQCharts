// Decompiled with JetBrains decompiler
// Type: SmartQuant.Charting.TMarker
// Assembly: SmartQuant.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=23953e483e363d68
// MVID: D7916FA1-2509-47A3-B59F-156017766DAE
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.Charting.dll

using System;
using System.ComponentModel;
#if XWT
using Xwt.Drawing;
using Compatibility.Xwt;
#elif GTK
using Gdk;
using Compatibility.Gtk;
using Font = Compatibility.Gtk.Font;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif
using System.Text;

namespace SmartQuant.Charting
{
    [Serializable]
    public class TMarker : IDrawable, IMovable
    {
        protected Color fBuyColor = Colors.Blue;
        protected Color fSellColor = Colors.Red;
        protected Color fSellShortColor = Colors.Yellow;
        protected Color fBuyShortColor = Colors.Green;
        protected double fX;
        protected double fY;
        protected double fZ;
        protected double fHigh;
        protected double fLow;
        protected double fOpen;
        protected double fClose;
        protected EMarkerStyle fStyle;
        protected Color fColor;
        protected int fSize;
        protected bool fFilled;
        [NonSerialized]
        protected string fText;
        [NonSerialized]
        protected bool fTextEnabled;
        [NonSerialized]
        protected EMarkerTextPosition fTextPosition;
        [NonSerialized]
        protected int fTextOffset;
        [NonSerialized]
        protected Color fTextColor;
        [NonSerialized]
        protected Font fTextFont;
        protected bool fToolTipEnabled;
        protected string fToolTipFormat;

        public Color BuyColor
        {
            get
            {
                return this.fBuyColor;
            }
            set
            {
                this.fBuyColor = value;
            }
        }

        public Color SellColor
        {
            get
            {
                return this.fSellColor;
            }
            set
            {
                this.fSellColor = value;
            }
        }

        public Color BuyShortColor
        {
            get
            {
                return this.fBuyShortColor;
            }
            set
            {
                this.fBuyShortColor = value;
            }
        }

        public Color SellShortColor
        {
            get
            {
                return this.fSellShortColor;
            }
            set
            {
                this.fSellShortColor = value;
            }
        }

        [Description("Enable or disable tooltip appearance for this marker.")]
        [Category("ToolTip")]
        public bool ToolTipEnabled
        {
            get
            {
                return this.fToolTipEnabled;
            }
            set
            {
                this.fToolTipEnabled = value;
            }
        }

        [Category("ToolTip")]
        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        public string ToolTipFormat
        {
            get
            {
                return this.fToolTipFormat;
            }
            set
            {
                this.fToolTipFormat = value;
            }
        }

        [Category("Position")]
        [Description("X position of this marker on the pad. (World coordinate system)")]
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
        [Description("Y position of this marker on the pad. (World coordinate system)")]
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

        [Description("Marker style")]
        [Category("Marker")]
        public EMarkerStyle Style
        {
            get
            {
                return this.fStyle;
            }
            set
            {
                this.fStyle = value;
            }
        }

        [Description("Marker color")]
        [Category("Marker")]
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

        [Description("Marker size in pixels")]
        [Category("Marker")]
        public int Size
        {
            get
            {
                return this.fSize;
            }
            set
            {
                this.fSize = value;
            }
        }

        [Description("Marker interior will be filled if this propery is set to true, otherwise it will be transparent")]
        [Category("Marker")]
        public bool Filled
        {
            get
            {
                return this.fFilled;
            }
            set
            {
                this.fFilled = value;
            }
        }

        [Description("High of bar marker")]
        [Category("Value")]
        public double High
        {
            get
            {
                return this.fHigh;
            }
            set
            {
                this.fHigh = value;
            }
        }

        [Category("Value")]
        [Description("Low of bar marker")]
        public double Low
        {
            get
            {
                return this.fLow;
            }
            set
            {
                this.fLow = value;
            }
        }

        [Description("Open of bar marker")]
        [Category("Value")]
        public double Open
        {
            get
            {
                return this.fOpen;
            }
            set
            {
                this.fOpen = value;
            }
        }

        [Category("Value")]
        [Description("Close of bar marker")]
        public double Close
        {
            get
            {
                return this.fClose;
            }
            set
            {
                this.fClose = value;
            }
        }

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

        public bool TextEnabled
        {
            get
            {
                return this.fTextEnabled;
            }
            set
            {
                this.fTextEnabled = value;
            }
        }

        public EMarkerTextPosition TextPosition
        {
            get
            {
                return this.fTextPosition;
            }
            set
            {
                this.fTextPosition = value;
            }
        }

        public int TextOffset
        {
            get
            {
                return this.fTextOffset;
            }
            set
            {
                this.fTextOffset = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return this.fTextColor;
            }
            set
            {
                this.fTextColor = value;
            }
        }

        public Font TextFont
        {
            get
            {
                return this.fTextFont;
            }
            set
            {
                this.fTextFont = value;
            }
        }

        public TMarker(double x, double y)
        {
            this.fX = x;
            this.fY = y;
            this.fZ = 0.0;
            this.InitMarker();
        }

        public TMarker(DateTime X, double Y)
        {
            this.fX = (double) X.Ticks;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
        }

        public TMarker(string X, double Y)
        {
            this.fX = (double) DateTime.Parse(X).Ticks;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
        }

        public TMarker(double X, double Y, Color Color)
        {
            this.fX = X;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
            this.fColor = Color;
        }

        public TMarker(double X, double Y, EMarkerStyle Style)
        {
            this.fX = X;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
            this.fStyle = Style;
            if (this.fStyle != EMarkerStyle.Buy && this.fStyle != EMarkerStyle.Sell && (this.fStyle != EMarkerStyle.SellShort && this.fStyle != EMarkerStyle.BuyShort))
                return;
            this.fSize = 10;
        }

        public TMarker(DateTime X, double Y, EMarkerStyle Style)
        {
            this.fX = (double) X.Ticks;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
            this.fStyle = Style;
            if (this.fStyle != EMarkerStyle.Buy && this.fStyle != EMarkerStyle.Sell && (this.fStyle != EMarkerStyle.SellShort && this.fStyle != EMarkerStyle.BuyShort))
                return;
            this.fSize = 10;
        }

        public TMarker(string X, double Y, EMarkerStyle Style)
        {
            this.fX = (double) DateTime.Parse(X).Ticks;
            this.fY = Y;
            this.fZ = 0.0;
            this.InitMarker();
            this.fStyle = Style;
            if (this.fStyle != EMarkerStyle.Buy && this.fStyle != EMarkerStyle.Sell && (this.fStyle != EMarkerStyle.SellShort && this.fStyle != EMarkerStyle.BuyShort))
                return;
            this.fSize = 10;
        }

        public TMarker(double X, double Y, double Z)
        {
            this.fX = X;
            this.fY = Y;
            this.fZ = Z;
            this.InitMarker();
        }

        public TMarker(double X, double Y, double Z, Color Color)
        {
            this.fX = X;
            this.fY = Y;
            this.fZ = Z;
            this.InitMarker();
            this.fColor = Color;
        }

        public TMarker(double X, double High, double Low, double Open, double Close)
        {
            this.fX = X;
            this.fY = 0.0;
            this.fZ = 0.0;
            this.fHigh = High;
            this.fLow = Low;
            this.fOpen = Open;
            this.fClose = Close;
            this.InitMarker();
        }

        public TMarker(double X, double High, double Low, double Open, double Close, Color Color)
        {
            this.fX = X;
            this.fY = 0.0;
            this.fZ = 0.0;
            this.fHigh = High;
            this.fLow = Low;
            this.fOpen = Open;
            this.fClose = Close;
            this.InitMarker();
            this.fColor = Color;
        }

        private void InitMarker()
        {
            this.fStyle = EMarkerStyle.Rectangle;
            this.fColor = Colors.Black;
            this.fSize = 5;
            this.fFilled = true;
            this.fTextEnabled = true;
            this.fTextOffset = 2;
            this.fTextPosition = EMarkerTextPosition.Bottom;
            this.fTextFont = Fonts.SystemFont();
            this.fTextColor = Colors.Black;
            this.fToolTipEnabled = true;
            this.fToolTipFormat = "X = {0:F2} Y = {1:F2}";
        }

        public virtual void Draw()
        {
            throw new NotImplementedException();
        }

        public virtual void Paint(Pad Pad, double XMin, double XMax, double YMin, double YMax)
        {
            throw new NotImplementedException();
        }

        public virtual TDistance Distance(double x, double y)
        {
            TDistance tdistance = new TDistance();
            tdistance.X = this.fX;
            tdistance.Y = this.fY;
            tdistance.dX = Math.Abs(x - this.fX);
            tdistance.dY = Math.Abs(y - this.fY);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(this.fToolTipFormat, (object) this.fX, (object) this.fY);
            tdistance.ToolTipText = ((object) stringBuilder).ToString();
            return tdistance;
        }

        public void Move(double x, double y, double dx, double dy)
        {
            this.fX += dx;
            this.fY += dy;
        }
    }
}
