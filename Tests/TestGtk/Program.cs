using System;
using Gtk;

namespace TestGtk
{
    public partial class MainWindow: Gtk.Window
    {

        public MainWindow()
            : base(Gtk.WindowType.Toplevel)
        {
            var chart1 = new SmartQuant.Charting.Chart();
            var chart2 = new SmartQuant.FinChart.Chart();
            HBox hb = new HBox();
            hb.PackStart(chart1);
            hb.PackStart(chart2);
            this.Add(hb);
            this.SetDefaultSize(400, 300);
            this.ShowAll();
            this.DeleteEvent += new DeleteEventHandler((sender, e) =>
            {
                Application.Quit();
                e.RetVal = true;
            });
            ;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
