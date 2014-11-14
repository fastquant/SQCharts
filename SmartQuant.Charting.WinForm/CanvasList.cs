// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections;

namespace SmartQuant.Charting
{
    public class CanvasList : SortedList
    {
        public Canvas this[string name]
        {
            get
            {
                return base[name] as Canvas;
            }
        }

        public void Add(Canvas canvas)
        {
            Add(canvas.Name, canvas);
        }

        public void Remove(Canvas canvas)
        {
            Remove(canvas.Name);
        }

        public void Print()
        {
            foreach (Canvas canvas in this)
                canvas.Print();
        }
    }
}
