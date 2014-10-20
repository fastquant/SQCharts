// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Drawing;
using System.Collections;
using System;
using PrintPageEventArgs = System.Drawing.Printing.PrintPageEventArgs;
using PaintEventArgs = System.Windows.Forms.PaintEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace SmartQuant.FinChart
{
    public class Pad
    {
        public Chart Chart
        {
            get
            {
                return chart;
            }
            set
            {
            }
        }

        //        private bool drawGrid = true;
        private Chart chart;
        //        private AxisRight axis;
        //        private ArrayList primitives;
        //        private ArrayList simplePrimitives;
        //        private SortedRangeList rangeList;
        //        private SortedRangeList intervalLeftList;
        //        private SortedRangeList intervalRightList;
        //        private int axisGap;
        private object selectedObject;
        //        private int x1;
        //        private int x2;
        //        private int y1;
        //        private int y2;
        //        private int width;
        //        private int height;
        //        private int marginLeft;
        //        private int marginRight;
        //        private double maxValue;
        //        private double minValue;
        private Graphics graphics;
        private IChartDrawable selectedPrimitive;
        //        private bool onPrimitive;
        //        private bool outlineEnabled;
        //        private Rectangle outlineRectangle;
        //        private PadScaleStyle scaleStyle;
        //        private string axisLabelFormat;
        //        private bool drawItems;

        public AxisRight Axis { get; private set; }

        public bool DrawItems { get; set; }

        //        public string AxisLabelFormat
        //        {
        //            get
        //            {
        //                if (this.axisLabelFormat == null)
        //                    return "F" + this.chart.LabelDigitsCount.ToString();
        //                else
        //                    return this.axisLabelFormat;
        //            }
        //            set
        //            {
        //                this.axisLabelFormat = value;
        //            }
        //        }

        public bool DrawGrid { get; set; }

        public PadScaleStyle ScaleStyle { get; set; }

        public int X1 { get; private set; }

        public int X2  { get; private set; }

        public int Y1 { get; private set; }

        public int Y2 { get; private set; }

        public double MaxValue { get; private set; }

        public double MinValue { get; private set; }

        public ISeries Series
        {
            get
            {
                return this.chart.Series;
            }
        }

        public ISeries MainSeries
        {
            get
            {
                return this.chart.MainSeries;
            }
        }

        public double IntervalWidth
        {
            get
            {
                return this.chart.IntervalWidth;
            }
        }

        public int FirstIndex
        {
            get
            {
                return this.chart.FirstIndex;
            }
        }

        public int LastIndex
        {
            get
            {
                return this.chart.LastIndex;
            }
        }

        public Graphics Graphics { get; private set; }

        public ArrayList Primitives { get; private set; }

        public Pad(Chart chart, int x1, int x2, int y1, int y2)
        {
            this.chart = chart;
            this.SetCanvas(x1, x2, y1, y2);
            this.Primitives = ArrayList.Synchronized(new ArrayList());
        }

        public void SetCanvas(int x1, int x2, int y1, int y2)
        {
            Y1 = y1;
            Y2 = y2;
//            X1 = x1 + this.MarginLeft;
//            this.x2 = x2 - this.marginRight;
//            this.width = this.x2 - this.x1;
//            this.height = this.y2 - this.y1;
//            if (this.axis == null)
//                this.axis = new AxisRight(this.chart, this, x2, y1, y2);
//            else
//                this.axis.SetBounds(x2, y1, y2);
        }

        public void AddPrimitive(IChartDrawable primitive)
        {
            Primitives.Add(primitive);
        }

        public void RemovePrimitive(IChartDrawable primitive)
        {
            Primitives.Remove(primitive);
        }

        public void ClearPrimitives()
        {
            Primitives.Clear();
        }

        public void SetSelectedObject(object obj)
        {
            this.selectedObject = obj;
        }

        public int ClientX(DateTime dateTime)
        {
            throw new NotImplementedException(); 
        }

        public int ClientY(double worldY)
        {
            throw new NotImplementedException(); 

        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            foreach (IChartDrawable d in Primitives)
                d.SetInterval(minDate, maxDate);
        }

        public void DrawHorizontalGrid(Pen pen, double y)
        {
        }

        public void DrawHorizontalTick(Pen pen, double x, double y, int length)
        {
            this.graphics.DrawLine(pen, (int)x, this.ClientY(y), (int)x + length, this.ClientY(y));
        }

        public DateTime GetDateTime(int x)
        {
            return this.chart.GetDateTime(x);
        }

        public double WorldY(int y)
        {
            throw new NotImplementedException(); 

        }

        public virtual void MouseDown(MouseEventArgs Event)
        {

        }

        public virtual void MouseUp(MouseEventArgs Event)
        {
            this.chart.ContentUpdated = true;
            this.chart.Invalidate();
        }

        public virtual void MouseMove(MouseEventArgs Event)
        {

        }

        public bool IsInRange(double x, double y)
        {
            return  this.X1 <= x && x <= this.X2 && this.Y1 <= y && y <= this.Y2;
        }
    }
}
