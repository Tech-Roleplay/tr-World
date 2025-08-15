using System;
using AltV.Net;
using MongoDB.Driver;
using MySqlConnector;

public static class Databank
{
    public static IMongoClient Client;
    public static IMongoDatabase MongoDatabase;
    public static MySqlConnection SqlConnection;

    public static bool MongoConnected;
    public static bool MySQLConnected;

    private static readonly string Host = "localhost";
    private static readonly string Port = "27017";
    private static readonly string Username = "root";
    private static readonly string Password = "TheBlokker";
    private static readonly string PasswordMySQL = "";
    private static readonly string DatabaseName = "altv-server";
    private static readonly string MongoDBName = "tech-roleplay";

    public static void InitMongo()
    {
        try
        {
            string connectionString = string.IsNullOrWhiteSpace(Username)
                ? $"mongodb://{Host}:{Port}"
                : $"mongodb://{Username}:{Password}@{Host}:{Port}";

            Client = new MongoClient(connectionString);
            MongoDatabase = Client.GetDatabase(MongoDBName);

            MongoConnected = true;
            Alt.LogWarning("MongoDB connected.");
        }
        catch (Exception e)
        {
            MongoConnected = false;
            Alt.LogError("MongoDB failed.");
            Alt.LogError(e.ToString());
        }
    }

    public static void InitMySQL()
    {
        try
        {
            string sqlConnStr = $"SERVER={Host}; DATABASE={DatabaseName}; UID={Username}; Password={PasswordMySQL}";
            SqlConnection = new MySqlConnection(sqlConnStr);
            SqlConnection.Open();

            MySQLConnected = true;
            Alt.LogWarning("MySQL connected.");
        }
        catch (Exception e)
        {
            MySQLConnected = false;
            Alt.LogError("MySQL failed.");
            Alt.LogError(e.ToString());
        }
    }
}