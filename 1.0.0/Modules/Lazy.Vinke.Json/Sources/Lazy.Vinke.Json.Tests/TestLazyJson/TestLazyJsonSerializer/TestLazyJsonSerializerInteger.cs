// TestLazyJsonSerializerInteger.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 25

using System;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class TestLazyJsonSerializerInteger
    {
        [TestMethod]
        public void TestSerializerIntegerDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == null);
        }

        [TestMethod]
        public void TestSerializerIntegerDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = true;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerInt16()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int16 data = 1;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 1);
        }

        [TestMethod]
        public void TestSerializerInt16Nullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int16? data = 11;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 11);
        }

        [TestMethod]
        public void TestSerializerInt32()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int32 data = 100;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 100);
        }

        [TestMethod]
        public void TestSerializerInt32Nullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int32? data = 1001;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 1001);
        }

        [TestMethod]
        public void TestSerializerInt64()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int64 data = 50005;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 50005);
        }

        [TestMethod]
        public void TestSerializerInt64Nullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int64? data = 8086;
            LazyJsonSerializerInteger serializerInteger = new LazyJsonSerializerInteger();

            // Act
            resToken = serializerInteger.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)resToken).Value == 8086);
        }
    }
}