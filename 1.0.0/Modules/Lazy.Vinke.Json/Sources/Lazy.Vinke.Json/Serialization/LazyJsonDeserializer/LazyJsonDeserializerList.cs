// LazyJsonDeserializerList.cs
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
    public class LazyJsonDeserializerList : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json list array property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json list array property to be deserialized</param>
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
        /// Deserialize the json list array token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json list array token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType)
        {
            if (jsonToken == null || jsonToken.Type != LazyJsonType.Array || dataType == null || dataType.IsGenericType == false || dataType.GetGenericTypeDefinition() != typeof(List<>))
                return null;

            LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;
            Object list = Activator.CreateInstance(dataType);
            MethodInfo methodAdd = dataType.GetMethods().First(x => x.Name == "Add");

            Type jsonDeserializerType = LazyJsonDeserializer.FindTypeDeserializer(dataType.GenericTypeArguments[0]);

            if (jsonDeserializerType == null)
                jsonDeserializerType = LazyJsonDeserializer.FindBuiltInDeserializer(jsonToken, dataType.GenericTypeArguments[0]);

            if (jsonDeserializerType != null)
            {
                LazyJsonDeserializerBase jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);

                for (int index = 0; index < jsonArray.Count; index++)
                    methodAdd.Invoke(list, new Object[] { jsonDeserializer.Deserialize(jsonArray.TokenList[index], dataType.GenericTypeArguments[0]) });
            }
            else
            {
                for (int index = 0; index < jsonArray.Count; index++)
                    methodAdd.Invoke(list, new Object[] { LazyJsonDeserializer.DeserializeToken(jsonArray.TokenList[index], dataType.GenericTypeArguments[0]) });
            }
            
            return list;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}