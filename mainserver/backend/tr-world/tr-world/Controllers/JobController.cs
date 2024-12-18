﻿using System;
using AltV.Net;

namespace tr_world.Controllers;

public abstract class JobController : IScript
{
    public static object[] LoadJobDetailsFromDb(string jobName)
    {
        try
        {
            var cmd = Databank.Connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM jobs WHERE name=@name LIMIT 1";
            cmd.Parameters.AddWithValue("@name", jobName);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                var label = reader.GetString("label");
                var requireInvitation = reader.GetBoolean("is_required_to_be_invited");
                var whitlistedjob = reader.GetBoolean("whitelisted");

                var backArray = new object[] { label, requireInvitation, whitlistedjob };

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
            var cmd = Databank.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM job_grades WHERE job_name=@job_name AND grade=@grade";
            cmd.Parameters.AddWithValue("@job_name", jobname);
            cmd.Parameters.AddWithValue("@grade", grade);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                var name = reader.GetString("name");
                var label = reader.GetString("label");
                var salary = reader.GetUInt32("salary");
                var skinMale = reader.GetString("skin_male");
                var skinFemale = reader.GetString("skin_female");

                var backArray = new object[] { name, label, salary, skinMale, skinFemale };

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

    public static void GetInventoryForJob()
    {
    }


    public static void GetVehicleListForJob()
    {
    }
}