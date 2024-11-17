using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Player;

namespace tr_world.Controllers
{
    public class PhoneController
    {
        /// <summary>
        /// Check if the phone Number is Registered
        /// </summary>
        /// <param name="player">The target player</param>
        /// <param name="phoneNumber">the giving phone number</param>
        /// <returns>If the number is Registered true, otherwise false</returns>
        public static bool IsPhoneRegistered(BPlayer player, int phoneNumber)
        {
            // adding mysql check
            if (phoneNumber == 911) return true;
            
            return phoneNumber > 1000000;
        }
    }
}
