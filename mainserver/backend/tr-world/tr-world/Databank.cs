using System;
using System.Threading;
using AltV.Net;
using MySqlConnector;

namespace trWorld;

// lot of code for basic constructer from here https://github.com/nSystemz/AltV-C-Tutorial-NemesusTV/blob/main/resources/AltVTutorial/AltVTutorial/Datenbank.cs
public class Databank : Main
{
    public static bool DatabankConnection;
    public static MySqlConnection Connection;

    public Databank()
    {
        Host = "localhost";
        Username = "root";
        Password = "";
        Database = "altv-server";
    }

    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }

    public static string GetConnectionString()
    {
        var sql = new Databank();
        var SQLConnection = $"SERVER={sql.Host}; DATABASE={sql.Database}; UID={sql.Username}; Password={sql.Password}";
        return SQLConnection;
    }

    public static void InitConnection()
    {
        var SQLConnection = GetConnectionString();
        Connection = new MySqlConnection(SQLConnection);
        try
        {
            Connection.Open();
            DatabankConnection = true;
            Alt.LogWarning("MYSQL connection established successfully!");
        }
        catch (Exception e)
        {
            DatabankConnection = false;
            Alt.LogError("MYSQL connection could not be established.");
            Alt.LogError(e.ToString());
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}