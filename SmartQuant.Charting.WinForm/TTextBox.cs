// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
#if XWT
using Xwt.Drawing;
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
    public class TTextBox : IDrawable
    {
        public bool ToolTipEnabled { get; set; }

        public string ToolTipFormat { get; set; }

        public ETextBoxPosition Position { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool BorderEnabled { get; set; }

        public Color BorderColor { get; set; }

        public Color BackColor { get; set; }

        public ArrayList Items { get; private set; }

        public TTextBox()
            : this(10, 10)
        {
        }

        public TTextBox(int x, int y)
        {
            X = x;
            Y = y;
            Width = -1;
            Height = -1;
            Position = ETextBoxPosition.TopRight;
            BorderEnabled = true;
            BorderColor = Colors.Black;
            BackColor = Colors.LightYellow;
            Items = new ArrayList();
        }

        public void Add(string text, Color color)
        {
            Items.Add(new TTextBoxItem(text, color));
        }

        public void Add(string text, Color color, Font font)
        {
            Items.Add(new TTextBoxItem(text, color, font));
        }

        public void Add(TTextBoxItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public virtual void Draw()
        {
            throw new NotImplementedException();
        }

        private float GetWidth(Pad pad)
        {
            throw new NotImplementedException();
        }

        private float GetHeight(Pad pad)
        {
            throw new NotImplementedException();
        }

        public virtual void Paint(Pad pad, double minX, double maxX, double minY, double maxY)
        {
            throw new NotImplementedException();
        }

        public TDistance Distance(double x, double y)
        {
            return null;
        }
    }
}
