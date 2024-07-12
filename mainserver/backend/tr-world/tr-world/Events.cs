using AltV.Net;
using AltV.Net.Resources.Chat.Api;
using AltV.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Base;
using tr_world;

using AltV.Net.Elements.Entities;
using System.Runtime.InteropServices;

namespace tr_world
{
    public class Events : IScript
    {
        // Scripted Events
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void PlayerConnect(BPlayer player, string reason)
        {
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 72), 0);

            AltChat.SendBroadcast($"[{player.Id}] The Player {player.Name} has joined the Server. Welcome.");
            Alt.Log($"[{player.Id}] The Player {player.Name} has joined the Server. Discord-ID is: {player.DiscordId}. Welcome.");


            Utility.DClog($"[{player.Id}] The Player {player.Name} has joined the Server. Welcome.", "Join-Log", "https://ptb.discord.com/api/webhooks/1169370156791771216/13iMjj-nh-h1LOiSFw-W1Usz95gJyIbt9niM-cv5mq42CYLWCYXaDctwkjqCN2p5Mszl", "https://altv.mp/img/branding/logo_black.png");
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void PlayerDisconnect(BPlayer player, string reason)
        {
            //AltChat.SendBroadcast($"[{player.Id}] The Player {player.Name} has left the Server. Bye.");
            Alt.Log($"[{player.Id}] The Player {player.Name} has left the Server. Discord-ID is: {player.DiscordId}. Bye. Of Reason: {reason}.");
        }

        // death handle || only for dev
        [ScriptEvent(ScriptEventType.PlayerDead)]
        public void PlayerDead(BPlayer player, IEntity killer, uint weapon)
        {
            player.Spawn(new AltV.Net.Data.Position(0, 0, 72), 100);
            //Alt.Log($"")
        }


        // Server Events
    }
}
