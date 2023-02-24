// LazyJsonDeserializerSampleSimpleDictionary.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    public class LazyJsonDeserializerSampleSimpleDictionary
    {
        public Dictionary<Int16, String> DicInt16StringInvalidSingle { get; set; }

        public Dictionary<Int16, String> DicInt16StringInvalidMultiple { get; set; }

        public Dictionary<Int16, String> DicInt16StringValidSingle { get; set; }

        public Dictionary<Int16, String> DicInt16StringValidMultiple { get; set; }

        public Dictionary<Int16, String> DicInt16StringEmpty { get; set; }

        public Dictionary<Int16, String> DicInt16StringNull { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalInvalidSingle { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalInvalidMultiple { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalValidSingle { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalValidMultiple { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalEmpty { get; set; }

        public Dictionary<String, Decimal> DicStringDecimalNull { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16InvalidSingle { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16InvalidMultiple { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16ValidSingle { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16ValidMultiple { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16Empty { get; set; }

        public Dictionary<Int64, Int16> DicInt64Int16Null { get; set; }
    }
}