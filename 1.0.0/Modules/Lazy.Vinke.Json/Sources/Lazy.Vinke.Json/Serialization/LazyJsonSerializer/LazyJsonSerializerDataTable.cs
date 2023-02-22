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
        public override LazyJsonToken Serialize(Object data)
        {
            if (data == null || data is not DataTable)
                return new LazyJsonNull();

            DataTable dataTable = (DataTable)data;
            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();

            // Rows
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            LazyJsonSerializerDataRow jsonSerializerDataRow = new LazyJsonSerializerDataRow();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                LazyJsonToken jsonTokenDataTableRow = jsonSerializerDataRow.Serialize(dataRow);

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
        public LazyJsonToken Serialize(DataRow dataRow)
        {
            LazyJsonObject jsonObjectDataRow = new LazyJsonObject();

            // State
            jsonObjectDataRow.Add(new LazyJsonProperty("State", new LazyJsonString(Enum.GetName(typeof(DataRowState), dataRow.RowState))));

            // Values
            LazyJsonObject jsonObjectDataRowValues = new LazyJsonObject();
            jsonObjectDataRow.Add(new LazyJsonProperty("Values", jsonObjectDataRowValues));

            // Values Original
            jsonObjectDataRowValues.Add(new LazyJsonProperty("Original", dataRow.RowState != DataRowState.Modified ? new LazyJsonNull() : SerializeDataRow(dataRow, DataRowVersion.Original)));

            // Values Current
            jsonObjectDataRowValues.Add(new LazyJsonProperty("Current", SerializeDataRow(dataRow, DataRowVersion.Current)));

            return jsonObjectDataRow;
        }

        /// <summary>
        /// Serialize a datarow to a json datarow object token
        /// </summary>
        /// <param name="dataRow">The datarow to be serialized</param>
        /// <param name="dataRowVersion">The datarow version to be serialized</param>
        /// <returns>The json datarow object token</returns>
        private LazyJsonObject SerializeDataRow(DataRow dataRow, DataRowVersion dataRowVersion)
        {
            LazyJsonObject jsonObjectDataRowColumns = new LazyJsonObject();

            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                if (dataRow[dataColumn.ColumnName, dataRowVersion] == null || dataRow[dataColumn.ColumnName, dataRowVersion] == DBNull.Value)
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonNull()));
                }
                else if (dataColumn.DataType == typeof(Boolean))
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonBoolean(Convert.ToBoolean(dataRow[dataColumn.ColumnName, dataRowVersion]))));
                }
                else if (dataColumn.DataType == typeof(Char) || dataColumn.DataType == typeof(String))
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonString(Convert.ToString(dataRow[dataColumn.ColumnName, dataRowVersion]))));
                }
                else if (dataColumn.DataType == typeof(Int16) || dataColumn.DataType == typeof(Int32) || dataColumn.DataType == typeof(Int64))
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonInteger(Convert.ToInt64(dataRow[dataColumn.ColumnName, dataRowVersion]))));
                }
                else if (dataColumn.DataType == typeof(float) || dataColumn.DataType == typeof(Double) || dataColumn.DataType == typeof(Decimal))
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonDecimal(Convert.ToDecimal(dataRow[dataColumn.ColumnName, dataRowVersion]))));
                }
                else if (dataColumn.DataType == typeof(DateTime))
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonSerializerDateTime().Serialize(dataRow[dataColumn.ColumnName, dataRowVersion])));
                }
                else
                {
                    jsonObjectDataRowColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, new LazyJsonString(Convert.ToString(dataRow[dataColumn.ColumnName, dataRowVersion]))));
                }
            }

            return jsonObjectDataRowColumns;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}