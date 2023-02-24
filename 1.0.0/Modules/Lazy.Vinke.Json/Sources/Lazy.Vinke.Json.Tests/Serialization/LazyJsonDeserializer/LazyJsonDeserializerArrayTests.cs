// LazyJsonDeserializerArrayTests.cs
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
    public class LazyJsonDeserializerArrayTests
    {
        [TestMethod]
        public void TestDeserializerArrayPropertyNull()
        {
            // Arrange
            Object resArray = null;
            LazyJsonProperty jsonProperty = null;
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(String[]));

            // Assert
            Assert.IsNull(resArray);
        }

        [TestMethod]
        public void TestDeserializerArrayTokenNull()
        {
            // Arrange
            Object resArray = null;
            LazyJsonToken jsonToken = null;
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonToken, typeof(Int32[]));

            // Assert
            Assert.IsNull(resArray);
        }

        [TestMethod]
        public void TestDeserializerArrayTokenTypeNotArray()
        {
            // Arrange
            Object resArray = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonDecimal(123.45m));
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(List<Decimal>));

            // Assert
            Assert.IsNull(resArray);
        }

        [TestMethod]
        public void TestDeserializerArrayDataTypeNull()
        {
            // Arrange
            Object resArray = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, null);

            // Assert
            Assert.IsNull(resArray);
        }

        [TestMethod]
        public void TestDeserializerArrayDataTypeNotArray()
        {
            // Arrange
            Object resArray = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(DateTime));

            // Assert
            Assert.IsNull(resArray);
        }

        [TestMethod]
        public void TestDeserializerArrayEmpty()
        {
            // Arrange
            Object resArray = null;
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", new LazyJsonArray());
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(Int32[]));

            // Assert
            Assert.IsTrue(((Array)resArray).Length == 0);
        }

        [TestMethod]
        public void TestDeserializerArrayString()
        {
            // Arrange
            Object resArray = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json"));
            jsonArray.Add(new LazyJsonString("Array"));
            jsonArray.Add(new LazyJsonString("Tests"));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(String[]));

            // Assert
            Assert.IsTrue(((String[])resArray)[0] == "Json");
            Assert.IsTrue(((String[])resArray)[1] == "Array");
            Assert.IsTrue(((String[])resArray)[2] == "Tests");
        }

        [TestMethod]
        public void TestDeserializerArrayDecimal()
        {
            // Arrange
            Object resArray = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(10.4m));
            jsonArray.Add(new LazyJsonDecimal(20.23m));
            jsonArray.Add(new LazyJsonDecimal(60.834m));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(Decimal[]));

            // Assert
            Assert.IsTrue(((Decimal[])resArray)[0] == 10.4m);
            Assert.IsTrue(((Decimal[])resArray)[1] == 20.23m);
            Assert.IsTrue(((Decimal[])resArray)[2] == 60.834m);
        }

        [TestMethod]
        public void TestDeserializerArrayBoolean()
        {
            // Arrange
            Object resArray = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(true));
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonBoolean(true));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(Boolean[]));

            // Assert
            Assert.IsTrue(((Boolean[])resArray)[0] == true);
            Assert.IsTrue(((Boolean[])resArray)[1] == false);
            Assert.IsTrue(((Boolean[])resArray)[2] == true);
        }

        [TestMethod]
        public void TestDeserializerArrayObject()
        {
            // Arrange
            Object resArray = null;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json"));
            jsonArray.Add(new LazyJsonDecimal(123.4567m));
            jsonArray.Add(new LazyJsonBoolean(true));
            jsonArray.Add(new LazyJsonInteger(50));

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Prop", jsonArray);
            LazyJsonDeserializerArray deserializerArray = new LazyJsonDeserializerArray();

            // Act
            resArray = deserializerArray.Deserialize(jsonProperty, typeof(Object[]));

            // Assert
            Assert.IsTrue(((String)((Object[])resArray)[0]) == "Json");
            Assert.IsTrue(((Decimal)((Object[])resArray)[1]) == 123.4567m);
            Assert.IsTrue(((Boolean)((Object[])resArray)[2]) == true);
            Assert.IsTrue(((Int64)((Object[])resArray)[3]) == 50);
        }
    }
}