using System;
using System.Collections.Generic;
using System.Linq;

namespace tr_world.Models;

public class Inventory
{
    private readonly List<Item> _items = new();

    // Füge ein Item hinzu
    public void AddItem(Item item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
            existingItem.Quantity += item.Quantity;
        else
            _items.Add(item);
    }

    // Entferne ein Item
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

    public void UseItem(int itemId, float quantity, ItemType itemType)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            Console.WriteLine($"Item mit ID {itemId} nicht gefunden.");
            return;
        }

        if (item.Quantity < quantity)
        {
            Console.WriteLine(
                $"Nicht genügend von {item.Name} vorhanden. Benötigt: {quantity}, Verfügbar: {item.Quantity}.");
            return;
        }

        // Aktionen basierend auf dem ItemType
        switch (itemType)
        {
            case ItemType.Consum:
                Console.WriteLine($"Du hast {quantity}x {item.Name} konsumiert.");
                // Beispiel: Effekte anwenden, Spieler-Status ändern etc.
                //ApplyConsumableEffect(item, quantity);
                break;

            case ItemType.Weapon:
                Console.WriteLine($"Du hast {item.Name} als Waffe verwendet.");
                // Beispiel: Munition laden, Waffe ausrüsten etc.
                //EquipWeapon(item);
                break;

            case ItemType.Equipment:
                Console.WriteLine($"Du hast {item.Name} ausgerüstet.");
                // Beispiel: Spieler-Stärke erhöhen, Kleidung ausrüsten etc.
                //EquipEquipment(item);
                break;

            default:
                Console.WriteLine($"Item-Typ {itemType} wird nicht unterstützt.");
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
    public List<Item> GetItems()
    {
        return _items;
    }
}

public class Item
{
    public Item(int id, string name, string description, float quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Quantity = quantity;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }
    public string Description { get; set; }
    public float Quantity { get; set; }
}

public enum ItemType
{
    Consum,
    Weapon,
    Equipment
}