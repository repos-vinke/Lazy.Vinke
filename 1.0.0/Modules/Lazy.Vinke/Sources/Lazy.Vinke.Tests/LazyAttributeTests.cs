// LazyAttributeTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke;

namespace Lazy.Vinke.Tests
{
    [TestClass]
    public class LazyAttributeTests
    {
        [TestMethod]
        public void TestGetCustomAttributeFromClass()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromClass<LazyAttributeGeneric>(typeof(LazySampleAttributeClass));

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "TClass");
            Assert.IsTrue(attributeGeneric.Name == "Test Class");
        }

        [TestMethod]
        public void TestGetCustomAttributeFromClassField()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromClassField<LazyAttributeGeneric>(typeof(LazySampleAttributeClass), "identity");

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "Id");
            Assert.IsTrue(attributeGeneric.Name == "Identity");
        }

        [TestMethod]
        public void TestGetCustomAttributeFromClassMethod()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromClassMethod<LazyAttributeGeneric>(typeof(LazySampleAttributeClass), "Insert");

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "Add");
            Assert.IsTrue(attributeGeneric.Name == "Insert");
        }

        [TestMethod]
        public void TestGetCustomAttributeFromClassProperty()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromClassProperty<LazyAttributeGeneric>(typeof(LazySampleAttributeClass), "Location");

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "Loc");
            Assert.IsTrue(attributeGeneric.Name == "Location");
        }

        [TestMethod]
        public void TestGetCustomAttributeFromEnum()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromEnum<LazyAttributeGeneric>(typeof(LazySampleAttributeEnum));

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "TEnum");
            Assert.IsTrue(attributeGeneric.Name == "Test Enum");
        }

        [TestMethod]
        public void TestGetCustomAttributeFromEnumValue()
        {
            // Arrange

            // Act
            LazyAttributeGeneric attributeGeneric = LazyAttribute.GetCustomAttributeFromEnumValue<LazyAttributeGeneric>(LazySampleAttributeEnum.Option2);

            // Assert
            Assert.IsTrue(attributeGeneric.Code == "Opt2");
            Assert.IsTrue(attributeGeneric.Name == "Option2");
        }
    }
}