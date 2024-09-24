using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Player;
using tr_world.Vehicle;

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
                BVehicle veh = (BVehicle)Alt.CreateVehicle(Alt.Hash(VehicleName), new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z), player.Rotation);
                veh.NumberplateText = player.Name;
                veh.EngineOn = true;

                //set keyfunc here
                veh.CreateVehicleKey();

                //Log
                player.SendChatMessage($"[Vehicle] The {VehicleName} spawned.");
                Utility.DClog($"{player.Name}(<@{player.DiscordId}>) has spawned a new vehicle: a/an {VehicleName}", "Vehicle Spawner", Config.secret.URL_VehSpawner, "", true);
            }
            else
            {
                player.SendChatMessage("{FF0000}" + $"Vehicle Name: {VehicleName} was not founded.");
            }
            
        }

        [Command("repair")]
        public void CMD_repair(BPlayer player, string[] args)
        {
            IVehicle veh = player.Vehicle;

            if ( veh != null )
            {
                veh.Repair();
            }
            else
            {
                player.SendChatMessage("{FF0000}You are not in a vehicle.");
            }
        }

        [Command("pos", true)]
        public void CMD_pos(BPlayer player, string name){
            player.SendChatMessage($"X: {player.Position.X} Y: {player.Position.Y} Z: {player.Position.Z} and with Name: {name}.");

            Alt.Log("==POS==");
            Alt.Log($"{player.Position.X}, {player.Position.Y}, {player.Position.Z} and with Name: {name}.");
            Alt.Log("");

            Utility.DClog($"X/Y/Z: {player.Position.X}, {player.Position.Y}, {player.Position.Z} and with Name: {name}.", "Position Save", "https://ptb.discord.com/api/webhooks/1217755784172015686/8-B8A7rFN3E6PE_W-H5mZuQMRI6Q5BMfRv7jR4PvL7L5y-epGr1bLfwGEc8hlMdRK-oI", "https://cdn.discordapp.com/avatars/1217755784172015686/2f17a9989ab2dc3869219a803d3dda10.webp?size=1024");
        }

        [Command("addmoneycash")]
        public void CMD_addmoneycash(BPlayer player, int amount)
        {
            player.AddMoneyToCash(amount, "");
        }

        [Command ("showcash")]
        public void CMD_showcash(BPlayer player, string[] args)
        {
            player.SendChatMessage($"Your Cash Balance is: {player.CashBalance}");
        }

        [Command("setjob")]
        public void CMD_set(BPlayer player, int targetid, string jobname, int jobgrade)
        {
            foreach (BPlayer p in Alt.GetAllPlayers())
            {
                if (targetid == p.Id)
                {
                    p.SetJob(jobname, jobgrade);
                }
            }

        }

        [Command("showjob")]
        public void CMD_showJob(BPlayer player, string[] args)
        {
            player.SendChatMessage($"Your Job is: {player.Job.Label} with Grade {player.Job.Grade_Label}");
        }
    }
}
