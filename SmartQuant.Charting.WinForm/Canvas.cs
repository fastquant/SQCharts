// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
using System.IO;
using System.Drawing.Printing;

#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    public partial class Canvas : Form
    {
        private System.ComponentModel.Container components;
        private Chart chart;

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
                return this.chart.GroupZoomEnabled;
            }
            set
            {
                this.chart.GroupZoomEnabled = value;
            }
        }

        public bool GroupLeftMarginEnabled
        {
            get
            {
                return this.chart.GroupLeftMarginEnabled;
            }
            set
            {
                this.chart.GroupLeftMarginEnabled = value;
            }
        }

        public bool DoubleBufferingEnabled
        {
            get
            {
                return this.chart.DoubleBufferingEnabled;
            }
            set
            {
                this.chart.DoubleBufferingEnabled = value;
            }
        }

        public bool SmoothingEnabled
        {
            get
            {
                return this.chart.SmoothingEnabled;
            }
            set
            {
                this.chart.SmoothingEnabled = value;
            }
        }

        public bool AntiAliasingEnabled
        {
            get
            {
                return this.chart.AntiAliasingEnabled;
            }
            set
            {
                this.chart.AntiAliasingEnabled = value;
            }
        }

        public PrintDocument PrintDocument
        {
            get
            {
                return this.chart.PrintDocument;
            }
            set
            {
                this.chart.PrintDocument = value;
            }
        }

        public int PrintX
        {
            get
            {
                return this.chart.PrintX;
            }
            set
            {
                this.chart.PrintX = value;
            }
        }

        public int PrintY
        {
            get
            {
                return this.chart.PrintY;
            }
            set
            {
                this.chart.PrintY = value;
            }
        }

        public int PrintWidth
        {
            get
            {
                return this.chart.PrintWidth;
            }
            set
            {
                this.chart.PrintWidth = value;
            }
        }

        public int PrintHeight
        {
            get
            {
                return this.chart.PrintHeight;
            }
            set
            {
                this.chart.PrintHeight = value;
            }
        }

        public EPrintAlign PrintAlign
        {
            get
            {
                return this.chart.PrintAlign;
            }
            set
            {
                this.chart.PrintAlign = value;
            }
        }

        public EPrintLayout PrintLayout
        {
            get
            {
                return this.chart.PrintLayout;
            }
            set
            {
                this.chart.PrintLayout = value;
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
            this.chart = new Chart();
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
//            this.chart.FileName = fileName ?? Path.Combine(FileDir, string.Format("{0}{1}{2}{3}.gif", FileNamePrefix, Name, DateTime.Now.ToString("MMddyyyhhmmss"), FileNameSuffix));
            Show();
        }

        public Pad cd(int pad)
        {
            return this.chart.cd(pad);
        }

        #if GTK
        public new void Clear()
        {
            this.chart.Clear();
            base.Clear();
        }
        #else
        public void Clear()
        {
            this.chart.Clear();
        }
        #endif

        public void UpdateChart()
        {
            this.chart.UpdatePads();
        }

        public new void Update()
        {
            base.Update();
            this.UpdateChart();
        }

        public Pad AddPad(double x1, double y1, double x2, double y2)
        {
            return this.chart.AddPad(x1, y1, x2, y2);
        }

        public void Divide(int x, int y)
        {
            this.chart.Divide(x, y);
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            this.chart.Divide(x, y, widths, heights);
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
            this.chart.Print();
        }

        public virtual void PrintPreview()
        {
            this.chart.PrintPreview();
        }

        public virtual void PrintSetup()
        {
            this.chart.PrintSetup();
        }

        public virtual void PrintPageSetup()
        {
            this.chart.PrintPageSetup();
        }
    }
}

