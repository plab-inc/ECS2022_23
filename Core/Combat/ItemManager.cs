using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
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
        var weaponChance = 4;

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
        var randomInt = random.Next(5);
        switch (randomInt)
        {
            case 0: return AnimationLoader.CreateSwordWeapon(position);
            case 1: return AnimationLoader.CreatePhaserWeapon(position);
            case 2: return AnimationLoader.CreateCrowbarWeapon(position);
            case 3: return AnimationLoader.CreateKnifeWeapon(position);
            case 4: return AnimationLoader.CreateStickWeapon(position);
            default: return AnimationLoader.CreateSwordWeapon(position);
        }
    }
    
    private static Consumable GetRandomConsumable(Vector2 position)
    {
        var random = new Random();
        var randomInt = random.Next(3);
        switch (randomInt)
        {
            case 0: return AnimationLoader.CreateHealthPotion(position);
            case 1: return AnimationLoader.CreateArmorPotion(position);
            case 2: return AnimationLoader.CreateCake(position);
            default: return AnimationLoader.CreateHealthPotion(position);
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
            }
            _activeItems.Remove(item);
            return;
        }
    }
}