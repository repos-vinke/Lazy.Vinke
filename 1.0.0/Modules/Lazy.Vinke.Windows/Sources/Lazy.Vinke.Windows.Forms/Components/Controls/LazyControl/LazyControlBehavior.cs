// LazyControlBehavior.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, August 20

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Lazy.Vinke.Windows.Forms
{
    public class LazyControlBehavior : LazyComponentBehavior
    {
        #region Variables

        private Control controlLastParent;

        private Boolean dockOnCenter;

        #endregion Variables

        #region Constructors

        public LazyControlBehavior(ILazyControl iControl) :
            base(iControl)
        {
            this.Control.Font = new Font("Cascadia Mono", 12, FontStyle.Regular, GraphicsUnit.Point);
            this.Control.ParentChanged += OnControlParentChanged;
            this.Control.LocationChanged += OnControlLocationChanged;
            this.Control.SizeChanged += OnControlSizeChanged;
        }

        #endregion Constructors

        #region Methods

        private void HandleControlParent()
        {
            if (this.controlLastParent != null)
            {
                if (this.controlLastParent != this.Control.Parent)
                    ((Control)this.controlLastParent).SizeChanged -= OnControlParentSizeChanged;
            }

            if (this.Control.Parent != null)
            {
                this.Control.Parent.SizeChanged += OnControlParentSizeChanged;

                HandleDockOnCenter();
            }

            PropertyInfo propertyInfo = this.Control.GetType().GetProperty("LastParentChangingEventHandler", BindingFlags.Public | BindingFlags.Instance);
            ControlEventHandler lastParentChangingEventHandler = (ControlEventHandler)propertyInfo?.GetValue(this.Control);
            lastParentChangingEventHandler?.Invoke(this.Control, new ControlEventArgs(this.controlLastParent));

            this.controlLastParent = this.Control.Parent;
        }

        private void HandleDockOnCenter()
        {
            if (this.dockOnCenter == true)
            {
                if (this.Control != null && this.Control.Parent != null)
                {
                    this.Control.LocationChanged -= OnControlLocationChanged;
                    this.Control.Location = new Point((this.Control.Parent.ClientSize.Width / 2) - (this.Control.Width / 2), (this.Control.Parent.ClientSize.Height / 2) - (this.Control.Height / 2));
                    this.Control.LocationChanged += OnControlLocationChanged;
                }
            }
        }

        private void OnControlParentChanged(Object sender, EventArgs args)
        {
            HandleControlParent();
        }

        private void OnControlParentSizeChanged(Object sender, EventArgs args)
        {
            HandleDockOnCenter();
        }

        private void OnControlLocationChanged(Object sender, EventArgs args)
        {
            HandleDockOnCenter();
        }

        private void OnControlSizeChanged(Object sender, EventArgs args)
        {
            HandleDockOnCenter();
        }

        #endregion Methods

        #region Properties

        public Boolean DockOnCenter
        {
            get { return this.dockOnCenter; }
            set
            {
                this.dockOnCenter = value;

                HandleDockOnCenter();
            }
        }

        private ILazyControl IControl
        {
            get { return (ILazyControl)this.IComponent; }
        }

        private Control Control
        {
            get { return (Control)this.IComponent; }
        }

        #endregion Properties
    }
}