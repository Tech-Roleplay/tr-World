using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Shared.Utils;
using MySqlConnector;

namespace tr_world
{
    // lot of code for basic constructer from here https://github.com/nSystemz/AltV-C-Tutorial-NemesusTV/blob/main/resources/AltVTutorial/AltVTutorial/Datenbank.cs
    public class Databank : Main 
    {
        public static bool DatabankConnection = false;
        public static MySqlConnection Connection;
        public string Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }

        public Databank()
        {
            this.Host = "localhost";
            this.Username = "root";
            this.Password = "";
            this.Database = "altv-server";
        }

        public static String GetConnectionString()
        {
            Databank sql = new Databank();
            String SQLConnection = $"SERVER={sql.Host}; DATABASE={sql.Database}; UID={sql.Username}; Password={sql.Password}";
            return SQLConnection;
        }

        public static void InitConnection()
        {
            String SQLConnection = GetConnectionString();
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
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }

    }
}
