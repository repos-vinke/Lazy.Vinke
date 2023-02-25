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
using System.Globalization;

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
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null || data.GetType() != typeof(DateTime))
                return new LazyJsonNull();
            
            if (serializerOptions != null && serializerOptions.Contains<LazyJsonSerializerOptionsDateTime>() == true)
            {
                String format = serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format;

                if (String.IsNullOrWhiteSpace(format) == false)
                    return new LazyJsonString(((DateTime)data).ToString(format));
            }

            return new LazyJsonString(((DateTime)data).ToString());
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonSerializerOptionsDateTime : LazyJsonSerializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Contructors

        public LazyJsonSerializerOptionsDateTime()
        {
        }

        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Format { get; set; }

        #endregion Properties
    }
}