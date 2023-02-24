// LazyJsonDeserializerDictionary.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 28

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDictionary : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json dictionary array property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json dictionary array property to be deserialized</param>
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
        /// Deserialize the json dictionary array token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json dictionary array token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || jsonToken.Type != LazyJsonType.Array || dataType == null || dataType.IsGenericType == false || dataType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return null;

            LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;
            Object dictionary = Activator.CreateInstance(dataType);
            MethodInfo methodAdd = dataType.GetMethods().First(x => x.Name == "Add");

            Type jsonDeserializerType = null;
            LazyJsonDeserializerBase jsonDeserializerKey = null;
            LazyJsonDeserializerBase jsonDeserializerValue = null;

            // Keys deserializer
            jsonDeserializerType = LazyJsonDeserializer.FindTypeDeserializer(dataType.GenericTypeArguments[0]);

            if (jsonDeserializerType == null)
                jsonDeserializerType = LazyJsonDeserializer.FindBuiltInDeserializer(jsonToken, dataType.GenericTypeArguments[0]);

            if (jsonDeserializerType != null)
                jsonDeserializerKey = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);

            // Values deserializer
            jsonDeserializerType = LazyJsonDeserializer.FindTypeDeserializer(dataType.GenericTypeArguments[1]);

            if (jsonDeserializerType == null)
                jsonDeserializerType = LazyJsonDeserializer.FindBuiltInDeserializer(jsonToken, dataType.GenericTypeArguments[1]);

            if (jsonDeserializerType != null)
                jsonDeserializerValue = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);

            for (int index = 0; index < jsonArray.Count; index++)
            {
                if (jsonArray.TokenList[index].Type != LazyJsonType.Array)
                    continue;

                LazyJsonArray jsonArrayKeyValuePair = (LazyJsonArray)jsonArray.TokenList[index];

                if (jsonArrayKeyValuePair.TokenList.Count != 2)
                    continue;

                Object key = null;
                Object value = null;

                if (jsonDeserializerKey != null)
                {
                    key = jsonDeserializerKey.Deserialize(jsonArrayKeyValuePair.TokenList[0], dataType.GenericTypeArguments[0], deserializerOptions);
                }
                else
                {
                    key = LazyJsonDeserializer.DeserializeToken(jsonArrayKeyValuePair.TokenList[0], dataType.GenericTypeArguments[0], deserializerOptions);
                }

                if (jsonDeserializerValue != null)
                {
                    value = jsonDeserializerValue.Deserialize(jsonArrayKeyValuePair.TokenList[1], dataType.GenericTypeArguments[1], deserializerOptions);
                }
                else
                {
                    value = LazyJsonDeserializer.DeserializeToken(jsonArrayKeyValuePair.TokenList[1], dataType.GenericTypeArguments[1], deserializerOptions);
                }

                /* This is necessary to avoid invalid cast from Int32 to Int16 */
                if (dataType.GenericTypeArguments[0] == typeof(Int16)) key = Convert.ToInt16(key);
                if (dataType.GenericTypeArguments[1] == typeof(Int16)) value = Convert.ToInt16(value);

                methodAdd.Invoke(dictionary, new Object[] { key, value });
            }

            return dictionary;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}