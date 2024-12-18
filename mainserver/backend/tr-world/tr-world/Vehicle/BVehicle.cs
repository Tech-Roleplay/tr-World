using System;
using AltV.Net;

namespace tr_world.Vehicle;

public class BVehicle : AltV.Net.Elements.Entities.Vehicle
{
    public BVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
    }

    /// <summary>
    ///     The giving ID for a vehicle
    /// </summary>
    public int VehicleID { get; set; }

    /// <summary>
    ///     The KeyID for access the vehicle
    /// </summary>
    public string KeyID { get; set; }

    public string GarageID { get; set; }
    public float Fuel { get; set; }
}