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
        public static object[] LoadJobDetailsFromDb(string jobname)
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

                    object[] backArray = new object[] {label,  requireInvitation, whitlistedjob};

                    reader.Close();

                    return backArray;                    
                }
            }
            catch (Exception e)
			{
                Alt.LogError("ERROR == ERROR == ERROR");
                Alt.LogError(e.ToString());

                return null;
            }
        }
        public static object[] LoadJobGradeFromDb(string jobname, int grade)
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
                    uint salary = reader.GetUInt32("salary");
                    string skinMale = reader.GetString("skin_male");
                    string skinFemale = reader.GetString("skin_female");

                    object[] backArray = new object[] { name, label, salary, skinMale, skinFemale};

                    reader.Close();

                    return backArray;
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
