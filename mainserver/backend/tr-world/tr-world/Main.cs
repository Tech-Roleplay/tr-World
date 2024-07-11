using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using tr_world.Base;

namespace tr_world
{
    public class Main : Resource
    {
        // Start Func
        public override void OnStart()
        {
            Alt.Log("Server-C#-backend is starting!");
        }

        // Player Factory
        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new BPlayerFactory();
        }

        // Close Func
        public override void OnStop()
        {
            Alt.Log("Server-C#-backend is stopping!");
        }
    }
}
