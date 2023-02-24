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
using System.Globalization;

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
            if (jsonToken == null || jsonToken.Type != LazyJsonType.String || dataType == null)
                return null;

            LazyJsonString jsonString = (LazyJsonString)jsonToken;

            if (jsonString.Value == null)
                return null;

            if (deserializerOptions != null && deserializerOptions.Contains<LazyJsonDeserializerOptionsDateTime>() == true)
            {
                String regex = deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Regex;

                if (String.IsNullOrWhiteSpace(regex) == false)
                {
                    if (new Regex(regex).Match(jsonString.Value).Success == false)
                        return null;
                }

                String format = deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format;
                CultureInfo cultureInfo = deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().CultureInfo;

                if (String.IsNullOrWhiteSpace(format) == false)
                    return DateTime.ParseExact(jsonString.Value, format, cultureInfo != null ? cultureInfo : CultureInfo.CurrentCulture);
            }

            try { return DateTime.Parse(jsonString.Value); }
            catch { return null; }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonDeserializerOptionsDateTime : LazyJsonDeserializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Contructors

        public LazyJsonDeserializerOptionsDateTime()
        {
        }

        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Regex { get; set; }

        public String Format { get; set; }

        public CultureInfo CultureInfo { get; set; }

        #endregion Properties
    }
}