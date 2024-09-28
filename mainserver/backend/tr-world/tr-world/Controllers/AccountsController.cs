using System;
using System.Reflection.Metadata;
using System.Text.Json;
using AltV.Net;
using MySqlConnector;
using tr_world.Player;

namespace tr_world;

public class AccountsController : IScript
{
    #region Load Function
    /// <summary>
    /// Loads the players Account from the "accounts"-DB.
    /// </summary>
    /// <param name="player">The target player.</param>
    public static void LoadAccount(BPlayer player)
    {
        try
        {
            
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM accounts WHERE discordid=@discordid LIMIT 1";
            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
            
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string json = reader.GetString("charids");

                    CharList charList = JsonSerializer.Deserialize<CharList>(json);
                    
                    //account.CharList[player.CharId] = reader.GetString(1);

                    reader.Close();
                }
            }

        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }

    }
    #endregion

    #region Save Function
    /// <summary>
    /// Save the players Account into the "accounts"-DB.
    /// </summary>
    /// <param name="player">The target player.</param>
    public static void SaveAccount(BPlayer player)
    {
        try
        {
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "UPDATE accounts SET discordid=@discordid, name=@name, charids=@charids";

            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
            cmd.Parameters.AddWithValue("@name", player.Name);

            string json = JsonSerializer.Serialize(player.CharList);

            cmd.Parameters.AddWithValue("@charids", json);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }
    }
    #endregion

    #region Create Function
    /// <summary>
    /// Create the players Account in the "accounts"-DB.
    /// </summary>
    /// <param name="player">The target player.</param>
    public static void CreateAccount(BPlayer player) 
    {
        try
        {
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO accounts (discordid, name, charids) VALUES (@discordid, @name, @charids)";

            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
            cmd.Parameters.AddWithValue("@name", player.Name);

            string json = JsonSerializer.Serialize(player.CharList);

            cmd.Parameters.AddWithValue("@charids", json);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }
    }
    #endregion

    #region HasAccount Functions
    /// <summary>
    /// Check if the players Account is in the "accounts"-DB.
    /// </summary>
    /// <param name="player">The target player.</param>
    /// <returns>If yes the player have account, no the player haven't a account</returns>
    public static bool HasAccount(BPlayer player)
    {
        try
        {
            MySqlCommand cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM accounts WHERE discordid=@discordid LIMIT 1";
            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);

            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                } else {
                    reader.Close();
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
            return false;
        }
    }
    #endregion
}
