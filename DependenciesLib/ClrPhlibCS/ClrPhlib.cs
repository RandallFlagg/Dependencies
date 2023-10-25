using System;
using System.Collections.Generic;

namespace Dependencies.ClrPhlibCS
{
    public enum CLRPH_ARCH
    {
        x86,
        x64,
        WOW64
    };

    public static class Phlib
    {
        private static bool bInitializedPhLib = false;
        public static bool InitializePhLib()
        {
            //throw new NotImplementedException();
            //if (!bInitializedPhLib)
            //{
            //    bInitializedPhLib = NT_SUCCESS(PhInitializePhLib());
            //}

            //KnownDll64List = BuildKnownDllList(false);
            //KnownDll32List = BuildKnownDllList(true);

            //return bInitializedPhLib;
            return false;
        }

        //public static CLRPH_ARCH GetClrPhArch()
        //{
        //    if (Environment.Is64BitProcess)
        //    {
        //        return CLRPH_ARCH.x64;
        //    }
        //    else if (Environment.Is64BitOperatingSystem)
        //    {
        //        return CLRPH_ARCH.WOW64;
        //    }
        //    else return CLRPH_ARCH.x86;
        //}
        internal static ApiSetSchema GetApiSetSchema()
        {
            throw new NotImplementedException();
        }

        internal static List<string> GetKnownDlls(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
