// LazyDatabasePostgre.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, March 30

using System;
using System.Xml;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;

using Lazy.Vinke;
using Lazy.Vinke.Database;

namespace Lazy.Vinke.Database.Postgre
{
    public class LazyDatabasePostgre : LazyDatabase
    {
        #region Variables

        private NpgsqlCommand npgSqlCommand;
        private NpgsqlConnection npgSqlConnection;
        private NpgsqlDataAdapter npgSqlDataAdapter;
        private NpgsqlTransaction npgSqlTransaction;

        #endregion Variables

        #region Constructors

        public LazyDatabasePostgre()
        {
        }

        public LazyDatabasePostgre(String connectionString)
            : base(connectionString)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Open the connection with the database
        /// </summary>
        public override void OpenConnection()
        {
            #region Validations

            if (String.IsNullOrEmpty(this.ConnectionString) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionStringNullOrEmpty);

            if (this.npgSqlConnection != null && this.npgSqlConnection.State == ConnectionState.Open)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyOpen);

            #endregion Validations

            if (this.npgSqlConnection == null)
            {
                this.npgSqlConnection = new NpgsqlConnection(this.ConnectionString);
                this.npgSqlCommand = new NpgsqlCommand();
                this.npgSqlCommand.Connection = this.npgSqlConnection;
                this.npgSqlDataAdapter = new NpgsqlDataAdapter();
            }

            if (this.npgSqlConnection.State == ConnectionState.Closed)
                this.npgSqlConnection.Open();
        }

        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public override void CloseConnection()
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyClosed);

            #endregion Validations

            this.npgSqlTransaction = null;

            if (this.npgSqlDataAdapter != null)
            {
                this.npgSqlDataAdapter.Dispose();
                this.npgSqlDataAdapter = null;
            }

            if (this.npgSqlCommand != null)
            {
                this.npgSqlCommand.Dispose();
                this.npgSqlCommand = null;
            }

            if (this.npgSqlConnection != null)
            {
                if (this.npgSqlConnection.State == ConnectionState.Open)
                    this.npgSqlConnection.Close();

                this.npgSqlConnection.Dispose();
                this.npgSqlConnection = null;
            }
        }

        /// <summary>
        /// Begin a new transaction
        /// </summary>
        public override void BeginTransaction()
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.npgSqlTransaction != null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTransactionAlreadyStarted);

            #endregion Validations

            this.npgSqlTransaction = this.npgSqlConnection.BeginTransaction();
        }

        /// <summary>
        /// Commit current transaction
        /// </summary>
        public override void CommitTransaction()
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.npgSqlTransaction == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTransactionNotInitialized);

            #endregion Validations

            this.npgSqlTransaction.Commit();
            this.npgSqlTransaction = null;
        }

        /// <summary>
        /// Rollback current transaction
        /// </summary>
        public override void RollbackTransaction()
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.npgSqlTransaction == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTransactionNotInitialized);

            #endregion Validations

            this.npgSqlTransaction.Rollback();
            this.npgSqlTransaction = null;
        }

        /// <summary>
        /// Execute a sql sentence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The number of affected records</returns>
        public override Int32 QueryExecute(String sql, Object[] values, String[] parameters)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql sentence to retrieve a desired value
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The desired value from the sql sentence</returns>
        public override Object QueryValue(String sql, Object[] values, String[] parameters)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable("Table");
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0][0];

            return null;
        }

        /// <summary>
        /// Execute a sql sentence to verify records existence
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The records existence</returns>
        public override Boolean QueryFind(String sql, Object[] values, String[] parameters)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable("Table");
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Execute a sql sentence to retrieve a single record
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The record found</returns>
        public override DataRow QueryRecord(String sql, String tableName, Object[] values, String[] parameters)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionSqlNullOrEmpty);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        /// <summary>
        /// Execute a sql sentence to retrieve many records
        /// </summary>
        /// <param name="sql">The sql sentence to be executed</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The sql sentence parameters values</param>
        /// <param name="parameters">The sql sentence parameters</param>
        /// <returns>The generated table from the sql sentence</returns>
        public override DataTable QueryTable(String sql, String tableName, Object[] values, String[] parameters, Int32? limit = null)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionSqlNullOrEmpty);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            if (limit != null && limit >= 0)
                sql = String.Format("select * from ({0}) as {1} limit {2}", sql, tableName, limit);

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Execute a sql stored procedure
        /// </summary>
        /// <param name="procedureName">The stored procedure name</param>
        /// <param name="tableName">The desired table name</param>
        /// <param name="values">The stored procedure parameters values</param>
        /// <param name="parameters">The stored procedure parameters</param>
        /// <returns>The generated table from the stored procedure</returns>
        public override DataTable QueryProcedure(String procedureName, String tableName, Object[] values, String[] parameters)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(procedureName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionProcedureNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                    NpgsqlParameter npgSqlParameter = new NpgsqlParameter(parameters[i], dbType);
                    npgSqlParameter.Value = values[i];

                    this.npgSqlCommand.Parameters.Add(npgSqlParameter);
                }
            }

            this.npgSqlCommand.CommandText = procedureName;
            this.npgSqlCommand.CommandType = CommandType.StoredProcedure;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataRow">The datarow witch contains the key values</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public override DataTable Select(String tableName, DataRow dataRow, DataRowState dataRowState, String[] returnFields)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Select(tableName, dataRow, dataRowState, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public override DataTable Select(String tableName, String[] keyFields, Object[] keyValues, String[] returnFields)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String returnFieldString = String.Empty;
            String whereKeyFieldString = String.Empty;

            foreach (String returnField in returnFields)
                returnFieldString += returnField + ",";
            returnFieldString = returnFieldString.Remove(returnFieldString.Length - 1, 1);

            foreach (String keyField in keyFields)
                whereKeyFieldString += keyField + " = @" + keyField + " and ";
            whereKeyFieldString = whereKeyFieldString.Remove(whereKeyFieldString.Length - 5, 5);

            for (int i = 0; i < keyValues.Length; i++)
            {
                NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                NpgsqlParameter mySqlParameter = new NpgsqlParameter(keyFields[i], dbType);
                mySqlParameter.Value = keyValues[i];

                this.npgSqlCommand.Parameters.Add(mySqlParameter);
            }

            String sql = "select " + returnFieldString + " from " + tableName + " where " + whereKeyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="dataTable">The datatable containg the records to be selected</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public override DataTable SelectAll(String tableName, DataTable dataTable, DataRowState dataRowState, String[] returnFields)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.SelectAll(tableName, dataTable, dataRowState, returnFields);
        }

        /// <summary>
        /// Execute a sql select sentence
        /// </summary>
        /// <param name="tableName">The table name to select the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <param name="returnFields">The fields to be returned by the select sentence</param>
        /// <returns>The datatable with selected records</returns>
        public override DataTable SelectAll(String tableName, String[] keyFields, List<Object[]> keyValuesList, String[] returnFields)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String returnFieldString = String.Empty;
            String tempTableKeyFieldString = String.Empty;
            String tempTableKeyValueString = String.Empty;
            String onClauseKeyFieldString = String.Empty;

            foreach (String returnField in returnFields)
                returnFieldString += returnField == "*" ? "T.*," : returnField + ",";
            returnFieldString = returnFieldString.Remove(returnFieldString.Length - 1, 1);

            foreach (String keyField in keyFields)
            {
                tempTableKeyFieldString += "Key" + keyField + ",";
                onClauseKeyFieldString += "T." + keyField + " = " + "Temp.Key" + keyField + " and ";
            }
            tempTableKeyFieldString = tempTableKeyFieldString.Remove(tempTableKeyFieldString.Length - 1, 1);
            onClauseKeyFieldString = onClauseKeyFieldString.Remove(onClauseKeyFieldString.Length - 5, 5);

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                #endregion Validations

                tempTableKeyValueString += "(";
                for (int keyIndex = 0; keyIndex < keyValuesList[listIndex].Length; keyIndex++)
                    tempTableKeyValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][keyIndex]) + ",";
                tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 1, 1);
                tempTableKeyValueString += "),";
            }
            tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 1, 1);

            String sql = "select " + returnFieldString + " from " + tableName + " as T join (values " + tempTableKeyValueString + ") as Temp (" + tempTableKeyFieldString + ") on " + onClauseKeyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.npgSqlDataAdapter.SelectCommand = this.npgSqlCommand;
            this.npgSqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Execute a sql insert sentence
        /// </summary>
        /// <param name="tableName">The table name to insert the record</param>
        /// <param name="dataRow">The datarow to be inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Insert(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Insert(tableName, dataRow, dataRowState);
        }

        /// <summary>
        /// Execute a sql insert sentence
        /// </summary>
        /// <param name="tableName">The table name to insert the record</param>
        /// <param name="fields">The table fields to be included on the insert sentence</param>
        /// <param name="values">The respective fields values to be inserted on the table</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Insert(String tableName, String[] fields, Object[] values)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (values == null || values.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLenght);

            if (fields.Length != values.Length)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsAndValuesNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String fieldString = String.Empty;
            String parameterString = String.Empty;

            foreach (String field in fields)
                fieldString += "" + field + ",";
            fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            for (int i = 0; i < values.Length; i++)
            {
                NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                NpgsqlParameter npgSqlParameter = new NpgsqlParameter(fields[i], dbType);
                npgSqlParameter.Value = values[i];

                this.npgSqlCommand.Parameters.Add(npgSqlParameter);

                parameterString += "@" + fields[i] + ",";
            }
            parameterString = parameterString.Remove(parameterString.Length - 1, 1);

            String sql = "insert into " + tableName + " (" + fieldString + ") values (" + parameterString + ")";

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql insert sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 InsertAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.InsertAll(tableName, dataTable, dataRowState);
        }

        /// <summary>
        /// Execute a sql insert sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to insert the records</param>
        /// <param name="fields">The table fields to be included on the insert sentence</param>
        /// <param name="valuesList">The list of respective fields values to be inserted on the table</param>
        /// <returns>The number of affected records</returns>
        public override Int32 InsertAll(String tableName, String[] fields, List<Object[]> valuesList)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (valuesList == null || valuesList.Count == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesListNullOrZeroLenght);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String fieldString = String.Empty;
            String valuesListString = String.Empty;

            foreach (String field in fields)
                fieldString += "" + field + ",";
            fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            foreach (Object[] values in valuesList)
            {
                #region Validations

                if (fields.Length != values.Length)
                    throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsAndValuesNotMatch);

                #endregion Validations

                valuesListString += "(";
                for (int i = 0; i < values.Length; i++)
                    valuesListString += ConvertToDatabaseStringFormat(values[i]) + ",";
                valuesListString = valuesListString.Remove(valuesListString.Length - 1, 1);
                valuesListString += "),";
            }
            valuesListString = valuesListString.Remove(valuesListString.Length - 1, 1);

            String sql = "insert into " + tableName + " (" + fieldString + ") values " + valuesListString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Indate(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Indate(tableName, dataRow, dataRowState);
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
        public override Int32 Indate(String tableName, String[] nonKeyFields, Object[] nonKeyValues, String[] keyFields, Object[] keyValues)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Indate(tableName, nonKeyFields, nonKeyValues, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 IndateAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.IndateAll(tableName, dataTable, dataRowState);
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
        public override Int32 IndateAll(String tableName, String[] nonKeyFields, List<Object[]> nonKeyValuesList, String[] keyFields, List<Object[]> keyValuesList)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.IndateAll(tableName, nonKeyFields, nonKeyValuesList, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql update sentence
        /// </summary>
        /// <param name="tableName">The table name to update the record</param>
        /// <param name="dataRow">The datarow to be updated</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Update(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Update(tableName, dataRow, dataRowState);
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
        public override Int32 Update(String tableName, String[] fields, Object[] values, String[] keyFields, Object[] keyValues)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (values == null || values.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLenght);

            if (fields.Length != values.Length)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsAndValuesNotMatch);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String fieldString = String.Empty;
            String keyFieldString = String.Empty;

            foreach (String field in fields)
                fieldString += field + " = @" + field + ", ";
            fieldString = fieldString.Remove(fieldString.Length - 2, 2);

            foreach (String keyField in keyFields)
                keyFieldString += keyField + " = @key" + keyField + " and ";
            keyFieldString = keyFieldString.Remove(keyFieldString.Length - 5, 5);

            for (int i = 0; i < values.Length; i++)
            {
                NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(values[i].GetType());
                NpgsqlParameter npgSqlParameter = new NpgsqlParameter(fields[i], dbType);
                npgSqlParameter.Value = values[i];

                this.npgSqlCommand.Parameters.Add(npgSqlParameter);
            }

            for (int i = 0; i < keyValues.Length; i++)
            {
                NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                NpgsqlParameter npgSqlParameter = new NpgsqlParameter("key" + keyFields[i], dbType);
                npgSqlParameter.Value = keyValues[i];

                this.npgSqlCommand.Parameters.Add(npgSqlParameter);
            }

            String sql = "update " + tableName + " set " + fieldString + " where " + keyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql update sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to update the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 UpdateAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.UpdateAll(tableName, dataTable, dataRowState);
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
        public override Int32 UpdateAll(String tableName, String[] fields, List<Object[]> valuesList, String[] keyFields, List<Object[]> keyValuesList)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (valuesList == null || valuesList.Count == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesListNullOrZeroLenght);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (valuesList.Count != keyValuesList.Count)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionValuesListAndKeyValueListNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String setFieldString = String.Empty;
            String tempTableValueString = String.Empty;
            String tempTableFieldString = String.Empty;
            String whereKeyFieldString = String.Empty;

            foreach (String field in fields)
                setFieldString += field + " = Temp." + field + ", ";
            setFieldString = setFieldString.Remove(setFieldString.Length - 2, 2);

            for (int listIndex = 0; listIndex < valuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                if (fields.Length != valuesList[listIndex].Length)
                    throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionFieldsAndValuesNotMatch);

                #endregion Validations

                tempTableValueString += "(";

                for (int columnIndex = 0; columnIndex < keyValuesList[listIndex].Length; columnIndex++)
                    tempTableValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][columnIndex]) + ", ";

                for (int columnIndex = 0; columnIndex < valuesList[listIndex].Length; columnIndex++)
                    tempTableValueString += ConvertToDatabaseStringFormat(valuesList[listIndex][columnIndex]) + ", ";

                tempTableValueString = tempTableValueString.Remove(tempTableValueString.Length - 2, 2);
                tempTableValueString += "),";
            }
            tempTableValueString = tempTableValueString.Remove(tempTableValueString.Length - 1, 1);

            foreach (String keyField in keyFields)
                tempTableFieldString += "Key" + keyField + ", ";
            foreach (String field in fields)
                tempTableFieldString += field + ", ";
            tempTableFieldString = tempTableFieldString.Remove(tempTableFieldString.Length - 2, 2);

            foreach (String keyField in keyFields)
                whereKeyFieldString += "T." + keyField + " = Temp.Key" + keyField + " and ";
            whereKeyFieldString = whereKeyFieldString.Remove(whereKeyFieldString.Length - 5, 5);

            String sql = "update " + tableName + " as T set " + setFieldString + " from (values " + tempTableValueString + ") as Temp(" + tempTableFieldString + ") where " + whereKeyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql update or insert sentence depending on record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the record</param>
        /// <param name="dataRow">The datarow to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Upsert(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Upsert(tableName, dataRow, dataRowState);
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
        public override Int32 Upsert(String tableName, String[] fields, Object[] values, String[] keyFields, Object[] keyValues)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Upsert(tableName, fields, values, keyFields, keyValues);
        }

        /// <summary>
        /// Execute a sql update or insert sentence for many records depending on each record existence
        /// </summary>
        /// <param name="tableName">The table name to update or insert the records</param>
        /// <param name="dataTable">The datatable containg the records to be updated or inserted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 UpsertAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.UpsertAll(tableName, dataTable, dataRowState);
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
        public override Int32 UpsertAll(String tableName, String[] fields, List<Object[]> valuesList, String[] keyFields, List<Object[]> keyValuesList)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.UpsertAll(tableName, fields, valuesList, keyFields, keyValuesList);
        }

        /// <summary>
        /// Execute a sql delete sentence
        /// </summary>
        /// <param name="tableName">The table name to delete the record</param>
        /// <param name="dataRow">The datarow to be deleted</param>
        /// <param name="dataRowState">The datarow state to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Delete(String tableName, DataRow dataRow, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.Delete(tableName, dataRow, dataRowState);
        }

        /// <summary>
        /// Execute a sql delete sentence
        /// </summary>
        /// <param name="tableName">The table name to delete the record</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValues">The respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Delete(String tableName, String[] keyFields, Object[] keyValues)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String whereKeyFieldString = String.Empty;

            foreach (String keyField in keyFields)
                whereKeyFieldString += keyField + " = @" + keyField + " and ";
            whereKeyFieldString = whereKeyFieldString.Remove(whereKeyFieldString.Length - 5, 5); // Remove last " and "

            for (int i = 0; i < keyValues.Length; i++)
            {
                NpgsqlDbType dbType = (NpgsqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                NpgsqlParameter npgSqlParameter = new NpgsqlParameter(keyFields[i], dbType);
                npgSqlParameter.Value = keyValues[i];

                this.npgSqlCommand.Parameters.Add(npgSqlParameter);
            }

            String sql = "delete from " + tableName + " where " + whereKeyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a sql delete sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to delete the records</param>
        /// <param name="dataTable">The datatable containg the records to be deleted</param>
        /// <param name="dataRowState">The datarow state on datatable to be considered</param>
        /// <returns>The number of affected records</returns>
        public override Int32 DeleteAll(String tableName, DataTable dataTable, DataRowState dataRowState)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.DeleteAll(tableName, dataTable, dataRowState);
        }

        /// <summary>
        /// Execute a sql delete sentence for many records
        /// </summary>
        /// <param name="tableName">The table name to delete the records</param>
        /// <param name="keyFields">The table key fields</param>
        /// <param name="keyValuesList">The list of respective key fields values</param>
        /// <returns>The number of affected records</returns>
        public override Int32 DeleteAll(String tableName, String[] keyFields, List<Object[]> keyValuesList)
        {
            #region Validations

            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            #endregion Validations

            this.npgSqlCommand.Parameters.Clear();

            String tempTableKeyValueString = String.Empty;
            String whereClauseKeyFieldString = String.Empty;

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                #endregion Validations

                tempTableKeyValueString += "select ";
                for (int keyIndex = 0; keyIndex < keyValuesList[listIndex].Length; keyIndex++)
                    tempTableKeyValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][keyIndex]) + " Key" + keyFields[keyIndex] + ", ";
                tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 2, 2);
                tempTableKeyValueString += " union all ";
            }
            tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 11, 11);

            foreach (String keyField in keyFields)
                whereClauseKeyFieldString += "T." + keyField + " = " + "Temp.Key" + keyField + " and ";
            whereClauseKeyFieldString = whereClauseKeyFieldString.Remove(whereClauseKeyFieldString.Length - 5, 5);

            String sql = "delete from " + tableName + " T using (" + tempTableKeyValueString + ") Temp where " + whereClauseKeyFieldString;

            this.npgSqlCommand.CommandText = sql;
            this.npgSqlCommand.CommandType = CommandType.Text;
            this.npgSqlCommand.Transaction = this.npgSqlTransaction;

            return this.npgSqlCommand.ExecuteNonQuery();
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
        public override Int32[] IncrementRange(String tableName, String[] keyFields, Object[] keyValues, String incrementField, Int32 range)
        {
            if (this.npgSqlConnection == null || this.npgSqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            return base.IncrementRange(tableName, keyFields, keyValues, incrementField, range);
        }

        /// <summary>
        /// Initialize database string format
        /// </summary>
        protected override void InitializeDatabaseStringFormat()
        {
            this.StringFormat.Char = "'{0}'";
            this.StringFormat.Byte = "{0}";
            this.StringFormat.Int16 = "{0}";
            this.StringFormat.Int32 = "{0}";
            this.StringFormat.Int64 = "{0}";
            this.StringFormat.UInt16 = "{0}";
            this.StringFormat.UInt32 = "{0}";
            this.StringFormat.UInt64 = "{0}";
            this.StringFormat.String = "'{0}'";
            this.StringFormat.Boolean = "'{0}'";
            this.StringFormat.ByteArray = "'{0}'";
            this.StringFormat.DateTime = "cast('{0:yyyy-MM-ddTHH:mm:ssZ}' as timestamp)";
            this.StringFormat.Decimal = "{0:f4}";
            this.StringFormat.Float = "{0:f4}";
            this.StringFormat.Double = "{0:f4}";
        }

        /// <summary>
        /// Convert a value to a database string format
        /// </summary>
        /// <param name="value">The value to be converted to the string format</param>
        /// <returns>The string format of the value</returns>
        protected override String ConvertToDatabaseStringFormat(Object value)
        {
            return base.ConvertToDatabaseStringFormat(value);
        }

        /// <summary>
        /// Convert a system type to a database type
        /// </summary>
        /// <param name="systemType">The system type to be converted</param>
        /// <returns>The database type</returns>
        protected override Int32 ConvertToDatabaseType(Type systemType)
        {
            if (systemType == typeof(Char)) return (Int32)NpgsqlDbType.Char;
            if (systemType == typeof(Byte)) return (Int32)NpgsqlDbType.Smallint;
            if (systemType == typeof(Int16)) return (Int32)NpgsqlDbType.Smallint;
            if (systemType == typeof(Int32)) return (Int32)NpgsqlDbType.Integer;
            if (systemType == typeof(Int64)) return (Int32)NpgsqlDbType.Bigint;
            if (systemType == typeof(UInt16)) return (Int32)NpgsqlDbType.Smallint;
            if (systemType == typeof(UInt32)) return (Int32)NpgsqlDbType.Integer;
            if (systemType == typeof(UInt64)) return (Int32)NpgsqlDbType.Bigint;
            if (systemType == typeof(String)) return (Int32)NpgsqlDbType.Varchar;
            if (systemType == typeof(Boolean)) return (Int32)NpgsqlDbType.Bit;
            if (systemType == typeof(Byte[])) return (Int32)NpgsqlDbType.Bytea;
            if (systemType == typeof(DateTime)) return (Int32)NpgsqlDbType.Timestamp;
            if (systemType == typeof(Decimal)) return (Int32)NpgsqlDbType.Numeric;
            if (systemType == typeof(float)) return (Int32)NpgsqlDbType.Real;
            if (systemType == typeof(double)) return (Int32)NpgsqlDbType.Double;

            return (Int32)NpgsqlDbType.Varchar;
        }

        #endregion Methods

        #region Properties

        public override Boolean InTransaction
        {
            get { return this.npgSqlConnection != null && this.npgSqlTransaction != null; }
        }

        #endregion Properties
    }
}
