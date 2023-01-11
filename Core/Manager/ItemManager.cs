using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class ItemManager
{
    private static List<Item> _activeItems = new List<Item>();
    private static bool _keyHasDropped = false;
    
    public static void Init()
    {
        _keyHasDropped = false;
        _activeItems = new List<Item>();
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        try
        {
            foreach (var item in _activeItems)
            {
                item.DrawIcon(spriteBatch);
            }
        }
        catch (NullReferenceException e)
        {
            _activeItems = new List<Item>();
        }
    }

    private static void AddItem(Item item)
    {
        try
        {
            _activeItems.Add(item);
        }  catch (NullReferenceException e)
        {
            _activeItems = new List<Item>();
        }
       
    }
    public static void DropLoot(Enemy enemy) 
    {
        if (!_keyHasDropped)
        {
            var enemyDropsKey = EnemyManager.EnemyDropsKey(enemy);
            
            if (enemyDropsKey)
            {
                DropKey(enemy.Position);
                _keyHasDropped = true;
            }
            else
            {
               DropRandomLoot(enemy.Position, enemy.ItemSpawnRate);
            }
        }
        else
        {
            DropRandomLoot(enemy.Position, enemy.ItemSpawnRate);
        }
    }

    private static void DropRandomLoot(Vector2 position, float dropChance)
    {
        var random = new Random();
        var randomFloat = random.Next(10) * 0.1f;
        
        if (randomFloat > dropChance) return;
        
        randomFloat = random.Next(10) * 0.1f;
        var weaponChance = 0.4f;
        var trinketChance = 0.2f;
 
        if (randomFloat < trinketChance)
        {
            AddItem(GetRandomTrinket(position));
        } else if (randomFloat < weaponChance)
        {
            AddItem(GetRandomWeapon(position));
        }
        else
        {
            AddItem(GetRandomConsumable(position));
        }
    }

    private static void DropKey(Vector2 position)
    {
        AddItem(ItemLoader.CreateItem(position, ItemType.Key));
        
    }

    private static Weapon GetRandomWeapon(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(5);
        switch (randomInt)
        {
            case 0: return (Weapon) ItemLoader.CreateItem(position, ItemType.Sword);
            case 1: return (Weapon) ItemLoader.CreateItem(position, ItemType.Phaser);
            case 2: return (Weapon) ItemLoader.CreateItem(position, ItemType.Crowbar);
            case 3: return (Weapon) ItemLoader.CreateItem(position, ItemType.Knife);
            case 4: return (Weapon) ItemLoader.CreateItem(position, ItemType.Stick);
            default: return (Weapon) ItemLoader.CreateItem(position, ItemType.Sword);
        }
    }
    
    private static Consumable GetRandomConsumable(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(3);
        switch (randomInt)
        {
            case 0: return (Consumable) ItemLoader.CreateItem(position, ItemType.HealthPotion);
            case 1: return (Consumable) ItemLoader.CreateItem(position, ItemType.Armor);
            case 2: return (Consumable) ItemLoader.CreateItem(position, ItemType.Cake);
            default: return (Consumable) ItemLoader.CreateItem(position, ItemType.HealthPotion);
        }
    }
    
    private static Trinket GetRandomTrinket(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(1);
        switch (randomInt)
        {
            default: return (Trinket) ItemLoader.CreateItem(position, ItemType.SwimmingGoggles);

        }
    }

    public static void PickItemUp(Player player)
    {
        try
        {
            foreach (var item in _activeItems)
            {
                if (!item.Rectangle.Intersects(player.Rectangle)) continue;
                if (item.GetType() == typeof(Weapon))
                {
                    if (player.Weapon != null)
                    {
                        var weapon = player.Weapon;
                        weapon.Position = player.Position;
                        _activeItems.Add(weapon);
                    }
                    InventoryManager.AddItem(item);
                }
                else
                {
                    InventoryManager.AddItem(item);
                }
                _activeItems.Remove(item);
                return;
            } 
        }
        catch (NullReferenceException e)
        {
            _activeItems = new List<Item>();
        }
    }
}