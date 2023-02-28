// LazyListBoxBehavior.cs
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
    public class LazyListBoxBehavior : LazyListControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyListBoxBehavior(ILazyListBox iListBox) :
            base(iListBox)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyListBox IListBox
        {
            get { return (ILazyListBox)this.IComponent; }
        }

        private ListBox ListBox
        {
            get { return (ListBox)this.IComponent; }
        }

        #endregion Properties
    }
}