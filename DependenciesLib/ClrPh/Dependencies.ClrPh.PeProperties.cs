// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.PeProperties
using System;
namespace Dependencies.ClrPh
{
	public class PeProperties
	{
		public short Machine;

		public ValueType Time;

		public short Magic;

		public long ImageBase;

		public int SizeOfImage;

		public long EntryPoint;

		public int Checksum;

		public bool CorrectChecksum;

		public short Subsystem;

		public Tuple<short, short> SubsystemVersion;

		public short Characteristics;

		public short DllCharacteristics;

		public ulong FileSize;
	}
}