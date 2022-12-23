using System;
using System.Diagnostics;
using System.IO;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core;

public static class ItemLoader
{
    private static Texture2D _texturePink;
    private static Texture2D _textureRed;
    private static ContentManager _content;
    
    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        _content = content;
        try
        {
            _texturePink = _content.Load<Texture2D>("sprites/spritesheet");
            _textureRed = _content.Load<Texture2D>("sprites/spritesheet_red");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine("Asset not found");
        }
    }
     public static ProjectileShot CreateLaserShot(Weapon weapon, int aimDirection)
     {
         return new ProjectileShot(_texturePink, new Rectangle(19 * 16, 5 * 16, 16, 16), weapon, aimDirection);
     }

    public static Weapon CreateSwordWeapon(Vector2 position)
    {
        return new Weapon(position, _texturePink, AnimationLoader.CreateSwordAnimations(), new Rectangle(13 * 16, 6 * 16, 16, 16))
            {
                DamagePoints = 1
            };
    }
    public static Weapon CreatePhaserWeapon(Vector2 position)
    {
        return new Weapon(position, _texturePink, AnimationLoader.CreatePhaserAnimations(), new Rectangle(16 * 16, 5 * 16, 16, 16), WeaponType.RANGE)
            {
                DamagePoints = 3
            };
    }
    
    public static Weapon CreateKnifeWeapon(Vector2 position)
    {
        return new Weapon(position, _texturePink, AnimationLoader.CreateKnifeAnimations(), new Rectangle(13 * 16, 5 * 16, 16, 16))
        {
            DamagePoints = 2.5f
        };
    }
    
    public static Weapon CreateCrowbarWeapon(Vector2 position)
    {
        return new Weapon(position, _texturePink, AnimationLoader.CreateCrowbarAnimations(), new Rectangle(16 * 16, 6 * 16, 16, 16))
        {
            DamagePoints = 3.5f
        };
    }
    
    public static Weapon CreateStickWeapon(Vector2 position)
    {
        return new Weapon(position, _texturePink, AnimationLoader.CreateStickAnimations(), new Rectangle(19 * 16, 6 * 16, 16, 16))
        {
            DamagePoints = 2
        };
    }
    public static Consumable CreateHealthPotion(Vector2 position)
    {
        return new Consumable(position, _texturePink, new Rectangle(19*16, 4*16, 16,16))
        {
            HealMultiplier = 0.3f
        };
    }
    public static Consumable CreateCake(Vector2 position)
    {
        return new Consumable(position, _texturePink, new Rectangle(21*16, 4*16, 16,16))
        {
            HealMultiplier = 1
        };
    }
    public static Consumable CreateArmorPotion(Vector2 position)
    {
        return new Consumable(position, _texturePink, new Rectangle(20*16, 4*16, 16,16))
        {
            HealMultiplier = 0,
            ArmorPoints = 2
        };
    }

    public static Trinket CreateSwimmingGoggles(Vector2 position)
    {
        return new Trinket(position, _texturePink, new Rectangle(18*16, 3*16, 16,16));
    }
    
    public static Currency CreateCoin(Vector2 position)
    {
        return new Currency(position, _texturePink, new Rectangle(17*16, 4*16, 16,16));
    }
    
    public static Currency CreateKey(Vector2 position)
    {
        return new Currency(position, _texturePink, new Rectangle(18*16, 4*16, 16,16));
    }
}