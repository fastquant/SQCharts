using System.ComponentModel;
using System.Drawing;
using System.Resources;
#if GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    #if GTK
    public class PadProperyForm : Form
    {
        private object obj;
        private Pad pad;
        public PadProperyForm(object obj, Pad pad)
        {
            InitializeComponent();
            this.obj = obj;
            this.pad = pad;
            Text = string.Format("{0}  properties", obj.GetType().Name);
        }

        private void InitializeComponent()
        {
        }

        public int ShowDialog()
        {
            return 0;
            //throw new System.NotImplementedException();
        }
    }
    #else
    public class PadProperyForm : Form
    {
        private object obj;
        private Pad pad;
        private PropertyGrid propertyGrid;

        public PadProperyForm(object obj, Pad pad)
        {
            InitializeComponent();
            this.obj = obj;
            this.pad = pad;
            Text = string.Format("{0}  properties", obj.GetType().Name);
            this.propertyGrid.SelectedObject = obj;
        }

        private void InitializeComponent()
        {

            ResourceManager resourceManager = new ResourceManager(typeof(PadProperyForm));
            this.propertyGrid = new PropertyGrid();
            this.SuspendLayout();
            this.propertyGrid.CommandsVisibleIfAvailable = true;
            this.propertyGrid.Dock = DockStyle.Fill;
            this.propertyGrid.LargeButtons = false;
            this.propertyGrid.LineColor = SystemColors.ScrollBar;
            this.propertyGrid.Location = new Point(0, 0);
            this.propertyGrid.Name = "PropertyGrid";
            this.propertyGrid.Size = new Size(336, 381);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.Text = "propertyGrid1";
            this.propertyGrid.ViewBackColor = SystemColors.Window;
            this.propertyGrid.ViewForeColor = SystemColors.WindowText;
            this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(336, 381);
            this.Controls.Add(this.propertyGrid);
            this.Icon = (Icon)resourceManager.GetObject("$this.Icon");
            this.Name = "PadProperyForm";
            this.Text = "Pad properties";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.pad.Update();
        }
       
    }
    #endif
}
