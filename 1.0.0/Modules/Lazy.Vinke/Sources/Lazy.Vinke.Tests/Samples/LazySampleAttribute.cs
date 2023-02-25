// LazySampleAttribute.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, February 24

using System;

using Lazy.Vinke;

namespace Lazy.Vinke.Tests
{
    [LazyAttributeGeneric("TEnum", "Test Enum")]
    public enum LazySampleAttributeEnum
    {
        [LazyAttributeGeneric("Opt1", "Option1")]
        Option1 = 1,

        [LazyAttributeGeneric("Opt2", "Option2")]
        Option2 = 2
    }

    [LazyAttributeGeneric("TClass", "Test Class")]
    public class LazySampleAttributeClass
    {
        [LazyAttributeGeneric("Id", "Identity")]
        public Int32 identity;

        [LazyAttributeGeneric("Add", "Insert")]
        private void Insert()
        {
        }

        [LazyAttributeGeneric("Loc", "Location")]
        public String Location { get; set; }
    }
}