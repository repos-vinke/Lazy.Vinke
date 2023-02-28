﻿// LazyToolStripButtonBehavior.cs
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
    public class LazyToolStripButtonBehavior : LazyToolStripItemBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyToolStripButtonBehavior(ILazyToolStripButton iToolStripButton) :
            base(iToolStripButton)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyToolStripButton IToolStripButton
        {
            get { return (ILazyToolStripButton)this.IComponent; }
        }

        private ToolStripButton ToolStripButton
        {
            get { return (ToolStripButton)this.IComponent; }
        }

        #endregion Properties
    }
}