using System;
using System.Drawing;
using System.Drawing.Drawing2D;
#if GTK
using Compatibility.Gtk;
using MouseButtons = System.Windows.Forms.MouseButtons;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class Axis
    {
        private Pad fPad;
        private EAxisType fType;
        private EAxisPosition fPosition;
        private EAxisTitlePosition fTitlePosition;
        private EVerticalGridStyle fVerticalGridStyle;
        private bool fEnabled;
        private bool fZoomed;
        private Color fColor;
        private bool fTitleEnabled;
        private string fTitle;
        private Font fTitleFont;
        private Color fTitleColor;
        private int fTitleOffset;
        private bool fLabelEnabled;
        private Font fLabelFont;
        private Color fLabelColor;
        private int fLabelOffset;
        private string fLabelFormat;
        private EAxisLabelAlignment fLabelAlignment;
        private bool fGridEnabled;
        private Color fGridColor;
        private float fGridWidth;
        private DashStyle fGridDashStyle;
        private bool fMinorGridEnabled;
        private Color fMinorGridColor;
        private float fMinorGridWidth;
        private DashStyle fMinorGridDashStyle;
        private bool fMajorTicksEnabled;
        private Color fMajorTicksColor;
        private float fMajorTicksWidth;
        private int fMajorTicksLength;
        private bool fMinorTicksEnabled;
        private Color fMinorTicksColor;
        private float fMinorTicksWidth;
        private int fMinorTicksLength;
        private double fX1;
        private double fX2;
        private double fY1;
        private double fY2;
        private double fMin;
        private double fMax;
        private bool fMouseDown;
        private int fMouseDownX;
        private int fMouseDownY;
        private bool fOutlineEnabled;
        private int fOutline1;
        private int fOutline2;
        private int fLastValidAxisWidth;
        private int fWidth;
        private int fHeight;

        public double X1
        {
            get
            {
                return this.fX1;
            }
            set
            {
                this.fX1 = value;
            }
        }

        public double Y1
        {
            get
            {
                return this.fY1;
            }
            set
            {
                this.fY1 = value;
            }
        }

        public double X2
        {
            get
            {
                return this.fX2;
            }
            set
            {
                this.fX2 = value;
            }
        }

        public double Y2
        {
            get
            {
                return this.fY2;
            }
            set
            {
                this.fY2 = value;
            }
        }

        public Color Color
        {
            get
            {
                return this.fColor;
            }
            set
            {
                this.fColor = value;
            }
        }

        public EAxisType Type
        {
            get
            {
                return this.fType;
            }
            set
            {
                this.fType = value;
            }
        }

        public EAxisPosition Position
        {
            get
            {
                return this.fPosition;
            }
            set
            {
                this.fPosition = value;
            }
        }

        public EVerticalGridStyle VerticalGridStyle
        {
            get
            {
                return this.fVerticalGridStyle;
            }
            set
            {
                this.fVerticalGridStyle = value;
            }
        }

        public bool MajorTicksEnabled
        {
            get
            {
                return this.fMajorTicksEnabled;
            }
            set
            {
                this.fMajorTicksEnabled = value;
            }
        }

        public Color MajorTicksColor
        {
            get
            {
                return this.fMajorTicksColor;
            }
            set
            {
                this.fMajorTicksColor = value;
            }
        }

        public float MajorTicksWidth
        {
            get
            {
                return this.fMajorTicksWidth;
            }
            set
            {
                this.fMajorTicksWidth = value;
            }
        }

        public int MajorTicksLength
        {
            get
            {
                return this.fMajorTicksLength;
            }
            set
            {
                this.fMajorTicksLength = value;
            }
        }

        public bool MinorTicksEnabled
        {
            get
            {
                return this.fMinorTicksEnabled;
            }
            set
            {
                this.fMinorTicksEnabled = value;
            }
        }

        public Color MinorTicksColor
        {
            get
            {
                return this.fMinorTicksColor;
            }
            set
            {
                this.fMinorTicksColor = value;
            }
        }

        public float MinorTicksWidth
        {
            get
            {
                return this.fMinorTicksWidth;
            }
            set
            {
                this.fMinorTicksWidth = value;
            }
        }

        public int MinorTicksLength
        {
            get
            {
                return this.fMinorTicksLength;
            }
            set
            {
                this.fMinorTicksLength = value;
            }
        }

        public EAxisTitlePosition TitlePosition
        {
            get
            {
                return this.fTitlePosition;
            }
            set
            {
                this.fTitlePosition = value;
            }
        }

        public Font TitleFont
        {
            get
            {
                return this.fTitleFont;
            }
            set
            {
                this.fTitleFont = value;
            }
        }

        public Color TitleColor
        {
            get
            {
                return this.fTitleColor;
            }
            set
            {
                this.fTitleColor = value;
            }
        }

        public int TitleOffset
        {
            get
            {
                return this.fTitleOffset;
            }
            set
            {
                this.fTitleOffset = value;
            }
        }

        public double Min
        {
            get
            {
                return this.fMin;
            }
            set
            {
                this.fMin = value;
            }
        }

        public double Max
        {
            get
            {
                return this.fMax;
            }
            set
            {
                this.fMax = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.fEnabled;
            }
            set
            {
                this.fEnabled = value;
            }
        }

        public bool Zoomed
        {
            get
            {
                return this.fZoomed;
            }
            set
            {
                this.fZoomed = value;
            }
        }

        public bool GridEnabled
        {
            get
            {
                return this.fGridEnabled;
            }
            set
            {
                this.fGridEnabled = value;
            }
        }

        public Color GridColor
        {
            get
            {
                return this.fGridColor;
            }
            set
            {
                this.fGridColor = value;
            }
        }

        public float GridWidth
        {
            get
            {
                return this.fGridWidth;
            }
            set
            {
                this.fGridWidth = value;
            }
        }

        public DashStyle GridDashStyle
        {
            get
            {
                return this.fGridDashStyle;
            }
            set
            {
                this.fGridDashStyle = value;
            }
        }

        public bool MinorGridEnabled
        {
            get
            {
                return this.fMinorGridEnabled;
            }
            set
            {
                this.fMinorGridEnabled = value;
            }
        }

        public Color MinorGridColor
        {
            get
            {
                return this.fMinorGridColor;
            }
            set
            {
                this.fMinorGridColor = value;
            }
        }

        public float MinorGridWidth
        {
            get
            {
                return this.fMinorGridWidth;
            }
            set
            {
                this.fMinorGridWidth = value;
            }
        }

        public DashStyle MinorGridDashStyle
        {
            get
            {
                return this.fMinorGridDashStyle;
            }
            set
            {
                this.fMinorGridDashStyle = value;
            }
        }

        public bool TitleEnabled
        {
            get
            {
                return this.fTitleEnabled;
            }
            set
            {
                this.fTitleEnabled = value;
            }
        }

        public bool LabelEnabled
        {
            get
            {
                return this.fLabelEnabled;
            }
            set
            {
                this.fLabelEnabled = value;
            }
        }

        public Font LabelFont
        {
            get
            {
                return this.fLabelFont;
            }
            set
            {
                this.fLabelFont = value;
            }
        }

        public Color LabelColor
        {
            get
            {
                return this.fLabelColor;
            }
            set
            {
                this.fLabelColor = value;
            }
        }

        public int LabelOffset
        {
            get
            {
                return this.fLabelOffset;
            }
            set
            {
                this.fLabelOffset = value;
            }
        }

        public string LabelFormat
        {
            get
            {
                return this.fLabelFormat;
            }
            set
            {
                this.fLabelFormat = value;
            }
        }

        public EAxisLabelAlignment LabelAlignment
        {
            get
            {
                return this.fLabelAlignment;
            }
            set
            {
                this.fLabelAlignment = value;
            }
        }

        public int Width
        {
            get
            {
                if (!this.fEnabled)
                    return 0;
                if (this.fWidth != -1)
                    return this.fWidth;
                int num1 = 0;
                int num2 = 0;
                this.fLastValidAxisWidth = 0;
                if (this.fTitleEnabled)
                    num1 = (int) ((double) this.fTitleOffset + (double) this.fPad.Graphics.MeasureString(this.fMax.ToString("F1"), this.fTitleFont).Height);
                if (this.fLabelEnabled)
                    num2 = this.fLabelFormat != null ? (int) ((double) this.fLabelOffset + (double) this.fPad.Graphics.MeasureString(this.fMax.ToString(this.fLabelFormat), this.fLabelFont).Width) : (int) ((double) this.fLabelOffset + (double) this.fPad.Graphics.MeasureString(this.fMax.ToString("F1"), this.fLabelFont).Width);
                this.fLastValidAxisWidth = num2 + num1 + 2;
                return num2 + num1 + 2;
            }
            set
            {
                this.fWidth = value;
            }
        }

        public int LastValidAxisWidth
        {
            get
            {
                return this.fLastValidAxisWidth;
            }
            set
            {
                this.fLastValidAxisWidth = value;
            }
        }

        public int Height
        {
            get
            {
                if (!this.fEnabled)
                    return 0;
                if (this.fHeight != -1)
                    return this.fHeight;
                int num1 = 0;
                int num2 = 0;
                if (this.fTitleEnabled)
                    num1 = (int) ((double) this.fTitleOffset + (double) this.fPad.Graphics.MeasureString("Example", this.fTitleFont).Height);
                if (this.fLabelEnabled)
                    num2 = (int) ((double) this.fLabelOffset + (double) this.fPad.Graphics.MeasureString("Example", this.fLabelFont).Height);
                return num2 + num1;
            }
            set
            {
                this.fHeight = value;
            }
        }

        public string Title
        {
            get
            {
                return this.fTitle;
            }
            set
            {
                this.fTitle = value;
            }
        }

        public Axis(Pad Pad)
        {
            this.fPad = Pad;
            this.fPosition = EAxisPosition.None;
            this.fX1 = 0.0;
            this.fX2 = 0.0;
            this.fY1 = 0.0;
            this.fY2 = 0.0;
            this.Init();
        }

        public Axis(Pad Pad, EAxisPosition Position)
        {
            this.fPad = Pad;
            this.fPosition = Position;
            this.fX1 = 0.0;
            this.fX2 = 0.0;
            this.fY1 = 0.0;
            this.fY2 = 0.0;
            this.Init();
        }

        public Axis(Pad Pad, double X1, double Y1, double X2, double Y2)
        {
            this.fPad = Pad;
            this.fPosition = EAxisPosition.None;
            this.fX1 = X1;
            this.fX2 = X2;
            this.fY1 = Y1;
            this.fY2 = Y2;
            this.Init();
        }

        private void Init()
        {
            this.fEnabled = true;
            this.fZoomed = false;
            this.fColor = Color.Black;
            this.fTitle = "";
            this.fTitleEnabled = true;
            this.fTitlePosition = EAxisTitlePosition.Centre;
            this.fTitleFont = new Font("Arial", 8f);
            this.fTitleColor = Color.Black;
            this.fTitleOffset = 2;
            this.fLabelEnabled = true;
            this.fLabelFont = new Font("Arial", 8f);
            this.fLabelColor = Color.Black;
            this.fLabelFormat = (string) null;
            this.fLabelOffset = 2;
            this.fLabelAlignment = EAxisLabelAlignment.Centre;
            this.fGridEnabled = true;
            this.fGridColor = Color.Gray;
            this.fGridDashStyle = DashStyle.Solid;
            this.fGridWidth = 0.5f;
            this.fMinorGridEnabled = false;
            this.fMinorGridColor = Color.Gray;
            this.fMinorGridDashStyle = DashStyle.Solid;
            this.fMinorGridWidth = 0.5f;
            this.fMajorTicksEnabled = true;
            this.fMajorTicksColor = Color.Black;
            this.fMajorTicksWidth = 0.5f;
            this.fMajorTicksLength = 4;
            this.fMinorTicksEnabled = true;
            this.fMinorTicksColor = Color.Black;
            this.fMinorTicksWidth = 0.5f;
            this.fMinorTicksLength = 1;
            this.fType = EAxisType.Numeric;
            this.fVerticalGridStyle = EVerticalGridStyle.ByDateTime;
            this.fMouseDown = false;
            this.fMouseDownX = 0;
            this.fMouseDownY = 0;
            this.fOutlineEnabled = false;
            this.fOutline1 = 0;
            this.fOutline2 = 0;
            this.fWidth = -1;
            this.fHeight = -1;
        }

        public void SetLocation(double X1, double Y1, double X2, double Y2)
        {
            this.fX1 = X1;
            this.fX2 = X2;
            this.fY1 = Y1;
            this.fY2 = Y2;
        }

        public void SetRange(double Min, double Max)
        {
            this.fMin = Min;
            this.fMax = Max;
        }

        public void Zoom(double Min, double Max)
        {
            this.fMin = Min;
            this.fMax = Max;
            this.fZoomed = true;
            this.fPad.EmitZoom(true);
            if (this.fPad.Chart.GroupZoomEnabled)
                return;
            this.fPad.Update();
        }

        public void Zoom(DateTime Min, DateTime Max)
        {
            this.Zoom((double) Min.Ticks, (double) Max.Ticks);
        }

        public void Zoom(string Min, string Max)
        {
            this.Zoom(DateTime.Parse(Min), DateTime.Parse(Max));
        }

        public void UnZoom()
        {
            switch (this.fPosition)
            {
                case EAxisPosition.Left:
                    this.SetRange(this.fPad.YMin, this.fPad.YMax);
                    break;
                case EAxisPosition.Bottom:
                    this.SetRange(this.fPad.XMin, this.fPad.XMax);
                    break;
            }
            this.fZoomed = false;
            this.fPad.EmitZoom(false);
            if (this.fPad.Chart.GroupZoomEnabled)
                return;
            this.fPad.Update();
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

        public static EGridSize CalculateSize(double ticks)
        {
            int num1 = 10;
            int num2 = 3;
            double num3 = Math.Floor(ticks / 600000000.0);
            if (num3 >= (double) num2 && num3 <= (double) num1)
                return EGridSize.min1;
            double num4 = num3 / 2.0;
            if (num4 >= (double) num2 && num4 <= (double) num1)
                return EGridSize.min2;
            double num5 = num4 / 2.5;
            if (num5 >= (double) num2 && num5 <= (double) num1)
                return EGridSize.min5;
            double num6 = num5 / 2.0;
            if (num6 >= (double) num2 && num6 <= (double) num1)
                return EGridSize.min10;
            double num7 = num6 / 1.5;
            if (num7 >= (double) num2 && num7 <= (double) num1)
                return EGridSize.min15;
            double num8 = num7 / (4.0 / 3.0);
            if (num8 >= (double) num2 && num8 <= (double) num1)
                return EGridSize.min20;
            double num9 = num8 / 1.5;
            if (num9 >= (double) num2 && num9 <= (double) num1)
                return EGridSize.min30;
            double num10 = num9 / 2.0;
            if (num10 >= (double) num2 && num10 <= (double) num1)
                return EGridSize.hour1;
            double num11 = num10 / 2.0;
            if (num11 >= (double) num2 && num11 <= (double) num1)
                return EGridSize.hour2;
            double num12 = num11 / 1.5;
            if (num12 >= (double) num2 && num12 <= (double) num1)
                return EGridSize.hour3;
            double num13 = num12 / (4.0 / 3.0);
            if (num13 >= (double) num2 && num13 <= (double) num1)
                return EGridSize.hour4;
            double num14 = num13 / 1.5;
            if (num14 >= (double) num2 && num14 <= (double) num1)
                return EGridSize.hour6;
            double num15 = num14 / 2.0;
            if (num15 >= (double) num2 && num15 <= (double) num1)
                return EGridSize.hour12;
            double num16 = num15 / 2.0;
            if (num16 >= (double) num2 && num16 <= (double) num1)
                return EGridSize.day1;
            double num17 = num16 / 2.0;
            if (num17 >= (double) num2 && num17 <= (double) num1)
                return EGridSize.day2;
            double num18 = num17 / 1.5;
            if (num18 >= (double) num2 && num18 <= (double) num1)
                return EGridSize.day3;
            double num19 = num18 / (5.0 / 3.0);
            if (num19 >= (double) num2 && num19 <= (double) num1)
                return EGridSize.day5;
            double num20 = num19 / 1.4;
            if (num20 >= (double) num2 && num20 <= (double) num1)
                return EGridSize.week1;
            double num21 = num20 / 2.0;
            if (num21 >= (double) num2 && num21 <= (double) num1)
                return EGridSize.week2;
            double num22 = num21 / (15.0 / 7.0);
            if (num22 >= (double) num2 && num22 <= (double) num1)
                return EGridSize.month1;
            double num23 = num22 / 2.0;
            if (num23 >= (double) num2 && num23 <= (double) num1)
                return EGridSize.month2;
            double num24 = num23 / 1.5;
            if (num24 >= (double) num2 && num24 <= (double) num1)
                return EGridSize.month3;
            double num25 = num24 / (4.0 / 3.0);
            if (num25 >= (double) num2 && num25 <= (double) num1)
                return EGridSize.month4;
            double num26 = num25 / 1.5;
            if (num26 >= (double) num2 && num26 <= (double) num1)
                return EGridSize.month6;
            double num27 = num26 / 2.0;
            if (num27 >= (double) num2 && num27 <= (double) num1)
                return EGridSize.year1;
            double num28 = num27 / 2.0;
            if (num28 >= (double) num2 && num28 <= (double) num1)
                return EGridSize.year2;
            double num29 = num28 / 1.5;
            if (num29 >= (double) num2 && num29 <= (double) num1)
                return EGridSize.year3;
            double num30 = num29 / (4.0 / 3.0);
            if (num30 >= (double) num2 && num30 <= (double) num1)
                return EGridSize.year4;
            double num31 = num30 / 0.8;
            if (num31 >= (double) num2 && num31 <= (double) num1)
                return EGridSize.year5;
            double num32 = num31 / 2.0;
            if (num32 >= (double) num2 && num32 <= (double) num1)
                return EGridSize.year10;
            double num33 = num32 / 2.0;
            return num33 >= (double) num2 && num33 <= (double) num1 ? EGridSize.year20 : EGridSize.year20;
        }

        public void PaintWithDates()
        {
            if (!this.fEnabled)
                return;
            this.fPad.DrawLine(new Pen(this.fColor), this.fX1, this.fY1, this.fX2, this.fY2, false);
            SolidBrush solidBrush1 = new SolidBrush(this.fTitleColor);
            SolidBrush solidBrush2 = new SolidBrush(this.fLabelColor);
            Pen Pen1 = new Pen(this.fTitleColor);
            Pen Pen2 = new Pen(this.fGridColor);
            Pen Pen3 = new Pen(this.fMinorGridColor);
            Pen Pen4 = new Pen(this.fMinorTicksColor);
            Pen Pen5 = new Pen(this.fMajorTicksColor);
            Pen2.Width = this.fGridWidth;
            Pen2.DashStyle = this.fGridDashStyle;
            Pen3.Width = this.fMinorGridWidth;
            Pen3.DashStyle = this.fMinorGridDashStyle;
            DateTime FirstDateTime = new DateTime();
            double Min = this.fMin;
            double Max = this.fMax;
            FirstDateTime = new DateTime(Math.Max(0L, (long) Min));
            DateTime dateTime1 = new DateTime((long) Max);
            EGridSize GridSize = EGridSize.min1;
            this.fPad.GetFirstGridDivision(ref GridSize, ref Min, ref Max, ref FirstDateTime);
            double num1 = 5.0;
            double num2;
            DateTime dateTime2;
            switch (GridSize)
            {
                case EGridSize.year10:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (9 - FirstDateTime.Year % 10));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.year20:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (19 - FirstDateTime.Year % 20));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.year4:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (3 - FirstDateTime.Year % 4));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.year5:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (4 - FirstDateTime.Year % 5));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.year2:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (1 - FirstDateTime.Year % 2));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.year3:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1 + (2 - FirstDateTime.Year % 3));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.month6:
                    DateTime dateTime3 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, 1);
                    dateTime3 = dateTime3.AddMonths(1 + (12 - FirstDateTime.Month) % 6);
                    num2 = (double) dateTime3.Ticks;
                    break;
                case EGridSize.year1:
                    dateTime2 = new DateTime(FirstDateTime.Year, 1, 1);
                    dateTime2 = dateTime2.AddYears(1);
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.month3:
                    DateTime dateTime4 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, 1);
                    dateTime4 = dateTime4.AddMonths(1 + (12 - FirstDateTime.Month) % 3);
                    num2 = (double) dateTime4.Ticks;
                    break;
                case EGridSize.month4:
                    DateTime dateTime5 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, 1);
                    dateTime5 = dateTime5.AddMonths(1 + (12 - FirstDateTime.Month) % 4);
                    num2 = (double) dateTime5.Ticks;
                    break;
                case EGridSize.month1:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, 1);
                    dateTime2 = dateTime2.AddMonths(1);
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.month2:
                    DateTime dateTime6 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, 1);
                    dateTime6 = dateTime6.AddMonths(1 + FirstDateTime.Month % 2);
                    num2 = (double) dateTime6.Ticks;
                    break;
                case EGridSize.week1:
                    DateTime dateTime7 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day);
                    dateTime7 = dateTime7.AddDays(8.0 - (double) FirstDateTime.DayOfWeek);
                    num2 = (double) dateTime7.Ticks;
                    break;
                case EGridSize.week2:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day).AddDays(8.0 - (double) FirstDateTime.DayOfWeek + (double) (7 * (1 - (int) Math.Floor(new TimeSpan(FirstDateTime.AddDays(8.0 - (double) FirstDateTime.DayOfWeek).Ticks).TotalDays) / 7 % 2))).Ticks;
                    break;
                case EGridSize.day3:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day).AddDays((double) (1 + (2 - (int) new TimeSpan(FirstDateTime.Ticks).TotalDays % 3)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.day5:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day).AddDays((double) (1 + (4 - (int) new TimeSpan(FirstDateTime.Ticks).TotalDays % 5)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.day1:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day);
                    dateTime2 = dateTime2.AddDays(1.0);
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.day2:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day).AddDays((double) (1 + (int) new TimeSpan(FirstDateTime.Ticks).TotalDays % 2)).Ticks;
                    break;
                case EGridSize.hour6:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0).AddHours((double) (1 + (5 - (int) new TimeSpan(FirstDateTime.Ticks).TotalHours % 6)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.hour12:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0).AddHours((double) (1 + (11 - (int) new TimeSpan(FirstDateTime.Ticks).TotalHours % 12)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.hour3:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0).AddHours((double) (1 + (2 - (int) new TimeSpan(FirstDateTime.Ticks).TotalHours % 3)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.hour4:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0).AddHours((double) (1 + (3 - (int) new TimeSpan(FirstDateTime.Ticks).TotalHours % 4)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.hour1:
                    DateTime dateTime8 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0);
                    dateTime8 = dateTime8.AddHours(1.0);
                    num2 = (double) dateTime8.Ticks;
                    break;
                case EGridSize.hour2:
                    dateTime2 = new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, 0, 0).AddHours((double) (1 + (1 - (int) new TimeSpan(FirstDateTime.Ticks).TotalHours % 2)));
                    num2 = (double) dateTime2.Ticks;
                    break;
                case EGridSize.min20:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (19 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 20))).Ticks;
                    break;
                case EGridSize.min30:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (29 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 30))).Ticks;
                    break;
                case EGridSize.min10:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (9 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 10))).Ticks;
                    break;
                case EGridSize.min15:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (14 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 15))).Ticks;
                    break;
                case EGridSize.min1:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes(1.0).Ticks;
                    break;
                case EGridSize.min2:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (1 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 2))).Ticks;
                    break;
                case EGridSize.min5:
                    num2 = (double) new DateTime(FirstDateTime.Year, FirstDateTime.Month, FirstDateTime.Day, FirstDateTime.Hour, FirstDateTime.Minute, 0).AddMinutes((double) (1 + (4 - (int) new TimeSpan(FirstDateTime.Ticks).TotalMinutes % 5))).Ticks;
                    break;
                default:
                    num2 = (double) (FirstDateTime.Ticks + GridSize);
                    break;
            }
            int num3 = 0;
            int num4 = 0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            string str = "";
            int MajorCount = 0;
            double num8 = Max;
            FirstDateTime = new DateTime((long) num2);
            DateTime dateTime9 = new DateTime((long) num8);
            while (num5 < num8)
            {
                num5 = this.fPad.GetNextGridDivision(num2, num6, MajorCount, GridSize);
                if (num5 < num8)
                {
                    if (this.fType == EAxisType.DateTime)
                    {
                        if (this.fLabelFormat == null)
                        {
                            dateTime2 = new DateTime((long) num5);
                            str = dateTime2.ToString("MMM yyyy");
                        }
                        else
                        {
                            long num9 = (long) num5 % 864000000000L;
                            TimeSpan timeSpan = this.fPad.SessionStart;
                            long ticks1 = timeSpan.Ticks;
                            string format;
                            if (num9 != ticks1)
                            {
                                long num10 = (long) num5 % 864000000000L;
                                timeSpan = this.fPad.SessionEnd;
                                long ticks2 = timeSpan.Ticks;
                                if (num10 != ticks2)
                                {
                                    if (num6 == 0.0)
                                    {
                                        format = "yyy MMM dd HH:mm";
                                        goto label_47;
                                    }
                                    else
                                    {
                                        DateTime dateTime10 = new DateTime((long) num6);
                                        DateTime dateTime11 = new DateTime((long) num5);
                                        format = dateTime10.Year == dateTime11.Year ? (dateTime10.Month == dateTime11.Month ? (dateTime10.Day == dateTime11.Day ? (dateTime10.Minute != dateTime11.Minute || dateTime10.Hour != dateTime11.Hour ? "HH:mm" : "HH:mm:ss") : "MMM dd HH:mm") : "MMM dd HH:mm") : "yyy MMM dd HH:mm";
                                        goto label_47;
                                    }
                                }
                            }
                            format = num6 != 0.0 ? (new DateTime((long) num6).Year == new DateTime((long) num5).Year ? "MMM dd" : "yyyy MMM dd") : "yyy MMM dd";
                            label_47:
                            dateTime2 = new DateTime((long) num5);
                            str = dateTime2.ToString(format);
                            solidBrush2 = new SolidBrush(Color.Black);
                        }
                    }
                    if (this.fPosition == EAxisPosition.Bottom)
                    {
                        if (this.fGridEnabled)
                            this.fPad.DrawVerticalGrid(Pen2, num5);
                        if (this.fMajorTicksEnabled)
                            this.fPad.DrawVerticalTick(Pen1, num5, this.fY2, -5);
                        if (this.fLabelEnabled)
                        {
                            SizeF sizeF = this.fPad.Graphics.MeasureString(str, this.fLabelFont);
                            int num9 = (int) sizeF.Width;
                            int num10 = (int) sizeF.Height;
                            if (this.fLabelAlignment == EAxisLabelAlignment.Right)
                                this.fPad.Graphics.DrawString(str, this.fLabelFont, (Brush) solidBrush2, (float) this.fPad.ClientX(num5), (float) (int) (this.fY2 + (double) this.fLabelOffset));
                            if (this.fLabelAlignment == EAxisLabelAlignment.Left)
                                this.fPad.Graphics.DrawString(str, this.fLabelFont, (Brush) solidBrush2, (float) (this.fPad.ClientX(num5) - num9), (float) (int) (this.fY2 + (double) this.fLabelOffset));
                            if (this.fLabelAlignment == EAxisLabelAlignment.Centre)
                            {
                                int num11 = this.fPad.ClientX(num5) - num9 / 2;
                                int num12 = (int) (this.fY2 + (double) this.fLabelOffset);
                                if (MajorCount == 0 || num11 - num3 >= 1)
                                {
                                    this.fPad.Graphics.DrawString(str, this.fLabelFont, (Brush) solidBrush2, (float) num11, (float) num12);
                                    num3 = num11 + num9;
                                }
                            }
                        }
                    }
                    if (this.fPosition == EAxisPosition.Left || this.fPosition == EAxisPosition.Right)
                    {
                        if (this.fGridEnabled)
                            this.fPad.DrawHorizontalGrid(Pen2, num5);
                        if (this.fMajorTicksEnabled)
                            this.fPad.DrawHorizontalTick(Pen5, this.fX1, num5, 5);
                        if (this.fLabelEnabled)
                        {
                            SizeF sizeF = this.fPad.Graphics.MeasureString(str, this.fLabelFont);
                            int num9 = (int) ((double) sizeF.Width + (double) this.fLabelOffset);
                            int num10 = (int) sizeF.Height;
                            if (this.fLabelAlignment == EAxisLabelAlignment.Centre)
                            {
                                int num11 = (int) (this.fX1 - (double) num9);
                                int num12 = this.fPad.ClientY(num5) - num10 / 2;
                                if (MajorCount == 0 || num4 - (num12 + num10) >= 1)
                                {
                                    this.fPad.Graphics.DrawString(str, this.fLabelFont, (Brush) solidBrush2, (float) num11, (float) num12);
                                    num4 = num12;
                                }
                            }
                        }
                    }
                }
                if (MajorCount != 0)
                {
                    if (MajorCount == 1)
                        num7 = (num5 - num6 - this.fPad.Transformation.CalculateNotInSessionTicks(num6, num5)) / num1;
                    for (int index = 1; (double) index <= num1; ++index)
                    {
                        double num9 = num6 + this.fPad.Transformation.CalculateRealQuantityOfTicks_Right(num6, num6 + (double) index * num7);
                        if (num9 < Max)
                        {
                            if (this.fPosition == EAxisPosition.Bottom)
                            {
                                if (this.fMinorGridEnabled)
                                    this.fPad.DrawVerticalGrid(Pen3, num9);
                                if (this.fMinorTicksEnabled)
                                    this.fPad.DrawVerticalTick(Pen4, num9, this.fY2, -2);
                            }
                            if (this.fPosition == EAxisPosition.Left)
                            {
                                if (this.fMinorGridEnabled)
                                    this.fPad.DrawHorizontalGrid(Pen3, num9);
                                if (this.fMinorTicksEnabled)
                                    this.fPad.DrawHorizontalTick(Pen5, this.fX1, num9, 2);
                                if (this.fPosition == EAxisPosition.Right)
                                {
                                    if (this.fMinorGridEnabled)
                                        this.fPad.DrawHorizontalGrid(Pen3, num9);
                                    if (this.fMinorTicksEnabled)
                                        this.fPad.DrawHorizontalTick(Pen4, this.fX2 - 2.0, num9, 2);
                                }
                            }
                        }
                    }
                }
                else
                    num2 = num5;
                num6 = num5;
                ++MajorCount;
            }
            for (int index = 0; (double) index <= num1; ++index)
            {
                double num9 = num2 + this.fPad.Transformation.CalculateRealQuantityOfTicks_Left(num2, num2 - (double) index * num7);
                if (num9 > this.fMin)
                {
                    if (this.fPosition == EAxisPosition.Bottom)
                    {
                        if (this.fMinorGridEnabled)
                            this.fPad.DrawVerticalGrid(Pen3, num9);
                        if (this.fMinorTicksEnabled)
                            this.fPad.DrawVerticalTick(Pen4, num9, this.fY2, -2);
                    }
                    if (this.fPosition == EAxisPosition.Left)
                    {
                        if (this.fMinorGridEnabled)
                            this.fPad.DrawHorizontalGrid(Pen3, num9);
                        if (this.fMinorTicksEnabled)
                            this.fPad.DrawHorizontalTick(Pen4, this.fX1, num9, 2);
                    }
                }
            }
            if (this.fPad.SessionGridEnabled && ((TIntradayTransformation) this.fPad.Transformation).Session >= 2L * (long) GridSize)
            {
                int num9 = 0;
                double X1;
                for (double X2 = (double) (((long) this.fMin / 864000000000L + 1L) * 864000000000L); (X1 = X2 + this.fPad.Transformation.CalculateRealQuantityOfTicks_Right(X2, X2 + (double) ((long) num9 * ((TIntradayTransformation) this.fPad.Transformation).Session))) < Max; ++num9)
                    this.fPad.DrawVerticalGrid(new Pen(this.fPad.SessionGridColor), X1);
            }
            if (this.fOutlineEnabled)
            {
                if (this.fPosition == EAxisPosition.Bottom)
                {
                    this.fPad.DrawVerticalGrid(new Pen(Color.Green), this.fPad.WorldX(this.fOutline1));
                    this.fPad.DrawVerticalGrid(new Pen(Color.Green), this.fPad.WorldX(this.fOutline2));
                }
                if (this.fPosition == EAxisPosition.Left)
                {
                    this.fPad.DrawHorizontalGrid(new Pen(Color.Green), this.fPad.WorldY(this.fOutline1));
                    this.fPad.DrawHorizontalGrid(new Pen(Color.Green), this.fPad.WorldY(this.fOutline2));
                }
            }
            if (!this.fTitleEnabled)
                return;
            int num13 = (int) this.fPad.Graphics.MeasureString("Example", this.fLabelFont).Height;
            int num14 = (int) this.fPad.Graphics.MeasureString(this.fMax.ToString("F1"), this.fLabelFont).Width;
            int num15 = (int) this.fPad.Graphics.MeasureString(this.fTitle, this.fTitleFont).Height;
            int num16 = (int) this.fPad.Graphics.MeasureString(this.fTitle, this.fTitleFont).Width;
            if (this.fPosition == EAxisPosition.Bottom)
            {
                if (this.fTitlePosition == EAxisTitlePosition.Left)
                    this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) this.fX1, (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
                if (this.fTitlePosition == EAxisTitlePosition.Right)
                    this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) ((int) this.fX2 - num16), (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
                if (this.fTitlePosition == EAxisTitlePosition.Centre)
                    this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) (this.fX1 + (this.fX2 - this.fX1 - (double) num16) / 2.0), (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
            }
            if (this.fPosition != EAxisPosition.Left || this.fTitlePosition != EAxisTitlePosition.Centre)
                return;
            this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) (this.fX1 - (double) this.fLabelOffset - (double) num14 - (double) this.fTitleOffset - (double) num15), (float) (int) (this.fY1 + (this.fY2 - this.fY1 - (double) num16) / 2.0), new StringFormat()
            {
                FormatFlags = StringFormatFlags.DirectionRightToLeft | StringFormatFlags.DirectionVertical
            });
            this.fPad.Graphics.ResetTransform();
        }

        public virtual void Paint()
        {
            try
            {
                if (!this.fEnabled)
                    return;
                if (this.fVerticalGridStyle == EVerticalGridStyle.ByDateTime && this.fType == EAxisType.DateTime && this.fMax > 100000.0)
                {
                    this.PaintWithDates();
                }
                else
                {
                    bool flag = false;
                    string str1 = "";
                    if (this.fMax <= 1000000.0 && this.fType == EAxisType.DateTime)
                    {
                        this.fType = EAxisType.Numeric;
                        flag = true;
                        str1 = this.fLabelFormat;
                        this.fLabelFormat = "F1";
                    }
                    SolidBrush solidBrush1 = new SolidBrush(this.fTitleColor);
                    SolidBrush solidBrush2 = new SolidBrush(this.fLabelColor);
                    Pen pen = new Pen(this.fTitleColor);
                    Pen Pen1 = new Pen(this.fGridColor);
                    Pen Pen2 = new Pen(this.fMinorGridColor);
                    Pen Pen3 = new Pen(this.fMinorTicksColor);
                    Pen Pen4 = new Pen(this.fMajorTicksColor);
                    Pen1.DashStyle = this.fGridDashStyle;
                    Pen2.DashStyle = this.fMinorGridDashStyle;
                    this.fPad.DrawLine(new Pen(this.fColor), this.fX1, this.fY1, this.fX2, this.fY2, false);
                    int num1 = 10;
                    int num2 = 5;
                    double num3 = Axis.Ceiling125(Math.Abs(this.fMax - this.fMin) * 0.999999 / (double) num1);
                    double num4 = Axis.Ceiling125(num3 / (double) num2);
                    double num5 = Math.Ceiling((this.fMin - 0.001 * num3) / num3) * num3;
                    double num6 = Math.Floor((this.fMax + 0.001 * num3) / num3) * num3;
                    int num7 = 0;
                    int num8 = 0;
                    if (num3 != 0.0)
                        num7 = Math.Min(10000, (int) Math.Floor((num6 - num5) / num3 + 0.5) + 1);
                    if (num3 != 0.0)
                        num8 = Math.Abs((int) Math.Floor(num3 / num4 + 0.5)) - 1;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = 0;
                    string str2 = "";
                    int num12 = 0;
                    for (int index1 = 0; index1 < num7; ++index1)
                    {
                        double num13 = num5 + (double) index1 * num3;
                        switch (this.fType)
                        {
                            case EAxisType.Numeric:
                                str2 = this.fLabelFormat != null ? num13.ToString(this.fLabelFormat) : num13.ToString("F1");
                                break;
                            case EAxisType.DateTime:
                                str2 = this.fLabelFormat != null ? new DateTime((long) num13).ToString(this.fLabelFormat) : new DateTime((long) num13).ToString("MMM yyyy");
                                break;
                        }
                        if (this.fPosition == EAxisPosition.Bottom)
                        {
                            if (this.fGridEnabled)
                                this.fPad.DrawVerticalGrid(Pen1, num13);
                            if (this.fMajorTicksEnabled)
                                this.fPad.DrawVerticalTick(Pen4, num13, this.fY2 - 1.0, -this.fMajorTicksLength);
                            if (this.fLabelEnabled)
                            {
                                SizeF sizeF = this.fPad.Graphics.MeasureString(str2, this.fLabelFont);
                                int num14 = (int) sizeF.Width;
                                num12 = (int) sizeF.Height;
                                if (this.fLabelAlignment == EAxisLabelAlignment.Right)
                                    this.fPad.Graphics.DrawString(str2, this.fLabelFont, (Brush) solidBrush2, (float) this.fPad.ClientX(num13), (float) (int) (this.fY2 + (double) this.fLabelOffset));
                                if (this.fLabelAlignment == EAxisLabelAlignment.Left)
                                    this.fPad.Graphics.DrawString(str2, this.fLabelFont, (Brush) solidBrush2, (float) (this.fPad.ClientX(num13) - num14), (float) (int) (this.fY2 + (double) this.fLabelOffset));
                                if (this.fLabelAlignment == EAxisLabelAlignment.Centre)
                                {
                                    num9 = this.fPad.ClientX(num13) - num14 / 2;
                                    int num15 = (int) (this.fY2 + (double) this.fLabelOffset);
                                    if (index1 == 0 || num9 - num10 >= 1)
                                    {
                                        this.fPad.Graphics.DrawString(str2, this.fLabelFont, (Brush) solidBrush2, (float) num9, (float) num15);
                                        num10 = num9 + num14;
                                    }
                                }
                            }
                        }
                        if (this.fPosition == EAxisPosition.Left || this.fPosition == EAxisPosition.Right)
                        {
                            if (this.fPosition == EAxisPosition.Left && this.fGridEnabled)
                                this.fPad.DrawHorizontalGrid(Pen1, num13);
                            if (this.fPosition == EAxisPosition.Right && (!this.fPad.AxisLeft.Enabled || !this.fPad.AxisLeft.GridEnabled) && this.fGridEnabled)
                                this.fPad.DrawHorizontalGrid(Pen1, num13);
                            if (this.fMajorTicksEnabled)
                            {
                                switch (this.fPosition)
                                {
                                    case EAxisPosition.Left:
                                        this.fPad.DrawHorizontalTick(Pen4, this.fX1 + 1.0, num13, this.fMajorTicksLength);
                                        break;
                                    case EAxisPosition.Right:
                                        this.fPad.DrawHorizontalTick(Pen4, this.fX1 - (double) this.fMajorTicksLength - 1.0, num13, this.fMajorTicksLength);
                                        break;
                                }
                            }
                            if (this.fLabelEnabled)
                            {
                                SizeF sizeF = this.fPad.Graphics.MeasureString(str2, this.fLabelFont);
                                int num14 = (int) ((double) sizeF.Width + (double) this.fLabelOffset);
                                int num15 = (int) sizeF.Height;
                                if (this.fLabelAlignment == EAxisLabelAlignment.Centre)
                                {
                                    switch (this.fPosition)
                                    {
                                        case EAxisPosition.Left:
                                            num9 = (int) (this.fX1 - (double) num14);
                                            break;
                                        case EAxisPosition.Right:
                                            num9 = (int) (this.fX1 + 2.0);
                                            break;
                                    }
                                    int num16 = this.fPad.ClientY(num13) - num15 / 2;
                                    if (index1 == 0 || num11 - (num16 + num15) >= 1)
                                    {
                                        if ((double) num16 > this.fY1 && (double) (num16 + num15) < this.fY2)
                                            this.fPad.Graphics.DrawString(str2, this.fLabelFont, (Brush) solidBrush2, (float) num9, (float) num16);
                                        num11 = num16;
                                    }
                                }
                            }
                        }
                        for (int index2 = 1; index2 <= num8; ++index2)
                        {
                            double num14 = num5 + (double) index1 * num3 + (double) index2 * num4;
                            if (num14 < this.fMax)
                            {
                                if (this.fPosition == EAxisPosition.Bottom)
                                {
                                    if (this.fMinorGridEnabled)
                                        this.fPad.DrawVerticalGrid(Pen2, num14);
                                    if (this.fMinorTicksEnabled)
                                        this.fPad.DrawVerticalTick(Pen3, num14, this.fY2 - 1.0, -this.fMinorTicksLength);
                                }
                                if (this.fPosition == EAxisPosition.Left || this.fPosition == EAxisPosition.Right)
                                {
                                    if (this.fPosition == EAxisPosition.Left && this.fMinorGridEnabled)
                                        this.fPad.DrawHorizontalGrid(Pen1, num14);
                                    if (this.fPosition == EAxisPosition.Right && (!this.fPad.AxisLeft.Enabled || !this.fPad.AxisLeft.MinorGridEnabled) && this.fMinorGridEnabled)
                                        this.fPad.DrawHorizontalGrid(Pen1, num14);
                                    if (this.fMinorTicksEnabled)
                                    {
                                        switch (this.fPosition)
                                        {
                                            case EAxisPosition.Left:
                                                this.fPad.DrawHorizontalTick(Pen3, this.fX1 + 1.0, num14, this.fMinorTicksLength);
                                                continue;
                                            case EAxisPosition.Right:
                                                this.fPad.DrawHorizontalTick(Pen3, this.fX1 - (double) this.fMinorTicksLength - 1.0, num14, this.fMinorTicksLength);
                                                continue;
                                            default:
                                                continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int index = 1; index <= num8; ++index)
                    {
                        double num13 = num5 - (double) index * num4;
                        if (num13 > this.fMin)
                        {
                            if (this.fPosition == EAxisPosition.Bottom)
                            {
                                if (this.fMinorGridEnabled)
                                    this.fPad.DrawVerticalGrid(Pen2, num13);
                                if (this.fMinorTicksEnabled)
                                    this.fPad.DrawVerticalTick(Pen3, num13, this.fY2 - 1.0, -this.fMinorTicksLength);
                            }
                            if (this.fPosition == EAxisPosition.Left || this.fPosition == EAxisPosition.Right)
                            {
                                if (this.fPosition == EAxisPosition.Left && this.fMinorGridEnabled)
                                    this.fPad.DrawHorizontalGrid(Pen1, num13);
                                if (this.fPosition == EAxisPosition.Right && (!this.fPad.AxisLeft.Enabled || !this.fPad.AxisLeft.MinorGridEnabled) && this.fMinorGridEnabled)
                                    this.fPad.DrawHorizontalGrid(Pen1, num13);
                                if (this.fMinorTicksEnabled)
                                {
                                    switch (this.fPosition)
                                    {
                                        case EAxisPosition.Left:
                                            this.fPad.DrawHorizontalTick(Pen3, this.fX1 + 1.0, num13, this.fMinorTicksLength);
                                            continue;
                                        case EAxisPosition.Right:
                                            this.fPad.DrawHorizontalTick(Pen3, this.fX1 - (double) this.fMinorTicksLength - 1.0, num13, this.fMinorTicksLength);
                                            continue;
                                        default:
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                    if (this.fOutlineEnabled)
                    {
                        if (this.fPosition == EAxisPosition.Bottom)
                        {
                            this.fPad.DrawVerticalGrid(new Pen(Color.Green), this.fPad.WorldX(this.fOutline1));
                            this.fPad.DrawVerticalGrid(new Pen(Color.Green), this.fPad.WorldX(this.fOutline2));
                        }
                        if (this.fPosition == EAxisPosition.Left)
                        {
                            this.fPad.DrawHorizontalGrid(new Pen(Color.Green), this.fPad.WorldY(this.fOutline1));
                            this.fPad.DrawHorizontalGrid(new Pen(Color.Green), this.fPad.WorldY(this.fOutline2));
                        }
                    }
                    if (this.fTitleEnabled)
                    {
                        int num13 = (int) this.fPad.Graphics.MeasureString("Example", this.fLabelFont).Height;
                        int num14 = (int) this.fPad.Graphics.MeasureString(this.fMax.ToString("F1"), this.fLabelFont).Width;
                        int num15 = (int) this.fPad.Graphics.MeasureString(this.fTitle, this.fTitleFont).Height;
                        int num16 = (int) this.fPad.Graphics.MeasureString(this.fTitle, this.fTitleFont).Width;
                        if (this.fPosition == EAxisPosition.Bottom)
                        {
                            if (this.fTitlePosition == EAxisTitlePosition.Left)
                                this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) this.fX1, (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
                            if (this.fTitlePosition == EAxisTitlePosition.Right)
                                this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) ((int) this.fX2 - num16), (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
                            if (this.fTitlePosition == EAxisTitlePosition.Centre)
                                this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) (this.fX1 + (this.fX2 - this.fX1 - (double) num16) / 2.0), (float) (int) (this.fY2 + (double) this.fLabelOffset + (double) num13 + (double) this.fTitleOffset));
                        }
                        if (this.fPosition == EAxisPosition.Left && this.fTitlePosition == EAxisTitlePosition.Centre)
                        {
                            this.fPad.Graphics.DrawString(this.fTitle, this.fTitleFont, (Brush) solidBrush1, (float) (int) (this.fX1 - (double) this.fLabelOffset - (double) num14 - (double) this.fTitleOffset - (double) num15), (float) (int) (this.fY1 + (this.fY2 - this.fY1 - (double) num16) / 2.0), new StringFormat()
                            {
                                FormatFlags = StringFormatFlags.DirectionRightToLeft | StringFormatFlags.DirectionVertical
                            });
                            this.fPad.Graphics.ResetTransform();
                        }
                    }
                    if (!flag)
                        return;
                    this.fType = EAxisType.DateTime;
                    this.fLabelFormat = str1;
                }
            }
            catch
            {
            }
        }

        public virtual void MouseMove(MouseEventArgs Event)
        {
            if (!this.fMouseDown)
                return;
            switch (this.fPosition)
            {
                case EAxisPosition.Left:
                    if (!this.fPad.MouseZoomYAxisEnabled)
                        break;
                    this.fOutline2 = Event.Y;
                    this.fPad.Update();
                    break;
                case EAxisPosition.Bottom:
                    if (!this.fPad.MouseZoomXAxisEnabled)
                        break;
                    this.fOutline2 = Event.X;
                    this.fPad.Update();
                    break;
            }
        }

        public virtual void MouseDown(MouseEventArgs Event)
        {
            if (Event.Button != MouseButtons.Left)
                return;
            switch (this.fPosition)
            {
                case EAxisPosition.Left:
                    if (!this.fPad.MouseZoomYAxisEnabled || this.fX1 - 10.0 > (double) Event.X || (this.fX1 < (double) Event.X || this.fY1 > (double) Event.Y) || this.fY2 < (double) Event.Y)
                        break;
                    this.fMouseDown = true;
                    this.fMouseDownX = Event.X;
                    this.fMouseDownY = Event.Y;
                    this.fOutline1 = Event.Y;
                    this.fOutlineEnabled = true;
                    break;
                case EAxisPosition.Bottom:
                    if (!this.fPad.MouseZoomXAxisEnabled || this.fX1 > (double) Event.X || (this.fX2 < (double) Event.X || this.fY1 > (double) Event.Y) || this.fY1 + 10.0 < (double) Event.Y)
                        break;
                    this.fMouseDown = true;
                    this.fMouseDownX = Event.X;
                    this.fMouseDownY = Event.Y;
                    this.fOutline1 = Event.X;
                    this.fOutlineEnabled = true;
                    break;
            }
        }

        public virtual void MouseUp(MouseEventArgs Event)
        {
            this.fOutlineEnabled = false;
            if (Event.Button == MouseButtons.Right)
            {
                switch (this.fPosition)
                {
                    case EAxisPosition.Left:
                        if (this.fX1 - 10.0 <= (double) Event.X && this.fX1 >= (double) Event.X && (this.fY1 <= (double) Event.Y && this.fY2 >= (double) Event.Y))
                        {
                            this.UnZoom();
                            break;
                        }
                        else
                            break;
                    case EAxisPosition.Bottom:
                        if (this.fX1 <= (double) Event.X && this.fX2 >= (double) Event.X && (this.fY1 <= (double) Event.Y && this.fY1 + 10.0 >= (double) Event.Y))
                        {
                            this.UnZoom();
                            break;
                        }
                        else
                            break;
                }
            }
            if (this.fMouseDown && Event.Button == MouseButtons.Left)
            {
                switch (this.fPosition)
                {
                    case EAxisPosition.Left:
                        if (this.fPad.MouseZoomYAxisEnabled)
                        {
                            double num1 = this.fPad.WorldY(this.fMouseDownY);
                            double num2 = this.fPad.WorldY(Event.Y);
                            if (num1 < num2)
                            {
                                this.Zoom(num1, num2);
                                break;
                            }
                            else
                            {
                                this.Zoom(num2, num1);
                                break;
                            }
                        }
                        else
                            break;
                    case EAxisPosition.Bottom:
                        if (this.fPad.MouseZoomXAxisEnabled)
                        {
                            double num1 = this.fPad.WorldX(this.fMouseDownX);
                            double num2 = this.fPad.WorldX(Event.X);
                            if (num1 < num2)
                            {
                                this.Zoom(num1, num2);
                                break;
                            }
                            else
                            {
                                this.Zoom(num2, num1);
                                break;
                            }
                        }
                        else
                            break;
                }
            }
            this.fMouseDown = false;
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
