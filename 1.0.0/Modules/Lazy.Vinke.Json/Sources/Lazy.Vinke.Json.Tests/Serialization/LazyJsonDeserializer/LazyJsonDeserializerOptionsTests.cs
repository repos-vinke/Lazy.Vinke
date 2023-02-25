// LazyJsonDeserializerOptionsTests.cs
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
    public class LazyJsonDeserializerOptionsTests
    {
        [TestMethod]
        public void TestDeserializerOptions()
        {
            // Arrange
            LazyJsonDeserializerOptionsBase deserializerOptionsBase = null;
            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            deserializerOptionsBase = deserializerOptions.Item<LazyJsonDeserializerOptionsBase>();

            // Assert
            Assert.IsTrue(deserializerOptions.Contains<LazyJsonDeserializerOptionsBase>());
            Assert.IsNotNull(deserializerOptionsBase);
        }
    }
}