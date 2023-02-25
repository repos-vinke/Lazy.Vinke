// LazyJsonDeserializerDataTable.cs
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

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDataTable : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json datatable property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json datatable property to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonProperty == null || jsonProperty.Token == null || jsonProperty.Token.Type != LazyJsonType.Object || dataType == null || dataType != typeof(DataTable))
                return null;

            LazyJsonObject jsonObjectDataTable = (LazyJsonObject)jsonProperty.Token;
            DataTable dataTable = new DataTable(jsonProperty.Name);

            // Rows
            if (jsonObjectDataTable["Rows"] != null && jsonObjectDataTable["Rows"].Token.Type == LazyJsonType.Array)
            {
                LazyJsonArray jsonArrayDataTableRows = (LazyJsonArray)jsonObjectDataTable["Rows"].Token;
                LazyJsonDeserializerDataRow jsonDeserializerDataRow = new LazyJsonDeserializerDataRow();

                Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData> customColumnDictionary = null;
                if (deserializerOptions?.Contains<LazyJsonDeserializerOptionsDataTable>() == true && deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>().dataTableColumnDictionary?.ContainsKey(jsonProperty.Name) == true)
                    customColumnDictionary = deserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>().dataTableColumnDictionary[jsonProperty.Name].columnCollectionOptions?.columnDictionary;

                foreach (LazyJsonToken jsonTokenDataTableRow in jsonArrayDataTableRows.TokenList)
                {
                    if (jsonTokenDataTableRow.Type == LazyJsonType.Object)
                        jsonDeserializerDataRow.Deserialize(jsonTokenDataTableRow, dataTable, customColumnDictionary, deserializerOptions);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Deserialize the json datatable token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json datatable token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions deserializerOptions = null)
        {
            if (jsonToken == null || jsonToken.Type != LazyJsonType.Object || dataType == null || dataType != typeof(DataTable))
                return null;

            LazyJsonObject jsonObjectDataTable = (LazyJsonObject)jsonToken;
            DataTable dataTable = new DataTable();

            // Rows
            if (jsonObjectDataTable["Rows"] != null && jsonObjectDataTable["Rows"].Token.Type == LazyJsonType.Array)
            {
                LazyJsonArray jsonArrayDataTableRows = (LazyJsonArray)jsonObjectDataTable["Rows"].Token;
                LazyJsonDeserializerDataRow jsonDeserializerDataRow = new LazyJsonDeserializerDataRow();

                /* Unable to retrieve the data table custom column types from deserializer options at this call because the table name is unknown */

                foreach (LazyJsonToken jsonTokenDataTableRow in jsonArrayDataTableRows.TokenList)
                {
                    if (jsonTokenDataTableRow.Type == LazyJsonType.Object)
                        jsonDeserializerDataRow.Deserialize(jsonTokenDataTableRow, dataTable, null, deserializerOptions);
                }
            }

            return dataTable;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    internal class LazyJsonDeserializerDataRow
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json datarow token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json datarow token</param>
        /// <param name="dataTable">The datatable</param>
        internal void Deserialize(LazyJsonToken jsonToken, DataTable dataTable, Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData> customColumnDictionary, LazyJsonDeserializerOptions deserializerOptions)
        {
            LazyJsonObject jsonObjectDataRow = (LazyJsonObject)jsonToken;
            DataRow dataRow = dataTable.NewRow();

            dataTable.Rows.Add(dataRow);
            dataRow.AcceptChanges();

            // State
            DataRowState dataRowState = DataRowState.Unchanged;

            if (jsonObjectDataRow["State"] != null && jsonObjectDataRow["State"].Token.Type == LazyJsonType.String)
            {
                switch (((LazyJsonString)jsonObjectDataRow["State"].Token).Value.ToLower())
                {
                    case "added": dataRowState = DataRowState.Added; break;
                    case "modified": dataRowState = DataRowState.Modified; break;
                    case "deleted": dataRowState = DataRowState.Deleted; break;
                }
            }

            // Values
            if (jsonObjectDataRow["Values"] != null && jsonObjectDataRow["Values"].Token.Type == LazyJsonType.Object)
            {
                LazyJsonObject jsonObjectDataRowValues = (LazyJsonObject)jsonObjectDataRow["Values"].Token;

                // Values Original
                if (dataRowState == DataRowState.Modified)
                {
                    if (jsonObjectDataRowValues["Original"] != null && jsonObjectDataRowValues["Original"].Token.Type == LazyJsonType.Object)
                        DeserializeDataRow((LazyJsonObject)jsonObjectDataRowValues["Original"].Token, dataRow, customColumnDictionary, deserializerOptions);

                    dataRow.AcceptChanges();
                }

                // Values Current
                if (jsonObjectDataRowValues["Current"] != null && jsonObjectDataRowValues["Current"].Token.Type == LazyJsonType.Object)
                    DeserializeDataRow((LazyJsonObject)jsonObjectDataRowValues["Current"].Token, dataRow, customColumnDictionary, deserializerOptions);
            }

            switch (dataRowState)
            {
                case DataRowState.Added: dataRow.AcceptChanges(); dataRow.SetAdded(); break;
                case DataRowState.Deleted: dataRow.AcceptChanges(); dataRow.Delete(); break;
                case DataRowState.Unchanged: dataRow.AcceptChanges(); break;
            }
        }

        /// <summary>
        /// Deserialize the json datarow object to the desired object
        /// </summary>
        /// <param name="jsonObjectDataRowColumns">The json datarow object</param>
        /// <param name="dataRow">The datarow</param>
        internal void DeserializeDataRow(LazyJsonObject jsonObjectDataRowColumns, DataRow dataRow, Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData> customColumnDictionary, LazyJsonDeserializerOptions deserializerOptions)
        {
            foreach (LazyJsonProperty jsonPropertyDataRowColumn in jsonObjectDataRowColumns.PropertyList)
            {
                if (customColumnDictionary != null && customColumnDictionary.ContainsKey(jsonPropertyDataRowColumn.Name) == true)
                {
                    Type dataType = customColumnDictionary[jsonPropertyDataRowColumn.Name].Type;

                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, dataType);

                    Object value = null;

                    if (customColumnDictionary[jsonPropertyDataRowColumn.Name].Deserializer != null)
                    {
                        value = customColumnDictionary[jsonPropertyDataRowColumn.Name].Deserializer.Deserialize(jsonPropertyDataRowColumn, dataType, deserializerOptions);
                    }
                    else
                    {
                        value = LazyJsonDeserializer.DeserializeProperty(jsonPropertyDataRowColumn, dataType, deserializerOptions);
                    }

                    dataRow[jsonPropertyDataRowColumn.Name] = value == null ? DBNull.Value : value;
                }
                else if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.Null)
                {
                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, typeof(String));

                    dataRow[jsonPropertyDataRowColumn.Name] = DBNull.Value;
                }
                else if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.Boolean)
                {
                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, typeof(Boolean));

                    Boolean? value = ((LazyJsonBoolean)jsonPropertyDataRowColumn.Token).Value;
                    dataRow[jsonPropertyDataRowColumn.Name] = value == null ? DBNull.Value : value;
                }
                else if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.String)
                {
                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, typeof(String));

                    String value = ((LazyJsonString)jsonPropertyDataRowColumn.Token).Value;
                    dataRow[jsonPropertyDataRowColumn.Name] = value == null ? DBNull.Value : value;
                }
                else if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.Integer)
                {
                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, typeof(Int32));

                    Int64? value = ((LazyJsonInteger)jsonPropertyDataRowColumn.Token).Value;
                    dataRow[jsonPropertyDataRowColumn.Name] = value == null ? DBNull.Value : value;
                }
                else if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.Decimal)
                {
                    if (dataRow.Table.Columns.Contains(jsonPropertyDataRowColumn.Name) == false)
                        dataRow.Table.Columns.Add(jsonPropertyDataRowColumn.Name, typeof(Decimal));

                    Decimal? value = ((LazyJsonDecimal)jsonPropertyDataRowColumn.Token).Value;
                    dataRow[jsonPropertyDataRowColumn.Name] = value == null ? DBNull.Value : value;
                }
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonDeserializerOptionsDataTable : LazyJsonDeserializerOptionsBase
    {
        #region Variables

        internal Dictionary<String, LazyJsonDeserializerOptionsDataTableColumn> dataTableColumnDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerOptionsDataTableColumn this[String name]
        {
            get
            {
                if (this.dataTableColumnDictionary == null)
                    this.dataTableColumnDictionary = new Dictionary<String, LazyJsonDeserializerOptionsDataTableColumn>();

                if (this.dataTableColumnDictionary.ContainsKey(name) == false)
                    this.dataTableColumnDictionary.Add(name, new LazyJsonDeserializerOptionsDataTableColumn());

                return this.dataTableColumnDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerOptionsDataTableColumn
    {
        #region Variables

        internal LazyJsonDeserializerOptionsDataTableColumnCollection columnCollectionOptions;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonDeserializerOptionsDataTableColumnCollection Columns
        {
            get
            {
                if (this.columnCollectionOptions == null)
                    this.columnCollectionOptions = new LazyJsonDeserializerOptionsDataTableColumnCollection();

                return this.columnCollectionOptions;
            }
            set
            {
                this.columnCollectionOptions = value;
            }
        }

        #endregion Properties
    }

    public class LazyJsonDeserializerOptionsDataTableColumnCollection
    {
        #region Variables

        internal Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData> columnDictionary;

        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerOptionsDataTableColumnData this[String name]
        {
            get
            {
                if (this.columnDictionary == null)
                    this.columnDictionary = new Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData>();

                if (this.columnDictionary.ContainsKey(name) == false)
                    this.columnDictionary.Add(name, new LazyJsonDeserializerOptionsDataTableColumnData());

                return this.columnDictionary[name];
            }
            set
            {
                this[name] = value;
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerOptionsDataTableColumnData
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        public void Set(Type type, LazyJsonDeserializerBase deserializer = null)
        {
            this.Type = type;
            this.Deserializer = deserializer;
        }

        #endregion Methods

        #region Properties

        public Type Type { get; set; }

        public LazyJsonDeserializerBase Deserializer { get; set; }

        #endregion Properties
    }
}