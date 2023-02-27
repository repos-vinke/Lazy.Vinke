// TestLazyDatabase.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 27

using System;
using System.Text;

using Lazy.Vinke;
using Lazy.Vinke.Database;

namespace Lazy.Vinke.Database.Tests
{
    public abstract class TestLazyDatabase
    {
        public abstract void TestQueryExecute();
    }
}