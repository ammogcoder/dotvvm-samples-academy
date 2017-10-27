﻿namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    /// <summary>
    /// A C# indexer.
    /// </summary>
    public interface ICSharpIndexer : ICSharpAllowsAccessModifier, ICSharpAllowsAbstractModifier, ICSharpAllowsVirtualModifier
    {
        void ReturnType(CSharpTypeDescriptor returnType);

        ICSharpAccessor Getter();

        ICSharpAccessor Setter();
    }
}