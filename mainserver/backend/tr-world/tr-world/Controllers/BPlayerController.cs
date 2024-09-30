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
using System.Text.Json;
using AltV.Net.Data;
using tr_world.Models;
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

                        player.Group = reader.GetString("pgroup");

                        player.Firstname = reader.GetString("fname");
                        player.Surname = reader.GetString("lname");

                        player.CashBalance = reader.GetInt16("cash_money");
                        player.BankBalance = reader.GetInt16("bank_money");

                        player.Sex = reader.GetChar("sex");
                        player.Height = reader.GetByte("height");

                        player.Inventory = reader.GetString("inventory");

                        player.Backstory = reader.GetString("backstory");

                        
                        string jsonMetadata = reader.GetString("metadata");
                        player.Metadata = JsonSerializer.Deserialize<TMetadata>(jsonMetadata);
                        
                        bool disabled = reader.GetBoolean("disabled");

                        // Jobs
                        string jobname = reader.GetString("job");
                        int jobgrade = reader.GetUInt16("jobgrade");

                        object[] jobobj = JobController.LoadJobDetailsFromDb(jobname);
                        object[] jobgradeobj = JobController.LoadJobGradeFromDb(jobname, jobgrade);

                        player.Job.Name = jobname;
                        player.Job.Grade_level = (uint)jobgrade;
                        player.Job.Label = (string)jobobj[0];
                        player.Job.Grade_name = (string)jobgradeobj[0];
                        player.Job.Grade_Label = (string)jobgradeobj[1];
                        player.Job.Payment = (uint)jobgradeobj[2];
                        player.Job.Skin_Male = (string)jobgradeobj[3];
                        player.Job.Skin_Female = (string)jobgradeobj[4];

                        // gang
                        
                        
                        player.Skin = reader.GetString("skin");
                        
                        player.Phone.Number = reader.GetInt32("phone_number");
                        string jsonPhone = reader.GetString("phone");
                        player.Phone = JsonSerializer.Deserialize<Phone>(jsonPhone);
                        
                        //iban
                        //callsign
                        

                        

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

        private const string UpdateString = "UPDATE users SET charid=@charid, discordid=@discordid, pgroup=@pgroup, fname=@fname, lname=@lname, bank_money=@bank_money, cash_money=@cash_money, sex=@sex, height=@height, skin=@skin, status=@status, position=@position, metadata=@metadata, inventory=@inventory, backstory=@backstory, is_dead=@is_dead, is_downed=@is_downed, is_cuffed=@is_cuffed, is_headshotted=@is_headshotted, is_logout=@is_logout, is_inprision=@is_inprision, disabled=@disabled, main_property=@main_property, created=@created, last_seen=@last_seen, job=@job, jobgrade=@jobgrade, gang=@gang, ganggrade=@ganggrade, phone_number=@phone_number, phone_pic_url=@phone_pic_url, phone=@phone, iban=@iban, callsign=@callsign WHERE charid=@charid";

        public static void SaveBPlayerData(BPlayer player)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = UpdateString;

                cmd.Parameters.AddWithValue("@charid", player.CharId);
                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
                cmd.Parameters.AddWithValue("@pgroup", player.Group);
                cmd.Parameters.AddWithValue("@fname", player.Firstname);
                cmd.Parameters.AddWithValue("@lname", player.Surname);
                cmd.Parameters.AddWithValue("@cash_money", player.CashBalance);
                cmd.Parameters.AddWithValue("@bank_money", player.BankBalance);
                cmd.Parameters.AddWithValue("@sex", player.Sex);
                cmd.Parameters.AddWithValue("@height", player.Height);
                cmd.Parameters.AddWithValue("@skin", player.Skin);
//              cmd.Parameters.AddWithValue("@status", player.Status);
                cmd.Parameters.AddWithValue("@position", JsonSerializer.Serialize<Position>(player.Position));
                string jsonMetadata = JsonSerializer.Serialize(player.Metadata);
                cmd.Parameters.AddWithValue("@backstory", jsonMetadata);
                cmd.Parameters.AddWithValue("@inventory", player.Inventory);
                cmd.Parameters.AddWithValue("@backstory", player.Backstory);
                cmd.Parameters.AddWithValue("@job", player.Job.Name);
                cmd.Parameters.AddWithValue("@jobgrade", player.Job.Grade_level);
                cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
                cmd.Parameters.AddWithValue("@ganggrade", player.Gang.Grade_level);
                cmd.Parameters.AddWithValue("@phone_number", player.Phone.Number);
                string jsonPhone = JsonSerializer.Serialize(player.Phone);
                cmd.Parameters.AddWithValue("@phone", jsonPhone);
                

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());
            }
        }

        public static void CreateBPlayerAccount(BPlayer player, string charId)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO users (charid, discordid, pgroup, fname, lname, cash_money, bank_money, sex, height, skin, status, position, metadata, inventory, backstory, job, jobgrade, gang, ganggrade, phone_number, phone) VALUES (@charid, @discordid, @pgroup, @fname, @lname, @cash_money, @bank_money, @sex, @height, @skin, @status, @position, @metadata, @inventory, @backstory, @job, @jobgrade, @gang, @ganggrade, @phone_number, @phone)";

                cmd.Parameters.AddWithValue("@charid", charId);
                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
                cmd.Parameters.AddWithValue("@pgroup", player.Group);
                cmd.Parameters.AddWithValue("@fname", player.Firstname);
                cmd.Parameters.AddWithValue("@lname", player.Surname);
                cmd.Parameters.AddWithValue("@cash_money", player.CashBalance);
                cmd.Parameters.AddWithValue("@bank_money", player.BankBalance);
                cmd.Parameters.AddWithValue("@sex", player.Sex);
                cmd.Parameters.AddWithValue("@height", player.Height);
                cmd.Parameters.AddWithValue("@skin", player.Skin);
                cmd.Parameters.AddWithValue("@status", "n/n");
                string jsonPostion = JsonSerializer.Serialize(player.Position.X + player.Position.Y + player.Position.Z);
                cmd.Parameters.AddWithValue("@position", jsonPostion);
                string jsonMetadata = JsonSerializer.Serialize(player.Metadata);
                cmd.Parameters.AddWithValue("@metadata", jsonMetadata);
                cmd.Parameters.AddWithValue("@inventory", player.Inventory);
                cmd.Parameters.AddWithValue("@backstory", player.Backstory);
                cmd.Parameters.AddWithValue("@job", player.Job.Name);
                cmd.Parameters.AddWithValue("@jobgrade", player.Job.Grade_level);
                cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
                cmd.Parameters.AddWithValue("@ganggrade", player.Gang.Grade_level);
                cmd.Parameters.AddWithValue("@phone_number", player.Phone.Number);
                string jsonPhone = JsonSerializer.Serialize(player.Phone);
                cmd.Parameters.AddWithValue("@phone", jsonPhone);    
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());
            }
        }

        public static bool HasBPlayerAccount(BPlayer player, string charId)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE charid=@charid LIMIT 1";
                cmd.Parameters.AddWithValue("@charid", charId);

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
        
    }
}
