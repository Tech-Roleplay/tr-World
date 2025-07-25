using System.Threading;
using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using trWorld.Base;
using trWorld.Controllers;
using trWorld.Player;
using trWorld.Vehicle;

namespace trWorld;

public class Main : Resource
{
    // Start Func
    /// <summary>
    ///     On the start of the script.
    /// </summary>
    public override void OnStart()
    {
        Console.WriteLine(AppContext.BaseDirectory);
        Alt.Log("Server-C#-backend is starting!");
        
        Databank.InitMongo();
        Databank.InitMySQL();

        if (!Databank.MongoConnected && !Databank.MySQLConnected)
        {
            Alt.LogError("Keine Datenbank konnte verbunden werden. Server wird beendet.");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        //GameTimeService.Init();
        
        // Timer
        var saveTimer = new Timer(OnSaveTimer, null, 1000, 120000);
        var timeSyncTimer = new Timer(SendTimeToPlayer, null, 0, 4000);
    }

    // Timers
    public static void OnSaveTimer(object state)
    {
        var (hour, minute, day, month, year) = GameTimeService.GetCurrentGameDateTime();
        var newStartTime = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
        //GameTimeService.SetStartTime(newStartTime);
        
        foreach (var bplayer in Alt.GetAllPlayers())
        {
            var player = (TPlayer)bplayer;
            TPlayerController.SaveTPlayerData(player);
        }

        Alt.LogInfo("All players have been saved!");
    }

    public static void SendTimeToPlayer(object state)
    {
        var (hour, minute, day, month, year) = GameTimeService.GetCurrentGameDateTime();

        foreach (var player in Alt.GetAllPlayers())
        {
            player.Emit("updateTime", hour, minute, day, month, year);
        }
    }

    // Player Factory
    public override IEntityFactory<IPlayer> GetPlayerFactory()
    {
        return new TPlayerFactory();
    }

    // Vehicle Factory
    public override IEntityFactory<IVehicle> GetVehicleFactory()
    {
        return new BVehicleFactory();
    }


    // Close Func
    public override void OnStop()
    {
        Alt.LogWarning("Server-C#-backend is stopping!");
        OnSaveTimer(null);
    }
}