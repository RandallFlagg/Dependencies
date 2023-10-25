using System.Collections;
using System.Collections.Generic;

namespace Dependencies.ClrPhlibCS
{
    internal abstract class ApiSetSchema
    {
        internal abstract ApiSetTarget[] Lookup(string name);
    }
}