// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.PeImport
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace Dependencies.ClrPh
{

	public class PeImport : IDisposable
	{
		public ushort Hint;

		public ushort Ordinal;

		public string Name;

		public string ModuleName;

		public bool ImportByOrdinal;

		public bool DelayImport;

		public PeImport(PeImport other)
		{
			Hint = other.Hint;
			Ordinal = other.Ordinal;
			DelayImport = other.DelayImport;
			Name = other.Name;
			ModuleName = other.ModuleName;
			ImportByOrdinal = other.ImportByOrdinal;
		}

		public unsafe PeImport(_PH_MAPPED_IMAGE_IMPORT_DLL* importDll, uint Index)
		{
			Unsafe.SkipInit(out _PH_MAPPED_IMAGE_IMPORT_ENTRY pH_MAPPED_IMAGE_IMPORT_ENTRY);
			if (global::_003CModule_003E.PhGetMappedImageImportEntry(importDll, Index, &pH_MAPPED_IMAGE_IMPORT_ENTRY) >= 0)
			{
				Hint = Unsafe.As<_PH_MAPPED_IMAGE_IMPORT_ENTRY, ushort>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_IMPORT_ENTRY, 4));
				Ordinal = Unsafe.As<_PH_MAPPED_IMAGE_IMPORT_ENTRY, ushort>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_IMPORT_ENTRY, 4));
				DelayImport = (byte)((uint)(*(int*)((byte*)importDll + 4)) & (true ? 1u : 0u)) != 0;
				Name = new string((sbyte*)(int)(*(uint*)(&pH_MAPPED_IMAGE_IMPORT_ENTRY)));
				ModuleName = new string((sbyte*)(int)(*(uint*)((byte*)importDll + 8)));
				ImportByOrdinal = ((*(int*)(&pH_MAPPED_IMAGE_IMPORT_ENTRY) == 0) ? true : false);
			}
		}

		private void _007EPeImport()
		{
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				_007EPeImport();
			}
			else
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
	}
}