// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Controls
{
    public class ShowPropertiesEventArgs : EventArgs
    {
        public bool Focus { get; private set; }

        internal ShowPropertiesEventArgs(bool focus)
        {
            Focus = focus;
        }
    }
}
