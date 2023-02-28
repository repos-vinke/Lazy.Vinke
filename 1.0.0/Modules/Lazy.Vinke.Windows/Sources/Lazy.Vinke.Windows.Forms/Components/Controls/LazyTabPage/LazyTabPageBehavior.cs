// LazyTabPageBehavior.cs
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
    public class LazyTabPageBehavior : LazyPanelBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyTabPageBehavior(ILazyTabPage iTabPage) :
            base(iTabPage)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyTabPage ITabPage
        {
            get { return (ILazyTabPage)this.IComponent; }
        }

        private TabPage TabPage
        {
            get { return (TabPage)this.IComponent; }
        }

        #endregion Properties
    }
}