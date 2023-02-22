// LazyJsonDeserializerDataSet.cs
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
    public class LazyJsonDeserializerDataSet : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Deserialize the json dataset property to the desired object
        /// </summary>
        /// <param name="jsonProperty">The json dataset property to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonProperty jsonProperty, Type dataType)
        {
            if (jsonProperty == null)
                return null;

            return Deserialize(jsonProperty.Token, dataType);
        }

        /// <summary>
        /// Deserialize the json dataset token to the desired object
        /// </summary>
        /// <param name="jsonToken">The json dataset token to be deserialized</param>
        /// <param name="dataType">The type of the desired object</param>
        /// <param name="deserializerOptions">The json deserializer options</param>
        /// <returns>The desired object instance</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType)
        {
            if (jsonToken == null || jsonToken.Type != LazyJsonType.Object || dataType == null || dataType != typeof(DataSet))
                return null;

            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)jsonToken;
            DataSet dataSet = new DataSet();

            // Name
            if (jsonObjectDataSet["Name"] != null && jsonObjectDataSet["Name"].Token.Type == LazyJsonType.String)
                dataSet.DataSetName = ((LazyJsonString)jsonObjectDataSet["Name"].Token).Value;

            // Tables
            if (jsonObjectDataSet["Tables"] != null && jsonObjectDataSet["Tables"].Token.Type == LazyJsonType.Object)
            {
                LazyJsonObject jsonObjectDataSetTables = (LazyJsonObject)jsonObjectDataSet["Tables"].Token;
                LazyJsonDeserializerDataTable jsonDeserializerDataTable = new LazyJsonDeserializerDataTable();

                foreach (LazyJsonProperty jsonPropertyDataSetTable in jsonObjectDataSetTables.PropertyList)
                {
                    DataTable dataTable = jsonDeserializerDataTable.Deserialize<DataTable>(jsonPropertyDataSetTable);

                    if (dataTable != null)
                        dataSet.Tables.Add(dataTable);
                }
            }

            return dataSet;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}