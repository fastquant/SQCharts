// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using System.Collections.Generic;

namespace SmartQuant.Controls
{
    public class ControlSettings : Dictionary<string, string>
    {
        protected internal void SetValue(string key, string value)
        {
            this[key] = value;
        }

        protected internal void SetEnumValue<T>(string key, T value) where T : struct
        {
            SetValue(key, value.ToString());
        }

        protected internal void SetValue(string key, bool value)
        {
            SetValue(key, value.ToString());
        }

        protected internal void SetValue(string key, byte value)
        {
            SetValue(key, value.ToString());
        }

        protected internal string GetStringValue(string key, string defaultValue)
        {
            string str;
            return TryGetValue(key, out str) ? str : defaultValue;
        }

        protected internal T GetEnumValue<T>(string key, T defaultValue) where T : struct
        {
            T result;
            return Enum.TryParse<T>(GetStringValue(key, defaultValue.ToString()), out result) ? result : defaultValue;
        }

        protected internal bool GetBooleanValue(string key, bool defaultValue)
        {
            bool result;
            return bool.TryParse(GetStringValue(key, defaultValue.ToString()), out result) ? result : defaultValue;
        }

        protected internal byte GetByteValue(string key, byte defaultValue)
        {
            byte result;
            return byte.TryParse(GetStringValue(key, defaultValue.ToString()), out result) ? result : defaultValue;
        }
    }
}
