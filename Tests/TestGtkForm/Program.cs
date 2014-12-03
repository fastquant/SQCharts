using System;
using Gtk;

namespace TestGtkForm
{
    public class MyControl : Gtk.EventBox
    {
        HScrollbar hScrollBar;
        double sbOldValue;
        public MyControl()
        {
            Events = Gdk.EventMask.AllEventsMask;
            var vb = new VBox();
            var hScrollBar = new HScrollbar(new Adjustment(1, 1, 100, 1, 20, 10));
            hScrollBar.ChangeValue += (o, args) =>
            {
                //Console.WriteLine("Arg.Value: {0}", args.Args[1]);
                Console.WriteLine("scrollBar.Value: {0}", hScrollBar.Value);
                Console.WriteLine("scrollBar.OldValue: {0}", sbOldValue);
                sbOldValue = hScrollBar.Value;
            };
            hScrollBar.ValueChanged += (o, args) =>
            {
                Console.WriteLine("scrollBar.Value: {0}", hScrollBar.Value);
            };
            vb.PackEnd(hScrollBar, false, false, 0);
            Add(vb);
            ShowAll();

        }

        protected override bool OnMotionNotifyEvent(Gdk.EventMotion evnt)
        {
            GdkWindow.Cursor = new Gdk.Cursor(Gdk.CursorType.SbVDoubleArrow);
            return base.OnMotionNotifyEvent(evnt);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
            : base(WindowType.Toplevel)
        {
            var control = new MyControl();
            SetDefaultSize(400, 300);
            Add(control);
            ShowAll();
            DeleteEvent += (sender, e) =>
            {
                Application.Quit();
                e.RetVal = true;
            };
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
