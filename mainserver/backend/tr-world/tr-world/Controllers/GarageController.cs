using System;
using AltV.Net;
using MySqlConnector;
using tr_world.Player;

namespace tr_world.Controllers;

public class GarageController : IScript
{
    public static void GetGarage()
    {
        
    }
    
    public static void GetGarageForPlayer(int garageID, BPlayer player)
    {
        try
        {
            MySqlCommand cmd = tr_world.Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM garage WHERE garage_id=@garage_id";
            cmd.Parameters.AddWithValue("@garage_id", garageID);
            
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    
                }
            }


            }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }
    }

    public static void SaveGarageForPlayer(BPlayer player)
    {
        
    }

    public static void UpdateGarageForPlayer(BPlayer player)
    {
        
    }

    [Obsolete]
    public static void GetTheListOfVehFromPlayer(BPlayer player)
    {
        
    }
}