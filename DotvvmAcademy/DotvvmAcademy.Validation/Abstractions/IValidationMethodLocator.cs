﻿using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.Abstractions
{
    public interface IValidationMethodLocator
    {
        IEnumerable<MethodInfo> GetMethods(Assembly assembly);

        IEnumerable<MethodInfo> GetMethods<TDocument>(Assembly assembly)
            where TDocument : IDocument;
    }
}