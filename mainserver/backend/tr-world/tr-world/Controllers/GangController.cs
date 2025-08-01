﻿using System;
using AltV.Net;

namespace trWorld.Controllers;

public class ReturnGangClass
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string SkinMale { get; set; }
    public string SkinFemale { get; set; }
}

public class GangController : IScript
{
    public static string LoadGangDetailsFromDb(string gangName)
    {
        try
        {
            var cmd = Databank.SqlConnection.CreateCommand();

            cmd.CommandText = "SELECT * FROM gangs WHERE name=@name LIMIT 1";
            cmd.Parameters.AddWithValue("@name", gangName);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                var label = reader.GetString("label");

                if (label != null) label = "No Gang";

                reader.Close();

                return label;
            }
        }
        catch (Exception e)
        {
            Alt.LogError("ERROR == ERROR == ERROR");
            Alt.LogError(e.ToString());

            return null;
        }
    }


    public static ReturnGangClass LoadGangGradeFromDb(string gangName, int grade)
    {
        ReturnGangClass returnGangClass = new();
        try
        {
            var cmd = Databank.SqlConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM gang_grades WHERE gang_name=@gang_name AND grade=@grade";
            cmd.Parameters.AddWithValue("@gang_name", gangName);
            cmd.Parameters.AddWithValue("@grade", grade);

            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();

                returnGangClass.Name = reader.GetString("name");
                returnGangClass.Label = reader.GetString("label");
                returnGangClass.SkinMale = reader.GetString("skin_male");
                returnGangClass.SkinFemale = reader.GetString("skin_female");

                reader.Close();

                return returnGangClass;
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