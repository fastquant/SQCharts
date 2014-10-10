﻿// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.FinChart.Objects
{
    public class DrawingPoint : IUpdatable
    {
        private DateTime x;
        private double y;

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

        public DrawingPoint(DateTime x, double y)
        {
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
