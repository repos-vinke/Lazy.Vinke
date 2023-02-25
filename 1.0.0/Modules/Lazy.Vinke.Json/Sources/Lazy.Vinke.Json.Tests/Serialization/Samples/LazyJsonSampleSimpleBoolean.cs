// LazyJsonSampleSimpleBoolean.cs
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
    public class LazyJsonSampleSimpleBoolean
    {
        public Boolean BooleanValue { get; set; }

        public Boolean? BooleanValueNullableNull { get; set; }

        public Boolean? BooleanValueNullableNotNull { get; set; }
    }
}