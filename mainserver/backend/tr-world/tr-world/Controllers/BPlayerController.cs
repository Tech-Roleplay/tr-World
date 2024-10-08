﻿using AltV.Net;
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

        public static void LoadBPlayerData(BPlayer player)
        {
            // try & catch function
            
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM users WHERE discordid=@discordid LIMIT 1";

                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        //player.DiscordId;

                        //player.SetMetaData("user:discordid", reader.GetInt64("discordid"));
                        //player.Name = reader.GetString("name");
                        player.Permission = reader.GetInt32("permissionlevel");

                        player.Firstname = reader.GetString("fname");
                        player.Surname = reader.GetString("lname");

                        player.CashBalance = reader.GetInt32("cash_money");
                        player.BankBalance = reader.GetInt32("bank_money");

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
                        // gang
                        string gangname = reader.GetString("gang");
                        int ganggrade = reader.GetUInt16("ganggrade");
                        
                        
                        
                        
                        
                        
                        //player.VehKeysID = reader.GetString("vehkeysid",);
                        
                        player.Skin = reader.GetString("skin");
                        
                        player.Phone.Number = reader.GetInt32("phone_number");
                        string jsonPhone = reader.GetString("phone");
                        player.Phone = JsonSerializer.Deserialize<Phone>(jsonPhone);
                        
                        //iban
                        //callsign

                        reader.Close();
                        
                        //job 2
                        object[] jobobj = JobController.LoadJobDetailsFromDb(jobname);
                        object[] jobgradeobj = JobController.LoadJobGradeFromDb(jobname, jobgrade);
                        
                        
                        player.Job.Name = jobname;
                        player.Job.GradeLevel = (uint)jobgrade;
                        player.Job.Label = (string)jobobj[0];
                        player.Job.GradeName = (string)jobgradeobj[0];
                        player.Job.GradeLabel = (string)jobgradeobj[1];
                        player.Job.Payment = (uint)jobgradeobj[2];
                        player.Job.SkinMale = (string)jobgradeobj[3];
                        player.Job.SkinFemale = (string)jobgradeobj[4];
                        
                        // gang 2
                        object[] gangObjects = GangController.LoadGangDetailsFromDb(gangname);
                        object[] gangGradeObjects = GangController.LoadGangGradeFromDb(gangname, ganggrade);
                        
                        player.Gang.Name = gangname;
                        player.Gang.GradeLevel = (uint)ganggrade;
                        player.Gang.Label = (string)gangObjects[0];
                        player.Gang.GradeName = (string)gangGradeObjects[0];
                        player.Gang.GradeLabel = (string)gangGradeObjects[1];
                        player.Gang.SkinMale = (string)gangGradeObjects[2];
                        player.Gang.SkinFemale = (string)gangGradeObjects[3];
                        
                        
                        Alt.Log($"Sucessfully loading playerdata for (${player.Name})!");
                    }
                }

            }
            catch (Exception e)
            {
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());
            }

        }

        private const string UpdateString = "UPDATE users SET discordid=@discordid, name=@name, `permissionlevel`=@permissionlevel, fname=@fname, lname=@lname, bank_money=@bank_money, cash_money=@cash_money, sex=@sex, height=@height, skin=@skin, status=@status, position=@position, metadata=@metadata, inventory=@inventory, backstory=@backstory, disabled=@disabled, main_property=@main_property, job=@job, jobgrade=@jobgrade, gang=@gang, ganggrade=@ganggrade, phone_number=@phone_number, phone=@phone, iban=@iban, callsign=@callsign WHERE discordid=@discordid LIMIT 1";
        /// <summary>
        /// Save the player-data
        /// </summary>
        /// <param name="player">the target player</param>
        public static void SaveBPlayerData(BPlayer player)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = UpdateString;
                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
                cmd.Parameters.AddWithValue("@name", player.Name);
                cmd.Parameters.AddWithValue("@permissionlevel", player.Permission);
                cmd.Parameters.AddWithValue("@fname", player.Firstname);
                cmd.Parameters.AddWithValue("@lname", player.Surname);
                cmd.Parameters.AddWithValue("@cash_money", player.CashBalance);
                cmd.Parameters.AddWithValue("@bank_money", player.BankBalance);
                cmd.Parameters.AddWithValue("@sex", player.Sex);
                cmd.Parameters.AddWithValue("@height", player.Height);
                cmd.Parameters.AddWithValue("@skin", player.Skin);
//              cmd.Parameters.AddWithValue("@status", player.Status);
                string jsonPosition = JsonSerializer.Serialize(player.Position.Normalized);
                cmd.Parameters.AddWithValue("@position", jsonPosition);
                player.Metadata.LastUpdate = DateTime.Now;
                string jsonMetadata = JsonSerializer.Serialize(player.Metadata);
                cmd.Parameters.AddWithValue("@backstory", jsonMetadata);
                cmd.Parameters.AddWithValue("@inventory", player.Inventory);
                cmd.Parameters.AddWithValue("@backstory", player.Backstory);
                cmd.Parameters.AddWithValue("@job", player.Job.Name);
                cmd.Parameters.AddWithValue("@jobgrade", player.Job.GradeLevel);
                cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
                cmd.Parameters.AddWithValue("@ganggrade", player.Gang.GradeLevel);
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

        public static void CreateBPlayerAccount(BPlayer player)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO users (discordid, name, `permissionlevel`, fname, lname, cash_money, bank_money, sex, height, skin, status, position, metadata, inventory, backstory, job, jobgrade, gang, ganggrade, phone_number, phone) VALUES (@discordid, @name, @permissionlevel, @fname, @lname, @cash_money, @bank_money, @sex, @height, @skin, @status, @position, @metadata, @inventory, @backstory, @job, @jobgrade, @gang, @ganggrade, @phone_number, @phone)";
                
                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
                cmd.Parameters.AddWithValue("@name", player.Name);
                cmd.Parameters.AddWithValue("@permissionlevel", player.Permission);
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
                cmd.Parameters.AddWithValue("@jobgrade", player.Job.GradeLevel);
                cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
                cmd.Parameters.AddWithValue("@ganggrade", player.Gang.GradeLevel);
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

        public static bool HasBPlayerAccount(BPlayer player)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE discordid=@discordid LIMIT 1";
                cmd.Parameters.AddWithValue("@discordid", player.DiscordId);

                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    reader.Close();
                    return false;
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
