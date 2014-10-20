using System;
using Xwt;
using Compatibility.Xwt;

namespace TestUserControl
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = "Xwt UserControl Application";
            Width = 500;
            Height = 400;
            HBox hb = new HBox();
            UserControl control = new UserControl();
            hb.PackStart(control, true);
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
