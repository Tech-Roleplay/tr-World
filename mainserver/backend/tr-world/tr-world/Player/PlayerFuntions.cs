using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using tr_world.Controllers;
using tr_world.Models;
using tr_world.Player;

namespace tr_world.Player
{
    public static class PlayerFuntions
    {
        private static Random random = new Random();

        #region Money functions

        /// <summary>
        /// Adding Money to the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to add?</param>
        /// <param name="reason">Why are you adding Money?</param>
        public static void AddMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.CashBalance;

            // Temporäres Geld mit amount addieren
            TempMoney += amount;

            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Adding Money to the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to add?</param>
        /// <param name="reason">Why are you adding Money?</param>
        public static void AddMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.BankBalance;

            // Temporäres Geld mit amount addieren
            TempMoney += amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Subtract Money from the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to subtract?</param>
        /// <param name="reason">Why are you subtracting Money?</param>
        public static void SubMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.CashBalance;

            // Temporäres Geld mit amount subtrahieren, mit Schutz
            if (TempMoney >= amount && TempMoney > 0)
            {
                TempMoney -= amount;
            }


            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Subtract Money from the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to subtract?</param>
        /// <param name="reason">Why are you subtracting Money?</param>
        public static void SubMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.BankBalance;

            // Temporäres Geld mit amount subtrahieren, ohne Schutz
            TempMoney -= amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Set Money for the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to set?</param>
        /// <param name="reason">Why are you setting Money?</param>
        public static void SetMoneyToCash(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.CashBalance;

            // Temporäres Geld mit amount gesetzt
            TempMoney = amount;

            // Temporäres Geld dem Spieler Aufladen
            player.CashBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Set Money for the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="amount">How much to set?</param>
        /// <param name="reason">Why are you setting Money?</param>
        public static void SetMoneyToBank(this BPlayer player, int amount, string reason)
        {
            // Temporäres Geld erstellen
            var TempMoney = player.BankBalance;

            // Temporäres Geld mit amount gesetzt
            TempMoney = amount;

            // Temporäres Geld dem Spieler Aufladen
            player.BankBalance = TempMoney;

            // Log erstellen des Transfers
            // ND
        }

        /// <summary>
        /// Return the Player's Cash Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <returns>A Int of the Player's Cash Balance</returns>
        public static int GetMoneyFromCash(this BPlayer player)
        {
            return player.CashBalance;
        }

        /// <summary>
        /// Return the Player's Bank Balance.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <returns>A Int of the Player's Bank Balance</returns>
        public static int GetMoneyFromBank(this BPlayer player)
        {
            return player.BankBalance;
        }




        #endregion

        // all from job
        #region job functions

        /// <summary>
        /// Set Job for Player.
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <param name="jobname">Name of the Job</param>
        /// <param name="jobgrade">Grade Level of the Job</param>
        /// <example><code>SetJob("police", 4)</code></example>
        public static void SetJob(this BPlayer player, string jobname, int jobgrade)
        {
            // Jobs
           
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
        }
        
        /// <summary>
        /// Change the state of the Player's JobDuty
        /// </summary>
        /// <param name="player">The target player.</param>
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
        /// <param name="player">The target player.</param>
        public static void PaymentJob(this BPlayer player)
        {
            AddMoneyToBank(player, (int)player.Job.Payment, $"Salary by {player.Job.Label}");
        }

        /// <summary>
        /// Reset Job for Player.
        /// </summary>
        /// <param name="player">The target player.</param>
        public static void ResetJob(this BPlayer player)
        {
            player.SetJob("unemployed", 0);
        }

        //gets
        /// <summary>
        /// Gets if the player isBoss
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <returns>boolean of the isBoss State</returns>
        public static bool IsPlayerBoss(this BPlayer player)
        {
            return player.Job.IsBoss;
        }

        /// <summary>
        /// Gets the players Job Type
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <returns>string of the type</returns>
        public static string GetPlayerJobType(this BPlayer player)
        {
            return player.Job.Type;
        }

        /// <summary>
        /// TEMP: Gets the Skin for the player
        /// </summary>
        /// <param name="player">The target player.</param>
        /// <returns>string of the players skin</returns>
        public static string GetSkinForJob(this BPlayer player)
        {
            if (player.Sex == 'm')
            {
                return player.Job.Skin_Male;
            } 
            else if (player.Sex == 'f')
            {
                return player.Job.Skin_Female;
            }
            else
            {
                return string.Empty;
            }

        }


        #endregion
        // all from 

        #region gang functions

        #endregion

        // Metadata
        #region Metadata
        /// <summary>
        /// Gets if player is cuffed
        /// </summary>
        /// <param name="player">The target player</param>
        /// <returns>Cuffed-State for the player</returns>
        public static bool GetIsPlayerCuffed(this BPlayer player)
        {
            return player.Metadata.IsCuffed;
        }

        /// <summary>
        /// Sets 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="value"></param>
        public static void SetIsPlayerCuffed(this BPlayer player, bool value)
        {
            player.Metadata.IsCuffed = value;
        }

        public static bool GetIsPlayerInPrison(this BPlayer player)
        {
            return player.Metadata.IsInPrison;
        }

        public static void SetIsPlayerInPrison(this BPlayer player, bool value)
        {
            player.Metadata.IsInPrison = value;
        }

        public static bool GetIsPlayerDead(this BPlayer player)
        {
            return player.Metadata.IsPlyDead;
        }

        public static void SetIsPlayerDead(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyDead = value;
        }

        public static bool GetISPlayerDown(this BPlayer player)
        {
            return player.Metadata.IsPlyDown;
        }

        public static void SetISPlayerDown(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyDown = value;    
        }

        public static bool GetIsPlayerHeadshotted(this BPlayer player)
        {
            return player.Metadata.IsPlyHeadshotted;
        }

        public static void SetIsPlayerHeadshotted(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyHeadshotted = value;
        }

        public static bool GetIsPlayerLogout(this BPlayer player)
        {
            return player.Metadata.IsPlyLogout;
        }

        public static void SetIsPlayerLogout(this BPlayer player, bool value)
        {
            player.Metadata.IsPlyLogout = value;
        }

        public static void SetHunger(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Hunger = value;
        }

        public static float GetHunger(this BPlayer player)
        {
            return player.Metadata.Hunger;
        }

        public static void SetThirst(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Thirst = value;
        }

        public static float GetThirst(this BPlayer player)
        {
            return player.Metadata.Thirst;
        }

        public static void SetArmor(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Armor = value;
        }

        public static float GetArmor(this BPlayer player)
        {
            return player.Metadata.Armor;
        }

        public static void SetStress(this BPlayer player, float value)
        {
            if (value is <= 100f and >= 0f) return;
            player.Metadata.Stress = value;
        }

        public static float GetStress(this BPlayer player)
        {
            return player.Metadata.Stress;
        }

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
    }
}
