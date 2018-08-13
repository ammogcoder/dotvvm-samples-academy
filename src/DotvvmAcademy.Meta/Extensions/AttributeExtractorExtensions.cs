﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace DotvvmAcademy.Meta
{
    public static class AttributeExtractorExtensions
    {
        public static ImmutableArray<Attribute> Extract(this IAttributeExtractor extractor, INamedTypeSymbol attributeClass, ISymbol symbol)
        {
            return symbol.GetAttributes()
                .Where(a => a.AttributeClass.Equals(attributeClass))
                .Select(extractor.Extract)
                .ToImmutableArray();
        }

        public static ImmutableArray<TAttribute> Extract<TAttribute>(
            this IAttributeExtractor extractor,
            INamedTypeSymbol attributeClass,
            ISymbol symbol)
            where TAttribute : Attribute
        {
            return symbol.GetAttributes()
                .Where(a => a.AttributeClass.Equals(attributeClass))
                .Select(extractor.Extract)
                .OfType<TAttribute>()
                .ToImmutableArray();
        }
    }
}