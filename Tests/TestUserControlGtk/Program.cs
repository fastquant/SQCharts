using System;
using Gtk;
using Compatibility.Gtk;
using System.Drawing;

namespace TestUserControlGtk
{
    class MyUserControl : UserControl
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.Clear(Color.Blue);
            pe.Graphics.DrawString("ttteeee", new Font("arial", 8f), new SolidBrush(Color.Red), new PointF(50, 50));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseMove");
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseWheel");
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //                        Console.WriteLine("OnMouseDown");
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //            Console.WriteLine("OnMouseUp");
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            //            Console.WriteLine("OnMouseEnter");
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //            Console.WriteLine("OnMouseLeave");
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            //                        Console.WriteLine("OnDoubleClick");
        }

        protected override void OnResize(EventArgs e)
        {
            //            Console.WriteLine("ScreenBounds:{0}", this.ScreenBounds);
            //            Console.WriteLine("Bounds:{0}", this.Bounds);
        }

        protected override void OnKeyPress(Compatibility.Gtk.KeyPressEventArgs e)
        {
                        Console.WriteLine("OnKeyPress");
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
