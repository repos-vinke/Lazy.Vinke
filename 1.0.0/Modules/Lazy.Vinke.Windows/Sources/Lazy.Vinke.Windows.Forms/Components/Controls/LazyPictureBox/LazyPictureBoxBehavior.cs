// LazyPictureBoxBehavior.cs
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
    public class LazyPictureBoxBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyPictureBoxBehavior(ILazyPictureBox iPictureBox) :
            base(iPictureBox)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyPictureBox IPictureBox
        {
            get { return (ILazyPictureBox)this.IComponent; }
        }

        private PictureBox PictureBox
        {
            get { return (PictureBox)this.IComponent; }
        }

        #endregion Properties
    }
}