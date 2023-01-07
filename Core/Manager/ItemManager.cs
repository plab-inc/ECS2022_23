using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
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
               DropRandomLoot(enemy.Position);
            }
        }
        else
        {
            DropRandomLoot(enemy.Position);
        }
    }

    private static void DropRandomLoot(Vector2 position)
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

    private static void DropKey(Vector2 position)
    {
        AddItem(ItemLoader.CreateKey(position));
        
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
                    player.Weapon = (Weapon) item;
                    InventoryManager.AddItem(item);
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
        catch (NullReferenceException e)
        {
            _activeItems = new List<Item>();
        }
    }
}