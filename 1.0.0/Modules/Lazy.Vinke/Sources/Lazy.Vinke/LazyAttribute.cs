// LazyAttribute.cs
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
    public class LazyAttribute : Attribute
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyAttribute(String code, String name)
        {
            this.Code = code;
            this.Name = name;
        }

        public LazyAttribute(String code, String name, Object data)
        {
            this.Code = code;
            this.Name = name;
            this.Data = data;
        }

        #endregion Constructors

        #region Methods

        public static LazyAttribute GetCustomAttributeFromClass(Type type, Int32 index = 0)
        {
            Object[] attributeArray = type.GetCustomAttributes(typeof(LazyAttribute), false);

            if (attributeArray.Length > index)
                return (LazyAttribute)attributeArray[index];

            return null;
        }

        public static LazyAttribute GetCustomAttributeFromClassField(Type type, String field, Int32 index = 0)
        {
            FieldInfo fieldInfo = type.GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                Object[] attributeArray = fieldInfo.GetCustomAttributes(typeof(LazyAttribute), false);

                if (attributeArray.Length > index)
                    return (LazyAttribute)attributeArray[index];
            }

            return null;
        }

        public static LazyAttribute GetCustomAttributeFromEnum(Type type, Int32 index = 0)
        {
            return GetCustomAttributeFromClass(type, index);
        }

        public static LazyAttribute GetCustomAttributeFromEnumValue(Object value, Int32 index = 0)
        {
            return GetCustomAttributeFromClassField(value.GetType(), Enum.GetName(value.GetType(), value), index);
        }

        #endregion Methods

        #region Properties

        public String Code { get; }

        public String Name { get; }

        public Object Data { get; }

        #endregion Properties
    }
}