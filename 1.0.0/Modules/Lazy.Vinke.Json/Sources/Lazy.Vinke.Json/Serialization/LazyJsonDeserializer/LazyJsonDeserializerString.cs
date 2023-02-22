// LazyJsonDeserializerString.cs
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
    public class LazyJsonDeserializerString : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json string property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json string property to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType)
        {
            if (jsonProperty == null)
                return null;

            return Deserialize(jsonProperty.Token, dataType);
        }

        /// <summary>
        /// Deserialize the json string token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json string token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType)
        {
            if (jsonToken == null || dataType == null)
                return null;

            if (jsonToken.Type == LazyJsonType.Null)
            {
                if (dataType == typeof(Char)) return '\0';
                if (dataType == typeof(String)) return null;
                if (dataType == typeof(Object)) return null;
                if (dataType == typeof(Nullable<Char>)) return null;
            }
            else if (jsonToken.Type == LazyJsonType.String)
            {
                LazyJsonString jsonString = (LazyJsonString)jsonToken;

                if (dataType == typeof(Char)) return jsonString.Value == null ? '\0' : Convert.ToChar(jsonString.Value);
                if (dataType == typeof(String)) return jsonString.Value;
                if (dataType == typeof(Object)) return jsonString.Value;
                if (dataType == typeof(Nullable<Char>)) return jsonString.Value == null ? null : Convert.ToChar(jsonString.Value);
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}