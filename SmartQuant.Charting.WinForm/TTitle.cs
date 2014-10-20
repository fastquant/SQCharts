// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;
#if XWT
using Xwt.Drawing;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class TTitle
    {
        private Pad pad;

        public ArrayList Items { get; private set; }

        public bool ItemsEnabled { get; set; }

        public string Text { get; set; }

        public Font Font { get; set; }

        public Color Color { get; set; }

        public ETitlePosition Position { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Height
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ETitleStrategy Strategy { get; set; }

        public TTitle(Pad pad, string text = "")
        {
            this.pad = pad;
            Text = Text;
            Items = new ArrayList();
            ItemsEnabled = false;
            Font = Fonts.SystemFont();
            Color = Colors.Black;
            Position = ETitlePosition.Left;
            Strategy = ETitleStrategy.Smart;
            X = 0;
            Y = 0;
        }

        public void Add(string text, Color color)
        {
            Items.Add(new TTitleItem(text, color));
        }

        public void Paint()
        {
        }
    }
}

