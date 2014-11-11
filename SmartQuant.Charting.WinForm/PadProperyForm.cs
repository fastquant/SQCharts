// Decompiled with JetBrains decompiler
// Type: SmartQuant.Charting.PadProperyForm
// Assembly: SmartQuant.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=23953e483e363d68
// MVID: F3B55EE9-4DBA-4875-B18A-7BD8DFCF4D88
// Assembly location: C:\Program Files\SmartQuant Ltd\OpenQuant 2014\SmartQuant.Charting.dll

using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace SmartQuant.Charting
{
    public class PadProperyForm : Form
    {
        private object fObject;
        private Pad fPad;
        private PropertyGrid PropertyGrid;
        private Container components;

        public PadProperyForm(object Object, Pad Pad)
        {
            this.InitializeComponent();
            this.fObject = Object;
            this.fPad = Pad;
            this.Text = Object.GetType().Name + " properties";
            this.PropertyGrid.SelectedObject = Object;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ResourceManager resourceManager = new ResourceManager(typeof (PadProperyForm));
            this.PropertyGrid = new PropertyGrid();
            this.SuspendLayout();
            this.PropertyGrid.CommandsVisibleIfAvailable = true;
            this.PropertyGrid.Dock = DockStyle.Fill;
            this.PropertyGrid.LargeButtons = false;
            this.PropertyGrid.LineColor = SystemColors.ScrollBar;
            this.PropertyGrid.Location = new Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new Size(336, 381);
            this.PropertyGrid.TabIndex = 0;
            this.PropertyGrid.Text = "propertyGrid1";
            this.PropertyGrid.ViewBackColor = SystemColors.Window;
            this.PropertyGrid.ViewForeColor = SystemColors.WindowText;
            this.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(336, 381);
            this.Controls.Add((Control) this.PropertyGrid);
            this.Icon = (Icon) resourceManager.GetObject("$this.Icon");
            this.Name = "PadProperyForm";
            this.Text = "Pad properties";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.fPad.Update();
        }
    }
}
