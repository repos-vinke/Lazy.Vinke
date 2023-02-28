// LazyUserControlBehavior.cs
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
    public class LazyUserControlBehavior : LazyContainerControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyUserControlBehavior(ILazyUserControl iUserControl) :
            base(iUserControl)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyUserControl IUserControl
        {
            get { return (ILazyUserControl)this.IComponent; }
        }

        private UserControl UserControl
        {
            get { return (UserControl)this.IComponent; }
        }

        #endregion Properties
    }
}