using System;
using AltV.Net;
using trWorld.Models;

namespace trWorld.Player;

/// <summary>
/// TPlayer Class:
/// Represents a custom player class that extends the AltV.Net.Elements.Entities.Player.
/// Adds additional properties, methods, and attributes relevant to the gameplay experience.
/// </summary>
public class TPlayer : AltV.Net.Elements.Entities.Player
{
    #region Metadata

    /// This property represents the permission level of the player, used to define their access rights and roles in the system.
    //public string Group { get; set; }
    public int Permission { get; set; }


    /// This property is used to store the first name of a player.
    public string Firstname { get; set; }

    /// This property is used to store the surname of a player.
    public string Surname { get; set; }


    /// This property is used to store the sex of the player as a single character,
    /// typically representing 'm' for male or 'f' for female.
    public char Sex { get; set; }

    /// This property represents the height of a player.
    public int Height { get; set; }

    /// This property is used to store the birthday of the player.
    public DateOnly Birthday { get; set; }

    /// This property is used to store the backstory of a player.
    public string Backstory { get; set; }

    /// This property is used to store the ID card information of the player. It is marked as obsolete and should be avoided in favor of updated implementations.
    [Obsolete]
    public string id_card { get; set; }

    #endregion

    #region Interfaces Job & Gang

    /// This property represents the job information associated with a player, including details such as job type, name, label, payment, grade level, and other attributes.
    public IJob Job { get; set; }

    /// This property is used to store information about a player's gang affiliation.
#nullable enable
    public IGang Gang { get; set; }

    #endregion


    #region Money

    /// This property represents the player's bank account balance. It is used to store the amount of money the player has in their bank.
    public int BankBalance { get; set; }

    /// This property represents the amount of cash a player currently holds.
    public int CashBalance { get; set; }

    #endregion

    #region Inventory

    /// This property is used to store the inventory details of a player as a string.
    /// It represents the serialized data containing the player's inventory information.
    /// Marked as obsolete and should be updated or replaced in the future as needed.
    [Obsolete]
    public string sInventory { get; set; }

    /// This property is used to manage the player's inventory, allowing the addition, removal, and use of items.
    public Inventory Inventory { get; set; }

    #endregion

    #region Vehicle

    /// This property is used to store an array of vehicle key identifiers associated with the player.
    public string[] VehKeysID { get; set; }

    /// This property represents the driver's license status or information of the player.
    public string Driver_License { get; set; }

    #endregion

    #region Detailed Metas

    /// This property holds metadata associated with a player that can be used to store additional custom information for gameplay purposes.
    public TMetadata Metadata { get; set; }

    #region phone infos

    /// This property is used to store the phone object associated with a player.
    public Phone Phone { get; set; }

    #endregion

    #endregion

    /// This property stores the appearance customization data for a player.
    public string Skin { get; set; }

    /// This property is used to define or access the main identifier or primary attribute associated with the TPlayer class.
    public string MainProperty { get; set; }


    #region Basic Constructor

    /// <summary>
    /// Represents a player entity in the game, with extensions for jobs, gangs, metadata, and phone details.
    /// </summary>
    /// <remarks>
    /// This class inherits from AltV.Net.Elements.Entities.Player and adds custom properties and functionality
    /// specific to the game, such as job, gang affiliation, permissions, and personal metadata.
    /// </remarks>
    public TPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
    {
        Job = new TJob();
        Gang = new TGang();
        Metadata = new TMetadata();
        Phone = new Phone();

        Permission = (int)TPermission.Player;

        // 1-1 Change for custom values
        Job.Name = "unemployed";
        Job.Label = "Civilian";
        Job.IsBoss = false;
        Job.OnDuty = true;
        Job.Type = "none";
        Job.Payment = 420;
        Job.GradeLevel = 0;
        Job.GradeName = "No Grades";

        Gang.Name = "none";
        Gang.Label = "No Gang Affiliaton";
        Gang.IsBoss = false;
        Gang.OnDuty = false;
        Gang.Type = "none";
        Gang.GradeLevel = 0;
        Gang.GradeName = "No Grades";

        Metadata.Armor = 100.0f;
        Metadata.Hunger = 100.0f;
        Metadata.Thirst = 100.0f;
        Metadata.JailTime = 0;
        Metadata.IsCuffed = false;
        Metadata.CreateDate = DateTime.Now;
        Metadata.IsInPrison = false;
        Metadata.IsPlyDead = false;
        Metadata.IsPlyDown = false;
        Metadata.IsPlyHeadshotted = false;
        Metadata.IsPlyLogout = false;
        Metadata.LastUpdate = DateTime.Now;


        Firstname = "CHANGE";
        Surname = "CHANGE";

        Skin = "";
        MainProperty = "";

        Phone.ProfilePicUrl =
            "https://cdn.icon-icons.com/icons2/3553/PNG/512/account_profile_user_ecommerce_icon_224942.png";
        Phone.Number = 0;
    }

    #endregion

    #region Funtions

    // OBSOLDET
    
    
    #region Money functions

    /// <summary>
    /// Adds a certain amount of money to a player's balance.
    /// </summary>
    /// <param name="player"> The player to whom the money will be added.</param>
    /// <param name="amount"> The amount of money to add.</param>
    /// <param name="reason"> The reason for adding the money.</param>
    /// <param name="moneytype"> The type of money to add (e.g., cash or bank).</param>
    [Obsolete("Use instate AddMoneyToCash, AddMoneyToBank", true)]
    public static void AddMoney(TPlayer player, int amount, string reason, string moneytype)
    {
    }

    // add function
    // add

    #endregion

    // all from job

    #endregion
}