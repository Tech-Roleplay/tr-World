using AltV.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net.Resources.Chat.Api;
using AltV.Net.Elements.Entities;
using System.Data;
using tr_world.Player;

namespace tr_world.Controllers
{
    public class BPlayerController : IScript
    {
        public static void LoadBPlayerData(BPlayer player, string CharId)
        {
            // try & catch function

            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM users WHERE charid=@charid LIMIT 1";

                cmd.Parameters.AddWithValue("@charid", CharId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        //player.DiscordId;

                        player.CharId = reader.GetString("charid");
                        //player.SetMetaData("user:discordid", reader.GetInt64("discordid"));

                        player.Group = reader.GetString("group");

                        player.Firstname = reader.GetString("fname");
                        player.Surname = reader.GetString("lname");

                        player.CashBalance = reader.GetInt16("cash_money");
                        player.BankBalance = reader.GetInt16("bank_money");

                        player.Sex = reader.GetChar("sex");
                        player.Height = reader.GetByte("height");

                        player.Inventory = reader.GetString("inventory");

                        player.Backstory = reader.GetString("backstory");

                        // Booleans
                        player.IsPlyDead = reader.GetBoolean("is_dead");
                        player.IsPlyDown = reader.GetBoolean("is_downed");
                        player.IsPlyHeadshotted = reader.GetBoolean("is_headshotted");
                        player.IsPlyLogout = reader.GetBoolean("is_logout");
                        player.IsInPrison = reader.GetBoolean("is_inprision");
                        bool disabled = reader.GetBoolean("disabled");

                        // Jobs
                        string jobname = reader.GetString("job");

                        JobController.LoadJobDetailsFromDB(jobname);

                        player.Job.Grade_level = reader.GetUInt16("jobgrade");
                        player.Job.Name = jobname;
                        

                        








                        //player.SetMetaData("user:skin", reader.GetString("skin"));
                        //player.SetMetaData("user:status", reader.GetString("status"));
                        //player.SetMetaData("user:position", reader.GetString("position"));
                        //player.SetMetaData("user:metadata", reader.GetString("metadata"));
                        //player.SetMetaData("user:inventory", reader.GetString("inventory"));
                        //player.SetMetaData("user:backstory", reader.GetString("backstory"));
                        //player.SetMetaData("user:is_dead", reader.GetBoolean("is_dead"));
                        //player.SetMetaData("user:is_downed", reader.GetBoolean("is_downed"));
                        //player.SetMetaData("user:is_cuffed", reader.GetBoolean("is_cuffed"));
                        //player.SetMetaData("user:is_inprision", reader.GetBoolean("is_inprision"));
                        //player.SetMetaData("user:disabled", reader.GetBoolean("disabled"));
                        //player.SetMetaData("user:main_property", reader.GetString("main_property"));
                        //player.SetMetaData("user:created", reader.GetDateTime("created"));
                        //player.SetMetaData("user:last_seen", reader.GetDateTime("last_seen"));
                        //player.SetMetaData("user:job", reader.GetString("job"));
                        //player.SetMetaData("user:jobgrade", reader.GetInt16("jobgrade"));
                        //player.SetMetaData("user:gang", reader.GetString("gang"));
                        //player.SetMetaData("user:ganggrade", reader.GetInt16("ganggrade"));
                        //player.SetMetaData("user:phone_number", reader.GetInt64("phone_number"));
                        //player.SetMetaData("user:phone_pic_url", reader.GetString("phone_pic_url"));
                        //player.SetMetaData("user:phone_background", reader.GetString("phone_background"));
                        //player.SetMetaData("user:iban", reader.GetString("iban"));
                        //player.SetMetaData("user:callsign", reader.GetString("callsign"));

                        Alt.Log($"Sucessfully loading playerdata for (${player.Name})!");
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
        public static void SaveBPlayerData()
        {

        }
        public static void UpdateBPlayerData()
        {

        }

    }
}
