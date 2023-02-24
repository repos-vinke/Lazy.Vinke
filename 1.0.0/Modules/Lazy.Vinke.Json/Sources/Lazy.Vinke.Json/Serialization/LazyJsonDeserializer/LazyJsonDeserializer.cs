// LazyJsonDeserializer.cs
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
    public static class LazyJsonDeserializer
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Deserialize the json to the desired object
        /// </summary>
        /// <typeparam name="T">The type of the desired object</typeparam>
        /// <param name="json">The json to be deserialized</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public static T Deserialize<T>(String json, LazyJsonDeserializerOptions deserializerOptions = null, LazyJsonReaderOptions readerOptions = null)
        {
            return (T)Deserialize(json, typeof(T), deserializerOptions, readerOptions);
        }

        /// <summary>
        /// Deserialize the json token to the desired object
        /// </summary>
        /// <typeparam name="T">The type of the desired object</typeparam>
        /// <param name="jsonToken">The json token to be deserialized</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public static T DeserializeToken<T>(LazyJsonToken jsonToken, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            return (T)DeserializeToken(jsonToken, typeof(T), deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json to the desired object
        /// </summary>
        /// <param name="json">The json to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public static Object Deserialize(String json, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null, LazyJsonReaderOptions readerOptions = null)
        {
            LazyJson lazyJson = LazyJsonReader.Read(json, readerOptions);
            return DeserializeToken(lazyJson.Root, dataType, deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public static Object DeserializeToken(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || jsonToken.Type == LazyJsonType.Null || dataType == null)
                return null;

            Type jsonDeserializerType = FindTypeDeserializer(dataType);

            if (jsonDeserializerType == null)
                jsonDeserializerType = FindBuiltInDeserializer(jsonToken, dataType);

            if (jsonDeserializerType != null)
            {
                return ((LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType)).Deserialize(jsonToken, dataType, deserializerOptions);
            }
            else
            {
                if (jsonToken.Type != LazyJsonType.Object)
                    return null;

                return DeserializeObject((LazyJsonObject)jsonToken, dataType, deserializerOptions);
            }
        }

        /// <summary>
        /// Deserialize the json object token to the desired object
        /// </summary>
        /// <param name="jsonObject">The json object token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public static Object DeserializeObject(LazyJsonObject jsonObject, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonObject == null || dataType == null)
                return null;

            Object data = Activator.CreateInstance(dataType);
            List<String> alreadyDeserializer = new List<String>();
            PropertyInfo[] propertyInfoArray = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                if (propertyInfo.SetMethod == null)
                    continue;

                if (alreadyDeserializer.Contains(propertyInfo.Name) == true)
                    continue;

                if (propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributePropertyIgnore), false).Length > 0)
                    continue;

                alreadyDeserializer.Add(propertyInfo.Name);

                Type jsonDeserializerType = null;
                String propertyName = propertyInfo.Name;
                Object[] jsonAttributeBaseArray = propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributeBase), false);

                foreach (Object attribute in jsonAttributeBaseArray)
                {
                    if (attribute is LazyJsonAttributePropertyRename)
                    {
                        propertyName = ((LazyJsonAttributePropertyRename)attribute).Name;
                    }
                    else if (attribute is LazyJsonAttributeDeserializer)
                    {
                        LazyJsonAttributeDeserializer jsonAttributeDeserializer = (LazyJsonAttributeDeserializer)attribute;

                        if (jsonAttributeDeserializer.DeserializerType != null && jsonAttributeDeserializer.DeserializerType.IsSubclassOf(typeof(LazyJsonDeserializerBase)) == true)
                            jsonDeserializerType = jsonAttributeDeserializer.DeserializerType;
                    }
                }

                if (jsonObject[propertyName] != null)
                {
                    if (jsonDeserializerType != null)
                    {
                        propertyInfo.SetValue(data, ((LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType)).Deserialize(jsonObject[propertyName], propertyInfo.PropertyType, deserializerOptions));
                    }
                    else
                    {
                        Object value = DeserializeToken(jsonObject[propertyName].Token, propertyInfo.PropertyType, deserializerOptions);

                        /* This is necessary to avoid invalid cast from Int32 to Int16 */
                        if (propertyInfo.PropertyType == typeof(Int16)) value = Convert.ToInt16(value);

                        propertyInfo.SetValue(data, value);
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Find a deserializer for the type defined on class declaration
        /// </summary>
        /// <param name="dataType">The data type to look for</param>
        /// <returns>The type of the deserializer</returns>
        public static Type FindTypeDeserializer(Type dataType)
        {
            #region Invalid Types for GetCustomAttributes

            if (dataType == typeof(DateTime) || dataType == typeof(Nullable<DateTime>)) return null;

            #endregion Invalid Types for GetCustomAttributes

            LazyJsonAttributeDeserializer jsonAttributeDeserializer = null;
            Object[] jsonAttributeDeserializerArray = dataType.GetCustomAttributes(typeof(LazyJsonAttributeDeserializer), false);

            if (jsonAttributeDeserializerArray.Length > 0)
                jsonAttributeDeserializer = (LazyJsonAttributeDeserializer)jsonAttributeDeserializerArray[0];

            if (jsonAttributeDeserializer != null && jsonAttributeDeserializer.DeserializerType != null && jsonAttributeDeserializer.DeserializerType.IsSubclassOf(typeof(LazyJsonDeserializerBase)) == true)
                return jsonAttributeDeserializer.DeserializerType;

            return null;
        }

        /// <summary>
        /// Find built in deserializer for the json token or the type
        /// </summary>
        /// <param name="jsonToken">The json token to be deserialized</param>
        /// <param name="dataType">The data type to look for</param>
        /// <returns>The type of the deserializer</returns>
        public static Type FindBuiltInDeserializer(LazyJsonToken jsonToken, Type dataType)
        {
            if (dataType == null)
                return null;

            if (dataType == typeof(Boolean)) return typeof(LazyJsonDeserializerBoolean);
            if (dataType == typeof(Char)) return typeof(LazyJsonDeserializerString);
            if (dataType == typeof(String)) return typeof(LazyJsonDeserializerString);
            if (dataType == typeof(Int16)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(Int32)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(Int64)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(float)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(Double)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(Decimal)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(DateTime)) return typeof(LazyJsonDeserializerDateTime);

            if (dataType == typeof(Nullable<Boolean>)) return typeof(LazyJsonDeserializerBoolean);
            if (dataType == typeof(Nullable<Char>)) return typeof(LazyJsonDeserializerString);
            if (dataType == typeof(Nullable<Int16>)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(Nullable<Int32>)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(Nullable<Int64>)) return typeof(LazyJsonDeserializerInteger);
            if (dataType == typeof(Nullable<float>)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(Nullable<Double>)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(Nullable<Decimal>)) return typeof(LazyJsonDeserializerDecimal);
            if (dataType == typeof(Nullable<DateTime>)) return typeof(LazyJsonDeserializerDateTime);

            if (dataType.IsArray == true) return typeof(LazyJsonDeserializerArray);
            if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(List<>)) return typeof(LazyJsonDeserializerList);
            if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return typeof(LazyJsonDeserializerDictionary);

            if (dataType == typeof(DataSet)) return typeof(LazyJsonDeserializerDataSet);
            if (dataType == typeof(DataTable)) return typeof(LazyJsonDeserializerDataTable);

            if (jsonToken.Type == LazyJsonType.String) return typeof(LazyJsonDeserializerString);
            if (jsonToken.Type == LazyJsonType.Boolean) return typeof(LazyJsonDeserializerBoolean);
            if (jsonToken.Type == LazyJsonType.Integer) return typeof(LazyJsonDeserializerInteger);
            if (jsonToken.Type == LazyJsonType.Decimal) return typeof(LazyJsonDeserializerDecimal);

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public abstract class LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Deserialize the json property to the desired object
        /// </summary>
        /// <typeparam name="T">The type of the desired object</typeparam>
        /// <param name="jsonProperty">The json property to be deserialized</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public T Deserialize<T>(LazyJsonProperty jsonProperty, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            return (T)Deserialize(jsonProperty, typeof(T), deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json token to the desired object
        /// </summary>
        /// <typeparam name="T">The type of the desired object</typeparam>
        /// <param name="jsonToken">The json token to be deserialized</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public T Deserialize<T>(LazyJsonToken jsonToken, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            return (T)Deserialize(jsonToken, typeof(T), deserializerOptions);
        }

        /// <summary>
        /// Deserialize the json property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json property to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public abstract Object Deserialize(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null);

        /// <summary>
        /// Deserialize the json token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public abstract Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null);

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}