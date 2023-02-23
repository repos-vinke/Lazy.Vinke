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
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType)
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

                foreach (LazyJsonToken jsonTokenDataTableRow in jsonArrayDataTableRows.TokenList)
                {
                    if (jsonTokenDataTableRow.Type == LazyJsonType.Object)
                        jsonDeserializerDataRow.Deserialize(jsonTokenDataTableRow, dataTable);
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
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType)
        {
            /* Unable to retrieve the data table column types from deserializer options at this call because the table name is unknown */

            if (jsonToken == null || jsonToken.Type != LazyJsonType.Object || dataType == null || dataType != typeof(DataTable))
                return null;

            LazyJsonObject jsonObjectDataTable = (LazyJsonObject)jsonToken;
            DataTable dataTable = new DataTable();

            // Rows
            if (jsonObjectDataTable["Rows"] != null && jsonObjectDataTable["Rows"].Token.Type == LazyJsonType.Array)
            {
                LazyJsonArray jsonArrayDataTableRows = (LazyJsonArray)jsonObjectDataTable["Rows"].Token;
                LazyJsonDeserializerDataRow jsonDeserializerDataRow = new LazyJsonDeserializerDataRow();

                foreach (LazyJsonToken jsonTokenDataTableRow in jsonArrayDataTableRows.TokenList)
                {
                    if (jsonTokenDataTableRow.Type == LazyJsonType.Object)
                        jsonDeserializerDataRow.Deserialize(jsonTokenDataTableRow, dataTable);
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
        public void Deserialize(LazyJsonToken jsonToken, DataTable dataTable)
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
                        DeserializeDataRow((LazyJsonObject)jsonObjectDataRowValues["Original"].Token, dataRow);

                    dataRow.AcceptChanges();
                }

                // Values Current
                if (jsonObjectDataRowValues["Current"] != null && jsonObjectDataRowValues["Current"].Token.Type == LazyJsonType.Object)
                    DeserializeDataRow((LazyJsonObject)jsonObjectDataRowValues["Current"].Token, dataRow);
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
        private void DeserializeDataRow(LazyJsonObject jsonObjectDataRowColumns, DataRow dataRow)
        {
            foreach (LazyJsonProperty jsonPropertyDataRowColumn in jsonObjectDataRowColumns.PropertyList)
            {
                if (jsonPropertyDataRowColumn.Token.Type == LazyJsonType.Null)
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
}