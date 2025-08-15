using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using tr_world.Config;
using trWorld.Config;
using trWorld.Controllers;
using trWorld.Player;

namespace trWorld;

public class Events : IScript
{
    // Scripted Events
    [ScriptEvent(ScriptEventType.PlayerConnect)]
    public void PlayerConnect(TPlayer player, string reason)
    {
        if (player.SocialClubId == 0) 
        {
            player.Kick("Hey, Rockstar Launcher is not working");
            return;
        }

        player.Spawn((uint)PedModel.FreemodeMale01, new Position(0, 0, 72));

        AltChat.SendBroadcast($"[{player.Id}] The Player {player.Name} has joined the Server. Welcome.");
        Alt.Log(
            $"[{player.Id}] The Player {player.Name} has joined the Server. Rockstar-ID is: {player.SocialClubId}. Welcome.");

        Utility.DClog(
            $"[{player.Id}] The Player {player.Name} has joined the Server. Welcome. Rockstar-ID is {player.SocialClubId}.",
            "Join-Log", secret.URL_Join, "https://altv.mp/img/branding/logo_black.png", true);

        // Spieler-Daten laden oder neu erstellen
        if (!MongoTPlayerController.HasTPlayerAccount(player.SocialClubId.ToString())) 
            MongoTPlayerController.CreateTPlayerAccount(player);
    
        MongoTPlayerController.LoadTPlayerData(player);
    }


    [ScriptEvent(ScriptEventType.PlayerDisconnect)]
    public void PlayerDisconnect(TPlayer player, string reason)
    {
        if (reason == string.Empty) reason = "disconnected by user or else";
        //AltChat.SendBroadcast($"[{player.Id}] The Player {player.Name} has left the Server. Bye.");
        Alt.Log(
            $"[{player.Id}] The Player {player.Name} has left the Server. Discord-ID is: {player.SocialClubId}. Bye. Of Reason: {reason}.");

        Utility.DClog(
            $"[{player.Id}] The Player {player.Name} has left the Server. Discord-ID is: <@{player.SocialClubId}>. Bye. Of Reason: {reason}.",
            "Disconnect-Log", secret.URL_Join, "https://altv.mp/img/branding/logo_black.png", true);

        MongoTPlayerController.SaveTPlayerData(player);
    }

    // death handle || only for dev
    [ScriptEvent(ScriptEventType.PlayerDead)]
    public static void PlayerDead(TPlayer player, IEntity killer, uint weapon)
    {
        player.Spawn(new Position(0, 0, 72), 100);
        //Alt.Log($"")
    }


    /*// Server Events
    [ClientEvent("Char.LoadPlayer")]
    public static void CharLoadPlayer(TPlayer player, string arg)
    {
        switch (arg)
        {
            case "Character 1":
                player.
                break;
            default:
                break;
        }
    }*/
}