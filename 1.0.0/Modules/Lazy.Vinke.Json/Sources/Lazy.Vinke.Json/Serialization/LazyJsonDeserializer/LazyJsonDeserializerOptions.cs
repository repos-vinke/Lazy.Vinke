// LazyJsonDeserializerOptions.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 23

using System;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptions
    {
        #region Variables

        private Dictionary<Type, Object> deserializerOptionsDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptions()
        {
        }

        #endregion Constructors

        #region Methods

        public T Item<T>() where T : LazyJsonDeserializerOptionsBase
        {
            if (this.deserializerOptionsDictionary == null)
                this.deserializerOptionsDictionary = new Dictionary<Type, Object>();

            if (this.deserializerOptionsDictionary.ContainsKey(typeof(T)) == false)
                this.deserializerOptionsDictionary.Add(typeof(T), Activator.CreateInstance(typeof(T)));

            return (T)this.deserializerOptionsDictionary[typeof(T)];
        }

        public Boolean Contains<T>() where T : LazyJsonDeserializerOptionsBase
        {
            return this.deserializerOptionsDictionary.ContainsKey(typeof(T));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonDeserializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsBase()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}