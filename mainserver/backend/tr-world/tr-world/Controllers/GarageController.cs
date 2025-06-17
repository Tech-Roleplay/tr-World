using System;
using AltV.Net;
using MySqlConnector;
using trWorld.Player;

namespace trWorld.Controllers;

public class GarageController : IScript
{
    public static void GetGarage()
    {
        
    }
    
    public static void GetGarageForPlayer(int garageId, TPlayer player)
    {
        try
        {
            MySqlCommand cmd = trWorld.Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM garage WHERE garage_id=@garage_id";
            cmd.Parameters.AddWithValue("@garage_id", garageId);
            
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

    public static void SaveGarageForPlayer(TPlayer player)
    {
        
    }

    public static void UpdateGarageForPlayer(TPlayer player)
    {
        
    }

    [Obsolete]
    public static void GetTheListOfVehFromPlayer(TPlayer player)
    {
        
    }
}