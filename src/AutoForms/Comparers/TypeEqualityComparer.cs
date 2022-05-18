using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.Comparers;

internal class TypeEqualityComparer : IEqualityComparer<Type>
{
    public bool Equals(Type x, Type y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.FullName == y.FullName;
    }

    public int GetHashCode(Type obj)
    {
        return obj.FullName != null
            ? obj.FullName.GetHashCode()
            : 0;
    }
}
