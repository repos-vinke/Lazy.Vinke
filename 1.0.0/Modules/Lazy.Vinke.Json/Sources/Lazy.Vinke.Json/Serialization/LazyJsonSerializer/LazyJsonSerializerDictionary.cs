// LazyJsonSerializerDictionary.cs
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
    public class LazyJsonSerializerDictionary : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json dictionary array token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json dictionary array token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonNull();

            Type dataType = data.GetType();

            if (dataType.IsGenericType == false || dataType.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return new LazyJsonNull();

            Object collection = null;
            Int32 itemsCount = (Int32)dataType.GetProperties().First(x => x.Name == "Count").GetValue(data);
            LazyJsonArray jsonArray = new LazyJsonArray();

            Array keysArray = Array.CreateInstance(dataType.GenericTypeArguments[0], itemsCount);
            collection = dataType.GetProperties().First(x => x.Name == "Keys").GetValue(data);
            collection.GetType().GetMethods().First(x => x.Name == "CopyTo").Invoke(collection, new Object[] { keysArray, 0 });

            Array valuesArray = Array.CreateInstance(dataType.GenericTypeArguments[1], itemsCount);
            collection = dataType.GetProperties().First(x => x.Name == "Values").GetValue(data);
            collection.GetType().GetMethods().First(x => x.Name == "CopyTo").Invoke(collection, new Object[] { valuesArray, 0 });

            Type jsonSerializerType = null;
            LazyJsonSerializerBase jsonSerializerKey = null;
            LazyJsonSerializerBase jsonSerializerValue = null;

            // Keys serializer
            jsonSerializerType = LazyJsonSerializer.FindTypeSerializer(dataType.GenericTypeArguments[0]);

            if (jsonSerializerType == null)
                jsonSerializerType = LazyJsonSerializer.FindBuiltInSerializer(dataType.GenericTypeArguments[0]);

            if (jsonSerializerType != null)
                jsonSerializerKey = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);

            // Values serializer
            jsonSerializerType = LazyJsonSerializer.FindTypeSerializer(dataType.GenericTypeArguments[1]);

            if (jsonSerializerType == null)
                jsonSerializerType = LazyJsonSerializer.FindBuiltInSerializer(dataType.GenericTypeArguments[1]);

            if (jsonSerializerType != null)
                jsonSerializerValue = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);

            for (int i = 0; i < itemsCount; i++)
            {
                LazyJsonToken jsonTokenKey = null;
                LazyJsonToken jsonTokenValue = null;

                if (jsonSerializerKey != null)
                {
                    jsonTokenKey = jsonSerializerKey.Serialize(keysArray.GetValue(i), serializerOptions);
                }
                else
                {
                    jsonTokenKey = LazyJsonSerializer.SerializeToken(keysArray.GetValue(i), serializerOptions);
                }

                if (jsonSerializerValue != null)
                {
                    jsonTokenValue = jsonSerializerValue.Serialize(valuesArray.GetValue(i), serializerOptions);
                }
                else
                {
                    jsonTokenValue = LazyJsonSerializer.SerializeToken(valuesArray.GetValue(i), serializerOptions);
                }

                LazyJsonArray jsonArrayKeyValuePair = new LazyJsonArray();
                jsonArrayKeyValuePair.TokenList.Add(jsonTokenKey);
                jsonArrayKeyValuePair.TokenList.Add(jsonTokenValue);
                jsonArray.Add(jsonArrayKeyValuePair);
            }

            return jsonArray;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}