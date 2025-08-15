using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using MongoDB.Bson;
using MongoDB.Driver;

public class LoginService : IScript
{
    private readonly IMongoCollection<BsonDocument> _codes;

    public LoginService()
    {
        
        _codes = Databank.MongoDatabase.GetCollection<BsonDocument>("login_codes");
    }

    [Command("login")]
    public void LoginCommand(IPlayer player, string discordTag)
    {
        var code = new Random().Next(100000, 999999).ToString();

        var doc = new BsonDocument
        {
            { "ingameName", player.Name },
            { "discordTag", discordTag },
            { "code", code },
            { "createdAt", DateTime.UtcNow },
            { "used", false },
            { "sent", false }
        };

        _codes.InsertOne(doc);

        player.SendChatMessage("📨 Ein Login-Code wurde an deinen Discord gesendet. Bitte überprüfe deine DMs!");
    }
    
    [Command("verify")]
    public void VerifyCommand(IPlayer player, string code)
    {
        var filter = Builders<BsonDocument>.Filter.And(
            Builders<BsonDocument>.Filter.Eq("ingameName", player.Name),
            Builders<BsonDocument>.Filter.Eq("code", code),
            Builders<BsonDocument>.Filter.Eq("used", false),
            Builders<BsonDocument>.Filter.Gt("createdAt", DateTime.UtcNow.AddMinutes(-5))
        );

        var doc = _codes.Find(filter).FirstOrDefault();
        if (doc != null)
        {
            var update = Builders<BsonDocument>.Update.Set("used", true);
            _codes.UpdateOne(filter, update);

            player.SendChatMessage("✅ Login erfolgreich!");
            // Hier Spieler-Session setzen
        }
        else
        {
            player.SendChatMessage("❌ Ungültiger oder abgelaufener Code.");
        }
    }

}