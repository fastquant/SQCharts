// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

#if XWT
namespace SmartQuant.Charting
{
    public class Form : Xwt.Window
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public void Update()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}

#else
using System.Drawing.Printing;
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    public class Canvas : Form
    {
        private Chart fChart;

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
                return this.fChart.GroupZoomEnabled;
            }
            set
            {
                this.fChart.GroupZoomEnabled = value;
            }
        }

        public bool GroupLeftMarginEnabled
        {
            get
            {
                return this.fChart.GroupLeftMarginEnabled;
            }
            set
            {
                this.fChart.GroupLeftMarginEnabled = value;
            }
        }

        public bool DoubleBufferingEnabled
        {
            get
            {
                return this.fChart.DoubleBufferingEnabled;
            }
            set
            {
                this.fChart.DoubleBufferingEnabled = value;
            }
        }

        public bool SmoothingEnabled
        {
            get
            {
                return this.fChart.SmoothingEnabled;
            }
            set
            {
                this.fChart.SmoothingEnabled = value;
            }
        }

        public bool AntiAliasingEnabled
        {
            get
            {
                return this.fChart.AntiAliasingEnabled;
            }
            set
            {
                this.fChart.AntiAliasingEnabled = value;
            }
        }

        public PrintDocument PrintDocument
        {
            get
            {
                return this.fChart.PrintDocument;
            }
            set
            {
                this.fChart.PrintDocument = value;
            }
        }

        public int PrintX
        {
            get
            {
                return this.fChart.PrintX;
            }
            set
            {
                this.fChart.PrintX = value;
            }
        }

        public int PrintY
        {
            get
            {
                return this.fChart.PrintY;
            }
            set
            {
                this.fChart.PrintY = value;
            }
        }

        public int PrintWidth
        {
            get
            {
                return this.fChart.PrintWidth;
            }
            set
            {
                this.fChart.PrintWidth = value;
            }
        }

        public int PrintHeight
        {
            get
            {
                return this.fChart.PrintHeight;
            }
            set
            {
                this.fChart.PrintHeight = value;
            }
        }

        public EPrintAlign PrintAlign
        {
            get
            {
                return this.fChart.PrintAlign;
            }
            set
            {
                this.fChart.PrintAlign = value;
            }
        }

        public EPrintLayout PrintLayout
        {
            get
            {
                return this.fChart.PrintLayout;
            }
            set
            {
                this.fChart.PrintLayout = value;
            }
        }

        public Chart Chart { get; private set; }

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
            throw new NotImplementedException();
        }

        public Canvas(string name, string title, string fileName, int width, int height)
        {
            InitializeComponent();
            Name = name;
            Text = title;
            Width = width;
            Height = height;        
        }

        public Pad cd(int pad)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void UpdateChart()
        {
            throw new NotImplementedException();
        }

        public new void Update()
        {
            throw new NotImplementedException();
        }

        public Pad AddPad(double x1, double y1, double x2, double y2)
        {
            throw new NotImplementedException();
        }

        public void Divide(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public virtual void Print()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintPreview()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintSetup()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintPageSetup()
        {
            throw new NotImplementedException();
        }
    }
}

