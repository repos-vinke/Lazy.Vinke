// LazyJsonSerializerSampleSimpleInteger.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 23

using System;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    public class LazyJsonSerializerSampleSimpleInteger
    {
        public Int16 Int16Value { get; set; }

        public Int16? Int16ValueNullableNull { get; set; }

        public Int16? Int16ValueNullableNotNull { get; set; }

        public Int32 Int32Value { get; set; }

        public Int32? Int32ValueNullableNull { get; set; }

        public Int32? Int32ValueNullableNotNull { get; set; }

        public Int64 Int64Value { get; set; }

        public Int64? Int64ValueNullableNull { get; set; }

        public Int64? Int64ValueNullableNotNull { get; set; }
    }
}