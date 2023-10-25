using System;
using System.Collections.Generic;

namespace Dependencies.ClrPhlibCS
{
    // C# visible class representing a parsed PE file
    public class PE //TODO: What is ref class?
    {
        public PE(string destFilePath)
        {
            DestFilePath = destFilePath;
        }

        public string Filepath { get; internal set; }
        public string DestFilePath { get; }
        public bool LoadSuccessful { get; internal set; }

        //public:
        //        PE(_In_ String^ Filepath);
        //~PE();

        //// Mapped the PE in memory and init infos
        //public bool Load()
        //{
        //    throw new Exception("Not implemented");
        //}

        //// Unmapped the PE from memory
        //void Unload();

        //// Check if the PE is 32-bit
        //bool IsWow64Dll();

        //// Check if the PE is 32-bit
        //bool IsArm32Dll();

        //// return the processorArchiture of PE
        //String^ GetProcessor();

        //// Return the ApiSetSchema
        //ApiSetSchema^ GetApiSetSchema();

        //// Return the list of functions exported by the PE
        //List<PeExport ^>^ GetExports();

        //// Return the list of functions imported by the PE, bundled by Dll name
        //List<PeImportDll ^>^ GetImports();

        //// Retrieve the manifest embedded within the PE
        //// Return an empty string if there is none.
        //String^ GetManifest();

        //// PE properties parsed from the NT header
        //PeProperties ^Properties;

        //        // Check if the specified file has been successfully parsed as a PE file.
        //        Boolean LoadSuccessful;

        //// Path to PE file.
        //String^ Filepath;

        //    protected:
        //        // Deallocate the native object on the finalizer just in case no destructor is called  
        //        !PE();

        //// Initalize PeProperties struct once the PE has been loaded into memory
        //bool InitProperties();

        //private:

        //        // C++ part interfacing with phlib
        //        UnmanagedPE* m_Impl;

        //// local cache for imports and exports list
        //List<PeImportDll ^>^ m_Imports;
        //        List<PeExport ^>^  m_Exports;
        //        bool m_ExportsInit;
        //bool m_ImportsInit;
        public List<PeExport> GetExports()
        {
            throw new NotImplementedException();
        }

        internal string GetManifest()
        {
            throw new NotImplementedException();
        }

        internal string GetProcessor()
        {
            throw new NotImplementedException();
        }

        internal bool IsArm32Dll()
        {
            throw new NotImplementedException();
        }

        internal bool IsWow64Dll()
        {
            throw new NotImplementedException();
        }

        public bool Load()
        {
            throw new NotImplementedException();
        }

        internal void Unload()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PeImportDll> GetImports()
        {
            throw new NotImplementedException();
        }
    }
}