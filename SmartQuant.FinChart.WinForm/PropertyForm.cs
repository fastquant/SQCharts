// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
#if XWT
using Compatibility.Xwt;
#elif GTK
using Compatibility.Gtk;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public class PropertyForm : Form
    {
        private Container components;

        public PropertyForm(object properties)
        {
            this.InitializeComponent();
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

