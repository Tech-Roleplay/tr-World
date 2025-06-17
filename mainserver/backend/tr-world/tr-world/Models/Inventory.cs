using System;
using System.Collections.Generic;
using System.Linq;
using AltV.Net;

namespace trWorld.Models;

/// <summary>
/// Represents an inventory system to manage items.
/// </summary>
public class Inventory
{
    /// <summary>
    /// Represents a private list that holds items in the inventory.
    /// </summary>
    /// <remarks>
    /// The variable maintains a collection of <see cref="Item"/> objects.
    /// Items can be added, removed, or modified through the provided methods in the <see cref="Inventory"/> class.
    /// </remarks>
    private readonly List<Item> _items = new();

    // Füge ein Item hinzu
    /// <summary>
    /// Adds an item to the inventory. If the item already exists in the inventory,
    /// it will increase the quantity of the existing item. Otherwise, it will add the item as a new entry.
    /// </summary>
    /// <param name="item">The item to be added to the inventory. This should include the item’s ID, name, description, quantity, and type.</param>
    public void AddItem(Item item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
            existingItem.Quantity += item.Quantity;
        else
            _items.Add(item);
    }

    // Entferne ein Item
    /// <summary>
    /// Removes a specified quantity of an item from the inventory.
    /// If the item's quantity becomes zero or less, it will be removed from the inventory.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to be removed.</param>
    /// <param name="quantity">The quantity of the item to be removed.</param>
    /// <returns>
    /// A boolean value indicating whether the removal was successful.
    /// Returns true if the item was successfully removed in the specified quantity, otherwise false.
    /// </returns>
    public bool RemoveItem(int itemId, float quantity)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null && item.Quantity >= quantity)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                _items.Remove(item);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Uses an item from the inventory based on its ID and type.
    /// Executes actions depending on the item's type and adjusts its quantity.
    /// </summary>
    /// <param name="itemId">The ID of the item to be used.</param>
    /// <param name="quantity">The quantity of the item to use.</param>
    /// <param name="itemType">The type of the item to determine its action (e.g., Consumable, Weapon, Equipment).</param>
    public void UseItem(int itemId, float quantity, ItemType itemType)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            //Console.WriteLine($"Item mit ID {itemId} nicht gefunden.");
            return;
        }

        if (item.Quantity < quantity)
        {
            //Console.WriteLine($"Nicht genügend von {item.Name} vorhanden. Benötigt: {quantity}, Verfügbar: {item.Quantity}.");
            return;
        }

        // Aktionen basierend auf den Itemtypen
        switch (itemType)
        {
            case ItemType.Consum:
                //Console.WriteLine($"Du hast {quantity}x {item.Name} konsumiert.");
                // Beispiel: Effekte anwenden, Spieler-Status ändern etc.
                //ApplyConsumableEffect(item, quantity);
                break;

            case ItemType.Weapon:
                //Console.WriteLine($"Du hast {item.Name} als Waffe verwendet.");
                // Beispiel: Munition laden, Waffe ausrüsten etc.
                //EquipWeapon(item);
                break;

            case ItemType.Equipment:
                //Console.WriteLine($"Du hast {item.Name} ausgerüstet.");
                // Beispiel: Spieler-Stärke erhöhen, Kleidung ausrüsten etc.
                //EquipEquipment(item);
                break;

            default:
                //WriteLine($"Item-Typ {itemType} wird nicht unterstützt.");
                return;
        }

        // Reduzierung der Menge
        item.Quantity -= quantity;
        if (item.Quantity <= 0)
        {
            _items.Remove(item);
            Console.WriteLine($"{item.Name} wurde komplett aufgebraucht.");
        }
    }

    // Hol alle Items
    /// <summary>
    /// Retrieves the list of all items currently present in the inventory.
    /// </summary>
    /// <returns>
    /// A list of items in the inventory.
    /// </returns>
    public List<Item> GetItems()
    {
        return _items;
    }
}

/// <summary>
/// Represents an individual item within an inventory.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the attributes and behavior of an item.
/// Items can belong to specific categories defined by the <see cref="ItemType"/> enum.
/// </remarks>
public class Item
{
    /// <summary>
    /// Represents an item with an ID, name, description, quantity, and type.
    /// </summary>
    public Item(int id, string name, string description, float quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Quantity = quantity;
    }

    /// <summary>
    /// Represents the unique identifier of an item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents the type of item in the inventory.
    /// </summary>
    /// <remarks>
    /// This property is used to categorize items into different functional types,
    /// such as consumable items, weapons, or equipment. It enables the inventory system to manage and process items according to their specific uses.
    /// </remarks>
    public ItemType ItemType { get; set; }

    /// <summary>
    /// Gets or sets the description of the item.
    /// Provides textual information or details about the item,
    /// such as its purpose, usage, or appearance.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Represents the available amount of a specific item in the inventory.
    /// This value is dynamically updated when items are added, used, or removed.
    /// </summary>
    public float Quantity { get; set; }
    
    public float Weight { get; set; } 
    
}

/// <summary>
/// Defines the type of an item in the inventory system.
/// </summary>
/// <remarks>
/// This enumeration categorizes items based on their purpose or usage. For instance:
/// - Consum defines items that can be consumed, possibly providing effects or benefits to the player.
/// - Weapon defines items that can be used as weapons.
/// - Equipment defines items that can be equipped, such as armor or accessories.
/// </remarks>
public enum ItemType
{
    /// <summary>
    /// Represents items that can be consumed, such as food, potions, or other consumable resources.
    /// This item type typically decreases in quantity when used and may trigger specific effects
    /// or changes in the user's status.
    /// </summary>
    Consum,

    /// <summary>
    /// Represents an item type that is used as a weapon.
    /// Items classified as weapons may be used in combat or for dealing damage.
    /// </summary>
    Weapon,

    /// <summary>
    /// Represents an item of type equipment within the inventory system.
    /// Equipment items are typically used to enhance or modify the player's capabilities,
    /// such as providing armor, accessories, or skill-related boosts when equipped.
    /// </summary>
    Equipment
}