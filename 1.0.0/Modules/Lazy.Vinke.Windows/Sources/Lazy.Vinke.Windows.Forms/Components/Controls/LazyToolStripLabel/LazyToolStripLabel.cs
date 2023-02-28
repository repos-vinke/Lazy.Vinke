// LazyToolStripLabel.cs
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
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class LazyToolStripLabel : ToolStripLabel, ILazyToolStripLabel
    {
        #region Variables

        private LazyToolStripLabelBehavior toolStripLabelBehavior;

        #endregion Variables

        #region Constructors

        public LazyToolStripLabel()
        {
            this.toolStripLabelBehavior = new LazyToolStripLabelBehavior(this);
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}