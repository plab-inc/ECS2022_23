using System;
using System.Collections.Generic;
using System.Diagnostics;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Combat;

public static class ItemManager
{
    private static List<Item> _activeItems = new List<Item>();
    public static void Update(GameTime gameTime)
    {
        
    }

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
        Debug.WriteLine("randomDrop: " + randomInt);
        if (randomInt < dropChance) return;
        
        randomInt = random.Next(10);
        var weaponChance = 4;
        Debug.WriteLine("random loot: " + randomInt);
        if (randomInt > weaponChance)
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
        var randomInt = random.Next(2);
        Debug.WriteLine("random weapon loot: " + randomInt);
        switch (randomInt)
        {
            case 0: return AnimationLoader.CreateSwordWeapon(position);
            case 1: return AnimationLoader.CreatePhaserWeapon(position);
            default: return AnimationLoader.CreateSwordWeapon(position);
        }
    }
    
    private static Consumable GetRandomConsumable(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(2);
        Debug.WriteLine("random cons loot: " + randomInt);
        switch (randomInt)
        {
            case 0: return AnimationLoader.CreateHealthPotion(position);
            case 1: return AnimationLoader.CreateArmorPotion(position);
            default: return AnimationLoader.CreateHealthPotion(position);
        }
    }

    public static void PickItemUp(Player player)
    {
        foreach (var item in _activeItems)
        {
            if (item.Rectangle.Intersects(player.Rectangle))
            {
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
                }
                _activeItems.Remove(item);
                return;
            }
        }
    }
}