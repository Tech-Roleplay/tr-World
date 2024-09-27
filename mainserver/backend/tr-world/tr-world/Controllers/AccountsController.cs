using System;
using AltV.Net;
using tr_world.Player;

namespace tr_world;

public class AccountsController : IScript
{
   public static void LoadAccount(BPlayer player){
    try
    {
        
    }
    catch (Exception e)
    {
        Alt.LogError("ERROR == ERROR == ERROR");
        Alt.LogError(e.ToString());
    } 

   }
}
