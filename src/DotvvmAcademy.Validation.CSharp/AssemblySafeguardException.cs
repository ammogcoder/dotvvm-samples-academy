﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp
{
    public class AssemblySafeguardException : Exception
    {
        public AssemblySafeguardException(string message) : base(message)
        {
        }
    }
}
