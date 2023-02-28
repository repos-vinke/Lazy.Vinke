// LazyListControlBehavior.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, August 21

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
    public class LazyListControlBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyListControlBehavior(ILazyListControl iListControl) :
            base(iListControl)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyListControl IListControl
        {
            get { return (ILazyListControl)this.IComponent; }
        }

        private ListControl ListControl
        {
            get { return (ListControl)this.IComponent; }
        }

        #endregion Properties
    }
}