// LazyJsonReader.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 12

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonReader
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Read a json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <returns>The lazy json</returns>
        public static LazyJson Read(String json, LazyJsonReaderOptions jsonReaderOptions = null)
        {
            LazyJson lazyJson = new LazyJson();

            if (jsonReaderOptions == null)
                jsonReaderOptions = new LazyJsonReaderOptions();

            ReadRoot(json, jsonReaderOptions, lazyJson);

            return lazyJson;
        }

        /// <summary>
        /// Read the root node of the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="lazyJson">The lazy json</param>
        private static void ReadRoot(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJson lazyJson)
        {
            Int32 line = 1;
            Int32 index = 0;

            while (index < json.Length)
            {
                if (json[index] == ' ') { index++; continue; }
                if (json[index] == '\r') { index++; continue; }
                if (json[index] == '\n') { index++; line++; continue; }
                if (json[index] == '\t') { index++; continue; }

                if (json[index] == '/')
                {
                    index++;
                    LazyJsonComments jsonComments = new LazyJsonComments();
                    ReadComments(json, jsonReaderOptions, lazyJson.ReaderResult, jsonComments, ref line, ref index);

                    if (index >= json.Length && jsonComments.IsInBlock == true && jsonComments.IsInBlockComplete == false)
                    {
                        lazyJson.ReaderResult.Success = false;
                        lazyJson.ReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, "*/", String.Empty));
                    }
                }
                else if (json[index] == '[')
                {
                    index++;
                    LazyJsonArray jsonArray = new LazyJsonArray();
                    ReadArray(json, jsonReaderOptions, lazyJson.ReaderResult, jsonArray, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonArray;

                    break;
                }
                else if (json[index] == '{')
                {
                    index++;
                    LazyJsonObject jsonObject = new LazyJsonObject();
                    ReadObject(json, jsonReaderOptions, lazyJson.ReaderResult, jsonObject, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonObject;

                    break;
                }
                else if (json[index] == '\"')
                {
                    index++;
                    LazyJsonString jsonString = new LazyJsonString();
                    ReadString(json, jsonReaderOptions, lazyJson.ReaderResult, jsonString, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonString;

                    break;
                }
                else if (json[index] == '-')
                {
                    LazyJsonToken jsonToken = null;
                    ReadIntegerOrDecimalNegative(json, jsonReaderOptions, lazyJson.ReaderResult, out jsonToken, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonToken;

                    break;
                }
                else if (Char.IsDigit(json[index]) == true)
                {
                    LazyJsonToken jsonToken = null;
                    ReadIntegerOrDecimal(json, jsonReaderOptions, lazyJson.ReaderResult, out jsonToken, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonToken;

                    break;
                }
                else if (Char.IsLetter(json[index]) == true)
                {
                    LazyJsonToken jsonToken = null;
                    ReadNullOrBoolean(json, jsonReaderOptions, lazyJson.ReaderResult, out jsonToken, ref line, ref index);

                    if (lazyJson.ReaderResult.Success == true)
                        lazyJson.Root = jsonToken;

                    break;
                }
                else
                {
                    lazyJson.Root = new LazyJsonNull();

                    lazyJson.ReaderResult.Success = false;
                    lazyJson.ReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, "//", json[index]));
                    index = json.Length; // Exit

                    break;
                }
            }

            while (index < json.Length)
            {
                if (json[index] == ' ') { index++; continue; }
                if (json[index] == '\r') { index++; continue; }
                if (json[index] == '\n') { index++; line++; continue; }
                if (json[index] == '\t') { index++; continue; }

                if (json[index] == '/')
                {
                    index++;
                    LazyJsonComments jsonComments = new LazyJsonComments();
                    ReadComments(json, jsonReaderOptions, lazyJson.ReaderResult, jsonComments, ref line, ref index);

                    if (index >= json.Length && jsonComments.IsInBlock == true && jsonComments.IsInBlockComplete == false)
                    {
                        lazyJson.ReaderResult.Success = false;
                        lazyJson.ReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, "*/", String.Empty));
                    }
                }
                else
                {
                    lazyJson.Root = new LazyJsonNull();

                    lazyJson.ReaderResult.Success = false;
                    lazyJson.ReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, "//", json[index]));
                    index = json.Length; // Exit

                    break;
                }
            }
        }

        /// <summary>
        /// Read a comment on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonComments">The json comment</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadComments(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, LazyJsonComments jsonComments, ref Int32 line, ref Int32 index)
        {
            if (index < json.Length && json[index] == '/')
            {
                index++;

                Int32 startIndex = index;
                while (index < json.Length && json[index] != '\r' && json[index] != '\n')
                    index++;

                jsonComments.Value = "//" + json.Substring(startIndex, index - startIndex);
            }
            else if (index < json.Length && json[index] == '*')
            {
                index++;

                Int32 startIndex = index;
                Int32 jsonLength = json.Length - 1;
                while (index < jsonLength && (json[index] != '*' || (json[index] == '*' && json[index + 1] != '/')))
                    index++;

                if (index < jsonLength)
                {
                    jsonComments.Value = "/*" + json.Substring(startIndex, index - startIndex) + "*/";
                    index += 2; // Jump */ terminators
                }
                else
                {
                    jsonComments.Value = "/*" + json.Substring(startIndex, index - startIndex);
                    index++; // Because jsonLenght is "json.Length - 1"
                }
            }
            else
            {
                jsonReaderResult.Success = false;
                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, "/", index < json.Length ? json[index] : String.Empty));
                index = json.Length; // Exit
            }
        }

        /// <summary>
        /// Read an array on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonArray">The json array</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadArray(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, LazyJsonArray jsonArray, ref Int32 line, ref Int32 index)
        {
            while (index < json.Length)
            {
                if (json[index] == ' ') { index++; continue; }
                if (json[index] == '\r') { index++; continue; }
                if (json[index] == '\n') { index++; line++; continue; }
                if (json[index] == '\t') { index++; continue; }

                if (json[index] == '/')
                {
                    index++;
                    LazyJsonComments jsonComments = new LazyJsonComments();
                    ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                    if (index >= json.Length)
                    {
                        jsonReaderResult.Success = false;
                        jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidArrayItem, line, String.Empty));
                    }
                }
                else
                {
                    if (json[index] == '[')
                    {
                        index++;
                        LazyJsonArray jsonArrayInner = new LazyJsonArray();
                        ReadArray(json, jsonReaderOptions, jsonReaderResult, jsonArrayInner, ref line, ref index);
                        jsonArray.TokenList.Add(jsonArrayInner);
                    }
                    else if (json[index] == '{')
                    {
                        index++;
                        LazyJsonObject jsonObject = new LazyJsonObject();
                        ReadObject(json, jsonReaderOptions, jsonReaderResult, jsonObject, ref line, ref index);
                        jsonArray.TokenList.Add(jsonObject);
                    }
                    else if (json[index] == '\"')
                    {
                        index++;
                        LazyJsonString jsonString = new LazyJsonString();
                        ReadString(json, jsonReaderOptions, jsonReaderResult, jsonString, ref line, ref index);
                        jsonArray.TokenList.Add(jsonString);
                    }
                    else if (json[index] == '-')
                    {
                        LazyJsonToken jsonToken = null;
                        ReadIntegerOrDecimalNegative(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                        jsonArray.TokenList.Add(jsonToken);
                    }
                    else if (Char.IsDigit(json[index]) == true)
                    {
                        LazyJsonToken jsonToken = null;
                        ReadIntegerOrDecimal(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                        jsonArray.TokenList.Add(jsonToken);
                    }
                    else if (Char.IsLetter(json[index]) == true)
                    {
                        LazyJsonToken jsonToken = null;
                        ReadNullOrBoolean(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                        jsonArray.TokenList.Add(jsonToken);
                    }
                    else if (json[index] == ']' && jsonArray.TokenList.Count == 0)
                    {
                        index++;
                        break;
                    }
                    else
                    {
                        jsonReaderResult.Success = false;
                        jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidArrayItem, line, json[index]));
                        index = json.Length; // Exit
                    }

                    while (index < json.Length)
                    {
                        if (json[index] == ' ') { index++; continue; }
                        if (json[index] == '\r') { index++; continue; }
                        if (json[index] == '\n') { index++; line++; continue; }
                        if (json[index] == '\t') { index++; continue; }

                        if (json[index] == '/')
                        {
                            index++;
                            LazyJsonComments jsonComments = new LazyJsonComments();
                            ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                            if (index >= json.Length)
                            {
                                jsonReaderResult.Success = false;
                                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, ", | ]", String.Empty));
                            }
                        }
                        else if (json[index] == ',' || json[index] == ']')
                        {
                            break;
                        }
                        else
                        {
                            jsonReaderResult.Success = false;
                            jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, ", | ]", json[index]));
                            index = json.Length; // Exit
                        }
                    }

                    if (index < json.Length)
                    {
                        if (json[index] == ',') { index++; continue; }
                        if (json[index] == ']') { index++; break; }
                    }
                }
            }
        }

        /// <summary>
        /// Read an object on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonObject">The json object</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadObject(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, LazyJsonObject jsonObject, ref Int32 line, ref Int32 index)
        {
            while (index < json.Length)
            {
                if (json[index] == ' ') { index++; continue; }
                if (json[index] == '\r') { index++; continue; }
                if (json[index] == '\n') { index++; line++; continue; }
                if (json[index] == '\t') { index++; continue; }

                if (json[index] == '/')
                {
                    index++;
                    LazyJsonComments jsonComments = new LazyJsonComments();
                    ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                    if (index >= json.Length)
                    {
                        jsonReaderResult.Success = false;
                        jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, " \" | } ", String.Empty));
                    }
                }
                else if (json[index] == '\"')
                {
                    index++;
                    LazyJsonProperty jsonProperty = new LazyJsonProperty(LazyJson.UNNAMED_PROPERTY, new LazyJsonNull());
                    ReadProperty(json, jsonReaderOptions, jsonReaderResult, jsonProperty, ref line, ref index);

                    if (jsonObject[jsonProperty.Name] == null)
                    {
                        jsonObject.Add(jsonProperty);
                    }
                    else
                    {
                        jsonReaderResult.Success = false;
                        jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderObjectPropertyDuplicated, line, jsonProperty.Name));
                        index = json.Length; // Exit
                    }

                    while (index < json.Length)
                    {
                        if (json[index] == ' ') { index++; continue; }
                        if (json[index] == '\r') { index++; continue; }
                        if (json[index] == '\n') { index++; line++; continue; }
                        if (json[index] == '\t') { index++; continue; }

                        if (json[index] == '/')
                        {
                            index++;
                            LazyJsonComments jsonComments = new LazyJsonComments();
                            ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                            if (index >= json.Length)
                            {
                                jsonReaderResult.Success = false;
                                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, ", | }", String.Empty));
                            }
                        }
                        else if (json[index] == ',' || json[index] == '}')
                        {
                            break;
                        }
                        else
                        {
                            jsonReaderResult.Success = false;
                            jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, ", | }", json[index]));
                            index = json.Length; // Exit
                        }
                    }

                    if (index < json.Length)
                    {
                        if (json[index] == ',') { index++; continue; }
                        if (json[index] == '}') { index++; break; }
                    }
                }
                else if (json[index] == '}' && jsonObject.PropertyList.Count == 0)
                {
                    index++;
                    break;
                }
                else
                {
                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, " \" ", String.Empty));
                    index = json.Length; // Exit
                }
            }
        }

        /// <summary>
        /// Read a property on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadProperty(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, LazyJsonProperty jsonProperty, ref Int32 line, ref Int32 index)
        {
            LazyJsonString jsonStringPropertyName = new LazyJsonString();
            ReadString(json, jsonReaderOptions, jsonReaderResult, jsonStringPropertyName, ref line, ref index);
            jsonProperty.Name = jsonStringPropertyName.Value;

            if (index < json.Length)
            {
                while (index < json.Length)
                {
                    if (json[index] == ' ') { index++; continue; }
                    if (json[index] == '\r') { index++; continue; }
                    if (json[index] == '\n') { index++; line++; continue; }
                    if (json[index] == '\t') { index++; continue; }

                    if (json[index] == '/')
                    {
                        index++;
                        LazyJsonComments jsonComments = new LazyJsonComments();
                        ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                        if (index >= json.Length)
                        {
                            jsonReaderResult.Success = false;
                            jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, " : ", String.Empty));
                        }
                    }
                    else if (json[index] == ':')
                    {
                        index++;
                        break;
                    }
                    else
                    {
                        jsonReaderResult.Success = false;
                        jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidObjectPropertyValue, line, json[index]));
                        index = json.Length; // Exit
                    }
                }

                if (index < json.Length)
                {
                    while (index < json.Length)
                    {
                        if (json[index] == ' ') { index++; continue; }
                        if (json[index] == '\r') { index++; continue; }
                        if (json[index] == '\n') { index++; line++; continue; }
                        if (json[index] == '\t') { index++; continue; }

                        if (json[index] == '/')
                        {
                            index++;
                            LazyJsonComments jsonComments = new LazyJsonComments();
                            ReadComments(json, jsonReaderOptions, jsonReaderResult, jsonComments, ref line, ref index);

                            if (index >= json.Length)
                            {
                                jsonReaderResult.Success = false;
                                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, " : ", String.Empty));
                            }
                        }
                        else if (json[index] == '[')
                        {
                            index++;
                            LazyJsonArray jsonArray = new LazyJsonArray();
                            ReadArray(json, jsonReaderOptions, jsonReaderResult, jsonArray, ref line, ref index);
                            jsonProperty.Token = jsonArray;
                            break;
                        }
                        else if (json[index] == '{')
                        {
                            index++;
                            LazyJsonObject jsonObject = new LazyJsonObject();
                            ReadObject(json, jsonReaderOptions, jsonReaderResult, jsonObject, ref line, ref index);
                            jsonProperty.Token = jsonObject;
                            break;
                        }
                        else if (json[index] == '\"')
                        {
                            index++;
                            LazyJsonString jsonString = new LazyJsonString();
                            ReadString(json, jsonReaderOptions, jsonReaderResult, jsonString, ref line, ref index);
                            jsonProperty.Token = jsonString;
                            break;
                        }
                        else if (json[index] == '-')
                        {
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimalNegative(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                            jsonProperty.Token = jsonToken;
                            break;
                        }
                        else if (Char.IsDigit(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimal(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                            jsonProperty.Token = jsonToken;
                            break;
                        }
                        else if (Char.IsLetter(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadNullOrBoolean(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);
                            jsonProperty.Token = jsonToken;
                            break;
                        }
                        else
                        {
                            jsonReaderResult.Success = false;
                            jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidObjectPropertyValue, line, json[index]));
                            index = json.Length; // Exit
                        }
                    }
                }
                else
                {
                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidObjectPropertyValue, line, String.Empty));
                }
            }
            else
            {
                jsonReaderResult.Success = false;
                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderInvalidObjectPropertyValue, line, String.Empty));
            }
        }

        /// <summary>
        /// Read a string on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonString">The json string</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadString(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, LazyJsonString jsonString, ref Int32 line, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length && json[index] != '\"')
                index = json[index] == '\\' ? index + 2 : index + 1;

            if (index < json.Length || (index == json.Length && json[index - 1] == '\"'))
            {
                jsonString.Value = json.Substring(startIndex, index - startIndex);
                index++; // Jump last "
            }
            else
            {
                jsonReaderResult.Success = false;
                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedCharacter, line, " \" ", String.Empty));
            }
        }

        /// <summary>
        /// Read null or boolean on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadNullOrBoolean(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length && Char.IsLetter(json[index]) == true)
                index++;

            String token = json.Substring(startIndex, index - startIndex);

            if (token.ToLower() == "null")
            {
                jsonToken = new LazyJsonNull();

                if (jsonReaderOptions.CaseSensitive == true && token != "null")
                {
                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "null", token));
                    index = json.Length; // Exit
                }
            }
            else if (token.ToLower() == "true")
            {
                jsonToken = new LazyJsonBoolean(true);

                if (jsonReaderOptions.CaseSensitive == true && token != "true")
                {
                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "true", token));
                    index = json.Length; // Exit
                }
            }
            else if (token.ToLower() == "false")
            {
                jsonToken = new LazyJsonBoolean(false);

                if (jsonReaderOptions.CaseSensitive == true && token != "false")
                {
                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "false", token));
                    index = json.Length; // Exit
                }
            }
            else
            {
                jsonToken = new LazyJsonNull();

                jsonReaderResult.Success = false;
                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "null|true|false", token));
                index = json.Length; // Exit
            }
        }

        /// <summary>
        /// Read an negative integer or decimal on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadIntegerOrDecimalNegative(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 index)
        {
            index++;

            if (index < json.Length && Char.IsDigit(json[index]) == true)
            {
                ReadIntegerOrDecimal(json, jsonReaderOptions, jsonReaderResult, out jsonToken, ref line, ref index);

                if (jsonToken != null)
                {
                    if (jsonToken.Type == LazyJsonType.Integer) { ((LazyJsonInteger)jsonToken).Value *= (-1); };
                    if (jsonToken.Type == LazyJsonType.Decimal) { ((LazyJsonDecimal)jsonToken).Value *= (-1); };
                }
            }
            else
            {
                jsonToken = new LazyJsonNull();

                jsonReaderResult.Success = false;
                jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "0-9", index < json.Length ? json[index] : String.Empty));
                index = json.Length; // Exit
            }
        }

        /// <summary>
        /// Read an integer or decimal on the json
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonReaderResult">The json reader result</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="index">The current index</param>
        private static void ReadIntegerOrDecimal(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJsonReaderResult jsonReaderResult, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length && Char.IsDigit(json[index]) == true)
                index++;

            if (index < json.Length && json[index] == '.')
            {
                index++;
                while (index < json.Length && Char.IsDigit(json[index]) == true)
                    index++;
            }

            String token = json.Substring(startIndex, index - startIndex);

            if (token.Contains('.') == true)
            {
                if (token.Substring(token.IndexOf('.') + 1).Length > 0)
                {
                    jsonToken = new LazyJsonDecimal(Convert.ToDecimal(token, CultureInfo.InvariantCulture));
                }
                else
                {
                    jsonToken = new LazyJsonNull();

                    jsonReaderResult.Success = false;
                    jsonReaderResult.ErrorList.Add(String.Format(Properties.LazyResourcesJson.LazyJsonReaderUnexpectedToken, line, "0-9", index < json.Length ? json[index] : String.Empty));
                    index = json.Length; // Exit
                }
            }
            else
            {
                jsonToken = new LazyJsonInteger(Convert.ToInt64(token));
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonReaderOptions
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonReaderOptions()
        {
            this.CaseSensitive = true;
            this.IgnoreComments = true;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean CaseSensitive { get; set; }

        public Boolean IgnoreComments { get; set; }

        #endregion Properties
    }

    public class LazyJsonReaderResult
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonReaderResult()
        {
            this.Success = true;
            this.ErrorList = new List<String>();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean Success { get; internal set; }

        public List<String> ErrorList { get; internal set; }

        #endregion Properties
    }
}