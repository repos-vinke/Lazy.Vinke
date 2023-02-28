// LazyTabControlBehavior.cs
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
    public class LazyTabControlBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyTabControlBehavior(ILazyTabControl iTabControl) :
            base(iTabControl)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyTabControl ITabControl
        {
            get { return (ILazyTabControl)this.IComponent; }
        }

        private TabControl TabControl
        {
            get { return (TabControl)this.IComponent; }
        }

        #endregion Properties
    }
}