using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Dargon.Plugins.InibinTroybin
{
	public partial class InibinTroybinFile
	{
		private byte m_version;
		private ushort m_oldLength;
		private ushort m_format;

		private Dictionary<InibinTroybinDataSection, List<InibinProperty>> m_propertyLists;

		private Dictionary<uint, InibinProperty> m_properties;
		/// <summary>
		/// A dictionary of all the key-value pairs contained within the inibin file. 
		/// <para> A good majority of the keys have been identified and can be referenced with the CharacterInibinKeyHashes enum</para>
		/// <para> Ex: Properties[(uint)CharacterInibinKeyHashes.SkinOneName)]</para>
		/// </summary>
		public ReadOnlyDictionary<uint, InibinProperty> Properties { get; private set; }

		/// <summary>
		/// Class to read and write inibin/trobyin files. 
		/// <para> The file is just a list of key value pairs.</para>
		/// <para> The keys are uint hashes and the values can be anything from byte to Vector4&lt;float&gt;</para>
		/// </summary>
		/// <param name="filePath">The path to the inibin/troybin</param>
		public InibinTroybinFile(string filePath)
		{
			using (FileStream stream = new FileStream(filePath, FileMode.Open))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					ReadInibin(reader);
				}
			}
		}

		/// <summary>
		/// Class to read and write inibin/trobyin files. 
		/// <para> The file is just a list of key value pairs.</para>
		/// <para> The keys are uint hashes and the values can be anything from byte to Vector4&lt;float&gt;</para>
		/// </summary>
		/// <param name="fileContents">The byte contents of the inibin/troybin file</param>
		public InibinTroybinFile(byte[] fileContents)
		{
			using (MemoryStream stream = new MemoryStream(fileContents))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					ReadInibin(reader);
				}
			}
		}

		/// <summary>
		/// Factory method for creating an InibinTroybinFile object. It's exactly the same as using the constructor
		/// </summary>
		/// <param name="filePath">The path to the inibin/troybin</param>
		/// <returns></returns>
		public static InibinTroybinFile CreateInibin(string filePath)
		{
			return new InibinTroybinFile(filePath);
		}

		/// <summary>
		/// Factory method for creating an InibinTroybinFile object. It's exactly the same as using the constructor
		/// </summary>
		/// <param name="fileContents">The byte contents of the inibin/troybin file</param>
		/// <returns></returns>
		public static InibinTroybinFile CreateInibin(byte[] fileContents)
		{
			return new InibinTroybinFile(fileContents);
		}

	}
}
