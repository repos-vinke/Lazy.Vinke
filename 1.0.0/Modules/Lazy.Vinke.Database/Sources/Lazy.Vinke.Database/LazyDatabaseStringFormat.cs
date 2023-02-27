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
            this.Char = "'{0}'";
            this.Byte = "{0}";
            this.Int16 = "{0}";
            this.Int32 = "{0}";
            this.Int64 = "{0}";
            this.UInt16 = "{0}";
            this.UInt32 = "{0}";
            this.UInt64 = "{0}";
            this.String = "'{0}'";
            this.Boolean = "'{0}'";
            this.ByteArray = "'{0}'";
            this.DateTime = "'{0:yyyy-MM-dd HH:mm:ss}'";
            this.Decimal = "{0:f4}";
            this.Float = "{0:f4}";
            this.Double = "{0:f4}";
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