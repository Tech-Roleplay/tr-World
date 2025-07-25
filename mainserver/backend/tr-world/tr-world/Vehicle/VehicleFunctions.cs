using System;
using trWorld.Player;

namespace trWorld.Vehicle;

public static class VehicleFunctions
{
    private static readonly Random random = new();

    /// <summary>
    ///     Create a random Key for vehicle
    /// </summary>
    /// <param name="vehicle">the target vehicle.</param>
    /// <param name="min">minimale Int</param>
    /// <param name="max">maximale Int</param>
    public static void CreateVehicleKey(this BVehicle vehicle, int min = 1000001, int max = 9999999)
    {
        var key = random.Next(min, max);
        vehicle.KeyID = key.ToString();
    }

    /// <summary>
    ///     Get the Vehicle Key
    /// </summary>
    /// <param name="vehicle">the target vehicle.</param>
    /// <returns>Gives the vehicle key</returns>
    public static string GetVehicleKey(this BVehicle vehicle)
    {
        return vehicle.KeyID;
    }
    

   



}