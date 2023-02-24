// LazyJsonDeserializerDictionaryTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 23

using System;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    [TestClass]
    public class LazyJsonDeserializerDictionaryTests
    {
        [TestMethod]
        public void TestDeserializerDictionaryPropertyNull()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<String, String>));

            // Assert
            Assert.IsNull(resDictionary);
        }

        [TestMethod]
        public void TestDeserializerDictionaryTokenNull()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonToken, typeof(Dictionary<Int32, Int32>));

            // Assert
            Assert.IsNull(resDictionary);
        }

        [TestMethod]
        public void TestDeserializerDictionaryTokenTypeNotDictionary()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(123.45m));
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<String, Decimal>));

            // Assert
            Assert.IsNull(resDictionary);
        }

        [TestMethod]
        public void TestDeserializerDictionaryDataTypeNull()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resDictionary);
        }

        [TestMethod]
        public void TestDeserializerDictionaryDataTypeNotDictionary()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resDictionary);
        }

        [TestMethod]
        public void TestDeserializerDictionaryTokenChildElementNotArray()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json"));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<Int32, String>));

            // Assert
            Assert.IsTrue(((Dictionary<Int32, String>)resDictionary).Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDictionaryTokenChildElementArrayNotMatch()
        {
            // Arrange
            Object resDictionary = null;

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonInteger(10));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonInteger(20));
            jsonArrayKeyPair2.Add(new LazyJsonString("Dictionary"));
            jsonArrayKeyPair2.Add(new LazyJsonDecimal(78.43m));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<Int32, String>));

            // Assert
            Assert.IsTrue(((Dictionary<Int32, String>)resDictionary).Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDictionaryEmpty()
        {
            // Arrange
            Object resDictionary = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<Int32, Int32>));

            // Assert
            Assert.IsTrue(((Dictionary<Int32, Int32>)resDictionary).Count == 0);
        }

        [TestMethod]
        public void TestDeserializerDictionaryIntegerString()
        {
            // Arrange
            Object resDictionary = null;

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonInteger(10));
            jsonArrayKeyPair1.Add(new LazyJsonString("Json"));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonInteger(20));
            jsonArrayKeyPair2.Add(new LazyJsonString("Dictionary"));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonInteger(30));
            jsonArrayKeyPair3.Add(new LazyJsonString("Tests"));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<Int32, String>));

            // Assert
            Assert.IsTrue(((Dictionary<Int32, String>)resDictionary)[10] == "Json");
            Assert.IsTrue(((Dictionary<Int32, String>)resDictionary)[20] == "Dictionary");
            Assert.IsTrue(((Dictionary<Int32, String>)resDictionary)[30] == "Tests");
        }

        [TestMethod]
        public void TestDeserializerDictionaryStringDecimal()
        {
            // Arrange
            Object resDictionary = null;

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonString("Json"));
            jsonArrayKeyPair1.Add(new LazyJsonDecimal(123.45m));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonString("Dictionary"));
            jsonArrayKeyPair2.Add(new LazyJsonDecimal(234.56m));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonString("Tests"));
            jsonArrayKeyPair3.Add(new LazyJsonDecimal(345.67m));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<String, Decimal>));

            // Assert
            Assert.IsTrue(((Dictionary<String, Decimal>)resDictionary)["Json"] == 123.45m);
            Assert.IsTrue(((Dictionary<String, Decimal>)resDictionary)["Dictionary"] == 234.56m);
            Assert.IsTrue(((Dictionary<String, Decimal>)resDictionary)["Tests"] == 345.67m);
        }

        [TestMethod]
        public void TestDeserializerDictionaryStringBoolean()
        {
            // Arrange
            Object resDictionary = null;

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonString("Json"));
            jsonArrayKeyPair1.Add(new LazyJsonBoolean(true));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonString("Dictionary"));
            jsonArrayKeyPair2.Add(new LazyJsonBoolean(false));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonString("Tests"));
            jsonArrayKeyPair3.Add(new LazyJsonBoolean(false));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<String, Boolean>));

            // Assert
            Assert.IsTrue(((Dictionary<String, Boolean>)resDictionary)["Json"] == true);
            Assert.IsTrue(((Dictionary<String, Boolean>)resDictionary)["Dictionary"] == false);
            Assert.IsTrue(((Dictionary<String, Boolean>)resDictionary)["Tests"] == false);
        }

        [TestMethod]
        public void TestDeserializerDictionaryIntegerObject()
        {
            // Arrange
            Object resDictionary = null;

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonInteger(0));
            jsonArrayKeyPair1.Add(new LazyJsonBoolean(false));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonInteger(1));
            jsonArrayKeyPair2.Add(new LazyJsonString("Json"));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonInteger(2));
            jsonArrayKeyPair3.Add(new LazyJsonDecimal(10.4m));

            LazyJsonArray jsonArrayKeyPair4 = new LazyJsonArray();
            jsonArrayKeyPair4.Add(new LazyJsonInteger(3));
            jsonArrayKeyPair4.Add(new LazyJsonInteger(158));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);
            jsonArray.Add(jsonArrayKeyPair4);

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerDictionary deserializerDictionary = new LazyJsonDeserializerDictionary();

            // Act
            resDictionary = deserializerDictionary.Deserialize(jsonProperty, typeof(Dictionary<Int32, Object>));

            // Assert
            Assert.IsTrue(((Boolean)((Dictionary<Int32, Object>)resDictionary)[0]) == false);
            Assert.IsTrue(((String)((Dictionary<Int32, Object>)resDictionary)[1]) == "Json");
            Assert.IsTrue(((Decimal)((Dictionary<Int32, Object>)resDictionary)[2]) == 10.4m);
            Assert.IsTrue(((Int64)((Dictionary<Int32, Object>)resDictionary)[3]) == 158);
        }
    }
}