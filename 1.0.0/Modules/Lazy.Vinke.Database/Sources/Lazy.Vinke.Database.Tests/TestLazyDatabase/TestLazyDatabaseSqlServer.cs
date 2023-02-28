// TestLazyDatabaseSqlServer.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 27

using System;
using System.Data;
using System.Text;

using Lazy.Vinke;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.SqlServer;

namespace Lazy.Vinke.Database.Tests
{
    [TestClass]
    public class TestLazyDatabaseSqlServer : TestLazyDatabase
    {
        [TestMethod]
        public override void TestQueryExecute()
        {
            // Arrange
            String sqlCreate = @"create table QueryExecuteTest (Id integer, Name varchar(32))";
            String sqlInsert = @"insert into QueryExecuteTest (Id, Name) values (1, 'Lazy'),(2, 'Vinke')";
            String sqlUpdate = @"update QueryExecuteTest set Name = 'Database' where Id = :Id";
            String sqlDelete = @"delete from QueryExecuteTest where Id = :Id";
            String sqlDrop = @"drop table if exists QueryExecuteTest";

            // Act
            LazyDatabase database = new LazyDatabaseSqlServer(Globals.SqlServer.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            Int32 rowsInserted = database.QueryExecute(sqlInsert, null);
            Int32 rowsUpdated = database.QueryExecute(sqlUpdate, new Object[] { 2 });
            Int32 rowsDeleted = database.QueryExecute(sqlDelete, new Object[] { 1 });

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(rowsInserted == 2);
            Assert.IsTrue(rowsUpdated == 1);
            Assert.IsTrue(rowsDeleted == 1);
        }

        [TestMethod]
        public override void TestQueryValue()
        {
            // Arrange
            String sqlCreate = @"create table TestQueryValue (Id integer, Name varchar(32))";
            String sqlInsert = @"insert into TestQueryValue (Id, Name) values (1, 'Lazy'),(2, 'Vinke')";
            String sqlSelect = @"select Name from TestQueryValue where Id = :Id";
            String sqlDrop = @"drop table if exists TestQueryValue";

            // Act
            LazyDatabase database = new LazyDatabaseSqlServer(Globals.SqlServer.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            Int32 rowsInserted = database.QueryExecute(sqlInsert, null);
            Object value1 = database.QueryValue(sqlSelect, new Object[] { 1 });
            Object value2 = database.QueryValue(sqlSelect, new Object[] { 2 });

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(rowsInserted == 2);
            Assert.IsTrue((String)value1 == "Lazy");
            Assert.IsTrue((String)value2 == "Vinke");
        }

        [TestMethod]
        public override void TestQueryFind()
        {
            // Arrange
            String sqlCreate = @"create table TestQueryFind (Id integer, Name varchar(32))";
            String sqlInsert = @"insert into TestQueryFind (Id, Name) values (:Id, :Name)";
            String sqlSelect = @"select 1 from TestQueryFind where Id = :Id";
            String sqlDrop = @"drop table if exists TestQueryFind";

            // Act
            LazyDatabase database = new LazyDatabaseSqlServer(Globals.SqlServer.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            database.QueryExecute(sqlInsert, new Object[] { 1, "Lazy" });
            database.QueryExecute(sqlInsert, new Object[] { 2, "Vinke" });

            Boolean value1 = database.QueryFind(sqlSelect, new Object[] { 1 });
            Boolean value2 = database.QueryFind(sqlSelect, new Object[] { 2 });
            Boolean value3 = database.QueryFind(sqlSelect, new Object[] { 3 });
            Boolean value4 = database.QueryFind(sqlSelect, new Object[] { 4 });

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(value1);
            Assert.IsTrue(value2);
            Assert.IsFalse(value3);
            Assert.IsFalse(value4);
        }

        [TestMethod]
        public override void TestQueryRecord()
        {
            // Arrange
            String sqlCreate = @"create table TestQueryRecord (Id integer, Name varchar(32))";
            String sqlInsert = @"insert into TestQueryRecord (Id, Name) values (:Id, :Name)";
            String sqlSelect = @"select * from TestQueryRecord where Id = :Id";
            String sqlDrop = @"drop table if exists TestQueryRecord";

            // Act
            LazyDatabase database = new LazyDatabaseSqlServer(Globals.SqlServer.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            database.QueryExecute(sqlInsert, new Object[] { 1, "Lazy" });
            database.QueryExecute(sqlInsert, new Object[] { 2, "Vinke" });

            DataRow dataRow1 = database.QueryRecord(sqlSelect, "TestQueryRecord", new Object[] { 2 });
            DataRow dataRow2 = database.QueryRecord(sqlSelect, "TestQueryRecord", new Object[] { 3 });

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(Convert.ToInt32(dataRow1["Id"]) == 2);
            Assert.IsTrue(Convert.ToString(dataRow1["Name"]) == "Vinke");
            Assert.IsNull(dataRow2);
        }

        [TestMethod]
        public override void TestQueryTable()
        {
            // Arrange
            String sqlCreate = @"create table TestQueryTable (Parent integer, Id integer, Name varchar(32))";
            String sqlInsert = @"insert into TestQueryTable (Parent, Id, Name) values (:Parent, :Id, :Name)";
            String sqlSelect = @"select * from TestQueryTable where Parent = :Parent";
            String sqlDrop = @"drop table if exists TestQueryTable";

            // Act
            LazyDatabase database = new LazyDatabaseSqlServer(Globals.SqlServer.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            database.QueryExecute(sqlInsert, new Object[] { 1, 1, "Lazy" });
            database.QueryExecute(sqlInsert, new Object[] { 1, 2, "Vinke" });
            database.QueryExecute(sqlInsert, new Object[] { 1, 3, "Database" });
            database.QueryExecute(sqlInsert, new Object[] { 1, 4, "MySql" });
            database.QueryExecute(sqlInsert, new Object[] { 2, 1, "Tests" });

            DataTable dataTable1 = database.QueryTable(sqlSelect, "TestQueryTable", new Object[] { 1 });
            DataTable dataTable2 = database.QueryTable(sqlSelect, "TestQueryTable", new Object[] { 1 }, limit: 2);
            DataTable dataTable3 = database.QueryTable(sqlSelect, "TestQueryTable", new Object[] { 2 });
            DataTable dataTable4 = database.QueryTable(sqlSelect, "TestQueryTable", new Object[] { 3 });

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(dataTable1.Rows.Count == 4);
            Assert.IsTrue(dataTable2.Rows.Count == 2);
            Assert.IsTrue(dataTable3.Rows.Count == 1);
            Assert.IsTrue(dataTable4.Rows.Count == 0);
        }
    }
}