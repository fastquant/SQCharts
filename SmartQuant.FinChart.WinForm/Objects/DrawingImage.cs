// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
#if XWT
using Xwt.Drawing;
using Compatibility.Xwt;
#elif GTK
using Gdk;
using Compatibility.Gtk;
#else
using System.Drawing;
#endif

namespace SmartQuant.FinChart.Objects
{
    public class DrawingImage : IUpdatable
    {
        private DateTime x;
        private double y;
        private Image image;

        public string Name { get; private set; }

        public DateTime X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
                this.EmitUpdated();
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
                this.EmitUpdated();
            }
        }

        public Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                this.EmitUpdated();
            }
        }

        public event EventHandler Updated;

        public DrawingImage(DateTime x, double y, Image image, string name)
        {
            this.Name = name;
            this.x = x;
            this.y = y;
            this.image = image;
        }

        private void EmitUpdated()
        {
            if (Updated != null)
                Updated(this, EventArgs.Empty);
        }
    }
}
