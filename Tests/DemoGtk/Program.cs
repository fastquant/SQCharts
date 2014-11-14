using System;
using Gtk;
using GLib;
using SmartQuant;
using SmartQuant.Controls.BarChart;
using SmartQuant.FinChart;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Demo
{
    public partial class MainWindow : Window
    {
        private BarChart barChart;
        private BarChart2 barChart2;
        private Chart chart3;
        private Portfolio portfolio;
        private Framework framework;

        public MainWindow()
            : base(WindowType.Toplevel)
        {
            this.barChart = new SmartQuant.Controls.BarChart.BarChart();
            this.barChart2 = new SmartQuant.Controls.BarChart.BarChart2();
            this.chart3 = new SmartQuant.FinChart.Chart();

            var nb = new  Notebook();
//            var sw1 = new ScrolledWindow();
//            sw1.AddWithViewport(barChart);
            nb.Add(barChart);
            nb.SetTabLabelText(barChart, "Chart");
//            var sw2 = new ScrolledWindow();
//            sw2.AddWithViewport(barChart2);
            nb.Add(barChart2);
            nb.SetTabLabelText(barChart2, "Chart(Gapless)");
//            var sw3 = new ScrolledWindow();
//            sw3.AddWithViewport(chart3);
            nb.Add(chart3);
            nb.SetTabLabelText(chart3, "Performance");
            Add(nb);
            SetDefaultSize(800, 300);
            ShowAll();
            DeleteEvent += (sender, e) =>
            {
                Application.Quit();
                e.RetVal = true;
            };

            var f = new Framework("Demo", true);
            f.IsDisposable = false;
            f.GroupDispatcher = new GroupDispatcher(f);
            this.framework = f;
            this.barChart.Init(f, null, null);
            this.barChart2.Init(f, null, null);
            this.barChart.ResumeUpdates();
            this.barChart2.ResumeUpdates();
            Scenario scenario = new Backtest(f);
            scenario.Run();
            Reset();
            barChart.UpdateGUI();
            barChart2.UpdateGUI();
        }

        //        protected override void OnShown()
        //        {
        //            base.OnShown();
        ////            new System.Threading.Thread(new ThreadStart(() =>
        ////            {
        //            Scenario scenario = new Backtest(Framework.Current);
        //                scenario.Run();
        //                Reset();
        ////            })).Start();
        //        }

        private void Reset()
        {
            this.portfolio = Framework.Current.PortfolioManager.Portfolios.GetByIndex(0);
            if (this.portfolio == null)
                return;
            PortfolioPerformance performance = this.portfolio.Performance;
            this.chart3.Reset();
            this.chart3.SetMainSeries(performance.EquitySeries, false, Color.White);
            this.chart3.AddPad();
            this.chart3.DrawSeries(performance.DrawdownSeries, 2, Color.White, SimpleDSStyle.Line, SearchOption.ExactFirst, SmoothingMode.HighSpeed);
            this.chart3.UpdateStyle = ChartUpdateStyle.WholeRange;
            performance.Updated += new EventHandler((sender, e) =>
            {
                this.chart3.OnItemAdedd(this.portfolio.Performance.EquitySeries.LastDateTime);
            });
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            var win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
