﻿using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public class ConstraintEqualityComparer : IEqualityComparer<IConstraint>
    {
        public bool Equals(IConstraint x, IConstraint y)
        {
            return x.CanOverwrite(y);
        }

        public int GetHashCode(IConstraint obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}