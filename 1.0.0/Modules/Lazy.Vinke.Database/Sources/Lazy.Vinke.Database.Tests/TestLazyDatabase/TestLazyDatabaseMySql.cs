// TestLazyDatabaseMySql.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 27

using System;
using System.Text;

using Lazy.Vinke;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.MySql;

namespace Lazy.Vinke.Database.Tests
{
    [TestClass]
    public class TestLazyDatabaseMySql : TestLazyDatabase
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
            LazyDatabase database = new LazyDatabaseMySql(Globals.MySql.ConnectionString);

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
            LazyDatabase database = new LazyDatabaseMySql(Globals.MySql.ConnectionString);

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
    }
}