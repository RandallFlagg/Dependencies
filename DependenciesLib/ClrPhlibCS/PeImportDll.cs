using System.Collections.Generic;

namespace Dependencies.ClrPhlibCS
{
    public class PeImportDll
    {
        public IEnumerable<PeImport> ImportList { get; internal set; }
        public int Name { get; set; }
    }
}