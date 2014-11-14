using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;

#if GTK
using Gtk;
using Compatibility.Gtk;






#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Charting
{
    [Serializable]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                if (this.fPrintDocument == null)
                {
                    this.fPrintDocument = new PrintDocument();
                    this.fPrintDocument.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
                    this.fPrintDocument.DefaultPageSettings.Landscape = this.fPrintLayout == EPrintLayout.Landscape;
                }
                return this.fPrintDocument;
            }
            set
            {
                if (this.fPrintDocument != null)
                    this.fPrintDocument.PrintPage -= new PrintPageEventHandler(this.OnPrintPage);
                this.fPrintDocument = value;
                this.fPrintDocument.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
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
                this.fPrintLayout = value;
                if (this.fPrintDocument != null)
                    this.fPrintDocument.DefaultPageSettings.Landscape = this.fPrintLayout == EPrintLayout.Landscape;
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
                foreach (Pad pad in this.fPads)
                    pad.ForeColor = this.fPadsForeColor;
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
                this.fSessionStart = TimeSpan.FromDays(0);
                this.fSessionEnd = TimeSpan.FromDays(1);
                foreach (Pad pad in this.fPads)
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
                foreach (Pad pad in this.fPads)
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
                foreach (Pad pad in this.fPads)
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
                foreach (Pad pad in this.fPads)
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
                foreach (Pad pad in this.fPads)
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
            Init();
            Name = name;
        }

        public Chart(DateTime date)
        {
        }

        protected virtual void Init()
        {
            InitializeComponent();
            #if GTK
            #else
            this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick | ControlStyles.UserPaint, true);
            #endif
            this.fPadsForeColor = Color.White;
            this.fPads = new PadList();
            AddPad(0, 0, 1, 1);
            this.fPadSplit = false;
            this.fPadSplitIndex = 0;
            this.fDoubleBufferingEnabled = true;
            this.fSmoothingEnabled = false;
            this.fAntiAliasingEnabled = false;
            this.fToolTip = new ToolTip();
            this.fIsUpdating = false;
            this.fPrintX = 10;
            this.fPrintY = 10;
            this.fPrintWidth = 600;
            this.fPrintHeight = 400;
            this.fPrintAlign = EPrintAlign.None;
            this.fPrintLayout = EPrintLayout.Portrait;
            this.fSessionGridColor = Color.Blue;
        }

        public Pad cd(int padIndex)
        {
            padIndex = Math.Max(1, padIndex);
            padIndex = Math.Min(this.fPads.Count, padIndex);
            return Chart.fPad = this.fPads[padIndex - 1];
        }

        public void Clear()
        {
            this.fPads.Clear();
        }

        public void SetRangeX(double Min, double Max)
        {
            foreach (Pad pad in this.fPads)
                pad.SetRangeX(Min, Max);
        }

        public void SetRangeX(DateTime Min, DateTime Max)
        {
            foreach (Pad pad in this.fPads)
                pad.SetRangeX(Min, Max);
        }

        public void SetRangeY(double Min, double Max)
        {
            foreach (Pad pad in this.fPads)
                pad.SetRangeY(Min, Max);
        }

        public virtual Pad AddPad(double x1, double y1, double x2, double y2)
        {
            var pad = new Pad(this, x1, y1, x2, y2);
            pad.Name = string.Format("Pad {0}", this.fPads.Count + 1);
            pad.ForeColor = this.fPadsForeColor;
            pad.Zoom += new ZoomEventHandler(this.ZoomChanged);
            this.fPads.Add(pad);
            return Chart.fPad = pad;
        }

        public void Connect()
        {
            foreach (Pad pad in this.fPads)
                pad.Zoom += new ZoomEventHandler(this.ZoomChanged);
        }

        public void Disconnect()
        {
            foreach (Pad pad in this.fPads)
                pad.Zoom -= new ZoomEventHandler(this.ZoomChanged);
        }

        protected void ZoomChanged(object sender, ZoomEventArgs e)
        {
            if (!this.GroupZoomEnabled)
                return;
            foreach (Pad pad in this.fPads)
            {
                if (e.ZoomUnzoom)
                {
                    pad.AxisBottom.SetRange(e.XMin, e.XMax);
                    pad.AxisTop.SetRange(e.XMin, e.XMax);
                    pad.AxisBottom.Zoomed = true;
                    pad.AxisTop.Zoomed = true;
                }
                else
                    pad.UnZoom();
            }
            this.UpdatePads();
        }

        private void AdaptLeftMargin()
        {
            int w = 0;
            foreach (Pad pad in this.fPads)
                w = Math.Max(w, pad.AxisLeft.LastValidAxisWidth);
            foreach (Pad pad in this.fPads)
                pad.MarginLeft = w - pad.AxisLeft.LastValidAxisWidth;
        }

        private void AdaptRightMargin()
        {
            int w = 0;
            foreach (Pad pad in this.fPads)
                w = Math.Max(w, pad.AxisRight.LastValidAxisWidth);
            foreach (Pad pad in this.fPads)
                pad.MarginRight = w - pad.AxisRight.LastValidAxisWidth;
        }

        public void Divide(int x, int y)
        {
            var widths = Enumerable.Range(1, x).Select(i => i / (double)x).ToArray();
            var heights = Enumerable.Range(1, y).Select(i => i / (double)y).ToArray();
            Divide(x, y, widths, heights);
        }

        public void Divide(int x, int y, double[] widths, double[] heights)
        {
            this.fPads.Clear();
            var xs = Enumerable.Concat(new double[] { 0 }, widths).CumulativeSum().ToArray();
            var ys = Enumerable.Concat(new double[] { 0 }, heights).CumulativeSum().ToArray();
            for (int i = 1; i < ys.Length; ++i)
                for (int j = 1; j < xs.Length; ++j)
                    AddPad(xs[j - 1], ys[i - 1], xs[j], ys[i]);
        }

        public void UpdatePads(Graphics padGraphics, int x, int y, int width, int height)
        {
            padGraphics.Clear(BackColor);
            if (SmoothingEnabled)
                padGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (AntiAliasingEnabled)
                padGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            foreach (Pad pad in this.fPads)
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
            return new Bitmap(GetMetafile(EmfType.EmfPlusOnly));
        }

        public Bitmap GetBitmap(float dpi)
        {
            Graphics graphics = this.CreateGraphics();
            int num1 = (int)((double)this.ClientRectangle.Width * (double)dpi / (double)graphics.DpiX);
            int num2 = (int)((double)this.ClientRectangle.Height * (double)dpi / (double)graphics.DpiY);
            Bitmap bitmap = new Bitmap(num1, num2);
            bitmap.SetResolution(dpi, dpi);
            var g1 = Graphics.FromImage(bitmap);
            g1.Clear(BackColor);
            if (this.SmoothingEnabled)
                g1.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (this.GroupLeftMarginEnabled)
                this.AdaptLeftMargin();
            if (this.GroupRightMarginEnabled)
                this.AdaptRightMargin();
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(num1, num2);
                pad.Update(g1);
            }
            g1.Dispose();
            return bitmap;
        }

        public Metafile GetMetafile(EmfType type)
        {
            int w = this.ClientRectangle.Width;
            int h = this.ClientRectangle.Height;

            Metafile metafile;
            using (var g = this.CreateGraphics())
            {
                IntPtr hdc = g.GetHdc();
                metafile = new Metafile(hdc, type);
                g.ReleaseHdc(hdc);
                g.Dispose();
            }

            using (var g = Graphics.FromImage(metafile))
            {
                g.Clear(BackColor);
                if (SmoothingEnabled)
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                if (AntiAliasingEnabled)
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                if (GroupLeftMarginEnabled)
                    AdaptLeftMargin();
                if (GroupRightMarginEnabled)
                    AdaptRightMargin();
                foreach (Pad pad in this.fPads)
                {
                    pad.SetCanvas(w, h);
                    pad.Update(g);
                }
            }
            return metafile;
        }

        public void SaveImage(string filename, ImageFormat format)
        {
            using (var metafile = GetMetafile(EmfType.EmfPlusOnly))
                metafile.Save(filename, format);
        }

        public void UpdatePads()
        {
            Invalidate();
            #if !GTK
            Application.DoEvents();
            #endif
        }

        //TODO:rewrite it!
        public void UpdatePads(Graphics g)
        {
            if (Disposing || this.fIsUpdating)
                return;
            this.fIsUpdating = true;
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            Bitmap bitmap = null;
            Graphics g1;
            try
            {
                if (this.fDoubleBufferingEnabled)
                {
                    bitmap = new Bitmap(width, height);
                    g1 = Graphics.FromImage(bitmap);
                }
                else
                    g1 = g;
            }
            catch
            {
                this.fIsUpdating = false;
                return;
            }
            g1.Clear(BackColor);
            if (SmoothingEnabled)
                g1.SmoothingMode = SmoothingMode.AntiAlias;
            if (AntiAliasingEnabled)
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (GroupLeftMarginEnabled)
                AdaptLeftMargin();
            if (GroupRightMarginEnabled)
                AdaptRightMargin();
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(width, height);
                pad.Update(g1);
            }
            if (this.fDoubleBufferingEnabled)
            {
                Graphics g2;
                try
                {
                    g2 = g;
                }
                catch
                {
                    this.fIsUpdating = false;
                    return;
                }
                if (g2 != null)
                {
                    g2.DrawImage(bitmap, 0, 0);
                    if (this.fFileName != null)
                        bitmap.Save(FileName, ImageFormat.Gif);
                    bitmap.Dispose();
                    g2.Dispose();
                }
            }
            g1.Dispose();
            this.fIsUpdating = false;
        }

        public virtual void Print()
        {
            PrintDocument.Print();
        }

        public virtual void PrintPreview()
        {
            #if !GTK
            new PrintPreviewDialog() { Document = PrintDocument }.Show();
            #endif
        }

        public virtual void PrintSetup()
        {
            #if !GTK
            new PrintDialog() { Document = PrintDocument }.ShowDialog();
            #endif
        }

        public virtual void PrintPageSetup()
        {
            #if !GTK
            new PageSetupDialog() { Document = PrintDocument }.ShowDialog();
            #endif
        }

        private void OnPrintPage(object sender, PrintPageEventArgs args)
        {
            int x = this.fPrintX;
            int y = this.fPrintY;
            switch (this.fPrintAlign)
            {
                case EPrintAlign.Veritcal:
                    y = (args.PageBounds.Height - this.fPrintHeight) / 2;
                    break;
                case EPrintAlign.Horizontal:
                    x = (args.PageBounds.Width - this.fPrintWidth) / 2;
                    break;
                case EPrintAlign.Center:
                    x = (args.PageBounds.Width - this.fPrintWidth) / 2;
                    y = (args.PageBounds.Height - this.fPrintHeight) / 2;
                    break;
            }
            UpdatePads(args.Graphics, x, y, this.fPrintWidth, this.fPrintHeight);
        }

        protected void InitializeComponent()
        {
            #if GTK
            #else
            Size = new Size(272, 168);
            #endif
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            UpdatePads(pe.Graphics);
            base.OnPaint(pe);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // no-op
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.fPadSplit)
            {
                Pad pad1 = this.fPads[this.fPadSplitIndex];
                int width = this.ClientRectangle.Width;
                int height = this.ClientRectangle.Height;
                double num = (double)e.Y / (double)height;
                pad1.SetCanvas(pad1.CanvasX1, num, pad1.CanvasX2, pad1.CanvasY2, width, height);
                if (this.fPadSplitIndex != 0)
                {
                    Pad pad2 = this.fPads[this.fPadSplitIndex - 1];
                    pad2.SetCanvas(pad2.CanvasX1, pad2.CanvasY1, pad2.CanvasX2, num, width, height);
                }
                UpdatePads();
            }
            foreach (Pad pad in this.fPads)
            {
                if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                {
                    #if GTK
                    //Gdk.CursorType.
                    #else
                    if (Cursor.Current == Cursors.HSplit)
                        return;
                    Cursor.Current = Cursors.HSplit;
                    #endif
                    return;
                }
            }
            foreach (Pad pad in this.fPads)
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseMove(e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            foreach (Pad pad in this.fPads)
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseWheel(e);
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            foreach (Pad pad in this.fPads)
            {
                if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                {
                    this.fPadSplit = true;
                    this.fPadSplitIndex = this.fPads.IndexOf(pad);
                    return;
                }
            }
            foreach (Pad pad in this.fPads)
                if (pad.X1 <= e.X && e.X <= pad.X2 && pad.Y1 <= e.Y && e.Y <= pad.Y2)
                    pad.MouseDown(e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.fPadSplit)
            {
                this.fPadSplit = false;
                if (PadSplitMouseUp == null)
                    return;
                this.PadSplitMouseUp(this, EventArgs.Empty);
            }
            else
            {
                foreach (Pad pad in this.fPads)
                    pad.MouseUp(e);
                base.OnMouseUp(e);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            #if GTK
            var evt = (e as ButtonPressEventArgs).Event;
            var p = new Point((int)evt.X, (int)evt.Y);
            #else
            var p = this.PointToClient(Cursor.Position);
            #endif
            foreach (Pad pad in this.fPads)
                if (pad.X1 <= p.X && pad.X2 >= p.X && pad.Y1 <= p.Y && p.Y <= pad.Y2)
                    pad.DoubleClick(p.X, p.Y); 
            base.OnDoubleClick(e);
        }

        protected override void Dispose(bool disposing)
        {
            foreach (Pad pad in this.fPads)
                pad.Monitored = false;
            base.Dispose(disposing);
        }
    }
}