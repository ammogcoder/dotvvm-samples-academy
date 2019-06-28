﻿using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("PropertyTemplate: {Property.FullName,nq}, Count = {Content.Length,nq}")]
    public class ValidationPropertyTemplate : ValidationPropertySetter, IAbstractPropertyTemplate
    {
        public ValidationPropertyTemplate(
            DothtmlNode node,
            IPropertyDescriptor property,
            ImmutableArray<ValidationControl> content)
            : base(node, property)
        {
            Content = content;
            foreach (var control in Content)
            {
                control.Parent = this;
            }
        }

        public ImmutableArray<ValidationControl> Content { get; }

        IEnumerable<IAbstractControl> IAbstractPropertyTemplate.Content => Content;
    }
}