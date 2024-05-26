using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Base
{
    public class BPlayer : Player
    {
        public BPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {
        }
    }
}
