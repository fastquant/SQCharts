// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;
using SmartQuant;
using System.ComponentModel;

#if GTK
using Compatibility.Gtk;

#else
using System.Windows.Forms;
#endif

namespace SmartQuant.Controls
{
    #if GTK
    public class FrameworkControl : NoPaintUserControl
    #else
    public class FrameworkControl : UserControl
    #endif
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
            : base()
        {
        }

        public void Init(Framework framework, ControlSettings settings, object[] args)
        {
            this.framework = framework;
            this.settings = settings;
            this.args = args;
            OnInit();
        }

        public void Close(CancelEventArgs args)
        {
            OnClosing(args);
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnClosing(CancelEventArgs args)
        {
        }

        public void SuspendUpdates()
        {
            UpdatedSuspened = true;
            OnSuspendUpdates();
        }

        public void ResumeUpdates()
        {
            UpdatedSuspened = false;
            OnResumeUpdates();
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
            #if GTK
            Gtk.Application.Invoke((sender, e) =>
            {
                action();
            });
            #else
            if (InvokeRequired)
                Invoke(action);
            else
                action();
            #endif
        }

        protected string GetMessageBoxCaption()
        {
            #if GTK
            return Parent != null ? Parent.Name : Name;
            #else
            return Parent != null ? Parent.Text : Text;
            #endif
        }
    }
}
