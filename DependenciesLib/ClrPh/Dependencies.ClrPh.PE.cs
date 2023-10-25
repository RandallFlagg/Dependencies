// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.PE
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
namespace Dependencies.ClrPh
{
	public class PE : IDisposable
	{
		public PeProperties Properties;

		public bool LoadSuccessful;

		public string Filepath;

		private unsafe UnmanagedPE* m_Impl;

		private List<PeImportDll> m_Imports;

		private List<PeExport> m_Exports;

		private bool m_ExportsInit;

		private bool m_ImportsInit;

		public unsafe PE(string Filepath)
		{
			UnmanagedPE* ptr = (UnmanagedPE*)global::_003CModule_003E.@new(96u);
			UnmanagedPE* impl;
			try
			{
				impl = ((ptr != null) ? global::_003CModule_003E.UnmanagedPE_002E_007Bctor_007D(ptr) : null);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			m_Impl = impl;
			this.Filepath = Filepath;
			LoadSuccessful = false;
			m_ExportsInit = false;
			m_ImportsInit = false;
		}

		private unsafe void _007EPE()
		{
			Unload();
			UnmanagedPE* impl = m_Impl;
			if (impl != null)
			{
				void* ptr = global::_003CModule_003E.UnmanagedPE_002E__delDtor(impl, 1u);
			}
			else
			{
				void* ptr = null;
			}
			GC.KeepAlive(this);
		}

		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool Load()
		{
			char* ptr = (char*)((IntPtr)Marshal.StringToHGlobalUni(Filepath)).ToPointer();
			UnmanagedPE* impl = m_Impl;
			LoadSuccessful = global::_003CModule_003E.UnmanagedPE_002ELoadPE(impl, ptr);
			nint hglobal = new IntPtr(ptr);
			Marshal.FreeHGlobal(hglobal);
			if (!LoadSuccessful)
			{
				return false;
			}
			LoadSuccessful &= InitProperties();
			if (!LoadSuccessful)
			{
				global::_003CModule_003E.UnmanagedPE_002EUnloadPE(m_Impl);
				GC.KeepAlive(this);
				return false;
			}
			return LoadSuccessful;
		}

		public unsafe void Unload()
		{
			if (LoadSuccessful)
			{
				global::_003CModule_003E.UnmanagedPE_002EUnloadPE(m_Impl);
			}
			GC.KeepAlive(this);
		}

		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsWow64Dll()
		{
			return (Properties.Machine & 0xFFFF) == 332;
		}

		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsArm32Dll()
		{
			return (Properties.Machine & 0xFFFF) == 452;
		}

		public string GetProcessor()
		{
			if ((Properties.Machine & 0xFFFF) == 332)
			{
				return "x86";
			}
			if ((Properties.Machine & 0xFFFF) == 452)
			{
				return "arm";
			}
			if ((Properties.Machine & 0xFFFF) == 43620)
			{
				return "arm64";
			}
			return "unknown";
		}

		public unsafe ApiSetSchema GetApiSetSchema()
		{
			Unsafe.SkipInit(out _PH_MAPPED_IMAGE pH_MAPPED_IMAGE);
			// IL cpblk instruction
			Unsafe.CopyBlock(ref pH_MAPPED_IMAGE, m_Impl, 28);
			for (uint num = 0u; num < (uint)Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 16)); num++)
			{
				_IMAGE_SECTION_HEADER* ptr = (_IMAGE_SECTION_HEADER*)(int)((uint)Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 20)) + num * 40);
				if (global::_003CModule_003E.strncmp((sbyte*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xd9102de2_002Eunnamed_002Dglobal_002D4), (sbyte*)ptr, 8u) == 0)
				{
					ApiSetSchema result = global::_003CModule_003E.ApiSetSchemaImpl_002EParseApiSetSchema((_API_SET_NAMESPACE*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 4)) + *(int*)((byte*)ptr + 20)));
					GC.KeepAlive(this);
					return result;
				}
			}
			GC.KeepAlive(this);
			return new EmptyApiSetSchema();
		}

		public unsafe List<PeExport> GetExports()
		{
			PeExport peExport = null;
			if (m_ExportsInit)
			{
				return m_Exports;
			}
			m_ExportsInit = true;
			m_Exports = new List<PeExport>();
			if (!LoadSuccessful)
			{
				return m_Exports;
			}
			if (global::_003CModule_003E.PhGetMappedImageExports((_PH_MAPPED_IMAGE_EXPORTS*)((byte*)m_Impl + 28), (_PH_MAPPED_IMAGE*)m_Impl) >= 0)
			{
				for (uint num = 0u; num < (uint)(*(int*)((byte*)m_Impl + 32)); num++)
				{
					peExport = PeExport.FromMapimg(m_Impl, num);
					if (peExport != null)
					{
						m_Exports.Add(peExport);
					}
				}
			}
			GC.KeepAlive(this);
			return m_Exports;
		}

		public unsafe List<PeImportDll> GetImports()
		{
			if (m_ImportsInit)
			{
				return m_Imports;
			}
			m_ImportsInit = true;
			m_Imports = new List<PeImportDll>();
			if (!LoadSuccessful)
			{
				return m_Imports;
			}
			if (global::_003CModule_003E.PhGetMappedImageImports((_PH_MAPPED_IMAGE_IMPORTS*)((byte*)m_Impl + 56), (_PH_MAPPED_IMAGE*)m_Impl) >= 0)
			{
				for (uint num = 0u; num < (uint)(*(int*)((byte*)m_Impl + 64)); num++)
				{
					List<PeImportDll> imports = m_Imports;
					_PH_MAPPED_IMAGE_IMPORTS* ptr = (_PH_MAPPED_IMAGE_IMPORTS*)((byte*)m_Impl + 56);
					PeImportDll item = new PeImportDll(&ptr, num);
					imports.Add(item);
				}
			}
			if (global::_003CModule_003E.PhGetMappedImageDelayImports((_PH_MAPPED_IMAGE_IMPORTS*)((byte*)m_Impl + 72), (_PH_MAPPED_IMAGE*)m_Impl) >= 0)
			{
				for (uint num2 = 0u; num2 < (uint)(*(int*)((byte*)m_Impl + 80)); num2++)
				{
					List<PeImportDll> imports2 = m_Imports;
					_PH_MAPPED_IMAGE_IMPORTS* ptr2 = (_PH_MAPPED_IMAGE_IMPORTS*)((byte*)m_Impl + 72);
					PeImportDll item2 = new PeImportDll(&ptr2, num2);
					imports2.Add(item2);
				}
			}
			GC.KeepAlive(this);
			return m_Imports;
		}

		public unsafe string GetManifest()
		{
			UTF8Encoding uTF8Encoding = null;
			byte[] array = null;
			if (!LoadSuccessful)
			{
				return "";
			}
			Unsafe.SkipInit(out byte* ptr);
			Unsafe.SkipInit(out int num);
			if (!global::_003CModule_003E.UnmanagedPE_002EGetPeManifest(m_Impl, &ptr, &num))
			{
				GC.KeepAlive(this);
				return "";
			}
			uTF8Encoding = new UTF8Encoding();
			array = new byte[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = ptr[i];
			}
			array[num] = 0;
			string @string = uTF8Encoding.GetString(array, 0, num);
			GC.KeepAlive(this);
			return @string;
		}

		private unsafe void _0021PE()
		{
			Unload();
			UnmanagedPE* impl = m_Impl;
			if (impl != null)
			{
				void* ptr = global::_003CModule_003E.UnmanagedPE_002E__delDtor(impl, 1u);
			}
			else
			{
				void* ptr = null;
			}
			GC.KeepAlive(this);
		}

		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe bool InitProperties()
		{
			Unsafe.SkipInit(out _PH_MAPPED_IMAGE pH_MAPPED_IMAGE);
			// IL cpblk instruction
			Unsafe.CopyBlock(ref pH_MAPPED_IMAGE, m_Impl, 28);
			Properties = new PeProperties();
			Properties.Machine = (short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 4));
			Properties.Magic = (short)(*(ushort*)((byte*)m_Impl + 24));
			Properties.Checksum = *(int*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 88);
			int correctChecksum = ((Properties.Checksum == (int)global::_003CModule_003E.PhCheckSumMappedImage(&pH_MAPPED_IMAGE)) ? 1 : 0);
			Properties.CorrectChecksum = (byte)correctChecksum != 0;
			Unsafe.SkipInit(out _LARGE_INTEGER lARGE_INTEGER);
			global::_003CModule_003E.RtlSecondsSince1970ToTime(*(uint*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 8), &lARGE_INTEGER);
			Unsafe.SkipInit(out _SYSTEMTIME sYSTEMTIME);
			global::_003CModule_003E.PhLargeIntegerToLocalSystemTime(&sYSTEMTIME, &lARGE_INTEGER);
			ValueType valueType = default(DateTime);
			(DateTime)valueType = new DateTime(*(ushort*)(&sYSTEMTIME), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 2)), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 6)), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 8)), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 10)), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 12)), Unsafe.As<_SYSTEMTIME, ushort>(ref Unsafe.AddByteOffset(ref sYSTEMTIME, 14)), DateTimeKind.Local);
			Properties.Time = valueType;
			if (Unsafe.As<_PH_MAPPED_IMAGE, ushort>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 24)) == 267)
			{
				_IMAGE_OPTIONAL_HEADER* ptr = (_IMAGE_OPTIONAL_HEADER*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 24);
				Properties.ImageBase = (uint)(*(int*)((byte*)ptr + 28));
				Properties.SizeOfImage = *(int*)((byte*)ptr + 56);
				Properties.EntryPoint = (uint)(*(int*)((byte*)ptr + 16));
			}
			else
			{
				_IMAGE_OPTIONAL_HEADER64* ptr2 = (_IMAGE_OPTIONAL_HEADER64*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 24);
				Properties.ImageBase = Unsafe.ReadUnaligned<long>((byte*)ptr2 + 24);
				Properties.SizeOfImage = *(int*)((byte*)ptr2 + 56);
				Properties.EntryPoint = (uint)(*(int*)((byte*)ptr2 + 16));
			}
			Properties.Subsystem = (short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 92));
			Tuple<short, short> subsystemVersion = new Tuple<short, short>((short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 72)), (short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 74)));
			Properties.SubsystemVersion = subsystemVersion;
			Properties.Characteristics = (short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 22));
			Properties.DllCharacteristics = (short)(*(ushort*)(Unsafe.As<_PH_MAPPED_IMAGE, int>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 12)) + 94));
			Properties.FileSize = Unsafe.As<_PH_MAPPED_IMAGE, uint>(ref Unsafe.AddByteOffset(ref pH_MAPPED_IMAGE, 8));
			return true;
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				_007EPE();
				return;
			}
			try
			{
				_0021PE();
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

		~PE()
		{
			Dispose(A_0: false);
		}
	}
}