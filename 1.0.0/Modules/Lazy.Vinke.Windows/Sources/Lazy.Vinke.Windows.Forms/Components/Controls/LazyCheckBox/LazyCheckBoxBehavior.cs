// LazyCheckBoxBehavior.cs
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
    public class LazyCheckBoxBehavior : LazyButtonBaseBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyCheckBoxBehavior(ILazyCheckBox iCheckBox) :
            base(iCheckBox)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyCheckBox ICheckBox
        {
            get { return (ILazyCheckBox)this.IComponent; }
        }

        private CheckBox CheckBox
        {
            get { return (CheckBox)this.IComponent; }
        }

        #endregion Properties
    }
}