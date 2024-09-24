using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Vehicle
{
    public static class VehicleFunctions
    {
        private static Random random = new Random();
        public static void CreateVehicleKey(this BVehicle vehicle, int min = 1000001, int max = 9999999)
        {
            int key = random.Next(min, max);
            vehicle.KeyID = key.ToString();
        }
        public static string GetVehicleKey(this BVehicle vehicle)
        {
            return vehicle.KeyID;
        }
    }
}
