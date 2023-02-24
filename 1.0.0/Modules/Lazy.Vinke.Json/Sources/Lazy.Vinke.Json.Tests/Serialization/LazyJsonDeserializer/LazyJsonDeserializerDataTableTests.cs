// LazyJsonDeserializerDataTableTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using System.Data;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerDataTableTests
    {
        [TestMethod]
        public void TestDeserializerDataTablePropertyNull()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(DataTable));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyTokenTypeNotObject()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataTable", new LazyJsonArray());
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(DataTable));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyDataTypeNull()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataTable", new LazyJsonObject());
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyDataTypeNotDataTable()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataTable", new LazyJsonObject());
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(Decimal));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyWithoutRows()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("WithoutRows", new LazyJsonObject());
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(DataTable));

            // Assert
            Assert.IsTrue(((DataTable)resDataTable).TableName == "WithoutRows");
            Assert.IsTrue(((DataTable)resDataTable).Rows.Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyWithRows()
        {
            // Arrange
            LazyJsonObject jsonObjectCurrent = new LazyJsonObject();
            jsonObjectCurrent.Add("DBNull", new LazyJsonNull());

            jsonObjectCurrent.Add("Boolean1", new LazyJsonBoolean(true));
            jsonObjectCurrent.Add("Boolean2", new LazyJsonBoolean(false));
            jsonObjectCurrent.Add("BooleanDBNull", new LazyJsonBoolean(null));

            jsonObjectCurrent.Add("String1", new LazyJsonString("Json"));
            jsonObjectCurrent.Add("String2", new LazyJsonString("DataTable"));
            jsonObjectCurrent.Add("String3", new LazyJsonString("Tests"));
            jsonObjectCurrent.Add("StringDBNull", new LazyJsonString(null));

            jsonObjectCurrent.Add("Integer1", new LazyJsonInteger(-50));
            jsonObjectCurrent.Add("Integer2", new LazyJsonInteger(0));
            jsonObjectCurrent.Add("Integer3", new LazyJsonInteger(2075));
            jsonObjectCurrent.Add("IntegerDBNull", new LazyJsonInteger(null));

            jsonObjectCurrent.Add("Decimal1", new LazyJsonDecimal(1.2m));
            jsonObjectCurrent.Add("Decimal2", new LazyJsonDecimal(34.56m));
            jsonObjectCurrent.Add("Decimal3", new LazyJsonDecimal(789.123m));
            jsonObjectCurrent.Add("DecimalDBNull", new LazyJsonDecimal(null));

            LazyJsonObject jsonObjectValues = new LazyJsonObject();
            jsonObjectValues.Add("Original", new LazyJsonNull());
            jsonObjectValues.Add("Current", jsonObjectCurrent);

            LazyJsonObject jsonObjectRow1 = new LazyJsonObject();
            jsonObjectRow1.Add("State", new LazyJsonString("Unchanged"));
            jsonObjectRow1.Add("Values", jsonObjectValues);

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonObjectRow1);

            LazyJsonObject jsonObjectRows = new LazyJsonObject();
            jsonObjectRows.Add("Rows", jsonArray);

            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("WithRows", jsonObjectRows);
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(DataTable));

            // Assert
            DataTable dataTable = (DataTable)resDataTable;

            Assert.IsTrue(dataTable.TableName == "WithRows");
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.IsTrue(dataTable.Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(dataTable.Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean2"] == false);
            Assert.IsTrue(dataTable.Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)dataTable.Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)dataTable.Rows[0]["String2"] == "DataTable");
            Assert.IsTrue((String)dataTable.Rows[0]["String3"] == "Tests");
            Assert.IsTrue(dataTable.Rows[0]["StringDBNull"] == DBNull.Value);

            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer1"] == -50);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer2"] == 0);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer3"] == 2075);
            Assert.IsTrue(dataTable.Rows[0]["IntegerDBNull"] == DBNull.Value);

            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal1"] == 1.2m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal2"] == 34.56m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal3"] == 789.123m);
            Assert.IsTrue(dataTable.Rows[0]["DecimalDBNull"] == DBNull.Value);
        }

        [TestMethod]
        public void TestDeserializerDataTablePropertyWithRowsAndColumnOptions()
        {
            // Arrange
            LazyJsonObject jsonObjectCurrent = new LazyJsonObject();
            jsonObjectCurrent.Add("DBNull", new LazyJsonNull());

            jsonObjectCurrent.Add("Boolean1", new LazyJsonBoolean(true));
            jsonObjectCurrent.Add("Boolean2", new LazyJsonBoolean(false));
            jsonObjectCurrent.Add("BooleanDBNull", new LazyJsonBoolean(null));

            jsonObjectCurrent.Add("String1", new LazyJsonString("Json"));
            jsonObjectCurrent.Add("String2", new LazyJsonString("DataTable"));
            jsonObjectCurrent.Add("String3", new LazyJsonString("Tests"));
            jsonObjectCurrent.Add("StringDBNull", new LazyJsonString(null));

            jsonObjectCurrent.Add("Integer1", new LazyJsonInteger(-50));
            jsonObjectCurrent.Add("Integer2", new LazyJsonInteger(0));
            jsonObjectCurrent.Add("Integer3", new LazyJsonInteger(2075));
            jsonObjectCurrent.Add("IntegerDBNull", new LazyJsonInteger(null));

            jsonObjectCurrent.Add("Decimal1", new LazyJsonDecimal(1.2m));
            jsonObjectCurrent.Add("Decimal2", new LazyJsonDecimal(34.56m));
            jsonObjectCurrent.Add("Decimal3", new LazyJsonDecimal(789.123m));
            jsonObjectCurrent.Add("DecimalDBNull", new LazyJsonDecimal(null));

            DateTime dateTime1 = DateTime.Now;
            jsonObjectCurrent.Add("DateTime1", new LazyJsonString(dateTime1.ToString(TestStringFormat.DateTimeISO_GMTXX)));

            DateTime dateTime2 = DateTime.Now.AddDays(10);
            jsonObjectCurrent.Add("DateTime2", new LazyJsonString(dateTime2.ToString(TestStringFormat.DateTimeISO_GMTXX)));

            LazyJsonObject jsonObjectValues = new LazyJsonObject();
            jsonObjectValues.Add("Original", new LazyJsonNull());
            jsonObjectValues.Add("Current", jsonObjectCurrent);

            LazyJsonObject jsonObjectRow1 = new LazyJsonObject();
            jsonObjectRow1.Add("State", new LazyJsonString("Unchanged"));
            jsonObjectRow1.Add("Values", jsonObjectValues);

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonObjectRow1);

            LazyJsonObject jsonObjectRows = new LazyJsonObject();
            jsonObjectRows.Add("Rows", jsonArray);

            Object resDataTable = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("WithRows", jsonObjectRows);
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["WithRows"].Columns["DateTime1"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["WithRows"].Columns["DateTime2"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Regex = TestStringRegex.DateTimeISO_GMTXX;
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = TestStringFormat.DateTimeISO_GMTXX;

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonProperty, typeof(DataTable), deserializerOptions);

            // Assert
            DataTable dataTable = (DataTable)resDataTable;

            Assert.IsTrue(dataTable.TableName == "WithRows");
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.IsTrue(dataTable.Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(dataTable.Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean2"] == false);
            Assert.IsTrue(dataTable.Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)dataTable.Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)dataTable.Rows[0]["String2"] == "DataTable");
            Assert.IsTrue((String)dataTable.Rows[0]["String3"] == "Tests");
            Assert.IsTrue(dataTable.Rows[0]["StringDBNull"] == DBNull.Value);

            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer1"] == -50);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer2"] == 0);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer3"] == 2075);
            Assert.IsTrue(dataTable.Rows[0]["IntegerDBNull"] == DBNull.Value);

            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal1"] == 1.2m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal2"] == 34.56m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal3"] == 789.123m);
            Assert.IsTrue(dataTable.Rows[0]["DecimalDBNull"] == DBNull.Value);

            Assert.IsTrue(dataTable.Columns["DateTime1"].DataType == typeof(DateTime));
            Assert.IsTrue(((DateTime)dataTable.Rows[0]["DateTime1"]).ToString(TestStringFormat.DateTimeISO_GMTXX) == dateTime1.ToString(TestStringFormat.DateTimeISO_GMTXX));

            Assert.IsTrue(dataTable.Columns["DateTime2"].DataType == typeof(DateTime));
            Assert.IsTrue(((DateTime)dataTable.Rows[0]["DateTime2"]).ToString(TestStringFormat.DateTimeISO_GMTXX) == dateTime2.ToString(TestStringFormat.DateTimeISO_GMTXX));
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenNull()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonToken, typeof(DataTable));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenTypeNotObject()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonToken jsonToken = new LazyJsonArray();
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonToken, typeof(DataTable));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenDataTypeNull()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonToken jsonToken = new LazyJsonObject();
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenDataTypeNotDataTable()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonToken jsonToken = new LazyJsonObject();
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonToken, typeof(Decimal));

            // Assert
            Assert.IsNull(resDataTable);
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenWithoutRows()
        {
            // Arrange
            Object resDataTable = null;
            LazyJsonToken jsonToken = new LazyJsonObject();
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonToken, typeof(DataTable));

            // Assert
            Assert.IsTrue(((DataTable)resDataTable).Rows.Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDataTableTokenWithRows()
        {
            // Arrange
            LazyJsonObject jsonObjectCurrent = new LazyJsonObject();
            jsonObjectCurrent.Add("DBNull", new LazyJsonNull());

            jsonObjectCurrent.Add("Boolean1", new LazyJsonBoolean(true));
            jsonObjectCurrent.Add("Boolean2", new LazyJsonBoolean(false));
            jsonObjectCurrent.Add("BooleanDBNull", new LazyJsonBoolean(null));

            jsonObjectCurrent.Add("String1", new LazyJsonString("Json"));
            jsonObjectCurrent.Add("String2", new LazyJsonString("DataTable"));
            jsonObjectCurrent.Add("String3", new LazyJsonString("Tests"));
            jsonObjectCurrent.Add("StringDBNull", new LazyJsonString(null));

            jsonObjectCurrent.Add("Integer1", new LazyJsonInteger(-50));
            jsonObjectCurrent.Add("Integer2", new LazyJsonInteger(0));
            jsonObjectCurrent.Add("Integer3", new LazyJsonInteger(2075));
            jsonObjectCurrent.Add("IntegerDBNull", new LazyJsonInteger(null));

            jsonObjectCurrent.Add("Decimal1", new LazyJsonDecimal(1.2m));
            jsonObjectCurrent.Add("Decimal2", new LazyJsonDecimal(34.56m));
            jsonObjectCurrent.Add("Decimal3", new LazyJsonDecimal(789.123m));
            jsonObjectCurrent.Add("DecimalDBNull", new LazyJsonDecimal(null));

            LazyJsonObject jsonObjectValues = new LazyJsonObject();
            jsonObjectValues.Add("Original", new LazyJsonNull());
            jsonObjectValues.Add("Current", jsonObjectCurrent);

            LazyJsonObject jsonObjectRow1 = new LazyJsonObject();
            jsonObjectRow1.Add("State", new LazyJsonString("Unchanged"));
            jsonObjectRow1.Add("Values", jsonObjectValues);

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonObjectRow1);

            LazyJsonObject jsonObjectRows = new LazyJsonObject();
            jsonObjectRows.Add("Rows", jsonArray);

            Object resDataTable = null;
            LazyJsonDeserializerDataTable deserializerDataTable = new LazyJsonDeserializerDataTable();

            // Act
            resDataTable = deserializerDataTable.Deserialize(jsonObjectRows, typeof(DataTable));

            // Assert
            DataTable dataTable = (DataTable)resDataTable;

            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.IsTrue(dataTable.Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(dataTable.Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean2"] == false);
            Assert.IsTrue(dataTable.Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)dataTable.Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)dataTable.Rows[0]["String2"] == "DataTable");
            Assert.IsTrue((String)dataTable.Rows[0]["String3"] == "Tests");
            Assert.IsTrue(dataTable.Rows[0]["StringDBNull"] == DBNull.Value);

            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer1"] == -50);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer2"] == 0);
            Assert.IsTrue((Int32)dataTable.Rows[0]["Integer3"] == 2075);
            Assert.IsTrue(dataTable.Rows[0]["IntegerDBNull"] == DBNull.Value);

            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal1"] == 1.2m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal2"] == 34.56m);
            Assert.IsTrue((Decimal)dataTable.Rows[0]["Decimal3"] == 789.123m);
            Assert.IsTrue(dataTable.Rows[0]["DecimalDBNull"] == DBNull.Value);
        }
    }
}