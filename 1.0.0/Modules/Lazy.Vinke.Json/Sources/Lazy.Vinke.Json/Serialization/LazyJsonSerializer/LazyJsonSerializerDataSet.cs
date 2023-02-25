// LazyJsonSerializerDataSet.cs
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
    public class LazyJsonSerializerDataSet : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Contructors
        #endregion Contructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json dataset object token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <returns>The json dataset object token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions serializerOptions = null)
        {
            if (data == null || data is not DataSet)
                return new LazyJsonNull();

            DataSet dataSet = (DataSet)data;
            LazyJsonObject jsonObjectDataSet = new LazyJsonObject();

            // Name
            jsonObjectDataSet.Add(new LazyJsonProperty("Name", new LazyJsonString(dataSet.DataSetName)));

            // Tables
            LazyJsonObject jsonObjectDataSetTables = new LazyJsonObject();
            jsonObjectDataSet.Add(new LazyJsonProperty("Tables", jsonObjectDataSetTables));

            LazyJsonSerializerDataTable jsonSerializerDataTable = new LazyJsonSerializerDataTable();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                LazyJsonToken jsonTokenDataSetTable = jsonSerializerDataTable.Serialize(dataTable, serializerOptions);

                if (jsonTokenDataSetTable != null)
                    jsonObjectDataSetTables.Add(new LazyJsonProperty(dataTable.TableName, jsonTokenDataSetTable));
            }
            
            return jsonObjectDataSet;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}