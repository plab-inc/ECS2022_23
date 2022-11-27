using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Animations;

public static class AnimationLoader
{
    private static ContentManager _content;
    private static Texture2D _texture;
    public static Dictionary<string, Animation> PlayerAnimations { get; private set; }
    public static Dictionary<string, Animation> SwordAnimations { get; private set; }
    public static Animation KnifeAnimation { get; private set; }
    public static Animation StickAnimation { get; private set; }
    public static Animation PhaserAnimation { get; private set; }
    public static Animation CrowbarAnimation { get; private set; }
    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        _content = content;
        _texture = _content.Load<Texture2D>("sprites/spritesheet");
        LoadPlayerAnimations();
        LoadWeaponAnimations();
        LoadSwordAnimations();
    }
    
    private static void LoadPlayerAnimations()
    {
        PlayerAnimations = new Dictionary<string, Animation>()
        {
            {
                "WalkUp",
                new Animation(_texture, 16, 16, 6, new Vector2(1, 5), true)
            },
            {
                "WalkRight",
                new Animation(_texture, 16, 16, 6, new Vector2(1, 4), true)
            },
            {
                "WalkLeft",
                new Animation(_texture, 16, 16, 6, new Vector2(1, 4), true, true,
                    false)
            },
            {
                "WalkDown",
                new Animation(_texture, 16, 16, 6, new Vector2(1, 3), true)
            },
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(6, 6), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(6, 6), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 2, new Vector2(9, 6), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 2, new Vector2(11, 6), false)
            },
            {
                "Default",
                new Animation(_texture, 16, 16, 7, new Vector2(1, 2), true)
            }
        };
    }

    private static void LoadWeaponAnimations()
    {
        CrowbarAnimation = new Animation(_texture, 16, 16, 3, new Vector2(16, 6), false);
        KnifeAnimation = new Animation(_texture, 16, 16, 3, new Vector2(13, 5), false);
        PhaserAnimation = new Animation(_texture, 16, 16, 3, new Vector2(16, 5), false);
        StickAnimation = new Animation(_texture, 16, 16, 3, new Vector2(19, 6), false);
    }

    private static void LoadSwordAnimations()
    {
        SwordAnimations = new Dictionary<string, Animation>()
        {
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false, false, false, -90)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false, false, false, 90)
            }
        };
    }
}