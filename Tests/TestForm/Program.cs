using System;
using System.Windows.Forms;
using System.Drawing;

namespace TestForm
{
    public class MyControl : UserControl
    {
        HScrollBar scrollBar;
        public MyControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Cursor = Cursors.HSplit;
            scrollBar = new HScrollBar();
            scrollBar.Maximum = 100;
            scrollBar.LargeChange = 20;
            scrollBar.Dock = DockStyle.Bottom;
            scrollBar.Scroll += (sender, e) =>
            {
                Console.WriteLine("scrollBar.Value: {0}", scrollBar.Value);
            };
            Controls.Add(scrollBar);

        }
    }

    public partial class MainForm : Form
    {
        MyControl control;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            Console.WriteLine("ClientSize:{0}", control.ClientSize);
            base.OnShown(e);
        }
        private void InitializeComponent()
        {
            control = new MyControl();
            control.Dock = DockStyle.Fill;
            Size = new Size(400, 300);
            Controls.Add(control);
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
