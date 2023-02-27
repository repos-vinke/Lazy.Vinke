// TestLazyJsonSerializerList.cs
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
    public class TestLazyJsonSerializerList
    {
        [TestMethod]
        public void TestSerializerListDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerListDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = new Object[] { };
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerListEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            List<Decimal> data = new List<Decimal>();
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 0);
        }

        [TestMethod]
        public void TestSerializerListSingleItem()
        {
            // Arrange
            LazyJsonToken resToken = null;
            List<String> data = new List<String>() { "Item1" };
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 1);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[0]).Value == "Item1");
        }

        [TestMethod]
        public void TestSerializerListMultipleItems()
        {
            // Arrange
            LazyJsonToken resToken = null;
            List<float> data = new List<float>() { 10.4f, 143.45f, 1123.543f };
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 3);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)resToken)[0]).Value == 10.4m);
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)resToken)[1]).Value == 143.45m);
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)resToken)[2]).Value == 1123.543m);
        }

        [TestMethod]
        public void TestSerializerListMultipleObjects()
        {
            // Arrange
            LazyJsonToken resToken = null;
            List<Object> data = new List<Object>() { 12, "Json", 12.4f, true };
            LazyJsonSerializerList serializerList = new LazyJsonSerializerList();

            // Act
            resToken = serializerList.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)resToken).Count == 4);
            Assert.IsTrue(((LazyJsonArray)resToken)[0].Type == LazyJsonType.Integer);
            Assert.IsTrue(((LazyJsonArray)resToken)[1].Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonArray)resToken)[2].Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonArray)resToken)[3].Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonInteger)((LazyJsonArray)resToken)[0]).Value == 12);
            Assert.IsTrue(((LazyJsonString)((LazyJsonArray)resToken)[1]).Value == "Json");
            Assert.IsTrue(((LazyJsonDecimal)((LazyJsonArray)resToken)[2]).Value == 12.4m);
            Assert.IsTrue(((LazyJsonBoolean)((LazyJsonArray)resToken)[3]).Value == true);
        }
    }
}