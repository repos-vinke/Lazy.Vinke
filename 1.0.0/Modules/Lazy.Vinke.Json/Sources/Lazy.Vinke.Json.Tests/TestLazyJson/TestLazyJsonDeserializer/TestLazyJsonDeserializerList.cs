// TestLazyJsonDeserializerList.cs
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
    public class TestLazyJsonDeserializerList
    {
        [TestMethod]
        public void TestDeserializerListPropertyNull()
        {
            // Arrange
            Object resList = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<String>));

            // Assert
            Assert.IsNull(resList);
        }

        [TestMethod]
        public void TestDeserializerListTokenNull()
        {
            // Arrange
            Object resList = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonToken, typeof(List<Int32>));

            // Assert
            Assert.IsNull(resList);
        }

        [TestMethod]
        public void TestDeserializerListTokenTypeNotList()
        {
            // Arrange
            Object resList = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(123.45m));
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<Decimal>));

            // Assert
            Assert.IsNull(resList);
        }

        [TestMethod]
        public void TestDeserializerListDataTypeNull()
        {
            // Arrange
            Object resList = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resList);
        }

        [TestMethod]
        public void TestDeserializerListDataTypeNotList()
        {
            // Arrange
            Object resList = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resList);
        }

        [TestMethod]
        public void TestDeserializerListEmpty()
        {
            // Arrange
            Object resList = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<Int32>));

            // Assert
            Assert.IsTrue(((List<Int32>)resList).Count == 0);
        }

        [TestMethod]
        public void TestDeserializerListString()
        {
            // Arrange
            Object resList = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json"));
            jsonArray.Add(new LazyJsonString("List"));
            jsonArray.Add(new LazyJsonString("Tests"));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<String>));

            // Assert
            Assert.IsTrue(((List<String>)resList)[0] == "Json");
            Assert.IsTrue(((List<String>)resList)[1] == "List");
            Assert.IsTrue(((List<String>)resList)[2] == "Tests");
        }

        [TestMethod]
        public void TestDeserializerListDecimal()
        {
            // Arrange
            Object resList = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(30.7m));
            jsonArray.Add(new LazyJsonDecimal(403.89m));
            jsonArray.Add(new LazyJsonDecimal(8057.246m));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<Decimal>));

            // Assert
            Assert.IsTrue(((List<Decimal>)resList)[0] == 30.7m);
            Assert.IsTrue(((List<Decimal>)resList)[1] == 403.89m);
            Assert.IsTrue(((List<Decimal>)resList)[2] == 8057.246m);
        }

        [TestMethod]
        public void TestDeserializerListBoolean()
        {
            // Arrange
            Object resList = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonBoolean(true));
            jsonArray.Add(new LazyJsonBoolean(false));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<Boolean>));

            // Assert
            Assert.IsTrue(((List<Boolean>)resList)[0] == false);
            Assert.IsTrue(((List<Boolean>)resList)[1] == true);
            Assert.IsTrue(((List<Boolean>)resList)[2] == false);
        }

        [TestMethod]
        public void TestDeserializerListObject()
        {
            // Arrange
            Object resList = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json Tests"));
            jsonArray.Add(new LazyJsonDecimal(456.789m));
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonInteger(750));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerList deserializerList = new LazyJsonDeserializerList();

            // Act
            resList = deserializerList.Deserialize(jsonProperty, typeof(List<Object>));

            // Assert
            Assert.IsTrue(((String)((List<Object>)resList)[0]) == "Json Tests");
            Assert.IsTrue(((Decimal)((List<Object>)resList)[1]) == 456.789m);
            Assert.IsTrue(((Boolean)((List<Object>)resList)[2]) == false);
            Assert.IsTrue(((Int64)((List<Object>)resList)[3]) == 750);
        }
    }
}