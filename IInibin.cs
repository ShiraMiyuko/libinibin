using System.Collections.Generic;

namespace Dargon.LeagueOfLegends {
   public interface IInibin {
      IReadOnlyDictionary<uint, object> Properties { get; }
   }
}