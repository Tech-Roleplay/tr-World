using AltV.Net.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using trWorld.Models;
using trWorld.Player;

namespace trWorld.Controllers;

public class TPlayerModel
{
    [BsonId] // Marks this as the MongoDB _id
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int Permission { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public char Sex { get; set; }
    public int Height { get; set; }
    public string Skin { get; set; }
    public Position Position { get; set; }
    public TMetadata Metadata { get; set; }
    public string Backstory { get; set; }
    public TJob Job { get; set; }
    public TGang Gang { get; set; }
    public Phone Phone { get; set; }

    public string SocialClubId { get; set; }

    public string Name { get; set; }
    // weitere Felder...
}


public static class MongoTPlayerController
{
    private static readonly IMongoCollection<TPlayerModel> _players =
        Databank.MongoDatabase.GetCollection<TPlayerModel>("players");

    public static bool HasTPlayerAccount(string socialClubId)
    {
        var indexKeys = Builders<TPlayerModel>.IndexKeys.Ascending(p => p.SocialClubId);
        _players.Indexes.CreateOne(new CreateIndexModel<TPlayerModel>(indexKeys));
        return _players.CountDocuments(p => p.SocialClubId == socialClubId) > 0;
    }

    public static void CreateTPlayerAccount(TPlayer player)
    {
        var model = ConvertToModel(player);
        _players.InsertOne(model);
    }

    public static void SaveTPlayerData(TPlayer player)
    {
        var model = ConvertToModel(player);
        var filter = Builders<TPlayerModel>.Filter.Eq(p => p.SocialClubId, model.SocialClubId);
        var update = Builders<TPlayerModel>.Update
            .Set(p => p.Name, model.Name)
            .Set(p => p.Permission, model.Permission)
            .Set(p => p.Firstname, model.Firstname)
            .Set(p => p.Surname, model.Surname)
            .Set(p => p.Sex, model.Sex)
            .Set(p => p.Height, model.Height)
            .Set(p => p.Skin, model.Skin)
            .Set(p => p.Position, model.Position)
            .Set(p => p.Metadata, model.Metadata)
            .Set(p => p.Backstory, model.Backstory)
            .Set(p => p.Job, model.Job)
            .Set(p => p.Gang, model.Gang)
            .Set(p => p.Phone, model.Phone);
    
        _players.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

    }

    public static bool LoadTPlayerData(TPlayer player)
    {
        var model = _players.Find(p => p.SocialClubId == player.SocialClubId.ToString()).FirstOrDefault();
        if (model == null) return false;

        // Eigenschaften zurückschreiben
        //player.Name = model.Name;
        player.Permission = model.Permission;
        player.Firstname = model.Firstname;
        player.Surname = model.Surname;
        player.Sex = model.Sex;
        player.Height = model.Height;
        player.Skin = model.Skin;
        player.Position = model.Position;
        player.Metadata = model.Metadata;
        player.Backstory = model.Backstory;
        player.Job = model.Job;
        player.Gang = model.Gang;
        player.Phone = model.Phone;

        return true;
    }


    private static TPlayerModel ConvertToModel(TPlayer player)
    {
        return new TPlayerModel
        {
            SocialClubId = player.SocialClubId.ToString(),
            Name = player.Name,
            Permission = player.Permission,
            Firstname = player.Firstname,
            Surname = player.Surname,
            Sex = player.Sex,
            Height = player.Height,
            Skin = player.Skin,
            //Metadata.Status = "n/n",
            Position = player.Position,
            Metadata = player.Metadata,
           // Inventory = player.sInventory ?? new Inventory() "{}",
            Backstory = player.Backstory,
            Job = (TJob)player.Job,
            Gang = (TGang)player.Gang,
            Phone = player.Phone,
            //Phone.Number = player.Phone.Number
        };
    }
}