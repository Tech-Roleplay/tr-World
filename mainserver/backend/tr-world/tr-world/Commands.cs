using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Base;

namespace tr_world
{
    public class Commands : IScript
    {
        [CommandEvent(CommandEventType.CommandNotFound)]
        public void CommandNotFound(BPlayer player, string cmd) 
        {
            player.SendChatMessage("{FF0000}Command not found: " +  cmd);
        }

        [Command("veh")]
        public void CMD_veh(BPlayer player, string VehicleName)
        {
            if (VehicleName != null)
            {
                IVehicle veh = Alt.CreateVehicle(Alt.Hash(VehicleName), new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z), player.Rotation);
                veh.NumberplateText = player.Name;
                veh.EngineOn = true;
            }
            else
            {
                player.SendChatMessage($"Vehicle Name: {VehicleName} was not founded.");
            }
            
        }

    }
}
