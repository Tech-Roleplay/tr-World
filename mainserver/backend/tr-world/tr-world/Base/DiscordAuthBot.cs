using System.Threading.Tasks;
using DSharpPlus;
using MongoDB.Driver;
using MongoDB.Bson;
using tr_world.Config;

public class DiscordAuthBot
{
    private DiscordClient _client;
    private IMongoCollection<BsonDocument> _codes;

    public async Task StartAsync()
    {
        // Mongo verbinden
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var db = mongoClient.GetDatabase("techroleplay");
        _codes = db.GetCollection<BsonDocument>("login_codes");

        // Bot konfigurieren
        _client = new DiscordClient(new DiscordConfiguration
        {
            Token = secret.discordBot_Token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });

        await _client.ConnectAsync();

        // Code-Loop starten
        _ = Task.Run(CheckCodesLoop);
    }

    private async Task CheckCodesLoop()
    {
        while (true)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("sent", false);
            var newCodes = await _codes.Find(filter).ToListAsync();

            foreach (var codeDoc in newCodes)
            {
                var discordId = codeDoc["discordId"].AsInt64;
                var code = codeDoc["code"].AsString;
                var id = codeDoc["_id"];

                var user = await _client.GetUserAsync((ulong)discordId);
                if (user != null)
                {
                    var dm = await _client.GetChannelAsync(user.Id);
                    await dm.SendMessageAsync($"🔑 Dein Login-Code lautet: **{code}**");
                    var update = Builders<BsonDocument>.Update.Set("sent", true);
                    await _codes.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", id), update);
                }
            }

            await Task.Delay(1000); // 1 Sekunde warten
        }
    }
}