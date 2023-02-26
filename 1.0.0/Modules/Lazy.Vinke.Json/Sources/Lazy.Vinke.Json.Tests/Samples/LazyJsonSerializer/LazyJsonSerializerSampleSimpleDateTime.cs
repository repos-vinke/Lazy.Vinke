// LazyJsonSerializerSampleSimpleDateTime.cs
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
    public class LazyJsonSerializerSampleSimpleDateTime1
    {
        public DateTime DateTimeValueNormal { get; set; }

        public DateTime? DateTimeValueNormalNullableNotNull { get; set; }

        public DateTime? DateTimeValueNormalNullableNull { get; set; }
    }

    public class LazyJsonSerializerSampleSimpleDateTime2
    {
        public DateTime DateTimeValueFormatted { get; set; }

        public DateTime? DateTimeValueFormattedNullableNotNull { get; set; }

        public DateTime? DateTimeValueFormattedNullableNull { get; set; }
    }
}