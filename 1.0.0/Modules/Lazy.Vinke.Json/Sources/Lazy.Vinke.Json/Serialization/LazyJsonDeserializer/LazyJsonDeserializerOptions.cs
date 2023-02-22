// LazyJsonDeserializerOptions.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 04

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptions
    {
        #region Variables

        internal LazyJsonDeserializerDataTableOptions dataTableOptions;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonDeserializerDataTableOptions DataTable
        {
            get
            {
                if (this.dataTableOptions == null)
                    this.dataTableOptions = new LazyJsonDeserializerDataTableOptions();

                return this.dataTableOptions; 
            }
            set
            {
                this.dataTableOptions = value;
            }
        }

        #endregion Properties
    }

    public class LazyJsonDeserializerDataTableOptions
    {
        #region Variables

        internal Dictionary<String, LazyJsonDeserializerColumnOptions> dataTableDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerColumnOptions this[String name]
        {
            get
            {
                if (this.dataTableDictionary == null)
                    this.dataTableDictionary = new Dictionary<String, LazyJsonDeserializerColumnOptions>();

                if (this.dataTableDictionary.ContainsKey(name) == false)
                    this.dataTableDictionary.Add(name, new LazyJsonDeserializerColumnOptions());

                return this.dataTableDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerColumnOptions
    {
        #region Variables

        internal LazyJsonDeserializerColumnCollectionOptions columnCollectionOptions;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonDeserializerColumnCollectionOptions Columns
        {
            get
            {
                if (this.columnCollectionOptions == null)
                    this.columnCollectionOptions = new LazyJsonDeserializerColumnCollectionOptions();

                return this.columnCollectionOptions;
            }
            set
            {
                this.columnCollectionOptions = value;
            }
        }

        #endregion Properties
    }

    public class LazyJsonDeserializerColumnCollectionOptions
    {
        #region Variables

        internal Dictionary<String, LazyJsonDeserializerColumnDataOptions> columnDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerColumnDataOptions this[String name]
        {
            get
            {
                if (this.columnDictionary == null)
                    this.columnDictionary = new Dictionary<String, LazyJsonDeserializerColumnDataOptions>();

                if (this.columnDictionary.ContainsKey(name) == false)
                    this.columnDictionary.Add(name, new LazyJsonDeserializerColumnDataOptions());

                return this.columnDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerColumnDataOptions
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        public void Set(Type type)
        {
            this.Type = type;
        }

        #endregion Methods

        #region Properties

        public Type Type { get; set; }

        #endregion Properties
    }
}