// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using EmfType = System.Drawing.Imaging.EmfType;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;

#else
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    public partial class Chart : UserControl
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

        public Chart()
            : this("")
        {
        }

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
            padIndex = padIndex > Pads.Count ? Pads.Count : padIndex;
            Chart.Pad = Pads[padIndex - 1];
            return Chart.Pad;
        }

        public void Clear()
        {
            Pads.Clear();
        }

        public void SetRangeX(double min, double max)
        {
            foreach (Pad pad in Pads)
                pad.SetRangeX(min, max);
        }

        public void SetRangeX(DateTime min, DateTime max)
        {
            foreach (Pad pad in Pads)
                pad.SetRangeX(min, max);
        }

        public void SetRangeY(double min, double max)
        {
            foreach (Pad pad in Pads)
                pad.SetRangeY(min, max);
        }

        public virtual Pad AddPad(double x1, double y1, double x2, double y2)
        {
            var pad = new Pad(this, x1, y1, x2, y2);
            pad.Name = string.Format("Pad {0}", Pads.Count + 1);
            pad.ForeColor = this.PadsForeColor;
            pad.Zoom += new ZoomEventHandler(this.ZoomChanged);
            Pads.Add(pad);
            return Chart.Pad = pad;
        }

        public void Connect()
        {
            foreach (Pad pad in Pads)
                pad.Zoom += new ZoomEventHandler(this.ZoomChanged);
        }

        public void Disconnect()
        {
            foreach (Pad pad in Pads)
                pad.Zoom -= new ZoomEventHandler(this.ZoomChanged);
        }

        protected void ZoomChanged(object sender, ZoomEventArgs e)
        {
            throw new NotImplementedException();
        }            

        public void Divide(int x, int y)
        {
            Pads.Clear();
            double num1 = 1.0 / x;
            double num2 = 1.0 / y;
            Parallel.For(0, y, i => Parallel.For(0, x, j =>
                    {
                        double x1 = (double)j * num1;
                        double x2 = (double)(j + 1) * num1;
                        double y1 = (double)i * num2;
                        double y2 = (double)(i + 1) * num2;
                        this.AddPad(x1, y1, x2, y2);
                    }));  
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            throw new NotImplementedException();
        }

        public void UpdatePads(Graphics padGraphics, int x, int y, int width, int height)
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
            this.Invalidate();
//            Application.DoEvents();
        }

        public void UpdatePads(Graphics g)
        {
            throw new NotImplementedException();
        }

        public virtual void Print()
        {
            PrintDocument.Print();
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        protected override void Dispose(bool disposing)
        {
            foreach (Pad pad in Pads)
                pad.Monitored = false;
            base.Dispose(disposing);   
        }
    }
}

