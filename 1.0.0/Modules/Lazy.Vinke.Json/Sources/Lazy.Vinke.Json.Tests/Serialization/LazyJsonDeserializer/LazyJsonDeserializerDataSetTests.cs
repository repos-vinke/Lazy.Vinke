// LazyJsonDeserializerDataSetTests.cs
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
    public class LazyJsonDeserializerDataSetTests
    {
        [TestMethod]
        public void TestDeserializerDataSetPropertyNull()
        {
            // Arrange
            Object resDataSet = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(DataSet));

            // Assert
            Assert.IsNull(resDataSet);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenNull()
        {
            // Arrange
            Object resDataSet = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonToken, typeof(DataSet));

            // Assert
            Assert.IsNull(resDataSet);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenTypeNotObject()
        {
            // Arrange
            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", new LazyJsonArray());
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(DataSet));

            // Assert
            Assert.IsNull(resDataSet);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenDataTypeNull()
        {
            // Arrange
            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", new LazyJsonObject());
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resDataSet);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenDataTypeNotDataSet()
        {
            // Arrange
            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", new LazyJsonObject());
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(String));

            // Assert
            Assert.IsNull(resDataSet);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenWithoutRows()
        {
            // Arrange
            LazyJsonProperty jsonPropertyEmptyTable = new LazyJsonProperty("WithoutRows", new LazyJsonObject());

            LazyJsonObject jsonObjectTables = new LazyJsonObject();
            jsonObjectTables.Add(jsonPropertyEmptyTable);

            LazyJsonProperty jsonPropertyName = new LazyJsonProperty("Name", new LazyJsonString("DataSet"));
            LazyJsonProperty jsonPropertyTables = new LazyJsonProperty("Tables", jsonObjectTables);

            LazyJsonObject jsonObjectDatSet = new LazyJsonObject();
            jsonObjectDatSet.Add(jsonPropertyName);
            jsonObjectDatSet.Add(jsonPropertyTables);

            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", jsonObjectDatSet);
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(DataSet));

            // Assert
            Assert.IsTrue(((DataSet)resDataSet).DataSetName == "DataSet");
            Assert.IsTrue(((DataSet)resDataSet).Tables.Count == 1);
            Assert.IsTrue(((DataSet)resDataSet).Tables[0].TableName == "WithoutRows");
            Assert.IsTrue(((DataSet)resDataSet).Tables[0].Rows.Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDataSetTokenWithRows()
        {
            // Arrange
            LazyJsonObject jsonObjectCurrent = new LazyJsonObject();
            jsonObjectCurrent.Add("DBNull", new LazyJsonNull());

            jsonObjectCurrent.Add("Boolean1", new LazyJsonBoolean(true));
            jsonObjectCurrent.Add("Boolean2", new LazyJsonBoolean(false));
            jsonObjectCurrent.Add("BooleanDBNull", new LazyJsonBoolean(null));

            jsonObjectCurrent.Add("String1", new LazyJsonString("Json"));
            jsonObjectCurrent.Add("String2", new LazyJsonString("DataSet"));
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

            LazyJsonProperty jsonPropertyPopulatedTable = new LazyJsonProperty("WithRows", jsonObjectRows);

            LazyJsonObject jsonObjectTables = new LazyJsonObject();
            jsonObjectTables.Add(jsonPropertyPopulatedTable);

            LazyJsonProperty jsonPropertyName = new LazyJsonProperty("Name", new LazyJsonString("DataSet"));
            LazyJsonProperty jsonPropertyTables = new LazyJsonProperty("Tables", jsonObjectTables);

            LazyJsonObject jsonObjectDatSet = new LazyJsonObject();
            jsonObjectDatSet.Add(jsonPropertyName);
            jsonObjectDatSet.Add(jsonPropertyTables);

            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", jsonObjectDatSet);
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(DataSet));

            // Assert
            DataSet dataSet = (DataSet)resDataSet;

            Assert.IsTrue(dataSet.DataSetName == "DataSet");
            Assert.IsTrue(dataSet.Tables.Count == 1);

            DataTable dataTable = (DataTable)dataSet.Tables[0];

            Assert.IsTrue(dataTable.TableName == "WithRows");
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.IsTrue(dataTable.Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(dataTable.Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean2"] == false);
            Assert.IsTrue(dataTable.Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)dataTable.Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)dataTable.Rows[0]["String2"] == "DataSet");
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
        public void TestDeserializerDataTableTokenWithRowsAndColumnOptions()
        {
            // Arrange
            LazyJsonObject jsonObjectCurrent = new LazyJsonObject();
            jsonObjectCurrent.Add("DBNull", new LazyJsonNull());

            jsonObjectCurrent.Add("Boolean1", new LazyJsonBoolean(true));
            jsonObjectCurrent.Add("Boolean2", new LazyJsonBoolean(false));
            jsonObjectCurrent.Add("BooleanDBNull", new LazyJsonBoolean(null));

            jsonObjectCurrent.Add("String1", new LazyJsonString("Json"));
            jsonObjectCurrent.Add("String2", new LazyJsonString("DataSet"));
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

            LazyJsonProperty jsonPropertyPopulatedTable = new LazyJsonProperty("WithRows", jsonObjectRows);

            LazyJsonObject jsonObjectTables = new LazyJsonObject();
            jsonObjectTables.Add(jsonPropertyPopulatedTable);

            LazyJsonProperty jsonPropertyName = new LazyJsonProperty("Name", new LazyJsonString("DataSet"));
            LazyJsonProperty jsonPropertyTables = new LazyJsonProperty("Tables", jsonObjectTables);

            LazyJsonObject jsonObjectDatSet = new LazyJsonObject();
            jsonObjectDatSet.Add(jsonPropertyName);
            jsonObjectDatSet.Add(jsonPropertyTables);

            Object resDataSet = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("DataSet", jsonObjectDatSet);
            LazyJsonDeserializerDataSet deserializerDataSet = new LazyJsonDeserializerDataSet();

            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["WithRows"].Columns["DateTime1"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["WithRows"].Columns["DateTime2"].Set(typeof(DateTime));
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Regex = TestStringRegex.DateTimeISO_GMTXX;
            deserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = TestStringFormat.DateTimeISO_GMTXX;

            // Act
            resDataSet = deserializerDataSet.Deserialize(jsonProperty, typeof(DataSet), deserializerOptions);

            // Assert
            DataSet dataSet = (DataSet)resDataSet;

            Assert.IsTrue(dataSet.DataSetName == "DataSet");
            Assert.IsTrue(dataSet.Tables.Count == 1);

            DataTable dataTable = (DataTable)dataSet.Tables[0];

            Assert.IsTrue(dataTable.TableName == "WithRows");
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.IsTrue(dataTable.Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(dataTable.Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)dataTable.Rows[0]["Boolean2"] == false);
            Assert.IsTrue(dataTable.Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)dataTable.Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)dataTable.Rows[0]["String2"] == "DataSet");
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
    }
}