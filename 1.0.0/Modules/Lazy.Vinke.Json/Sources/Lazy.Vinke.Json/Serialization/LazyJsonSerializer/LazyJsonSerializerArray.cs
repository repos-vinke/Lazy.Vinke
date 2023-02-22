// LazyJsonSerializerArray.cs
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
    public class LazyJsonSerializerArray : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json array token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json array token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonNull();

            Type dataType = data.GetType();

            if (dataType.IsArray == false)
                return new LazyJsonNull();

            Array dataArray = (data as Array);
            LazyJsonArray jsonArray = new LazyJsonArray();

            Type arrayElementType = dataType.GetElementType();

            Type jsonSerializerType = LazyJsonSerializer.FindTypeSerializer(arrayElementType);

            if (jsonSerializerType == null)
                jsonSerializerType = LazyJsonSerializer.FindBuiltInSerializer(arrayElementType);

            if (jsonSerializerType != null)
            {
                LazyJsonSerializerBase jsonSerializer = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);

                foreach (Object item in dataArray)
                    jsonArray.TokenList.Add(jsonSerializer.Serialize(item, serializerOptions));
            }
            else
            {
                foreach (Object item in dataArray)
                    jsonArray.TokenList.Add(LazyJsonSerializer.SerializeToken(item, serializerOptions));
            }

            return jsonArray;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}