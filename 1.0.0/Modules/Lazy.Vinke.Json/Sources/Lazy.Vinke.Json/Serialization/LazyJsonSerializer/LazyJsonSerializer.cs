// LazyJsonSerializer.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 18

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonSerializer
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Serialize an object to json
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json</returns>
        public static String Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null, LazyJsonWriterOptions jsonWriterOptions = null)
        {
            LazyJson lazyJson = new LazyJson();
            lazyJson.AppendRoot(SerializeToken(data, serializerOptions));
            return LazyJsonWriter.Write(lazyJson, jsonWriterOptions);
        }

        /// <summary>
        /// Serialize an object to a json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json token</returns>
        public static LazyJsonToken SerializeToken(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonNull();

            Type dataType = data.GetType();

            Type jsonSerializerType = FindTypeSerializer(dataType);

            if (jsonSerializerType == null)
                jsonSerializerType = FindBuiltInSerializer(dataType);

            if (jsonSerializerType != null)
            {
                return ((LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType)).Serialize(data, serializerOptions);
            }
            else
            {
                return SerializeObject(data, serializerOptions);
            }
        }

        /// <summary>
        /// Serialize an object to a json token by object property info
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json token</returns>
        public static LazyJsonToken SerializeObject(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null)
                return new LazyJsonNull();

            Type dataType = data.GetType();

            if (dataType.IsArray == true || dataType.IsGenericType == true)
                return SerializeToken(data, serializerOptions);

            LazyJsonObject jsonObject = new LazyJsonObject();
            List<String> alreadySerializer = new List<String>();
            PropertyInfo[] propertyInfoArray = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                if (propertyInfo.GetMethod == null)
                    continue;

                if (alreadySerializer.Contains(propertyInfo.Name) == true)
                    continue;

                if (propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributePropertyIgnore), false).Length > 0)
                    continue;

                alreadySerializer.Add(propertyInfo.Name);

                Type jsonSerializerType = null;
                String propertyName = propertyInfo.Name;
                Object[] jsonAttributeBaseArray = propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributeBase), false);

                foreach (Object attribute in jsonAttributeBaseArray)
                {
                    if (attribute is LazyJsonAttributePropertyRename)
                    {
                        propertyName = ((LazyJsonAttributePropertyRename)attribute).Name;
                    }
                    else if (attribute is LazyJsonAttributeSerializer)
                    {
                        LazyJsonAttributeSerializer jsonAttributeSerializer = (LazyJsonAttributeSerializer)attribute;

                        if (jsonAttributeSerializer.SerializerType != null && jsonAttributeSerializer.SerializerType.IsSubclassOf(typeof(LazyJsonSerializerBase)) == true)
                            jsonSerializerType = jsonAttributeSerializer.SerializerType;
                    }
                }

                if (jsonSerializerType != null)
                {
                    jsonObject.Add(new LazyJsonProperty(propertyName, ((LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType)).Serialize(propertyInfo.GetValue(data), serializerOptions)));
                }
                else
                {
                    jsonObject.Add(new LazyJsonProperty(propertyName, SerializeToken(propertyInfo.GetValue(data), serializerOptions)));
                }
            }

            return jsonObject;
        }

        /// <summary>
        /// Find a serializer for the type defined on class declaration
        /// </summary>
        /// <param name="dataType">The data type to look for</param>
        /// <returns>The type of the serializer</returns>
        public static Type FindTypeSerializer(Type dataType)
        {
            #region Invalid Types for GetCustomAttributes

            if (dataType == typeof(DateTime) || dataType == typeof(Nullable<DateTime>)) return null;

            #endregion Invalid Types for GetCustomAttributes

            LazyJsonAttributeSerializer jsonAttributeSerializer = null;
            Object[] jsonAttributeSerializerArray = dataType.GetCustomAttributes(typeof(LazyJsonAttributeSerializer), false);

            if (jsonAttributeSerializerArray.Length > 0)
                jsonAttributeSerializer = (LazyJsonAttributeSerializer)jsonAttributeSerializerArray[0];

            if (jsonAttributeSerializer != null && jsonAttributeSerializer.SerializerType != null && jsonAttributeSerializer.SerializerType.IsSubclassOf(typeof(LazyJsonSerializerBase)) == true)
                return jsonAttributeSerializer.SerializerType;

            return null;
        }

        /// <summary>
        /// Find built in serializer for the type
        /// </summary>
        /// <param name="dataType">The data type to look for</param>
        /// <returns>The type of the serializer</returns>
        public static Type FindBuiltInSerializer(Type dataType)
        {
            if (dataType == null)
                return null;

            if (dataType == typeof(Boolean)) return typeof(LazyJsonSerializerBoolean);
            if (dataType == typeof(Char)) return typeof(LazyJsonSerializerString);
            if (dataType == typeof(String)) return typeof(LazyJsonSerializerString);
            if (dataType == typeof(Int16)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(Int32)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(Int64)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(float)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(Double)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(Decimal)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(DateTime)) return typeof(LazyJsonSerializerDateTime);

            if (dataType == typeof(Nullable<Boolean>)) return typeof(LazyJsonSerializerBoolean);
            if (dataType == typeof(Nullable<Char>)) return typeof(LazyJsonSerializerString);
            if (dataType == typeof(Nullable<Int16>)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(Nullable<Int32>)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(Nullable<Int64>)) return typeof(LazyJsonSerializerInteger);
            if (dataType == typeof(Nullable<float>)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(Nullable<Double>)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(Nullable<Decimal>)) return typeof(LazyJsonSerializerDecimal);
            if (dataType == typeof(Nullable<DateTime>)) return typeof(LazyJsonSerializerDateTime);

            if (dataType.IsArray == true) return typeof(LazyJsonSerializerArray);
            if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(List<>)) return typeof(LazyJsonSerializerList);
            if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return typeof(LazyJsonSerializerDictionary);

            if (dataType == typeof(DataSet)) return typeof(LazyJsonSerializerDataSet);
            if (dataType == typeof(DataTable)) return typeof(LazyJsonSerializerDataTable);

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public abstract class LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Serialize an object to a json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json token</returns>
        public abstract LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null);

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}