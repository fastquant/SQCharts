using System;
using System.Windows.Forms;
using System.ComponentModel;
using SmartQuant;
using SmartQuant.Charting;
using SmartQuant.FinChart;

namespace TestWinForm
{
    public class MainForm : Form
    {
        private IContainer components = null;
        private global::SmartQuant.Charting.Chart chart1;
        private global::SmartQuant.FinChart.Chart chart2;

        public MainForm()
        {
            InitializeComponent();
            chart2.AddPad();
            chart1.AddPad(0, 0.2, 1, 0.6);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chart2 = new SmartQuant.FinChart.Chart();
            this.chart1 = new SmartQuant.Charting.Chart();
            this.SuspendLayout();
            // 
            // chart2
            // 
//            this.chart2.ActionType = SmartQuant.FinChart.ChartActionType.None;
//            this.chart2.AutoScroll = true;
//            this.chart2.BarSeriesStyle = SmartQuant.FinChart.BSStyle.Candle;
//            this.chart2.BorderColor = System.Drawing.Color.Gray;
//            this.chart2.BottomAxisGridColor = System.Drawing.Color.LightGray;
//            this.chart2.BottomAxisLabelColor = System.Drawing.Color.LightGray;
//            this.chart2.CanvasColor = System.Drawing.Color.MidnightBlue;
//            this.chart2.ChartBackColor = System.Drawing.Color.MidnightBlue;
//            this.chart2.ContextMenuEnabled = true;
//            this.chart2.CrossColor = System.Drawing.Color.DarkGray;
//            this.chart2.DateTipRectangleColor = System.Drawing.Color.LightGray;
//            this.chart2.DateTipTextColor = System.Drawing.Color.Black;
//            this.chart2.DrawItems = false;
//            this.chart2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.chart2.ItemTextColor = System.Drawing.Color.LightGray;
//            this.chart2.LabelDigitsCount = 2;
//            this.chart2.Location = new System.Drawing.Point(12, 12);
//            this.chart2.MinNumberOfBars = 125;
            this.chart2.Name = "chart2";
            this.chart2.PrimitiveDeleteImage = null;
            this.chart2.PrimitivePropertiesImage = null;
            this.chart2.RightAxesFontSize = 7;
            this.chart2.RightAxisGridColor = System.Drawing.Color.DimGray;
            this.chart2.RightAxisMajorTicksColor = System.Drawing.Color.LightGray;
            this.chart2.RightAxisMinorTicksColor = System.Drawing.Color.LightGray;
            this.chart2.RightAxisTextColor = System.Drawing.Color.LightGray;
            this.chart2.ScaleStyle = SmartQuant.FinChart.PadScaleStyle.Arith;
            this.chart2.SelectedFillHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.chart2.SelectedItemTextColor = System.Drawing.Color.Yellow;
            this.chart2.SessionEnd = System.TimeSpan.Parse("00:00:00");
            this.chart2.SessionGridColor = System.Drawing.Color.Empty;
            this.chart2.SessionGridEnabled = false;
            this.chart2.SessionStart = System.TimeSpan.Parse("00:00:00");
            this.chart2.Dock = DockStyle.Left;
            this.chart2.Size = new System.Drawing.Size(358, 424);
            this.chart2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.chart2.SplitterColor = System.Drawing.Color.LightGray;
            this.chart2.TabIndex = 1;
            this.chart2.UpdateStyle = SmartQuant.FinChart.ChartUpdateStyle.Trailing;
            this.chart2.ValTipRectangleColor = System.Drawing.Color.LightGray;
            this.chart2.ValTipTextColor = System.Drawing.Color.Black;
            this.chart2.VolumePadVisible = false;
            // 
            // chart1
            // 
            this.chart1.AntiAliasingEnabled = false;
            this.chart1.DoubleBufferingEnabled = true;
            this.chart1.FileName = null;
            this.chart1.GroupLeftMarginEnabled = false;
            this.chart1.GroupRightMarginEnabled = false;
            this.chart1.GroupZoomEnabled = false;
            this.chart1.Location = new System.Drawing.Point(376, 12);
            this.chart1.Name = "chart1";
            this.chart1.PadsForeColor = System.Drawing.Color.White;
            this.chart1.PrintAlign = SmartQuant.Charting.EPrintAlign.None;
            this.chart1.PrintHeight = 400;
            this.chart1.PrintLayout = SmartQuant.Charting.EPrintLayout.Portrait;
            this.chart1.PrintWidth = 600;
            this.chart1.PrintX = 10;
            this.chart1.PrintY = 10;
            this.chart1.SessionEnd = System.TimeSpan.Parse("1.00:00:00");
            this.chart1.SessionGridColor = System.Drawing.Color.Blue;
            this.chart1.SessionGridEnabled = false;
            this.chart1.SessionStart = System.TimeSpan.Parse("00:00:00");
            this.chart1.Size = new System.Drawing.Size(366, 424);
            this.chart1.SmoothingEnabled = false;
            this.chart1.TabIndex = 2;
            this.chart1.TransformationType = SmartQuant.Charting.ETransformationType.Empty;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 448);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.Name = "MainForm";
            this.Text = "SmartQuant.FinChart.Test";
            this.ResumeLayout(false);
        }
    }

    class MainClass
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
