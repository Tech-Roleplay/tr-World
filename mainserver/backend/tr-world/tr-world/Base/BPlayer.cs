﻿using AltV.Net;

using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static tr_world.Base.BPlayer;

namespace tr_world.Base
{
    /**
     * BPlayer Class:
     * This class extends the AsyncPlayer class and adds additional functionality.
     */
    public class BPlayer : Player
    {
        #region Metadata

        /// <summary>
        /// This property is used to store Character's ID of a player.
        /// <summary>
        public string CharId { get; set; }

        /// <summary>
        /// This property is used to store Group of a player.
        /// <summary>
        public string Group {  get;  set; }


        /// <summary>
        /// This property is used to store the first name of a player.
        /// <summary>
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

        #endregion

        #region Interfaces Job & Gang

        public IJob Job { get; set; }

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

        #region Invenotry
        public string Inventory {  get; set; }
        #endregion

        #region Detailed Metas
        #region Booleans
        public bool IsCuffed { get; set; }
        public bool IsInPrison { get; set; }
        public bool IsPlyDead { get; set; }
        public bool IsPlyDown { get; set; }
        public bool IsPlyHeadshotted { get; set; }
        public bool IsPlyLogout { get; set; }

        #endregion

        #region phone infos
        public int phonenumber {get; set;}
        public string phone_profilpic_url {get; set;}
        public string phone_name {get; set;}

        #endregion

        #endregion

        #region Basic Constructor

        /// <summary>
        /// This constructor is used to initialize a new instance of the BPlayer class.
        /// </summary>
        /// <param name="core"> The core instance of the game.</param>
        /// <param name="nativePointer"> A pointer to the native player instance.</param>
        /// <param name="id"> The unique ID of the player.</param>
        public BPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {
            /**
             * Initialize the default instance with default values.
             */

            Job = new TJob();
            Gang = new TGang();

            Group = "Player";

            // 1-1 Change for custome values
            Job.Name = "unemployed";
            Job.Label = "Civilian";
            Job.IsBoss = false;
            Job.OnDuty = true;
            Job.Type = "none";
            Job.Payment = 30;
            Job.Grade_level = 0;
            Job.Grade_name = "No Grades";

            Gang.Name = "none";
            Gang.Label = "No Gang Affiliaton";
            Gang.IsBoss = false;
            Gang.OnDuty = false;
            Gang.Type = "none";
            Gang.Grade_level = 0;
            Gang.Grade_name = "No Grades";

            Firstname = "ABC";
            Surname = "DEF";
        }

        #endregion

        #region Funtions

        #region Money functions


        public static void AddMoney(BPlayer target, int amount, string reason, string moneytype )
        {

        }
        // add function
        // add
        #endregion

        // all from job
        #region job functions

        #endregion
        // all from 

        #region gang functions

        #endregion

        #endregion
    }
}