// LazyJsonSerializerOptions.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerOptions
    {
        #region Variables

        private Dictionary<Type, Object> serializerOptionsDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptions()
        {
        }

        #endregion Constructors

        #region Methods

        public T Item<T>() where T : LazyJsonSerializerOptionsBase
        {
            if (this.serializerOptionsDictionary == null)
                this.serializerOptionsDictionary = new Dictionary<Type, Object>();

            if (this.serializerOptionsDictionary.ContainsKey(typeof(T)) == false)
                this.serializerOptionsDictionary.Add(typeof(T), Activator.CreateInstance(typeof(T)));

            return (T)this.serializerOptionsDictionary[typeof(T)];
        }

        public Boolean Contains<T>() where T : LazyJsonSerializerOptionsBase
        {
            return this.serializerOptionsDictionary.ContainsKey(typeof(T));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonSerializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsBase()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}