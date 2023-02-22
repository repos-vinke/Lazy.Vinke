// LazyJsonPath.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2022, January 08

using System;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonPath
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Parse a jPath to a list of json path node
        /// </summary>
        /// <param name="jPath">The jPath to be parsed</param>
        /// <returns>The list of json path node</returns>
        public static List<LazyJsonPathNode> Parse(String jPath)
        {
            List<LazyJsonPathNode> pathNodeList = new List<LazyJsonPathNode>();

            if (String.IsNullOrEmpty(jPath) == false && jPath.StartsWith("$") == true)
            {
                pathNodeList.Add(new LazyJsonPathProperty("$"));

                Int32 index = 1;
                Int32 startIndex = 0;

                while (index < jPath.Length)
                {
                    if (jPath[index] == '.')
                    {
                        index++;
                    }
                    else if (jPath[index] == '[')
                    {
                        startIndex = index;
                        while (index < jPath.Length && jPath[index] != ']')
                            index++;

                        pathNodeList.Add(new LazyJsonPathArrayIndex(jPath.Substring(startIndex + 1, index - startIndex - 1)));
                        index++;
                    }
                    else
                    {
                        startIndex = index;
                        while (index < jPath.Length && jPath[index] != '.' && jPath[index] != '[')
                            index++;

                        pathNodeList.Add(new LazyJsonPathProperty(jPath.Substring(startIndex, index - startIndex)));
                    }
                }
            }

            return pathNodeList;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    public enum LazyJsonPathType
    {
        Property,
        ArrayIndex
    }

    public class LazyJsonPathNode
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonPathType Type { get; protected set; }

        #endregion Properties
    }

    public class LazyJsonPathProperty : LazyJsonPathNode
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonPathProperty(String name)
        {
            this.Name = name;
            this.Type = LazyJsonPathType.Property;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Name { get; private set; }

        #endregion Properties
    }

    public class LazyJsonPathArrayIndex : LazyJsonPathNode
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonPathArrayIndex(String index)
        {
            this.Index = index;
            this.Type = LazyJsonPathType.ArrayIndex;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Index { get; private set; }

        #endregion Properties
    }

    public static class LazyJsonPathExtensions
    {
        /// <summary>
        /// Convert a list of json path node into array of string
        /// </summary>
        /// <param name="jsonPathNodeList">The list of json path node</param>
        /// <returns>The array of string</returns>
        public static String[] ToJsonPathNodeArray(this List<LazyJsonPathNode> jsonPathNodeList)
        {
            String[] jsonPathNodeArray = new String[jsonPathNodeList.Count];
            for (int i = 0; i < jsonPathNodeList.Count; i++)
                jsonPathNodeArray[i] = jsonPathNodeList[i].Type == LazyJsonPathType.Property ? ((LazyJsonPathProperty)jsonPathNodeList[i]).Name : "[" + ((LazyJsonPathArrayIndex)jsonPathNodeList[i]).Index + "]";

            return jsonPathNodeArray;
        }
    }
}