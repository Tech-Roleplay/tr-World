using AltV.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Base;

namespace tr_world.Controllers
{
    public class JobController : IScript
    {
        public static object LoadJobDetailsFromDB(string jobname)
        {
			try
			{
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM jobs WHERE name=@name LIMIT 1";
                cmd.Parameters.AddWithValue("@name", jobname);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    var label = reader.GetString("label");
                    bool requireInvitation = reader.GetBoolean("is_required_to_be_invited");
                    bool whitlistedjob = reader.GetBoolean("whitelisted");



                    return (label, requireInvitation, whitlistedjob);

                }
            }
            catch (Exception e)
			{
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());

                return null;
            }
        }
    }
}
