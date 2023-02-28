// LazyHScrollBarBehavior.cs
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
    public class LazyHScrollBarBehavior : LazyScrollBarBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyHScrollBarBehavior(ILazyHScrollBar iHScrollBar) :
            base(iHScrollBar)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyHScrollBar IHScrollBar
        {
            get { return (ILazyHScrollBar)this.IComponent; }
        }

        private HScrollBar HScrollBar
        {
            get { return (HScrollBar)this.IComponent; }
        }

        #endregion Properties
    }
}