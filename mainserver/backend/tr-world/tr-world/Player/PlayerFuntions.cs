using System;
using System.Linq;
using System.Runtime.Serialization;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using tr_world.Controllers;
using tr_world.Models;
using tr_world.Vehicle;

// TODO: Adding ban/freez func

namespace tr_world.Player
{
    public static class PlayerFuntions
    {
        private static Random random = new Random();

        /// <summary>
        /// Checks if the player haves the permission to do something
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="RequiredPermission">the minimun, use (int)TPermission.[...]</param>
        /// <returns>if true then return true, if false then return false and send a chat message</returns>
        public static bool HasPlayerPermission(this BPlayer player, int RequiredPermission)
        {
            if (player.Permission >= RequiredPermission)
            {
                return true;
            }
            player.SendChatMessage("{FF0000}You do not have the permission to use this command.");
            return false;
        }
        
        /// <summary>
        /// Search the nearest player to the player
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="distance">a max distance between both, standard is 5</param>
        /// <returns>Gives player back (in Form of BPlayer)</returns>
        public static BPlayer GetClosestPlayer(this BPlayer player, float distance = 5.0f)
        {
            BPlayer tPlayer = null;
            foreach (BPlayer ply in Alt.GetAllPlayers())
            {
                AltV.Net.Data.Position plyPos = ply.Position;
                float plyDist = player.Position.Distance(plyPos);
                if (plyDist < distance && player.Dimension == ply.Dimension)
                {
                    distance = plyDist;
                    tPlayer = ply;
                }
            }
            return tPlayer;
        }
        
        /// <summary>
        /// Search the nearest vehicle to the player 
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="distance">a max distance between both, a standard is 5</param>
        /// <returns>Gives vehicle back (in Form of BVehicle)</returns>
        public static BVehicle GetClosestVehicle(this BPlayer player, float distance = 5.0f)
        {
            BVehicle tVehicle = null;
            foreach (BVehicle veh in Alt.GetAllVehicles())
            {
                AltV.Net.Data.Position vehPos = veh.Position;
                float vehDist = player.Position.Distance(vehPos);
                if (vehDist < distance && player.Dimension == veh.Dimension)
                {
                    distance = vehDist;
                    tVehicle = veh;
                }
            }
            return tVehicle;
        }
        
        /// <summary>
        /// Search the nearest ped to the player
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="distance">a max distance between both, a standard is 5</param>
        /// <returns></returns>
        public static Ped GetClosestPed(this BPlayer player, float distance = 5.0f)
        {
            Ped tPed = null;
            foreach (Ped ped in Alt.GetAllPeds())
            {
                AltV.Net.Data.Position pedPos = ped.Position;
                float pedDist = player.Position.Distance(pedPos);
                if (pedDist < distance && player.Dimension == ped.Dimension)
                {
                    distance = pedDist;
                    tPed = ped;
                }
            }
            return tPed;
        }

        #region Adminstration
        /// <summary>
        /// freeze the player
        /// </summary>
        /// <param name="player">the player</param>
        public static void Freeze(this BPlayer player)
        {
            player.Emit("admin:player:freeze", !player.Frozen);
            

            //DCLog
        }

        /// <summary>
        /// gives the player a warning
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="reason">the reason why the player is doing this</param>
        public static void Warn(this BPlayer player, string reason)
        {
            player.Emit("admin:player:warn", reason);

            // DCLog
        }

        /// <summary>
        /// kicks the player from the server
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="reason">the reason why the player is doing this</param>
        public static void Kicking(this BPlayer player, string reason)
        {
            player.Kick(reason);

            //DCLog
        }

        public static void TempBan(this BPlayer player, int duration, string timeformat, string reason)
        {
            
        }
        

        /// <summary>
        /// Ban the playes from the server
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="reason">the reason why the player is doing this</param>
        public static void Ban(this BPlayer player, string reason)
        {
            // BanFunc
            BPlayerController.AddPlayerToBanList(player, reason);
            player.Kick("Ban: " + reason);

            //DCLog
        }
        #endregion

        #region Money functions

        /// <summary>
        /// Adding Money to the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The amount how much to add</param>
        /// <param name="reason">The reason why,e.g. Payment or Income by ...</param>
        public static void AddMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var tempMoney = player.CashBalance;

            // Temporäres Geld mit amount addieren
            tempMoney += amount;

            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Adding Money to the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The amount how much to add</param>
        /// <param name="reason">The reason why,e.g. Payment or Income by ...</param>
        public static void AddMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var tempMoney = player.BankBalance;

            // Temporäres Geld mit amount addieren
            tempMoney += amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Subtract Money from the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The amount how much to subtract</param>
        /// <param name="reason">The reason why,e.g. paying the rent or paying bills ...</param>
        public static void SubMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var tempMoney = player.CashBalance;

            // Temporäres Geld mit amount subtrahieren, mit Schutz
            if (tempMoney >= amount && tempMoney > 0)
            {
                tempMoney -= amount;
            }

            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Subtract Money from the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The amount how much to subtract</param>
        /// <param name="reason">The reason why,e.g. paying the rent or paying bills ...</param>
        public static void SubMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            int tempMoney = player.BankBalance;

            // Temporäres Geld mit amount subtrahieren, ohne Schutz
            tempMoney -= amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Set Money for the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The value how much a player money have</param>
        /// <param name="reason">The reason why,e.g. setting money back after cheating or after spwan</param>
        public static void SetMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            int tempMoney;

            // Temporäres Geld mit amount gesetzt
            tempMoney = amount;

            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Set Money for the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="amount">The value how much a player money have</param>
        /// <param name="reason">The reason why,e.g. setting money back after cheating or after spwan</param>
        public static void SetMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            int tempMoney;

            // Temporäres Geld mit amount gesetzt
            tempMoney = amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = tempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Return the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>A Int of the Player's Cash Balance</returns>
        public static int GetMoneyFromCash(this BPlayer player)
        {
            return player.CashBalance;
        }

        /// <summary>
        /// Return the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>A Int of the Player's Bank Balance</returns>
        public static int GetMoneyFromBank(this BPlayer player)
        {
            return player.BankBalance;
        }




        #endregion

        // all from job
        #region job functions

        /// <summary>
        /// Set the Job for Player.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="jobname">Name(handling-name) of the Job</param>
        /// <param name="jobgrade">Grade Level of the Job</param>
        /// <example><code lang="cs">SetJob("police", 4)</code></example>
        public static void SetJob(this BPlayer player, string jobname, int jobgrade)
        {
            // Jobs
           
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
        }
        
        /// <summary>
        /// Change the state of the Player's JobDuty
        /// </summary>
        /// <param name="player">The player</param>
        public static void ChangeDuty(this BPlayer player)
        {
            if (player != null)
            {
                switch (player.Job.OnDuty)
                {
                    case true:
                        player.Job.OnDuty = false;
                        break;
                    case false:
                        player.Job.OnDuty = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Pays the Player for his work
        /// </summary>
        /// <param name="player">The player</param>
        public static void PaymentJob(this BPlayer player)
        {
            AddMoneyToBank(player, (int)player.Job.Payment, $"Salary by {player.Job.Label}");
        }

        /// <summary>
        /// Reset Job for Player.
        /// </summary>
        /// <param name="player">The player</param>
        public static void ResetJob(this BPlayer player)
        {
            player.SetJob("unemployed", 0);
        }

        //gets
        /// <summary>
        /// Gets if the player isBoss
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>boolean of the isBoss State</returns>
        public static bool IsPlayerBoss(this BPlayer player)
        {
            return player.Job.IsBoss;
        }

        /// <summary>
        /// Gets the players Job Type
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>string of the type</returns>
        public static string GetPlayerJobType(this BPlayer player)
        {
            return player.Job.Type;
        }

        /// <summary>
        /// TEMP: Gets the Skin for the player
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>string of the players skin</returns>
        public static string GetSkinForJob(this BPlayer player)
        {
            if (player.Sex == 'm')
            {
                return player.Job.SkinMale;
            } 
            else if (player.Sex == 'f')
            {
                return player.Job.SkinFemale;
            }
            else
            {
                return string.Empty;
            }

        }


        #endregion
        // all from 

        #region gang functions

        public static void SetGang(this BPlayer player, string gangname, int ganggrade)
        {
            string gangObject = GangController.LoadGangDetailsFromDb(gangname);
            ReturnGangClass returnGangClass = GangController.LoadGangGradeFromDb(gangname, ganggrade);

            player.Gang.Name = gangname;
            player.Gang.GradeLevel = (uint)ganggrade;
            player.Gang.Label = gangObject;
            player.Gang.GradeName = returnGangClass.Name;
            player.Gang.GradeLabel = returnGangClass.Label;
            player.Gang.SkinMale = returnGangClass.SkinMale;
            player.Gang.SkinFemale = returnGangClass.SkinFemale;
        }
        #endregion

        // Metadata
        #region Metadata
        /// <summary>
        /// Gets if player is cuffed
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>Cuffed-State for the player</returns>
        public static bool GetIsPlayerCuffed(this BPlayer player)
        {
            return player.Metadata.IsCuffed;
        }

        /// <summary>
        /// Set the cuffed player state
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">Is cuffed or not.</param>
        public static void SetIsPlayerCuffed(this BPlayer player, bool value)
        {
            player.Metadata.IsCuffed = value;
        }

        /// <summary>
        /// Get the value of the player is prison
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>bool of of in prision</returns>
        public static bool GetIsPlayerInPrison(this BPlayer player)
        {
            return player.Metadata.IsInPrison;
        }

        /// <summary>
        /// Set the player into prision
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">state to be in prision</param>
        public static void SetIsPlayerInPrison(this BPlayer player, bool value)
        {
            player.Metadata.IsInPrison = value;
        }

        /// <summary>
        /// Get if the player is in prision
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>bool a palyer is dead</returns>
        public static bool GetIsPlayerDead(this BPlayer player)
        {
            return player.Metadata.IsPlyDead;
        }

        /// <summary>
        /// Set is the player is dead
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">state of death</param>
        public static void SetIsPlayerDead(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyDead = value;
        }

        /// <summary>
        /// Gets the state of the player is onGround
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>bool a player is down</returns>
        public static bool GetIsPlayerDown(this BPlayer player)
        {
            return player.Metadata.IsPlyDown;
        }

        /// <summary>
        /// Sets is the player onGround
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">state of ground</param>
        public static void SetIsPlayerDown(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyDown = value;    
        }

        /// <summary>
        /// Gets the state of the player is Headshotted
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>bool, a player is headshotted</returns>
        public static bool GetIsPlayerHeadshotted(this BPlayer player)
        {
            return player.Metadata.IsPlyHeadshotted;
        }

        /// <summary>
        /// Sets is the player headshotted
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">state of the player is headshotted</param>
        public static void SetIsPlayerHeadshotted(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyHeadshotted = value;
        }

        /// <summary>
        /// Gets the state of the player is logout
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>bool, a player is logout</returns>
        public static bool GetIsPlayerLogout(this BPlayer player)
        {
            return player.Metadata.IsPlyLogout;
        }

        /// <summary>
        /// Sets the player Logout state
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">state of the player's logout</param>
        public static void SetIsPlayerLogout(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyLogout = value;
        }

        /// <summary>
        /// Sets the hunger state
        /// </summary> 
        /// <param name="player">the  player</param>
        /// <param name="value">sets the hunger state. between 0.0 and 100.0</param>
        public static void SetHunger(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Hunger = value;
        }

        /// <summary>
        /// Get the state of the hunger
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>floating the hunger state</returns>
        public static float GetHunger(this BPlayer player)
        {
            return player.Metadata.Hunger;
        }

        /// <summary>
        /// Sets the thirst state
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">sets the thirst state. It is between 0.0 and 100.0</param>
        public static void SetThirst(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Thirst = value;
        }

        /// <summary>
        /// Get the state of the thirst
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>floating the hunger state</returns>
        public static float GetThirst(this BPlayer player)
        {
            return player.Metadata.Thirst;
        }

        /// <summary>
        /// Sets the armor state
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">sets the armor state. It is between 0.0 and 100.0</param>
        public static void SetArmor(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Armor = value;
        }

        /// <summary>
        /// Gets the state of the armor
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>floating the armor state</returns>
        public static float GetArmor(this BPlayer player)
        {
            return player.Metadata.Armor;
        }

        /// <summary>
        /// Sets the stress state
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="value">sets the stress state. It is between 0.0 and 100.0</param>
        public static void SetStress(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Stress = value;
        }

        /// <summary>
        /// Gets the stress state
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>floating the stress state</returns>
        public static float GetStress(this BPlayer player)
        {
            return player.Metadata.Stress;
        }

        /// <summary>
        /// Sets the jailtime
        /// </summary>
        /// <param name="player"></param>
        /// <param name="value"></param>
        public static void SetJailTime(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.JailTime = value;
        }

        public static float GetJailTime(this BPlayer player)
        {
            return player.Metadata.JailTime;
        }

        public static void UpdateLastUpdate(this BPlayer player)
        {
            player.Metadata.LastUpdate = DateTime.Now;
        }

        public static DateTime GetLastUpdate(this BPlayer player)
        {
            return player.Metadata.LastUpdate;
        }

        public static void CreateCreateDate(this BPlayer player)
        {
            player.Metadata.CreateDate = DateTime.Now;
        }

        public static DateTime GetCreateDate(this BPlayer player)
        {
            return player.Metadata.CreateDate;
        }
        
        #endregion
        
        
        
        #region Skin

        public static void SetCloth(this BPlayer player, ComponentIDs componet, int drawable, int texture,
            int? pallette)
        {
            
        }

        public static string GetCloth(this BPlayer player)
        {
            //temp return "Cloth";
            return "Cloth";
        }
        
        public static void SetDlcCloth(this BPlayer player, int dlc, ComponentIDs componet, int drawable, int texture,
            int? pallette)
        {
            
        }

        #endregion

        [Obsolete]
        public static string GetRandomCharid(this BPlayer player)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var finalString = (Enumerable.Repeat(chars, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            string result = finalString + "-" + random.Next(101, 999);
            return result;
        }
    }
}
