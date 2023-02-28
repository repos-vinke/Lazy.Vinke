// LazyToolStripTextBox.cs
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
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class LazyToolStripTextBox : ToolStripTextBox, ILazyToolStripTextBox
    {
        #region Variables

        private LazyToolStripTextBoxBehavior toolStripTextBoxBehavior;

        #endregion Variables

        #region Constructors

        public LazyToolStripTextBox()
        {
            this.toolStripTextBoxBehavior = new LazyToolStripTextBoxBehavior(this);
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}