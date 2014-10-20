// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
using System.IO;


#if XWT
using Compatibility.Xwt;
#else
using System.Drawing.Printing;
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    public partial class Canvas : Form
    {
        private Container components;
        private Chart fChart;

        public Chart Chart { get; private set; }

        public Pad Pad
        {
            get
            {
                return Chart.Pad;
            }
        }

        public static bool FileEnabled { get; set; }

        public static string FileDir { get; set; }

        public static string FileNamePrefix { get; set; }

        public static string FileNameSuffix { get; set; }

        public bool GroupZoomEnabled
        {
            get
            {
                return Chart.GroupZoomEnabled;
            }
            set
            {
                Chart.GroupZoomEnabled = value;
            }
        }

        public bool GroupLeftMarginEnabled
        {
            get
            {
                return Chart.GroupLeftMarginEnabled;
            }
            set
            {
                Chart.GroupLeftMarginEnabled = value;
            }
        }

        public bool DoubleBufferingEnabled
        {
            get
            {
                return Chart.DoubleBufferingEnabled;
            }
            set
            {
                Chart.DoubleBufferingEnabled = value;
            }
        }

        public bool SmoothingEnabled
        {
            get
            {
                return Chart.SmoothingEnabled;
            }
            set
            {
                Chart.SmoothingEnabled = value;
            }
        }

        public bool AntiAliasingEnabled
        {
            get
            {
                return Chart.AntiAliasingEnabled;
            }
            set
            {
                Chart.AntiAliasingEnabled = value;
            }
        }

        public PrintDocument PrintDocument
        {
            get
            {
                return Chart.PrintDocument;
            }
            set
            {
                Chart.PrintDocument = value;
            }
        }

        public int PrintX
        {
            get
            {
                return Chart.PrintX;
            }
            set
            {
                Chart.PrintX = value;
            }
        }

        public int PrintY
        {
            get
            {
                return Chart.PrintY;
            }
            set
            {
                Chart.PrintY = value;
            }
        }

        public int PrintWidth
        {
            get
            {
                return Chart.PrintWidth;
            }
            set
            {
                Chart.PrintWidth = value;
            }
        }

        public int PrintHeight
        {
            get
            {
                return Chart.PrintHeight;
            }
            set
            {
                Chart.PrintHeight = value;
            }
        }

        public EPrintAlign PrintAlign
        {
            get
            {
                return Chart.PrintAlign;
            }
            set
            {
                Chart.PrintAlign = value;
            }
        }

        public EPrintLayout PrintLayout
        {
            get
            {
                return Chart.PrintLayout;
            }
            set
            {
                Chart.PrintLayout = value;
            }
        }


        public Canvas()
            : this("Canvas", "SmartQuant Canvas")
        {
        }

        public Canvas(string name)
            : this(name, name)
        {
        }

        public Canvas(string name, string title)
            : this(name, title, null)
        {
        }

        public Canvas(string name, string title, string fileName)
            : this(name, title, fileName, 0, 0)
        {
        }

        public Canvas(string name, string title, int width, int height)
            : this(name, title, null, width, height)
        {
        }

        public Canvas(string name, int width, int height)
            : this(name, name, null, width, height)
        {
        }
            
        private void InitializeComponent()
        {
            #if XWT
            Chart = new Chart();
            #else
            Chart = new Chart();
            #endif
        }

        public Canvas(string name, string title, string fileName, int width, int height)
        {
            InitializeComponent();
            Name = name;
            Text = title;
            Width = width;
            Height = height;
            CanvasManager.Add(this);
            // TODO: FileEnabled?
            Chart.FileName = fileName ?? Path.Combine(FileDir, string.Format("{0}{1}{2}{3}.gif", FileNamePrefix, Name, DateTime.Now.ToString("MMddyyyhhmmss"), FileNameSuffix));
            Show();
        }

        public Pad cd(int pad)
        {
            return Chart.cd(pad);
        }

        public void Clear()
        {
            Chart.Clear();
        }

        public void UpdateChart()
        {
            Chart.UpdatePads();
        }

        public new void Update()
        {
            base.Update();
            this.UpdateChart();
        }

        public Pad AddPad(double x1, double y1, double x2, double y2)
        {
            return Chart.AddPad(x1, y1, x2, y2);
        }

        public void Divide(int x, int y)
        {
            Chart.Divide(x, y);
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            Chart.Divide(x, y, widths, heights);
        }

        protected override void Dispose(bool disposing)
        {
            CanvasManager.Remove(this);
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        public virtual void Print()
        {
            Chart.Print();
        }

        public virtual void PrintPreview()
        {
            Chart.PrintPreview();
        }

        public virtual void PrintSetup()
        {
            Chart.PrintSetup();
        }

        public virtual void PrintPageSetup()
        {
            Chart.PrintPageSetup();
        }
    }
}

