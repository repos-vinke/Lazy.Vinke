// LazyLinkLabelBehavior.cs
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
    public class LazyLinkLabelBehavior : LazyLabelBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyLinkLabelBehavior(ILazyLinkLabel iLinkLabel) :
            base(iLinkLabel)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyLinkLabel ILinkLabel
        {
            get { return (ILazyLinkLabel)this.IComponent; }
        }

        private LinkLabel LinkLabel
        {
            get { return (LinkLabel)this.IComponent; }
        }

        #endregion Properties
    }
}