using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace tr_world.Vehicle;

internal class BVehicleFactory : IEntityFactory<IVehicle>
{
    public IVehicle Create(ICore core, IntPtr entityPointer, uint id)
    {
        return new BVehicle(core, entityPointer, id);
    }
}