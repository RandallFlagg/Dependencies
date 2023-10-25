// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// ClrPhlib, Version=1.10.8697.42658, Culture=neutral, PublicKeyToken=null
// Dependencies.ClrPh.ApiSetSchema
using System.Collections.Generic;
namespace Dependencies.ClrPh
{

	public abstract class ApiSetSchema
	{
		public abstract List<KeyValuePair<string, ApiSetTarget>> GetAll();

		public abstract ApiSetTarget Lookup(string name);

		public ApiSetSchema()
		{
		}
	}
}