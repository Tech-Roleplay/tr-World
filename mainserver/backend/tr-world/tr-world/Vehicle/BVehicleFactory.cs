using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Vehicle
{
    internal class BVehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(ICore core, IntPtr entityPointer, uint id)
        {
            return new BVehicle(core, entityPointer, id);
        }
    }
}
