using System;
using Xwt;
using Xwt.Drawing;
using SmartQuant.FinChart;
using SmartQuant.Charting;

namespace TestXwt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = "Xwt Demo Application";
            Width = 500;
            Height = 400;
            HBox hb = new HBox();
            SmartQuant.FinChart.Chart chart1 = new SmartQuant.FinChart.Chart();
            SmartQuant.Charting.Chart chart2 = new SmartQuant.Charting.Chart();
            hb.PackStart(chart1, true);
            hb.PackStart(chart2, true);
            Content = hb;
            hb.Show();
        }
    }
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Initialize(ToolkitType.Gtk);
            MainWindow w = new MainWindow();
            w.Show();
            Application.Run();
            w.Dispose();
            Application.Dispose();
        }
    }
}
