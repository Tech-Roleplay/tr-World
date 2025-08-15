using System;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using trWorld.Base;
using trWorld.Controllers;
using trWorld.Player;
using trWorld.Vehicle;

namespace trWorld
{
    public class Main : Resource
    {
        // Timers for periodic actions: saving player data and synchronizing game time
        private Timer? _saveTimer;
        private Timer? _timeSyncTimer;

        // ====================
        // Server start method
        // ====================
        public override void OnStart()
        {
            Alt.Log("Server C# backend is starting!");

            // Initialize databases
            Databank.InitMongo();   // Initialize MongoDB connection
            Databank.InitMySQL();   // Initialize MySQL connection

            // If neither database is connected, terminate the server
            if (!Databank.MongoConnected && !Databank.MySQLConnected)
            {
                Alt.LogError("No database connection could be established. Server will shut down.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

            // Initialize timers:
            // - Save all player data every 2 minutes
            // - Synchronize game time to all players every 4 seconds
            _saveTimer = new Timer(OnSaveTimer, null, 1000, 120000);
            _timeSyncTimer = new Timer(SendTimeToPlayer, null, 0, 4000);

            // Start the Discord bot asynchronously
            _ = Task.Run(async () =>
            {
                var bot = new DiscordAuthBot();
                await bot.StartAsync();
            });
        }

        // ====================
        // Timer callback methods
        // ====================

        /// <summary>
        /// Saves all connected players' data to the database.
        /// This is triggered periodically by a timer and also on server stop.
        /// </summary>
        private void OnSaveTimer(object? state)
        {
            // Get all connected players
            var players = Alt.GetAllPlayers();

            // Iterate over all players and save their data
            foreach (var player in players)
            {
                // Cast player to custom TPlayer type before saving
                MongoTPlayerController.SaveTPlayerData(player as TPlayer);
            }

            Alt.LogInfo($"Saved {players.Count} players successfully!");
        }

        /// <summary>
        /// Synchronizes the current game time to all connected players.
        /// This ensures that every player's client sees the same in-game time.
        /// </summary>
        public static void SendTimeToPlayer(object state)
        {
            // Retrieve the current in-game date and time
            var (hour, minute, day, month, year) = GameTimeService.GetCurrentGameDateTime();

            // Send the time update to all players
            foreach (var player in Alt.GetAllPlayers())
            {
                player.Emit("updateTime", hour, minute, day, month, year);
            }
        }

        // ====================
        // Factory methods
        // ====================

        /// <summary>
        /// Provides a custom factory for player entities.
        /// This allows creation of TPlayer instances instead of generic IPlayer.
        /// </summary>
        public override IEntityFactory<IPlayer> GetPlayerFactory() => new TPlayerFactory();

        /// <summary>
        /// Provides a custom factory for vehicle entities.
        /// This allows creation of BVehicle instances instead of generic IVehicle.
        /// </summary>
        public override IEntityFactory<IVehicle> GetVehicleFactory() => new BVehicleFactory();

        // ====================
        // Server stop method
        // ====================

        /// <summary>
        /// Called when the server is stopping.
        /// Ensures that all player data is saved before shutdown.
        /// </summary>
        public override void OnStop()
        {
            Alt.LogWarning("Server C# backend is stopping!");
            OnSaveTimer(null); // Save all player data on server shutdown
        }
    }
}
