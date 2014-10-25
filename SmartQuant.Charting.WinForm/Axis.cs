// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#elif GTK
using Gdk;
using Compatibility.Gtk;
using Font = Compatibility.Gtk.Font;
#else
using System.Windows.Forms;
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public class Axis
    {
        private int width;
        private int height;
        private Pad pad;

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
                this.width = value;
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
                this.height = value;
            }
        }

        public Axis(Pad pad)
            : this(pad, EAxisPosition.None)
        {
        }

        public Axis(Pad pad, EAxisPosition position)
            : this(pad, position, 0, 0, 0, 0)
        {
        }

        public Axis(Pad pad, double x1, double y1, double x2, double y2)
            : this(pad, EAxisPosition.None, x1, y1, x2, y2)
        {
        }

        private Axis(Pad pad, EAxisPosition position, double x1, double y1, double x2, double y2)
        {
            this.pad = pad;
            Position = position;
            SetLocation(x1, y1, x2, y2);
            this.width = -1;
            this.height = -1;
            Enabled = true;
            Zoomed = false;
            Color = Colors.Black;

            // title
            TitleEnabled = true;
            Title = "";
            TitlePosition = EAxisTitlePosition.Centre;
            TitleFont = new Font("Arial", 8f);
            TitleColor = Colors.Black;
            TitleOffset = 2;

            // label
            LabelEnabled = true;
            LabelFont = new Font("Arial", 8f);
            LabelColor = Colors.Black;
            LabelFormat =  null;
            LabelOffset = 2;
            LabelAlignment = EAxisLabelAlignment.Centre;

            // grid
            GridEnabled = true;
            GridColor = Colors.Gray;
            GridDashStyle = DashStyle.Solid;
            GridWidth = 0.5f;
            MinorGridEnabled = false;
            MinorGridColor = Colors.Gray;
            MinorGridDashStyle = DashStyle.Solid;
            MinorGridWidth = 0.5f;
            MajorTicksEnabled = true;
            MajorTicksColor = Colors.Black;
            MajorTicksWidth = 0.5f;
            MajorTicksLength = 4;
            MinorTicksEnabled = true;
            MinorTicksColor = Colors.Black;
            MinorTicksWidth = 0.5f;
            MinorTicksLength = 1;
            Type = EAxisType.Numeric;
            VerticalGridStyle = EVerticalGridStyle.ByDateTime;

            // mouse
//            this.fMouseDown = false;
//            this.fMouseDownX = 0;
//            this.fMouseDownY = 0;
//            this.fOutlineEnabled = false;
//            this.fOutline1 = 0;
//            this.fOutline2 = 0;
        }

        public void SetLocation(double x1, double y1, double x2, double y2)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

        public void SetRange(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public void Zoom(double min, double max)
        {
            Min = min;
            Max = max;
            Zoomed = true;
            this.pad.EmitZoom(true);
            if (!this.pad.Chart.GroupZoomEnabled)
                this.pad.Update();
        }

        public void Zoom(DateTime min, DateTime max)
        {
            Zoom(min.Ticks, max.Ticks);
        }

        public void Zoom(string min, string max)
        {
            Zoom(DateTime.Parse(min), DateTime.Parse(max));
        }

        public void UnZoom()
        {
            switch (this.Position)
            {
                case EAxisPosition.Left:
                    this.SetRange(this.pad.YMin, this.pad.YMax);
                    break;
                case EAxisPosition.Bottom:
                    this.SetRange(this.pad.XMin, this.pad.XMax);
                    break;
            }
            Zoomed = false;
            this.pad.EmitZoom(false);
            if (!this.pad.Chart.GroupZoomEnabled)
                this.pad.Update();
        }

        public static long GetNextMajor(long prevMajor, EGridSize gridSize)
        {
            long num;
            switch (gridSize)
            {
                case EGridSize.year5:
                    num = new DateTime(prevMajor).AddYears(5).Ticks;
                    break;
                case EGridSize.year10:
                    num = new DateTime(prevMajor).AddYears(10).Ticks;
                    break;
                case EGridSize.year20:
                    num = new DateTime(prevMajor).AddYears(20).Ticks;
                    break;
                case EGridSize.year2:
                    num = new DateTime(prevMajor).AddYears(2).Ticks;
                    break;
                case EGridSize.year3:
                    num = new DateTime(prevMajor).AddYears(3).Ticks;
                    break;
                case EGridSize.year4:
                    num = new DateTime(prevMajor).AddYears(4).Ticks;
                    break;
                case EGridSize.month4:
                    num = new DateTime(prevMajor).AddMonths(4).Ticks;
                    break;
                case EGridSize.month6:
                    num = new DateTime(prevMajor).AddMonths(6).Ticks;
                    break;
                case EGridSize.year1:
                    num = new DateTime(prevMajor).AddYears(1).Ticks;
                    break;
                case EGridSize.month1:
                    num = new DateTime(prevMajor).AddMonths(1).Ticks;
                    break;
                case EGridSize.month2:
                    num = new DateTime(prevMajor).AddMonths(2).Ticks;
                    break;
                case EGridSize.month3:
                    num = new DateTime(prevMajor).AddMonths(3).Ticks;
                    break;
                default:
                    num = (long)(prevMajor + gridSize);
                    break;
            }
            return num;
        }

        public static EGridSize CalculateSize(double ticks)
        {
            double num1 = 10;
            double num2 = 3;
            double minutes = Math.Floor(ticks / TimeSpan.TicksPerMinute);
            if ((double)num2 <= minutes && minutes <= (double)num1)
                return EGridSize.min1;
            double num4 = minutes / 2.0;
            if (num4 >= (double)num2 && num4 <= (double)num1)
                return EGridSize.min2;
            double num5 = num4 / 2.5;
            if (num5 >= (double)num2 && num5 <= (double)num1)
                return EGridSize.min5;
            double num6 = num5 / 2.0;
            if (num6 >= (double)num2 && num6 <= (double)num1)
                return EGridSize.min10;
            double num7 = num6 / 1.5;
            if (num7 >= (double)num2 && num7 <= (double)num1)
                return EGridSize.min15;
            double num8 = num7 / (4.0 / 3.0);
            if (num8 >= (double)num2 && num8 <= (double)num1)
                return EGridSize.min20;
            double num9 = num8 / 1.5;
            if (num9 >= (double)num2 && num9 <= (double)num1)
                return EGridSize.min30;
            double num10 = num9 / 2.0;
            if (num10 >= (double)num2 && num10 <= (double)num1)
                return EGridSize.hour1;
            double num11 = num10 / 2.0;
            if (num11 >= (double)num2 && num11 <= (double)num1)
                return EGridSize.hour2;
            double num12 = num11 / 1.5;
            if (num12 >= (double)num2 && num12 <= (double)num1)
                return EGridSize.hour3;
            double num13 = num12 / (4.0 / 3.0);
            if (num13 >= (double)num2 && num13 <= (double)num1)
                return EGridSize.hour4;
            double num14 = num13 / 1.5;
            if (num14 >= (double)num2 && num14 <= (double)num1)
                return EGridSize.hour6;
            double num15 = num14 / 2.0;
            if (num15 >= (double)num2 && num15 <= (double)num1)
                return EGridSize.hour12;
            double num16 = num15 / 2.0;
            if (num16 >= (double)num2 && num16 <= (double)num1)
                return EGridSize.day1;
            double num17 = num16 / 2.0;
            if (num17 >= (double)num2 && num17 <= (double)num1)
                return EGridSize.day2;
            double num18 = num17 / 1.5;
            if (num18 >= (double)num2 && num18 <= (double)num1)
                return EGridSize.day3;
            double num19 = num18 / (5.0 / 3.0);
            if (num19 >= (double)num2 && num19 <= (double)num1)
                return EGridSize.day5;
            double num20 = num19 / 1.4;
            if (num20 >= (double)num2 && num20 <= (double)num1)
                return EGridSize.week1;
            double num21 = num20 / 2.0;
            if (num21 >= (double)num2 && num21 <= (double)num1)
                return EGridSize.week2;
            double num22 = num21 / (15.0 / 7.0);
            if (num22 >= (double)num2 && num22 <= (double)num1)
                return EGridSize.month1;
            double num23 = num22 / 2.0;
            if (num23 >= (double)num2 && num23 <= (double)num1)
                return EGridSize.month2;
            double num24 = num23 / 1.5;
            if (num24 >= (double)num2 && num24 <= (double)num1)
                return EGridSize.month3;
            double num25 = num24 / (4.0 / 3.0);
            if (num25 >= (double)num2 && num25 <= (double)num1)
                return EGridSize.month4;
            double num26 = num25 / 1.5;
            if (num26 >= (double)num2 && num26 <= (double)num1)
                return EGridSize.month6;
            double num27 = num26 / 2.0;
            if (num27 >= (double)num2 && num27 <= (double)num1)
                return EGridSize.year1;
            double num28 = num27 / 2.0;
            if (num28 >= (double)num2 && num28 <= (double)num1)
                return EGridSize.year2;
            double num29 = num28 / 1.5;
            if (num29 >= (double)num2 && num29 <= (double)num1)
                return EGridSize.year3;
            double num30 = num29 / (4.0 / 3.0);
            if (num30 >= (double)num2 && num30 <= (double)num1)
                return EGridSize.year4;
            double num31 = num30 / 0.8;
            if (num31 >= (double)num2 && num31 <= (double)num1)
                return EGridSize.year5;
            double num32 = num31 / 2.0;
            if (num32 >= (double)num2 && num32 <= (double)num1)
                return EGridSize.year10;
            double num33 = num32 / 2.0;
            return num33 >= (double)num2 && num33 <= (double)num1 ? EGridSize.year20 : EGridSize.year20;
        }

        public void PaintWithDates()
        {
        }

        public virtual void Paint()
        {
        }

        public virtual void MouseMove(MouseEventArgs me)
        {
        }

        public virtual void MouseDown(MouseEventArgs me)
        {
        }

        public virtual void MouseUp(MouseEventArgs me)
        {
        }
    }
}
