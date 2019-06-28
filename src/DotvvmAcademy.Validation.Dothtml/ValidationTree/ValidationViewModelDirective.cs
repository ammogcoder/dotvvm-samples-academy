﻿using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser.Binding.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using System.Diagnostics;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("ViewModelDirective: {Type,nq}")]
    public class ValidationViewModelDirective : ValidationTypeDirective, IAbstractViewModelDirective
    {
        public ValidationViewModelDirective(
            DothtmlDirectiveNode node,
            BindingParserNode typeSyntax,
            ValidationTypeDescriptor type)
            : base(node, typeSyntax, type)
        {
        }
    }
}