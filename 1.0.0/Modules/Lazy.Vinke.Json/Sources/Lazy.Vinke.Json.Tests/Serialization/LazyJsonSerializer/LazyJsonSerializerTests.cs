// LazyJsonSerializerTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 25

using System;
using System.Collections.Generic;
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
        public void TestSerializerSampleSimpleInteger()
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
        public void TestSerializerSampleSimpleString()
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

        [TestMethod]
        public void TestSerializerSampleSimpleList()
        {
            // Arrange
            LazyJsonSampleSimpleList sampleSimpleList = new LazyJsonSampleSimpleList();

            sampleSimpleList.ListBooleanValueSingle = new List<Boolean>() { false };
            sampleSimpleList.ListBooleanValueMultiple = new List<Boolean>() { true, false, false, true, true, false, false, true, true };
            sampleSimpleList.ListBooleanValueEmpty = new List<Boolean>() { };
            sampleSimpleList.ListBooleanValueNull = null;
            sampleSimpleList.ListDecimalValueSingle = new List<Decimal>() { 0.0m };
            sampleSimpleList.ListDecimalValueMultiple = new List<Decimal>() { 1.1m, 2.2m, 3.3m, 4.4m, 5.5m, 6.6m, 7.7m, 8.8m, 9.9m };
            sampleSimpleList.ListDecimalValueEmpty = new List<Decimal>() { };
            sampleSimpleList.ListDecimalValueNull = null;
            sampleSimpleList.ListInt16ValueSingle = new List<Int16>() { 0 };
            sampleSimpleList.ListInt16ValueMultiple = new List<Int16>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            sampleSimpleList.ListInt16ValueEmpty = new List<Int16>() { };
            sampleSimpleList.ListInt16ValueNull = null;
            sampleSimpleList.ListInt64ValueSingle = new List<Int64>() { 0 };
            sampleSimpleList.ListInt64ValueMultiple = new List<Int64>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            sampleSimpleList.ListInt64ValueEmpty = new List<Int64>() { };
            sampleSimpleList.ListInt64ValueNull = null;
            sampleSimpleList.ListStringValueSingle = new List<String>() { "Zero" };
            sampleSimpleList.ListStringValueMultiple = new List<String>() { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            sampleSimpleList.ListStringValueEmpty = new List<String>() { };
            sampleSimpleList.ListStringValueNull = null;
            sampleSimpleList.ListCharValueSingle = new List<Char>() { 'Z' };
            sampleSimpleList.ListCharValueMultiple = new List<Char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
            sampleSimpleList.ListCharValueEmpty = new List<Char>() { };
            sampleSimpleList.ListCharValueNull = null;
            sampleSimpleList.ListObjectValueSingle = new List<Object>() { "A" };
            sampleSimpleList.ListObjectValueMultiple = new List<Object>() { true, "B", "Json", 1.1, 256, 23.456, 0, "Tests", false };
            sampleSimpleList.ListObjectValueEmpty = new List<Object>() { };
            sampleSimpleList.ListObjectValueNull = null;

            LazyJsonWriterOptions writerOptions = new LazyJsonWriterOptions() { Indent = false };

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleList);
            originalJson = LazyJsonWriter.Write(LazyJsonReader.Read(originalJson), jsonWriterOptions: writerOptions);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleList, jsonWriterOptions: writerOptions);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDictionary()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleDictionary sampleSimpleDictionary = new LazyJsonSerializerSampleSimpleDictionary();

            sampleSimpleDictionary.DicInt16StringValidSingle = new Dictionary<Int16, String>() { { 1, "Json" } };
            sampleSimpleDictionary.DicInt16StringValidMultiple = new Dictionary<Int16, String>() { { 1, "Json" }, { 2, "Tests" } };
            sampleSimpleDictionary.DicInt16StringEmpty = new Dictionary<Int16, String>() { };
            sampleSimpleDictionary.DicInt16StringNull = null;

            sampleSimpleDictionary.DicStringDecimalValidSingle = new Dictionary<String, Decimal>() { { "Item1", 12.345m } };
            sampleSimpleDictionary.DicStringDecimalValidMultiple = new Dictionary<String, Decimal>() { { "Item2", 34.567m }, { "Item3", 45.789m } };
            sampleSimpleDictionary.DicStringDecimalEmpty = new Dictionary<String, Decimal>() { };
            sampleSimpleDictionary.DicStringDecimalNull = null;

            sampleSimpleDictionary.DicInt64Int16ValidSingle = new Dictionary<Int64, Int16>() { { 10, 100 } };
            sampleSimpleDictionary.DicInt64Int16ValidMultiple = new Dictionary<Int64, Int16>() { { 20, 200 }, { 30, 300 } };
            sampleSimpleDictionary.DicInt64Int16Empty = new Dictionary<Int64, Int16>() { };
            sampleSimpleDictionary.DicInt64Int16Null = null;

            LazyJsonWriterOptions writerOptions = new LazyJsonWriterOptions() { Indent = false };

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDictionary);
            originalJson = LazyJsonWriter.Write(LazyJsonReader.Read(originalJson), jsonWriterOptions: writerOptions);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDictionary, jsonWriterOptions: writerOptions);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

    }
}