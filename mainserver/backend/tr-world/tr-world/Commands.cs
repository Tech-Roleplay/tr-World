using System;
using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Resources.Chat.Api;
using tr_world.Config;
using trWorld.Config;
using trWorld.Player;
using trWorld.Vehicle;

namespace trWorld;

public class Commands : IScript
{
    private static readonly string[] blacklistVehicles = {
        "rhino",            // Panzer
        "hydra",            // Jet mit Raketen
        "lazer",            // Kampfjet
        "buzzard",          // Kampfhelikopter mit Raketen
        "annihilator",      // Schwer bewaffneter Heli
        "insurgent",        // Militärfahrzeug mit Geschütz
        "insurgent2",       // Mit Geschützturm
        "nightshark",       // Bewaffnetes SUV
        "technical",        // Pickup mit MG
        "technical2",       // Technischer Pickup
        "technical3",       // Panzerpickup
        "oppressor",        // Raketen-Motorrad
        "oppressor2",       // MK2 mit Lenkraketen
        "deluxo",           // Fliegendes Auto mit Waffen
        "phantom2",         // Bewaffneter Truck
        "chernobog",        // Raketenwerfer-Fahrzeug
        "trailersmall2",    // Raketenplattform
        "tampa3",           // Bewaffneter Muscle-Car
        "halftrack",        // Halbkettenfahrzeug mit MG
        "apc",              // Schwimmfähiger Schützenpanzer
        "barrage",          // Offroad mit Bewaffnung
        "menacer",          // SUV mit Maschinengewehr
        "revolter",         // Sportwagen mit MG
        "scramjet",         // Raketenauto mit Waffen
        "bruiser",          // Arena-War-Fahrzeuge (Mad Max Style)
        "brutus",           // Arena-War
        "cerberus",         // Bewaffneter Truck (Arena-War)
        "deathbike",        // Arena-War-Motorrad mit Waffen
        "dominator4",       // Arena-War Muscle-Car
        "impaler2",         // Arena-War Lowrider
        "monster5",         // Arena-War Monstertruck
        "zr380",            // Arena-War Sportwagen
        "strikeforce",      // Kampfflugzeug
        "alkonost",         // Tarnbomber
        "volatol",          // Bomber
        "toreador",         // U-Boot-Auto mit Raketen
        "vetir",            // Militärtruck
        "kosatka",          // U-Boot mit Raketen (eig. kein „Fahrzeug“, aber relevant für Realismus)
        "ruiner2",          // Ruiner 2000 mit Raketen und Fallschirm
        "thruster",         // Jetpack
        "wastelander",      // Postapokalyptisches Fahrzeug
    };
    
    [CommandEvent(CommandEventType.CommandNotFound)]
    public void CommandNotFound(TPlayer player, string cmd)
    {
        player.SendChatMessage("{FF0000}Command not found: " + cmd);
    }

    [Command("login")]
    public void Login(TPlayer player, string discordTag)
    {
        // Random Code generieren
        string code = new Random().Next(100000, 999999).ToString();
        
        
    }
    
    
    // Dev 
    // create peronal bo
    [Command("cpb")]
    public void Cpb(TPlayer player, string name = "")
    {
        player.Emit("DEV:CreateBlip", player.Position.X,player.Position.Y,player.Position.Z, 835, 49, 1.0f, false, name );
    }
    
    // Very basic Commands for Administration
    [Command("warn")]
    public void CMD_warn(TPlayer player, uint targetid, string reason)
    {
        if (!player.HasPlayerPermission((int)TPermission.Administrator))
        {
            foreach (var player1 in Alt.GetAllPlayers())
            {
                var p = (TPlayer)player1;
                if (targetid == p.Id) p.Emit("client:warn:showFullUI", reason, 4000);
            }
        }

        // emit event
    }

    [Command("kick")]
    public void CMD_kick(TPlayer player, uint targetid, string reason)
    {
        if (!player.HasPlayerPermission((int)TPermission.Administrator)) return;

        foreach (var player1 in Alt.GetAllPlayers())
        {
            var p = (TPlayer)player1;
            if (targetid == p.Id) p.Kick(reason);
        }
    }

    [Command("tempban")]
    public void CMD_tempban(TPlayer player, uint targetid, int amount, string timevalue, string reason)
    {
        if (!player.HasPlayerPermission((int)TPermission.Administrator)) return;

        foreach (var player1 in Alt.GetAllPlayers())
        {
            var p = (TPlayer)player1;
            if (targetid == p.Id) p.TempBan(amount, timevalue, reason);
        }
    }

    /// <summary>
    ///     Bans player forever
    /// </summary>
    /// <param name="player">the owner</param>
    /// <param name="targetid">the target id</param>
    /// <param name="reason">The reason behind it</param>
    [Command("ban")]
    public void CMD_ban(TPlayer player, uint targetid, string reason)
    {
        if (!player.HasPlayerPermission((int)TPermission.Owner)) return;

        foreach (var player1 in Alt.GetAllPlayers())
        {
            var p = (TPlayer)player1;
            if (targetid == p.Id) p.Ban(reason);
        }
    }

    [Command("veh", aliases: new[] { "car", "auto", "vehicle" })]
    public void CMD_veh(TPlayer player, string VehicleName)
    {
        // removed 
        //if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;
        
        if (blacklistVehicles.Contains(VehicleName.ToLower()))
        {
            if (!player.HasPlayerPermission((int)TPermission.Moderator))
            {
                player.SendChatMessage("{FF0000} The specified vehicle is blacklisted.");
                return;
            }
                
        }

        
        if (!string.IsNullOrWhiteSpace(VehicleName))
        {
            var veh = (BVehicle)Alt.CreateVehicle(Alt.Hash(VehicleName),
                new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z + 0.5f), player.Rotation);
            if (veh != null)
            {
                veh.NumberplateText = player.Name;
                veh.EngineOn = true;

                //set keyfunc here
                veh.CreateVehicleKey();

                //Log
                player.SendChatMessage($"[Vehicle] The {VehicleName} spawned.");
                Utility.DClog($"{player.Name}({player.SocialClubId}) has spawned a new vehicle: a/an {VehicleName}",
                    "Vehicle Spawner", secret.URL_VehSpawner, "https://th.bing.com/th/id/OIP.kgO6McTklBzmVlcSH4w-2wHaHa?rs=1&pid=ImgDetMain", true);
                player.Emit("SetPlayerIntoVehicle", veh, 1);
                player.SetIntoVehicle(veh, 1);
            }
            else
            {
                player.SendChatMessage("{FF0000}" + $"Vehicle Name: {VehicleName} was not founded.");
            }
        }
    }

    [Command("rmveh")]
    public void CMD_rmVeh(TPlayer player, string[] args)
    {
        if (!player.HasPlayerPermission((int)TPermission.Administrator)) return;
        var veh = player.Vehicle;

        if (veh != null)
            veh.Destroy();
        else
            player.SendChatMessage("{FF0000}You are not in a vehicle.");
    }

    [Command("repair")]
    public void CMD_repair(TPlayer player, string[] args)
    {
        if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;
        var veh = player.Vehicle;

        if (veh != null)
            veh.Repair();
        else
            player.SendChatMessage("{FF0000}You are not in a vehicle.");
    }

    public void CMD_rep(TPlayer player, string[] args)
    {
        if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;

        if (player.GetClosestVehicle() == null || player.GetClosestVehicle().GetType() == typeof(BVehicle))
        {
            player.SendChatMessage("{FF0000}You are not near a vehicle.");
            return;
        }

        player.GetClosestVehicle().Repair();
    }

    [Command("pos", true)]
    public void CMD_pos(TPlayer player, string name)
    {
        if (!player.HasPlayerPermission((int)TPermission.Developer)) return;
        player.SendChatMessage($"X: {player.Position.X} Y: {player.Position.Y} Z: {player.Position.Z} and with Name: {name}.");

        Alt.Log("==POS==");
        Alt.Log($"{player.Position.X}, {player.Position.Y}, {player.Position.Z} and with Name: {name}.");
        Alt.Log("====");

        Utility.DClog($"X/Y/Z: {player.Position.X}, {player.Position.Y}, {player.Position.Z} and with Name: {name}.",
            "Position Save", secret.URL_Position,
            "https://cdn.discordapp.com/avatars/1217755784172015686/2f17a9989ab2dc3869219a803d3dda10.webp?size=1024");
    }

    [Command("addmoneycash")]
    public void CMD_AddMoneyCash(TPlayer player, int amount)
    {
        if (!player.HasPlayerPermission((int)TPermission.Administrator)) return;
        player.AddMoneyToCash(amount, "Administrator giving.");
    }

    [Command("showcash")]
    public void CMD_showcash(TPlayer player, string[] args)
    {
        player.SendChatMessage($"Your Cash Balance is: {player.CashBalance}");
    }

    [Command("setjob")]
    public void CMD_set(TPlayer player, int targetid, string jobname, int jobgrade)
    {
        if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;
        foreach (TPlayer p in Alt.GetAllPlayers())
            if (targetid == p.Id)
                p.SetJob(jobname, jobgrade);
    }

    [Command("showjob")]
    public void CMD_showJob(TPlayer player, string[] args)
    {
        player.SendChatMessage($"Your Job is at: {player.Job.Label} with Grade: {player.Job.GradeLabel}");
    }

    [Command("setgang")]
    public void CMD_setGang(TPlayer player, int targetid, string gangname, int ganggrade)
    {
        if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;
        foreach (TPlayer p in Alt.GetAllPlayers())
            if (targetid == p.Id)
                p.SetGang(gangname, ganggrade);
    }

    [Command("showgang")]
    public void CMD_showGang(TPlayer player, string[] args)
    {
        player.SendChatMessage($"Your Gang-Activity is at: {player.Gang.Label} with Grade: {player.Gang.GradeLabel}");
    }


    [Command("testAdminmenu")]
    public void CMD_testAdminMenu(TPlayer player)
    {
        if (!player.HasPlayerPermission((int)TPermission.Moderator)) return;
        player.Emit("adminmenu:Show");
    }
}