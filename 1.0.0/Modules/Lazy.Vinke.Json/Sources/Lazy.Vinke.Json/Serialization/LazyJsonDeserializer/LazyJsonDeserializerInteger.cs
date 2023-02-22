// LazyJsonDeserializerInteger.cs
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
    public class LazyJsonDeserializerInteger : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json integer property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json integer property to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonProperty == null)
                return null;

            return Deserialize(jsonProperty.Token, dataType, deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json integer token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json integer token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || dataType == null)
                return null;

            if (jsonToken.Type == LazyJsonType.Null)
            {
                if (dataType == typeof(Int16)) return 0;
                if (dataType == typeof(Int32)) return 0;
                if (dataType == typeof(Int64)) return 0;
                if (dataType == typeof(Object)) return 0;
                if (dataType == typeof(Nullable<Int16>)) return null;
                if (dataType == typeof(Nullable<Int32>)) return null;
                if (dataType == typeof(Nullable<Int64>)) return null;
            }
            else if (jsonToken.Type == LazyJsonType.Integer)
            {
                LazyJsonInteger jsonInteger = (LazyJsonInteger)jsonToken;

                if (dataType == typeof(Int16)) return jsonInteger.Value == null ? 0 : Convert.ToInt16(jsonInteger.Value);
                if (dataType == typeof(Int32)) return jsonInteger.Value == null ? 0 : Convert.ToInt32(jsonInteger.Value);
                if (dataType == typeof(Int64)) return jsonInteger.Value == null ? 0 : Convert.ToInt64(jsonInteger.Value);
                if (dataType == typeof(Object)) return jsonInteger.Value == null ? 0 : Convert.ToInt64(jsonInteger.Value);
                if (dataType == typeof(Nullable<Int16>)) return jsonInteger.Value == null ? null : Convert.ToInt16(jsonInteger.Value);
                if (dataType == typeof(Nullable<Int32>)) return jsonInteger.Value == null ? null : Convert.ToInt32(jsonInteger.Value);
                if (dataType == typeof(Nullable<Int64>)) return jsonInteger.Value == null ? null : Convert.ToInt64(jsonInteger.Value);
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}