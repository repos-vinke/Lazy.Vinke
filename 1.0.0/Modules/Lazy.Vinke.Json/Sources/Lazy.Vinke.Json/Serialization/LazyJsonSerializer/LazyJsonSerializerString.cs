// LazyJsonSerializerString.cs
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
    public class LazyJsonSerializerString : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json string token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json string token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonString(null);

            Type dataType = data.GetType();

            if (dataType != typeof(String) && dataType != typeof(Char))
                return new LazyJsonNull();

            return new LazyJsonString(Convert.ToString(data));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}