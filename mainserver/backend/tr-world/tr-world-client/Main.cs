﻿using AltV.Net.Client;

namespace tr_world_client
{
    public class Main : Resource
    {
        public override void OnStart()
        {
            Alt.Log("Hello from Client");
        }

        public override void OnStop()
        {
            throw new NotImplementedException();
        }
    }
}