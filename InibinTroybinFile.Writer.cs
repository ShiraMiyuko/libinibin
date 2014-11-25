using System;
using System.Collections.Generic;
using System.IO;
using ItzWarty;

namespace Dargon.Plugins.InibinTroybin
{
	public partial class InibinTroybinFile
	{
		/// <summary>
		/// Writes the inibin/trobyin to a byte[]
		/// </summary>
		/// <returns>A byte[] containing the contents of the inibin/troybin file</returns>
		public byte[] Write()
		{
			byte[] returnBytes;
			using(MemoryStream stream = new MemoryStream())
			{
				using(BinaryWriter writer = new BinaryWriter(stream))
				{
					WriteInibinHelper(writer);
					returnBytes = stream.GetBuffer();
				}
			}

			return returnBytes;
		}

		/// <summary>
		/// Writes the inibin/troybin to a file
		/// </summary>
		/// <param name="filePath">Path to the file to be written. The file will be created and any previous data overwritten</param>
		public void Write(string filePath)
		{
			using (FileStream stream = new FileStream(filePath, FileMode.Create))
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					WriteInibinHelper(writer);
				}
			}
		}

		/// <summary>
		/// Helper method used to actuall write the data
		/// </summary>
		/// <param name="writer">The BinaryWriter to use. AKA, where you want the data to be written to</param>
		private void WriteInibinHelper(BinaryWriter writer)
		{
			// Write the header
			writer.Write(m_version);

			// Store a dummy for the fileLength. We'll edit it at the end
			writer.Write((ushort)0); // fileLength - oldLen == offset to string section
			writer.Write(m_format);


			// Section 0. uint values
			if (m_propertyLists[InibinTroybinDataSection.UInt] != null)
				WriteValues(writer, InibinTroybinDataSection.UInt);

			// Section 1. Float values
			if (m_propertyLists[InibinTroybinDataSection.Float] != null)
				WriteValues(writer, InibinTroybinDataSection.Float);

			// Section 2. Small floats that are written as bytes
			if (m_propertyLists[InibinTroybinDataSection.SmallFloats] != null)
				WriteValues(writer, InibinTroybinDataSection.SmallFloats);

			// Section 3. ushort values
			if (m_propertyLists[InibinTroybinDataSection.UShort] != null)
				WriteValues(writer, InibinTroybinDataSection.UShort);

			// Section 4. Byte values
			if (m_propertyLists[InibinTroybinDataSection.Byte] != null)
				WriteValues(writer, InibinTroybinDataSection.Byte);

			// Section 5. Bool values. Each is stored in a single bit
			if (m_propertyLists[InibinTroybinDataSection.Bool] != null)
				WriteBool(writer);

			// Section 6. Vector3<byte> values
			if (m_propertyLists[InibinTroybinDataSection.Vector3Byte] != null)
				WriteValues(writer, InibinTroybinDataSection.Vector3Byte);

			// Section 7. Vector3<float> values
			if (m_propertyLists[InibinTroybinDataSection.Vector3Float] != null)
				WriteValues(writer, InibinTroybinDataSection.Vector3Float);

			// Section 8. Vector2<byte>
			if (m_propertyLists[InibinTroybinDataSection.Vector2Byte] != null)
				WriteValues(writer, InibinTroybinDataSection.Vector2Byte);

			// Section 9. Vector2<float>
			if (m_propertyLists[InibinTroybinDataSection.Vector2Float] != null)
				WriteValues(writer, InibinTroybinDataSection.Vector2Float);

			// Section 10. Vector4<byte> values
			if (m_propertyLists[InibinTroybinDataSection.Vector4Byte] != null)
				WriteValues(writer, InibinTroybinDataSection.Byte);

			// Section 11. Vector4<float>
			if (m_propertyLists[InibinTroybinDataSection.Vector4Float] != null)
				WriteValues(writer, InibinTroybinDataSection.Vector4Float);

			// Reset the length since we have to calculate it
			m_oldLength = 0;

			// Offsets to the strings. These are all going to be empty and filled later.
			if (m_properties[12] != null)
			{
				// Write the placeholders for the string offsets
				WriteValues(writer, InibinTroybinDataSection.String);

				// Write the actual strings. Record duplications or inner string duplications
				WriteStrings(writer);
			}

			// Re-write the string offset
			m_oldLength = (UInt16)(writer.BaseStream.Length - m_oldLength);
			writer.BaseStream.Seek(1, SeekOrigin.Begin);
			writer.Write(m_oldLength);
		}

		/// <summary>
		/// Writes the keys and then the values for a specific data section
		/// </summary>
		/// <param name="writer">The BinaryWriter to use. AKA, where you want the data to be written to</param>
		/// <param name="section">The data type of the values to be written</param>
		private void WriteValues(BinaryWriter writer, InibinTroybinDataSection section)
		{
			ushort entryCount = (ushort)m_propertyLists[section].Count;

			// The first part of a section is how many entries are in the section
			writer.Write(entryCount);
			// Next are the keys for the section
			for (int i = 0; i < entryCount; i++)
			{
				writer.Write(m_propertyLists[section][i].Key);
			}

			// Lastly are the values
			for (int i = 0; i < entryCount; i++)
			{
				if (section == InibinTroybinDataSection.Byte)
				{
					writer.Write((byte)m_propertyLists[section][i].Value);
				}
				else if (section == InibinTroybinDataSection.UShort)
				{
					writer.Write((ushort)m_propertyLists[section][i].Value);
				}
				else if (section == InibinTroybinDataSection.UInt)
				{
					writer.Write((uint)m_propertyLists[section][i].Value);
				}
				else if (section == InibinTroybinDataSection.Float)
				{
					writer.Write((float)m_propertyLists[section][i].Value);
				}
				else if (section == InibinTroybinDataSection.SmallFloats)
				{
					float value = 10.0f * (float)m_propertyLists[section][i].Value;
					writer.Write(Convert.ToByte(value));
				}
				else if (section == InibinTroybinDataSection.Vector2Byte)
				{
					Vector2<byte> value = (Vector2<byte>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
				}
				else if (section == InibinTroybinDataSection.Vector2Float)
				{
					Vector2<float> value = (Vector2<float>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
				}
				else if (section == InibinTroybinDataSection.Vector3Byte)
				{
					Vector3<byte> value = (Vector3<byte>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
					writer.Write(value.Z);
				}
				else if (section == InibinTroybinDataSection.Vector3Float)
				{
					Vector3<float> value = (Vector3<float>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
					writer.Write(value.Z);
				}
				else if (section == InibinTroybinDataSection.Vector4Byte)
				{
					Vector4<byte> value = (Vector4<byte>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
					writer.Write(value.Z);
					writer.Write(value.W);
				}
				else if (section == InibinTroybinDataSection.Vector4Float)
				{
					Vector4<float> value = (Vector4<float>)m_propertyLists[section][i].Value;
					writer.Write(value.X);
					writer.Write(value.Y);
					writer.Write(value.Z);
					writer.Write(value.W);
				}
				else if (section == InibinTroybinDataSection.String)
				{
					// Placeholders
					writer.Write(0U);
				}
			}
		}

		/// <summary>
		/// Similar to WriteValues. Strings require offsets and a data section so they don't quite conform to WriteValues
		/// </summary>
		/// <param name="writer">The BinaryWriter to use. AKA, where you want the data to be written to</param>
		private void WriteStrings(BinaryWriter writer)
		{
			m_oldLength = (ushort)writer.BaseStream.Position;

			// Write the actual strings. Record duplications or inner string duplications
			Dictionary<string, ushort> strings = new Dictionary<string, ushort>();
			List<ushort> offsets = new List<UInt16>();

			int entryCount = m_propertyLists[InibinTroybinDataSection.String].Count;

			for (int i = 0; i < entryCount; i++)
			{
				string value = (string)m_propertyLists[InibinTroybinDataSection.String][i].Value;
				int offset = -1;
				foreach (KeyValuePair<string, ushort> stringKVP in strings)
				{
					if (stringKVP.Key.EndsWith(value))
					{
						offset = strings[stringKVP.Key] + stringKVP.Key.Length - value.Length;
						offsets.Add((UInt16)offset);
						break;
					}
				}

				if (offset != -1)
					continue;

				offset = (int)(writer.BaseStream.Position - m_oldLength);
				strings.Add(value, (UInt16)offset);
				offsets.Add((UInt16)offset);
				writer.WriteNullTerminatedString(value);
			}

			// Fill in the offsets;
			writer.BaseStream.Seek(m_oldLength - 4 * entryCount, SeekOrigin.Begin);
			for (int i = 0; i < offsets.Count; i++)
			{
				writer.Write(offsets[i]);
			}
		}

		/// <summary>
		/// Similar to WriteValues. Bool values are written as single bits within bytes, so they don't conform to WriteValues
		/// </summary>
		/// <param name="writer">The BinaryWriter to use. AKA, where you want the data to be written to</param>
		private void WriteBool(BinaryWriter writer)
		{
			ushort entryCount = (ushort)m_propertyLists[InibinTroybinDataSection.Bool].Count;
			writer.Write(entryCount);

			for (int i = 0; i < entryCount; i++)
			{
				writer.Write(m_propertyLists[InibinTroybinDataSection.Bool][i].Key);
			}

			// 8 values per byte: NumKeys / 8  (round up)
			int numBytes = (entryCount / 8) + (entryCount % 8 > 0 ? 1 : 0);

			for (int i = 0; i < numBytes; i++)
			{
				int value = 0;
				for (int j = 0; j < 8; j++)
				{
					// Check for array out of bounds
					if ((i * 8) + j >= entryCount)
						break;

					// Convert the bool to a bit then Bitwise-OR it with 'value'
					int boolInt = ((bool)m_propertyLists[InibinTroybinDataSection.Bool][(i * 8) + j].Value) ? 1 : 0;
					value = value | boolInt << j;
				}

				writer.Write((byte)value);
			}
		}
	}
}
