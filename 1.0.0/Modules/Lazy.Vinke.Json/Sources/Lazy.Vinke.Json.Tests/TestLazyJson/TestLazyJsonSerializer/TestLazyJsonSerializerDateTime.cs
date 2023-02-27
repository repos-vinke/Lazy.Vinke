// TestLazyJsonSerializerDateTime.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class TestLazyJsonSerializerDateTime
    {
        [TestMethod]
        public void TestSerializerDateTimeDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerDateTime serializerDateTime = new LazyJsonSerializerDateTime();

            // Act
            resToken = serializerDateTime.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == null);
        }

        [TestMethod]
        public void TestSerializerDateTimeDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = 10;
            LazyJsonSerializerDateTime serializerDateTime = new LazyJsonSerializerDateTime();

            // Act
            resToken = serializerDateTime.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDateTimeWithoutSerializationOptions()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DateTime data = DateTime.Now;
            LazyJsonSerializerDateTime serializerDateTime = new LazyJsonSerializerDateTime();

            // Act
            resToken = serializerDateTime.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == data.ToString());
        }

        [TestMethod]
        public void TestSerializerDateTimeWithSerializationOptions()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DateTime data = DateTime.Now;
            LazyJsonSerializerDateTime serializerDateTime = new LazyJsonSerializerDateTime();

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = Globals.StringFormat.DateTime.ISO8601Z;

            // Act
            resToken = serializerDateTime.Serialize(data, serializerOptions);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == data.ToString(Globals.StringFormat.DateTime.ISO8601Z));
        }

        [TestMethod]
        public void TestSerializerDateTimeNullableWithSerializationOptions()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DateTime? data = DateTime.Now;
            LazyJsonSerializerDateTime serializerDateTime = new LazyJsonSerializerDateTime();

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = Globals.StringFormat.DateTime.ISO8601Z;

            // Act
            resToken = serializerDateTime.Serialize(data, serializerOptions);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)resToken).Value == ((DateTime)data).ToString(Globals.StringFormat.DateTime.ISO8601Z));
        }
    }
}