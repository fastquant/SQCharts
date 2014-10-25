// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.
using System;
using System.Collections.Generic;
using System.Collections;
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
    public class TLegend
    {
        private int width;
        private int height;


        public Pad Pad { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.height = value;
            }
        }

        public bool BorderEnabled { get; set; }

        public Color BorderColor { get; set; }

        public Color BackColor { get; set; }

        public ArrayList Items { get; private set; }
        public TLegend(Pad pad)
        {
            Pad = pad;
            BorderEnabled = true;
            BorderColor = Colors.Black;
            BackColor = Colors.LightYellow;
            Items = new ArrayList();
        }

        public void Add(string text, Color color)
        {
            Items.Add(new TLegendItem(text, color));
        }

        public void Add(string text, Color color, Font font)
        {
            Items.Add(new TLegendItem(text, color, font));
        }

        public void Add(TLegendItem item)
        {
            Items.Add(item);
        }

        public virtual void Paint()
        {
            throw new NotImplementedException();
        }
    }
}
