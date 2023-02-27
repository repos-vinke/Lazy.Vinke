// LazyDatabaseMySql.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2020, December 01

using System;
using System.Xml;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

using MySql.Data;
using MySql.Data.MySqlClient;

using Lazy.Vinke;
using Lazy.Vinke.Database;

namespace Lazy.Vinke.Database.MySql
{
    public class LazyDatabaseMySql : LazyDatabase
    {
        #region Variables

        private MySqlCommand mySqlCommand;
        private MySqlConnection mySqlConnection;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlTransaction mySqlTransaction;

        #endregion Variables

        #region Constructors

        public LazyDatabaseMySql()
        {
        }

        public LazyDatabaseMySql(String connectionString)
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
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionStringNullOrEmpty);

            if (this.mySqlConnection != null && this.mySqlConnection.State == ConnectionState.Open)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionAlreadyOpen);

            #endregion Validations

            if (this.mySqlConnection == null)
            {
                this.mySqlConnection = new MySqlConnection(this.ConnectionString);
                this.mySqlCommand = new MySqlCommand();
                this.mySqlCommand.Connection = this.mySqlConnection;
                this.mySqlDataAdapter = new MySqlDataAdapter();
            }

            if (this.mySqlConnection.State == ConnectionState.Closed)
                this.mySqlConnection.Open();
        }

        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public override void CloseConnection()
        {
            #region Validations

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionAlreadyClosed);

            #endregion Validations

            this.mySqlTransaction = null;

            if (this.mySqlDataAdapter != null)
            {
                this.mySqlDataAdapter.Dispose();
                this.mySqlDataAdapter = null;
            }

            if (this.mySqlCommand != null)
            {
                this.mySqlCommand.Dispose();
                this.mySqlCommand = null;
            }

            if (this.mySqlConnection != null)
            {
                if (this.mySqlConnection.State == ConnectionState.Open)
                    this.mySqlConnection.Close();

                this.mySqlConnection.Dispose();
                this.mySqlConnection = null;
            }
        }

        /// <summary>
        /// Begin a new transaction
        /// </summary>
        public override void BeginTransaction()
        {
            #region Validations

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (this.mySqlTransaction != null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTransactionAlreadyStarted);

            #endregion Validations

            this.mySqlTransaction = this.mySqlConnection.BeginTransaction();
        }

        /// <summary>
        /// Commit current transaction
        /// </summary>
        public override void CommitTransaction()
        {
            #region Validations

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (this.mySqlTransaction == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTransactionNotInitialized);

            #endregion Validations

            this.mySqlTransaction.Commit();
            this.mySqlTransaction = null;
        }

        /// <summary>
        /// Rollback current transaction
        /// </summary>
        public override void RollbackTransaction()
        {
            #region Validations

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (this.mySqlTransaction == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTransactionNotInitialized);

            #endregion Validations

            this.mySqlTransaction.Rollback();
            this.mySqlTransaction = null;
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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable("Table");
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionSqlNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable("Table");
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionSqlNullOrEmpty);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(sql) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionSqlNullOrEmpty);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            sql = LazyDatabaseQuery.Replace(sql, new String[] { ":" }, new String[] { "@" });

            if (limit != null && limit >= 0)
                sql = String.Format("select * from ({0}) as {1} limit {2}", sql, tableName, limit);

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(procedureName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionProcedureNameNullOrEmpty);

            if ((values != null && values.Length >= 0) && parameters == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if ((parameters != null && parameters.Length >= 0) && values == null)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            if (values != null && parameters != null && (values.Length != parameters.Length))
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesAndParametersNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                    MySqlParameter mySqlParameter = new MySqlParameter(parameters[i], dbType);
                    mySqlParameter.Value = values[i];

                    this.mySqlCommand.Parameters.Add(mySqlParameter);
                }
            }

            this.mySqlCommand.CommandText = procedureName;
            this.mySqlCommand.CommandType = CommandType.StoredProcedure;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

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
                MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                MySqlParameter mySqlParameter = new MySqlParameter(keyFields[i], dbType);
                mySqlParameter.Value = keyValues[i];

                this.mySqlCommand.Parameters.Add(mySqlParameter);
            }

            String sql = "select " + returnFieldString + " from " + tableName + " where " + whereKeyFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (returnFields == null || returnFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionReturnFieldsNullOrZeroLenght);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String returnFieldString = String.Empty;
            String tempTableKeyValueString = String.Empty;
            String onClauseKeyFieldString = String.Empty;

            foreach (String returnField in returnFields)
                returnFieldString += returnField == "*" ? "T.*," : returnField + ",";
            returnFieldString = returnFieldString.Remove(returnFieldString.Length - 1, 1);

            foreach (String keyField in keyFields)
                onClauseKeyFieldString += "T." + keyField + " = " + "Temp.Key" + keyField + " and ";
            onClauseKeyFieldString = onClauseKeyFieldString.Remove(onClauseKeyFieldString.Length - 5, 5);

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                #endregion Validations

                tempTableKeyValueString += "select ";
                for (int keyIndex = 0; keyIndex < keyValuesList[listIndex].Length; keyIndex++)
                    tempTableKeyValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][keyIndex]) + " Key" + keyFields[keyIndex] + ", ";
                tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 2, 2);
                tempTableKeyValueString += " union all ";
            }
            tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 11, 11);

            String sql = "select " + returnFieldString + " from " + tableName + " as T join (" + tempTableKeyValueString + ") as Temp on " + onClauseKeyFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.mySqlDataAdapter.SelectCommand = this.mySqlCommand;
            this.mySqlDataAdapter.Fill(dataTable);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (values == null || values.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesNullOrZeroLenght);

            if (fields.Length != values.Length)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsAndValuesNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String fieldString = String.Empty;
            String parameterString = String.Empty;

            foreach (String field in fields)
                fieldString += "" + field + ",";
            fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            for (int i = 0; i < values.Length; i++)
            {
                MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                MySqlParameter mySqlParameter = new MySqlParameter(fields[i], dbType);
                mySqlParameter.Value = values[i];

                this.mySqlCommand.Parameters.Add(mySqlParameter);

                parameterString += "@" + fields[i] + ",";
            }
            parameterString = parameterString.Remove(parameterString.Length - 1, 1);

            String sql = "insert into " + tableName + " (" + fieldString + ") values (" + parameterString + ")";

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (valuesList == null || valuesList.Count == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesListNullOrZeroLenght);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String fieldString = String.Empty;
            String valuesListString = String.Empty;

            foreach (String field in fields)
                fieldString += "" + field + ",";
            fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            foreach (Object[] values in valuesList)
            {
                #region Validations

                if (fields.Length != values.Length)
                    throw new Exception(Properties.Resources.LazyDatabaseExceptionFieldsAndValuesNotMatch);

                #endregion Validations

                valuesListString += "(";
                for (int i = 0; i < values.Length; i++)
                    valuesListString += ConvertToDatabaseStringFormat(values[i]) + ",";
                valuesListString = valuesListString.Remove(valuesListString.Length - 1, 1);
                valuesListString += "),";
            }
            valuesListString = valuesListString.Remove(valuesListString.Length - 1, 1);

            String sql = "insert into " + tableName + " (" + fieldString + ") values " + valuesListString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (values == null || values.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesNullOrZeroLenght);

            if (fields.Length != values.Length)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsAndValuesNotMatch);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

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
                MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(values[i].GetType());
                MySqlParameter mySqlParameter = new MySqlParameter(fields[i], dbType);
                mySqlParameter.Value = values[i];

                this.mySqlCommand.Parameters.Add(mySqlParameter);
            }

            for (int i = 0; i < keyValues.Length; i++)
            {
                MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                MySqlParameter mySqlParameter = new MySqlParameter("key" + keyFields[i], dbType);
                mySqlParameter.Value = keyValues[i];

                this.mySqlCommand.Parameters.Add(mySqlParameter);
            }

            String sql = "update " + tableName + " set " + fieldString + " where " + keyFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (fields == null || fields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsNullOrZeroLenght);

            if (valuesList == null || valuesList.Count == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesListNullOrZeroLenght);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            if (valuesList.Count != keyValuesList.Count)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionValuesListAndKeyValueListNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String tempTableValueString = String.Empty;
            String onClauseString = String.Empty;
            String setFieldString = String.Empty;

            for (int listIndex = 0; listIndex < valuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                if (fields.Length != valuesList[listIndex].Length)
                    throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionFieldsAndValuesNotMatch);

                #endregion Validations

                tempTableValueString += "select ";

                for (int columnIndex = 0; columnIndex < keyValuesList[listIndex].Length; columnIndex++)
                    tempTableValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][columnIndex]) + " as " + ("key" + keyFields[columnIndex]) + ", ";

                for (int columnIndex = 0; columnIndex < valuesList[listIndex].Length; columnIndex++)
                    tempTableValueString += ConvertToDatabaseStringFormat(valuesList[listIndex][columnIndex]) + " as " + (fields[columnIndex]) + ", ";

                tempTableValueString = tempTableValueString.Remove(tempTableValueString.Length - 2, 2);
                tempTableValueString += " union all ";
            }
            tempTableValueString = tempTableValueString.Remove(tempTableValueString.Length - 11, 11);

            foreach (String keyField in keyFields)
                onClauseString += "T." + keyField + " = Temp.key" + keyField + " and ";
            onClauseString = onClauseString.Remove(onClauseString.Length - 5, 5);

            foreach (String field in fields)
                setFieldString += "T." + field + " = Temp." + field + ", ";
            setFieldString = setFieldString.Remove(setFieldString.Length - 2, 2);

            String sql = "update " + tableName + " as T join (" + tempTableValueString + ") as Temp on " + onClauseString + " set " + setFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValues == null || keyValues.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesNullOrZeroLenght);

            if (keyFields.Length != keyValues.Length)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String whereKeyFieldString = String.Empty;

            foreach (String keyField in keyFields)
                whereKeyFieldString += keyField + " = @" + keyField + " and ";
            whereKeyFieldString = whereKeyFieldString.Remove(whereKeyFieldString.Length - 5, 5); // Remove last " and "

            for (int i = 0; i < keyValues.Length; i++)
            {
                MySqlDbType dbType = (MySqlDbType)ConvertToDatabaseType(keyValues[i].GetType());
                MySqlParameter mySqlParameter = new MySqlParameter(keyFields[i], dbType);
                mySqlParameter.Value = keyValues[i];

                this.mySqlCommand.Parameters.Add(mySqlParameter);
            }

            String sql = "delete from " + tableName + " where " + whereKeyFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

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

            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (keyFields == null || keyFields.Length == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsNullOrZeroLenght);

            if (keyValuesList == null || keyValuesList.Count == 0)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyValuesListNullOrZeroLenght);

            #endregion Validations

            this.mySqlCommand.Parameters.Clear();

            String tempTableKeyValueString = String.Empty;
            String onClauseKeyFieldString = String.Empty;

            for (int listIndex = 0; listIndex < keyValuesList.Count; listIndex++)
            {
                #region Validations

                if (keyFields.Length != keyValuesList[listIndex].Length)
                    throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionKeyFieldsAndKeyValuesNotMatch);

                #endregion Validations

                tempTableKeyValueString += "select ";
                for (int keyIndex = 0; keyIndex < keyValuesList[listIndex].Length; keyIndex++)
                    tempTableKeyValueString += ConvertToDatabaseStringFormat(keyValuesList[listIndex][keyIndex]) + " Key" + keyFields[keyIndex] + ", ";
                tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 2, 2);
                tempTableKeyValueString += " union all ";
            }
            tempTableKeyValueString = tempTableKeyValueString.Remove(tempTableKeyValueString.Length - 11, 11);

            foreach (String keyField in keyFields)
                onClauseKeyFieldString += "T." + keyField + " = " + "Temp.Key" + keyField + " and ";
            onClauseKeyFieldString = onClauseKeyFieldString.Remove(onClauseKeyFieldString.Length - 5, 5);

            String sql = "delete T from " + tableName + " T join (" + tempTableKeyValueString + ") Temp on " + onClauseKeyFieldString;

            this.mySqlCommand.CommandText = sql;
            this.mySqlCommand.CommandType = CommandType.Text;
            this.mySqlCommand.Transaction = this.mySqlTransaction;

            return this.mySqlCommand.ExecuteNonQuery();
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
            if (this.mySqlConnection == null || this.mySqlConnection.State == ConnectionState.Closed)
                throw new Exception(Lazy.Vinke.Database.Properties.Resources.LazyDatabaseExceptionConnectionNotOpen);

            return base.IncrementRange(tableName, keyFields, keyValues, incrementField, range);
        }

        /// <summary>
        /// Convert a value to a database string format
        /// </summary>
        /// <param name="value">The value to be converted to the string format</param>
        /// <returns>The string format of the value</returns>
        protected override String ConvertToDatabaseStringFormat(Object value)
        {
            Type dataType = value.GetType();

            if (dataType == typeof(Char)) return String.Format(this.CultureInfo, this.StringFormat.Char, value);
            if (dataType == typeof(Byte)) return String.Format(this.CultureInfo, this.StringFormat.Byte, value);
            if (dataType == typeof(Int16)) return String.Format(this.CultureInfo, this.StringFormat.Int16, value);
            if (dataType == typeof(Int32)) return String.Format(this.CultureInfo, this.StringFormat.Int32, value);
            if (dataType == typeof(Int64)) return String.Format(this.CultureInfo, this.StringFormat.Int64, value);
            if (dataType == typeof(UInt16)) return String.Format(this.CultureInfo, this.StringFormat.UInt16, value);
            if (dataType == typeof(UInt32)) return String.Format(this.CultureInfo, this.StringFormat.UInt32, value);
            if (dataType == typeof(UInt64)) return String.Format(this.CultureInfo, this.StringFormat.UInt64, value);
            if (dataType == typeof(String)) return String.Format(this.CultureInfo, this.StringFormat.String, value);
            if (dataType == typeof(Boolean)) return String.Format(this.CultureInfo, this.StringFormat.Boolean, value);
            if (dataType == typeof(Byte[])) return String.Format(this.CultureInfo, this.StringFormat.ByteArray, value);
            if (dataType == typeof(DateTime)) return String.Format(this.CultureInfo, this.StringFormat.DateTime, value);
            if (dataType == typeof(Decimal)) return String.Format(this.CultureInfo, this.StringFormat.Decimal, value);
            if (dataType == typeof(float)) return String.Format(this.CultureInfo, this.StringFormat.Float, value);
            if (dataType == typeof(double)) return String.Format(this.CultureInfo, this.StringFormat.Double, value);

            return String.Format(this.CultureInfo, this.StringFormat.String, value);
        }

        /// <summary>
        /// Convert a system type to a database type
        /// </summary>
        /// <param name="systemType">The system type to be converted</param>
        /// <returns>The database type</returns>
        protected override Int32 ConvertToDatabaseType(Type systemType)
        {
            if (systemType == typeof(Char)) return (Int32)MySqlDbType.VarChar;
            if (systemType == typeof(Byte)) return (Int32)MySqlDbType.Byte;
            if (systemType == typeof(Int16)) return (Int32)MySqlDbType.Int16;
            if (systemType == typeof(Int32)) return (Int32)MySqlDbType.Int32;
            if (systemType == typeof(Int64)) return (Int32)MySqlDbType.Int64;
            if (systemType == typeof(UInt16)) return (Int32)MySqlDbType.UInt16;
            if (systemType == typeof(UInt32)) return (Int32)MySqlDbType.UInt32;
            if (systemType == typeof(UInt64)) return (Int32)MySqlDbType.UInt64;
            if (systemType == typeof(String)) return (Int32)MySqlDbType.VarChar;
            if (systemType == typeof(Boolean)) return (Int32)MySqlDbType.Bit;
            if (systemType == typeof(Byte[])) return (Int32)MySqlDbType.VarBinary;
            if (systemType == typeof(DateTime)) return (Int32)MySqlDbType.DateTime;
            if (systemType == typeof(Decimal)) return (Int32)MySqlDbType.Decimal;
            if (systemType == typeof(float)) return (Int32)MySqlDbType.Float;
            if (systemType == typeof(double)) return (Int32)MySqlDbType.Double;

            return (Int32)MySqlDbType.VarChar;
        }

        #endregion Methods

        #region Properties

        public override Boolean InTransaction
        {
            get { return this.mySqlConnection != null && this.mySqlTransaction != null; }
        }

        #endregion Properties
    }
}
