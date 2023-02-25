// LazyAttributes.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2021, January 09

using System;
using System.Xml;
using System.Data;
using System.Reflection;

namespace Lazy.Vinke
{
    public static class LazyAttribute
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Get custom attribute from class
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="type">The type of the object that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromClass<T>(Type type, Int32 index = 0)
        {
            Object[] attributeArray = type.GetCustomAttributes(typeof(T), false);

            if (attributeArray.Length > index)
                return (T)attributeArray[index];

            return default(T);
        }

        /// <summary>
        /// Get custom attribute from class field
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="type">The type of the object that holds the attribute</param>
        /// <param name="fieldName">The field name of the object that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromClassField<T>(Type type, String fieldName, Int32 index = 0)
        {
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                Object[] attributeArray = fieldInfo.GetCustomAttributes(typeof(T), false);

                if (attributeArray.Length > index)
                    return (T)attributeArray[index];
            }

            return default(T);
        }

        /// <summary>
        /// Get custom attribute from class method
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="type">The type of the object that holds the attribute</param>
        /// <param name="methodName">The method name of the object that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromClassMethod<T>(Type type, String methodName, Int32 index = 0)
        {
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (methodInfo != null)
            {
                Object[] attributeArray = methodInfo.GetCustomAttributes(typeof(T), false);

                if (attributeArray.Length > index)
                    return (T)attributeArray[index];
            }

            return default(T);
        }

        /// <summary>
        /// Get custom attribute from class property
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="type">The type of the object that holds the attribute</param>
        /// <param name="propertyName">The property name of the object that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromClassProperty<T>(Type type, String propertyName, Int32 index = 0)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                Object[] attributeArray = propertyInfo.GetCustomAttributes(typeof(T), false);

                if (attributeArray.Length > index)
                    return (T)attributeArray[index];
            }

            return default(T);
        }

        /// <summary>
        /// Get custom attribute from enumerator
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="type">The type of the enumerator that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromEnum<T>(Type type, Int32 index = 0)
        {
            return GetCustomAttributeFromClass<T>(type, index);
        }

        /// <summary>
        /// Get custom attribute from enumerator value
        /// </summary>
        /// <typeparam name="T">The type of the attribute</typeparam>
        /// <param name="value">The value of the enumerator that holds the attribute</param>
        /// <param name="index">The index of the attribute case exists more than one</param>
        /// <returns>The desired attribute</returns>
        public static T GetCustomAttributeFromEnumValue<T>(Object value, Int32 index = 0)
        {
            return GetCustomAttributeFromClassField<T>(value.GetType(), Enum.GetName(value.GetType(), value), index);
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyAttributeBase : Attribute
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyAttributeBase()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyAttributeGeneric : LazyAttributeBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyAttributeGeneric(String code, String name, Object data = null)
        {
            this.Code = code;
            this.Name = name;
            this.Data = data;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Code { get; }

        public String Name { get; }

        public Object Data { get; }

        #endregion Properties
    }
}