using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AltV.Net;
using AltV.Net.Elements.Entities;
using tr_world.Base;
using tr_world.Controllers;
using tr_world.Player;

namespace tr_world
{
    public class Main : Resource
    {
        // Start Func
        /// <summary>
        /// On the start of the script.
        /// </summary>
        public override void OnStart()
        {
            Alt.Log("Server-C#-backend is starting!");

            Databank.InitConnection();

            // Timer
            Timer saveTimer = new Timer(OnSaveTimer, null, 1000, 120000);
        }

        // Timers
        public static void OnSaveTimer(object state)
        {
            foreach (var bplayer in Alt.GetAllPlayers())
            {
                var player = (BPlayer)bplayer;
                BPlayerController.SaveBPlayerData(player);
            }
            Alt.LogInfo("All players have been saved!");
        }

       

        // Player Factory
        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new Player.BPlayerFactory();
        }
        
        // Vehicle Factory
        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new Vehicle.BVehicleFactory();
        }



        // Close Func
        public override void OnStop()
        {
            Alt.LogWarning("Server-C#-backend is stopping!");
            OnSaveTimer( null );
        }
    }
}
