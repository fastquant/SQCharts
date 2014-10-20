using System;
using System.Collections.Generic;
using SmartQuant.Charting;

namespace SmartQuant.Controls
{
    public class GroupItem
    {
        public Dictionary<int, Tuple<Viewer, object>> Table { get; private set; }

        public int PadNumber { get; set; }

        public string Format { get; set; }

        public GroupItem(Group group)
        {
            Table = new Dictionary<int, Tuple<Viewer, object>>();
            PadNumber = (int) group.Fields["Pad"].Value;
            Format = group.Fields.ContainsKey("Format") ? (string)group.Fields["Format"].Value : "F2";
        }
    }
}

