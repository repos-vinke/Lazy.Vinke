// LazyUpDownBaseBehavior.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, August 21

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
    public class LazyUpDownBaseBehavior : LazyContainerControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyUpDownBaseBehavior(ILazyUpDownBase iUpDownBase) :
            base(iUpDownBase)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyUpDownBase IUpDownBase
        {
            get { return (ILazyUpDownBase)this.IComponent; }
        }

        private UpDownBase UpDownBase
        {
            get { return (UpDownBase)this.IComponent; }
        }

        #endregion Properties
    }
}