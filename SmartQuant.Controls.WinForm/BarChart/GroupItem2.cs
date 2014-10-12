using System;
using System.Collections.Generic;
using SmartQuant.FinChart;

#if XWT
using Compatibility.Xwt;
using Xwt.Drawing;
#else
using System.Drawing;
using System.Windows.Forms;
#endif

namespace SmartQuant.Controls
{
    public class GroupItem2
    {
        public string Name { get; private set; }

        public int PadNumber { get; private set; }

        public string Format { get; private set; }

        public string SelectorKey { get; private set; }

        public bool IsColor { get; private set; }

        public Color Color { get; private set; }

        public bool IsStyle { get; private set; }

        public SimpleDSStyle Style { get; private set; }

        public Dictionary<int, object> Table { get; private set; }

        public GroupItem2(Group group)
        {
            this.Table = new Dictionary<int, object>();
        }
    }
}

