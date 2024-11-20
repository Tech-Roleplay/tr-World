using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Models
{
    public class Garage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsForJob => !string.IsNullOrEmpty(JobGarage);
        public string JobGarage { get; set; }
        public TGagVehicleList[] VehicleList {  get; set; } 
    }
    public class TGagVehicleList
    {
        public int VehicleID { get; set; }
        public double VehicleDamage { get; set; }
    }
}
