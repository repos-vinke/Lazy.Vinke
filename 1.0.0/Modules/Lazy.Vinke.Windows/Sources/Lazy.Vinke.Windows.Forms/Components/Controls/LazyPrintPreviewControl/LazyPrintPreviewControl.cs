// LazyPrintPreviewControl.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, August 22

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
    public class LazyPrintPreviewControl : PrintPreviewControl, ILazyPrintPreviewControl
    {
        #region Events

        public event ControlEventHandler LastParentChanging;

        #endregion Events

        #region Variables

        private LazyPrintPreviewControlBehavior printPreviewControlBehavior;

        #endregion Variables

        #region Constructors

        public LazyPrintPreviewControl()
        {
            this.printPreviewControlBehavior = new LazyPrintPreviewControlBehavior(this);
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ControlEventHandler LastParentChangingEventHandler
        {
            get { return this.LastParentChanging; }
        }

        public Boolean DockOnCenter
        {
            get { return this.printPreviewControlBehavior.DockOnCenter; }
            set { this.printPreviewControlBehavior.DockOnCenter = value; }
        }

        #endregion Properties
    }
}