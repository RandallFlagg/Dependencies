// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.PeExport
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace Dependencies.ClrPh
{

	public class PeExport : IDisposable
	{
		public ushort Ordinal;

		public string Name;

		public bool ExportByOrdinal;

		public long VirtualAddress;

		public string ForwardedName;

		public PeExport(PeExport other)
		{
			Ordinal = Ordinal;
			ExportByOrdinal = ExportByOrdinal;
			Name = other.Name;
			ForwardedName = other.ForwardedName;
			VirtualAddress = other.VirtualAddress;
		}

		public PeExport()
		{
		}

		private void _007EPeExport()
		{
		}

		public unsafe static PeExport FromMapimg(UnmanagedPE* refPe, uint Index)
		{
			PeExport peExport = null;
			peExport = null;
			Unsafe.SkipInit(out _PH_MAPPED_IMAGE_EXPORT_ENTRY pH_MAPPED_IMAGE_EXPORT_ENTRY);
			Unsafe.SkipInit(out _PH_MAPPED_IMAGE_EXPORT_FUNCTION pH_MAPPED_IMAGE_EXPORT_FUNCTION);
			if (global::_003CModule_003E.PhGetMappedImageExportEntry((_PH_MAPPED_IMAGE_EXPORTS*)((byte*)refPe + 28), Index, &pH_MAPPED_IMAGE_EXPORT_ENTRY) >= 0 && global::_003CModule_003E.PhGetMappedImageExportFunction((_PH_MAPPED_IMAGE_EXPORTS*)((byte*)refPe + 28), null, *(ushort*)(&pH_MAPPED_IMAGE_EXPORT_ENTRY), &pH_MAPPED_IMAGE_EXPORT_FUNCTION) >= 0)
			{
				peExport = new PeExport();
				peExport.Ordinal = *(ushort*)(&pH_MAPPED_IMAGE_EXPORT_ENTRY);
				int exportByOrdinal = ((Unsafe.As<_PH_MAPPED_IMAGE_EXPORT_ENTRY, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_EXPORT_ENTRY, 4)) == 0) ? 1 : 0);
				peExport.ExportByOrdinal = (byte)exportByOrdinal != 0;
				peExport.Name = new string((sbyte*)(int)Unsafe.As<_PH_MAPPED_IMAGE_EXPORT_ENTRY, uint>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_EXPORT_ENTRY, 4)));
				peExport.ForwardedName = new string((sbyte*)(int)Unsafe.As<_PH_MAPPED_IMAGE_EXPORT_FUNCTION, uint>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_EXPORT_FUNCTION, 4)));
				if (Unsafe.As<_PH_MAPPED_IMAGE_EXPORT_ENTRY, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE_EXPORT_ENTRY, 4)) == 0)
				{
					peExport.VirtualAddress = *(int*)(&pH_MAPPED_IMAGE_EXPORT_FUNCTION);
				}
				peExport.VirtualAddress = *(int*)(&pH_MAPPED_IMAGE_EXPORT_FUNCTION);
			}
			return peExport;
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				_007EPeExport();
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