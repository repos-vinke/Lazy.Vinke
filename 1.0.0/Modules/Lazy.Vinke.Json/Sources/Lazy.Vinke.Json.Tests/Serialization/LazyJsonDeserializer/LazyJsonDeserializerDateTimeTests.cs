// LazyJsonDeserializerDateTimeTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerDateTimeTests
    {
        [TestMethod]
        public void TestDeserializerDateTimePropertyNull()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeTokenNull()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonToken, typeof(DateTime));

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeDataTypeNull()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString(DateTime.Now.ToString()));
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeTokenTypeNull()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonNull());
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeValueNull()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString(null));
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeNotIso()
        {
            // Arrange
            Object resDateTime = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString(DateTime.Now.ToString()));
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            resDateTime = deserializerDateTime.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resDateTime);
        }

        [TestMethod]
        public void TestDeserializerDateTimeIso()
        {
            // Arrange
            DateTime dateTime = DateTime.Now;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonString(dateTime.ToString("yyyy-MM-ddTHH:mm:ss:fffZ")));
            LazyJsonDeserializerDateTime deserializerDateTime = new LazyJsonDeserializerDateTime();

            // Act
            DateTime resDateTime = (DateTime)deserializerDateTime.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsTrue(resDateTime.Year == dateTime.Year);
            Assert.IsTrue(resDateTime.Month == dateTime.Month);
            Assert.IsTrue(resDateTime.Day == dateTime.Day);
            Assert.IsTrue(resDateTime.Hour == dateTime.Hour);
            Assert.IsTrue(resDateTime.Minute == dateTime.Minute);
            Assert.IsTrue(resDateTime.Second == dateTime.Second);
            Assert.IsTrue(resDateTime.Millisecond == dateTime.Millisecond);
        }
    }
}