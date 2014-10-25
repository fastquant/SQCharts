// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System.Collections;
using System;

#if XWT
using Compatibility.Xwt;
#elif GTK
using Compatibility.Gtk;

#else
using System.Drawing;
using System.Windows.Forms;
using  System.Drawing.Printing;
#endif

namespace SmartQuant.FinChart
{
    public class Pad
    {
        private int width;
        private int height;
        private object selectedObject;

        internal Chart Chart { get; private set; }

        public AxisRight Axis { get; private set; }

        public bool DrawItems { get; set; }

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
                return Chart.Series;
            }
        }

        public ISeries MainSeries
        {
            get
            {
                return Chart.MainSeries;
            }
        }

        public double IntervalWidth
        {
            get
            {
                return Chart.IntervalWidth;
            }
        }

        public int FirstIndex
        {
            get
            {
                return Chart.FirstIndex;
            }
        }

        public int LastIndex
        {
            get
            {
                return Chart.LastIndex;
            }
        }

        public string AxisLabelFormat { get; set; }

        public Graphics Graphics { get; private set; }

        public ArrayList Primitives { get; private set; }

        internal IChartDrawable SelectedPrimitive { get; set; }

        internal int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                X2 = X1 + this.width;
                Axis.SetBounds(X2, Y1, Y2);
            }
        }

        public Pad(Chart chart, int x1, int x2, int y1, int y2)
        {
            Chart = chart;
            Axis = new AxisRight(Chart, this, 0, 0, 0);
            SetCanvas(x1, x2, y1, y2);
            Primitives = ArrayList.Synchronized(new ArrayList());
            DrawGrid = true;
        }

        public void SetCanvas(int x1, int x2, int y1, int y2)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            this.width = X2 - X1;
            this.height = Y2 - Y1;
            Axis.SetBounds(x2, y1, y2);
        }

        #region Primitives Operations

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

        #endregion

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
            var clientY = this.ClientY(y);
            if (DrawGrid)
                Graphics.DrawLine(pen, X1, clientY, X2, clientY);
        }

        public void DrawHorizontalTick(Pen pen, double x, double y, int length)
        {
            var clientY = this.ClientY(y);
            Graphics.DrawLine(pen, (float)x, clientY, (float)x + length, clientY);
        }

        public DateTime GetDateTime(int x)
        {
            return Chart.GetDateTime(x);
        }

        public double WorldY(int y)
        {
            return 0;
//            if (ScaleStyle == PadScaleStyle.Log)
//                return Math.Pow(10.0, (double) (Y2 - y) / (Y2 - Y1) * (Math.Log10(this.maxValue) - Math.Log10(this.minValue))) * this.minValue;
//            else
//                return this.minValue + (this.maxValue - this.minValue) * (double) (Y2 - y) / (double) (Y2 - Y1);
        }

        public virtual void MouseDown(MouseEventArgs Event)
        {
        }

        public virtual void MouseUp(MouseEventArgs Event)
        {
            Chart.ContentUpdated = true;
            Chart.Invalidate();
        }

        public virtual void MouseMove(MouseEventArgs Event)
        {
        }

        public bool IsInRange(double x, double y)
        {
            return  X1 <= x && x <= X2 && Y1 <= y && y <= Y2;
        }

        #region Drawing
        internal void Update(Graphics graphics)
        {
        }
        #endregion
    }
}
