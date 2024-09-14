using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Player;

namespace tr_world.Player
{
    public static class PlayerFuntions
    {


        #region Money functions


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






        // add function
        // add
        #endregion

        // all from job
        #region job functions
        public static void SetJob(this BPlayer player)
        {

        }
        public static void SetOnDuty(this BPlayer player)
        {

        }
        public static void ResetJob(this BPlayer player)
        {

        }
        #endregion
        // all from 

        #region gang functions

        #endregion


    }
}
