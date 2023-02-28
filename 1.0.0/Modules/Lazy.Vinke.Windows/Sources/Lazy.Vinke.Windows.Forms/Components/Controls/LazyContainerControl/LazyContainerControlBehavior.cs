// LazyContainerControlBehavior.cs
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
    public class LazyContainerControlBehavior : LazyScrollableControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyContainerControlBehavior(ILazyContainerControl iContainerControl) :
            base(iContainerControl)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyContainerControl IContainerControl
        {
            get { return (ILazyContainerControl)this.IComponent; }
        }

        private ContainerControl ContainerControl
        {
            get { return (ContainerControl)this.IComponent; }
        }

        #endregion Properties
    }
}