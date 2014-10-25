// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Printing;
#if GTK
using Gdk;
using Color = System.Drawing.Color;
using Compatibility.Gtk;
#else
using Compatibility.WinForm;
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
                this.fPadsForeColor = value;
                foreach (Pad pad in Pads)
                    pad.ForeColor = value;
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
                this.fTransformationType = value;
                this.fSessionStart = new TimeSpan(0, 0, 0, 0);
                this.fSessionEnd = new TimeSpan(1, 0, 0, 0);
                foreach (Pad pad in Pads)
                    pad.TransformationType = value;
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
                this.fSessionGridEnabled = value;
                foreach (Pad pad in Pads)
                    pad.SessionGridEnabled = value;
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
                this.fSessionGridColor = value;
                foreach (Pad pad in Pads)
                    pad.SessionGridColor = value;
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
                this.fSessionStart = value;
                foreach (Pad pad in Pads)
                    pad.SessionStart = value;
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
                this.fSessionEnd = value;
                foreach (Pad pad in Pads)
                    pad.SessionEnd = value;
            }
        }

        public event EventHandler PadSplitMouseUp;

        public Chart()
            : this("")
        {
        }

        public Chart(string name)
        {
            InitializeComponent();
            #if XWT
            #elif GTK
            #else
            this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            this.UpdateStyles();
            #endif
            Name = name;
            Pads = new PadList();
            AddPad(0, 0, 1, 1);
            this.fToolTip = new ToolTip();
            this.fPadsForeColor = Color.White;
            this.fSessionGridColor = Color.Blue;
        }

        public Chart(DateTime date)
        {
            throw new NotImplementedException("Why this?");
        }

        public Pad cd(int padIndex)
        {
            padIndex = padIndex < 1 ? 1 : padIndex;
            padIndex = padIndex > Pads.Count ? Pads.Count : padIndex;
            Chart.Pad = Pads[padIndex - 1];
            return Chart.Pad;
        }

        #if XWT
        public new void Clear()
        {
            Pads.Clear();
            base.Clear();
        }
        #else
        public void Clear()
        {
            Pads.Clear();
        }
        #endif


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
            var widths = Enumerable.Range(0, x).Select<int, double>(d => 1.0 / x).ToArray();
            var heights = Enumerable.Range(0, y).Select<int, double>(d => 1.0 / y).ToArray();
            Divide(x, y, widths, heights);
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            Pads.Clear();
            double y1 = 0;
            double y2 = 0;
            foreach (var h in heights)
            {
                y2 += h;
                double x1 = 0;
                double x2 = 0;
                foreach (var w in widths)
                {
                    x2 += w;
                    AddPad(x1, y1, x2, y2);
                    x1 = x2;
                }
                y1 = y2;
            }
        }

        public void UpdatePads(Graphics padGraphics, int x, int y, int width, int height)
        {
            padGraphics.Clear(this.BackColor);
            if (this.SmoothingEnabled)
                padGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                padGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            foreach (Pad pad in Pads)
            {
                pad.SetCanvas(width, height);
                pad.X1 += x;
                pad.X2 += x;
                pad.Y1 += y;
                pad.Y2 += y;
                pad.Update(padGraphics);
                pad.X1 -= x;
                pad.X2 -= x;
                pad.Y1 -= y;
                pad.Y2 -= y;
            }   
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
            #if XWT
            #elif GTK
            #else
            Application.DoEvents();
            #endif
        }
            
        public void UpdatePads(Graphics graphics)
        {
            if (this.Disposing || this.fIsUpdating)
                return;
            this.fIsUpdating = true;

            int width = (int)this.ClientRectangle.Width;
            int height = (int)this.ClientRectangle.Height;

            graphics.Clear(BackColor);
            if (SmoothingEnabled)
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (AntiAliasingEnabled)
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
//            if (GroupLeftMarginEnabled)
//                this.AdaptLeftMargin();
//            if (this.GroupRightMarginEnabled)
//                this.AdaptRightMargin();
            foreach (Pad pad in Pads)
            {
                pad.SetCanvas(width, height);
                pad.Update(graphics);
            }

            this.fIsUpdating = false;
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
            //UpdatePads(pe.Graphics);
            base.OnPaint(pe);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Red);
            base.OnPaintBackground(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // When the mouse is over the boundaies of pads.
            foreach (Pad pad in Pads)
            {
                if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                {
                    #if XWT
                    this.Cursor = CursorType.ResizeUpDown;
                    #elif GTK
                    #else
                    Cursor.Current = Cursors.HSplit;
                    #endif
                    return;
                }
            }

            foreach (Pad pad in Pads)
            {
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseMove(e);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            foreach (Pad pad in Pads)
            {
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseWheel(e);
            }
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Console.WriteLine("OnMouseDown");
//            foreach (Pad pad in Pads)
//            {
//                if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
//                {
//                    this.fPadSplit = true;
//                    this.fPadSplitIndex = this.fPads.IndexOf(pad);
//                    return;
//                }
//            }

            foreach (Pad pad in Pads)
            {
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseDown(e);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Console.WriteLine("OnMouseUp");
            base.OnMouseUp(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            #if XWT
            var be = e as ButtonEventArgs;
            var screen = be.Position;
            var point = be.Position;
            #elif GTK
            #else
            var screen = Cursor.Position;
            var point = this.PointToClient(Cursor.Position);
            #endif
//            Console.WriteLine("Cursor.Position: {0}", screen);
//            Console.WriteLine("Client Position: {0}", point);
//
//            foreach (Pad pad in Pads)
//            {
//                if (pad.X1 <= point.X && point.X <= pad.X2 && pad.Y1 <= point.Y && point.Y <= pad.Y2)
//                    pad.DoubleClick((int)point.X, (int)point.Y);
//            }
//            base.OnDoubleClick(e);
        }

        protected override void Dispose(bool disposing)
        {
            foreach (Pad pad in Pads)
                pad.Monitored = false;
            base.Dispose(disposing);   
        }
    }
}

