// LazyJsonSerializerInteger.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 23

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerInteger : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json integer token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json integer token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonInteger(null);
            
            Type dataType = data.GetType();

            if (dataType != typeof(Int32) && dataType != typeof(Int16) && dataType != typeof(Int64))
                return new LazyJsonNull();

            return new LazyJsonInteger(Convert.ToInt64(data));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}