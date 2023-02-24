// LazyJsonDeserializerTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerTests
    {
        [TestMethod]
        public void TestDeserializerSampleSimpleInteger()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleInteger sampleSimpleInteger = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleInteger);

            // Act
            sampleSimpleInteger = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleInteger>(json);

            // Assert
            Assert.IsTrue(sampleSimpleInteger.Int16Value == 1);
            Assert.IsNull(sampleSimpleInteger.Int16ValueNullableNull);
            Assert.IsTrue(sampleSimpleInteger.Int16ValueNullableNotNull == 11);
            Assert.IsTrue(sampleSimpleInteger.Int32Value == 2);
            Assert.IsNull(sampleSimpleInteger.Int32ValueNullableNull);
            Assert.IsTrue(sampleSimpleInteger.Int32ValueNullableNotNull == 22);
            Assert.IsTrue(sampleSimpleInteger.Int64Value == 3);
            Assert.IsNull(sampleSimpleInteger.Int64ValueNullableNull);
            Assert.IsTrue(sampleSimpleInteger.Int64ValueNullableNotNull == 33);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleFloating()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleFloating sampleSimpleFloating = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleFloating);

            // Act
            sampleSimpleFloating = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleFloating>(json);

            // Assert
            Assert.IsTrue(sampleSimpleFloating.FloatValue == 1.2f);
            Assert.IsNull(sampleSimpleFloating.FloatValueNullableNull);
            Assert.IsTrue(sampleSimpleFloating.FloatValueNullableNotNull == 11.23f);
            Assert.IsTrue(sampleSimpleFloating.DoubleValue == 23.45d);
            Assert.IsNull(sampleSimpleFloating.DoubleValueNullableNull);
            Assert.IsTrue(sampleSimpleFloating.DoubleValueNullableNotNull == 234.453d);
            Assert.IsTrue(sampleSimpleFloating.DecimalValue == 567.891m);
            Assert.IsNull(sampleSimpleFloating.DecimalValueNullableNull);
            Assert.IsTrue(sampleSimpleFloating.DecimalValueNullableNotNull == 5672.8913m);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleString()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleString sampleSimpleString = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleString);

            // Act
            sampleSimpleString = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleString>(json);

            // Assert
            Assert.IsTrue(sampleSimpleString.CharValue == 'A');
            Assert.IsTrue(sampleSimpleString.CharValueNull == '\0');
            Assert.IsNull(sampleSimpleString.CharValueNullableNull);
            Assert.IsTrue(sampleSimpleString.CharValueNullableNotNull == 'B');
            Assert.IsTrue(sampleSimpleString.StringValue == "Json");
            Assert.IsNull(sampleSimpleString.StringValueNullableNull);
            Assert.IsTrue(sampleSimpleString.StringValueWhiteSpace == " ");
            Assert.IsTrue(sampleSimpleString.StringValueEmpty == String.Empty);
        }
    }
}