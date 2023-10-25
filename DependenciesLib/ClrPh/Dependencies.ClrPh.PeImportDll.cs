// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.PeImportDll
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Dependencies.ClrPh
{

	public class PeImportDll : IDisposable
	{
		public long Flags;

		public string Name;

		public long NumberOfEntries;

		public List<PeImport> ImportList;

		private unsafe _PH_MAPPED_IMAGE_IMPORT_DLL* ImportDll = (_PH_MAPPED_IMAGE_IMPORT_DLL*)global::_003CModule_003E.@new(24u);

		public unsafe PeImportDll(PeImportDll other)
		{
			ImportList = new List<PeImport>();
			global::_003CModule_003E.memcpy(ImportDll, other.ImportDll, 24u);
			Flags = other.Flags;
			Name = other.Name;
			NumberOfEntries = other.NumberOfEntries;
			for (uint num = 0u; num < (uint)NumberOfEntries; num++)
			{
				List<PeImport> importList = ImportList;
				PeImport item = new PeImport(other.ImportList[(int)num]);
				importList.Add(item);
			}
			GC.KeepAlive(this);
		}

		public unsafe PeImportDll(_PH_MAPPED_IMAGE_IMPORTS** PvMappedImports, uint ImportDllIndex)
		{
			ImportList = new List<PeImport>();
			if (global::_003CModule_003E.PhGetMappedImageImportDll((_PH_MAPPED_IMAGE_IMPORTS*)(int)(*(uint*)PvMappedImports), ImportDllIndex, ImportDll) < 0)
			{
				Flags = 0L;
				Name = "## PeImportDll error: Invalid DllName ##";
				NumberOfEntries = 0L;
				return;
			}
			Flags = (uint)(*(int*)((byte*)ImportDll + 4));
			Name = new string((sbyte*)(int)(*(uint*)((byte*)ImportDll + 8)));
			NumberOfEntries = (uint)(*(int*)((byte*)ImportDll + 12));
			for (uint num = 0u; num < (uint)NumberOfEntries; num++)
			{
				List<PeImport> importList = ImportList;
				PeImport item = new PeImport(ImportDll, num);
				importList.Add(item);
			}
			GC.KeepAlive(this);
		}

		private unsafe void _007EPeImportDll()
		{
			global::_003CModule_003E.delete(ImportDll);
			GC.KeepAlive(this);
		}

		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsDelayLoad()
		{
			return (byte)(Flags & 1) != 0;
		}

		private unsafe void _0021PeImportDll()
		{
			global::_003CModule_003E.delete(ImportDll);
			GC.KeepAlive(this);
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				_007EPeImportDll();
				return;
			}
			try
			{
				_0021PeImportDll();
			}
			finally
			{
				base.Finalize();
			}
		}

		public virtual sealed void Dispose()
		{
			Dispose(A_0: true);
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		~PeImportDll()
		{
			Dispose(A_0: false);
		}
	}
}