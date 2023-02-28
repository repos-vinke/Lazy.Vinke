// LazyDateTimePickerBehavior.cs
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
    public class LazyDateTimePickerBehavior : LazyControlBehavior
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyDateTimePickerBehavior(ILazyDateTimePicker iDateTimePicker) :
            base(iDateTimePicker)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        private ILazyDateTimePicker IDateTimePicker
        {
            get { return (ILazyDateTimePicker)this.IComponent; }
        }

        private DateTimePicker DateTimePicker
        {
            get { return (DateTimePicker)this.IComponent; }
        }

        #endregion Properties
    }
}