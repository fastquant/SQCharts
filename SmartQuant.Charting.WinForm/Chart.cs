using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
#if GTK
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
                if (this.fPrintDocument == null)
                    return;
                if (this.fPrintLayout == EPrintLayout.Landscape)
                    this.fPrintDocument.DefaultPageSettings.Landscape = true;
                else
                    this.fPrintDocument.DefaultPageSettings.Landscape = false;
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
                this.fSessionStart = new TimeSpan(0, 0, 0, 0);
                this.fSessionEnd = new TimeSpan(0, 24, 0, 0);
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
        {
            this.Init();
            this.Name = "";
        }

        public Chart(string Name)
        {
            this.Init();
            this.Name = Name;
        }

        public Chart(DateTime date)
        {
        }

        protected virtual void Init()
        {
            this.InitializeComponent();
            #if GTK
            #else
            this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick|ControlStyles.UserPaint, true);
            #endif
            this.fPadsForeColor = Color.White;
            this.fPads = new PadList();
            this.AddPad(0.0, 0.0, 1.0, 1.0);
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
            if (padIndex < 1)
                padIndex = 1;
            if (padIndex > this.fPads.Count)
                padIndex = this.fPads.Count;
            Chart.fPad = this.fPads[padIndex - 1];
            return Chart.fPad;
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

        public virtual Pad AddPad(double X1, double Y1, double X2, double Y2)
        {
            Chart.fPad = new Pad(this, X1, Y1, X2, Y2);
            Chart.fPad.Name = "Pad " + (this.fPads.Count + 1).ToString();
            Chart.fPad.ForeColor = this.fPadsForeColor;
            this.fPads.Add(Chart.fPad);
            Chart.fPad.Zoom += new ZoomEventHandler(this.ZoomChanged);
            return Chart.fPad;
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
            int val1 = 0;
            foreach (Pad pad in this.fPads)
                val1 = Math.Max(val1, pad.AxisLeft.LastValidAxisWidth);
            foreach (Pad pad in this.fPads)
                pad.MarginLeft = val1 - pad.AxisLeft.LastValidAxisWidth;
        }

        private void AdaptRightMargin()
        {
            int val1 = 0;
            foreach (Pad pad in this.fPads)
                val1 = Math.Max(val1, pad.AxisRight.LastValidAxisWidth);
            foreach (Pad pad in this.fPads)
                pad.MarginRight = val1 - pad.AxisRight.LastValidAxisWidth;
        }

        public void Divide(int X, int Y)
        {
            this.fPads.Clear();
            double num1 = 1.0 / (double) X;
            double num2 = 1.0 / (double) Y;
            for (int index1 = 0; index1 < Y; ++index1)
            {
                for (int index2 = 0; index2 < X; ++index2)
                {
                    double X1 = (double) index2 * num1;
                    double X2 = (double) (index2 + 1) * num1;
                    double Y1 = (double) index1 * num2;
                    double Y2 = (double) (index1 + 1) * num2;
                    this.AddPad(X1, Y1, X2, Y2);
                }
            }
        }

        public void Divide(int X, int Y, double[] Widths, double[] Heights)
        {
            this.fPads.Clear();
            double Y1 = 0.0;
            double Y2 = 0.0;
            for (int index1 = 0; index1 < Y; ++index1)
            {
                if (index1 > 0)
                    Y1 += Heights[index1 - 1];
                Y2 += Heights[index1];
                double X1 = 0.0;
                double X2 = 0.0;
                for (int index2 = 0; index2 < X; ++index2)
                {
                    if (index2 > 0)
                        X1 = Widths[index2 - 1];
                    X2 += Widths[index2];
                    this.AddPad(X1, Y1, X2, Y2);
                }
            }
        }

        public void UpdatePads(Graphics PadGraphics, int X, int Y, int Width, int Height)
        {
            PadGraphics.Clear(this.BackColor);
            if (this.SmoothingEnabled)
                PadGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                PadGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(Width, Height);
                pad.X1 += X;
                pad.X2 += X;
                pad.Y1 += Y;
                pad.Y2 += Y;
                pad.Update(PadGraphics);
                pad.X1 -= X;
                pad.X2 -= X;
                pad.Y1 -= Y;
                pad.Y2 -= Y;
            }
        }

        public Bitmap GetBitmap()
        {
            return new Bitmap((Image) this.GetMetafile(EmfType.EmfPlusOnly));
        }

        public Bitmap GetBitmap(float Dpi)
        {
            Graphics graphics = this.CreateGraphics();
            int num1 = (int) ((double) this.ClientRectangle.Width * (double) Dpi / (double) graphics.DpiX);
            int num2 = (int) ((double) this.ClientRectangle.Height * (double) Dpi / (double) graphics.DpiY);
            Bitmap bitmap = new Bitmap(num1, num2);
            bitmap.SetResolution(Dpi, Dpi);
            Graphics Graphics = Graphics.FromImage((Image) bitmap);
            Graphics.Clear(this.BackColor);
            if (this.SmoothingEnabled)
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (this.GroupLeftMarginEnabled)
                this.AdaptLeftMargin();
            if (this.GroupRightMarginEnabled)
                this.AdaptRightMargin();
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(num1, num2);
                pad.Update(Graphics);
            }
            Graphics.Dispose();
            return bitmap;
        }

        public Metafile GetMetafile(EmfType type)
        {
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            Graphics graphics = this.CreateGraphics();
            IntPtr hdc = graphics.GetHdc();
            Metafile metafile = new Metafile(hdc, type);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
            Graphics Graphics = Graphics.FromImage((Image) metafile);
            Graphics.Clear(this.BackColor);
            if (this.SmoothingEnabled)
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (this.GroupLeftMarginEnabled)
                this.AdaptLeftMargin();
            if (this.GroupRightMarginEnabled)
                this.AdaptRightMargin();
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(width, height);
                pad.Update(Graphics);
            }
            Graphics.Dispose();
            return metafile;
        }

        public void SaveImage(string filename, ImageFormat format)
        {
            Metafile metafile = this.GetMetafile(EmfType.EmfPlusOnly);
            metafile.Save(filename, format);
            metafile.Dispose();
        }

        public void UpdatePads()
        {
            this.Invalidate();
            #if !GTK
            Application.DoEvents();
            #endif
        }

        public void UpdatePads(Graphics g)
        {
            if (this.Disposing || this.fIsUpdating)
                return;
            this.fIsUpdating = true;
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            Bitmap bitmap = (Bitmap) null;
            Graphics Graphics;
            try
            {
                if (this.fDoubleBufferingEnabled)
                {
                    bitmap = new Bitmap(width, height);
                    Graphics = Graphics.FromImage((Image) bitmap);
                }
                else
                    Graphics = g;
            }
            catch
            {
                this.fIsUpdating = false;
                return;
            }
            Graphics.Clear(this.BackColor);
            if (this.SmoothingEnabled)
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.AntiAliasingEnabled)
                Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (this.GroupLeftMarginEnabled)
                this.AdaptLeftMargin();
            if (this.GroupRightMarginEnabled)
                this.AdaptRightMargin();
            foreach (Pad pad in this.fPads)
            {
                pad.SetCanvas(width, height);
                pad.Update(Graphics);
            }
            if (this.fDoubleBufferingEnabled)
            {
                Graphics graphics;
                try
                {
                    graphics = g;
                }
                catch
                {
                    this.fIsUpdating = false;
                    return;
                }
                if (graphics != null)
                {
                    graphics.DrawImage((Image) bitmap, 0, 0);
                    if (this.fFileName != null)
                        bitmap.Save(this.FileName, ImageFormat.Gif);
                    bitmap.Dispose();
                    graphics.Dispose();
                }
            }
            Graphics.Dispose();
            this.fIsUpdating = false;
        }

        public virtual void Print()
        {
            this.PrintDocument.Print();
        }

        public virtual void PrintPreview()
        {
            #if !GTK
            ((Control) new PrintPreviewDialog()
            {
                Document = this.PrintDocument
            }).Show();
            #endif
        }

        public virtual void PrintSetup()
        {
            #if !GTK
            int num = (int) new PrintDialog()
            {
                Document = this.PrintDocument
            }.ShowDialog();
            #endif
        }

        public virtual void PrintPageSetup()
        {
            #if !GTK
            int num = (int) new PageSetupDialog()
            {
                Document = this.PrintDocument
            }.ShowDialog();
            #endif
        }

        private void OnPrintPage(object sender, PrintPageEventArgs Args)
        {
            int X = this.fPrintX;
            int Y = this.fPrintY;
            switch (this.fPrintAlign)
            {
                case EPrintAlign.Veritcal:
                    Y = (Args.PageBounds.Height - this.fPrintHeight) / 2;
                    break;
                case EPrintAlign.Horizontal:
                    X = (Args.PageBounds.Width - this.fPrintWidth) / 2;
                    break;
                case EPrintAlign.Center:
                    X = (Args.PageBounds.Width - this.fPrintWidth) / 2;
                    Y = (Args.PageBounds.Height - this.fPrintHeight) / 2;
                    break;
            }
            this.UpdatePads(Args.Graphics, X, Y, this.fPrintWidth, this.fPrintHeight);
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
            this.UpdatePads(pe.Graphics);
            base.OnPaint(pe);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.fPadSplit)
            {
                Pad pad1 = this.fPads[this.fPadSplitIndex];
                int width = this.ClientRectangle.Width;
                int height = this.ClientRectangle.Height;
                double num = (double) e.Y / (double) height;
                pad1.SetCanvas(pad1.CanvasX1, num, pad1.CanvasX2, pad1.CanvasY2, width, height);
                if (this.fPadSplitIndex != 0)
                {
                    Pad pad2 = this.fPads[this.fPadSplitIndex - 1];
                    pad2.SetCanvas(pad2.CanvasX1, pad2.CanvasY1, pad2.CanvasX2, num, width, height);
                }
                this.UpdatePads();
            }
            foreach (Pad pad in this.fPads)
            {
                if (pad.Y1 - 1 <= e.Y && e.Y <= pad.Y1 + 1)
                {
                    #if GTK
                    #else
                    if (!(Cursor.Current != Cursors.HSplit))
                        return;
                    Cursor.Current = Cursors.HSplit;
                    #endif
                    return;
                }
            }
            foreach (Pad pad in this.fPads)
            {
                if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                    pad.MouseMove(e);
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            foreach (Pad pad in this.fPads)
            {
                if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                    pad.MouseWheel(e);
            }
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
            {
                if (pad.X1 <= e.X && pad.X2 >= e.X && (pad.Y1 <= e.Y && pad.Y2 >= e.Y))
                    pad.MouseDown(e);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.fPadSplit)
            {
                this.fPadSplit = false;
                if (this.PadSplitMouseUp == null)
                    return;
                this.PadSplitMouseUp((object) this, EventArgs.Empty);
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
            Point point = new Point(0, 0);
            #else
            Point point = this.PointToClient(Cursor.Position);
            #endif
            foreach (Pad pad in this.fPads)
            {
                if (pad.X1 <= point.X && pad.X2 >= point.X && (pad.Y1 <= point.Y && pad.Y2 >= point.Y))
                    pad.DoubleClick(point.X, point.Y);
            }
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
