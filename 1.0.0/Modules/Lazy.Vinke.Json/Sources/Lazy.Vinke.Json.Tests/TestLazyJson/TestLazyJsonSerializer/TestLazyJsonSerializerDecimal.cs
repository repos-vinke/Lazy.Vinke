// TestLazyJsonSerializerDecimal.cs
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
    public class TestLazyJsonSerializerDecimal
    {
        [TestMethod]
        public void TestSerializerDecimalDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == null);
        }

        [TestMethod]
        public void TestSerializerDecimalDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = new Object[] { 10 };
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerFloat()
        {
            // Arrange
            LazyJsonToken resToken = null;
            float data = 123.45f;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 123.45m);
        }

        [TestMethod]
        public void TestSerializerFloatNullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            float? data = 9876.543f;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 9876.543m);
        }

        [TestMethod]
        public void TestSerializerDouble()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Double data = 83753.4789d;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 83753.4789m);
        }

        [TestMethod]
        public void TestSerializerDoubleNullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Double? data = 1.5d;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 1.5m);
        }

        [TestMethod]
        public void TestSerializerDecimal()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Decimal data = 10.987m;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 10.987m);
        }

        [TestMethod]
        public void TestSerializerDecimalNullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Decimal? data = 510.98m;
            LazyJsonSerializerDecimal serializerDecimal = new LazyJsonSerializerDecimal();

            // Act
            resToken = serializerDecimal.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)resToken).Value == 510.98m);
        }
    }
}