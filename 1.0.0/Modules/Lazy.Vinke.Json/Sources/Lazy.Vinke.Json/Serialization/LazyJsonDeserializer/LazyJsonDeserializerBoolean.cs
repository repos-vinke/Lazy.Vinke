// LazyJsonDeserializerBoolean.cs
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
    public class LazyJsonDeserializerBoolean : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json boolean property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json boolean property to be deserialized</param>
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
        /// Deserialize the json boolean token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json boolean token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || dataType == null)
                return null;

            if (jsonToken.Type == LazyJsonType.Null)
            {
                if (dataType == typeof(Boolean)) return false;
                if (dataType == typeof(Object)) return false;
                if (dataType == typeof(Nullable<Boolean>)) return null;
            }
            else if (jsonToken.Type == LazyJsonType.Boolean)
            {
                LazyJsonBoolean jsonBoolean = (LazyJsonBoolean)jsonToken;
                
                if (dataType == typeof(Boolean)) return jsonBoolean.Value == null ? false : Convert.ToBoolean(jsonBoolean.Value);
                if (dataType == typeof(Object)) return jsonBoolean.Value == null ? false : Convert.ToBoolean(jsonBoolean.Value);
                if (dataType == typeof(Nullable<Boolean>)) return jsonBoolean.Value;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}