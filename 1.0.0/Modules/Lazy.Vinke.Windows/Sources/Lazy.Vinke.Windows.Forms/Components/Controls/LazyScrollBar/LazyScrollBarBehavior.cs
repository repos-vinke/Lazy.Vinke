// LazyScrollBarBehavior.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, August 22

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
    public class LazyScrollBarBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyScrollBarBehavior(ILazyScrollBar iScrollBar) :
            base(iScrollBar)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyScrollBar IScrollBar
        {
            get { return (ILazyScrollBar)this.IComponent; }
        }

        private ScrollBar ScrollBar
        {
            get { return (ScrollBar)this.IComponent; }
        }

        #endregion Properties
    }
}