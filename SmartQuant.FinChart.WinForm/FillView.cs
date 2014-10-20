using System;
using System.ComponentModel;
using System.Text;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#else
using Compatibility.WinForm;
using System.Drawing;
#endif

namespace SmartQuant.FinChart
{
    public class FillView : IChartDrawable, IDateDrawable
    {
        private Fill fill;

        protected bool toolTipEnabled = true;
        protected string toolTipFormat = "";
        protected Pad pad;
        protected DateTime firstDate;
        protected DateTime lastDate;
        protected bool selected;

        internal bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color BuyColor { get; set; }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color SellColor { get; set; }

        [Category("Drawing Style")]
        [Browsable(false)]
        public Color SellShortColor { get; set; }

        [Browsable(false)]
        [Category("Drawing Style")]
        public bool TextEnabled { get; set; }

        [Category("ToolTip")]
        [Description("Enable or disable tooltip appearance for this marker.")]
        public bool ToolTipEnabled
        {
            get
            {
                return this.toolTipEnabled;
            }
            set
            {
                this.toolTipEnabled = value;
            }
        }

        [Description("Tooltip format string. {1} - X coordinate, {2} - Y coordinte.")]
        [Category("ToolTip")]
        public string ToolTipFormat
        {
            get
            {
                return this.toolTipFormat;
            }
            set
            {
                this.toolTipFormat = value;
            }
        }

        public DateTime DateTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public FillView(Fill fill, Pad pad)
        {
            this.fill = fill;
            this.pad = pad;
            this.toolTipEnabled = true;
            this.toolTipFormat = "{0} {2} {1} @ {3} {4} {5}";
        }

        public void Paint()
        {
            throw new NotImplementedException();
        }

        public bool Compare(object obj)
        {
            return this.fill == obj;
        }

        public void SetInterval(DateTime minDate, DateTime maxDate)
        {
            this.firstDate = minDate;
            this.lastDate = maxDate;
        }

        public Distance Distance(int x, double y)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
        }

        public void UnSelect()
        {
        }
    }
}

