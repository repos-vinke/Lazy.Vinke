// LazyDataGridViewBehavior.cs
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
    public class LazyDataGridViewBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyDataGridViewBehavior(ILazyDataGridView iDataGridView) :
            base(iDataGridView)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyDataGridView IDataGridView
        {
            get { return (ILazyDataGridView)this.IComponent; }
        }

        private DataGridView DataGridView
        {
            get { return (DataGridView)this.IComponent; }
        }

        #endregion Properties
    }
}