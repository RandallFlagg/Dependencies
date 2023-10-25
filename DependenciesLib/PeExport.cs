namespace Dependencies
{
    public class PeExport
    {
        public int Ordinal { get; internal set; }
        public object ForwardedName { get; internal set; }
        public string Name { get; internal set; }
        public bool ExportByOrdinal { get; internal set; }
    }
}