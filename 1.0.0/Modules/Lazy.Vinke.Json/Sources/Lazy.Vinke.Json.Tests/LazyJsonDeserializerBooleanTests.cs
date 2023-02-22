// LazyJsonDeserializerBooleanTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerBooleanTests
    {
        [TestMethod]
        public void TestDeserializerBooleanPropertyNull()
        {
            // Arrange
            Object resBoolean = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerBoolean deserializerBoolean = new LazyJsonDeserializerBoolean();

            // Act
            resBoolean = deserializerBoolean.Deserialize(jsonProperty, typeof(Boolean));

            // Assert
            Assert.IsNull(resBoolean);
        }

        [TestMethod]
        public void TestDeserializerBooleanTokenNull()
        {
            // Arrange
            Object resBoolean = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerBoolean deserializerBoolean = new LazyJsonDeserializerBoolean();

            // Act
            resBoolean = deserializerBoolean.Deserialize(jsonToken, typeof(Boolean));

            // Assert
            Assert.IsNull(resBoolean);
        }

        [TestMethod]
        public void TestDeserializerBooleanDataTypeNull()
        {
            // Arrange
            Object resBoolean = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonBoolean(true));
            LazyJsonDeserializerBoolean deserializerBoolean = new LazyJsonDeserializerBoolean();

            // Act
            resBoolean = deserializerBoolean.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resBoolean);
        }

        [TestMethod]
        public void TestDeserializerBooleanJsonTypeNull()
        {
            // Arrange
            Type typeBoolean = typeof(Boolean);
            Type typeObject = typeof(Object);
            Type typeNullableBoolean = typeof(Boolean?);

            Object resBoolean = null;
            Object resObject = null;
            Object resNullableBoolean = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonNull());
            LazyJsonDeserializerBoolean deserializerBoolean = new LazyJsonDeserializerBoolean();

            // Act
            resBoolean = deserializerBoolean.Deserialize(jsonProperty, typeBoolean);
            resObject = deserializerBoolean.Deserialize(jsonProperty, typeObject);
            resNullableBoolean = deserializerBoolean.Deserialize(jsonProperty, typeNullableBoolean);

            // Assert
            Assert.IsFalse((Boolean)resBoolean);
            Assert.IsFalse((Boolean)resObject);
            Assert.IsNull(resNullableBoolean);
        }

        [TestMethod]
        public void TestDeserializerBooleanJsonTypeBoolean()
        {
            // Arrange
            Type typeBoolean = typeof(Boolean);
            Type typeObject = typeof(Object);
            Type typeNullableBoolean = typeof(Boolean?);

            Object resBoolean = null;
            Object resObject = null;
            Object resNullableBoolean = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonBoolean(true));
            LazyJsonDeserializerBoolean deserializerBoolean = new LazyJsonDeserializerBoolean();

            // Act
            resBoolean = deserializerBoolean.Deserialize(jsonProperty, typeBoolean);
            resObject = deserializerBoolean.Deserialize(jsonProperty, typeObject);
            resNullableBoolean = deserializerBoolean.Deserialize(jsonProperty, typeNullableBoolean);

            // Assert
            Assert.IsTrue((Boolean)resBoolean);
            Assert.IsTrue((Boolean)resObject);
            Assert.IsTrue((Boolean)resNullableBoolean);
        }
    }
}