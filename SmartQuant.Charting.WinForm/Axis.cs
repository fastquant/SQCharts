// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Axis
    {
        int fWidth;

        int fHeight;

        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public Color Color { get; set; }

        public EAxisType Type { get; set; }

        public EAxisPosition Position { get; set; }

        public EVerticalGridStyle VerticalGridStyle { get; set; }

        public bool MajorTicksEnabled { get; set; }

        public Color MajorTicksColor { get; set; }

        public float MajorTicksWidth { get; set; }

        public int MajorTicksLength { get; set; }

        public bool MinorTicksEnabled { get; set; }

        public Color MinorTicksColor { get; set; }

        public float MinorTicksWidth { get; set; }

        public int MinorTicksLength { get; set; }

        public EAxisTitlePosition TitlePosition { get; set; }

        public Font TitleFont { get; set; }

        public Color TitleColor { get; set; }

        public int TitleOffset { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public bool Enabled { get; set; }

        public bool Zoomed { get; set; }

        public bool GridEnabled { get; set; }

        public Color GridColor { get; set; }

        public float GridWidth { get; set; }

        public DashStyle GridDashStyle { get; set; }

        public bool MinorGridEnabled { get; set; }

        public Color MinorGridColor { get; set; }

        public float MinorGridWidth { get; set; }

        public DashStyle MinorGridDashStyle { get; set; }

        public bool TitleEnabled { get; set; }

        public bool LabelEnabled { get; set; }

        public Font LabelFont { get; set; }

        public Color LabelColor { get; set; }

        public int LabelOffset { get; set; }

        public string LabelFormat { get; set; }

        public EAxisLabelAlignment LabelAlignment { get; set; }

        public string Title { get; set; }

        public int LastValidAxisWidth{ get; set; }

        public int Width
        {
            get
            {
                throw new  NotImplementedException(); 
            }
            set
            {
                this.fWidth = value;
            }
        }


        public int Height
        {
            get
            {
                throw new  NotImplementedException();

            }
            set
            {
                this.fHeight = value;
            }
        }
    }
}

