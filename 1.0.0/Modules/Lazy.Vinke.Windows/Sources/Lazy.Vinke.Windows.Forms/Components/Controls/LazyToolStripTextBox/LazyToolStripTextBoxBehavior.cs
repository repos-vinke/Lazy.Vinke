// LazyToolStripTextBoxBehavior.cs
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
    public class LazyToolStripTextBoxBehavior : LazyToolStripControlHostBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyToolStripTextBoxBehavior(ILazyToolStripTextBox iToolStripTextBox) :
            base(iToolStripTextBox)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyToolStripTextBox IToolStripTextBox
        {
            get { return (ILazyToolStripTextBox)this.IComponent; }
        }

        private ToolStripTextBox ToolStripTextBox
        {
            get { return (ToolStripTextBox)this.IComponent; }
        }

        #endregion Properties
    }
}