// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
using EmfType = System.Drawing.Imaging.EmfType;
using PrintPageEventArgs = System.Drawing.Printing.PrintPageEventArgs;
using PaintEventArgs = System.Windows.Forms.PaintEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
#if XWT
using Xwt.Drawing;

namespace SmartQuant.Charting
{
    public class UserControl : Xwt.Widget
    {
        protected virtual void OnPaint(PaintEventArgs pe)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected virtual void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDoubleClick(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }

    public class ToolTip
    {
    }

    public class PrintDocument
    {
    }
}
#else
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    public class Chart : UserControl
    {
        protected static Pad fPad;
        protected PadList fPads;
        protected bool fPadSplit;
        protected int fPadSplitIndex;
        protected bool fDoubleBufferingEnabled;
        protected bool fSmoothingEnabled;
        protected bool fAntiAliasingEnabled;
        protected bool fIsUpdating;
        protected bool fGroupZoomEnabled;
        protected bool fGroupLeftMarginEnabled;
        protected bool fGroupRightMarginEnabled;
        protected string fFileName;
        protected ToolTip fToolTip;
        protected PrintDocument fPrintDocument;
        protected int fPrintX;
        protected int fPrintY;
        protected int fPrintWidth;
        protected int fPrintHeight;
        protected EPrintAlign fPrintAlign;
        protected EPrintLayout fPrintLayout;
        protected ETransformationType fTransformationType;
        protected Color fSessionGridColor;
        protected TimeSpan fSessionStart;
        protected TimeSpan fSessionEnd;
        protected bool fSessionGridEnabled;
        protected Color fPadsForeColor;

        public PadList Pads
        {
            get
            {
                return this.fPads;
            }
            set
            {
                this.fPads = value;
            }
        }

        public bool GroupLeftMarginEnabled
        {
            get
            {
                return this.fGroupLeftMarginEnabled;
            }
            set
            {
                this.fGroupLeftMarginEnabled = value;
            }
        }

        public bool GroupRightMarginEnabled
        {
            get
            {
                return this.fGroupRightMarginEnabled;
            }
            set
            {
                this.fGroupRightMarginEnabled = value;
            }
        }

        public bool GroupZoomEnabled
        {
            get
            {
                return this.fGroupZoomEnabled;
            }
            set
            {
                this.fGroupZoomEnabled = value;
            }
        }

        public bool DoubleBufferingEnabled
        {
            get
            {
                return this.fDoubleBufferingEnabled;
            }
            set
            {
                this.fDoubleBufferingEnabled = value;
            }
        }

        public bool SmoothingEnabled
        {
            get
            {
                return this.fSmoothingEnabled;
            }
            set
            {
                this.fSmoothingEnabled = value;
            }
        }

        public bool AntiAliasingEnabled
        {
            get
            {
                return this.fAntiAliasingEnabled;
            }
            set
            {
                this.fAntiAliasingEnabled = value;
            }
        }

        public static Pad Pad
        {
            get
            {
                return Chart.fPad;
            }
            set
            {
                Chart.fPad = value;
            }
        }

        public ToolTip ToolTip
        {
            get
            {
                return this.fToolTip;
            }
        }

        public PrintDocument PrintDocument
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int PrintX
        {
            get
            {
                return this.fPrintX;
            }
            set
            {
                this.fPrintX = value;
            }
        }

        public int PrintY
        {
            get
            {
                return this.fPrintY;
            }
            set
            {
                this.fPrintY = value;
            }
        }

        public int PrintWidth
        {
            get
            {
                return this.fPrintWidth;
            }
            set
            {
                this.fPrintWidth = value;
            }
        }

        public int PrintHeight
        {
            get
            {
                return this.fPrintHeight;
            }
            set
            {
                this.fPrintHeight = value;
            }
        }

        public EPrintAlign PrintAlign
        {
            get
            {
                return this.fPrintAlign;
            }
            set
            {
                this.fPrintAlign = value;
            }
        }

        public EPrintLayout PrintLayout
        {
            get
            {
                return this.fPrintLayout;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string FileName
        {
            get
            {
                return this.fFileName;
            }
            set
            {
                this.fFileName = value;
            }
        }

        public Color PadsForeColor
        {
            get
            {
                return this.fPadsForeColor;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Description("")]
        [Category("Transformation")]
        [RefreshProperties(RefreshProperties.All)]
        public ETransformationType TransformationType
        {
            get
            {
                return this.fTransformationType;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Category("Transformation")]
        [Description("")]
        public bool SessionGridEnabled
        {
            get
            {
                return this.fSessionGridEnabled;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Description("")]
        [Category("Transformation")]
        public Color SessionGridColor
        {
            get
            {
                return this.fSessionGridColor;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Description("")]
        [Category("Transformation")]
        public TimeSpan SessionStart
        {
            get
            {
                return this.fSessionStart;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Description("")]
        [Category("Transformation")]
        public TimeSpan SessionEnd
        {
            get
            {
                return this.fSessionEnd;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler PadSplitMouseUp;

        public Chart(string name)
        {
            this.InitializeComponent();
            Name = name;
        }

        public Chart(DateTime date)
        {
        }

        protected void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public Pad cd(int padIndex)
        {
            padIndex = padIndex < 1 ? 1 : padIndex;
            padIndex = padIndex > fPads.Count ? fPads.Count : padIndex;
            Chart.fPad = fPads[padIndex - 1];
            return Chart.fPad;
        }

        public void Clear()
        {
            this.fPads.Clear();
        }

        public void SetRangeX(double Min, double Max)
        {
            throw new NotImplementedException();
        }

        public void SetRangeX(DateTime Min, DateTime Max)
        {
            throw new NotImplementedException();
        }

        public void SetRangeY(double Min, double Max)
        {
            throw new NotImplementedException();
        }

        public virtual Pad AddPad(double X1, double Y1, double X2, double Y2)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        protected void ZoomChanged(object sender, ZoomEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AdaptLeftMargin()
        {
            throw new NotImplementedException();
        }

        private void AdaptRightMargin()
        {
            throw new NotImplementedException();
        }

        public void Divide(int X, int Y)
        {
            throw new NotImplementedException();
        }

        public void Divide(int X, int Y, double[] Widths, double[] Heights)
        {
            throw new NotImplementedException();
        }

        public void UpdatePads(Graphics PadGraphics, int X, int Y, int Width, int Height)
        {
            throw new NotImplementedException();
        }

        public Bitmap GetBitmap()
        {
            return new Bitmap(this.GetMetafile(EmfType.EmfPlusOnly));
        }

        public Bitmap GetBitmap(float Dpi)
        {
            throw new NotImplementedException();
        }

        public Metafile GetMetafile(EmfType type)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(string filename, ImageFormat format)
        {
            throw new NotImplementedException();
        }

        public void UpdatePads()
        {
            throw new NotImplementedException();
        }

        public void UpdatePads(Graphics g)
        {
            throw new NotImplementedException();
        }

        public virtual void Print()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintPreview()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintSetup()
        {
            throw new NotImplementedException();
        }

        public virtual void PrintPageSetup()
        {
            throw new NotImplementedException();
        }

        private void OnPrintPage(object sender, PrintPageEventArgs Args)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}

