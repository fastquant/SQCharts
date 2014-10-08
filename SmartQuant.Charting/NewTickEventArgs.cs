// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
    public class NewTickEventArgs : EventArgs
    {
        public DateTime DateTime { get; set; }

        public NewTickEventArgs(DateTime datetime)
        {
            DateTime = datetime;
        }
    }
}
