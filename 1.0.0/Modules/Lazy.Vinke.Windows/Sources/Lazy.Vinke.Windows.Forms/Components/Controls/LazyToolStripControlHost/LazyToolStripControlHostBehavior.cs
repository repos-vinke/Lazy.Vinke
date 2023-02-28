// LazyToolStripControlHostBehavior.cs
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
    public class LazyToolStripControlHostBehavior : LazyToolStripItemBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyToolStripControlHostBehavior(ILazyToolStripControlHost iToolStripControlHost) :
            base(iToolStripControlHost)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyToolStripControlHost IToolStripControlHost
        {
            get { return (ILazyToolStripControlHost)this.IComponent; }
        }

        private ToolStripControlHost ToolStripControlHost
        {
            get { return (ToolStripControlHost)this.IComponent; }
        }

        #endregion Properties
    }
}