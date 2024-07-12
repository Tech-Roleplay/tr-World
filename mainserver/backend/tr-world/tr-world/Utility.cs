using AltV.Net;
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
