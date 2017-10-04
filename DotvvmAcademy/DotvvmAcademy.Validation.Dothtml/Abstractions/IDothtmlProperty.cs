﻿using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Dothtml.Abstractions
{
    /// <summary>
    /// A dothtml property.
    /// </summary>
    public interface IDothtmlProperty
    {
        IDothtmlBinding Binding();

        void HardcodedValue(IEnumerable<object> allowedValues);

        IDothtmlControlCollection Template();
    }
}