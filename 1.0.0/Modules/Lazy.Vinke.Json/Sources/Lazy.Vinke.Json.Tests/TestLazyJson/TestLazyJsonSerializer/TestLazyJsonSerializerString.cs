// TestLazyJsonSerializerString.cs
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
    public class TestLazyJsonSerializerString
    {
        [TestMethod]
        public void TestSerializerStringDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerString serializerString = new LazyJsonSerializerString();

            // Act
            resToken = serializerString.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == null);
        }

        [TestMethod]
        public void TestSerializerStringDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = new Char[] { };
            LazyJsonSerializerString serializerString = new LazyJsonSerializerString();

            // Act
            resToken = serializerString.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerChar()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Char data = 'J';
            LazyJsonSerializerString serializerString = new LazyJsonSerializerString();

            // Act
            resToken = serializerString.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == "J");
        }

        [TestMethod]
        public void TestSerializerCharNullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Char? data = 'T';
            LazyJsonSerializerString serializerString = new LazyJsonSerializerString();

            // Act
            resToken = serializerString.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == "T");
        }

        [TestMethod]
        public void TestSerializerString()
        {
            // Arrange
            LazyJsonToken resToken = null;
            String data = "Json Tests";
            LazyJsonSerializerString serializerString = new LazyJsonSerializerString();

            // Act
            resToken = serializerString.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == "Json Tests");
        }
    }
}