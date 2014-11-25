using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ItzWarty;

namespace Dargon.LeagueOfLegends {
   public partial class InibinTroybinFile {
      /// <summary>
      /// Helper class that does the actual reading of the inibin/troybin files
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      private void ReadInibin(BinaryReader reader) {
         // Initialize the two members that store all the data
         m_properties = new Dictionary<uint, InibinProperty>();
         m_propertyLists = new Dictionary<InibinTroybinDataSection, List<InibinProperty>>();

         // Header Info
         m_version = reader.ReadByte();
         m_oldLength = reader.ReadUInt16(); // fileLength - oldLen == offset to string section
         m_format = reader.ReadUInt16();

         // Section 0. uint values
         if ((m_format & 0x0001) == 0x0001)
            ReadValues(reader, InibinTroybinDataSection.UInt);

         // Section 1. Float values
         if ((m_format & 0x0002) == 0x0002)
            ReadValues(reader, InibinTroybinDataSection.Float);

         // Section 2. Small floats that are written as bytes
         if ((m_format & 0x0004) == 0x0004)
            ReadValues(reader, InibinTroybinDataSection.SmallFloats);

         // Section 3. ushort values
         if ((m_format & 0x0008) == 0x0008)
            ReadValues(reader, InibinTroybinDataSection.UShort);

         // Section 4. Byte values
         if ((m_format & 0x0010) == 0x0010)
            ReadValues(reader, InibinTroybinDataSection.Byte);

         // Section 5. Bool values. Each is stored in a single bit
         if ((m_format & 0x0020) == 0x0020)
            ReadBoolValues(reader);

         // Section 6. Vector3<byte> values.
         if ((m_format & 0x0040) == 0x0040)
            ReadValues(reader, InibinTroybinDataSection.Vector3Byte);

         // Section 7. Vector3<float> values
         if ((m_format & 0x0080) == 0x0080)
            ReadValues(reader, InibinTroybinDataSection.Vector3Float);

         // Section 8. Vector2<byte> values
         if ((m_format & 0x0100) == 0x0100)
            ReadValues(reader, InibinTroybinDataSection.Vector2Byte);

         // Section 9. Vector2<float> values
         if ((m_format & 0x0200) == 0x0200)
            ReadValues(reader, InibinTroybinDataSection.Vector2Float);

         // Section 10. Vector4<byte> values
         if ((m_format & 0x0400) == 0x0400)
            ReadValues(reader, InibinTroybinDataSection.Vector4Byte);

         // Section 11. Vector4<float> values
         if ((m_format & 0x0800) == 0x0800)
            ReadValues(reader, InibinTroybinDataSection.Vector4Float);

         // Offsets to the strings.
         if ((m_format & 0x1000) == 0x1000)
            ReadStrings(reader);


         // Create the read-only shell for viewing
         Properties = new ReadOnlyDictionary<uint, InibinProperty>(m_properties);
      }

      /// <summary>
      /// Reads the uint keys
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      /// <returns>uint[] containing the keys</returns>
      private static UInt32[] ReadKeys(BinaryReader reader) {
         // Number of key value pairs
         ushort count = reader.ReadUInt16();

         // Sometimes this happens. AKA, Riot fails once again
         if (count == 0)
            return null;

         uint[] result = new uint[count];
         for (int i = 0; i < count; ++i) {
            result[i] = reader.ReadUInt32();
         }

         return result;
      }

      /// <summary>
      /// Reads the values using 'section' to know what type the values are
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      /// <param name="section">The data type of the values</param>
      private void ReadValues(BinaryReader reader, InibinTroybinDataSection section) {
         // Initialize the List for this section
         m_propertyLists.Add(section, new List<InibinProperty>());
         // Read the keys
         UInt32[] keys = ReadKeys(reader);

         // Believe it or not, RIOT fails enough to have a section declared in the format, but have nothing in it
         if (keys != null) {
            // Iterate through the keys and read a value for each one
            // Do NOT convert this to a foreach; foreach can not be guaranteed to be in a specific order
            for (int i = 0; i < keys.Length; i++) {
               object val = null;
               if (section == InibinTroybinDataSection.Byte) {
                  val = reader.ReadByte();
               } else if (section == InibinTroybinDataSection.UShort) {
                  val = reader.ReadUInt16();
               } else if (section == InibinTroybinDataSection.UInt) {
                  val = reader.ReadUInt32();
               } else if (section == InibinTroybinDataSection.Float) {
                  val = reader.ReadSingle();
               } else if (section == InibinTroybinDataSection.SmallFloats) {
                  val = reader.ReadByte() * 0.1f;
               } else if (section == InibinTroybinDataSection.Vector2Byte) {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  val = new Vector2<byte>(x, y);
               } else if (section == InibinTroybinDataSection.Vector2Float) {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  val = new Vector2<float>(x, y);
               } else if (section == InibinTroybinDataSection.Vector3Byte) {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  byte z = reader.ReadByte();
                  val = new Vector3<byte>(x, y, z);
               } else if (section == InibinTroybinDataSection.Vector3Float) {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  float z = reader.ReadSingle();
                  val = new Vector3<float>(x, y, z);
               } else if (section == InibinTroybinDataSection.Vector4Byte) {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  byte z = reader.ReadByte();
                  byte w = reader.ReadByte();
                  val = new Vector4<byte>(x, y, z, w);
               } else if (section == InibinTroybinDataSection.Vector4Float) {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  float z = reader.ReadSingle();
                  float w = reader.ReadSingle();
                  val = new Vector4<float>(x, y, z, w);
               }

               // Create a property for the key value pair and add it to our data collections
               InibinProperty prop = new InibinProperty(keys[i], val, section);
               m_propertyLists[section].Add(prop);
               m_properties.Add(keys[i], prop);
            }
         }
      }

      /// <summary>
      /// Similar to ReadValues but only for Bool values. 
      /// Boolean Values are stored in single bits so the code couldn't conform to ReadValues
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      private void ReadBoolValues(BinaryReader reader) {
         m_propertyLists[InibinTroybinDataSection.Bool] = new List<InibinProperty>();

         UInt32[] keys = ReadKeys(reader);
         if (keys != null) {
            // 8 values per byte: NumKeys / 8  (round up)
            int numBytes = (keys.Length / 8) + (keys.Length % 8 > 0 ? 1 : 0);

            for (int i = 0; i < numBytes; i++) {
               byte buffer = reader.ReadByte();
               for (int j = 0; j < 8; j++) {
                  // See if we're on the last key
                  if ((i * 8) + j >= keys.Length)
                     break;

                  // Check if the bit is on
                  bool val = (buffer & (1 << j)) == (1 << j);

                  InibinProperty prop = new InibinProperty(keys[(i * 8) + j], val, InibinTroybinDataSection.Bool);
                  m_propertyLists[InibinTroybinDataSection.Bool].Add(prop);
                  m_properties.Add(keys[(i * 8) + j], prop);
               }
            }
         }
      }

      /// <summary>
      /// Similar to ReadValues but only for String values. 
      /// Strings are first referenced as key value pairs where the values are offsets to a 'string data section'
      /// So we read the keys, then the offsets, then use the offsets to read the null-terminated string data
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      private void ReadStrings(BinaryReader reader) {
         m_propertyLists[InibinTroybinDataSection.String] = new List<InibinProperty>();

         UInt32[] keys = ReadKeys(reader);

         //
         // New method to read the newer .inibins.
         // Why determine the offset by reading in data from the objIn header
         // when we can just compute it here?  This seems to fix the problem
         // with newer .inibins.  I'm not sure what the value in the header
         // is used for though.
         //

         if (keys != null) {
            long strSectionOffset = reader.BaseStream.Position + keys.Length * 2;

            for (int i = 0; i < keys.Length; i++) {
               UInt16 offset = reader.ReadUInt16();

               // Save the old position
               Int64 oldPos = reader.BaseStream.Position;
               reader.BaseStream.Seek(strSectionOffset + offset, SeekOrigin.Begin);

               string val = reader.ReadNullTerminatedString();

               // Return to the old position
               reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

               InibinProperty prop = new InibinProperty(keys[i], val, InibinTroybinDataSection.String);
               m_propertyLists[InibinTroybinDataSection.String].Add(prop);
               m_properties.Add(keys[i], prop);
            }
         }
      }
   }
}
