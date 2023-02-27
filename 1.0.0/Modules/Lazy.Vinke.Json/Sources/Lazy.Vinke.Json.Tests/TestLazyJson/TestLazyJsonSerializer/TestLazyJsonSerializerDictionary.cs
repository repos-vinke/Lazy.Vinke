// TestLazyJsonSerializerDictionary.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 25

using System;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class TestLazyJsonSerializerDictionary
    {
        [TestMethod]
        public void TestSerializerDictionaryDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDictionaryDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = new Byte();
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDictionaryEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Dictionary<String, Decimal> data = new Dictionary<String, Decimal>();
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 0);
        }

        [TestMethod]
        public void TestSerializerDictionarySingleItem()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Dictionary<String, Decimal> data = new Dictionary<String, Decimal>() { { "Item1", 12.4m } };
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 1);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[0].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[1].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)resToken)[0])[0]).Value == "Item1");
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)((LazyJsonArray)resToken)[0])[1]).Value == 12.4m);
        }

        [TestMethod]
        public void TestSerializerDictionaryMultipleItems()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Dictionary<Int16, String> data = new Dictionary<Int16, String>() { { 1, "Item1" }, { 2, "Item2" }, { 3, "Item3" } };
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 3);

            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[0])[0]).Value == 1);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)resToken)[0])[1]).Value == "Item1");

            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1])[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[1])[0]).Value == 2);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)resToken)[1])[1]).Value == "Item2");

            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2])[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[2])[0]).Value == 3);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)resToken)[2])[1]).Value == "Item3");
        }

        [TestMethod]
        public void TestSerializerDictionaryMultipleObjects()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Dictionary<Int64, Object> data = new Dictionary<Int64, Object>() { { 100, false }, { 200, null }, { 300, 123.456d }, { 400, "Item4" } };
            LazyJsonSerializerDictionary serializerDictionary = new LazyJsonSerializerDictionary();

            // Act
            resToken = serializerDictionary.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 4);

            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[0])[1].Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[0])[0]).Value == 100);
            Assert.IsTrue(((LazyJsonBoolean)((LazyJsonArray)((LazyJsonArray)resToken)[0])[1]).Value == false);

            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[1])[1].Type == LazyJsonType.Null);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[1])[0]).Value == 200);

            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[2])[1].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[2])[0]).Value == 300);
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)((LazyJsonArray)resToken)[2])[1]).Value == 123.456m);

            Assert.IsTrue(((LazyJsonArray)resToken)[3].Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[3]).Count == 2);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[3])[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonArray)resToken)[3])[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)resToken)[3])[0]).Value == 400);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)resToken)[3])[1]).Value == "Item4");
        }
    }
}