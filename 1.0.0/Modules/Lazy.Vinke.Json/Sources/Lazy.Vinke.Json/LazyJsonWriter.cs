// LazyJsonWriter.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 11

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonWriter
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Write a json
        /// </summary>
        /// <param name="lazyJson">The lazy json</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <returns>The json</returns>
        public static String Write(LazyJson lazyJson, LazyJsonWriterOptions jsonWriterOptions = null)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (jsonWriterOptions == null)
                jsonWriterOptions = new LazyJsonWriterOptions();

            WriteRoot(stringBuilder, jsonWriterOptions, lazyJson.Root);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Write the root node of the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonToken">The root token</param>
        private static void WriteRoot(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonToken jsonToken)
        {
            switch (jsonToken.Type)
            {
                case LazyJsonType.Null:
                    #region LazyJsonType.Null

                    stringBuilder.Append("null");

                    #endregion LazyJsonType.Null
                    break;

                case LazyJsonType.Boolean:
                    #region LazyJsonType.Boolean

                    LazyJsonBoolean jsonBoolean = (LazyJsonBoolean)jsonToken;
                    stringBuilder.Append(jsonBoolean.Value == null ? "null" : jsonBoolean.Value.ToString().ToLower());

                    #endregion LazyJsonType.Boolean
                    break;

                case LazyJsonType.Integer:
                    #region LazyJsonType.Integer

                    LazyJsonInteger jsonInteger = (LazyJsonInteger)jsonToken;
                    stringBuilder.Append(jsonInteger.Value == null ? "null" : jsonInteger.Value);

                    #endregion LazyJsonType.Integer
                    break;

                case LazyJsonType.Decimal:
                    #region LazyJsonType.Decimal

                    LazyJsonDecimal jsonDecimal = (LazyJsonDecimal)jsonToken;
                    stringBuilder.Append(jsonDecimal.Value == null ? "null" : jsonDecimal.Value.ToString().Replace(',', '.'));

                    #endregion LazyJsonType.Decimal
                    break;

                case LazyJsonType.String:
                    #region LazyJsonType.String

                    LazyJsonString jsonString = (LazyJsonString)jsonToken;
                    stringBuilder.Append(jsonString.Value == null ? "null" : "\"" + jsonString.Value + "\"");

                    #endregion LazyJsonType.String
                    break;

                case LazyJsonType.Object:
                    #region LazyJsonType.Object

                    WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonToken, LazyJsonPathType.Property);

                    #endregion LazyJsonType.Object
                    break;

                case LazyJsonType.Array:
                    #region LazyJsonType.Array

                    WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonToken, LazyJsonPathType.Property);

                    #endregion LazyJsonType.Array
                    break;
            }
        }

        /// <summary>
        /// Write an object on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonObject">The json object</param>
        /// <param name="jsonPathType">The json path type</param>
        private static void WriteObject(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonObject jsonObject, LazyJsonPathType jsonPathType)
        {
            String openKey = "{0}{1}{2}";
            String closeKey = "{0}{1}{2}";

            if (jsonWriterOptions.Indent == true)
            {
                if (jsonPathType == LazyJsonPathType.ArrayIndex)
                {
                    openKey = String.Format(openKey, Environment.NewLine, jsonWriterOptions.IndentValue, "{");
                    closeKey = String.Format(closeKey, Environment.NewLine, jsonWriterOptions.IndentValue, "}");
                }
                else if (jsonPathType == LazyJsonPathType.Property)
                {
                    openKey = "{";
                    closeKey = String.Format(closeKey, Environment.NewLine, jsonWriterOptions.IndentValue, "}");
                }
            }
            else
            {
                openKey = "{";
                closeKey = "}";
            }

            stringBuilder.Append(openKey);
            jsonWriterOptions.IndentLevel++;
            for (int i = 0; i < jsonObject.PropertyList.Count; i++)
            {
                WriteProperty(stringBuilder, jsonWriterOptions, jsonObject.PropertyList[i]);
                stringBuilder.Append(",");
            }
            jsonWriterOptions.IndentLevel--;

            if (stringBuilder[stringBuilder.Length - 1] == ',')
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append(closeKey);
        }

        /// <summary>
        /// Write an array on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonArray">The json array</param>
        /// <param name="jsonPathType">The json path type</param>
        private static void WriteArray(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonArray jsonArray, LazyJsonPathType jsonPathType)
        {
            String value = String.Empty;
            String openBracket = "{0}{1}{2}";
            String closeBracket = "{0}{1}{2}";

            if (jsonWriterOptions.Indent == true)
            {
                jsonWriterOptions.IndentLevel++;
                value = Environment.NewLine + jsonWriterOptions.IndentValue + "{0}";
                jsonWriterOptions.IndentLevel--;

                if (jsonPathType == LazyJsonPathType.ArrayIndex)
                {
                    openBracket = String.Format(openBracket, Environment.NewLine, jsonWriterOptions.IndentValue, "[");
                    closeBracket = String.Format(closeBracket, Environment.NewLine, jsonWriterOptions.IndentValue, "]");
                }
                else if (jsonPathType == LazyJsonPathType.Property)
                {
                    openBracket = "[";
                    closeBracket = String.Format(closeBracket, Environment.NewLine, jsonWriterOptions.IndentValue, "]");
                }
            }
            else
            {
                value = "{0}";
                openBracket = "[";
                closeBracket = "]";
            }

            stringBuilder.Append(openBracket);
            for (int i = 0; i < jsonArray.TokenList.Count; i++)
            {
                switch (jsonArray.TokenList[i].Type)
                {
                    case LazyJsonType.Null:
                        #region LazyJsonType.Null

                        stringBuilder.Append(String.Format(value, "null"));

                        #endregion LazyJsonType.Null
                        break;

                    case LazyJsonType.Boolean:
                        #region LazyJsonType.Boolean

                        LazyJsonBoolean jsonBoolean = (LazyJsonBoolean)jsonArray.TokenList[i];
                        stringBuilder.Append(String.Format(value, jsonBoolean.Value == null ? "null" : jsonBoolean.Value.ToString().ToLower()));

                        #endregion LazyJsonType.Boolean
                        break;

                    case LazyJsonType.Integer:
                        #region LazyJsonType.Integer

                        LazyJsonInteger jsonInteger = (LazyJsonInteger)jsonArray.TokenList[i];
                        stringBuilder.Append(String.Format(value, jsonInteger.Value == null ? "null" : jsonInteger.Value));

                        #endregion LazyJsonType.Integer
                        break;

                    case LazyJsonType.Decimal:
                        #region LazyJsonType.Decimal

                        LazyJsonDecimal jsonDecimal = (LazyJsonDecimal)jsonArray.TokenList[i];
                        stringBuilder.Append(String.Format(value, jsonDecimal.Value == null ? "null" : jsonDecimal.Value.ToString().Replace(',', '.')));

                        #endregion LazyJsonType.Decimal
                        break;

                    case LazyJsonType.String:
                        #region LazyJsonType.String

                        LazyJsonString jsonString = (LazyJsonString)jsonArray.TokenList[i];
                        stringBuilder.Append(String.Format(value, jsonString.Value == null ? "null" : "\"" + jsonString.Value + "\""));

                        #endregion LazyJsonType.String
                        break;

                    case LazyJsonType.Object:
                        #region LazyJsonType.Object

                        jsonWriterOptions.IndentLevel++;
                        WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonArray.TokenList[i], LazyJsonPathType.ArrayIndex);
                        jsonWriterOptions.IndentLevel--;

                        #endregion LazyJsonType.Object
                        break;

                    case LazyJsonType.Array:
                        #region LazyJsonType.Array

                        jsonWriterOptions.IndentLevel++;
                        WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonArray.TokenList[i], LazyJsonPathType.ArrayIndex);
                        jsonWriterOptions.IndentLevel--;

                        #endregion LazyJsonType.Array
                        break;
                }
                stringBuilder.Append(",");
            }

            if (stringBuilder[stringBuilder.Length - 1] == ',')
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append(closeBracket);
        }

        /// <summary>
        /// Write a property on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonProperty">The json property</param>
        private static void WriteProperty(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonProperty jsonProperty)
        {
            String property = "{0}{1}\"{2}\"{3}";

            if (jsonWriterOptions.Indent == true)
            {
                property = String.Format(property, Environment.NewLine, jsonWriterOptions.IndentValue, jsonProperty.Name, ": ");
            }
            else
            {
                property = String.Format(property, String.Empty, String.Empty, jsonProperty.Name, ":");
            }

            stringBuilder.Append(property);

            switch (jsonProperty.Token.Type)
            {
                case LazyJsonType.Null:
                    #region LazyJsonType.Null

                    stringBuilder.Append("null");

                    #endregion LazyJsonType.Null
                    break;

                case LazyJsonType.Boolean:
                    #region LazyJsonType.Boolean

                    LazyJsonBoolean jsonBoolean = (LazyJsonBoolean)jsonProperty.Token;
                    stringBuilder.Append(jsonBoolean.Value == null ? "null" : jsonBoolean.Value.ToString().ToLower());

                    #endregion LazyJsonType.Boolean
                    break;

                case LazyJsonType.Integer:
                    #region LazyJsonType.Integer

                    LazyJsonInteger jsonInteger = (LazyJsonInteger)jsonProperty.Token;
                    stringBuilder.Append(jsonInteger.Value == null ? "null" : jsonInteger.Value);

                    #endregion LazyJsonType.Integer
                    break;

                case LazyJsonType.Decimal:
                    #region LazyJsonType.Decimal

                    LazyJsonDecimal jsonDecimal = (LazyJsonDecimal)jsonProperty.Token;
                    stringBuilder.Append(jsonDecimal.Value == null ? "null" : jsonDecimal.Value.ToString().Replace(',', '.'));

                    #endregion LazyJsonType.Decimal
                    break;

                case LazyJsonType.String:
                    #region LazyJsonType.String

                    LazyJsonString jsonString = (LazyJsonString)jsonProperty.Token;
                    stringBuilder.Append(jsonString.Value == null ? "null" : "\"" + jsonString.Value + "\"");

                    #endregion LazyJsonType.String
                    break;

                case LazyJsonType.Object:
                    #region LazyJsonType.Object

                    WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonProperty.Token, LazyJsonPathType.Property);

                    #endregion LazyJsonType.Object
                    break;

                case LazyJsonType.Array:
                    #region LazyJsonType.Array

                    WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonProperty.Token, LazyJsonPathType.Property);

                    #endregion LazyJsonType.Array
                    break;
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonWriterOptions
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonWriterOptions()
        {
            this.Indent = true;
            this.IndentSize = 2;
            this.IndentLevel = 0;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean Indent { get; set; }

        public Int32 IndentSize { get; set; }

        internal Int32 IndentLevel { get; set; }

        internal String IndentValue { get { return String.Empty.PadRight(this.IndentSize * this.IndentLevel); } }

        #endregion Properties
    }
}