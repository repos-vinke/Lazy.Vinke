// TestLazyJsonSerializerOptions.cs
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
    public class TestLazyJsonSerializerOptions
    {
        [TestMethod]
        public void TestSerializerOptions()
        {
            // Arrange
            LazyJsonSerializerOptionsBase serializerOptionsBase = null;
            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();

            // Act
            serializerOptionsBase = serializerOptions.Item<LazyJsonSerializerOptionsBase>();

            // Assert
            Assert.IsTrue(serializerOptions.Contains<LazyJsonSerializerOptionsBase>());
            Assert.IsNotNull(serializerOptionsBase);
        }
    }
}