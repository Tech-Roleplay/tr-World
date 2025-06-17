using System;
using System.Linq;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using trWorld.Controllers;
using trWorld.Models;
using trWorld.Vehicle;

// TODO: Adding ban/freez func

namespace trWorld.Player;

/// <summary>
/// Provides a collection of extension methods for the TPlayer class to handle various player-related operations
/// such as permissions, monetary transactions, job management, player states, and more.
/// </summary>
public static class PlayerFuntions
{
    /// Provides a set of utility methods for interacting with a player and their attributes, actions, or related entities like vehicles and other players.
    private static readonly Random random = new();

    /// <summary>
    /// Checks if the player has the required permission to perform an action.
    /// </summary>
    /// <param name="player">The player whose permissions are being checked.</param>
    /// <param name="RequiredPermission">The minimum required permission, represented as an integer.</param>
    /// <returns>Returns true if the player has the required permission, otherwise returns false and sends a chat message.</returns>
    public static bool HasPlayerPermission(this TPlayer player, int RequiredPermission)
    {
        if (player.Permission >= RequiredPermission) return true;
        player.SendChatMessage("{FF0000}You do not have the permission to use this command.");
        return false;
    }

    /// <summary>
    /// Searches for the nearest player relative to the specified player.
    /// </summary>
    /// <param name="player">The player for whom the closest player is being searched.</param>
    /// <param name="distance">The maximum distance within which to search for the nearest player, default is 5.0.</param>
    /// <returns>Returns the nearest player as a TPlayer instance if found; otherwise, returns null.</returns>
    public static TPlayer GetClosestPlayer(this TPlayer player, float distance = 5.0f)
    {
        TPlayer tPlayer = null;
        foreach (TPlayer ply in Alt.GetAllPlayers())
        {
            var plyPos = ply.Position;
            var plyDist = player.Position.Distance(plyPos);
            if (plyDist < distance && player.Dimension == ply.Dimension)
            {
                distance = plyDist;
                tPlayer = ply;
            }
        }

        return tPlayer;
    }

    /// <summary>
    /// Searches for the closest vehicle to the player within a specified distance.
    /// </summary>
    /// <param name="player">The player from whose position the search is conducted.</param>
    /// <param name="distance">The maximum distance to search for the closest vehicle. Default value is 5.0f.</param>
    /// <returns>Returns the closest vehicle as a BVehicle object, or null if no vehicle is found within the specified distance.</returns>
    public static BVehicle GetClosestVehicle(this TPlayer player, float distance = 5.0f)
    {
        BVehicle tVehicle = null;
        foreach (BVehicle veh in Alt.GetAllVehicles())
        {
            var vehPos = veh.Position;
            var vehDist = player.Position.Distance(vehPos);
            if (vehDist < distance && player.Dimension == veh.Dimension)
            {
                distance = vehDist;
                tVehicle = veh;
            }
        }

        return tVehicle;
    }

    /// <summary>
    /// Searches for the nearest pedestrian within the specified distance from the player.
    /// </summary>
    /// <param name="player">The player for whom the closest pedestrian is being searched.</param>
    /// <param name="distance">The maximum allowable distance to search for a pedestrian. Defaults to 5.0.</param>
    /// <returns>The closest pedestrian within the specified distance, or null if none are found.</returns>
    public static Ped GetClosestPed(this TPlayer player, float distance = 5.0f)
    {
        Ped tPed = null;
        foreach (Ped ped in Alt.GetAllPeds())
        {
            var pedPos = ped.Position;
            var pedDist = player.Position.Distance(pedPos);
            if (pedDist < distance && player.Dimension == ped.Dimension)
            {
                distance = pedDist;
                tPed = ped;
            }
        }

        return tPed;
    }

    // all from 

    #region gang functions

    /// <summary>
    /// Sets the gang information for the specified player.
    /// </summary>
    /// <param name="player">The player whose gang information is being set.</param>
    /// <param name="gangname">The name of the gang to assign to the player.</param>
    /// <param name="ganggrade">The grade level to assign within the gang.</param>
    public static void SetGang(this TPlayer player, string gangname, int ganggrade)
    {
        var gangObject = GangController.LoadGangDetailsFromDb(gangname);
        var returnGangClass = GangController.LoadGangGradeFromDb(gangname, ganggrade);

        player.Gang.Name = gangname;
        player.Gang.GradeLevel = (uint)ganggrade;
        player.Gang.Label = gangObject;
        player.Gang.GradeName = returnGangClass.Name;
        player.Gang.GradeLabel = returnGangClass.Label;
        player.Gang.SkinMale = returnGangClass.SkinMale;
        player.Gang.SkinFemale = returnGangClass.SkinFemale;
    }

    #endregion

    /// <summary>
    /// Generates a random character ID for the given player.
    /// </summary>
    /// <param name="player">The player for whom the character ID will be generated.</param>
    /// <returns>A string representing the generated random character ID.</returns>
    [Obsolete]
    public static string GetRandomCharid(this TPlayer player)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var finalString = Enumerable.Repeat(chars, 3)
            .Select(s => s[random.Next(s.Length)]).ToArray();
        var result = finalString + "-" + random.Next(101, 999);
        return result;
    }

    #region Adminstration

    /// <summary>
    /// Freezes or unfreezes the player based on their current state.
    /// </summary>
    /// <param name="player">The player to freeze or unfreeze.</param>
    public static void Freeze(this TPlayer player)
    {
        player.Emit("admin:player:freeze", !player.Frozen);

        
        //DCLog
    }

    /// <summary>
    /// Gives a warning to a player.
    /// </summary>
    /// <param name="player">The player receiving the warning.</param>
    /// <param name="reason">The reason for the warning.</param>
    public static void Warn(this TPlayer player, string reason)
    {
        player.Emit("admin:player:warn", reason);

        // DCLog
    }

    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    /// <param name="player">The player to be kicked.</param>
    /// <param name="reason">The reason for kicking the player.</param>
    public static void Kicking(this TPlayer player, string reason)
    {
        player.Kick(reason);

        //DCLog
    }

    /// <summary>
    /// Temporarily bans a player for a specified duration, with a specified reason.
    /// </summary>
    /// <param name="player">The player to be temporarily banned.</param>
    /// <param name="duration">The duration of the ban as an integer.</param>
    /// <param name="timeformat">The format of the time for the ban (e.g., "hours", "days").</param>
    /// <param name="reason">The reason for the temporary ban.</param>
    public static void TempBan(this TPlayer player, int duration, string timeformat, string reason)
    {
    }


    /// <summary>
    /// Bans a player from the server.
    /// </summary>
    /// <param name="player">The player to be banned.</param>
    /// <param name="reason">The reason for the ban.</param>
    public static void Ban(this TPlayer player, string reason)
    {
        // BanFunc
        TPlayerController.AddPlayerToBanList(player, reason);
        player.Kick("Ban: " + reason);

        //DCLog
    }

    #endregion

    #region Money functions

    /// <summary>
    /// Adds a specified amount of money to the player's cash balance.
    /// </summary>
    /// <param name="player">The player to whom the money is being added</param>
    /// <param name="amount">The amount of money to add to the player's cash balance</param>
    /// <param name="reason">The reason for adding the money, such as payment or income</param>
    public static void AddMoneyToCash(this TPlayer player, int amount, string reason)
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
    /// Adds the specified amount of money to the player's bank balance.
    /// </summary>
    /// <param name="player">The player whose bank balance will be updated.</param>
    /// <param name="amount">The amount of money to add to the bank balance.</param>
    /// <param name="reason">The reason for adding the money, such as payment or income source.</param>
    public static void AddMoneyToBank(this TPlayer player, int amount, string reason)
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
    /// Subtracts money from the player's cash balance.
    /// </summary>
    /// <param name="player">The player</param>
    /// <param name="amount">The amount to subtract</param>
    /// <param name="reason">The reason for the deduction, e.g., payment for purchases or services</param>
    public static void SubMoneyToCash(this TPlayer player, int amount, string reason)
    {
        // Temporäres Geld erstellen
        var tempMoney = player.CashBalance;

        // Temporäres Geld mit amount subtrahieren, mit Schutz
        if (tempMoney >= amount && tempMoney > 0) tempMoney -= amount;

        // Temporäres Geld dem Spieler Aufladen
        player.CashBalance = tempMoney;

        // Log erstellen des Transfers
        // ND
    }

    /// <summary>
    /// Subtracts money from the player's bank balance.
    /// </summary>
    /// <param name="player">The player whose bank balance will be updated.</param>
    /// <param name="amount">The amount to subtract from the bank balance.</param>
    /// <param name="reason">The reason for the transaction, for instance, rent or bill payments.</param>
    public static void SubMoneyToBank(this TPlayer player, int amount, string reason)
    {
        // Temporäres Geld erstellen
        var tempMoney = player.BankBalance;

        // Temporäres Geld mit amount subtrahieren, ohne Schutz
        tempMoney -= amount;

        // Temporäres Geld dem Spieler Aufladen
        player.BankBalance = tempMoney;

        // Log erstellen des Transfers
        // ND
    }

    /// <summary>
    /// Sets the player's cash balance to a specified amount.
    /// </summary>
    /// <param name="player">The player whose cash balance is being set.</param>
    /// <param name="amount">The amount to set as the player's cash balance.</param>
    /// <param name="reason">The reason for setting the player's cash balance (e.g., correcting balance after cheating or spawning).</param>
    public static void SetMoneyToCash(this TPlayer player, int amount, string reason)
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
    /// Sets the player's bank balance to the specified amount.
    /// </summary>
    /// <param name="player">The player whose bank balance will be updated.</param>
    /// <param name="amount">The new bank balance to be set.</param>
    /// <param name="reason">The reason for updating the bank balance, e.g., correcting an error, resetting after cheating detection, or spawning events.</param>
    public static void SetMoneyToBank(this TPlayer player, int amount, string reason)
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
    /// Returns the player's current cash balance.
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>An integer representing the player's cash balance</returns>
    public static int GetMoneyFromCash(this TPlayer player)
    {
        return player.CashBalance;
    }

    /// <summary>
    /// Retrieves the player's current bank balance.
    /// </summary>
    /// <param name="player">The player whose bank balance is being retrieved.</param>
    /// <returns>An integer representing the player's bank balance.</returns>
    public static int GetMoneyFromBank(this TPlayer player)
    {
        return player.BankBalance;
    }

    #endregion

    // all from job

    #region job functions

    /// <summary>
    /// Sets the job for the specified player.
    /// </summary>
    /// <param name="player">The player to whom the job will be assigned.</param>
    /// <param name="jobname">The job name (handling name) to assign to the player.</param>
    /// <param name="jobgrade">The grade level of the job to assign to the player.</param>
    public static void SetJob(this TPlayer player, string jobname, int jobgrade)
    {
        // Jobs

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
    }

    /// <summary>
    /// Changes the duty status of the player's job.
    /// </summary>
    /// <param name="player">The player whose duty status will be changed.</param>
    public static void ChangeDuty(this TPlayer player)
    {
        if (player != null)
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

    /// <summary>
    /// Pays the player for their work by adding their job payment to their bank balance.
    /// </summary>
    /// <param name="player">The player to be paid.</param>
    public static void PaymentJob(this TPlayer player)
    {
        AddMoneyToBank(player, (int)player.Job.Payment, $"Salary by {player.Job.Label}");
    }

    /// <summary>
    /// Resets the player's job to unemployed status.
    /// </summary>
    /// <param name="player">The player whose job will be reset</param>
    public static void ResetJob(this TPlayer player)
    {
        player.SetJob("unemployed", 0);
    }

    //gets
    /// <summary>
    /// Checks if the player is a boss in their current job.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>True if the player is a boss, otherwise false.</returns>
    public static bool IsPlayerBoss(this TPlayer player)
    {
        return player.Job.IsBoss;
    }

    /// <summary>
    /// Retrieves the job type of the specified player.
    /// </summary>
    /// <param name="player">The player whose job type is being retrieved.</param>
    /// <returns>A string representing the type of job the player has.</returns>
    public static string GetPlayerJobType(this TPlayer player)
    {
        return player.Job.Type;
    }

    /// <summary>
    /// Retrieves the appropriate skin for the job assigned to the player based on their gender.
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>
    /// A string representing the player's job skin; returns the male skin if the player's gender is male,
    /// the female skin if the player's gender is female, or an empty string if no match is found.
    /// </returns>
    public static string GetSkinForJob(this TPlayer player)
    {
        if (player.Sex == 'm') return player.Job.SkinMale;

        if (player.Sex == 'f') return player.Job.SkinFemale;

        return string.Empty;
    }

    #endregion

    // Metadata

    #region Metadata

    /// <summary>
    /// Checks if the player is currently cuffed
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>True if the player is cuffed, otherwise false</returns>
    public static bool GetIsPlayerCuffed(this TPlayer player)
    {
        return player.Metadata.IsCuffed;
    }

    /// <summary>
    /// Sets the player's cuffed state
    /// </summary>
    /// <param name="player">The player whose cuffed state is being set</param>
    /// <param name="value">The cuffed state to set, where true indicates cuffed and false indicates uncuffed</param>
    public static void SetIsPlayerCuffed(this TPlayer player, bool value)
    {
        player.Metadata.IsCuffed = value;
    }

    /// <summary>
    /// Gets whether the player is currently in prison.
    /// </summary>
    /// <param name="player">The player whose prison status is being checked.</param>
    /// <returns>True if the player is in prison, otherwise false.</returns>
    public static bool GetIsPlayerInPrison(this TPlayer player)
    {
        return player.Metadata.IsInPrison;
    }

    /// <summary>
    /// Sets the player's prison state.
    /// </summary>
    /// <param name="player">The player for whom the prison state is being set.</param>
    /// <param name="value">The state indicating whether the player is in prison.</param>
    public static void SetIsPlayerInPrison(this TPlayer player, bool value)
    {
        player.Metadata.IsInPrison = value;
    }

    /// <summary>
    /// Determines whether the player is dead.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>True if the player is dead; otherwise, false.</returns>
    public static bool GetIsPlayerDead(this TPlayer player)
    {
        return player.Metadata.IsPlyDead;
    }

    /// <summary>
    /// Sets whether the player is dead or alive
    /// </summary>
    /// <param name="player">The player</param>
    /// <param name="value">A boolean value indicating the player's death state</param>
    public static void SetIsPlayerDead(this TPlayer player, bool value)
    {
        player.Metadata.IsPlyDead = value;
    }

    /// <summary>
    /// Determines if the player is currently in a downed state.
    /// </summary>
    /// <param name="player">The player whose state is being checked.</param>
    /// <returns>True if the player is down, otherwise false.</returns>
    public static bool GetIsPlayerDown(this TPlayer player)
    {
        return player.Metadata.IsPlyDown;
    }

    /// <summary>
    /// Sets whether the player is down or not.
    /// </summary>
    /// <param name="player">The player whose state will be updated.</param>
    /// <param name="value">Boolean indicating if the player is down (true) or not (false).</param>
    public static void SetIsPlayerDown(this TPlayer player, bool value)
    {
        player.Metadata.IsPlyDown = value;
    }

    /// <summary>
    /// Gets the state of whether the player has been headshotted.
    /// </summary>
    /// <param name="player">The player whose headshot status is to be checked.</param>
    /// <returns>True if the player is headshotted; otherwise, false.</returns>
    public static bool GetIsPlayerHeadshotted(this TPlayer player)
    {
        return player.Metadata.IsPlyHeadshotted;
    }

    /// <summary>
    /// Sets the headshotted state of the player
    /// </summary>
    /// <param name="player">The player to apply the state change</param>
    /// <param name="value">Indicates whether the player is considered headshotted</param>
    public static void SetIsPlayerHeadshotted(this TPlayer player, bool value)
    {
        player.Metadata.IsPlyHeadshotted = value;
    }

    /// <summary>
    /// Gets the logout state of the player.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>True if the player is logged out, otherwise false.</returns>
    public static bool GetIsPlayerLogout(this TPlayer player)
    {
        return player.Metadata.IsPlyLogout;
    }

    /// <summary>
    /// Sets the player's logout state.
    /// </summary>
    /// <param name="player">The player whose logout state is being set.</param>
    /// <param name="value">The new logout state of the player.</param>
    public static void SetIsPlayerLogout(this TPlayer player, bool value)
    {
        player.Metadata.IsPlyLogout = value;
    }

    /// <summary>
    /// Sets the hunger state of a player.
    /// </summary>
    /// <param name="player">The player whose hunger state will be set.</param>
    /// <param name="value">The desired hunger state value, ranging between 0.0 and 100.0.</param>
    public static void SetHunger(this TPlayer player, float value)
    {
        value = Math.Clamp(value, 0f, 100f);
        player.Metadata.Hunger = value;
    }

    /// <summary>
    /// Retrieves the player's current hunger level.
    /// </summary>
    /// <param name="player">The player whose hunger state is being retrieved.</param>
    /// <returns>A float representing the current hunger state of the player.</returns>
    public static float GetHunger(this TPlayer player)
    {
        return player.Metadata.Hunger;
    }

    /// <summary>
    /// Sets the thirst level of the player
    /// </summary>
    /// <param name="player">The player whose thirst level is to be set</param>
    /// <param name="value">The thirst value to set, which should be between 0.0 and 100.0</param>
    public static void SetThirst(this TPlayer player, float value)
    {
        value = Math.Clamp(value, 0f, 100f);
        player.Metadata.Thirst = value;
    }

    /// <summary>
    /// Gets the state of the player's thirst
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>A float representing the player's thirst level</returns>
    public static float GetThirst(this TPlayer player)
    {
        return player.Metadata.Thirst;
    }

    /// <summary>
    /// Sets the armor value for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the armor value is being set.</param>
    /// <param name="value">The armor value to set, between 0.0 and 100.0.</param>
    public static void SetArmor(this TPlayer player, float value)
    {
        value = Math.Clamp(value, 0f, 100f);
        player.Metadata.Armor = value;
    }

    /// <summary>
    /// Retrieves the current armor value of the player.
    /// </summary>
    /// <param name="player">The player whose armor value is being retrieved.</param>
    /// <returns>The player's current armor value as a float.</returns>
    public static float GetArmor(this TPlayer player)
    {
        return player.Metadata.Armor;
    }

    /// <summary>
    /// Sets the stress state for the given player.
    /// </summary>
    /// <param name="player">The player whose stress state is to be set.</param>
    /// <param name="value">The stress value to be set, ranging between 0.0 and 100.0.</param>
    public static void SetStress(this TPlayer player, float value)
    {
        value = Math.Clamp(value, 0f, 100f);
        player.Metadata.Stress = value;
    }

    /// <summary>
    /// Gets the stress state of a player.
    /// </summary>
    /// <param name="player">The player whose stress level is being retrieved.</param>
    /// <returns>The stress level as a floating-point value.</returns>
    public static float GetStress(this TPlayer player)
    {
        return player.Metadata.Stress;
    }

    /// <summary>
    /// Sets the jail time for a player.
    /// </summary>
    /// <param name="player">The player whose jail time is being set.</param>
    /// <param name="value">The jail time value to set, must be between 0 and 100 inclusive.</param>
    public static void SetJailTime(this TPlayer player, float value)
    {
        value = Math.Clamp(value, 0f, 100f);
        player.Metadata.JailTime = value;
    }

    /// <summary>
    /// Retrieves the player's current jail time.
    /// </summary>
    /// <param name="player">The player whose jail time is being retrieved.</param>
    /// <returns>The amount of jail time the player has, in seconds.</returns>
    public static float GetJailTime(this TPlayer player)
    {
        return player.Metadata.JailTime;
    }

    /// <summary>
    /// Updates the player's last update timestamp to the current date and time.
    /// </summary>
    /// <param name="player">The player for whom the last update timestamp will be updated.</param>
    public static void UpdateLastUpdate(this TPlayer player)
    {
        player.Metadata.LastUpdate = DateTime.UtcNow;
    }

    /// <summary>
    /// Retrieves the last update timestamp for the player.
    /// </summary>
    /// <param name="player">The player whose last update timestamp is being retrieved.</param>
    /// <returns>The DateTime of the last update.</returns>
    public static DateTime GetLastUpdate(this TPlayer player)
    {
        return player.Metadata.LastUpdate;
    }

    /// <summary>
    /// Sets the player's creation date to the current date and time.
    /// </summary>
    /// <param name="player">The player whose creation date is being set.</param>
    public static void CreateCreateDate(this TPlayer player)
    {
        player.Metadata.CreateDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Retrieves the account creation date of the player.
    /// </summary>
    /// <param name="player">The player whose creation date is being retrieved.</param>
    /// <returns>The creation date of the player's account.</returns>
    public static DateTime GetCreateDate(this TPlayer player)
    {
        return player.Metadata.CreateDate;
    }

    #endregion


    #region Skin

    /// <summary>
    /// Sets the clothing item for the player.
    /// </summary>
    /// <param name="player">The player character to set clothing for.</param>
    /// <param name="componet">The component ID representing the part of the body (e.g., torso, legs, etc.).</param>
    /// <param name="drawable">The drawable ID representing the specific clothing model.</param>
    /// <param name="texture">The texture ID associated with the clothing model.</param>
    /// <param name="pallette">The optional palette ID for advanced customization of the clothing model.</param>
    public static void SetCloth(this TPlayer player, ComponentIDs componet, int drawable, int texture,
        int? pallette)
    {
    }

    /// <summary>
    /// Retrieves the cloth currently equipped by the player.
    /// </summary>
    /// <param name="player">The player instance from which to retrieve the cloth.</param>
    /// <returns>The name or identifier of the currently equipped cloth.</returns>
    public static string GetCloth(this TPlayer player)
    {
        //temp return "Cloth";
        return "Cloth";
    }

    /// <summary>
    /// Sets the DLC cloth for the player.
    /// </summary>
    /// <param name="player">The player for whom the DLC cloth will be set.</param>
    /// <param name="dlc">The DLC identifier.</param>
    /// <param name="componet">The component ID to be modified (e.g., mask, torso).</param>
    /// <param name="drawable">The drawable ID representing the clothing item.</param>
    /// <param name="texture">The texture ID for the clothing item.</param>
    /// <param name="pallette">The optional palette ID for the clothing customization.</param>
    public static void SetDlcCloth(this TPlayer player, int dlc, ComponentIDs componet, int drawable, int texture,
        int? pallette)
    {
    }

    #endregion

    #region Inventory
    
    public static void UseItem(this TPlayer player)
    {
        
    }

    public static void EquipeItem(this TPlayer player)
    {
        
    }

    public static void ConsumItem(this TPlayer player)
    {
        
    }

    #endregion

    /// <summary>
    /// Toggles the lock state of a vehicle if the player has the correct key.
    /// </summary>
    /// <param name="player">The player attempting to lock/unlock the vehicle.</param>
    /// <param name="vehicle">The vehicle to be locked or unlocked.</param>
    /// <returns>True if the player has the key and the lock state was toggled; otherwise, false.</returns>
    public static bool ToggleLockIfPlayerHasKey(this TPlayer player, BVehicle vehicle)
    {
        if (!player.VehKeysID.Contains(vehicle.KeyID))
            return false;

        vehicle.LockState = vehicle.LockState == AltV.Net.Enums.VehicleLockState.Unlocked
            ? AltV.Net.Enums.VehicleLockState.Locked
            : AltV.Net.Enums.VehicleLockState.Unlocked;

        // Optional: Hier könntest du ein Event emitten wie "OnVehicleLockChanged"

        return true;
    }

}