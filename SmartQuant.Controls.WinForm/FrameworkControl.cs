// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using SmartQuant;
using System.ComponentModel;

#if XWT
using Compatibility.Xwt;
#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Controls
{
    public class FrameworkControl : UserControl
    {
        protected Framework framework;
        protected ControlSettings settings;
        protected object[] args;

        public static bool UpdatedSuspened { get; set; }

        public virtual object PropertyObject
        {
            get
            {
                return null;
            }
        }

        public event EventHandler<ShowPropertiesEventArgs> ShowProperties;

        protected FrameworkControl()
        {
        }

        public void Init(Framework framework, ControlSettings settings, object[] args)
        {
            this.framework = framework;
            this.settings = settings;
            this.args = args;
            this.OnInit();
        }

        public void Close(CancelEventArgs args)
        {
            this.OnClosing(args);
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnClosing(CancelEventArgs args)
        {
        }

        public void SuspendUpdates()
        {
            FrameworkControl.UpdatedSuspened = true;
            this.OnSuspendUpdates();
        }

        public void ResumeUpdates()
        {
            FrameworkControl.UpdatedSuspened = false;
            this.OnResumeUpdates();
        }

        protected virtual void OnSuspendUpdates()
        {
        }

        protected virtual void OnResumeUpdates()
        {
        }

        protected void OnShowProperties(bool focus)
        {

            if (ShowProperties != null)
                ShowProperties(this, new ShowPropertiesEventArgs(focus));
        }

        protected void InvokeAction(Action action)
        {
            if (this.InvokeRequired)
                this.Invoke(action);
            else
                action();
        }

        protected string GetMessageBoxCaption()
        {
            #if XWT
            return Parent != null ? Parent.Name : Name;
            #else
            return Parent != null ? Parent.Text : Text;
            #endif
        }
    }
}