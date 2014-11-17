using System;
using Gtk;
using GLib;
using SmartQuant;
using SmartQuant.Controls;
using SmartQuant.Controls.BarChart;
using SmartQuant.FinChart;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DemoGtk
{
    public partial class MainWindow : Window
    {
        private BarChart barChart;
        private BarChart2 barChart2;
        private Chart chart3;
        private Portfolio portfolio;

        public MainWindow()
            : base(WindowType.Toplevel)
        {
            this.barChart = new SmartQuant.Controls.BarChart.BarChart();
            this.barChart2 = new SmartQuant.Controls.BarChart.BarChart2();
            this.chart3 = new SmartQuant.FinChart.Chart();

            var nb = new Notebook();
            nb.Add(barChart);
            nb.SetTabLabelText(barChart, "Chart");
            nb.Add(barChart2);
            nb.SetTabLabelText(barChart2, "Chart(Gapless)");
            nb.Add(chart3);
            nb.SetTabLabelText(chart3, "Performance");
            Add(nb);
            SetDefaultSize(624, 362);
         
            DeleteEvent += (sender, e) =>
            {
                Application.Quit();
                e.RetVal = true;
            };

            var f = new Framework("Demo", true);
            f.IsDisposable = false;
            f.GroupDispatcher = new GroupDispatcher(f);
            this.barChart.Init(f, null, null);
            this.barChart2.Init(f, null, null);
            this.barChart.ResumeUpdates();
            this.barChart2.ResumeUpdates();

            GLib.Timeout.Add(500, new TimeoutHandler(delegate
            {
                barChart.UpdateGUI();
                barChart2.UpdateGUI();
                return true;
            }));

            // Until the framework is created, We cannot show them
            ShowAll();
        }

        protected override void OnShown()
        {
            base.OnShown();
            var f = Framework.Current;
            new System.Threading.Thread(new ThreadStart(() =>
            {
                Console.WriteLine(f.Name);
                Scenario scenario = new Demo.Backtest(f);
                scenario.Run();
                Reset();
            })).Start();
        }

        private void Reset()
        {
            Gtk.Application.Invoke((sender, args) =>
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
                performance.Updated += new EventHandler((o, e) =>
                {
                    this.chart3.OnItemAdedd(this.portfolio.Performance.EquitySeries.LastDateTime);
                });
            });
        }
    }

    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            var win = new MainWindow();
            win.Show();
            Application.Run();
            Framework.Current.IsDisposable = true;
            Framework.Current.Dispose();
        }
    }
}
