// LazyJsonWriterTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 27

using System;
using System.Text;
using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonWriterTests
    {
        [TestMethod]
        public void TestWriterSample2MB()
        {
            // Arrange
            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonWriterSample2MB);

            LazyJsonWriterOptions writerOptions = new LazyJsonWriterOptions();
            writerOptions.IndentObjectEmpty = false;
            writerOptions.IndentArrayEmpty = false;

            // Act
            String json = LazyJsonWriter.Write(LazyJsonReader.Read(originalJson), writerOptions);

            // Assert
            Assert.IsTrue(json == originalJson);
        }
    }
}