// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace SmartQuant.FinChart
{
    public class Axis
    {
        protected Chart chart;
        protected EAxisTitlePosition titlePosition;
        protected bool enabled;
        protected bool zoomed;
        protected Color color;
        protected bool titleEnabled;
        protected string title;
        protected Font titleFont;
        protected Color titleColor;
        protected int titleOffset;
        protected bool labelEnabled;
        protected Font labelFont;
        protected Color labelColor;
        protected int labelOffset;
        protected string labelFormat;
        protected EAxisLabelAlignment labelAlignment;
        protected bool gridEnabled;
        protected Color gridColor;
        protected float gridWidth;
        protected DashStyle gridDashStyle;
        protected bool minorGridEnabled;
        protected Color minorGridColor;
        protected float minorGridWidth;
        protected DashStyle minorGridDashStyle;
        protected bool majorTicksEnabled;
        protected Color majorTicksColor;
        protected float majorTicksWidth;
        protected int majorTicksLength;
        protected bool minorTicksEnabled;
        protected Color minorTicksColor;
        protected float minorTicksWidth;
        protected int minorTicksLength;
        protected double min;
        protected double max;
        protected int width;
        protected int height;

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public bool MajorTicksEnabled
        {
            get
            {
                return this.majorTicksEnabled;
            }
            set
            {
                this.majorTicksEnabled = value;
            }
        }

        public Color MajorTicksColor
        {
            get
            {
                return this.majorTicksColor;
            }
            set
            {
                this.majorTicksColor = value;
            }
        }

        public float MajorTicksWidth
        {
            get
            {
                return this.majorTicksWidth;
            }
            set
            {
                this.majorTicksWidth = value;
            }
        }

        public int MajorTicksLength
        {
            get
            {
                return this.majorTicksLength;
            }
            set
            {
                this.majorTicksLength = value;
            }
        }

        public bool MinorTicksEnabled
        {
            get
            {
                return this.minorTicksEnabled;
            }
            set
            {
                this.minorTicksEnabled = value;
            }
        }

        public Color MinorTicksColor
        {
            get
            {
                return this.minorTicksColor;
            }
            set
            {
                this.minorTicksColor = value;
            }
        }

        public float MinorTicksWidth
        {
            get
            {
                return this.minorTicksWidth;
            }
            set
            {
                this.minorTicksWidth = value;
            }
        }

        public int MinorTicksLength
        {
            get
            {
                return this.minorTicksLength;
            }
            set
            {
                this.minorTicksLength = value;
            }
        }

        public EAxisTitlePosition TitlePosition
        {
            get
            {
                return this.titlePosition;
            }
            set
            {
                this.titlePosition = value;
            }
        }

        public Font TitleFont
        {
            get
            {
                return this.titleFont;
            }
            set
            {
                this.titleFont = value;
            }
        }

        public Color TitleColor
        {
            get
            {
                return this.titleColor;
            }
            set
            {
                this.titleColor = value;
            }
        }

        public int TitleOffset
        {
            get
            {
                return this.titleOffset;
            }
            set
            {
                this.titleOffset = value;
            }
        }

        public double Min
        {
            get
            {
                return this.min;
            }
            set
            {
                this.min = value;
            }
        }

        public double Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        public bool Zoomed
        {
            get
            {
                return this.zoomed;
            }
            set
            {
                this.zoomed = value;
            }
        }

        public bool GridEnabled
        {
            get
            {
                return this.gridEnabled;
            }
            set
            {
                this.gridEnabled = value;
            }
        }

        public Color GridColor
        {
            get
            {
                return this.gridColor;
            }
            set
            {
                this.gridColor = value;
            }
        }

        public float GridWidth
        {
            get
            {
                return this.gridWidth;
            }
            set
            {
                this.gridWidth = value;
            }
        }

        public DashStyle GridDashStyle
        {
            get
            {
                return this.gridDashStyle;
            }
            set
            {
                this.gridDashStyle = value;
            }
        }

        public bool MinorGridEnabled
        {
            get
            {
                return this.minorGridEnabled;
            }
            set
            {
                this.minorGridEnabled = value;
            }
        }

        public Color MinorGridColor
        {
            get
            {
                return this.minorGridColor;
            }
            set
            {
                this.minorGridColor = value;
            }
        }

        public float MinorGridWidth
        {
            get
            {
                return this.minorGridWidth;
            }
            set
            {
                this.minorGridWidth = value;
            }
        }

        public DashStyle MinorGridDashStyle
        {
            get
            {
                return this.minorGridDashStyle;
            }
            set
            {
                this.minorGridDashStyle = value;
            }
        }

        public bool TitleEnabled
        {
            get
            {
                return this.titleEnabled;
            }
            set
            {
                this.titleEnabled = value;
            }
        }

        public bool LabelEnabled
        {
            get
            {
                return this.labelEnabled;
            }
            set
            {
                this.labelEnabled = value;
            }
        }

        public Font LabelFont
        {
            get
            {
                return this.labelFont;
            }
            set
            {
                this.labelFont = value;
            }
        }

        public Color LabelColor
        {
            get
            {
                return this.labelColor;
            }
            set
            {
                this.labelColor = value;
            }
        }

        public int LabelOffset
        {
            get
            {
                return this.labelOffset;
            }
            set
            {
                this.labelOffset = value;
            }
        }

        public string LabelFormat
        {
            get
            {
                return this.labelFormat;
            }
            set
            {
                this.labelFormat = value;
            }
        }

        public EAxisLabelAlignment LabelAlignment
        {
            get
            {
                return this.labelAlignment;
            }
            set
            {
                this.labelAlignment = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
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
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
    }

    public class AxisBottom : Axis
    {
        protected double x1;
        protected double x2;
        protected double y;

        public double X1
        {
            get
            {
                return this.x1;
            }
            set
            {
                this.x1 = value;
            }
        }

        public double X2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
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
            }
        }

        public AxisBottom(Chart chart, int x1, int x2, int y)
        {
            this.chart = chart;
            this.x1 = (double) x1;
            this.x2 = (double) x2;
            this.y = (double) y;
            this.Init();
        }

        private void Init()
        {
            this.enabled = true;
            this.zoomed = false;
            this.color = Color.LightGray;
            this.title = "";
            this.titleEnabled = true;
            this.titlePosition = EAxisTitlePosition.Centre;
            this.titleFont = this.chart.Font;
            this.titleColor = Color.Black;
            this.titleOffset = 2;
            this.labelEnabled = true;
            this.labelFont = this.chart.Font;
            this.labelColor = Color.LightGray;
            this.labelFormat = (string) null;
            this.labelOffset = 2;
            this.labelAlignment = EAxisLabelAlignment.Centre;
            this.gridEnabled = true;
            this.gridColor = Color.LightGray;
            this.gridDashStyle = DashStyle.DashDotDot;
            this.gridWidth = 0.5f;
            this.minorGridEnabled = false;
            this.minorGridColor = Color.LightGray;
            this.minorGridDashStyle = DashStyle.Solid;
            this.minorGridWidth = 0.5f;
            this.majorTicksEnabled = true;
            this.majorTicksColor = Color.LightGray;
            this.majorTicksWidth = 0.5f;
            this.majorTicksLength = 4;
            this.minorTicksEnabled = true;
            this.minorTicksColor = Color.LightGray;
            this.minorTicksWidth = 0.5f;
            this.minorTicksLength = 1;
            this.width = -1;
            this.height = -1;
        }

        public void SetBounds(int x1, int x2, int y)
        {
            this.x1 = (double) x1;
            this.x2 = (double) x2;
            this.y = (double) y;
        }

        private long GetGridDivision(DateTime dateTime, EGridSize gridSize)
        {
            long num;
            switch (gridSize)
            {
                case EGridSize.year5:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (4 - dateTime.Year % 5)).Ticks;
                    break;
                case EGridSize.year10:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (9 - dateTime.Year % 10)).Ticks;
                    break;
                case EGridSize.year20:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (19 - dateTime.Year % 20)).Ticks;
                    break;
                case EGridSize.year3:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (2 - dateTime.Year % 3)).Ticks;
                    break;
                case EGridSize.year4:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (3 - dateTime.Year % 4)).Ticks;
                    break;
                case EGridSize.month6:
                    DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);
                    dateTime1 = dateTime1.AddMonths(1 + (12 - dateTime.Month) % 6);
                    num = dateTime1.Ticks;
                    break;
                case EGridSize.year1:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1).Ticks;
                    break;
                case EGridSize.year2:
                    num = new DateTime(dateTime.Year, 1, 1).AddYears(1 + (1 - dateTime.Year % 2)).Ticks;
                    break;
                case EGridSize.month3:
                    DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, 1);
                    dateTime2 = dateTime2.AddMonths(1 + (12 - dateTime.Month) % 3);
                    num = dateTime2.Ticks;
                    break;
                case EGridSize.month4:
                    DateTime dateTime3 = new DateTime(dateTime.Year, dateTime.Month, 1);
                    dateTime3 = dateTime3.AddMonths(1 + (12 - dateTime.Month) % 4);
                    num = dateTime3.Ticks;
                    break;
                case EGridSize.week2:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(8.0 - (double) dateTime.DayOfWeek + (double) (7 * (1 - (int) Math.Floor(new TimeSpan(dateTime.AddDays(8.0 - (double) dateTime.DayOfWeek).Ticks).TotalDays) / 7 % 2))).Ticks;
                    break;
                case EGridSize.month1:
                    DateTime dateTime4 = new DateTime(dateTime.Year, dateTime.Month, 1);
                    dateTime4 = dateTime4.AddMonths(1);
                    num = dateTime4.Ticks;
                    break;
                case EGridSize.month2:
                    DateTime dateTime5 = new DateTime(dateTime.Year, dateTime.Month, 1);
                    dateTime5 = dateTime5.AddMonths(1 + dateTime.Month % 2);
                    num = dateTime5.Ticks;
                    break;
                case EGridSize.day5:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays((double) (1 + (4 - (int) new TimeSpan(dateTime.Ticks).TotalDays % 5))).Ticks;
                    break;
                case EGridSize.week1:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(8.0 - (double) dateTime.DayOfWeek).Ticks;
                    break;
                case EGridSize.day2:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays((double) (1 + (int) new TimeSpan(dateTime.Ticks).TotalDays % 2)).Ticks;
                    break;
                case EGridSize.day3:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays((double) (1 + (2 - (int) new TimeSpan(dateTime.Ticks).TotalDays % 3))).Ticks;
                    break;
                case EGridSize.hour12:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours((double) (1 + (11 - (int) new TimeSpan(dateTime.Ticks).TotalHours % 12))).Ticks;
                    break;
                case EGridSize.day1:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1.0).Ticks;
                    break;
                case EGridSize.hour3:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours((double) (1 + (2 - (int) new TimeSpan(dateTime.Ticks).TotalHours % 3))).Ticks;
                    break;
                case EGridSize.hour4:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours((double) (1 + (3 - (int) new TimeSpan(dateTime.Ticks).TotalHours % 4))).Ticks;
                    break;
                case EGridSize.hour6:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours((double) (1 + (5 - (int) new TimeSpan(dateTime.Ticks).TotalHours % 6))).Ticks;
                    break;
                case EGridSize.hour1:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours(1.0).Ticks;
                    break;
                case EGridSize.hour2:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours((double) (1 + (1 - (int) new TimeSpan(dateTime.Ticks).TotalHours % 2))).Ticks;
                    break;
                case EGridSize.min20:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (19 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 20))).Ticks;
                    break;
                case EGridSize.min30:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (29 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 30))).Ticks;
                    break;
                case EGridSize.min10:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (9 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 10))).Ticks;
                    break;
                case EGridSize.min15:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (14 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 15))).Ticks;
                    break;
                case EGridSize.min1:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes(1.0).Ticks;
                    break;
                case EGridSize.min2:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (1 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 2))).Ticks;
                    break;
                case EGridSize.min5:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes((double) (1 + (4 - (int) new TimeSpan(dateTime.Ticks).TotalMinutes % 5))).Ticks;
                    break;
                case EGridSize.sec20:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds((double) (1 + (19 - (int) new TimeSpan(dateTime.Ticks).TotalSeconds % 20))).Ticks;
                    break;
                case EGridSize.sec30:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds((double) (1 + (29 - (int) new TimeSpan(dateTime.Ticks).TotalSeconds % 30))).Ticks;
                    break;
                case EGridSize.sec5:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds((double) (1 + (4 - (int) new TimeSpan(dateTime.Ticks).TotalSeconds % 5))).Ticks;
                    break;
                case EGridSize.sec10:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds((double) (1 + (9 - (int) new TimeSpan(dateTime.Ticks).TotalSeconds % 10))).Ticks;
                    break;
                case EGridSize.sec1:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds(1.0).Ticks;
                    break;
                case EGridSize.sec2:
                    num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).AddSeconds((double) (1 + (1 - (int) new TimeSpan(dateTime.Ticks).TotalSeconds % 2))).Ticks;
                    break;
                default:
                    num = (long) (dateTime.Ticks + gridSize);
                    break;
            }
            return num;
        }

        public void PaintWithDates(DateTime minDate, DateTime maxDate)
        {
            SolidBrush solidBrush1 = new SolidBrush(this.titleColor);
            SolidBrush solidBrush2 = new SolidBrush(this.labelColor);
            Pen Pen1 = new Pen(this.titleColor);
            Pen Pen2 = new Pen(this.gridColor);
            Pen pen1 = new Pen(this.minorGridColor);
            Pen pen2 = new Pen(this.minorTicksColor);
            Pen pen3 = new Pen(this.majorTicksColor);
            Pen2.Width = this.gridWidth;
            Pen2.DashStyle = this.gridDashStyle;
            pen1.Width = this.minorGridWidth;
            pen1.DashStyle = this.minorGridDashStyle;
            long ticks1 = minDate.Ticks;
            long ticks2 = maxDate.Ticks;
            DateTime dateTime1 = new DateTime(Math.Max(0L, ticks1));
            EGridSize egridSize = AxisBottom.CalculateSize((double) (ticks2 - ticks1));
            long num1 = 0L;
            long gridDivision = this.GetGridDivision(dateTime1, egridSize);
            int num2 = 0;
            long num3 = gridDivision;
            long num4 = 0L;
            int num5 = 0;
            long num6 = ticks2;
            int num7 = -1;
            while (num3 < num6)
            {
                if (num5 != 0)
                    num3 = AxisBottom.GetNextMajor(num4, egridSize);
                long num8 = num3;
                int index = this.chart.MainSeries.GetIndex(new DateTime(num3 - 1L), IndexOption.Next);
                if (num7 == index)
                {
                    num4 = num3;
                }
                else
                {
                    num7 = index;
                    if (index != -1)
                    {
                        DateTime dateTime2 = this.chart.MainSeries.GetDateTime(index);
                        TimeSpan timeOfDay = dateTime2.TimeOfDay;
                        long ticks3 = dateTime2.Ticks;
                        if (ticks3 < num6)
                        {
                            if (this.gridEnabled)
                                this.chart.DrawVerticalGrid(Pen2, ticks3);
                            if (this.majorTicksEnabled)
                                this.chart.DrawVerticalTick(Pen1, ticks3, -5);
                            if (this.labelEnabled)
                            {
                                string format;
                                if (ticks3 % 864000000000L == this.chart.SessionStart.Ticks || ticks3 % 864000000000L == this.chart.SessionEnd.Ticks)
                                    format = num4 != 0L ? (new DateTime(num4).Year == new DateTime(ticks3).Year ? "MMM dd" : "yyyy MMM dd") : "yyy MMM dd";
                                else if (num4 == 0L)
                                {
                                    format = "yyy MMM dd HH:mm";
                                }
                                else
                                {
                                    DateTime dateTime3 = new DateTime(num4);
                                    DateTime dateTime4 = new DateTime(ticks3);
                                    format = dateTime3.Year == dateTime4.Year ? (dateTime3.Month == dateTime4.Month ? (dateTime3.Day == dateTime4.Day ? (dateTime3.Minute != dateTime4.Minute || dateTime3.Hour != dateTime4.Hour ? "HH:mm" : "HH:mm:ss") : "MMM dd HH:mm") : "MMM dd HH:mm") : "yyy MMM dd HH:mm";
                                }
                                string str = new DateTime(ticks3).ToString(format);
                                SizeF sizeF = this.chart.Graphics.MeasureString(str, this.labelFont);
                                int num9 = (int) sizeF.Width;
                                int num10 = (int) sizeF.Height;
                                if (this.labelAlignment == EAxisLabelAlignment.Right)
                                    this.chart.Graphics.DrawString(str, this.labelFont, (Brush) solidBrush2, (float) this.chart.ClientX(new DateTime(ticks3)), (float) (int) (this.y + (double) this.labelOffset));
                                if (this.labelAlignment == EAxisLabelAlignment.Left)
                                    this.chart.Graphics.DrawString(str, this.labelFont, (Brush) solidBrush2, (float) (this.chart.ClientX(new DateTime(ticks3)) - num9), (float) (int) (this.y + (double) this.labelOffset));
                                if (this.labelAlignment == EAxisLabelAlignment.Centre)
                                {
                                    int num11 = this.chart.ClientX(new DateTime(ticks3)) - num9 / 2;
                                    int num12 = (int) (this.y + (double) this.labelOffset);
                                    if (num5 == 0 || num11 - num2 >= 1)
                                    {
                                        this.chart.Graphics.DrawString(str, this.labelFont, (Brush) solidBrush2, (float) num11, (float) num12);
                                        num2 = num11 + num9;
                                    }
                                }
                            }
                        }
                        num1 = ticks3;
                        num3 = num8;
                        num4 = num3;
                        ++num5;
                    }
                }
            }
            if (this.chart.SessionGridEnabled && (EGridSize) (this.chart.SessionEnd - this.chart.SessionStart).Ticks >= egridSize)
            {
                int num8 = 0;
                long X;
                for (long index = ticks1 / 864000000000L * 864000000000L + this.chart.SessionStart.Ticks; (X = index + (long) num8 * 864000000000L) < ticks2; ++num8)
                    this.chart.DrawSessionGrid(new Pen(this.chart.SessionGridColor), X);
            }
            if (!this.titleEnabled)
                return;
            int num13 = (int) this.chart.Graphics.MeasureString("Example", this.labelFont).Height;
            int num14 = (int) this.chart.Graphics.MeasureString(ticks2.ToString("F1"), this.labelFont).Width;
            double num15 = (double) this.chart.Graphics.MeasureString(this.title, this.titleFont).Height;
            int num16 = (int) this.chart.Graphics.MeasureString(this.title, this.titleFont).Width;
            if (this.titlePosition == EAxisTitlePosition.Left)
                this.chart.Graphics.DrawString(this.title, this.titleFont, (Brush) solidBrush1, (float) (int) this.x1, (float) (int) (this.y + (double) this.labelOffset + (double) num13 + (double) this.titleOffset));
            if (this.titlePosition == EAxisTitlePosition.Right)
                this.chart.Graphics.DrawString(this.title, this.titleFont, (Brush) solidBrush1, (float) ((int) this.x2 - num16), (float) (int) (this.y + (double) this.labelOffset + (double) num13 + (double) this.titleOffset));
            if (this.titlePosition != EAxisTitlePosition.Centre)
                return;
            this.chart.Graphics.DrawString(this.title, this.titleFont, (Brush) solidBrush1, (float) (int) (this.x1 + (this.x2 - this.x1 - (double) num16) / 2.0), (float) (int) (this.y + (double) this.labelOffset + (double) num13 + (double) this.titleOffset));
        }

        public static EGridSize CalculateSize(double ticks)
        {
            int num1 = 7;
            int num2 = 3;
            double num3 = Math.Floor(ticks / 10000000.0);
            if (num3 >= (double) num2 && num3 <= (double) num1)
                return EGridSize.sec1;
            double num4 = num3 / 2.0;
            if (num4 >= (double) num2 && num4 <= (double) num1)
                return EGridSize.sec2;
            double num5 = num4 / 2.5;
            if (num5 >= (double) num2 && num5 <= (double) num1)
                return EGridSize.sec5;
            double num6 = num5 / 2.0;
            if (num6 >= (double) num2 && num6 <= (double) num1)
                return EGridSize.sec10;
            double num7 = num6 / 2.0;
            if (num7 >= (double) num2 && num7 <= (double) num1)
                return EGridSize.sec20;
            double num8 = num7 / 1.5;
            if (num8 >= (double) num2 && num8 <= (double) num1)
                return EGridSize.sec30;
            double num9 = num8 / 2.0;
            if (num9 >= (double) num2 && num9 <= (double) num1)
                return EGridSize.min1;
            double num10 = num9 / 2.0;
            if (num10 >= (double) num2 && num10 <= (double) num1)
                return EGridSize.min2;
            double num11 = num10 / 2.5;
            if (num11 >= (double) num2 && num11 <= (double) num1)
                return EGridSize.min5;
            double num12 = num11 / 2.0;
            if (num12 >= (double) num2 && num12 <= (double) num1)
                return EGridSize.min10;
            double num13 = num12 / 1.5;
            if (num13 >= (double) num2 && num13 <= (double) num1)
                return EGridSize.min15;
            double num14 = num13 / (4.0 / 3.0);
            if (num14 >= (double) num2 && num14 <= (double) num1)
                return EGridSize.min20;
            double num15 = num14 / 1.5;
            if (num15 >= (double) num2 && num15 <= (double) num1)
                return EGridSize.min30;
            double num16 = num15 / 2.0;
            if (num16 >= (double) num2 && num16 <= (double) num1)
                return EGridSize.hour1;
            double num17 = num16 / 2.0;
            if (num17 >= (double) num2 && num17 <= (double) num1)
                return EGridSize.hour2;
            double num18 = num17 / 1.5;
            if (num18 >= (double) num2 && num18 <= (double) num1)
                return EGridSize.hour3;
            double num19 = num18 / (4.0 / 3.0);
            if (num19 >= (double) num2 && num19 <= (double) num1)
                return EGridSize.hour4;
            double num20 = num19 / 1.5;
            if (num20 >= (double) num2 && num20 <= (double) num1)
                return EGridSize.hour6;
            double num21 = num20 / 2.0;
            if (num21 >= (double) num2 && num21 <= (double) num1)
                return EGridSize.hour12;
            double num22 = num21 / 2.0;
            if (num22 >= (double) num2 && num22 <= (double) num1)
                return EGridSize.day1;
            double num23 = num22 / 2.0;
            if (num23 >= (double) num2 && num23 <= (double) num1)
                return EGridSize.day2;
            double num24 = num23 / 1.5;
            if (num24 >= (double) num2 && num24 <= (double) num1)
                return EGridSize.day3;
            double num25 = num24 / (5.0 / 3.0);
            if (num25 >= (double) num2 && num25 <= (double) num1)
                return EGridSize.day5;
            double num26 = num25 / 1.4;
            if (num26 >= (double) num2 && num26 <= (double) num1)
                return EGridSize.week1;
            double num27 = num26 / 2.0;
            if (num27 >= (double) num2 && num27 <= (double) num1)
                return EGridSize.week2;
            double num28 = num27 / (15.0 / 7.0);
            if (num28 >= (double) num2 && num28 <= (double) num1)
                return EGridSize.month1;
            double num29 = num28 / 2.0;
            if (num29 >= (double) num2 && num29 <= (double) num1)
                return EGridSize.month2;
            double num30 = num29 / 1.5;
            if (num30 >= (double) num2 && num30 <= (double) num1)
                return EGridSize.month3;
            double num31 = num30 / (4.0 / 3.0);
            if (num31 >= (double) num2 && num31 <= (double) num1)
                return EGridSize.month4;
            double num32 = num31 / 1.5;
            if (num32 >= (double) num2 && num32 <= (double) num1)
                return EGridSize.month6;
            double num33 = num32 / 2.0;
            if (num33 >= (double) num2 && num33 <= (double) num1)
                return EGridSize.year1;
            double num34 = num33 / 2.0;
            if (num34 >= (double) num2 && num34 <= (double) num1)
                return EGridSize.year2;
            double num35 = num34 / 1.5;
            if (num35 >= (double) num2 && num35 <= (double) num1)
                return EGridSize.year3;
            double num36 = num35 / (4.0 / 3.0);
            if (num36 >= (double) num2 && num36 <= (double) num1)
                return EGridSize.year4;
            double num37 = num36 / 0.8;
            if (num37 >= (double) num2 && num37 <= (double) num1)
                return EGridSize.year5;
            double num38 = num37 / 2.0;
            if (num38 >= (double) num2 && num38 <= (double) num1)
                return EGridSize.year10;
            double num39 = num38 / 2.0;
            return num39 >= (double) num2 && num39 <= (double) num1 ? EGridSize.year20 : EGridSize.year20;
        }

        public static long GetNextMajor(long PrevMajor, EGridSize GridSize)
        {
            long num;
            switch (GridSize)
            {
                case EGridSize.year5:
                    num = new DateTime(PrevMajor).AddYears(5).Ticks;
                    break;
                case EGridSize.year10:
                    num = new DateTime(PrevMajor).AddYears(10).Ticks;
                    break;
                case EGridSize.year20:
                    num = new DateTime(PrevMajor).AddYears(20).Ticks;
                    break;
                case EGridSize.year2:
                    num = new DateTime(PrevMajor).AddYears(2).Ticks;
                    break;
                case EGridSize.year3:
                    num = new DateTime(PrevMajor).AddYears(3).Ticks;
                    break;
                case EGridSize.year4:
                    num = new DateTime(PrevMajor).AddYears(4).Ticks;
                    break;
                case EGridSize.month4:
                    num = new DateTime(PrevMajor).AddMonths(4).Ticks;
                    break;
                case EGridSize.month6:
                    num = new DateTime(PrevMajor).AddMonths(6).Ticks;
                    break;
                case EGridSize.year1:
                    num = new DateTime(PrevMajor).AddYears(1).Ticks;
                    break;
                case EGridSize.month1:
                    num = new DateTime(PrevMajor).AddMonths(1).Ticks;
                    break;
                case EGridSize.month2:
                    num = new DateTime(PrevMajor).AddMonths(2).Ticks;
                    break;
                case EGridSize.month3:
                    num = new DateTime(PrevMajor).AddMonths(3).Ticks;
                    break;
                default:
                    num = (long) (PrevMajor + GridSize);
                    break;
            }
            return num;
        }
    }

    public class AxisRight : Axis
    {
        protected double x;
        protected double y1;
        protected double y2;
        private Pad pad;

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y1
        {
            get
            {
                return this.y1;
            }
            set
            {
                this.y1 = value;
            }
        }

        public double Y2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
            }
        }

        public AxisRight(Chart chart, Pad pad, int x, int y1, int y2)
        {
            this.chart = chart;
            this.pad = pad;
            this.x = (double) x;
            this.y1 = (double) y1;
            this.y2 = (double) y2;
            this.Init();
        }

        private void Init()
        {
            this.enabled = true;
            this.zoomed = false;
            this.color = Color.LightGray;
            this.title = "";
            this.titleEnabled = true;
            this.titlePosition = EAxisTitlePosition.Centre;
            this.titleFont = this.chart.Font;
            this.titleColor = Color.Black;
            this.titleOffset = 2;
            this.labelEnabled = true;
            this.labelFont = this.chart.Font;
            this.labelColor = this.chart.RightAxisTextColor;
            this.labelFormat = (string) null;
            this.labelOffset = 2;
            this.labelAlignment = EAxisLabelAlignment.Centre;
            this.gridEnabled = true;
            this.gridColor = this.chart.RightAxisGridColor;
            this.gridDashStyle = DashStyle.Dash;
            this.gridWidth = 0.5f;
            this.minorGridEnabled = false;
            this.minorGridColor = this.chart.RightAxisMinorTicksColor;
            this.minorGridDashStyle = DashStyle.Solid;
            this.minorGridWidth = 0.5f;
            this.majorTicksEnabled = true;
            this.majorTicksColor = this.chart.RightAxisMajorTicksColor;
            this.majorTicksWidth = 0.5f;
            this.majorTicksLength = 4;
            this.minorTicksEnabled = true;
            this.minorTicksColor = Color.LightGray;
            this.minorTicksWidth = 0.5f;
            this.minorTicksLength = 1;
            this.width = -1;
            this.height = -1;
        }

        public void SetBounds(int x, int y1, int y2)
        {
            this.x = (double) x;
            this.y1 = (double) y1;
            this.y2 = (double) y2;
        }

        public int GetAxisGap()
        {
            double maxValue = this.pad.MaxValue;
            double minValue = this.pad.MinValue;
            return (int) this.chart.Graphics.MeasureString(maxValue.ToString(this.pad.AxisLabelFormat), this.chart.Font).Width;
        }

        public void Paint()
        {
            SolidBrush solidBrush1 = new SolidBrush(this.titleColor);
            SolidBrush solidBrush2 = new SolidBrush(this.labelColor);
            Pen pen1 = new Pen(this.titleColor);
            Pen pen2 = new Pen(this.gridColor);
            Pen pen3 = new Pen(this.minorGridColor);
            Pen pen4 = new Pen(this.minorTicksColor);
            Pen pen5 = new Pen(this.majorTicksColor);
            pen2.DashStyle = this.gridDashStyle;
            pen3.DashStyle = this.minorGridDashStyle;
            this.min = this.pad.MinValue;
            this.max = this.pad.MaxValue;
            int num1 = 10;
            int num2 = 5;
            double num3 = AxisRight.Ceiling125(Math.Abs(this.max - this.min) * 0.999999 / (double) num1);
            double num4 = AxisRight.Ceiling125(num3 / (double) num2);
            double num5 = Math.Ceiling((this.min - 0.001 * num3) / num3) * num3;
            double num6 = Math.Floor((this.max + 0.001 * num3) / num3) * num3;
            int num7 = 0;
            int num8 = 0;
            if (num3 != 0.0)
                num7 = Math.Min(10000, (int) Math.Floor((num6 - num5) / num3 + 0.5) + 1);
            if (num3 != 0.0)
                num8 = Math.Abs((int) Math.Floor(num3 / num4 + 0.5)) - 1;
            int num9 = 0;
            for (int index1 = 0; index1 < num7; ++index1)
            {
                double num10 = num5 + (double) index1 * num3;
                string str = num10.ToString(this.pad.AxisLabelFormat);
                this.pad.DrawHorizontalGrid(pen2, num10);
                this.pad.DrawHorizontalTick(pen5, this.x - (double) this.majorTicksLength - 1.0, num10, this.majorTicksLength);
                if (this.labelEnabled)
                {
                    SizeF sizeF = this.pad.Graphics.MeasureString(str, this.labelFont);
                    double num11 = (double) sizeF.Width;
                    int num12 = this.labelOffset;
                    int num13 = (int) sizeF.Height;
                    if (this.labelAlignment == EAxisLabelAlignment.Centre)
                    {
                        int num14 = (int) (this.x + 2.0);
                        int num15 = this.pad.ClientY(num10) - num13 / 2;
                        if (index1 == 0 || num9 - (num15 + num13) >= 1)
                        {
                            if ((double) num15 > this.y1 && (double) (num15 + num13) < this.y2)
                                this.pad.Graphics.DrawString(str, this.labelFont, (Brush) solidBrush2, (float) num14, (float) num15);
                            num9 = num15;
                        }
                    }
                }
                for (int index2 = 1; index2 <= num8; ++index2)
                {
                    double y = num5 + (double) index1 * num3 + (double) index2 * num4;
                    if (y < this.max)
                        this.pad.DrawHorizontalTick(pen4, this.x - (double) this.minorTicksLength - 1.0, y, this.minorTicksLength);
                }
            }
            for (int index = 1; index <= num8; ++index)
            {
                double y = num5 - (double) index * num4;
                if (y > this.min && this.minorTicksEnabled)
                    this.pad.DrawHorizontalTick(pen4, this.x - (double) this.minorTicksLength - 1.0, y, this.minorTicksLength);
            }
            foreach (IChartDrawable chartDrawable in this.pad.Primitives)
            {
                if (chartDrawable is IAxesMarked)
                {
                    IAxesMarked axesMarked = chartDrawable as IAxesMarked;
                    if (axesMarked.IsMarkEnable)
                    {
                        double lastValue = axesMarked.LastValue;
                        if (!double.IsNaN(lastValue))
                        {
                            string str = lastValue.ToString("F" + axesMarked.LabelDigitsCount.ToString());
                            SizeF sizeF = this.chart.Graphics.MeasureString(str, this.chart.Font);
                            Color color = Color.FromArgb((int) axesMarked.Color.R ^ 128, (int) axesMarked.Color.G ^ 128, (int) axesMarked.Color.B ^ 128);
                            if (this.CompareColors(axesMarked.Color, Color.Black))
                                color = Color.White;
                            if (this.CompareColors(axesMarked.Color, Color.White))
                                color = Color.Black;
                            this.pad.Graphics.FillRectangle((Brush) new SolidBrush(axesMarked.Color), (float) this.X, (float) ((double) this.pad.ClientY(axesMarked.LastValue) - (double) sizeF.Height / 2.0 - 2.0), sizeF.Width, sizeF.Height + 2f);
                            this.pad.Graphics.DrawString(str, this.chart.RightAxesFont, (Brush) new SolidBrush(color), (float) this.X + 2f, (float) ((double) this.pad.ClientY(axesMarked.LastValue) - (double) sizeF.Height / 2.0 - 1.0));
                        }
                    }
                }
            }
        }

        private bool CompareColors(Color color1, Color color2)
        {
            if ((int) color1.A == (int) color2.A && (int) color1.R == (int) color2.R && (int) color1.G == (int) color2.G)
                return (int) color1.B == (int) color2.B;
            else
                return false;
        }

        private static double Ceiling125(double X)
        {
            double num1 = X > 0.0 ? 1.0 : -1.0;
            if (X == 0.0)
                return 0.0;
            double d = Math.Log10(Math.Abs(X));
            double y = Math.Floor(d);
            double num2 = Math.Pow(10.0, d - y);
            double num3 = (num2 > 1.0 ? (num2 > 2.0 ? (num2 > 5.0 ? 10.0 : 5.0) : 2.0) : 1.0) * Math.Pow(10.0, y);
            return num1 * num3;
        }

        private static double Floor125(double X)
        {
            double num1 = X > 0.0 ? 1.0 : -1.0;
            if (X == 0.0)
                return 0.0;
            double d = Math.Log10(Math.Abs(X));
            double y = Math.Floor(d);
            double num2 = Math.Pow(10.0, d - y);
            double num3 = (num2 < 10.0 ? (num2 < 5.0 ? (num2 < 2.0 ? 1.0 : 2.0) : 5.0) : 10.0) * Math.Pow(10.0, y);
            return num1 * num3;
        }
    }
}
