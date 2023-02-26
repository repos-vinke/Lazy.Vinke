// LazyJsonDeserializerTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using System.Data;
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
            LazyJsonSampleSimpleBoolean sampleSimpleBoolean = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleBoolean);

            // Act
            sampleSimpleBoolean = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleBoolean>(json);

            // Assert
            Assert.IsTrue(sampleSimpleBoolean.BooleanValue == true);
            Assert.IsNull(sampleSimpleBoolean.BooleanValueNullableNull);
            Assert.IsTrue(sampleSimpleBoolean.BooleanValueNullableNotNull == false);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDecimal()
        {
            // Arrange
            LazyJsonSampleSimpleDecimal sampleSimpleDecimal = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleDecimal);

            // Act
            sampleSimpleDecimal = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleDecimal>(json);

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
            LazyJsonSampleSimpleInteger sampleSimpleInteger = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleInteger);

            // Act
            sampleSimpleInteger = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleInteger>(json);

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
            LazyJsonSampleSimpleString sampleSimpleString = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleString);

            // Act
            sampleSimpleString = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleString>(json);

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

        [TestMethod]
        public void TestDeserializerSampleSimpleArray()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleArray sampleSimpleArray = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleArray);

            // Act
            sampleSimpleArray = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleArray>(json);

            // Assert
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueSingle[0] == false);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueMultiple[0] == true);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueMultiple[5] == false);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueMultiple[8] == true);
            Assert.IsTrue(sampleSimpleArray.ArrayBooleanValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayBooleanValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueSingle[0] == 0.0m);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueMultiple[0] == 1.1m);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueMultiple[3] == 4.4m);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueMultiple[6] == 7.7m);
            Assert.IsTrue(sampleSimpleArray.ArrayDecimalValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayDecimalValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueSingle[0] == 0);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueMultiple[2] == 3);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueMultiple[5] == 6);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueMultiple[8] == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayInt16ValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayInt16ValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueSingle[0] == 0);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueMultiple[1] == 2);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueMultiple[4] == 5);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueMultiple[7] == 8);
            Assert.IsTrue(sampleSimpleArray.ArrayInt64ValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayInt64ValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayStringValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueSingle[0] == "Zero");
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueMultiple[1] == "Two");
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueMultiple[4] == "Five");
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueMultiple[7] == "Eight");
            Assert.IsTrue(sampleSimpleArray.ArrayStringValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayStringValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayCharValueSingle.Length == 1);
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueSingle[0] == 'Z');
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueMultiple.Length == 9);
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueMultiple[2] == 'C');
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueMultiple[5] == 'F');
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueMultiple[8] == 'I');
            Assert.IsTrue(sampleSimpleArray.ArrayCharValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayCharValueNull);

            Assert.IsTrue(sampleSimpleArray.ArrayObjectValueSingle.Length == 1);
            Assert.IsTrue((String)sampleSimpleArray.ArrayObjectValueSingle[0] == "A");
            Assert.IsTrue(sampleSimpleArray.ArrayObjectValueMultiple.Length == 9);
            Assert.IsTrue((Boolean)sampleSimpleArray.ArrayObjectValueMultiple[0] == true);
            Assert.IsTrue((String)sampleSimpleArray.ArrayObjectValueMultiple[1] == "B");
            Assert.IsTrue((String)sampleSimpleArray.ArrayObjectValueMultiple[2] == "Json");
            Assert.IsTrue((Decimal)sampleSimpleArray.ArrayObjectValueMultiple[3] == 1.1m);
            Assert.IsTrue((Int64)sampleSimpleArray.ArrayObjectValueMultiple[4] == 256);
            Assert.IsTrue((Decimal)sampleSimpleArray.ArrayObjectValueMultiple[5] == 23.456m);
            Assert.IsTrue((Int64)sampleSimpleArray.ArrayObjectValueMultiple[6] == 0);
            Assert.IsTrue((String)sampleSimpleArray.ArrayObjectValueMultiple[7] == "Tests");
            Assert.IsTrue((Boolean)sampleSimpleArray.ArrayObjectValueMultiple[8] == false);
            Assert.IsTrue(sampleSimpleArray.ArrayObjectValueEmpty.Length == 0);
            Assert.IsNull(sampleSimpleArray.ArrayObjectValueNull);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDateTime()
        {
            // Arrange
            LazyJsonDeserializerSampleSimpleDateTime sampleSimpleDateTime = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonDeserializerSampleSimpleDateTime);

            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            sampleSimpleDateTime = LazyJsonDeserializer.Deserialize<LazyJsonDeserializerSampleSimpleDateTime>(json, deserializerOptions);

            // Assert
            Assert.IsTrue(sampleSimpleDateTime.DateTimeValueInvalid == DateTime.MinValue);
            Assert.IsTrue(sampleSimpleDateTime.DateTimeValueValid.ToString(TestStringFormat.DateTime.ISO8601Z) == "2023-02-24T10:00:01:000Z");
            Assert.IsNotNull(sampleSimpleDateTime.DateTimeValueNotNullableNull == DateTime.MinValue);
            Assert.IsNull(sampleSimpleDateTime.DateTimeValueNullableNull);
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDataTable()
        {
            // Arrange
            LazyJsonSampleSimpleDataTable sampleSimpleDataTable = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleDataTable);

            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithSingleRow"].Columns["ColumnChar"].Set(typeof(Char));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithSingleRow"].Columns["ColumnDateTime"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithMultipleRows"].Columns["ColumnChar"].Set(typeof(Char));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithMultipleRows"].Columns["ColumnDateTime"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            sampleSimpleDataTable = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleDataTable>(json, deserializerOptions);

            // Assert
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithoutRows.TableName == "DataTableWithoutRows");
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithoutRows.Rows.Count == 0);

            Assert.IsTrue(sampleSimpleDataTable.DataTableWithSingleRow.TableName == "DataTableWithSingleRow");
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithSingleRow.Rows.Count == 1);
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithSingleRow.Columns["ColumnChar"].DataType == typeof(Char));
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithSingleRow.Columns["ColumnDateTime"].DataType == typeof(DateTime));
            Assert.IsTrue((Int32)sampleSimpleDataTable.DataTableWithSingleRow.Rows[0]["ColumnInt32"] == 1);
            Assert.IsTrue((String)sampleSimpleDataTable.DataTableWithSingleRow.Rows[0]["ColumnString"] == "Row1");
            Assert.IsTrue((Char)sampleSimpleDataTable.DataTableWithSingleRow.Rows[0]["ColumnChar"] == 'A');
            Assert.IsTrue(Convert.ToDateTime(sampleSimpleDataTable.DataTableWithSingleRow.Rows[0]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2022-02-22T11:52:02:000Z");

            Assert.IsTrue(sampleSimpleDataTable.DataTableWithMultipleRows.TableName == "DataTableWithMultipleRows");
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithMultipleRows.Rows.Count == 3);
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithMultipleRows.Columns["ColumnChar"].DataType == typeof(Char));
            Assert.IsTrue(sampleSimpleDataTable.DataTableWithMultipleRows.Columns["ColumnDateTime"].DataType == typeof(DateTime));

            Assert.IsTrue((Int32)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[0]["ColumnInt32"] == 1);
            Assert.IsTrue((String)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[0]["ColumnString"] == "Row1");
            Assert.IsTrue((Char)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[0]["ColumnChar"] == 'A');
            Assert.IsTrue(Convert.ToDateTime(sampleSimpleDataTable.DataTableWithMultipleRows.Rows[0]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2023-03-23T11:53:03:000Z");

            Assert.IsTrue((Int32)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[1]["ColumnInt32"] == 2);
            Assert.IsTrue((String)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[1]["ColumnString"] == "Row2");
            Assert.IsTrue((Char)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[1]["ColumnChar"] == 'B');
            Assert.IsTrue(Convert.ToDateTime(sampleSimpleDataTable.DataTableWithMultipleRows.Rows[1]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2024-04-24T11:54:04:000Z");

            Assert.IsTrue((Int32)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[2]["ColumnInt32"] == 3);
            Assert.IsTrue((String)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[2]["ColumnString"] == "Row3");
            Assert.IsTrue((Char)sampleSimpleDataTable.DataTableWithMultipleRows.Rows[2]["ColumnChar"] == 'C');
            Assert.IsTrue(Convert.ToDateTime(sampleSimpleDataTable.DataTableWithMultipleRows.Rows[2]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2025-05-25T11:55:05:000Z");
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleDataSet()
        {
            // Arrange
            LazyJsonSampleSimpleDataSet sampleSimpleDataSet = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleDataSet);

            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithSingleRow"].Columns["ColumnChar"].Set(typeof(Char));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithSingleRow"].Columns["ColumnDateTime"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithMultipleRows"].Columns["ColumnChar"].Set(typeof(Char));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["DataTableWithMultipleRows"].Columns["ColumnDateTime"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = TestStringFormat.DateTime.ISO8601Z;

            // Act
            sampleSimpleDataSet = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleDataSet>(json, deserializerOptions);

            // Assert
            Assert.IsTrue(sampleSimpleDataSet.DataSet.DataSetName == "MyDataSet");
            Assert.IsTrue(sampleSimpleDataSet.DataSet.Tables.Contains("DataTableWithoutRows"));
            Assert.IsTrue(sampleSimpleDataSet.DataSet.Tables.Contains("DataTableWithSingleRow"));
            Assert.IsTrue(sampleSimpleDataSet.DataSet.Tables.Contains("DataTableWithMultipleRows"));

            DataTable dataTableWithoutRows = sampleSimpleDataSet.DataSet.Tables["DataTableWithoutRows"];
            Assert.IsTrue(dataTableWithoutRows.TableName == "DataTableWithoutRows");
            Assert.IsTrue(dataTableWithoutRows.Rows.Count == 0);

            DataTable dataTableWithSingleRow = sampleSimpleDataSet.DataSet.Tables["DataTableWithSingleRow"];
            Assert.IsTrue(dataTableWithSingleRow.TableName == "DataTableWithSingleRow");
            Assert.IsTrue(dataTableWithSingleRow.Rows.Count == 1);
            Assert.IsTrue(dataTableWithSingleRow.Columns["ColumnChar"].DataType == typeof(Char));
            Assert.IsTrue(dataTableWithSingleRow.Columns["ColumnDateTime"].DataType == typeof(DateTime));
            Assert.IsTrue((Int32)dataTableWithSingleRow.Rows[0]["ColumnInt32"] == 1);
            Assert.IsTrue((String)dataTableWithSingleRow.Rows[0]["ColumnString"] == "Row1");
            Assert.IsTrue((Char)dataTableWithSingleRow.Rows[0]["ColumnChar"] == 'A');
            Assert.IsTrue(Convert.ToDateTime(dataTableWithSingleRow.Rows[0]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2022-02-22T11:52:02:000Z");

            DataTable dataTableWithMultipleRows = sampleSimpleDataSet.DataSet.Tables["DataTableWithMultipleRows"];
            Assert.IsTrue(dataTableWithMultipleRows.TableName == "DataTableWithMultipleRows");
            Assert.IsTrue(dataTableWithMultipleRows.Rows.Count == 3);
            Assert.IsTrue(dataTableWithMultipleRows.Columns["ColumnChar"].DataType == typeof(Char));
            Assert.IsTrue(dataTableWithMultipleRows.Columns["ColumnDateTime"].DataType == typeof(DateTime));

            Assert.IsTrue((Int32)dataTableWithMultipleRows.Rows[0]["ColumnInt32"] == 1);
            Assert.IsTrue((String)dataTableWithMultipleRows.Rows[0]["ColumnString"] == "Row1");
            Assert.IsTrue((Char)dataTableWithMultipleRows.Rows[0]["ColumnChar"] == 'A');
            Assert.IsTrue(Convert.ToDateTime(dataTableWithMultipleRows.Rows[0]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2023-03-23T11:53:03:000Z");

            Assert.IsTrue((Int32)dataTableWithMultipleRows.Rows[1]["ColumnInt32"] == 2);
            Assert.IsTrue((String)dataTableWithMultipleRows.Rows[1]["ColumnString"] == "Row2");
            Assert.IsTrue((Char)dataTableWithMultipleRows.Rows[1]["ColumnChar"] == 'B');
            Assert.IsTrue(Convert.ToDateTime(dataTableWithMultipleRows.Rows[1]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2024-04-24T11:54:04:000Z");

            Assert.IsTrue((Int32)dataTableWithMultipleRows.Rows[2]["ColumnInt32"] == 3);
            Assert.IsTrue((String)dataTableWithMultipleRows.Rows[2]["ColumnString"] == "Row3");
            Assert.IsTrue((Char)dataTableWithMultipleRows.Rows[2]["ColumnChar"] == 'C');
            Assert.IsTrue(Convert.ToDateTime(dataTableWithMultipleRows.Rows[2]["ColumnDateTime"]).ToString(TestStringFormat.DateTime.ISO8601Z) == "2025-05-25T11:55:05:000Z");
        }

        [TestMethod]
        public void TestDeserializerSampleSimpleAttribute()
        {
            // Arrange
            LazyJsonSampleSimpleAttribute sampleSimpleAttribute = null;
            String json = Encoding.UTF8.GetString(Properties.Resources.LazyJsonSampleSimpleAttribute);

            // Act
            sampleSimpleAttribute = LazyJsonDeserializer.Deserialize<LazyJsonSampleSimpleAttribute>(json);

            // Assert
            Assert.IsTrue(sampleSimpleAttribute.BooleanValue == true);
            Assert.IsTrue(sampleSimpleAttribute.Int16Value == 123);
            Assert.IsNull(sampleSimpleAttribute.StringValue);
            Assert.IsTrue(sampleSimpleAttribute.DecimalValue == 456.789m);
            Assert.IsTrue(sampleSimpleAttribute.DecimalValueReadOnly == 0.0m);
        }
    }
}