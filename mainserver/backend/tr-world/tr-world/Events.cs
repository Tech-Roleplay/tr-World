using AltV.Net;
using AltV.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Base;

namespace tr_world
{
    internal class Events : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void PlayerConnect(BPlayer player, string reason)
        {
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 72), 0);
        }
    }
}
