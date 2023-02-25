// LazyJsonSerializerDataTable.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 20

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerDataTable : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json datatable object token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json datatable object token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null || data is not DataTable)
                return new LazyJsonNull();

            DataTable dataTable = (DataTable)data;
            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();

            // Rows
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            LazyJsonSerializerDataRow jsonSerializerDataRow = new LazyJsonSerializerDataRow();

            Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData> customColumnDictionary = null;
            if (serializerOptions?.Contains<LazyJsonSerializerOptionsDataTable>() == true && serializerOptions.Item<LazyJsonSerializerOptionsDataTable>().dataTableColumnDictionary?.ContainsKey(dataTable.TableName) == true)
                customColumnDictionary = serializerOptions.Item<LazyJsonSerializerOptionsDataTable>().dataTableColumnDictionary[dataTable.TableName].columnCollectionOptions?.customColumnDictionary;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                LazyJsonToken jsonTokenDataTableRow = jsonSerializerDataRow.Serialize(dataRow, customColumnDictionary, serializerOptions);

                if (jsonTokenDataTableRow != null && jsonTokenDataTableRow.Type != LazyJsonType.Null)
                    jsonArrayDataTableRows.Add(jsonTokenDataTableRow);
            }

            return jsonObjectDataTable;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    internal class LazyJsonSerializerDataRow
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json datarow object token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json datarow object token</returns>
        internal LazyJsonToken Serialize(DataRow dataRow, Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData> customColumnDictionary, LazyJsonSerializerOptions serializerOptions)
        {
            LazyJsonObject jsonObjectDataRow = new LazyJsonObject();

            // State
            jsonObjectDataRow.Add(new LazyJsonProperty("State", new LazyJsonString(Enum.GetName(typeof(DataRowState), dataRow.RowState))));

            // Values
            LazyJsonObject jsonObjectDataRowValues = new LazyJsonObject();
            jsonObjectDataRow.Add(new LazyJsonProperty("Values", jsonObjectDataRowValues));

            // Values Original
            jsonObjectDataRowValues.Add(new LazyJsonProperty("Original", dataRow.RowState != DataRowState.Modified ? new LazyJsonNull() : SerializeDataRow(dataRow, DataRowVersion.Original, customColumnDictionary, serializerOptions)));

            // Values Current
            jsonObjectDataRowValues.Add(new LazyJsonProperty("Current", SerializeDataRow(dataRow, DataRowVersion.Current, customColumnDictionary, serializerOptions)));

            return jsonObjectDataRow;
        }

        /// <summary>
        /// Serialize a datarow to a json datarow object token
        /// </summary>
        /// <param name="dataRow">The datarow to be serialized</param>
        /// <param name="dataRowVersion">The datarow version to be serialized</param>
        /// <returns>The json datarow object token</returns>
        internal LazyJsonObject SerializeDataRow(DataRow dataRow, DataRowVersion dataRowVersion, Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData> customColumnDictionary, LazyJsonSerializerOptions serializerOptions)
        {
            LazyJsonObject jsonObjectDataRowColumns = new LazyJsonObject();

            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                LazyJsonToken jsonToken = null;

                if (customColumnDictionary != null && customColumnDictionary.ContainsKey(dataColumn.ColumnName) == true && customColumnDictionary[dataColumn.ColumnName].Serializer != null)
                {
                    jsonToken = customColumnDictionary[dataColumn.ColumnName].Serializer.Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else if (dataRow[dataColumn.ColumnName, dataRowVersion] == DBNull.Value)
                {
                    jsonToken = new LazyJsonNull();
                }
                else if (dataColumn.DataType == typeof(Boolean))
                {
                    jsonToken = new LazyJsonSerializerBoolean().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else if (dataColumn.DataType == typeof(Char) || dataColumn.DataType == typeof(String))
                {
                    jsonToken = new LazyJsonSerializerString().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else if (dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64))
                {
                    jsonToken = new LazyJsonSerializerInteger().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else if (dataColumn.DataType == typeof(float) || dataColumn.DataType == typeof(Double) || dataColumn.DataType == typeof(Decimal))
                {
                    jsonToken = new LazyJsonSerializerDecimal().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else if (dataColumn.DataType == typeof(DateTime))
                {
                    jsonToken = new LazyJsonSerializerDateTime().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion], serializerOptions);
                }
                else
                {
                    jsonToken = new LazyJsonNull();
                }

                jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, jsonToken));
            }

            return jsonObjectDataRowColumns;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonSerializerOptionsDataTable : LazyJsonSerializerOptionsBase
    {
        #region Variables

        internal Dictionary<String, LazyJsonSerializerOptionsDataTableColumn> dataTableColumnDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonSerializerOptionsDataTableColumn this[String name]
        {
            get
            {
                if (this.dataTableColumnDictionary == null)
                    this.dataTableColumnDictionary = new Dictionary<String, LazyJsonSerializerOptionsDataTableColumn>();

                if (this.dataTableColumnDictionary.ContainsKey(name) == false)
                    this.dataTableColumnDictionary.Add(name, new LazyJsonSerializerOptionsDataTableColumn());

                return this.dataTableColumnDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonSerializerOptionsDataTableColumn
    {
        #region Variables

        internal LazyJsonSerializerOptionsDataTableColumnCollection columnCollectionOptions;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonSerializerOptionsDataTableColumnCollection Columns
        {
            get
            {
                if (this.columnCollectionOptions == null)
                    this.columnCollectionOptions = new LazyJsonSerializerOptionsDataTableColumnCollection();

                return this.columnCollectionOptions;
            }
            set
            {
                this.columnCollectionOptions = value;
            }
        }

        #endregion Properties
    }

    public class LazyJsonSerializerOptionsDataTableColumnCollection
    {
        #region Variables

        internal Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData> customColumnDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonSerializerOptionsDataTableColumnData this[String name]
        {
            get
            {
                if (this.customColumnDictionary == null)
                    this.customColumnDictionary = new Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData>();

                if (this.customColumnDictionary.ContainsKey(name) == false)
                    this.customColumnDictionary.Add(name, new LazyJsonSerializerOptionsDataTableColumnData());

                return this.customColumnDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonSerializerOptionsDataTableColumnData
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        public void Set(LazyJsonSerializerBase serializer)
        {
            this.Serializer = serializer;
        }

        #endregion Methods

        #region Properties

        public LazyJsonSerializerBase Serializer { get; set; }

        #endregion Properties
    }
}