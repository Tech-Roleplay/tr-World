using AltV.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace tr_world
{
    public class Utility : IScript
    {
        public static void ScriptLog(string message)
        {
            Alt.Log($"Script: [{Alt.Resource.Name}] " + message);
        }

        public static byte[] Post (string url, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.UploadValues(url, pairs);
            }
        }
        public static string DC_Url (string nameofURL)
        {
            string url;
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = $"SELECT * FROM dc_url WHERE name=@name LIMIT 1";

                cmd.Parameters.AddWithValue("@name", nameofURL);

                using(MySqlDataReader reader = cmd.ExecuteReader())
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

        public static void DClog(string message, string username, string url, string picurl)
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
}
