// LazyRadioButton.cs
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
    public class LazyRadioButton : RadioButton, ILazyRadioButton
    {
        #region Events

        public event ControlEventHandler LastParentChanging;

        #endregion Events

        #region Variables

        private LazyRadioButtonBehavior radioButtonBehavior;

        #endregion Variables

        #region Constructors

        public LazyRadioButton()
        {
            this.radioButtonBehavior = new LazyRadioButtonBehavior(this);
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
            get { return this.radioButtonBehavior.DockOnCenter; }
            set { this.radioButtonBehavior.DockOnCenter = value; }
        }

        #endregion Properties
    }
}