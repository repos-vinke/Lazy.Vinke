// LazyJsonDeserializerTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using System.Text;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerTests
    {
        [TestMethod]
        public void TestDeserializerSampleSimpleBoolean()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleBoolean sampleSimpleBoolean = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleBoolean);

            // Act
            sampleSimpleBoolean = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleBoolean>(json);

            // Assert
            Assert.IsTrue(sampleSimpleBoolean.BooleanValue == true);
            Assert.IsNull(sampleSimpleBoolean.BooleanValueNullableNull);
            Assert.IsTrue(sampleSimpleBoolean.BooleanValueNullableNotNull == false);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDecimal()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleDecimal sampleSimpleDecimal = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleDecimal);

            // Act
            sampleSimpleDecimal = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleDecimal>(json);

            // Assert
            Assert.IsTrue(sampleSimpleDecimal.FloatValue == 1.2f);
            Assert.IsNull(sampleSimpleDecimal.FloatValueNullableNull);
            Assert.IsTrue(sampleSimpleDecimal.FloatValueNullableNotNull == 11.23f);
            Assert.IsTrue(sampleSimpleDecimal.DoubleValue == 23.45d);
            Assert.IsNull(sampleSimpleDecimal.DoubleValueNullableNull);
            Assert.IsTrue(sampleSimpleDecimal.DoubleValueNullableNotNull == 234.453d);
            Assert.IsTrue(sampleSimpleDecimal.DecimalValue == 567.891m);
            Assert.IsNull(sampleSimpleDecimal.DecimalValueNullableNull);
            Assert.IsTrue(sampleSimpleDecimal.DecimalValueNullableNotNull == 5672.8913m);
        }

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

        [TestMethod]
        public void TestDeserializerSampleSimpleList()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleList sampleSimpleList = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleList);

            // Act
            sampleSimpleList = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleList>(json);

            // Assert
            Assert.IsTrue(sampleSimpleList.ListBooleanValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueSingle[0] == false);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueMultiple[0] == true);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueMultiple[5] == false);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueMultiple[8] == true);
            Assert.IsTrue(sampleSimpleList.ListBooleanValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListBooleanValueNull);

            Assert.IsTrue(sampleSimpleList.ListDecimalValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueSingle[0] == 0.0m);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueMultiple[0] == 1.1m);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueMultiple[3] == 4.4m);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueMultiple[6] == 7.7m);
            Assert.IsTrue(sampleSimpleList.ListDecimalValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListDecimalValueNull);

            Assert.IsTrue(sampleSimpleList.ListInt16ValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueSingle[0] == 0);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueMultiple[2] == 3);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueMultiple[5] == 6);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueMultiple[8] == 9);
            Assert.IsTrue(sampleSimpleList.ListInt16ValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListInt16ValueNull);

            Assert.IsTrue(sampleSimpleList.ListInt64ValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueSingle[0] == 0);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueMultiple[1] == 2);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueMultiple[4] == 5);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueMultiple[7] == 8);
            Assert.IsTrue(sampleSimpleList.ListInt64ValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListInt64ValueNull);

            Assert.IsTrue(sampleSimpleList.ListStringValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListStringValueSingle[0] == "Zero");
            Assert.IsTrue(sampleSimpleList.ListStringValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListStringValueMultiple[1] == "Two");
            Assert.IsTrue(sampleSimpleList.ListStringValueMultiple[4] == "Five");
            Assert.IsTrue(sampleSimpleList.ListStringValueMultiple[7] == "Eight");
            Assert.IsTrue(sampleSimpleList.ListStringValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListStringValueNull);

            Assert.IsTrue(sampleSimpleList.ListCharValueSingle.Count == 1);
            Assert.IsTrue(sampleSimpleList.ListCharValueSingle[0] == 'Z');
            Assert.IsTrue(sampleSimpleList.ListCharValueMultiple.Count == 9);
            Assert.IsTrue(sampleSimpleList.ListCharValueMultiple[2] == 'C');
            Assert.IsTrue(sampleSimpleList.ListCharValueMultiple[5] == 'F');
            Assert.IsTrue(sampleSimpleList.ListCharValueMultiple[8] == 'I');
            Assert.IsTrue(sampleSimpleList.ListCharValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListCharValueNull);

            Assert.IsTrue(sampleSimpleList.ListObjectValueSingle.Count == 1);
            Assert.IsTrue((String)sampleSimpleList.ListObjectValueSingle[0] == "A");
            Assert.IsTrue(sampleSimpleList.ListObjectValueMultiple.Count == 9);
            Assert.IsTrue((Boolean)sampleSimpleList.ListObjectValueMultiple[0] == true);
            Assert.IsTrue((String)sampleSimpleList.ListObjectValueMultiple[1] == "B");
            Assert.IsTrue((String)sampleSimpleList.ListObjectValueMultiple[2] == "Json");
            Assert.IsTrue((Decimal)sampleSimpleList.ListObjectValueMultiple[3] == 1.1m);
            Assert.IsTrue((Int64)sampleSimpleList.ListObjectValueMultiple[4] == 256);
            Assert.IsTrue((Decimal)sampleSimpleList.ListObjectValueMultiple[5] == 23.456m);
            Assert.IsTrue((Int64)sampleSimpleList.ListObjectValueMultiple[6] == 0);
            Assert.IsTrue((String)sampleSimpleList.ListObjectValueMultiple[7] == "Tests");
            Assert.IsTrue((Boolean)sampleSimpleList.ListObjectValueMultiple[8] == false);
            Assert.IsTrue(sampleSimpleList.ListObjectValueEmpty.Count == 0);
            Assert.IsNull(sampleSimpleList.ListObjectValueNull);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDictionary()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleDictionary sampleSimpleDictionary = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleDictionary);

            // Act
            sampleSimpleDictionary = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleDictionary>(json);

            // Assert
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringInvalidSingle.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringInvalidMultiple.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringValidSingle.Count == 1);
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringValidSingle[1] == "Json");
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringValidMultiple.Count == 2);
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringValidMultiple[1] == "Json");
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringValidMultiple[2] == "Tests");
            Assert.IsTrue(sampleSimpleDictionary.DicInt16StringEmpty.Count == 0);
            Assert.IsNull(sampleSimpleDictionary.DicInt16StringNull);

            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalInvalidSingle.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalInvalidMultiple.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalValidSingle.Count == 1);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalValidSingle["Item1"] == 12.345m);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalValidMultiple.Count == 2);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalValidMultiple["Item2"] == 34.567m);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalValidMultiple["Item3"] == 45.789m);
            Assert.IsTrue(sampleSimpleDictionary.DicStringDecimalEmpty.Count == 0);
            Assert.IsNull(sampleSimpleDictionary.DicStringDecimalNull);

            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16InvalidSingle.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16InvalidMultiple.Count == 0);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16ValidSingle.Count == 1);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16ValidSingle[10] == 100);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16ValidMultiple.Count == 2);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16ValidMultiple[20] == 200);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16ValidMultiple[30] == 300);
            Assert.IsTrue(sampleSimpleDictionary.DicInt64Int16Empty.Count == 0);
            Assert.IsNull(sampleSimpleDictionary.DicInt64Int16Null);
        }
    }
}