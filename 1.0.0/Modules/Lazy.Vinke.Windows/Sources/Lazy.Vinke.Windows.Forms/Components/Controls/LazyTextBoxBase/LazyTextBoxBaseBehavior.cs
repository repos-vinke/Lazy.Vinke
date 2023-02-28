// LazyTextBoxBaseBehavior.cs
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
    public class LazyTextBoxBaseBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyTextBoxBaseBehavior(ILazyTextBoxBase iTextBoxBase) :
            base(iTextBoxBase)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyTextBoxBase ITextBoxBase
        {
            get { return (ILazyTextBoxBase)this.IComponent; }
        }

        private TextBoxBase TextBoxBase
        {
            get { return (TextBoxBase)this.IComponent; }
        }

        #endregion Properties
    }
}