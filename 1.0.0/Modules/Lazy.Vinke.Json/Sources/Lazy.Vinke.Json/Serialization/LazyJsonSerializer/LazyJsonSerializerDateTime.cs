// LazyJsonSerializerDateTime.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 01

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerDateTime : LazyJsonSerializerBase
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
        public override LazyJsonToken Serialize(Object data)
        {
            if (data == null)
                return new LazyJsonString(null);

            if (data.GetType() == typeof(DateTime)) return new LazyJsonString(ToDateTimeIso((DateTime)data));
            if (data.GetType() == typeof(Nullable<DateTime>)) return new LazyJsonString(ToDateTimeIso((DateTime)data));

            return new LazyJsonString(null);
        }

        private String ToDateTimeIso(DateTime value)
        {
            return value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss':'fff'Z'");
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}