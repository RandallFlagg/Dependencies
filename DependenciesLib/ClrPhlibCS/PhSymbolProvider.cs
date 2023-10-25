using System;

namespace Dependencies.Test
{
    public class PhSymbolProvider
    {
        public virtual Tuple<CLRPH_DEMANGLER, string> UndecorateName(string DecoratedName)
        {
            throw new NotImplementedException();
        }

        public string UndecorateNameDemumble(string decoratedName)
        {
            throw new NotImplementedException();
        }

        public string UndecorateNameLLVMItanium(string decoratedName)
        {
            throw new NotImplementedException();
        }

        public string UndecorateNameLLVMMicrosoft(string decoratedName)
        {
            throw new NotImplementedException();
        }

        public string UndecorateNamePh(string decoratedName)
        {
            //return UndecorateNamePrv(DecoratedName, UndecorateSymbolDemangleName);
            throw new NotImplementedException();
        }
    }
}