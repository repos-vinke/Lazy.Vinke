// LazyLabelBehavior.cs
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
    public class LazyLabelBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyLabelBehavior(ILazyLabel iLabel) :
            base(iLabel)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyLabel ILabel
        {
            get { return (ILazyLabel)this.IComponent; }
        }

        private Label Label
        {
            get { return (Label)this.IComponent; }
        }

        #endregion Properties
    }
}