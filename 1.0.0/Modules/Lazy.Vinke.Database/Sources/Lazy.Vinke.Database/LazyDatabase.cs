// LazyDatabase.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2020, November 06

using System;
using System.Xml;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Lazy.Vinke.Database
{
    public abstract class LazyDatabase
    {
        #region Variables

        private String connectionString;

        #endregion Variables

        #region Constructors

        public LazyDatabase()
        {
        }

        public LazyDatabase(String connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Open the connection with the database
        /// </summary>
        public abstract void OpenConnection();

        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        /// Begin a new transaction
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// Commit current transaction
        /// </summary>
        public abstract void CommitTransaction();

        /// <summary>
        /// Rollback current transaction
        /// </summary>
        public abstract void RollbackTransaction();

        /// <summary>
        /// Execute a sql sentence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 QueryExecute(String sql, Object[] values)
        {
            return QueryExecute(sql, values, LazyDatabaseQuery.Parameter.Extract(sql));
        }

        /// <summary>
        /// Execute a sql sentence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 QueryExecute(String sql, Object[] values, String[] parameters);

        /// <summary>
        /// Execute a sql sentence to retrieve a desired value
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <returns>The desired value from the sql sentence</returns>
        public virtual Object QueryValue(String sql, Object[] values)
        {
            return QueryValue(sql, values, LazyDatabaseQuery.Parameter.Extract(sql));
        }

        /// <summary>
        /// Execute a sql sentence to retrieve a desired value
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The desired value from the sql sentence</returns>
        public abstract Object QueryValue(String sql, Object[] values, String[] parameters);

        /// <summary>
        /// Execute a sql sentence to verify records existence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <returns>The records existence</returns>
        public virtual Boolean QueryFind(String sql, Object[] values)
        {
            return QueryFind(sql, values, LazyDatabaseQuery.Parameter.Extract(sql));
        }

        /// <summary>
        /// Execute a sql sentence to verify records existence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The records existence</returns>
        public abstract Boolean QueryFind(String sql, Object[] values, String[] parameters);

        /// <summary>
        /// Execute a sql sentence to retrieve a single record
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <returns>The record found</returns>
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values)
        {
            return QueryRecord(sql, tableName, values, LazyDatabaseQuery.Parameter.Extract(sql));
        }

        /// <summary>
        /// Execute a sql sentence to retrieve a single record
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The record found</returns>
        public abstract DataRow QueryRecord(String sql, String tableName, Object[] values, String[] parameters);

        /// <summary>
        /// Execute a sql sentence to retrieve many records
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <returns>The generated table from the sql sentence</returns>
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, Int32? limit = null)
        {
            return QueryTable(sql, tableName, values, LazyDatabaseQuery.Parameter.Extract(sql), limit);
        }

        /// <summary>
        /// Execute a sql sentence to retrieve many records
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The generated table from the sql sentence</returns>
        public abstract DataTable QueryTable(String sql, String tableName, Object[] values, String[] parameters, Int32? limit = null);

        /// <summary>
        /// Execute a sql stored procedure
        /// </summary>
        /// <param name="procedureName">The stored procedure name</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The stored procedure parameters values</param>
        /// <param name="parameters">The stored procedure parameters</param>
        /// <returns>The generated table from the stored procedure</returns>
        public abstract DataTable QueryProcedure(String procedureName, String tableName, Object[] values, String[] parameters);

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataRow">The datarow witch contains the key values</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable Select(String tableName, DataRow dataRow)
        {
            return Select(tableName, dataRow, DataRowState.Unchanged);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataRow">The datarow witch contains the key values</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable Select(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            return Select(tableName, dataRow, dataRowState, new String[] { "*" });
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataRow">The datarow witch contains the key values</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable Select(String tableName, DataRow dataRow, String[] returnFields)
        {
            return Select(tableName, dataRow, DataRowState.Unchanged, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataRow">The datarow witch contains the key values</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable Select(String tableName, DataRow dataRow, DataRowState dataRowState, String[] returnFields)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            if (dataRow.Table.PrimaryKey == null || dataRow.Table.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            String[] keyFields = null;
            Object[] keyValues = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                keyFields = new String[dataRow.Table.PrimaryKey.Length];
                keyValues = new Object[dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
                {
                    keyFields[columnIndex] = dataColumn.ColumnName;
                    keyValues[columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Select(tableName, keyFields, keyValues, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable Select(String tableName, String[] keyFields, Object[] keyValues)
        {
            return Select(tableName, keyFields, keyValues, new String[] { "*" });
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public abstract DataTable Select(String tableName, String[] keyFields, Object[] keyValues, String[] returnFields);

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataTable">The datatable containg the records to be selected</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable SelectAll(String tableName, DataTable dataTable)
        {
            return SelectAll(tableName, dataTable, DataRowState.Unchanged);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataTable">The datatable containg the records to be selected</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable SelectAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            return SelectAll(tableName, dataTable, dataRowState, new String[] { "*" });
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataTable">The datatable containg the records to be selected</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable SelectAll(String tableName, DataTable dataTable, String[] returnFields)
        {
            return SelectAll(tableName, dataTable, DataRowState.Unchanged, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataTable">The datatable containg the records to be selected</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable SelectAll(String tableName, DataTable dataTable, DataRowState dataRowState, String[] returnFields)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            if (dataTable.PrimaryKey == null || dataTable.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] keyFields = new String[dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.PrimaryKey)
            {
                keyFields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> keyValuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                keyValuesList.Add(new Object[dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    keyValuesList[keyValuesList.Count - 1][columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return SelectAll(tableName, keyFields, keyValuesList, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The datatable with selected records</returns>
        public virtual DataTable SelectAll(String tableName, String[] keyFields, List<Object[]> keyValuesList)
        {
            return SelectAll(tableName, keyFields, keyValuesList, new String[] { "*" });
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public abstract DataTable SelectAll(String tableName, String[] keyFields, List<Object[]> keyValuesList, String[] returnFields);

        /// <summary>
        /// Execute a sql insert sentence
        /// </summary>
        /// <param name="tableName">The table name to insert the record</param>
        /// <param name="dataRow">The datarow to be inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Insert(String tableName, DataRow dataRow)
        {
            return Insert(tableName, dataRow, DataRowState.Added);
        }

        /// <summary>
        /// Execute a sql insert sentence
        /// </summary>
        /// <param name="tableName">The table name to insert the record</param>
        /// <param name="dataRow">The datarow to be inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Insert(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            #endregion Validations

            String[] fields = null;
            Object[] values = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                fields = new String[dataRow.Table.Columns.Count];
                values = new Object[dataRow.Table.Columns.Count];
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    fields[columnIndex] = dataColumn.ColumnName;
                    values[columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Insert(tableName, fields, values);
        }

        /// <summary>
        /// Execute a sql insert sentence
        /// </summary>
        /// <param name="tableName">The table name to insert the record</param>
        /// <param name="fields">The table fields to be included on the insert sentence</param>
        /// <param name="values">The respective fields values to be inserted on the table</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 Insert(String tableName, String[] fields, Object[] values);

        /// <summary>
        /// Execute a sql insert sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 InsertAll(String tableName, DataTable dataTable)
        {
            return InsertAll(tableName, dataTable, DataRowState.Added);
        }

        /// <summary>
        /// Execute a sql insert sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 InsertAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] fields = new String[dataTable.Columns.Count];
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                fields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> valuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                valuesList.Add(new Object[dataTable.Columns.Count]);
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    valuesList[valuesList.Count - 1][columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return InsertAll(tableName, fields, valuesList);
        }

        /// <summary>
        /// Execute a sql insert sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to insert the records</param>
        /// <param name="fields">The table fields to be included on the insert sentence</param>
        /// <param name="valuesList">The list of respective fields values to be inserted on the table</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 InsertAll(String tableName, String[] fields, List<Object[]> valuesList);

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Indate(String tableName, DataRow dataRow)
        {
            return Indate(tableName, dataRow, DataRowState.Added);
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Indate(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            if (dataRow.Table.PrimaryKey == null || dataRow.Table.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            String[] nonKeyFields = null;
            Object[] nonKeyValues = null;
            String[] keyFields = null;
            Object[] keyValues = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                nonKeyFields = new String[dataRow.Table.Columns.Count - dataRow.Table.PrimaryKey.Length];
                nonKeyValues = new Object[dataRow.Table.Columns.Count - dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    Int32 pkIndex = 0;
                    for (pkIndex = 0; pkIndex < dataRow.Table.PrimaryKey.Length; pkIndex++)
                    {
                        if (dataRow.Table.PrimaryKey[pkIndex].ColumnName == dataColumn.ColumnName)
                            break;
                    }

                    if (pkIndex == dataRow.Table.PrimaryKey.Length)
                    {
                        nonKeyFields[columnIndex] = dataColumn.ColumnName;
                        nonKeyValues[columnIndex] = dataRow[dataColumn];
                        columnIndex++;
                    }
                }

                columnIndex = 0;
                keyFields = new String[dataRow.Table.PrimaryKey.Length];
                keyValues = new Object[dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
                {
                    keyFields[columnIndex] = dataColumn.ColumnName;
                    keyValues[columnIndex] = dataRow.RowState == DataRowState.Modified ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Indate(tableName, nonKeyFields, nonKeyValues, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="nonKeyFields">The table non key fields to be included on the update or insert sentence</param>
        /// <param name="nonKeyValues">The respective non key fields values to be updated or inserted on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Indate(String tableName, String[] nonKeyFields, Object[] nonKeyValues, String[] keyFields, Object[] keyValues)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (nonKeyFields == null || nonKeyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyFieldsNullOrZeroLenght);

            if (nonKeyValues == null || nonKeyValues.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyValuesNullOrZeroLenght);

            if (nonKeyFields.Length != nonKeyValues.Length)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyFieldsAndNonKeyValuesNotMatch);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            DataTable dataTable = Select(tableName, keyFields, keyValues, new String[] { "1" });

            if (dataTable.Rows.Count == 0)
            {
                String[] fields = new String[keyFields.Length + nonKeyFields.Length];
                keyFields.CopyTo(fields, 0);
                nonKeyFields.CopyTo(fields, keyFields.Length);

                String[] values = new String[keyValues.Length + nonKeyValues.Length];
                keyValues.CopyTo(values, 0);
                nonKeyValues.CopyTo(values, keyValues.Length);

                return Insert(tableName, fields, values);
            }
            else
            {
                return Update(tableName, nonKeyFields, nonKeyValues, keyFields, keyValues);
            }
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 IndateAll(String tableName, DataTable dataTable)
        {
            return IndateAll(tableName, dataTable, DataRowState.Added);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 IndateAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            if (dataTable.PrimaryKey == null || dataTable.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] nonKeyFields = new String[dataTable.Columns.Count - dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                Int32 pkIndex = 0;
                for (pkIndex = 0; pkIndex < dataTable.PrimaryKey.Length; pkIndex++)
                {
                    if (dataTable.PrimaryKey[pkIndex].ColumnName == dataColumn.ColumnName)
                        break;
                }

                if (pkIndex == dataTable.PrimaryKey.Length)
                {
                    nonKeyFields[columnIndex] = dataColumn.ColumnName;
                    columnIndex++;
                }
            }

            columnIndex = 0;
            String[] keyFields = new String[dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.PrimaryKey)
            {
                keyFields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> nonKeyValuesList = new List<Object[]>();
            List<Object[]> keyValuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                nonKeyValuesList.Add(new Object[dataTable.Columns.Count - dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    Int32 pkIndex = 0;
                    for (pkIndex = 0; pkIndex < dataTable.PrimaryKey.Length; pkIndex++)
                    {
                        if (dataTable.PrimaryKey[pkIndex].ColumnName == dataColumn.ColumnName)
                            break;
                    }

                    if (pkIndex == dataTable.PrimaryKey.Length)
                    {
                        nonKeyValuesList[nonKeyValuesList.Count - 1][columnIndex] = dataRow[dataColumn];
                        columnIndex++;
                    }
                }

                columnIndex = 0;
                keyValuesList.Add(new Object[dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    keyValuesList[keyValuesList.Count - 1][columnIndex] = dataRow.RowState == DataRowState.Modified ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return IndateAll(tableName, nonKeyFields, nonKeyValuesList, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="nonKeyFields">The table non key fields to be included on the update or insert sentence</param>
        /// <param name="nonKeyValuesList">The list of respective non key fields values to be updated or inserted on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 IndateAll(String tableName, String[] nonKeyFields, List<Object[]> nonKeyValuesList, String[] keyFields, List<Object[]> keyValuesList)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (nonKeyFields == null || nonKeyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyFieldsNullOrZeroLenght);

            if (nonKeyValuesList == null || nonKeyValuesList.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyValuesListNullOrZeroLenght);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (nonKeyValuesList.Count != keyValuesList.Count)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionNonKeyValuesListAndKeyValueListNotMatch);

            #endregion Validations

            String filterString = String.Empty;
            for (int i = 0; i < keyFields.Length; i++)
                filterString += keyFields[i] + " = '{" + i + "}' and ";
            filterString = filterString.Remove(filterString.Length - 5, 5); // Remove last " and "

            String[] fields = new String[keyFields.Length + nonKeyFields.Length];
            keyFields.CopyTo(fields, 0);
            nonKeyFields.CopyTo(fields, keyFields.Length);

            DataTable dataTable = SelectAll(tableName, keyFields, keyValuesList, fields);

            List<Object[]> valuesListInsert = new List<Object[]>();
            List<Object[]> valuesListUpdate = new List<Object[]>();
            List<Object[]> keyValuesListUpdate = new List<Object[]>();

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                String filter = String.Format(filterString, keyValuesList[listIndex]);
                DataRow[] dataRow = dataTable.Select(filter);

                if (dataRow.Length > 0)
                {
                    valuesListUpdate.Add(new Object[keyFields.Length + nonKeyFields.Length]);
                    keyValuesList[listIndex].CopyTo(valuesListUpdate[valuesListUpdate.Count - 1], 0);
                    nonKeyValuesList[listIndex].CopyTo(valuesListUpdate[valuesListUpdate.Count - 1], keyFields.Length);

                    keyValuesListUpdate.Add(keyValuesList[listIndex]);
                }
                else
                {
                    valuesListInsert.Add(new Object[keyFields.Length + nonKeyFields.Length]);
                    keyValuesList[listIndex].CopyTo(valuesListInsert[valuesListInsert.Count - 1], 0);
                    nonKeyValuesList[listIndex].CopyTo(valuesListInsert[valuesListInsert.Count - 1], keyFields.Length);
                }
            }

            Int32 rowsAffected = 0;

            if (valuesListInsert.Count > 0)
                rowsAffected += InsertAll(tableName, fields, valuesListInsert);

            if (valuesListUpdate.Count > 0)
                rowsAffected += UpdateAll(tableName, fields, valuesListUpdate, keyFields, keyValuesListUpdate);

            return rowsAffected;
        }

        /// <summary>
        /// Execute a sql update sentence
        /// </summary>
        /// <param name="tableName">The table name to update the record</param>
        /// <param name="dataRow">The datarow to be updated</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Update(String tableName, DataRow dataRow)
        {
            return Update(tableName, dataRow, DataRowState.Modified);
        }

        /// <summary>
        /// Execute a sql update sentence
        /// </summary>
        /// <param name="tableName">The table name to update the record</param>
        /// <param name="dataRow">The datarow to be updated</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Update(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            if (dataRow.Table.PrimaryKey == null || dataRow.Table.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            String[] fields = null;
            Object[] values = null;
            String[] keyFields = null;
            Object[] keyValues = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                fields = new String[dataRow.Table.Columns.Count];
                values = new Object[dataRow.Table.Columns.Count];
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    fields[columnIndex] = dataColumn.ColumnName;
                    values[columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }

                columnIndex = 0;
                keyFields = new String[dataRow.Table.PrimaryKey.Length];
                keyValues = new Object[dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
                {
                    keyFields[columnIndex] = dataColumn.ColumnName;
                    keyValues[columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Update(tableName, fields, values, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql update sentence
        /// </summary>
        /// <param name="tableName">The table name to update the record</param>
        /// <param name="fields">The table fields to be included on the update sentence</param>
        /// <param name="values">The respective fields values to be updated on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 Update(String tableName, String[] fields, Object[] values, String[] keyFields, Object[] keyValues);

        /// <summary>
        /// Execute a sql update sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to update the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 UpdateAll(String tableName, DataTable dataTable)
        {
            return UpdateAll(tableName, dataTable, DataRowState.Modified);
        }

        /// <summary>
        /// Execute a sql update sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to update the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 UpdateAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            if (dataTable.PrimaryKey == null || dataTable.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] fields = new String[dataTable.Columns.Count];
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                fields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            columnIndex = 0;
            String[] keyFields = new String[dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.PrimaryKey)
            {
                keyFields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> valuesList = new List<Object[]>();
            List<Object[]> keyValuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                valuesList.Add(new Object[dataTable.Columns.Count]);
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    valuesList[valuesList.Count - 1][columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }

                columnIndex = 0;
                keyValuesList.Add(new Object[dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    keyValuesList[keyValuesList.Count - 1][columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return UpdateAll(tableName, fields, valuesList, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql update sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to update the records</param>
        /// <param name="fields">The table fields to be included on the update sentence</param>
        /// <param name="valuesList">The list of respective fields values to be updated on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 UpdateAll(String tableName, String[] fields, List<Object[]> valuesList, String[] keyFields, List<Object[]> keyValuesList);

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Upsert(String tableName, DataRow dataRow)
        {
            return Upsert(tableName, dataRow, DataRowState.Modified);
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Upsert(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            if (dataRow.Table.PrimaryKey == null || dataRow.Table.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            String[] fields = null;
            Object[] values = null;
            String[] keyFields = null;
            Object[] keyValues = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                fields = new String[dataRow.Table.Columns.Count];
                values = new Object[dataRow.Table.Columns.Count];
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    fields[columnIndex] = dataColumn.ColumnName;
                    values[columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }

                columnIndex = 0;
                keyFields = new String[dataRow.Table.PrimaryKey.Length];
                keyValues = new Object[dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
                {
                    keyFields[columnIndex] = dataColumn.ColumnName;
                    keyValues[columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Upsert(tableName, fields, values, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="fields">The table fields to be included on the update or insert sentence</param>
        /// <param name="values">The respective fields values to be updated or inserted on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Upsert(String tableName, String[] fields, Object[] values, String[] keyFields, Object[] keyValues)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (values == null || values.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionValuesNullOrZeroLenght);

            if (fields.Length != values.Length)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionFieldsAndValuesNotMatch);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            DataTable dataTable = Select(tableName, keyFields, keyValues, new String[] { "1" });

            if (dataTable.Rows.Count == 1)
            {
                return Update(tableName, fields, values, keyFields, keyValues);
            }
            else
            {
                return Insert(tableName, fields, values);
            }
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 UpsertAll(String tableName, DataTable dataTable)
        {
            return UpsertAll(tableName, dataTable, DataRowState.Modified);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 UpsertAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            if (dataTable.PrimaryKey == null || dataTable.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] fields = new String[dataTable.Columns.Count];
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                fields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            columnIndex = 0;
            String[] keyFields = new String[dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.PrimaryKey)
            {
                keyFields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> valuesList = new List<Object[]>();
            List<Object[]> keyValuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                valuesList.Add(new Object[dataTable.Columns.Count]);
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    valuesList[valuesList.Count - 1][columnIndex] = dataRow[dataColumn];
                    columnIndex++;
                }

                columnIndex = 0;
                keyValuesList.Add(new Object[dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    keyValuesList[keyValuesList.Count - 1][columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return UpsertAll(tableName, fields, valuesList, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="fields">The table fields to be included on the update or insert sentence</param>
        /// <param name="valuesList">The list of respective fields values to be updated or inserted on the table</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 UpsertAll(String tableName, String[] fields, List<Object[]> valuesList, String[] keyFields, List<Object[]> keyValuesList)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (valuesList == null || valuesList.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionValuesListNullOrZeroLenght);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (valuesList.Count != keyValuesList.Count)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionValuesListAndKeyValueListNotMatch);

            #endregion Validations

            String filterString = String.Empty;
            for (int i = 0; i < keyFields.Length; i++)
                filterString += keyFields[i] + " = '{" + i + "}' and ";
            filterString = filterString.Remove(filterString.Length - 5, 5); // Remove last " and "

            DataTable dataTable = SelectAll(tableName, keyFields, keyValuesList, fields);

            List<Object[]> valuesListInsert = new List<Object[]>();
            List<Object[]> valuesListUpdate = new List<Object[]>();
            List<Object[]> keyValuesListUpdate = new List<Object[]>();

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                String filter = String.Format(filterString, keyValuesList[listIndex]);
                DataRow[] dataRow = dataTable.Select(filter);

                if (dataRow.Length > 0)
                {
                    valuesListUpdate.Add(valuesList[listIndex]);
                    keyValuesListUpdate.Add(keyValuesList[listIndex]);
                }
                else
                {
                    valuesListInsert.Add(valuesList[listIndex]);
                }
            }

            Int32 rowsAffected = 0;

            if (valuesListUpdate.Count > 0)
                rowsAffected += UpdateAll(tableName, fields, valuesListUpdate, keyFields, keyValuesListUpdate);

            if (valuesListInsert.Count > 0)
                rowsAffected += InsertAll(tableName, fields, valuesListInsert);

            return rowsAffected;
        }

        /// <summary>
        /// Execute a sql delete sentence
        /// </summary>
        /// <param name="tableName">The table name to delete the record</param>
        /// <param name="dataRow">The datarow to be deleted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Delete(String tableName, DataRow dataRow)
        {
            return Delete(tableName, dataRow, DataRowState.Deleted);
        }

        /// <summary>
        /// Execute a sql delete sentence
        /// </summary>
        /// <param name="tableName">The table name to delete the record</param>
        /// <param name="dataRow">The datarow to be deleted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Delete(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataRow == null || dataRow.Table.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataRowNullOrColumnZeroLenght);

            if (dataRow.Table.PrimaryKey == null || dataRow.Table.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            String[] keyFields = null;
            Object[] keyValues = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                Int32 columnIndex = 0;

                keyFields = new String[dataRow.Table.PrimaryKey.Length];
                keyValues = new Object[dataRow.Table.PrimaryKey.Length];
                foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
                {
                    keyFields[columnIndex] = dataColumn.ColumnName;
                    keyValues[columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return Delete(tableName, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql delete sentence
        /// </summary>
        /// <param name="tableName">The table name to delete the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 Delete(String tableName, String[] keyFields, Object[] keyValues);

        /// <summary>
        /// Execute a sql delete sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to delete the records</param>
        /// <param name="dataTable">The datatable containg the records to be deleted</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 DeleteAll(String tableName, DataTable dataTable)
        {
            return DeleteAll(tableName, dataTable, DataRowState.Deleted);
        }

        /// <summary>
        /// Execute a sql delete sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to delete the records</param>
        /// <param name="dataTable">The datatable containg the records to be deleted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 DeleteAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableNullOrRowsZeroLenght);

            if (dataTable.Columns.Count == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTableRowsColumnsZeroLenght);

            if (dataTable.PrimaryKey == null || dataTable.PrimaryKey.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionDataTablePrimaryKeyNullOrZeroLenght);

            #endregion Validations

            Int32 columnIndex = 0;

            String[] keyFields = new String[dataTable.PrimaryKey.Length];
            foreach (DataColumn dataColumn in dataTable.PrimaryKey)
            {
                keyFields[columnIndex] = dataColumn.ColumnName;
                columnIndex++;
            }

            List<Object[]> keyValuesList = new List<Object[]>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRowState.HasFlag(dataRow.RowState) == false)
                    continue;

                columnIndex = 0;
                keyValuesList.Add(new Object[dataTable.PrimaryKey.Length]);
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    keyValuesList[keyValuesList.Count - 1][columnIndex] = (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted) ? dataRow[dataColumn, DataRowVersion.Original] : dataRow[dataColumn];
                    columnIndex++;
                }
            }

            return DeleteAll(tableName, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql delete sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to delete the records</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 DeleteAll(String tableName, String[] keyFields, List<Object[]> keyValuesList);

        /// <summary>
        /// Increment a table field value by one
        /// </summary>
        /// <param name="tableName">The increment table</param>
        /// <param name="keyFields">The increment table key fields</param>
        /// <param name="keyValues">The increment table key values</param>
        /// <param name="incrementField">The increment field</param>
        /// <returns>The incremented value</returns>
        public virtual Int32 Increment(String tableName, String[] keyFields, Object[] keyValues, String incrementField)
        {
            return IncrementRange(tableName, keyFields, keyValues, incrementField, 1)[0];
        }

        /// <summary>
        /// Increment a table field value by range
        /// </summary>
        /// <param name="tableName">The increment table</param>
        /// <param name="keyFields">The increment table key fields</param>
        /// <param name="keyValues">The increment table key values</param>
        /// <param name="incrementField">The increment field</param>
        /// <param name="range">The increment value</param>
        /// <returns>The incremented values</returns>
        public virtual Int32[] IncrementRange(String tableName, String[] keyFields, Object[] keyValues, String incrementField, Int32 range)
        {
            #region Validations

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            if (String.IsNullOrEmpty(incrementField) == true)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionIncrementFieldNullOrEmpty);

            if (range < 1)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionIncrementRangeLowerThanOne);

            #endregion Validations

            String keyFieldsString = String.Empty;
            foreach (String keyField in keyFields)
                keyFieldsString += keyField + " = :" + keyField + " and ";
            keyFieldsString = keyFieldsString.Remove(keyFieldsString.Length - 5, 5);

            String sql = "select " + incrementField + " from " + tableName + " where " + keyFieldsString;
            Int32 lastIncrement = LazyConvert.ToInt32(QueryValue(sql, keyValues, keyFields), -1);

            String[] fields = null;
            Object[] values = null;

            if (lastIncrement == -1)
            {
                lastIncrement = 0;

                fields = new String[keyFields.Length + 1];
                keyFields.CopyTo(fields, 0);
                fields[fields.Length - 1] = incrementField;

                values = new Object[keyValues.Length + 1];
                keyValues.CopyTo(values, 0);
                values[values.Length - 1] = lastIncrement + range;

                Insert(tableName, fields, values);
            }
            else
            {
                fields = new String[] { incrementField };
                values = new Object[] { lastIncrement + range };

                Update(tableName, fields, values, keyFields, keyValues);
            }

            return Enumerable.Range(lastIncrement + 1, range).ToArray();
        }

        /// <summary>
        /// Convert a value to a database string format
        /// </summary>
        /// <param name="value">The value to be converted to the string format</param>
        /// <returns>The string format of the value</returns>
        protected abstract String ConvertToDatabaseStringFormat(Object value);

        /// <summary>
        /// Convert a system type to a database type
        /// </summary>
        /// <param name="systemType">The system type to be converted</param>
        /// <returns>The database type</returns>
        protected abstract Int32 ConvertToDatabaseType(Type systemType);

        #endregion Methods

        #region Properties

        public String ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        public abstract Boolean InTransaction
        {
            get;
        }

        #endregion Properties
    }
}