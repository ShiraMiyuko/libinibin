using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Dargon.LeagueOfLegends {
      internal InibinTroybinFile(byte version, ushort oldLength, ushort format, Dictionary<uint, object> properties) {
   public class InibinTroybinFile {
         m_version = version;
         m_oldLength = oldLength;
         m_format = format;
         Properties = new ReadOnlyDictionary<uint, object>(properties);
      }


      private byte m_version;
      private ushort m_oldLength;
      private ushort m_format;

      /// <summary>
      /// A dictionary of all the key-value pairs contained within the inibin file. 
      /// <para> A good majority of the keys have been identified and can be referenced with the CharacterInibinKeyHashes enum</para>
      /// <para> Ex: Properties[(uint)CharacterInibinKeyHashes.SkinOneName)]</para>
      /// </summary>
      public IReadOnlyDictionary<uint, object> Properties { get; private set; }
   }
}
