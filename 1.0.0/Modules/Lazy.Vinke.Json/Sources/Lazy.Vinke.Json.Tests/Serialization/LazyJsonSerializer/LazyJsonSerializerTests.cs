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
            LazyJsonSerializerSampleSimpleBoolean sampleSimpleBoolean = new LazyJsonSerializerSampleSimpleBoolean();
            sampleSimpleBoolean.BooleanValue = true;
            sampleSimpleBoolean.BooleanValueNullableNull = null;
            sampleSimpleBoolean.BooleanValueNullableNotNull = false;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleBoolean);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleBoolean);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDecimal()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleDecimal sampleSimpleDecimal = new LazyJsonSerializerSampleSimpleDecimal();
            sampleSimpleDecimal.FloatValue = 1.2f;
            sampleSimpleDecimal.FloatValueNullableNull = null;
            sampleSimpleDecimal.FloatValueNullableNotNull = 11.23f;
            sampleSimpleDecimal.DoubleValue = 23.45d;
            sampleSimpleDecimal.DoubleValueNullableNull = null;
            sampleSimpleDecimal.DoubleValueNullableNotNull = 234.453d;
            sampleSimpleDecimal.DecimalValue = 567.891m;
            sampleSimpleDecimal.DecimalValueNullableNull = null;
            sampleSimpleDecimal.DecimalValueNullableNotNull = 5672.8913m;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDecimal);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDecimal);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleInteger()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleInteger sampleSimpleInteger = new LazyJsonSerializerSampleSimpleInteger();
            sampleSimpleInteger.Int16Value = 1;
            sampleSimpleInteger.Int16ValueNullableNull = null;
            sampleSimpleInteger.Int16ValueNullableNotNull = 11;
            sampleSimpleInteger.Int32Value = 2;
            sampleSimpleInteger.Int32ValueNullableNull = null;
            sampleSimpleInteger.Int32ValueNullableNotNull = 22;
            sampleSimpleInteger.Int64Value = 3;
            sampleSimpleInteger.Int64ValueNullableNull = null;
            sampleSimpleInteger.Int64ValueNullableNotNull = 33;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleInteger);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleInteger);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleString()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleString sampleSimpleString = new LazyJsonSerializerSampleSimpleString();
            sampleSimpleString.CharValue = 'A';
            sampleSimpleString.CharValueNull = '\0';
            sampleSimpleString.CharValueNullableNull = null;
            sampleSimpleString.CharValueNullableNotNull = 'B';
            sampleSimpleString.StringValue = "Json";
            sampleSimpleString.StringValueNullableNull = null;
            sampleSimpleString.StringValueWhiteSpace = " ";
            sampleSimpleString.StringValueEmpty = String.Empty;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleString);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleString);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleList()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleList sampleSimpleList = new LazyJsonSerializerSampleSimpleList();

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

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleList);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleList);

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

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDictionary);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDictionary);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleArray()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleArray sampleSimpleArray = new LazyJsonSerializerSampleSimpleArray();

            sampleSimpleArray.ArrayBooleanValueSingle = new Boolean[] { false };
            sampleSimpleArray.ArrayBooleanValueMultiple = new Boolean[] { true, false, false, true, true, false, false, true, true };
            sampleSimpleArray.ArrayBooleanValueEmpty = new Boolean[] { };
            sampleSimpleArray.ArrayBooleanValueNull = null;
            sampleSimpleArray.ArrayDecimalValueSingle = new Decimal[] { 0.0m };
            sampleSimpleArray.ArrayDecimalValueMultiple = new Decimal[] { 1.1m, 2.2m, 3.3m, 4.4m, 5.5m, 6.6m, 7.7m, 8.8m, 9.9m };
            sampleSimpleArray.ArrayDecimalValueEmpty = new Decimal[] { };
            sampleSimpleArray.ArrayDecimalValueNull = null;
            sampleSimpleArray.ArrayInt16ValueSingle = new Int16[] { 0 };
            sampleSimpleArray.ArrayInt16ValueMultiple = new Int16[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            sampleSimpleArray.ArrayInt16ValueEmpty = new Int16[] { };
            sampleSimpleArray.ArrayInt16ValueNull = null;
            sampleSimpleArray.ArrayInt64ValueSingle = new Int64[] { 0 };
            sampleSimpleArray.ArrayInt64ValueMultiple = new Int64[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            sampleSimpleArray.ArrayInt64ValueEmpty = new Int64[] { };
            sampleSimpleArray.ArrayInt64ValueNull = null;
            sampleSimpleArray.ArrayStringValueSingle = new String[] { "Zero" };
            sampleSimpleArray.ArrayStringValueMultiple = new String[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            sampleSimpleArray.ArrayStringValueEmpty = new String[] { };
            sampleSimpleArray.ArrayStringValueNull = null;
            sampleSimpleArray.ArrayCharValueSingle = new Char[] { 'Z' };
            sampleSimpleArray.ArrayCharValueMultiple = new Char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
            sampleSimpleArray.ArrayCharValueEmpty = new Char[] { };
            sampleSimpleArray.ArrayCharValueNull = null;
            sampleSimpleArray.ArrayObjectValueSingle = new Object[] { "A" };
            sampleSimpleArray.ArrayObjectValueMultiple = new Object[] { true, "B", "Json", 1.1, 256, 23.456, 0, "Tests", false };
            sampleSimpleArray.ArrayObjectValueEmpty = new Object[] { };
            sampleSimpleArray.ArrayObjectValueNull = null;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleArray);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleArray);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDateTime()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleDateTime1 sampleSimpleDateTime1 = new LazyJsonSerializerSampleSimpleDateTime1();
            sampleSimpleDateTime1.DateTimeValueNormal = new DateTime(2023, 02, 26, 14, 15, 16, 000);
            sampleSimpleDateTime1.DateTimeValueNormalNullableNotNull = new DateTime(2023, 02, 26, 17, 18, 19, 000);
            sampleSimpleDateTime1.DateTimeValueNormalNullableNull = null;

            LazyJsonSerializerSampleSimpleDateTime2 sampleSimpleDateTime2 = new LazyJsonSerializerSampleSimpleDateTime2();
            sampleSimpleDateTime2.DateTimeValueFormatted = new DateTime(2023, 02, 26, 14, 15, 16, 123);
            sampleSimpleDateTime2.DateTimeValueFormattedNullableNotNull = new DateTime(2023, 02, 26, 17, 18, 19, 123);
            sampleSimpleDateTime2.DateTimeValueFormattedNullableNull = null;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDateTime);

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            String parcialJson1 = LazyJsonSerializer.Serialize(sampleSimpleDateTime1);
            String parcialJson2 = LazyJsonSerializer.Serialize(sampleSimpleDateTime2, serializerOptions);

            LazyJson lazyJson = LazyJsonReader.Read(parcialJson1);
            LazyJson lazyJsonAux = LazyJsonReader.Read(parcialJson2);

            lazyJson.AppendProperty("$", "DateTimeValueFormatted", lazyJsonAux.Find("$.DateTimeValueFormatted"));
            lazyJson.AppendProperty("$", "DateTimeValueFormattedNullableNotNull", lazyJsonAux.Find("$.DateTimeValueFormattedNullableNotNull"));
            lazyJson.AppendProperty("$", "DateTimeValueFormattedNullableNull", lazyJsonAux.Find("$.DateTimeValueFormattedNullableNull"));

            String json = LazyJsonWriter.Write(lazyJson);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDataTable()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleDataTable sampleSimpleDataTable = new LazyJsonSerializerSampleSimpleDataTable();

            sampleSimpleDataTable.DataTableWithoutRows = new DataTable("DataTableWithoutRows");

            sampleSimpleDataTable.DataTableWithSingleRow = new DataTable("DataTableWithSingleRow");
            sampleSimpleDataTable.DataTableWithSingleRow.Columns.AddRange(new DataColumn[] { new DataColumn("ColumnInt32", typeof(Int32)), new DataColumn("ColumnString", typeof(String)), new DataColumn("ColumnChar", typeof(Char)), new DataColumn("ColumnDateTime", typeof(DateTime)) });
            sampleSimpleDataTable.DataTableWithSingleRow.Rows.Add(1, "Row1", 'A', new DateTime(2022, 02, 22, 11, 52, 02, 000));
            sampleSimpleDataTable.DataTableWithSingleRow.AcceptChanges();

            sampleSimpleDataTable.DataTableWithMultipleRows = new DataTable("DataTableWithMultipleRows");
            sampleSimpleDataTable.DataTableWithMultipleRows.Columns.AddRange(new DataColumn[] { new DataColumn("ColumnInt32", typeof(Int32)), new DataColumn("ColumnString", typeof(String)), new DataColumn("ColumnChar", typeof(Char)), new DataColumn("ColumnDateTime", typeof(DateTime)) });
            sampleSimpleDataTable.DataTableWithMultipleRows.Rows.Add(1, "Row1", 'A', new DateTime(2023, 03, 23, 11, 53, 03, 000));
            sampleSimpleDataTable.DataTableWithMultipleRows.Rows.Add(2, "Row2", 'B', new DateTime(2024, 04, 24, 11, 54, 04, 000));
            sampleSimpleDataTable.DataTableWithMultipleRows.Rows.Add(3, "Row3", 'C', new DateTime(2025, 05, 25, 11, 55, 05, 000));
            sampleSimpleDataTable.DataTableWithMultipleRows.AcceptChanges();

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDataTable);

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDataTable, serializerOptions);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleDataSet()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleDataSet sampleSimpleDataSet = new LazyJsonSerializerSampleSimpleDataSet();

            sampleSimpleDataSet.DataSet = new DataSet("MyDataSet");

            sampleSimpleDataSet.DataSet.Tables.Add(new DataTable("DataTableWithoutRows"));

            sampleSimpleDataSet.DataSet.Tables.Add(new DataTable("DataTableWithSingleRow"));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithSingleRow"].Columns.AddRange(new DataColumn[] { new DataColumn("ColumnInt32", typeof(Int32)), new DataColumn("ColumnString", typeof(String)), new DataColumn("ColumnChar", typeof(Char)), new DataColumn("ColumnDateTime", typeof(DateTime)) });
            sampleSimpleDataSet.DataSet.Tables["DataTableWithSingleRow"].Rows.Add(1, "Row1", 'A', new DateTime(2022, 02, 22, 11, 52, 02, 000));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithSingleRow"].AcceptChanges();

            sampleSimpleDataSet.DataSet.Tables.Add(new DataTable("DataTableWithMultipleRows"));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"].Columns.AddRange(new DataColumn[] { new DataColumn("ColumnInt32", typeof(Int32)), new DataColumn("ColumnString", typeof(String)), new DataColumn("ColumnChar", typeof(Char)), new DataColumn("ColumnDateTime", typeof(DateTime)) });
            sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"].Rows.Add(1, "Row1", 'A', new DateTime(2023, 03, 23, 11, 53, 03, 000));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"].Rows.Add(2, "Row2", 'B', new DateTime(2024, 04, 24, 11, 54, 04, 000));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"].Rows.Add(3, "Row3", 'C', new DateTime(2025, 05, 25, 11, 55, 05, 000));
            sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"].AcceptChanges();

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleDataSet);

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleDataSet, serializerOptions);

            // Assert
            Assert.IsTrue(json == originalJson);
        }

        [TestMethod]
        public void TestSerializerSampleSimpleAttribute()
        {
            // Arrange
            LazyJsonSerializerSampleSimpleAttribute sampleSimpleAttribute = new LazyJsonSerializerSampleSimpleAttribute();
            sampleSimpleAttribute.BooleanValue = true;
            sampleSimpleAttribute.Int16Value = 123;
            sampleSimpleAttribute.StringValue = "Json";
            sampleSimpleAttribute.DecimalValue = 456.789m;

            String originalJson = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSerializerSampleSimpleAttribute);

            // Act
            String json = LazyJsonSerializer.Serialize(sampleSimpleAttribute);

            // Assert
            Assert.IsTrue(json == originalJson);
        }
    }
}