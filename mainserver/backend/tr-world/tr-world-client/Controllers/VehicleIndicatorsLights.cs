using System;
using AltV.Net.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net;

namespace tr_world_client.Controllers
{
    public class VehicleIndicatorsLights : IScript
    {
        public VehicleIndicatorsLights() {
            Alt.OnKeyDown += Keypressed;
        }

        private void Keypressed(AltV.Net.Client.Elements.Data.Key key)
        {
            if (key == AltV.Net.Client.Elements.Data.Key.)
        }
    }
}
