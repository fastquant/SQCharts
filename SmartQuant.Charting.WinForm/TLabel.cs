// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

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

namespace SmartQuant.Charting
{
    [Serializable]
    public class TLabel : TMarker
    {

        [Category("Text")]
        [Description("Text that this label displays")]
        public new string Text { get; set; }

        [Description("Text position of this label")]
        [Category("Text")]
        public new ETextPosition TextPosition { get; set; }

        [Category("Text")]
        [Description("Text font of this label")]
        public new Font TextFont { get; set; }

        [Category("Text")]
        [Description("Text color of this label")]
        public new Color TextColor { get; set; }

        [Category("Text")]
        [Description("Text offset in pixels alone X coordinate")]
        public int TextOffsetX { get; set; }

        [Category("Text")]
        [Description("Text offset in pixels alone Y coordinate")]
        public int TextOffsetY { get; set; }

        public TLabel(string text, double x, double y)
            : this(text, x, y, default(Color), Colors.Black)
        {
        }

        public TLabel(string text, double x, double y, Color markerColor)
            : this(text, x, y, markerColor, Colors.Black)
        {
        }

        public TLabel(string text, double x, double y, Color markerColor, Color textColor)
            : base(x, y, markerColor)
        {
            Text = text;
            TextFont = Fonts.SystemFont();
            TextPosition = ETextPosition.RightBottom;
            TextColor = Colors.Black;
            TextOffsetX = 0;
            TextOffsetY = 2;
            TextColor = textColor;
        }

        public override void Paint(Pad Pad, double MinX, double MaxX, double MinY, double MaxY)
        {
            throw new NotImplementedException();
        }
    }
}
