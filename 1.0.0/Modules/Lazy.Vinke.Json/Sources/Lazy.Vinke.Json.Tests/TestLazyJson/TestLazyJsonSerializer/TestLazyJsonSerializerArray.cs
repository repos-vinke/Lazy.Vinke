// TestLazyJsonSerializerArray.cs
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
    public class TestLazyJsonSerializerArray
    {
        [TestMethod]
        public void TestSerializerArrayDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerArrayDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = "Tests";
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerArrayEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Decimal[] data = new Decimal[] { };
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 0);
        }

        [TestMethod]
        public void TestSerializerArraySingleItem()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Int16[] data = new Int16[] { 11 };
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 1);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)resToken)[0]).Value == 11);
        }

        [TestMethod]
        public void TestSerializerArrayMultipleItems()
        {
            // Arrange
            LazyJsonToken resToken = null;
            String[] data = new String[] { "Json", "Test", "Cases" };
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 3);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[0]).Value == "Json");
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[1]).Value == "Test");
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[2]).Value == "Cases");
        }

        [TestMethod]
        public void TestSerializerArrayMultipleObjects()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object[] data = new Object[] { 1, "Test", 12.4f, false };
            LazyJsonSerializerArray serializerArray = new LazyJsonSerializerArray();

            // Act
            resToken = serializerArray.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 4);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonArray)resToken)[3].Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)resToken)[0]).Value == 1);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[1]).Value == "Test");
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)resToken)[2]).Value == 12.4m);
            Assert.IsTrue(((LazyJsonBoolean)((LazyJsonArray)resToken)[3]).Value == false);
        }
    }
}