// LazyDatabaseStringFormat.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 27

using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Database
{
    public class LazyDatabaseStringFormat
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyDatabaseStringFormat()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Char { get; set; }

        public String Byte { get; set; }

        public String Int16 { get; set; }

        public String Int32 { get; set; }

        public String Int64 { get; set; }

        public String UInt16 { get; set; }

        public String UInt32 { get; set; }

        public String UInt64 { get; set; }

        public String String { get; set; }

        public String Boolean { get; set; }

        public String ByteArray { get; set; }

        public String DateTime { get; set; }

        public String Decimal { get; set; }

        public String Float { get; set; }

        public String Double { get; set; }

        #endregion Properties
    }
}