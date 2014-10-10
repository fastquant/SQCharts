// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
#if XWT
using Xwt.Drawing;
#else
using System.Drawing;
#endif

namespace SmartQuant.Charting
{
    public static class Fonts
    {
        public static Font SystemFont()
        {
            #if XWT
            return Font.SystemFont;
            #else
            return new Font("Arial", 8);
            #endif
        }
    }
}

