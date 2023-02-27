// TestLazyJsonSerializerDataTable.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 25

using System;
using System.Data;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class TestLazyJsonSerializerDataTable
    {
        [TestMethod]
        public void TestSerializerDataTableDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDataTableDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = new DataSet();
            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDataTableEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DataTable data = new DataTable("MyDataTable");
            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)resToken)["Rows"].Token.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)((LazyJsonObject)resToken)["Rows"].Token).Count == 0);
        }

        [TestMethod]
        public void TestSerializerDataTableSingleRow()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DataTable data = new DataTable("MyDataTable");
            data.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(Int16)), new DataColumn("Code", typeof(String)), new DataColumn("Value", typeof(Decimal)) });
            data.Rows.Add(1, "01", 10.4m);
            data.AcceptChanges();

            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)resToken)["Rows"].Token.Type == LazyJsonType.Array);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)((LazyJsonObject)resToken)["Rows"].Token;
            Assert.IsTrue(jsonArrayRows.Count == 1);
            Assert.IsTrue(jsonArrayRows[0].Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["State"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)((LazyJsonObject)jsonArrayRows[0])["State"].Token).Value == "Unchanged");
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["Values"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[0])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Code"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Value"].Token.Type == LazyJsonType.Decimal);
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Id"].Token).Value == 1);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Code"].Token).Value == "01");
            Assert.IsTrue(((LazyJsonDecimal)jsonObjectRowsValuesCurrent["Value"].Token).Value == 10.4m);
        }

        [TestMethod]
        public void TestSerializerDataTableMultipleRow()
        {
            // Arrange
            LazyJsonToken resToken = null;

            DateTime dateTimeRow1 = DateTime.Now.AddDays(1);
            DateTime dateTimeRow2 = DateTime.Now.AddDays(2);
            DateTime dateTimeRow3 = DateTime.Now.AddDays(3);

            DataTable data = new DataTable("MyDataTable");
            data.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(String)), new DataColumn("Count", typeof(Int64)), new DataColumn("Active", typeof(Boolean)), new DataColumn("Date", typeof(DateTime)) });
            data.Rows.Add("A", 10, true, dateTimeRow1);
            data.Rows.Add("B", 20, false, dateTimeRow2);
            data.Rows.Add("C", 30, true, dateTimeRow3);

            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)resToken)["Rows"].Token.Type == LazyJsonType.Array);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)((LazyJsonObject)resToken)["Rows"].Token;
            Assert.IsTrue(jsonArrayRows.Count == 3);
            Assert.IsTrue(jsonArrayRows[0].Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["State"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)((LazyJsonObject)jsonArrayRows[0])["State"].Token).Value == "Added");
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["Values"].Token.Type == LazyJsonType.Object);

            // Row1
            LazyJsonObject jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[0])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "A");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 10);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == true);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow1.ToString());

            // Row2
            jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[1])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "B");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 20);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == false);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow2.ToString());

            // Row3
            jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[2])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "C");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 30);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == true);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow3.ToString());
        }

        [TestMethod]
        public void TestSerializerDataTableMultipleRowWithSerializationOptions()
        {
            // Arrange
            LazyJsonToken resToken = null;

            DateTime dateTimeRow1 = DateTime.Now.AddDays(10);
            DateTime dateTimeRow2 = DateTime.Now.AddDays(20);
            DateTime dateTimeRow3 = DateTime.Now.AddDays(30);

            DataTable data = new DataTable("MyDataTable");
            data.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(String)), new DataColumn("Count", typeof(Int64)), new DataColumn("Active", typeof(Boolean)), new DataColumn("Date", typeof(DateTime)) });
            data.Rows.Add("A", 10, true, dateTimeRow1);
            data.Rows.Add("B", 20, false, dateTimeRow2);
            data.Rows.Add("C", 30, true, dateTimeRow3);

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = Globals.StringFormat.DateTime.ISO8601Z;
            serializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["MyDataTable"].Columns["Date"].Set(new LazyJsonSerializerDateTime());

            LazyJsonSerializerDataTable serializerDataTable = new LazyJsonSerializerDataTable();

            // Act
            resToken = serializerDataTable.Serialize(data, serializerOptions);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)resToken)["Rows"].Token.Type == LazyJsonType.Array);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)((LazyJsonObject)resToken)["Rows"].Token;
            Assert.IsTrue(jsonArrayRows.Count == 3);
            Assert.IsTrue(jsonArrayRows[0].Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["State"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(((LazyJsonString)((LazyJsonObject)jsonArrayRows[0])["State"].Token).Value == "Added");
            Assert.IsTrue(((LazyJsonObject)jsonArrayRows[0])["Values"].Token.Type == LazyJsonType.Object);

            // Row1
            LazyJsonObject jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[0])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "A");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 10);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == true);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow1.ToString(Globals.StringFormat.DateTime.ISO8601Z));

            // Row2
            jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[1])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "B");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 20);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == false);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow2.ToString(Globals.StringFormat.DateTime.ISO8601Z));

            // Row3
            jsonObjectRowsValues = (LazyJsonObject)((LazyJsonObject)jsonArrayRows[2])["Values"].Token;
            Assert.IsTrue(jsonObjectRowsValues["Original"].Token.Type == LazyJsonType.Null);
            Assert.IsTrue(jsonObjectRowsValues["Current"].Token.Type == LazyJsonType.Object);

            jsonObjectRowsValuesCurrent = ((LazyJsonObject)jsonObjectRowsValues["Current"].Token);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Id"].Token.Type == LazyJsonType.String);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Count"].Token.Type == LazyJsonType.Integer);
            Assert.IsTrue(jsonObjectRowsValuesCurrent["Active"].Token.Type == LazyJsonType.Boolean);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Id"].Token).Value == "C");
            Assert.IsTrue(((LazyJsonInteger)jsonObjectRowsValuesCurrent["Count"].Token).Value == 30);
            Assert.IsTrue(((LazyJsonBoolean)jsonObjectRowsValuesCurrent["Active"].Token).Value == true);
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date"].Token).Value == dateTimeRow3.ToString(Globals.StringFormat.DateTime.ISO8601Z));
        }
    }
}