// LazyConvertTests.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 22

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lazy.Vinke;

namespace Lazy.Vinke.Tests
{
    [TestClass]
    public class LazyConvertTests
    {
        [TestMethod]
        public void TestConvertToIntegerNullAsZero()
        {
            // Arrange

            // Act
            Int16 res16 = LazyConvert.ToInt16(null, -1, true);
            Int32 res32 = LazyConvert.ToInt32(null, -1, true);
            Int64 res64 = LazyConvert.ToInt64(null, -1, true);

            // Assert
            Assert.IsTrue(res16 == 0);
            Assert.IsTrue(res32 == 0);
            Assert.IsTrue(res64 == 0);
        }

        [TestMethod]
        public void TestConvertToIntegerNullNotZero()
        {
            // Arrange

            // Act
            Int16 res16 = LazyConvert.ToInt16(null, -1, false);
            Int32 res32 = LazyConvert.ToInt32(null, -1, false);
            Int64 res64 = LazyConvert.ToInt64(null, -1, false);

            // Assert
            Assert.IsTrue(res16 == -1);
            Assert.IsTrue(res32 == -1);
            Assert.IsTrue(res64 == -1);
        }

        [TestMethod]
        public void TestConvertToFloatingNullAsZero()
        {
            // Arrange

            // Act
            Double resFloat = LazyConvert.ToFloat(null, -1, true);
            Double resDouble = LazyConvert.ToDouble(null, -1, true);
            Decimal resDecimal = LazyConvert.ToDecimal(null, -1, true);

            // Assert
            Assert.IsTrue(resFloat == 0.0f);
            Assert.IsTrue(resDouble == 0.0d);
            Assert.IsTrue(resDecimal == 0.0m);
        }

        [TestMethod]
        public void TestConvertToFloatingNullNotZero()
        {
            // Arrange

            // Act
            Double resFloat = LazyConvert.ToFloat(null, -1, false);
            Double resDouble = LazyConvert.ToDouble(null, -1, false);
            Decimal resDecimal = LazyConvert.ToDecimal(null, -1, false);

            // Assert
            Assert.IsTrue(resFloat == -1);
            Assert.IsTrue(resDouble == -1);
            Assert.IsTrue(resDecimal == -1);
        }
    }
}