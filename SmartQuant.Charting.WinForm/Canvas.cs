using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
    public class Canvas : Form
    {
        private static string fFileDir = "";
        private static string fFileNamePrefix = "";
        private static string fFileNameSuffix = "";
        private static bool fFileEnabled = false;
        private Chart fChart;

        public Pad Pad
        {
            get
            {
                return Chart.Pad;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public static bool FileEnabled
        {
            get
            {
                return Canvas.fFileEnabled;
            }
            set
            {
                Canvas.fFileEnabled = value;
            }
        }

        public static string FileDir
        {
            get
            {
                return Canvas.fFileDir;
            }
            set
            {
                Canvas.fFileDir = value;
            }
        }

        public static string FileNamePrefix
        {
            get
            {
                return Canvas.fFileNamePrefix;
            }
            set
            {
                Canvas.fFileNamePrefix = value;
            }
        }

        public static string FileNameSuffix
        {
            get
            {
                return Canvas.fFileNameSuffix;
            }
            set
            {
                Canvas.fFileNameSuffix = value;
            }
        }

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

        public Chart Chart
        {
            get
            {
                return this.fChart;
            }
        }

        public Canvas()
        {
            this.Init();
            this.Name = "Canvas";
            this.Text = "SmartQuant Canvas";
            this.PostInit();
            CanvasManager.Add(this);
            if (Canvas.fFileEnabled)
                return;
            Show();
        }

        public Canvas(string Name, string Title)
        {
            this.Init();
            this.Name = Name;
            this.Text = Title;
            this.PostInit();
            CanvasManager.Add(this);
            if (Canvas.FileEnabled)
                return;
            Show();
        }

        public Canvas(string name)
        {
            this.Init();
            this.Name = name;
            this.Text = name;
            this.PostInit();
            CanvasManager.Add(this);
            if (Canvas.FileEnabled)
                return;
            Show();
        }

        public Canvas(string Name, string Title, string FileName)
        {
            this.Init();
            this.Name = Name;
            this.Text = Title;
            this.PostInit();
            this.fChart.FileName = FileName;
            CanvasManager.Add(this);
        }

        public Canvas(string Name, string Title, int Width, int Height)
        {
            this.Init();
            this.Name = Name;
            this.Text = Title;
            this.PostInit();
            CanvasManager.Add(this);
            this.Width = Width;
            this.Height = Height;
            if (Canvas.FileEnabled)
                return;
            Show();
        }

        public Canvas(string Name, int Width, int Height)
        {
            this.Init();
            this.Name = Name;
            this.Text = Name;
            this.PostInit();
            CanvasManager.Add(this);
            this.Width = Width;
            this.Height = Height;
            if (Canvas.FileEnabled)
                return;
            Show();
        }

        public Canvas(string Name, string Title, string FileName, int Width, int Height)
        {
            this.Init();
            this.Name = Name;
            this.Text = Title;
            this.PostInit();
            this.fChart.FileName = FileName;
            CanvasManager.Add(this);
            this.Width = Width;
            this.Height = Height;
        }

        private void Init()
        {
            this.InitializeComponent();
        }

        private void PostInit()
        {
            if (!Canvas.fFileEnabled)
                return;
            this.fChart.FileName = Canvas.fFileDir + "//" + Canvas.fFileNamePrefix + this.Name + DateTime.Now.ToString("MMddyyyhhmmss") + Canvas.fFileNameSuffix + ".gif";
        }

        public Pad cd(int pad)
        {
            return this.fChart.cd(pad);
        }

        public void Clear()
        {
            this.fChart.Clear();
        }

        public void UpdateChart()
        {
            this.fChart.UpdatePads();
        }

        public new void Update()
        {
            base.Update();
            this.UpdateChart();
        }

        public Pad AddPad(double x1, double y1, double x2, double y2)
        {
            return this.fChart.AddPad(x1, y1, x2, y2);
        }

        public void Divide(int x, int y)
        {
            this.fChart.Divide(x, y);
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            this.fChart.Divide(x, y, widths, heights);
        }

        protected override void Dispose(bool disposing)
        {
            CanvasManager.Remove(this);
            base.Dispose(disposing);
        }

        public virtual void Print()
        {
            this.fChart.Print();
        }

        public virtual void PrintPreview()
        {
            this.fChart.PrintPreview();
        }

        public virtual void PrintSetup()
        {
            this.fChart.PrintSetup();
        }

        public virtual void PrintPageSetup()
        {
            this.fChart.PrintPageSetup();
        }

        private void InitializeComponent()
        {
            #if GTK
            #else
            this.fChart = new Chart();
            this.SuspendLayout();
            this.fChart.AntiAliasingEnabled = false;
            this.fChart.Dock = DockStyle.Fill;
            this.fChart.DoubleBufferingEnabled = true;
            this.fChart.FileName = (string) null;
            this.fChart.GroupLeftMarginEnabled = false;
            this.fChart.GroupZoomEnabled = false;
            this.fChart.ImeMode = ImeMode.Off;
            this.fChart.Location = new Point(0, 0);
            this.fChart.Name = "fChart";
            this.fChart.PrintAlign = EPrintAlign.None;
            this.fChart.PrintHeight = 400;
            this.fChart.PrintLayout = EPrintLayout.Portrait;
            this.fChart.PrintWidth = 600;
            this.fChart.PrintX = 10;
            this.fChart.PrintY = 10;
            this.fChart.Size = new Size(488, 293);
            this.fChart.SmoothingEnabled = false;
            this.fChart.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(488, 293);
            this.Controls.Add((Control) this.fChart);
            this.Name = "TCanvas";
            this.Text = "TCanvas";
            this.ResumeLayout(false);
            #endif
        }
    }
}
