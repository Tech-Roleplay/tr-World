using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using tr_world.Controllers;
using tr_world.Player;

namespace tr_world.Player
{
    public static class PlayerFuntions
    {


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
        /// <param name="reason">Why are you subtract Money?</param>
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
        /// <param name="reason">Why are you subtract Money?</param>
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
           
            object[] jobobj = JobController.LoadJobDetailsFromDB(jobname);
            object[] jobgradeobj = JobController.LoadJobGradeFromDB(jobname, jobgrade);

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
        /// Reset Job for Player.
        /// </summary>
        /// <param name="player">The target player.</param>
        public static void ResetJob(this BPlayer player)
        {
            player.SetJob("unemployed", 0);
        }
        #endregion
        // all from 

        #region gang functions

        #endregion


    }
}
