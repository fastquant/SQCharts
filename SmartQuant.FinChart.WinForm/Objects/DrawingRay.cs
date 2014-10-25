// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Drawing;

namespace SmartQuant.FinChart.Objects
{
    public class DrawingRay : IUpdatable
    {
        private int wigth = 1;
        private DateTime x;
        private double y;
        private Color color;
        private bool rangeY;

        public bool RangeY
        {
            get
            {
                return this.rangeY;
            }
            set
            {
                this.rangeY = value;
                this.EmitUpdated();
            }
        }

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                this.EmitUpdated();
            }
        }

        public int Width
        {
            get
            {
                return this.wigth;
            }
            set
            {
                this.wigth = value;
                this.EmitUpdated();
            }
        }

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

        public event EventHandler Updated;

        public DrawingRay(DateTime x, double y, string name)
        {
            this.Name = name;
            this.x = x;
            this.y = y;
        }

        private void EmitUpdated()
        {
            if (Updated != null)
                Updated(this, EventArgs.Empty);
        }
    }
}
