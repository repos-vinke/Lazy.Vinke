// LazyJsonDeserializerSampleSimpleAttribute.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Json.Tests
{
    public class LazyJsonDeserializerSampleSimpleAttribute
    {
        public Boolean BooleanValue { get; set; }

        public Int16 Int16Value { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public String StringValue { get; set; }

        [LazyJsonAttributePropertyRename("DecimalValueRenamed")]
        public Decimal DecimalValue { get; set; }

        [LazyJsonAttributePropertyRename("DecimalValueReadOnlyRenamed")]
        public Decimal DecimalValueReadOnly { get; }
    }
}