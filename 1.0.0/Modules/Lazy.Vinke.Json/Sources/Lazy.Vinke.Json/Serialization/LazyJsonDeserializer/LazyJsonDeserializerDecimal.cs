// LazyJsonDeserializerDecimal.cs
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
    public class LazyJsonDeserializerDecimal : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json decimal property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json decimal property to be deserialized</param>
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
        /// Deserialize the json decimal token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json decimal token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType)
        {
            if (jsonToken == null || dataType == null)
                return null;

            if (jsonToken.Type == LazyJsonType.Null)
            {
                if (dataType == typeof(float)) return 0;
                if (dataType == typeof(Double)) return 0;
                if (dataType == typeof(Decimal)) return 0;
                if (dataType == typeof(Object)) return 0;
                if (dataType == typeof(Nullable<float>)) return null;
                if (dataType == typeof(Nullable<Double>)) return null;
                if (dataType == typeof(Nullable<Decimal>)) return null;
            }
            else if (jsonToken.Type == LazyJsonType.Decimal)
            {
                LazyJsonDecimal jsonDecimal = (LazyJsonDecimal)jsonToken;

                if (dataType == typeof(float)) return jsonDecimal.Value == null ? 0 : (float)jsonDecimal.Value;
                if (dataType == typeof(Double)) return jsonDecimal.Value == null ? 0 : Convert.ToDouble(jsonDecimal.Value);
                if (dataType == typeof(Decimal)) return jsonDecimal.Value == null ? 0 : Convert.ToDecimal(jsonDecimal.Value);
                if (dataType == typeof(Object)) return jsonDecimal.Value == null ? 0 : Convert.ToDecimal(jsonDecimal.Value);
                if (dataType == typeof(Nullable<float>)) return jsonDecimal.Value == null ? null : (float)jsonDecimal.Value;
                if (dataType == typeof(Nullable<Double>)) return jsonDecimal.Value == null ? null : Convert.ToDouble(jsonDecimal.Value);
                if (dataType == typeof(Nullable<Decimal>)) return jsonDecimal.Value == null ? null : Convert.ToDecimal(jsonDecimal.Value);
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}