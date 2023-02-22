// LazyJsonDeserializerDateTime.cs
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
using System.Text.RegularExpressions;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDateTime : LazyJsonDeserializerBase
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
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonProperty == null)
                return null;

            return Deserialize(jsonProperty.Token, dataType, deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json string token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json string token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || jsonToken.Type == LazyJsonType.Null || dataType == null)
                return null;

            if (jsonToken.Type == LazyJsonType.String)
            {
                LazyJsonString jsonString = (LazyJsonString)jsonToken;

                if (jsonString.Value == null)
                    return null;

                if (dataType == typeof(DateTime) || dataType == typeof(Nullable<DateTime>))
                {
                    if (IsDateTimeIso(jsonString.Value)) return FromDateTimeIso(jsonString.Value);
                }
            }
            
            return null;
        }

        private Boolean IsDateTimeIso(String value)
        {
            return new Regex("^([0-9]{4})-([0-9]{2})-([0-9]{2})T([0-9]{2}):([0-9]{2}):([0-9]{2}):([0-9]{3})Z$").Match(value).Success;
        }

        private DateTime FromDateTimeIso(String value)
        {
            return new DateTime(//2023-02-01T10:11:12:345Z
                Convert.ToInt32(value.Substring(0, 4)), 
                Convert.ToInt32(value.Substring(5, 2)), 
                Convert.ToInt32(value.Substring(8, 2)), 
                Convert.ToInt32(value.Substring(11, 2)), 
                Convert.ToInt32(value.Substring(14, 2)), 
                Convert.ToInt32(value.Substring(17, 2)), 
                Convert.ToInt32(value.Substring(20, 3)));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}