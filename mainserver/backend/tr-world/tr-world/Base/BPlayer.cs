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
        /// This property is used to store the first name of a player.
        /// <summary>
        public string Firstname { get; set; }

        /// <summary>
        /// This property is used to store the last name of a player.
        /// </summary>
        public string Lastname { get; set; }


        /// <summary>
        /// This property is used to store the gender of a player.
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// This property is used to store the birthday of a player.
        /// </summary>
        public DateOnly Birthday { get; set; }

        #endregion

        public IJob Job { get; set; }

        public IGang Gang { get; set; }

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



        /**
         * BPlayer Constructor:
         * This constructor is used to initialize a new instance of the BPlayer class.
         * 
         * @param core: The core instance of the game.
         * @param nativePointer: A pointer to the native player instance.
         * @param id: The unique ID of the player.
         */
        public BPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {
            /**
             * Initialize the metaData instance with default values.
             */

            Job = new TJob();
            Gang = new TGang();



            Firstname = "ABC";
            Lastname = "DEF";
        }
    }
}