// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public class CanvasManager
    {
        public static CanvasList Canvases { get; private set; }

        static CanvasManager()
        {
            Canvases = new CanvasList();
        }
            
        public static void Add(Canvas canvas)
        {
            if (Canvases[canvas.Name] != null)
                Canvases.Remove(canvas.Name);
            Canvases.Add(canvas.Name, canvas);
        }

        public static void Remove(Canvas canvas)
        {
            Canvases.Remove(canvas.Name);
        }

        public static Canvas GetCanvas(string name)
        {
            return Canvases[name];
        }
    }
}
