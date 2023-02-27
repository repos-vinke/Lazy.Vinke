// TestLazyJsonDeserializerInteger.cs
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
    public class TestLazyJsonDeserializerInteger
    {
        [TestMethod]
        public void TestDeserializerIntegerPropertyNull()
        {
            // Arrange
            Object resInteger = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInteger = deserializerInteger.Deserialize(jsonProperty, typeof(Int32));

            // Assert
            Assert.IsNull(resInteger);
        }

        [TestMethod]
        public void TestDeserializerIntegerTokenNull()
        {
            // Arrange
            Object resInteger = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInteger = deserializerInteger.Deserialize(jsonToken, typeof(Int32));

            // Assert
            Assert.IsNull(resInteger);
        }

        [TestMethod]
        public void TestDeserializerIntegerDataTypeNull()
        {
            // Arrange
            Object resInteger = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonInteger(123));
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInteger = deserializerInteger.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resInteger);
        }

        [TestMethod]
        public void TestDeserializerIntegerTokenTypeNull()
        {
            // Arrange
            Object resInt16 = null;
            Object resInt32 = null;
            Object resInt64 = null;
            Object resObject = null;
            Object resNullableInt16 = null;
            Object resNullableInt32 = null;
            Object resNullableInt64 = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonNull());
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16));
            resInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32));
            resInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64));
            resObject = deserializerInteger.Deserialize(jsonProperty, typeof(Object));
            resNullableInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16?));
            resNullableInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32?));
            resNullableInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64?));

            // Assert
            Assert.IsTrue(resInt16.Equals(0));
            Assert.IsTrue(resInt32.Equals(0));
            Assert.IsTrue(resInt64.Equals(0));
            Assert.IsTrue(resObject.Equals(0));
            Assert.IsNull(resNullableInt16);
            Assert.IsNull(resNullableInt32);
            Assert.IsNull(resNullableInt64);
        }

        [TestMethod]
        public void TestDeserializerIntegerValueNull()
        {
            // Arrange
            Object resInt16 = null;
            Object resInt32 = null;
            Object resInt64 = null;
            Object resObject = null;
            Object resNullableInt16 = null;
            Object resNullableInt32 = null;
            Object resNullableInt64 = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonInteger(null));
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16));
            resInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32));
            resInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64));
            resObject = deserializerInteger.Deserialize(jsonProperty, typeof(Object));
            resNullableInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16?));
            resNullableInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32?));
            resNullableInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64?));

            // Assert
            Assert.IsTrue(resInt16.Equals(0));
            Assert.IsTrue(resInt32.Equals(0));
            Assert.IsTrue(resInt64.Equals((Int64)0));
            Assert.IsTrue(resObject.Equals((Int64)0));
            Assert.IsNull(resNullableInt16);
            Assert.IsNull(resNullableInt32);
            Assert.IsNull(resNullableInt64);
        }

        [TestMethod]
        public void TestDeserializerInteger()
        {
            // Arrange
            Object resInt16 = null;
            Object resInt32 = null;
            Object resInt64 = null;
            Object resObject = null;
            Object resNullableInt16 = null;
            Object resNullableInt32 = null;
            Object resNullableInt64 = null;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonInteger(123));
            LazyJsonDeserializerInteger deserializerInteger = new LazyJsonDeserializerInteger();

            // Act
            resInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16));
            resInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32));
            resInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64));
            resObject = deserializerInteger.Deserialize(jsonProperty, typeof(Object));
            resNullableInt16 = deserializerInteger.Deserialize(jsonProperty, typeof(Int16?));
            resNullableInt32 = deserializerInteger.Deserialize(jsonProperty, typeof(Int32?));
            resNullableInt64 = deserializerInteger.Deserialize(jsonProperty, typeof(Int64?));

            // Assert
            Assert.IsTrue(resInt16.Equals(123));
            Assert.IsTrue(resInt32.Equals(123));
            Assert.IsTrue(resInt64.Equals((Int64)123));
            Assert.IsTrue(resObject.Equals((Int64)123));
            Assert.IsTrue(resNullableInt16.Equals((Int16?)123));
            Assert.IsTrue(resNullableInt32.Equals(123));
            Assert.IsTrue(resNullableInt64.Equals((Int64?)123));
        }
    }
}