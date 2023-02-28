// TestLazyDatabasePostgre.cs
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
using Lazy.Vinke.Database.Postgre;

namespace Lazy.Vinke.Database.Tests
{
    [TestClass]
    public class TestLazyDatabasePostgre : TestLazyDatabase
    {
        [TestMethod]
        public override void TestQueryExecute()
        {
            // Arrange
            String sqlCreate = @"create table QueryExecuteTest (Id integer, Name varchar(32))";
            String sqlInsert = @"insert into QueryExecuteTest (Id, Name) values (1, 'Lazy'),(2, 'Vinke')";
            String sqlUpdate = @"update QueryExecuteTest set Name = 'Database' where Id = 2";
            String sqlDelete = @"delete from QueryExecuteTest where Id = 1";
            String sqlDrop = @"drop table if exists QueryExecuteTest";

            // Act
            LazyDatabase database = new LazyDatabasePostgre(Globals.Postgre.ConnectionString);

            database.OpenConnection();
            database.QueryExecute(sqlCreate, null);

            Int32 rowsInserted = database.QueryExecute(sqlInsert, null);
            Int32 rowsUpdated = database.QueryExecute(sqlUpdate, null);
            Int32 rowsDeleted = database.QueryExecute(sqlDelete, null);

            database.QueryExecute(sqlDrop, null);
            database.CloseConnection();

            // Assert
            Assert.IsTrue(rowsInserted == 2);
            Assert.IsTrue(rowsUpdated == 1);
            Assert.IsTrue(rowsDeleted == 1);
        }
    }
}