// LazyMenuStripBehavior.cs
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
    public class LazyMenuStripBehavior : LazyToolStripBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyMenuStripBehavior(ILazyMenuStrip iMenuStrip) :
            base(iMenuStrip)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyMenuStrip IMenuStrip
        {
            get { return (ILazyMenuStrip)this.IComponent; }
        }

        private MenuStrip MenuStrip
        {
            get { return (MenuStrip)this.IComponent; }
        }

        #endregion Properties
    }
}