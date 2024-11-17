﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net;

namespace tr_world.Models
{
    public class Shop : IScript
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Adress { get; set; }
        public string Inventory { get; set; }

        public Shop()
        {
            
        }
        public Shop(string name,string label, string description, string image, string adress, string inventory = null)
        {
            var shop = new Shop();
            shop.Name = name;
            shop.Label = label;
            shop.Description = description;
            shop.Image = image;
            shop.Adress = adress;
            shop.Inventory = inventory;    
        }
        
        [Obsolete("This method will be removed in future versions. Use the normal Constructor instead.")]
        public static void CreateShop(string name,string label, string description, string image, string adress, string inventory = null)
        {
            var shop = new Shop();
            shop.Name = name;
            shop.Label = label;
            shop.Description = description;
            shop.Image = image;
            shop.Adress = adress;
            shop.Inventory = inventory;

        }
        
    }
}
