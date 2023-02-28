// LazyStatusStripBehavior.cs
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
    public class LazyStatusStripBehavior : LazyToolStripBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyStatusStripBehavior(ILazyStatusStrip iStatusStrip) :
            base(iStatusStrip)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyStatusStrip IStatusStrip
        {
            get { return (ILazyStatusStrip)this.IComponent; }
        }

        private StatusStrip StatusStrip
        {
            get { return (StatusStrip)this.IComponent; }
        }

        #endregion Properties
    }
}