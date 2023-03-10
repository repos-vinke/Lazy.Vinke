// LazyJsonDeserializerSampleSimpleString.cs
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
    public class LazyJsonDeserializerSampleSimpleString
    {
        public Char CharValue { get; set; }

        public Char CharValueNull { get; set; }

        public Char? CharValueNullableNull { get; set; }

        public Char? CharValueNullableNotNull { get; set; }

        public String StringValue { get; set; }

        public String StringValueNullableNull { get; set; }

        public String StringValueWhiteSpace { get; set; }

        public String StringValueEmpty { get; set; }
    }
}