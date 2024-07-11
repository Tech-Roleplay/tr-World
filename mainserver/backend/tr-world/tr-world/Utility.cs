using AltV.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world
{
    public class Utility : IScript
    {
        public static void ScriptLog(string message)
        {
            Alt.Log($"Script: [{Alt.Resource.Name}] " + message);
        }
    }
}
