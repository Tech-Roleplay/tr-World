using AltV.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Vehicle
{
    public class BVehicle : AltV.Net.Elements.Entities.Vehicle
    {

        public string KeyID {  get; set; }
        public string GarageID { get; set; }

        public BVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {

        }

    }
}
