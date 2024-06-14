using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Base
{
    internal class BPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create (ICore core, IntPtr entityPointer, uint id)
        {
            return new BPlayer(core, entityPointer, id);
        }
    }
}
