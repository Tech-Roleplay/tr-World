using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net;

namespace tr_world
{
    public class Main : Resource
    {
        // Start Func
        public override void OnStart()
        {
            Alt.Log("Server-C#-backend is starting!");


        }

        // Close Func
        public override void OnStop()
        {
            Alt.Log("Server-C#-backend is stopping!");
        }
    }
}
