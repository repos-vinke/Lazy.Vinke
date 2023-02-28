// LazyRadioButtonBehavior.cs
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
    public class LazyRadioButtonBehavior : LazyButtonBaseBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyRadioButtonBehavior(ILazyRadioButton iRadioButton) :
            base(iRadioButton)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyRadioButton IRadioButton
        {
            get { return (ILazyRadioButton)this.IComponent; }
        }

        private RadioButton RadioButton
        {
            get { return (RadioButton)this.IComponent; }
        }

        #endregion Properties
    }
}