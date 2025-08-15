using AltV.Net;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;

namespace trWorld.Base;

// Discord Class
public class Discord : IScript
{
    
    
    
    private static readonly HttpClient HttpClient = new HttpClient();

    [ClientEvent("token")]
    public static async void OAuth(IPlayer player, string token)
    {
        try
        {
            // Erstelle die Anfrage an die Discord API
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://discordapp.com/api/users/@me");
            requestMessage.Headers.Add("Authorization", $"Bearer {token}");
            requestMessage.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await HttpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                player.Kick("Authorization failed");
                return;
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseData);

            if (json["id"] == null || json["username"] == null)
            {
                player.Kick("Authorization failed");
                return;
            }

            // Beispielhafte Ausgabe der erhaltenen Daten
            Alt.Log($"Id: {json["id"]}");
            Alt.Log($"Name: {json["username"]}#{json["discriminator"]}");
        }
        catch (Exception ex)
        {
            Alt.LogError($"Error during token validation: {ex.Message}");
            player.Kick("Authorization failed");
        }
    }
}