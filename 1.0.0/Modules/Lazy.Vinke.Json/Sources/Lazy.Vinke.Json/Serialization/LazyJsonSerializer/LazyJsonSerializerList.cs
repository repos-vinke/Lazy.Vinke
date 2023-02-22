// LazyJsonSerializerList.cs
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
    public class LazyJsonSerializerList : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json list array token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json list array token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonNull();

            Type dataType = data.GetType();

            if (dataType.IsGenericType == false || dataType.GetGenericTypeDefinition() != typeof(List<>))
                return new LazyJsonNull();

            Int32 itemsCount = (Int32)dataType.GetProperties().First(x => x.Name == "Count").GetValue(data);
            PropertyInfo propertyIndexer = dataType.GetProperties().First(x => x.GetIndexParameters().Length == 1 && x.GetIndexParameters()[0].ParameterType == typeof(Int32));
            LazyJsonArray jsonArray = new LazyJsonArray();

            Type jsonSerializerType = LazyJsonSerializer.FindTypeSerializer(dataType.GenericTypeArguments[0]);

            if (jsonSerializerType == null)
                jsonSerializerType = LazyJsonSerializer.FindBuiltInSerializer(dataType.GenericTypeArguments[0]);

            if (jsonSerializerType != null)
            {
                LazyJsonSerializerBase jsonSerializer = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);

                for (int i = 0; i < itemsCount; i++)
                    jsonArray.TokenList.Add(jsonSerializer.Serialize(propertyIndexer.GetValue(data, new Object[] { i }), serializerOptions));
            }
            else
            {
                for (int i = 0; i < itemsCount; i++)
                    jsonArray.TokenList.Add(LazyJsonSerializer.SerializeToken(propertyIndexer.GetValue(data, new Object[] { i }), serializerOptions));
            }

            return jsonArray;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}