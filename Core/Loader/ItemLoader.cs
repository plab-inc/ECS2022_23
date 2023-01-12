using System;
using System.Diagnostics;
using System.IO;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Loader;

public static class ItemLoader
{
    private static Texture2D _spritesheetPink;
    private static Texture2D _spritesheetRed;
    private static ContentManager _content;
    
    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        _content = content;
        try
        {
            _spritesheetPink = _content.Load<Texture2D>("sprites/spritesheet");
            _spritesheetRed = _content.Load<Texture2D>("sprites/spritesheet_red");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine("Asset not found");
        }
    }
     public static ProjectileShot CreateLaserShot(Weapon weapon, Direction aimDirection)
     {
         return new ProjectileShot(_spritesheetPink, new Rectangle(19 * 16, 5 * 16, 16, 16), weapon, aimDirection);
     }

     public static ProjectileShot CreateLaserShot(Enemy enemy)
     {
         return new ProjectileShot(enemy, _spritesheetPink, new Rectangle(19 * 16, 5 * 16, 16, 16),
             enemy.AimVector);
     }
     
     public static ProjectileShot CreateLaserShot(Vector2 position, Vector2 direction)
     {
         return new ProjectileShot(position, direction, _spritesheetPink, new Rectangle(19 * 16, 5 * 16, 16, 16));
     }
     
    public static Item CreateItem(Vector2 position, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.UmlDiagram:
            {
                return new UmlDiagram(position, _spritesheetPink, new Rectangle(8 * 16, 3 * 16, 16, 16));
            }
            
            case ItemType.Cake:
                return new Consumable(position, _spritesheetPink, new Rectangle(21*16, 4*16, 16,16), itemType)
                {
                    HealMultiplier = 1
                };
            case ItemType.HealthPotion:
                return new Consumable(position, _spritesheetPink, new Rectangle(19*16, 4*16, 16,16), itemType)
                {
                    HealMultiplier = 0.3f
                };
            case ItemType.Armor:
                return new Consumable(position, _spritesheetPink, new Rectangle(20*16, 4*16, 16,16), itemType)
                {
                    HealMultiplier = 0,
                    ArmorPoints = 2
                };
            case ItemType.SwimmingGoggles:
                return new Trinket(position, _spritesheetPink, new Rectangle(18*16, 3*16, 16,16), itemType);
            case ItemType.Key:
                return new Key(position, _spritesheetPink, new Rectangle(18*16, 4*16, 16,16));
            case ItemType.Sword:
                return new Weapon(position, _spritesheetPink, AnimationLoader.CreateSwordAnimations(),
                    new Rectangle(13 * 16, 6 * 16, 16, 16), itemType,1);
            case ItemType.Phaser:
                return new Weapon(position, _spritesheetPink, AnimationLoader.CreatePhaserAnimations(), 
                    new Rectangle(16 * 16, 5 * 16, 16, 16), itemType, 3, WeaponType.Range);
            case ItemType.Knife:
                return new Weapon(position, _spritesheetPink, AnimationLoader.CreateKnifeAnimations(), 
                    new Rectangle(13 * 16, 5 * 16, 16, 16), itemType, 2.5f);
            case ItemType.Crowbar:
                return new Weapon(position, _spritesheetPink, AnimationLoader.CreateCrowbarAnimations(),
                    new Rectangle(16 * 16, 6 * 16, 16, 16), itemType,3.5f);
            case ItemType.Stick:
                return new Weapon(position, _spritesheetPink, AnimationLoader.CreateStickAnimations(), 
                    new Rectangle(19 * 16, 6 * 16, 16, 16), itemType, 2);
        }


        throw new InvalidOperationException();
    }
}