// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.FinChart
{
    public interface IAxesMarked
    {
//        Color Color { get; }
        double LastValue { get; }
        bool IsMarkEnable { get; }
        int LabelDigitsCount { get; }
    }
}
