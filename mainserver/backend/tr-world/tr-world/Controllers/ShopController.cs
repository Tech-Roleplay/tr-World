using AltV.Net;
using AltV.Net.Elements.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_world.Models;

namespace tr_world.Controllers
{
    public class ShopController : IScript
    {
        public static Shop LoadShop(string shopname)
        {
            try
			{
                Shop shop = new Shop();
                MySqlCommand cmd = Databank.Connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM shops WHERE name=@name LIMIT 1";

                cmd.Parameters.AddWithValue("@name", shopname);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                { 
                    if(reader.HasRows)
                    {
                        reader.Read();
                        shop.Label = reader.GetString("label");
                        shop.Description = reader.GetString("description");
                        shop.Image = reader.GetString("image_url");
                        shop.Adress = reader.GetString("adress");
                        shop.Inventory = reader.GetString("inventory");

                        reader.Close();

                        Alt.Log($"Sucessfully loading playerdata for (SHOPNAME)!");
                        return shop;

                    }
                    // if has no row
                    return null;
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
