﻿// LazyToolStripProgressBar.cs
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
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections.Generic;

namespace Lazy.Vinke.Windows.Forms
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class LazyToolStripProgressBar : ToolStripProgressBar, ILazyToolStripProgressBar
    {
        #region Variables

        private LazyToolStripProgressBarBehavior toolStripProgressBarBehavior;

        #endregion Variables

        #region Constructors

        public LazyToolStripProgressBar()
        {
            this.toolStripProgressBarBehavior = new LazyToolStripProgressBarBehavior(this);
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}