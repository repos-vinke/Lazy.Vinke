// LazySplitterBehavior.cs
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
    public class LazySplitterBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazySplitterBehavior(ILazySplitter iSplitter) :
            base(iSplitter)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazySplitter ISplitter
        {
            get { return (ILazySplitter)this.IComponent; }
        }

        private Splitter Splitter
        {
            get { return (Splitter)this.IComponent; }
        }

        #endregion Properties
    }
}