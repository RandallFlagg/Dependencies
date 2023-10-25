namespace Dependencies.Test
{
    public enum CLRPH_DEMANGLER
    {
        None,
        Demumble,
        LLVMItanium,
        LLVMMicrosoft,
        Microsoft,
        Default         // Synthetic demangler using all the previous ones
    }
}