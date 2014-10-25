using System;
using Gtk;
using Compatibility.Gtk;

namespace TestUserControlGtk
{
    class MyUserControl : UserControl
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.Clear(Colors.Maroon);
        }
    }

    public partial class MainWindow: Gtk.Window
    {

        public MainWindow()
            : base(Gtk.WindowType.Toplevel)
        {
            var control = new MyUserControl();
            this.Add(control);
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
