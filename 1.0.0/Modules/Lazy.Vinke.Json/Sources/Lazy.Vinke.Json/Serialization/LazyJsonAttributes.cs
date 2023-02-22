// LazyJsonAttributes.cs
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
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonAttributeBase : Attribute
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributeBase()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class LazyJsonAttributeSerializer : LazyJsonAttributeBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributeSerializer(Type jsonSerializerType)
        {
            this.SerializerType = jsonSerializerType;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Type SerializerType { get; private set; }

        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class LazyJsonAttributeDeserializer : LazyJsonAttributeBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributeDeserializer(Type jsonDeserializerType)
        {
            this.DeserializerType = jsonDeserializerType;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Type DeserializerType { get; private set; }

        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LazyJsonAttributeProperty : LazyJsonAttributeBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributeProperty()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LazyJsonAttributePropertyIgnore : LazyJsonAttributeProperty
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributePropertyIgnore()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LazyJsonAttributePropertyRename : LazyJsonAttributeProperty
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributePropertyRename(String name)
        {
            this.Name = name;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Name { get; private set; }

        #endregion Properties
    }
}