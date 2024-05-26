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
        public override void OnStart()
        {
            Alt.Log("Server-c#-backend is starting!");

        }

        public override void OnStop()
        {
            throw new NotImplementedException();
        }
    }
}
