using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using ItzWarty;
using ItzWarty.Collections;

namespace Dargon.LeagueOfLegends {
   public class InibinLoader : IInibinLoader {
      private static readonly IReadOnlyDictionary<InibinTroybinDataSection, Func<BinaryReader, object>> readersByDataType = new Dictionary<InibinTroybinDataSection, Func<BinaryReader, object>> {
         {InibinTroybinDataSection.Byte, reader => reader.ReadByte()},
         {InibinTroybinDataSection.UShort, reader => reader.ReadUInt16()}
                  break;
               case InibinTroybinDataSection.UInt:
                  val = reader.ReadUInt32();
                  break;
               case InibinTroybinDataSection.Float:
                  val = reader.ReadSingle();
                  break;
               case InibinTroybinDataSection.SmallFloats:
                  val = reader.ReadByte() * 0.1f;
                  break;
               case InibinTroybinDataSection.Vector2Byte: 
               {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  val = new Vector2<byte>(x, y);
                  break;
               }
               case InibinTroybinDataSection.Vector2Float: 
               {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  val = new Vector2<float>(x, y);
                  break;
               }
               case InibinTroybinDataSection.Vector3Byte: 
               {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  byte z = reader.ReadByte();
                  val = new Vector3<byte>(x, y, z);
                  break;
               }
               case InibinTroybinDataSection.Vector3Float: 
               {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  float z = reader.ReadSingle();
                  val = new Vector3<float>(x, y, z);
                  break;
               }
               case InibinTroybinDataSection.Vector4Byte: 
               {
                  byte x = reader.ReadByte();
                  byte y = reader.ReadByte();
                  byte z = reader.ReadByte();
                  byte w = reader.ReadByte();
                  val = new Vector4<byte>(x, y, z, w);
                  break;
               }
               case InibinTroybinDataSection.Vector4Float: 
               {
                  float x = reader.ReadSingle();
                  float y = reader.ReadSingle();
                  float z = reader.ReadSingle();
                  float w = reader.ReadSingle();
                  val = new Vector4<float>(x, y, z, w);
                  break;
               }
            }
      )

      public IInibin Load(Stream stream) {
         using (var reader = new BinaryReader(stream)) {
            // Read Header
            var version = reader.ReadByte();
            var oldLength = reader.ReadUInt16();
            var format = reader.ReadUInt16();

            var inibin = new Inibin(version, oldLength, format);
            ParseInibin(reader, inibin);
         }
      }

      /// <summary>
      /// Helper class that does the actual reading of the inibin/troybin files
      /// </summary>
      private void ParseInibin(BinaryReader reader, Inibin inibin) {
         var format = inibin.Format;

         // Section 0. uint values
         if ((format & 0x0001) == 0x0001) {
            ReadValues(reader, inibin, InibinTroybinDataSection.UInt);
         }

         // Section 1. Float values
         if ((format & 0x0002) == 0x0002) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Float);
         }

         // Section 2. Small floats that are written as bytes
         if ((format & 0x0004) == 0x0004) {
            ReadValues(reader, inibin, InibinTroybinDataSection.SmallFloats);
         }

         // Section 3. ushort values
         if ((format & 0x0008) == 0x0008) {
            ReadValues(reader, inibin, InibinTroybinDataSection.UShort);
         }

         // Section 4. Byte values
         if ((format & 0x0010) == 0x0010) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Byte);
         }

         // Section 5. Bool values. Each is stored in a single bit
         if ((format & 0x0020) == 0x0020) {
            ReadBoolValues(reader, inibin);
         }

         // Section 6. Vector3<byte> values.
         if ((format & 0x0040) == 0x0040) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector3Byte);
         }

         // Section 7. Vector3<float> values
         if ((format & 0x0080) == 0x0080) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector3Float);
         }

         // Section 8. Vector2<byte> values
         if ((format & 0x0100) == 0x0100) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector2Byte);
         }

         // Section 9. Vector2<float> values
         if ((format & 0x0200) == 0x0200) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector2Float);
         }

         // Section 10. Vector4<byte> values
         if ((format & 0x0400) == 0x0400) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector4Byte);
         }

         // Section 11. Vector4<float> values
         if ((format & 0x0800) == 0x0800) {
            ReadValues(reader, inibin, InibinTroybinDataSection.Vector4Float);
         }

         // Offsets to the strings.
         if ((format & 0x1000) == 0x1000) {
            ReadStrings(reader, inibin);
         }
      }

      /// <summary>
      /// Reads the uint keys
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      /// <returns>uint[] containing the keys</returns>
      private static UInt32[] ReadKeys(BinaryReader reader) {
         // Number of key value pairs
         ushort count = reader.ReadUInt16();

         return Util.Generate(count, i => reader.ReadUInt32());
      }

      /// <summary>
      /// Reads the values using 'section' to know what type the values are
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      /// <param name="section">The data type of the values</param>
      private static void ReadValues(BinaryReader reader, ref Dictionary<uint, object> properties, InibinTroybinDataSection section) {
         // Read the keys
         var keys = ReadKeys(reader);

         // Iterate through the keys and read a value for each one
         // Do NOT convert this to a foreach; foreach can not be guaranteed to be in a specific order
         foreach (var key in keys) {
            object val = null;
            properties.Add(key, val);
         }
      }

      /// <summary>
      /// Similar to ReadValues but only for Bool values. 
      /// Boolean Values are stored in single bits so the code couldn't conform to ReadValues
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      private static void ReadBoolValues(BinaryReader reader, ref Dictionary<uint, object> properties) {
         UInt32[] keys = ReadKeys(reader);

         // Believe it or not, RIOT will declare a section in the format, but have nothing in it. Fail...
         if (keys != null) {
            // 8 values per byte: NumKeys / 8  (round up)
            int numBytes = (keys.Length / 8) + (keys.Length % 8 > 0 ? 1 : 0);

            for (int i = 0; i < numBytes; i++) {
               byte buffer = reader.ReadByte();
               for (int j = 0; j < 8; j++) {
                  // See if we're on the last key
                  if ((i * 8) + j >= keys.Length) {
                     break;
                  }

                  // Check if the bit is on
                  bool val = (buffer & (1 << j)) == (1 << j);

                  properties.Add(keys[(i * 8) + j], val);
               }
            }
         }
      }

      private static string ReadNullTerminatedString() {
         return null;
      }

      /// <summary>
      /// Similar to ReadValues but only for String values. 
      /// Strings are first referenced as key value pairs where the values are offsets to a 'string data section'
      /// So we read the keys, then the offsets, then use the offsets to read the null-terminated string data
      /// </summary>
      /// <param name="reader">The BinaryReader to use. AKA, where you want the data to be read from</param>
      private static void ReadStrings(BinaryReader reader, ref Dictionary<uint, object> properties) {
         UInt32[] keys = ReadKeys(reader);

         //
         // New method to read the newer .inibins.
         // Why determine the offset by reading in data from the header
         // when we can just compute it here?  This seems to fix the problem
         // with newer .inibins.  I'm not sure what the value in the header
         // is used for though.
         //

         // Believe it or not, RIOT will declare a section in the format, but have nothing in it. Fail...
         if (keys != null) {
            long strSectionOffset = reader.BaseStream.Position + keys.Length * 2;

            for (int i = 0; i < keys.Length; i++) {
               UInt16 offset = reader.ReadUInt16();

               // Save the old position
               Int64 oldPos = reader.BaseStream.Position;
               reader.BaseStream.Seek(strSectionOffset + offset, SeekOrigin.Begin);

               string val = /*reader.*/ReadNullTerminatedString();

               // Return to the old position
               reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

               properties.Add(keys[i], val);
            }
         }
      }
   }
}
