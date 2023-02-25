// LazyJsonSerializerTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 25

using System;
using System.Data;
using System.Text;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonSerializerTests
    {
        [TestMethod]
        public void TestSerializerSampleSimpleBoolean()
        {
            // Arrange
            LazyJsonSampleSimpleBoolean sampleSimpleBoolean = new LazyJsonSampleSimpleBoolean();
            sampleSimpleBoolean.BooleanValue = true;
            sampleSimpleBoolean.BooleanValueNullableNull = null;
            sampleSimpleBoolean.BooleanValueNullableNotNull = false;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleBoolean);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleBoolean);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDecimal()
        {
            // Arrange
            LazyJsonSampleSimpleDecimal sampleSimpleDecimal = new LazyJsonSampleSimpleDecimal();
            sampleSimpleDecimal.FloatValue = 1.2f;
            sampleSimpleDecimal.FloatValueNullableNull = null;
            sampleSimpleDecimal.FloatValueNullableNotNull = 11.23f;
            sampleSimpleDecimal.DoubleValue = 23.45d;
            sampleSimpleDecimal.DoubleValueNullableNull = null;
            sampleSimpleDecimal.DoubleValueNullableNotNull = 234.453d;
            sampleSimpleDecimal.DecimalValue = 567.891m;
            sampleSimpleDecimal.DecimalValueNullableNull = null;
            sampleSimpleDecimal.DecimalValueNullableNotNull = 5672.8913m;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleDecimal);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDecimal);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleInteger()
        {
            // Arrange
            LazyJsonSampleSimpleInteger sampleSimpleInteger = new LazyJsonSampleSimpleInteger();
            sampleSimpleInteger.Int16Value = 1;
            sampleSimpleInteger.Int16ValueNullableNull = null;
            sampleSimpleInteger.Int16ValueNullableNotNull = 11;
            sampleSimpleInteger.Int32Value = 2;
            sampleSimpleInteger.Int32ValueNullableNull = null;
            sampleSimpleInteger.Int32ValueNullableNotNull = 22;
            sampleSimpleInteger.Int64Value = 3;
            sampleSimpleInteger.Int64ValueNullableNull = null;
            sampleSimpleInteger.Int64ValueNullableNotNull = 33;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleInteger);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleInteger);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleString()
        {
            // Arrange
            LazyJsonSampleSimpleString sampleSimpleString = new LazyJsonSampleSimpleString();
            sampleSimpleString.CharValue = 'A';
            sampleSimpleString.CharValueNull = '\0';
            sampleSimpleString.CharValueNullableNull = null;
            sampleSimpleString.CharValueNullableNotNull = 'B';
            sampleSimpleString.StringValue = "Json";
            sampleSimpleString.StringValueNullableNull = null;
            sampleSimpleString.StringValueWhiteSpace = " ";
            sampleSimpleString.StringValueEmpty = String.Empty;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleString);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleString);

            // Assert
            Assert.IsTrue(json == originalJson);
        }
    }
}