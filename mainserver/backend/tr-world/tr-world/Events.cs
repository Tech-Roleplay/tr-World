using AltV.Net;
using AltV.Net.Resources.Chat.Api;
using AltV.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AltV.Net.Elements.Entities;
using System.Runtime.InteropServices;
using tr_world.Player;
using tr_world.Config;

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


            Utility.DClog($"[{player.Id}] The Player {player.Name} has joined the Server. Welcome.", "Join-Log", secret.URL_Join, "https://altv.mp/img/branding/logo_black.png");
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void PlayerDisconnect(BPlayer player, string reason)
        {
            if (reason == string.Empty)
            {
                reason = "disconnected by user or else";
            }
            //AltChat.SendBroadcast($"[{player.Id}] The Player {player.Name} has left the Server. Bye.");
            Alt.Log($"[{player.Id}] The Player {player.Name} has left the Server. Discord-ID is: {player.DiscordId}. Bye. Of Reason: {reason}.");

            Utility.DClog($"[{player.Id}] The Player {player.Name} has left the Server. Discord-ID is: {player.DiscordId}. Bye. Of Reason: {reason}.", "Disconnect-Log", secret.URL_Join, "https://altv.mp/img/branding/logo_black.png");
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
