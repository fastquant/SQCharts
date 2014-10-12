// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace SmartQuant.Charting
{
    public class PadProperyForm : Form
    {
        private object obj;
        private Pad pad;
        private Container components = null;

        public PadProperyForm(object obj, Pad pad)
        {
            this.InitializeComponent();
            this.obj = obj;
            this.pad = pad;
            this.Text = obj.GetType().Name + " properties";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
        }
    }
}

