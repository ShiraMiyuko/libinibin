using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dargon.LeagueOfLegends {
   class InibinLoader {
      static InibinTroybinFile ParseInibinFromStream(Stream stream) {
         return InibinTroybinFile(stream);
      }
   }
}
