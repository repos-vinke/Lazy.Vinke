// LazyJsonSerializerBooleanTests.cs
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
    public class LazyJsonSerializerBooleanTests
    {
        [TestMethod]
        public void TestSerializerBooleanDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerBoolean serializerBoolean = new LazyJsonSerializerBoolean();

            // Act
            resToken = serializerBoolean.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonBoolean)resToken).Value == null);
        }

        [TestMethod]
        public void TestSerializerBooleanDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = "Tests";
            LazyJsonSerializerBoolean serializerBoolean = new LazyJsonSerializerBoolean();

            // Act
            resToken = serializerBoolean.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerBoolean()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Boolean data = true;
            LazyJsonSerializerBoolean serializerBoolean = new LazyJsonSerializerBoolean();

            // Act
            resToken = serializerBoolean.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonBoolean)resToken).Value == true);
        }

        [TestMethod]
        public void TestSerializerBooleanNullable()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Boolean? data = true;
            LazyJsonSerializerBoolean serializerBoolean = new LazyJsonSerializerBoolean();

            // Act
            resToken = serializerBoolean.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonBoolean)resToken).Value == true);
        }
    }
}