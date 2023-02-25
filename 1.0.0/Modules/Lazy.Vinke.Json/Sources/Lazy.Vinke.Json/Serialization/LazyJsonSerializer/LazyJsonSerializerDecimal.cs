// LazyJsonSerializerDecimal.cs
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
    public class LazyJsonSerializerDecimal : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json decimal token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json decimal token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonDecimal(null);

            Type dataType = data.GetType();

            if (dataType != typeof(Decimal) && dataType != typeof(Double) && dataType != typeof(float))
                return new LazyJsonNull();

            return new LazyJsonDecimal(Convert.ToDecimal(data));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}