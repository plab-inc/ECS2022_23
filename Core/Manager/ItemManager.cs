using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Combat;

public static class ItemManager
{
    private static List<Item> _activeItems = new List<Item>();
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var item in _activeItems)
        {
            item.DrawIcon(spriteBatch);
        }
    }

    private static void AddItem(Item item)
    {
        _activeItems.Add(item);
    }

    public static void DropRandomLoot(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(10);
        var dropChance = 0;
        
        if (randomInt < dropChance) return;
        
        randomInt = random.Next(10);
        var weaponChance = 5;
        var trinketChance = 2;

        if (randomInt < trinketChance)
        {
            AddItem(GetRandomTrinket(position));
        } else if (randomInt < weaponChance)
        {
            AddItem(GetRandomWeapon(position));
        }
        else
        {
            AddItem(GetRandomConsumable(position));
        }
    }

    private static Weapon GetRandomWeapon(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(5);
        switch (randomInt)
        {
            case 0: return ItemLoader.CreateSwordWeapon(position);
            case 1: return ItemLoader.CreatePhaserWeapon(position);
            case 2: return ItemLoader.CreateCrowbarWeapon(position);
            case 3: return ItemLoader.CreateKnifeWeapon(position);
            case 4: return ItemLoader.CreateStickWeapon(position);
            default: return ItemLoader.CreateSwordWeapon(position);
        }
    }
    
    private static Consumable GetRandomConsumable(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(3);
        switch (randomInt)
        {
            case 0: return ItemLoader.CreateHealthPotion(position);
            case 1: return ItemLoader.CreateArmorPotion(position);
            case 2: return ItemLoader.CreateCake(position);
            default: return ItemLoader.CreateHealthPotion(position);
        }
    }
    
    private static Trinket GetRandomTrinket(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(1);
        switch (randomInt)
        {
            case 0: return ItemLoader.CreateSwimmingGoggles(position);
            default: return ItemLoader.CreateSwimmingGoggles(position);
        }
    }

    public static void PickItemUp(Player player)
    {
        foreach (var item in _activeItems)
        {
            if (!item.Rectangle.Intersects(player.Rectangle)) continue;
            if (item.GetType() == typeof(Weapon))
            {
                var weapon = player.Weapon;
                weapon.Position = player.Position;
                _activeItems.Add(weapon);
                player.Weapon = (Weapon) item;
            }
            else
            {
                player.AddItem(item);
                InventoryManager.AddItem(item);
            }
            _activeItems.Remove(item);
            return;
        }
    }
}