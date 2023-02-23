// LazyJsonDeserializerDataTableTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke.Json;
using System.Data;

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
            Assert.IsTrue(((DataTable)resDataTable).TableName == "WithRows");
            Assert.IsTrue(((DataTable)resDataTable).Rows.Count == 1);
            Assert.IsTrue(((DataTable)resDataTable).Rows[0].RowState == DataRowState.Unchanged);

            Assert.IsTrue(((DataTable)resDataTable).Rows[0]["DBNull"] == DBNull.Value);

            Assert.IsTrue((Boolean)((DataTable)resDataTable).Rows[0]["Boolean1"] == true);
            Assert.IsTrue((Boolean)((DataTable)resDataTable).Rows[0]["Boolean2"] == false);
            Assert.IsTrue(((DataTable)resDataTable).Rows[0]["BooleanDBNull"] == DBNull.Value);

            Assert.IsTrue((String)((DataTable)resDataTable).Rows[0]["String1"] == "Json");
            Assert.IsTrue((String)((DataTable)resDataTable).Rows[0]["String2"] == "DataTable");
            Assert.IsTrue((String)((DataTable)resDataTable).Rows[0]["String3"] == "Tests");
            Assert.IsTrue(((DataTable)resDataTable).Rows[0]["StringDBNull"] == DBNull.Value);

            Assert.IsTrue((Int32)((DataTable)resDataTable).Rows[0]["Integer1"] == -50);
            Assert.IsTrue((Int32)((DataTable)resDataTable).Rows[0]["Integer2"] == 0);
            Assert.IsTrue((Int32)((DataTable)resDataTable).Rows[0]["Integer3"] == 2075);
            Assert.IsTrue(((DataTable)resDataTable).Rows[0]["IntegerDBNull"] == DBNull.Value);

            Assert.IsTrue((Decimal)((DataTable)resDataTable).Rows[0]["Decimal1"] == 1.2m);
            Assert.IsTrue((Decimal)((DataTable)resDataTable).Rows[0]["Decimal2"] == 34.56m);
            Assert.IsTrue((Decimal)((DataTable)resDataTable).Rows[0]["Decimal3"] == 789.123m);
            Assert.IsTrue(((DataTable)resDataTable).Rows[0]["DecimalDBNull"] == DBNull.Value);
        }
    }
}