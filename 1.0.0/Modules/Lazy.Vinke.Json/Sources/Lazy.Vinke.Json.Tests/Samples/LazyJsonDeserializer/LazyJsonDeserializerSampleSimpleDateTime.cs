// LazyJsonDeserializerSampleSimpleDateTime.cs
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
    public class LazyJsonDeserializerSampleSimpleDateTime
    {
        public DateTime DateTimeValueInvalid { get; set; }

        public DateTime DateTimeValueValid { get; set; }

        public DateTime DateTimeValueNotNullableNull { get; set; }

        public DateTime? DateTimeValueNullableNull { get; set; }
    }
}