using AltV.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tr_world
{
    public class Utility : IScript
    {
        [Obsolete]
        public static void ScriptLog(string message)
        {
            Alt.Log($"Script: [{Alt.Resource.Name}] " + message);
        }

        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.UploadValues(url, pairs);
            }
        }
        [Obsolete]
        public static string DC_Url(string nameofURL)
        {
            string url;
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = $"SELECT * FROM dc_url WHERE name=@name LIMIT 1";

                cmd.Parameters.AddWithValue("@name", nameofURL);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    url = reader.GetString("url");


                    reader.Close();
                    return url;
                }
            }
            catch (Exception e)
            {
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Sending Webhook to Discord.
        /// </summary>
        /// <param name="message">The Text</param>
        /// <param name="username">Username of webhook</param>
        /// <param name="url">URL of the webhook</param>
        /// <param name="picurl">Picture's URL of the webhook</param>
        /// <param name="suppress_Notifycation">Have it to do a sound?</param>
        public static void DClog(string message, string username, string url, string picurl, bool suppress_Notifycation = false)
        {

            if (suppress_Notifycation)
            {
                Post(url, new System.Collections.Specialized.NameValueCollection()
                {
                    {
                        "username",
                        username
                    },
                    {
                        "avatar",
                        picurl
                    },
                    {
                        "content",
                        message
                    },
                    {
                        "flags",
                        "12"
                    }
                });
            } else
            {
                Post(url, new System.Collections.Specialized.NameValueCollection()
                {
                    {
                        "username",
                        username
                    },
                    {
                        "avatar",
                        picurl
                    },
                    {
                        "content",
                        message
                    }
                });
            }

        }

        /// <summary>
        /// Sending Webhook with Embed to Discord.
        /// </summary>
        /// <param name="message">The Text</param>
        /// <param name="username">Username of webhook</param>
        /// <param name="url">URL of the webhook</param>
        /// <param name="picurl">Picture's URL of the webhook</param>
        /// <param name="suppress_Notifycation">Have it to do a sound?</param>
        /// <param name="discordEmbed1">A Discord Embed</param>
        /// <param name="discordEmbed2">A Discord Embed</param>
        public static void DClog_with_Embed(string message, string username, string url, string picurl, DiscordEmbed discordEmbed1, DiscordEmbed discordEmbed2 = null, bool suppress_Notifycation = false)
        {
            if (!suppress_Notifycation)
            {
                Post(url, new System.Collections.Specialized.NameValueCollection()
                {

                });
            } else
            {

            }
        }
    }
    public class DiscordEmbed
    {
        
    }
}
