// LazyTrackBarBehavior.cs
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
    public class LazyTrackBarBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyTrackBarBehavior(ILazyTrackBar iTrackBar) :
            base(iTrackBar)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyTrackBar ITrackBar
        {
            get { return (ILazyTrackBar)this.IComponent; }
        }

        private TrackBar TrackBar
        {
            get { return (TrackBar)this.IComponent; }
        }

        #endregion Properties
    }
}