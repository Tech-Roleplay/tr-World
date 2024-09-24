using AltV.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using tr_world.Base;

namespace tr_world.Controllers
{
    public class JobController : IScript
    {
        public static object[] LoadJobDetailsFromDB(string jobname)
        {
			try
			{
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM jobs WHERE name=@name LIMIT 1";
                cmd.Parameters.AddWithValue("@name", jobname);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    string label = reader.GetString("label");
                    bool requireInvitation = reader.GetBoolean("is_required_to_be_invited");
                    bool whitlistedjob = reader.GetBoolean("whitelisted");

                    object[] BackArray = new object[] {label,  requireInvitation, whitlistedjob};

                    reader.Close();

                    return BackArray;                    
                }
            }
            catch (Exception e)
			{
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());

                return null;
            }
        }
        public static object[] LoadJobGradeFromDB(string jobname, int grade)
        {
            try
            {
                MySqlCommand cmd = Databank.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM job_grades WHERE job_name=@job_name AND grade=@grade";
                cmd.Parameters.AddWithValue("@job_name", jobname);
                cmd.Parameters.AddWithValue("@grade", grade);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    string name = reader.GetString("name");
                    string label = reader.GetString("label");
                    int salary = reader.GetInt32("salary");
                    string skin_male = reader.GetString("skin_male");
                    string skin_female = reader.GetString("skin_female");

                    object[] BackArray = new object[] { name, label, salary, skin_male, skin_female};

                    reader.Close();

                    return BackArray;
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
