// LazyJsonDeserializerStringTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 23

using System;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerStringTests
    {
        [TestMethod]
        public void TestDeserializerStringPropertyNull()
        {
            // Arrange
            Object resString = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resString = deserializerString.Deserialize(jsonProperty, typeof(String));

            // Assert
            Assert.IsNull(resString);
        }

        [TestMethod]
        public void TestDeserializerStringTokenNull()
        {
            // Arrange
            Object resString = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resString = deserializerString.Deserialize(jsonToken, typeof(String));

            // Assert
            Assert.IsNull(resString);
        }

        [TestMethod]
        public void TestDeserializerStringDataTypeNull()
        {
            // Arrange
            Object resString = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString("Json"));
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resString = deserializerString.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resString);
        }

        [TestMethod]
        public void TestDeserializerStringTokenTypeNull()
        {
            // Arrange
            Object resChar = null;
            Object resString = null;
            Object resObject = null;
            Object resNullableChar = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonNull());
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resChar = deserializerString.Deserialize(jsonProperty, typeof(Char));
            resString = deserializerString.Deserialize(jsonProperty, typeof(String));
            resObject = deserializerString.Deserialize(jsonProperty, typeof(Object));
            resNullableChar = deserializerString.Deserialize(jsonProperty, typeof(Char?));

            // Assert
            Assert.IsTrue((Char)resChar == '\0');
            Assert.IsNull(resString);
            Assert.IsNull(resObject);
            Assert.IsNull(resNullableChar);
        }

        [TestMethod]
        public void TestDeserializerStringValueNull()
        {
            // Arrange
            Object resChar = null;
            Object resString = null;
            Object resObject = null;
            Object resNullableChar = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString(null));
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resChar = deserializerString.Deserialize(jsonProperty, typeof(Char));
            resString = deserializerString.Deserialize(jsonProperty, typeof(String));
            resObject = deserializerString.Deserialize(jsonProperty, typeof(Object));
            resNullableChar = deserializerString.Deserialize(jsonProperty, typeof(Char?));

            // Assert
            Assert.IsTrue((Char)resChar == '\0');
            Assert.IsNull(resString);
            Assert.IsNull(resObject);
            Assert.IsNull(resNullableChar);
        }

        [TestMethod]
        public void TestDeserializerString()
        {
            // Arrange
            Object resChar = null;
            Object resString = null;
            Object resObject = null;
            Object resNullableChar = null;

            LazyJsonProperty jsonPropertyChar = new LazyJsonProperty("Prop", new LazyJsonString("J"));
            LazyJsonDeserializerString deserializerChar = new LazyJsonDeserializerString();

            LazyJsonProperty jsonPropertyString = new LazyJsonProperty("Prop", new LazyJsonString("Json"));
            LazyJsonDeserializerString deserializerString = new LazyJsonDeserializerString();

            // Act
            resChar = deserializerChar.Deserialize(jsonPropertyChar, typeof(Char));
            resString = deserializerString.Deserialize(jsonPropertyString, typeof(String));
            resObject = deserializerString.Deserialize(jsonPropertyString, typeof(Object));
            resNullableChar = deserializerChar.Deserialize(jsonPropertyChar, typeof(Char?));

            // Assert
            Assert.IsTrue((Char)resChar == 'J');
            Assert.IsTrue((String)resString == "Json");
            Assert.IsTrue((String)resObject == "Json");
            Assert.IsTrue((Char?)resNullableChar == 'J');
        }
    }
}