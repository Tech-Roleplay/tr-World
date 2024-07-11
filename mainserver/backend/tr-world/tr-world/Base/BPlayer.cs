using AltV.Net;

using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
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
        public string Group {  get; protected set; }


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
        public bool IsPlyDead { get; set; }
        public bool IsPlyDown { get; set; }
        public bool IsPlyHeadshotted { get; set; }
        public bool IsPlyLogout { get; set; }

        #endregion

        #region phone infos

        #endregion

        #endregion



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

            Firstname = "ABC";
            Surname = "DEF";
        }
    }
}