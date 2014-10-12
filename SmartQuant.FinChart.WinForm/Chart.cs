// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
using System.Windows.Forms;
#endif

namespace SmartQuant.FinChart
{
    public class Chart : UserControl
	{
        public int LabelDigitsCount { get; set; }

	}
}

