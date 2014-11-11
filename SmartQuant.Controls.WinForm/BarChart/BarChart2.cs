using System;
using System.Collections.Generic;
using System.ComponentModel;
using SmartQuant.FinChart;
using System.Drawing;
using System.Drawing.Drawing2D;

#if GTK
using Gtk;

#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Controls.BarChart
{
    public class BarChart2 : FrameworkControl, IGroupListener
    {
        private bool freezeUpdate;
        private Dictionary<int, GroupItem2> table;
        private Dictionary<string, List<int>> groupIdsBySelectorKey;
        private Dictionary<object, List<GroupEvent>> eventsBySelectorKey;
        private Chart chart;
        private ComboBox cbxSelector;

        public PermanentQueue<Event> Queue { get; private set; }

        public BarChart2()
        {
            this.InitComponent();
            this.table = new Dictionary<int, GroupItem2>();
            this.groupIdsBySelectorKey = new Dictionary<string, List<int>>();
            this.eventsBySelectorKey = new Dictionary<object, List<GroupEvent>>();
        }

        protected override void OnInit()
        {
            this.Queue = new PermanentQueue<Event>();
            this.Queue.AddReader(this);
            this.Reset(true);
            this.framework.EventManager.Dispatcher.FrameworkCleared += new FrameworkEventHandler(this.Dispatcher_FrameworkCleared);
            this.framework.GroupDispatcher.AddListener((IGroupListener)this);
            this.eventsBySelectorKey[""] = new List<GroupEvent>();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            this.Queue.RemoveReader(this);
        }

        private void Dispatcher_FrameworkCleared(object sender, FrameworkEventArgs args)
        {
            InvokeAction(delegate
            {
                #if GTK
                this.cbxSelector.Model = null;
                #else
                this.cbxSelector.Items.Clear();
                #endif
                Reset(false);
            });
            this.eventsBySelectorKey.Clear();
            this.eventsBySelectorKey[""] = new List<GroupEvent>();
        }

        public bool OnNewGroup(Group group)
        {
            if (!group.Fields.ContainsKey("Pad"))
                return false;
            GroupItem2 gi = new GroupItem2(group);
            this.table[group.Id] = gi;
            if (this.groupIdsBySelectorKey.ContainsKey(gi.SelectorKey))
            {
                if (!this.groupIdsBySelectorKey[gi.SelectorKey].Contains(group.Id))
                    this.groupIdsBySelectorKey[gi.SelectorKey].Add(group.Id);
            }
            else
                this.groupIdsBySelectorKey[gi.SelectorKey] = new List<int>()
                {
                    group.Id
                };
            this.InvokeAction((System.Action)(() =>
            {
                #if GTK
                #else
                if (this.cbxSelector.Items.Contains((object)gi.SelectorKey))
                    return;
                this.cbxSelector.Items.Add((object)gi.SelectorKey);
                this.eventsBySelectorKey[(object)gi.SelectorKey] = new List<GroupEvent>();
                this.freezeUpdate = true;
                if (this.cbxSelector.Items.Count == 1)
                    this.cbxSelector.SelectedIndex = 0;
                #endif
                this.freezeUpdate = false;
            }));
            return true;
        }

        public void OnNewGroupUpdate(GroupUpdate groupUpdate)
        {
        }

        public void OnNewGroupEvent(GroupEvent groupEvent)
        {
            GroupItem2 groupItem2 = this.table[groupEvent.Group.Id];
            object obj;
            groupItem2.Table.TryGetValue((int)groupEvent.Obj.TypeId, out obj);
            switch (groupEvent.Obj.TypeId)
            {
                case DataObjectType.Bar:
                    if (obj == null)
                    {
                        obj = (object)new BarSeries(groupItem2.Name, "", -1);
                        groupItem2.Table.Add((int)groupEvent.Obj.TypeId, obj);
                    }
                    (obj as BarSeries).Add(groupEvent.Obj as Bar);
                    break;
                case DataObjectType.Fill:
                    if (obj == null)
                    {
                        obj = (object)new FillSeries(groupItem2.Name);
                        groupItem2.Table.Add((int)groupEvent.Obj.TypeId, obj);
                    }
                    (obj as FillSeries).Add(groupEvent.Obj as Fill);
                    break;
                case DataObjectType.TimeSeriesItem:
                    if (obj == null)
                    {
                        obj = (object)new TimeSeries(groupItem2.Name, "");
                        groupItem2.Table.Add((int)groupEvent.Obj.TypeId, obj);
                    }
                    (obj as TimeSeries).Add((groupEvent.Obj as TimeSeriesItem).DateTime, (groupEvent.Obj as TimeSeriesItem).Value);
                    break;
            }
        }

        public void UpdateGUI()
        {
            if (FrameworkControl.UpdatedSuspened && this.framework.Mode != FrameworkMode.Realtime)
                return;
            Event[] eventArray = this.Queue.DequeueAll((object)this);
            if (eventArray == null)
                return;
            List<GroupEvent> list1 = new List<GroupEvent>();
            for (int index = 0; index < eventArray.Length; ++index)
            {
                Event e = eventArray[index];
                if (e.TypeId == EventType.GroupEvent)
                {
                    GroupEvent groupEvent = e as GroupEvent;
                    GroupItem2 groupItem2 = this.table[groupEvent.Group.Id];
                    string selected = GetComboBoxSelected();
                    if (selected == null && string.IsNullOrWhiteSpace(groupItem2.SelectorKey) || selected != null && selected == groupItem2.SelectorKey)
                        list1.Add(groupEvent);
                    List<GroupEvent> list2;
                    if (this.eventsBySelectorKey.TryGetValue((object)groupItem2.SelectorKey, out list2))
                        list2.Add(groupEvent);
                }
                else if (e.TypeId == EventType.OnFrameworkCleared)
                {
                    list1.Clear();
                    this.Reset(false);
                }
            }
            for (int index = 0; index < list1.Count; ++index)
                this.ProcessEvent(list1[index], index == list1.Count - 1);
            this.SetSeries();
        }

        public void Crosshair(bool isChecked)
        {
            this.chart.ActionType = isChecked ? ChartActionType.None : ChartActionType.Cross;
        }

        public void ZoomIn()
        {
            this.chart.ZoomIn();
        }

        public void ZoomOut()
        {
            this.chart.ZoomOut();
        }

        private void ProcessEvent(GroupEvent groupEvent, bool lastEvent)
        {
            this.OnNewGroupEvent(groupEvent);
        }

        private void SetSeries()
        {
            List<int> list;
            var selected = (string)GetComboBoxSelected();
            if (selected == null || !this.groupIdsBySelectorKey.TryGetValue(selected, out list))
                return;
            this.chart.Reset();
            for (int index1 = 0; index1 < list.Count; ++index1)
            {
                GroupItem2 groupItem2 = this.table[list[index1]];
                foreach (int index2 in groupItem2.Table.Keys)
                {
                    this.EnsurePadExists(groupItem2.PadNumber, groupItem2.Format);
                    int padNumber = this.chart.VolumePadVisible || groupItem2.PadNumber <= 1 ? groupItem2.PadNumber : groupItem2.PadNumber + 1;
                    if (index2 == DataObjectType.Bar)
                    {
                        if (groupItem2.IsColor)
                            this.chart.SetMainSeries((ISeries)(groupItem2.Table[index2] as BarSeries), true, groupItem2.Color);
                        else
                            this.chart.SetMainSeries((ISeries)(groupItem2.Table[index2] as BarSeries));
                    }
                    if (index2 == DataObjectType.TimeSeriesItem)
                    {
                        Color color = groupItem2.IsColor ? groupItem2.Color : Color.White;
                        if (groupItem2.IsStyle)
                            this.chart.DrawSeries(groupItem2.Table[index2] as TimeSeries, padNumber, color, groupItem2.Style);
                        else
                            this.chart.DrawSeries(groupItem2.Table[index2] as TimeSeries, padNumber, color);
                    }
                    if (groupItem2.Table[index2] is FillSeries)
                    {
                        foreach (Fill fill in groupItem2.Table[index2] as FillSeries)
                            this.chart.DrawFill(fill, padNumber);
                    }
                }
            }
        }

        private void EnsurePadExists(int newPad, string labelFormat)
        {
            while (this.chart.PadCount < newPad + 1)
                this.chart.AddPad();
            this.chart.Pads[newPad].AxisLabelFormat = labelFormat;
        }

        private void Reset(bool clearTable)
        {
            if (clearTable)
            {
                this.table.Clear();
                this.groupIdsBySelectorKey.Clear();
                this.eventsBySelectorKey.Clear();
            }
            else
            {
                foreach (GroupItem2 groupItem2 in this.table.Values)
                    groupItem2.Table.Clear();
            }
            this.chart.Reset();
        }

        private void OnSelectorValueChanged(object sender, EventArgs e)
        {
            if (this.freezeUpdate)
                return;
            this.Reset(false);
            var selected = GetComboBoxSelected();
            var list = this.eventsBySelectorKey[selected];
            if (list.Count <= 0)
                return;
            for (int index = 0; index < list.Count; ++index)
                this.ProcessEvent(list[index], index == list.Count - 1);
            this.SetSeries();
        }

        private void InitComponent()
        {
            #if GTK
            this.chart = new Chart();
            this.cbxSelector = new ComboBox();
            this.cbxSelector.Changed += OnSelectorValueChanged;
            VBox vb = new VBox();
            vb.PackStart(this.cbxSelector, false, true, 0);
            vb.PackEnd(this.chart, true, true, 0);
            Add(vb);
            ShowAll();
            #else
            this.cbxSelector = new ComboBox();
            this.chart = new Chart();
            this.SuspendLayout();
            this.cbxSelector.Dock = DockStyle.Top;
            this.cbxSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxSelector.FormattingEnabled = true;
            this.cbxSelector.Location = new Point(0, 0);
            this.cbxSelector.Name = "cbxSelector";
            this.cbxSelector.Size = new Size(725, 21);
            this.cbxSelector.TabIndex = 1;
            this.cbxSelector.SelectedIndexChanged += new EventHandler(this.OnSelectorValueChanged);
            this.chart.ActionType = ChartActionType.None;
            this.chart.AutoScroll = true;
            this.chart.BarSeriesStyle = BSStyle.Candle;
            this.chart.BorderColor = Color.Gray;
            this.chart.BottomAxisGridColor = Color.LightGray;
            this.chart.BottomAxisLabelColor = Color.LightGray;
            this.chart.CanvasColor = Color.MidnightBlue;
            this.chart.ChartBackColor = Color.MidnightBlue;
            this.chart.ContextMenuEnabled = true;
            this.chart.CrossColor = Color.DarkGray;
            this.chart.DateTipRectangleColor = Color.LightGray;
            this.chart.DateTipTextColor = Color.Black;
            this.chart.Dock = DockStyle.Fill;
            this.chart.DrawItems = false;
            this.chart.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.chart.ItemTextColor = Color.LightGray;
            this.chart.LabelDigitsCount = 2;
            this.chart.Location = new Point(0, 0);
            this.chart.MinNumberOfBars = 125;
            this.chart.Name = "chart";
            this.chart.PrimitiveDeleteImage = (Image)null;
            this.chart.PrimitivePropertiesImage = (Image)null;
            this.chart.RightAxesFontSize = 7;
            this.chart.RightAxisGridColor = Color.DimGray;
            this.chart.RightAxisMajorTicksColor = Color.LightGray;
            this.chart.RightAxisMinorTicksColor = Color.LightGray;
            this.chart.RightAxisTextColor = Color.LightGray;
            this.chart.ScaleStyle = PadScaleStyle.Arith;
            this.chart.SelectedFillHighlightColor = Color.FromArgb(100, 173, 216, 230);
            this.chart.SelectedItemTextColor = Color.Yellow;
            this.chart.SessionEnd = TimeSpan.Parse("00:00:00");
            this.chart.SessionGridColor = Color.Empty;
            this.chart.SessionGridEnabled = false;
            this.chart.SessionStart = TimeSpan.Parse("00:00:00");
            this.chart.Size = new Size(725, 400);
            this.chart.SmoothingMode = SmoothingMode.Default;
            this.chart.SplitterColor = Color.LightGray;
            this.chart.TabIndex = 0;
            this.chart.UpdateStyle = ChartUpdateStyle.Trailing;
            this.chart.ValTipRectangleColor = Color.LightGray;
            this.chart.ValTipTextColor = Color.Black;
            this.chart.VolumePadVisible = false;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.chart);
            this.Controls.Add((Control)this.cbxSelector);
            this.Name = "BarChart2";
            this.Size = new Size(725, 400);
            this.ResumeLayout(false);
            #endif
        }

        private string GetComboBoxSelected()
        {
            #if GTK
            TreeIter iter;
            string value = null;
            if (this.cbxSelector.GetActiveIter(out iter))
                value = this.cbxSelector.Model.GetValue(iter, 0).ToString();
            return value;
            #else
            return this.cbxSelector.SelectedItem.ToString();
            #endif        
        }
    }
}
