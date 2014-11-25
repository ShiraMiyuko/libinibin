using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Dargon.LeagueOfLegends {
   public class Inibin : IInibin {
      private byte version;
      private ushort oldLength;
      private ushort format;
      private Dictionary<uint, object> properties;

      internal Inibin(byte version, ushort oldLength, ushort format) {
         this.version = version;
         this.oldLength = oldLength;
         this.format = format;
         this.properties = new Dictionary<uint, object>();
      }

      public byte Version { get { return version; } }
      public ushort OldLength { get { return oldLength; } }
      public ushort Format { get { return format; } }

      /// <summary>
      /// A dictionary of all the key-value pairs contained within the inibin file. 
      /// <para> A good majority of the keys have been identified and can be referenced with the CharacterInibinKeyHashes enum</para>
      /// <para> Ex: Properties[(uint)CharacterInibinKeyHashes.SkinOneName)]</para>
      /// </summary>
      public IReadOnlyDictionary<uint, object> Properties { get { return properties; } }

      public void Set(uint key, object value) {
         properties.Add(key, value);
      }
   }
}
