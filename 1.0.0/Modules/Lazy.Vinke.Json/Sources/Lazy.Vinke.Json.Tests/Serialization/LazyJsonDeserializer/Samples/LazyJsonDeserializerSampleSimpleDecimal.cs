// LazyJsonDeserializerSampleSimpleDecimal.cs
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
    public class LazyJsonDeserializerSampleSimpleDecimal
    {
        public float FloatValue { get; set; }

        public float? FloatValueNullableNull { get; set; }

        public float? FloatValueNullableNotNull { get; set; }

        public Double DoubleValue { get; set; }

        public Double? DoubleValueNullableNull { get; set; }

        public Double? DoubleValueNullableNotNull { get; set; }

        public Decimal DecimalValue { get; set; }

        public Decimal? DecimalValueNullableNull { get; set; }

        public Decimal? DecimalValueNullableNotNull { get; set; }
    }
}