using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Models;
using tr_world.Player;

namespace tr_world.Player
{
    /// <summary>
    /// BPlayer Class:
    /// This class extends the Player class and adds additional functionality.
    /// </summary>
    public class BPlayer : AltV.Net.Elements.Entities.Player
    {
        #region Metadata

       

        /// This property is used to store Group of a player.
        //public string Group { get; set; }
        public int Permission { get; set; }


        /// This property is used to store the first name of a player.
        public string Firstname { get; set; }

        /// <summary>
        /// This property is used to store the surname of a player.
        /// </summary>
        public string Surname { get; set; }


        /// <summary>
        /// This property is used to store the gender of a player.
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// This property is used to store the height of a player.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// This property is used to store the birthday of a player.
        /// </summary>
        public DateOnly Birthday { get; set; }

        /// <summary>
        /// This property is used to store the backstory of a player. (AI Ped will use it.)
        /// </summary>
        public string Backstory { get; set; }

        /// <summary>
        /// This property is used to store the id_card of a player. Next moved to MetadData
        /// </summary>
        [Obsolete]
        public string id_card { get; set; }
        #endregion
        
        #region Interfaces Job & Gang
        /// <summary>
        /// This property is used to store the job of a player.
        /// </summary>
        public IJob Job { get; set; }

        /// <summary>
        /// This property is used to store the gang activity of a player.
        /// </summary>
        #nullable enable
        public IGang Gang { get; set; }
        
        #endregion


        #region Money

        /// <summary>
        /// Gets or sets the amount of money in the player's bank.
        /// </summary>
        public int BankBalance { get; set; }

        /// <summary>
        /// Gets or sets the amount of money in the player's wallet.
        /// </summary>
        public int CashBalance { get; set; }

        #endregion

        #region Inventory
        /// <summary>
        /// This property is used to store the inventory of a player.
        /// </summary>
        public string Inventory { get; set; }
        #endregion
        #region Vehicle
        /// <summary>
        /// This property is used to store the vehicle keys of a player.
        /// </summary>
        public string[] VehKeysID { get; set; }

        /// <summary>
        /// This property is used to store the driver license of a player.
        /// </summary>
        public string Driver_License { get; set; }
        #endregion
        #region Detailed Metas
        public TMetadata Metadata { get; set; }
        
        #region Booleans

        #endregion

        #region phone infos

        public Phone Phone { get; set; }

        #endregion


        #endregion
        public string Skin { get; set; }
        public string MainProperty { get; set; }
        
        
        #region Basic Constructor

        /// <summary>
        /// This constructor is used to initialize a new instance of the BPlayer class.
        /// </summary>
        /// <param name="core"> The core instance of the game.</param>
        /// <param name="nativePointer"> A pointer to the native player instance.</param>
        /// <param name="id"> The unique ID of the player.</param>
        public BPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
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

            Firstname = "ABC";
            Surname = "DEF";
            
            

            Phone.ProfilePicUrl = "https://cdn.icon-icons.com/icons2/3553/PNG/512/account_profile_user_ecommerce_icon_224942.png";
            Phone.Number = 0;
            

        }

        #endregion

        #region Funtions

        #region Money functions

        [Obsolete("Use instate AddMoneyToCash, AddMoneyToBank", error: true)]
        public static void AddMoney(BPlayer player, int amount, string reason, string moneytype)
        {

        }
        // add function
        // add
        #endregion

        // all from job

        #endregion
    }
}

