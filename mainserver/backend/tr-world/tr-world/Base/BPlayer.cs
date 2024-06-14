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
    /// <summary>
    /// This class represents the metadata of a player in the game.
    /// </summary>
    public class MetaData : IMetaData
    {
        /// <summary>
        /// This enum is used to represent the gender of a player.
        /// </summary>
        public enum Gender
        {
            /// <summary>
            /// Male gender represented by 'M'.
            /// </summary>
            Male = 'M',

            /// <summary>
            /// Female gender represented by 'F'.
            /// </summary>
            Female = 'F',

            /// <summary>
            /// Unknown gender represented by 'U'.
            /// </summary>
            Unknow = 'U'
        }

        /// <summary>
        /// This property is used to store the first name of a players chachter.
        /// <summary>
#pragma warning disable CS8766 // Die NULL-Zulässigkeit von Verweistypen im Rückgabetyp entspricht (möglicherweise aufgrund von Attributen für die NULL-Zulässigkeit) nicht dem implizit implementierten Member.
        public string? Firstname { get; set; }
#pragma warning restore CS8766 // Die NULL-Zulässigkeit von Verweistypen im Rückgabetyp entspricht (möglicherweise aufgrund von Attributen für die NULL-Zulässigkeit) nicht dem implizit implementierten Member.

        /// <summary>
        /// This property is used to store the last name of a player.
        /// </summary>
#pragma warning disable CS8766 // Die NULL-Zulässigkeit von Verweistypen im Rückgabetyp entspricht (möglicherweise aufgrund von Attributen für die NULL-Zulässigkeit) nicht dem implizit implementierten Member.
        public string Lastname { get; set; }
#pragma warning restore CS8766 // Die NULL-Zulässigkeit von Verweistypen im Rückgabetyp entspricht (möglicherweise aufgrund von Attributen für die NULL-Zulässigkeit) nicht dem implizit implementierten Member.

        /**
         * Sex property:
         * This property is used to store the gender of a player.
         */
        public IMetaData.Gender Sex { get; set; }

        /**
         * Birthday property:
         * This property is used to store the birthday of a player.
         */
        public DateOnly Birthday { get; set; }

        public MetaData(){
            Firstname = "";
            Lastname = "";
            Sex = IMetaData.Gender.Unknow;
            Birthday = DateOnly.MinValue;
        }
    }

    /// <summary>
    /// Represents the in-game currency of a player.
    /// </summary>
    public class Money
    {
        /// <summary>
        /// Gets or sets the amount of money in the player's bank.
        /// </summary>
        public int BankBalance { get; set; }

        /// <summary>
        /// Gets or sets the amount of money in the player's wallet.
        /// </summary>
        public int CashBalance { get; set; }
    }

    /**
     * BPlayer Class:
     * This class extends the AsyncPlayer class and adds additional functionality.
     */
    public class BPlayer : Player
    {
        /**
         * IMetaData Interface:
         * This interface defines the metadata properties of a player.
         */
        public interface IMetaData
        {
            /**
             * Enum for Gender:
             * This enum is used to represent the gender of a player.
             */
            enum Gender
            {
                /**
                 * Male gender represented by 'M'.
                 */
                Male = 'M',

                /**
                 * Female gender represented by 'F'.
                 */
                Female = 'F',

                /**
                 * Unknown gender represented by 'U'.
                 */
                Unknow = 'U'
            }

            /**
             * Firstname property:
             * This property is used to store the first name of a player.
             */
            string Firstname { get; set; }

            /**
             * Lastname property:
             * This property is used to store the last name of a player.
             */
            string Lastname { get; set; }

            /**
             * Sex property:
             * This property is used to store the gender of a player.
             */
            Gender Sex { get; set; }

            /**
             * Birthday property:
             * This property is used to store the birthday of a player.
             */
            DateOnly Birthday { get; set; }
        }

        /**
         * metaData property:
         * This property is used to store the metadata of a player.
         */
        public MetaData metaData;

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
#pragma warning disable CS8602 // Dereferenzierung eines möglichen Nullverweises.
            metaData.Sex = IMetaData.Gender.Male;
#pragma warning restore CS8602 // Dereferenzierung eines möglichen Nullverweises.
            metaData.Firstname = "H";
            metaData.Lastname = "";
        }
    }
}