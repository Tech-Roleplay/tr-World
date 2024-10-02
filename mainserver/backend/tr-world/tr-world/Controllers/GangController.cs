using System;
using AltV.Net;
using MySqlConnector;

namespace tr_world.Controllers;

public class GangController : IScript
{
    public static object[] LoadGangDetailsFromDb(string gangName)
    {
        try
        {
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            
            cmd.CommandText = "SELECT * FROM gangs WHERE name=@name LIMIT 1";
            cmd.Parameters.AddWithValue("@name", gangName);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                
                string label = reader.GetString("label");
                object[] backArray = new object[] { label };
                
                reader.Close();
                
                return backArray;
            }
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
            
            return null;
        }
    }

    public static object[] LoadGangGradeFromDb(string gangName, int grade)
    {
        try
        {
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM gang_grades WHERE gang_name=@gang_name AND grade=@grade";
            cmd.Parameters.AddWithValue("@gang_name", gangName);
            cmd.Parameters.AddWithValue("@grade", grade);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                
                string name = reader.GetString("name");
                string label = reader.GetString("label");
                string skinMale = reader.GetString("skin_male");
                string skinFemale = reader.GetString("skin_female");
                
                object[] backArray = new object[] { name, label, skinMale, skinFemale};
                
                reader.Close();
                
                return backArray;
            }
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());

            return null;
        }
    }
}