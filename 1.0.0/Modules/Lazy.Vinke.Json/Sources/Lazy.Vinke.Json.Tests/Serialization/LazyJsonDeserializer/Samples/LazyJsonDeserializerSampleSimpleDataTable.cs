// LazyJsonDeserializerSampleSimpleDataTable.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    public class LazyJsonDeserializerSampleSimpleDataTable
    {
        public DataTable DataTableWithoutRows { get; set; }

        public DataTable DataTableWithSingleRow { get; set; }

        public DataTable DataTableWithMultipleRows { get; set; }
    }
}