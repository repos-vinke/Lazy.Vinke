// LazyJsonDeserializerDecimalTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 23

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerDecimalTests
    {
        [TestMethod]
        public void TestDeserializerDecimalPropertyNull()
        {
            // Arrange
            Object resDecimal = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal));

            // Assert
            Assert.IsNull(resDecimal);
        }

        [TestMethod]
        public void TestDeserializerDecimalTokenNull()
        {
            // Arrange
            Object resDecimal = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resDecimal = deserializerDecimal.Deserialize(jsonToken, typeof(Decimal));

            // Assert
            Assert.IsNull(resDecimal);
        }

        [TestMethod]
        public void TestDeserializerDecimalDataTypeNull()
        {
            // Arrange
            Object resDecimal = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(123.45m));
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resDecimal = deserializerDecimal.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resDecimal);
        }

        [TestMethod]
        public void TestDeserializerDecimalTokenTypeNull()
        {
            // Arrange
            Object resFloat = null;
            Object resDouble = null;
            Object resDecimal = null;
            Object resObject = null;
            Object resNullableFloat = null;
            Object resNullableDouble = null;
            Object resNullableDecimal = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonNull());
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float));
            resDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double));
            resDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal));
            resObject = deserializerDecimal.Deserialize(jsonProperty, typeof(Object));
            resNullableFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float?));
            resNullableDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double?));
            resNullableDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal?));

            // Assert
            Assert.IsTrue((float)resFloat == 0.0f);
            Assert.IsTrue((Double)resDouble == 0.0d);
            Assert.IsTrue((Decimal)resDecimal == 0.0m);
            Assert.IsTrue((Decimal)resObject == 0.0m);
            Assert.IsNull(resNullableFloat);
            Assert.IsNull(resNullableDouble);
            Assert.IsNull(resNullableDecimal);
        }

        [TestMethod]
        public void TestDeserializerDecimalValueNull()
        {
            // Arrange
            Object resFloat = null;
            Object resDouble = null;
            Object resDecimal = null;
            Object resObject = null;
            Object resNullableFloat = null;
            Object resNullableDouble = null;
            Object resNullableDecimal = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(null));
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float));
            resDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double));
            resDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal));
            resObject = deserializerDecimal.Deserialize(jsonProperty, typeof(Object));
            resNullableFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float?));
            resNullableDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double?));
            resNullableDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal?));

            // Assert
            Assert.IsTrue((float)resFloat == 0.0f);
            Assert.IsTrue((Double)resDouble == 0.0d);
            Assert.IsTrue((Decimal)resDecimal == 0.0m);
            Assert.IsTrue((Decimal)resObject == 0.0m);
            Assert.IsNull(resNullableFloat);
            Assert.IsNull(resNullableDouble);
            Assert.IsNull(resNullableDecimal);
        }

        [TestMethod]
        public void TestDeserializerDecimal()
        {
            // Arrange
            Object resFloat = null;
            Object resDouble = null;
            Object resDecimal = null;
            Object resObject = null;
            Object resNullableFloat = null;
            Object resNullableDouble = null;
            Object resNullableDecimal = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(123.45m));
            LazyJsonDeserializerDecimal deserializerDecimal = new LazyJsonDeserializerDecimal();

            // Act
            resFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float));
            resDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double));
            resDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal));
            resObject = deserializerDecimal.Deserialize(jsonProperty, typeof(Object));
            resNullableFloat = deserializerDecimal.Deserialize(jsonProperty, typeof(float?));
            resNullableDouble = deserializerDecimal.Deserialize(jsonProperty, typeof(Double?));
            resNullableDecimal = deserializerDecimal.Deserialize(jsonProperty, typeof(Decimal?));

            // Assert
            Assert.IsTrue((float)resFloat == 123.45f);
            Assert.IsTrue((Double)resDouble == 123.45d);
            Assert.IsTrue((Decimal)resDecimal == 123.45m);
            Assert.IsTrue((Decimal)resObject == 123.45m);
            Assert.IsTrue((float)resNullableFloat == 123.45f);
            Assert.IsTrue((Double)resNullableDouble == 123.45d);
            Assert.IsTrue((Decimal)resNullableDecimal == 123.45m);
        }
    }
}