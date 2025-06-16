using System;
using System.Text.Json;
using AltV.Net;
using tr_world.Models;
using tr_world.Player;

namespace tr_world.Controllers;

public class TPlayerController : IScript
{
    private const string UpdateString =
        "UPDATE users SET discordid=@discordid, name=@name, `permissionlevel`=@permissionlevel, fname=@fname, lname=@lname, bank_money=@bank_money, cash_money=@cash_money, sex=@sex, height=@height, skin=@skin, status=@status, position=@position, metadata=@metadata, inventory=@inventory, backstory=@backstory, disabled=@disabled, main_property=@main_property, job=@job, jobgrade=@jobgrade, gang=@gang, ganggrade=@ganggrade, phone_number=@phone_number, phone=@phone, iban=@iban, callsign=@callsign WHERE discordid=@discordid LIMIT 1";

    public static void LoadTPlayerData(TPlayer player)
    {
        // try & catch function

        try
        {
            var cmd = Databank.Connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM users WHERE discordid=@discordid LIMIT 1";

            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);

            using (var reader = cmd.ExecuteReader())
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

                    player.sInventory = reader.GetString("inventory");

                    player.Backstory = reader.GetString("backstory");


                    var jsonMetadata = reader.GetString("metadata");
                    player.Metadata = JsonSerializer.Deserialize<TMetadata>(jsonMetadata);

                    var disabled = reader.GetBoolean("disabled");

                    // Jobs
                    var jobname = reader.GetString("job");
                    int jobgrade = reader.GetUInt16("jobgrade");
                    // gang
                    var gangname = reader.GetString("gang");
                    int ganggrade = reader.GetUInt16("ganggrade");


                    //player.VehKeysID = reader.GetString("vehkeysid",);

                    player.Skin = reader.GetString("skin");

                    player.Phone.Number = reader.GetInt32("phone_number");
                    var jsonPhone = reader.GetString("phone");
                    player.Phone = JsonSerializer.Deserialize<Phone>(jsonPhone);

                    //iban
                    //callsign

                    reader.Close();

                    //job 2
                    var jobobj = JobController.LoadJobDetailsFromDb(jobname);
                    var jobgradeobj = JobController.LoadJobGradeFromDb(jobname, jobgrade);


                    player.Job.Name = jobname;
                    player.Job.GradeLevel = (uint)jobgrade;
                    player.Job.Label = (string)jobobj[0];
                    player.Job.GradeName = (string)jobgradeobj[0];
                    player.Job.GradeLabel = (string)jobgradeobj[1];
                    player.Job.Payment = (uint)jobgradeobj[2];
                    player.Job.SkinMale = (string)jobgradeobj[3];
                    player.Job.SkinFemale = (string)jobgradeobj[4];

                    // gang 2
                    var returnGangClass = GangController.LoadGangGradeFromDb(gangname, ganggrade);

                    player.Gang.Name = gangname;
                    player.Gang.GradeLevel = (uint)ganggrade;
                    player.Gang.Label = GangController.LoadGangDetailsFromDb(gangname);
                    player.Gang.GradeName = returnGangClass.Name;
                    player.Gang.GradeLabel = returnGangClass.Label;
                    player.Gang.SkinMale = returnGangClass.SkinMale;
                    player.Gang.SkinFemale = returnGangClass.SkinFemale;


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

    /// <summary>
    ///     Save the player-data
    /// </summary>
    /// <param name="player">the player</param>
    public static void SaveTPlayerData(TPlayer player)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();
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
            var jsonPosition = JsonSerializer.Serialize(player.Position.Normalized);
            cmd.Parameters.AddWithValue("@position", jsonPosition);
            player.Metadata.LastUpdate = DateTime.Now;
            var jsonMetadata = JsonSerializer.Serialize(player.Metadata);
            cmd.Parameters.AddWithValue("@backstory", jsonMetadata);
            cmd.Parameters.AddWithValue("@inventory", player.sInventory);
            cmd.Parameters.AddWithValue("@backstory", player.Backstory);
            cmd.Parameters.AddWithValue("@job", player.Job.Name);
            cmd.Parameters.AddWithValue("@jobgrade", player.Job.GradeLevel);
            cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
            cmd.Parameters.AddWithValue("@ganggrade", player.Gang.GradeLevel);
            cmd.Parameters.AddWithValue("@phone_number", player.Phone.Number);
            var jsonPhone = JsonSerializer.Serialize(player.Phone);
            cmd.Parameters.AddWithValue("@phone", jsonPhone);


            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }
    }

    public static void CreateTPlayerAccount(TPlayer player)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();
            cmd.CommandText =
                "INSERT INTO users (discordid, name, `permissionlevel`, fname, lname, cash_money, bank_money, sex, height, skin, status, position, metadata, inventory, backstory, job, jobgrade, gang, ganggrade, phone_number, phone) VALUES (@discordid, @name, @permissionlevel, @fname, @lname, @cash_money, @bank_money, @sex, @height, @skin, @status, @position, @metadata, @inventory, @backstory, @job, @jobgrade, @gang, @ganggrade, @phone_number, @phone)";

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
            // Serialize position as a JSON object
            var jsonPosition =
                JsonSerializer.Serialize(new { player.Position.X, player.Position.Y, player.Position.Z });
            cmd.Parameters.AddWithValue("@position", jsonPosition);

            // Serialize metadata and phone as JSON
            var jsonMetadata = JsonSerializer.Serialize(player.Metadata);
            cmd.Parameters.AddWithValue("@metadata", jsonMetadata);

            var jsonPhone = JsonSerializer.Serialize(player.Phone);
            cmd.Parameters.AddWithValue("@phone", jsonPhone);

            cmd.Parameters.AddWithValue("@inventory", player.sInventory);
            cmd.Parameters.AddWithValue("@backstory", player.Backstory);
            cmd.Parameters.AddWithValue("@job", player.Job.Name);
            cmd.Parameters.AddWithValue("@jobgrade", player.Job.GradeLevel);
            cmd.Parameters.AddWithValue("@gang", player.Gang.Name);
            cmd.Parameters.AddWithValue("@ganggrade", player.Gang.GradeLevel);
            cmd.Parameters.AddWithValue("@phone_number", player.Phone.Number);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
        }
    }

    public static bool HasTPlayerAccount(TPlayer player)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE discordid=@discordid LIMIT 1";
            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);

            using (var reader = cmd.ExecuteReader())
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

    public static bool IsPlayerBanned(TPlayer player)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM ban_list WHERE discordid=@discordid LIMIT 1";
            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }

                player.Kick("You are already banned. Reason was: " + reader.GetString("reason"));
                reader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
            player.Kick("Internal Error in system");

            return false;
        }
    }

    public static void AddPlayerToBanList(TPlayer player, string reason)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO ban_list (discordid, reason) VALUES (@discordid, @reason)";
            cmd.Parameters.AddWithValue("@discordid", player.DiscordId);
            cmd.Parameters.AddWithValue("@reason", reason);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());
            player.Kick("Internal Error in system.");
        }
    }

//        public static void 
}