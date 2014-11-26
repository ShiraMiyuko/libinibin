using System.IO;

namespace Dargon.LeagueOfLegends {
   public interface IInibinLoader {
      IInibin Load(Stream stream);
   }
}