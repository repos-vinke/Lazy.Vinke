// LazyJsonDeserializerArray.cs
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
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerArray : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json array property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json array property to be deserialized</param>
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
        /// Deserialize the json array token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json array token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || jsonToken.Type != LazyJsonType.Array || dataType == null || dataType.IsArray == false)
                return null;

            LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;
            Array array = Array.CreateInstance(dataType.GetElementType(), jsonArray.Count);
            Type arrayElementType = dataType.GetElementType();

            Type jsonDeserializerType = LazyJsonDeserializer.FindTypeDeserializer(arrayElementType);

            if (jsonDeserializerType == null)
                jsonDeserializerType = LazyJsonDeserializer.FindBuiltInDeserializer(jsonToken, arrayElementType);

            if (jsonDeserializerType != null)
            {
                LazyJsonDeserializerBase jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);

                for (int index = 0; index < jsonArray.Count; index++)
                {
                    Object value = jsonDeserializer.Deserialize(jsonArray.TokenList[index], arrayElementType, deserializerOptions);

                    /* This is necessary to avoid invalid cast from Int32 to Int16 */
                    if (arrayElementType == typeof(Int16)) value = Convert.ToInt16(value);

                    array.SetValue(value, index);
                }
            }
            else
            {
                for (int index = 0; index < jsonArray.Count; index++)
                {
                    Object value = LazyJsonDeserializer.DeserializeToken(jsonArray.TokenList[index], arrayElementType, deserializerOptions);

                    /* This is necessary to avoid invalid cast from Int32 to Int16 */
                    if (arrayElementType == typeof(Int16)) value = Convert.ToInt16(value);

                    array.SetValue(value, index);
                }
            }

            return array;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}