// TestLazyJsonSerializerDataSet.cs
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
    public class TestLazyJsonSerializerDataSet
    {
        [TestMethod]
        public void TestSerializerDataSetDataNull()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = null;
            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDataSetDataNotMatch()
        {
            // Arrange
            LazyJsonToken resToken = null;
            Object data = false;
            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Null);
        }

        [TestMethod]
        public void TestSerializerDataSetEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DataSet data = new DataSet("MyDataSet");
            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)resToken)["Tables"].Token.Type == LazyJsonType.Object);
            Assert.IsTrue(((LazyJsonObject)((LazyJsonObject)resToken)["Tables"].Token).Count == 0);
        }

        [TestMethod]
        public void TestSerializerDataSetTableEmpty()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DataTable dataTable = new DataTable("MyDataTable");
            DataSet data = new DataSet("MyDataSet");
            data.Tables.Add(dataTable);

            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)resToken;
            Assert.IsTrue(jsonObjectDataSet["Tables"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetTables = ((LazyJsonObject)jsonObjectDataSet["Tables"].Token);
            Assert.IsTrue(jsonObjectDataSetTables.Count == 1);
            Assert.IsTrue(jsonObjectDataSetTables["MyDataTable"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetMyDataTable = ((LazyJsonObject)jsonObjectDataSetTables["MyDataTable"].Token);
            Assert.IsTrue(jsonObjectDataSetMyDataTable["Rows"].Token.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token).Count == 0);
        }

        [TestMethod]
        public void TestSerializerDataSetTableSingleRow()
        {
            // Arrange
            LazyJsonToken resToken = null;
            DataTable dataTable = new DataTable("MyDataTable");
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(Int16)), new DataColumn("Code", typeof(String)), new DataColumn("Value", typeof(Decimal)) });
            dataTable.Rows.Add(1, "01", 10.4m);
            dataTable.AcceptChanges();

            DataSet data = new DataSet("MyDataSet");
            data.Tables.Add(dataTable);

            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)resToken;
            Assert.IsTrue(jsonObjectDataSet["Tables"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetTables = ((LazyJsonObject)jsonObjectDataSet["Tables"].Token);
            Assert.IsTrue(jsonObjectDataSetTables.Count == 1);
            Assert.IsTrue(jsonObjectDataSetTables["MyDataTable"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetMyDataTable = ((LazyJsonObject)jsonObjectDataSetTables["MyDataTable"].Token);
            Assert.IsTrue(jsonObjectDataSetMyDataTable["Rows"].Token.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token).Count == 1);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token;
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
        public void TestSerializerDataSetTableMultipleRow()
        {
            // Arrange
            LazyJsonToken resToken = null;

            DateTime dateTimeRow1 = DateTime.Now.AddDays(1);
            DateTime dateTimeRow2 = DateTime.Now.AddDays(2);
            DateTime dateTimeRow3 = DateTime.Now.AddDays(3);

            DataTable dataTable = new DataTable("MyDataTable");
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(String)), new DataColumn("Count", typeof(Int64)), new DataColumn("Active", typeof(Boolean)), new DataColumn("Date", typeof(DateTime)) });
            dataTable.Rows.Add("A", 10, true, dateTimeRow1);
            dataTable.Rows.Add("B", 20, false, dateTimeRow2);
            dataTable.Rows.Add("C", 30, true, dateTimeRow3);

            DataSet data = new DataSet("MyDataSet");
            data.Tables.Add(dataTable);

            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)resToken;
            Assert.IsTrue(jsonObjectDataSet["Tables"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetTables = ((LazyJsonObject)jsonObjectDataSet["Tables"].Token);
            Assert.IsTrue(jsonObjectDataSetTables.Count == 1);
            Assert.IsTrue(jsonObjectDataSetTables["MyDataTable"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetMyDataTable = ((LazyJsonObject)jsonObjectDataSetTables["MyDataTable"].Token);
            Assert.IsTrue(jsonObjectDataSetMyDataTable["Rows"].Token.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token).Count == 3);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token;
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
        public void TestSerializerDataSetTableMultipleRowWithSerializationOptions()
        {
            // Arrange
            LazyJsonToken resToken = null;

            DateTime dateTime1Row1 = DateTime.Now.AddDays(10);
            DateTime dateTime1Row2 = DateTime.Now.AddDays(20);
            DateTime dateTime1Row3 = DateTime.Now.AddDays(30);
            DateTime dateTime2Row1 = DateTime.Now.AddDays(15);
            DateTime dateTime2Row2 = DateTime.Now.AddDays(25);
            DateTime dateTime2Row3 = DateTime.Now.AddDays(35);

            DataTable dataTable = new DataTable("MyDataTable");
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(String)), new DataColumn("Count", typeof(Int64)), new DataColumn("Active", typeof(Boolean)), new DataColumn("Date1", typeof(DateTime)), new DataColumn("Date2", typeof(DateTime)) });
            dataTable.Rows.Add("A", 10, true, dateTime1Row1, dateTime2Row1);
            dataTable.Rows.Add("B", 20, false, dateTime1Row2, dateTime2Row2);
            dataTable.Rows.Add("C", 30, true, dateTime1Row3, dateTime2Row3);

            DataSet data = new DataSet("MyDataSet");
            data.Tables.Add(dataTable);

            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = Globals.StringFormat.DateTime.ISO8601Z;
            serializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["MyDataTable"].Columns["Date1"].Set(new LazyJsonSerializerDateTime());

            LazyJsonSerializerDataSet serializerDataSet = new LazyJsonSerializerDataSet();

            // Act
            resToken = serializerDataSet.Serialize(data, serializerOptions);

            // Assert
            Assert.IsTrue(resToken.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)resToken;
            Assert.IsTrue(jsonObjectDataSet["Tables"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetTables = ((LazyJsonObject)jsonObjectDataSet["Tables"].Token);
            Assert.IsTrue(jsonObjectDataSetTables.Count == 1);
            Assert.IsTrue(jsonObjectDataSetTables["MyDataTable"].Token.Type == LazyJsonType.Object);

            LazyJsonObject jsonObjectDataSetMyDataTable = ((LazyJsonObject)jsonObjectDataSetTables["MyDataTable"].Token);
            Assert.IsTrue(jsonObjectDataSetMyDataTable["Rows"].Token.Type == LazyJsonType.Array);
            Assert.IsTrue(((LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token).Count == 3);

            LazyJsonArray jsonArrayRows = (LazyJsonArray)jsonObjectDataSetMyDataTable["Rows"].Token;
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
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date1"].Token).Value == dateTime1Row1.ToString(Globals.StringFormat.DateTime.ISO8601Z));
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date2"].Token).Value == dateTime2Row1.ToString(Globals.StringFormat.DateTime.ISO8601Z));

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
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date1"].Token).Value == dateTime1Row2.ToString(Globals.StringFormat.DateTime.ISO8601Z));
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date2"].Token).Value == dateTime2Row2.ToString(Globals.StringFormat.DateTime.ISO8601Z));

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
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date1"].Token).Value == dateTime1Row3.ToString(Globals.StringFormat.DateTime.ISO8601Z));
            Assert.IsTrue(((LazyJsonString)jsonObjectRowsValuesCurrent["Date2"].Token).Value == dateTime2Row3.ToString(Globals.StringFormat.DateTime.ISO8601Z));
        }
    }
}