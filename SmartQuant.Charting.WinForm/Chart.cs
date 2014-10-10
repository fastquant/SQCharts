// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

#if XWT
using Xwt.Drawing;
namespace SmartQuant.Charting
{
public class UserControl
{
}

    public class ToolTip
    {
    }
    public class PrintDocument
    {
    }
}
#else
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
	}

}

