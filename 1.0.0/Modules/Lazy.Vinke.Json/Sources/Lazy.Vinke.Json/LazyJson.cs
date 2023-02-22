// LazyJson.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 07

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJson
    {
        #region Consts

        internal const String UNNAMED_PROPERTY = "Unnamed";

        #endregion Consts

        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJson()
        {
            this.Root = new LazyJsonNull();
            this.ReaderResult = new LazyJsonReaderResult();
        }

        public LazyJson(LazyJsonToken jsonToken)
        {
            this.Root = jsonToken != null ? jsonToken : new LazyJsonNull();
            this.ReaderResult = new LazyJsonReaderResult();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Find token at the specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location of the token</param>
        /// <param name="renameWith">A new property name to the token at specified jPath if it's an object</param>
        /// <param name="replaceWith">A new token to override the token at specified jPath</param>
        /// <returns>The located token</returns>
        public LazyJsonToken Find(String jPath, String renameWith = null, LazyJsonToken replaceWith = null)
        {
            return InternalFind(jPath, renameWith: renameWith, replaceWith: replaceWith);
        }

        /// <summary>
        /// Remove a token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location of the token</param>
        /// <returns>The located token</returns>
        public LazyJsonToken Remove(String jPath)
        {
            return InternalFind(jPath, remove: true);
        }

        /// <summary>
        /// Find token at the specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location of the token</param>
        /// <param name="renameWith">A new property name to the token at specified jPath if it's an object</param>
        /// <param name="replaceWith">A new token to override the token at specified jPath</param>
        /// <param name="remove">Indicate if the token should be removed</param>
        /// <returns>The located token</returns>
        private LazyJsonToken InternalFind(String jPath, String renameWith = null, LazyJsonToken replaceWith = null, Boolean remove = false)
        {
            List<LazyJsonPathNode> pathNodeList = LazyJsonPath.Parse(jPath);

            if (pathNodeList.Count == 0)
                return null;

            LazyJsonToken jsonTokenParent = this.Root;
            LazyJsonToken jsonToken = null;
            Int32 pathIndex = 0;

            if (pathNodeList.Count == 1)
            {
                if (replaceWith != null)
                    this.Root = replaceWith;

                return jsonTokenParent;
            }

            pathNodeList.RemoveAt(0);

            #region Find parent token

            for (pathIndex = 0; pathIndex < (pathNodeList.Count - 1); pathIndex++)
            {
                if (pathNodeList[pathIndex].Type == LazyJsonPathType.Property)
                {
                    if (jsonTokenParent.Type != LazyJsonType.Object)
                        return null;

                    LazyJsonProperty jsonProperty = ((LazyJsonObject)jsonTokenParent)[((LazyJsonPathProperty)pathNodeList[pathIndex]).Name];

                    if (jsonProperty == null)
                        return null;

                    jsonTokenParent = jsonProperty.Token;
                }
                else if (pathNodeList[pathIndex].Type == LazyJsonPathType.ArrayIndex)
                {
                    if (jsonTokenParent.Type != LazyJsonType.Array)
                        return null;

                    jsonTokenParent = ((LazyJsonArray)jsonTokenParent)[Convert.ToInt32(((LazyJsonPathArrayIndex)pathNodeList[pathIndex]).Index)];
                }
            }

            #endregion Find parent token

            #region Find token

            if (pathNodeList[pathIndex].Type == LazyJsonPathType.Property)
            {
                if (jsonTokenParent.Type != LazyJsonType.Object)
                    return null;

                LazyJsonObject jsonObjectParent = (LazyJsonObject)jsonTokenParent;
                LazyJsonProperty jsonProperty = jsonObjectParent[((LazyJsonPathProperty)pathNodeList[pathIndex]).Name];

                if (jsonProperty == null)
                    return null;

                jsonToken = jsonProperty.Token;

                if (remove == true)
                {
                    jsonObjectParent.Remove(jsonProperty.Name);
                }
                else
                {
                    if (renameWith != null)
                        jsonProperty.Name = renameWith;

                    if (replaceWith != null)
                        jsonProperty.Token = replaceWith;
                }
            }
            else if (pathNodeList[pathIndex].Type == LazyJsonPathType.ArrayIndex)
            {
                if (jsonTokenParent.Type != LazyJsonType.Array)
                    return null;

                LazyJsonArray jsonArrayParent = (LazyJsonArray)jsonTokenParent;
                Int32 index = Convert.ToInt32(((LazyJsonPathArrayIndex)pathNodeList[pathIndex]).Index);

                jsonToken = jsonArrayParent[index];

                if (jsonToken != null)
                {
                    if (remove == true)
                    {
                        jsonArrayParent.TokenList.RemoveAt(index);
                    }
                    else
                    {
                        if (replaceWith != null)
                            jsonArrayParent[index] = replaceWith;
                    }
                }
            }

            #endregion Find token

            return jsonToken;
        }

        /// <summary>
        /// Append a token at root
        /// </summary>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void AppendRoot(String json)
        {
            AppendToken("$", LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Append a token at root
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        public void AppendRoot(LazyJsonToken jsonToken)
        {
            AppendToken("$", jsonToken);
        }

        /// <summary>
        /// Append a json null token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        public void AppendNull(String jPath)
        {
            AppendToken(jPath, new LazyJsonNull());
        }

        /// <summary>
        /// Append a json boolean token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="value">The value of the json boolean token</param>
        public void AppendBoolean(String jPath, Boolean? value)
        {
            AppendToken(jPath, new LazyJsonBoolean(value));
        }

        /// <summary>
        /// Append a json boolean token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonBoolean">The json boolean token</param>
        public void AppendBoolean(String jPath, LazyJsonBoolean jsonBoolean)
        {
            AppendToken(jPath, jsonBoolean);
        }

        /// <summary>
        /// Append a json integer token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="value">The value of the json integer token</param>
        public void AppendInteger(String jPath, Int64? value)
        {
            AppendToken(jPath, new LazyJsonInteger(value));
        }

        /// <summary>
        /// Append a json integer token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonInteger">The json integer token</param>
        public void AppendInteger(String jPath, LazyJsonInteger jsonInteger)
        {
            AppendToken(jPath, jsonInteger);
        }

        /// <summary>
        /// Append a json decimal token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="value">The value of the json decimal token</param>
        public void AppendDecimal(String jPath, Decimal? value)
        {
            AppendToken(jPath, new LazyJsonDecimal(value));
        }

        /// <summary>
        /// Append a json decimal token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonDecimal">The json decimal token</param>
        public void AppendDecimal(String jPath, LazyJsonDecimal jsonDecimal)
        {
            AppendToken(jPath, jsonDecimal);
        }

        /// <summary>
        /// Append a json string token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="value">The value of the json string token</param>
        public void AppendString(String jPath, String value)
        {
            AppendToken(jPath, new LazyJsonString(value));
        }

        /// <summary>
        /// Append a json string token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonString">The json string token</param>
        public void AppendString(String jPath, LazyJsonString jsonString)
        {
            AppendToken(jPath, jsonString);
        }

        /// <summary>
        /// Append a json object token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void AppendObject(String jPath, String json)
        {
            AppendToken(jPath, LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Append a json object token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonObject">The json object token</param>
        public void AppendObject(String jPath, LazyJsonObject jsonObject)
        {
            AppendToken(jPath, jsonObject);
        }

        /// <summary>
        /// Append a json array token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void AppendArray(String jPath, String json)
        {
            AppendToken(jPath, LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Append a json array token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonArray">The json array token</param>
        public void AppendArray(String jPath, LazyJsonArray jsonArray)
        {
            AppendToken(jPath, jsonArray);
        }

        /// <summary>
        /// Append a json property at specified jPath object
        /// </summary>
        /// <param name="jPath">The jPath object location to append the property</param>
        /// <param name="name">The property name</param>
        /// <param name="json">The json that will be transformed on the desired property token</param>
        public void AppendProperty(String jPath, String name, String json)
        {
            AppendToken(String.Join('.', jPath, name != null ? name : LazyJson.UNNAMED_PROPERTY), LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Append a json property at specified jPath object
        /// </summary>
        /// <param name="jPath">The jPath object location to append the property</param>
        /// <param name="name">The property name</param>
        /// <param name="jsonToken">The json property token</param>
        public void AppendProperty(String jPath, String name, LazyJsonToken jsonToken)
        {
            AppendToken(String.Join('.', jPath, name != null ? name : LazyJson.UNNAMED_PROPERTY), jsonToken);
        }

        /// <summary>
        /// Append a json token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void AppendToken(String jPath, String json)
        {
            AppendToken(jPath, LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Append a json token at specified jPath
        /// </summary>
        /// <param name="jPath">The jPath location to append the token</param>
        /// <param name="jsonToken">The json token</param>
        public void AppendToken(String jPath, LazyJsonToken jsonToken)
        {
            if (jPath == "$")
            {
                this.Root = jsonToken != null ? jsonToken : new LazyJsonNull();
            }
            else
            {
                List<LazyJsonPathNode> jsonPathNodeList = LazyJsonPath.Parse(jPath);
                String jPathParent = String.Join('.', jsonPathNodeList.ToJsonPathNodeArray(), 0, jsonPathNodeList.Count - 1);

                LazyJsonToken jsonTokenParent = Find(jPathParent);

                if (jsonTokenParent != null)
                {
                    if (jsonPathNodeList[jsonPathNodeList.Count - 1].Type == LazyJsonPathType.ArrayIndex)
                    {
                        if (jsonTokenParent.Type == LazyJsonType.Array)
                        {
                            LazyJsonPathArrayIndex jsonPathArrayIndex = (LazyJsonPathArrayIndex)jsonPathNodeList[jsonPathNodeList.Count - 1];
                            ((LazyJsonArray)jsonTokenParent)[Convert.ToInt32(jsonPathArrayIndex.Index)] = jsonToken;
                        }
                        else
                        {
                            LazyJsonArray jsonArray = new LazyJsonArray();
                            jsonArray.Add(jsonToken);

                            Find(jPathParent, replaceWith: jsonArray);
                        }
                    }
                    else if (jsonPathNodeList[jsonPathNodeList.Count - 1].Type == LazyJsonPathType.Property)
                    {
                        if (jsonTokenParent.Type == LazyJsonType.Object)
                        {
                            LazyJsonPathProperty jsonPathProperty = (LazyJsonPathProperty)jsonPathNodeList[jsonPathNodeList.Count - 1];
                            ((LazyJsonObject)jsonTokenParent)[jsonPathProperty.Name] = new LazyJsonProperty(jsonPathProperty.Name, jsonToken);
                        }
                        else
                        {
                            LazyJsonObject jsonObject = new LazyJsonObject();
                            jsonObject.Add(new LazyJsonProperty(((LazyJsonPathProperty)jsonPathNodeList[jsonPathNodeList.Count - 1]).Name, jsonToken));

                            Find(jPathParent, replaceWith: jsonObject);
                        }
                    }
                }
                else
                {
                    if (jsonPathNodeList[jsonPathNodeList.Count - 1].Type == LazyJsonPathType.ArrayIndex)
                    {
                        LazyJsonArray jsonArray = new LazyJsonArray();
                        jsonArray.Add(jsonToken);

                        AppendToken(jPathParent, jsonArray);
                    }
                    else if (jsonPathNodeList[jsonPathNodeList.Count - 1].Type == LazyJsonPathType.Property)
                    {
                        LazyJsonObject jsonObject = new LazyJsonObject();
                        jsonObject.Add(new LazyJsonProperty(((LazyJsonPathProperty)jsonPathNodeList[jsonPathNodeList.Count - 1]).Name, jsonToken));

                        AppendToken(jPathParent, jsonObject);
                    }
                }
            }
        }

        #endregion Methods

        #region Properties

        public LazyJsonToken Root { get; internal set; }

        public LazyJsonReaderResult ReaderResult { get; internal set; }

        #endregion Properties
    }

    public enum LazyJsonType
    {
        Null,
        Boolean,
        Integer,
        Decimal,
        String,
        Object,
        Array
    }

    public abstract class LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonType Type { get; protected set; }

        #endregion Properties
    }

    public class LazyJsonComments
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonComments()
        {
            this.Value = null;
        }

        public LazyJsonComments(String value)
        {
            this.Value = value;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Value { get; set; }

        public Boolean IsInLine { get { return String.IsNullOrEmpty(this.Value) == false && this.Value.StartsWith("//"); } }

        public Boolean IsInBlock { get { return String.IsNullOrEmpty(this.Value) == false && this.Value.StartsWith("/*"); } }

        public Boolean IsInBlockComplete { get { return String.IsNullOrEmpty(this.Value) == false && this.Value.StartsWith("/*") && this.Value.EndsWith("*/"); } }

        #endregion Properties
    }

    public class LazyJsonProperty
    {
        #region Variables

        private String name;
        private LazyJsonToken token;

        #endregion Variables

        #region Constructors

        public LazyJsonProperty(String name, LazyJsonToken token)
        {
            this.Name = name;
            this.Token = token;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Name
        {
            get { return this.name; }
            set { this.name = value != null ? value : LazyJson.UNNAMED_PROPERTY; }
        }

        public LazyJsonToken Token
        {
            get { return this.token; }
            set { this.token = value != null ? value : new LazyJsonNull(); }
        }

        #endregion Properties
    }

    public class LazyJsonObject : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonObject()
        {
            this.PropertyList = new List<LazyJsonProperty>();
            this.Type = LazyJsonType.Object;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add a new property to the object
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void Add(String name, String json)
        {
            this[name] = new LazyJsonProperty(name, LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Add a new property to the object
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="jsonToken">The json token</param>
        public void Add(String name, LazyJsonToken jsonToken)
        {
            this[name] = new LazyJsonProperty(name, jsonToken);
        }

        /// <summary>
        /// Add a new property to the object
        /// </summary>
        /// <param name="property">The json property</param>
        public void Add(LazyJsonProperty property)
        {
            this[property.Name] = property;
        }

        /// <summary>
        /// Remove a property from the object
        /// </summary>
        /// <param name="propertyName">The property name</param>
        public void Remove(String propertyName)
        {
            for (Int32 index = 0; index < this.PropertyList.Count; index++)
            {
                if (this.PropertyList[index] != null && this.PropertyList[index].Name == propertyName)
                {
                    this.PropertyList.RemoveAt(index);
                    break;
                }
            }
        }

        #endregion Methods

        #region Properties

        public Int32 Count { get { return this.PropertyList.Count; } }

        // The property must be internal to avoid duplicated property name been added
        internal List<LazyJsonProperty> PropertyList { get; set; }

        #endregion Properties

        #region Indexers

        public LazyJsonProperty this[Int32 index]
        {
            get
            {
                if (index < this.PropertyList.Count)
                    return this.PropertyList[index];

                return null;
            }
        }

        public LazyJsonProperty this[String name]
        {
            get
            {
                foreach (LazyJsonProperty jsonProperty in this.PropertyList)
                {
                    if (jsonProperty != null && jsonProperty.Name == name)
                        return jsonProperty;
                }

                return null;
            }
            set
            {
                Int32 index = 0;

                for (index = 0; index < this.PropertyList.Count; index++)
                {
                    if (this.PropertyList[index] != null && this.PropertyList[index].Name == name)
                    {
                        if (value != null)
                        {
                            if (value.Token == null)
                                value.Token = new LazyJsonNull();

                            this.PropertyList[index] = value;
                        }
                        else
                        {
                            this.PropertyList[index] = new LazyJsonProperty(name, new LazyJsonNull());
                        }

                        break;
                    }
                }

                if (index == this.PropertyList.Count)
                {
                    if (value != null)
                    {
                        if (value.Token == null)
                            value.Token = new LazyJsonNull();

                        this.PropertyList.Add(value);
                    }
                    else
                    {
                        this.PropertyList.Add(new LazyJsonProperty(name, new LazyJsonNull()));
                    }
                }
            }
        }

        #endregion Indexers
    }

    public class LazyJsonArray : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonArray()
        {
            this.TokenList = new List<LazyJsonToken>();
            this.Type = LazyJsonType.Array;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add a new token to the array
        /// </summary>
        /// <param name="json">The json that will be transformed on the desired token</param>
        public void Add(String json)
        {
            this.TokenList.Add(LazyJsonReader.Read(json).Root);
        }

        /// <summary>
        /// Add a new token to the array
        /// </summary>
        /// <param name="token">The json token</param>
        public void Add(LazyJsonToken token)
        {
            this.TokenList.Add(token != null ? token : new LazyJsonNull());
        }

        /// <summary>
        /// Remote a token at the given index
        /// </summary>
        /// <param name="index">The index of the token to be removed</param>
        public void Remove(Int32 index)
        {
            if (index >= 0 && index < this.TokenList.Count)
                this.TokenList.RemoveAt(index);
        }

        #endregion Methods

        #region Properties

        public Int32 Count { get { return this.TokenList.Count; } }

        internal List<LazyJsonToken> TokenList { get; set; }

        #endregion Properties

        #region Indexers

        public LazyJsonToken this[Int32 index]
        {
            get
            {
                if (index >= 0 && index < this.TokenList.Count)
                    return this.TokenList[index];

                return null;
            }
            set
            {
                if (index >= 0)
                {
                    if (index < this.TokenList.Count)
                    {
                        this.TokenList[index] = value != null ? value : new LazyJsonNull();
                    }
                    else if (index == this.TokenList.Count)
                    {
                        this.TokenList.Add(value != null ? value : new LazyJsonNull());
                    }
                }
            }
        }

        #endregion Indexers
    }

    public class LazyJsonNull : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonNull()
        {
            this.Type = LazyJsonType.Null;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public class LazyJsonBoolean : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonBoolean()
        {
            this.Value = null;
            this.Type = LazyJsonType.Boolean;
        }

        public LazyJsonBoolean(Boolean? value)
        {
            this.Value = value;
            this.Type = LazyJsonType.Boolean;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean? Value { get; set; }

        #endregion Properties
    }

    public class LazyJsonInteger : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonInteger()
        {
            this.Value = null;
            this.Type = LazyJsonType.Integer;
        }

        public LazyJsonInteger(Int64? value)
        {
            this.Value = value;
            this.Type = LazyJsonType.Integer;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Int64? Value { get; set; }

        #endregion Properties
    }

    public class LazyJsonDecimal : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDecimal()
        {
            this.Value = null;
            this.Type = LazyJsonType.Decimal;
        }

        public LazyJsonDecimal(Decimal? value)
        {
            this.Value = value;
            this.Type = LazyJsonType.Decimal;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Decimal? Value { get; set; }

        #endregion Properties
    }

    public class LazyJsonString : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonString()
        {
            this.Value = null;
            this.Type = LazyJsonType.String;
        }

        public LazyJsonString(String value)
        {
            this.Value = value;
            this.Type = LazyJsonType.String;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Value { get; set; }

        #endregion Properties
    }
}