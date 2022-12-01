﻿using System.Collections.Generic;
using System.IO;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Animations;

public static class AnimationLoader
{
    private static ContentManager _content;
    private static Texture2D _texture;
    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        _content = content;
        _texture = _content.Load<Texture2D>("sprites/spritesheet");
    }
    public static Dictionary<string, Animation> CreatePlayerAnimations()
    {
        return new Dictionary<string, Animation>()
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
                "Hurt",
                new Animation(_texture, 16, 16, 2, new Vector2(2, 6), false)
            },
            {
                "Death",
                new Animation(_texture, 16, 16, 1, new Vector2(4, 6), true)
            },
            {
                "Default",
                new Animation(_texture, 16, 16, 7, new Vector2(1, 2), true)
            }
        };
    }
    public static Dictionary<string, Animation> CreateSwordAnimations()
    {
        return new Dictionary<string, Animation>()
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
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 6), false)
            }
        };
    }
    public static Dictionary<string, Animation> CreatePhaserAnimations()
    {
        return new Dictionary<string, Animation>()
        {
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 5), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 5), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 5), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 5), false)
            }
        };
    }
    public static Dictionary<string, Animation> CreateKnifeAnimations()
    {
        return new Dictionary<string, Animation>()
        {
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 5), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 5), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 5), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(13, 5), false)
            }
        };
    }
    public static Dictionary<string, Animation> CreateStickAnimations()
    {
       return new Dictionary<string, Animation>()
        {
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(19, 6), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(19, 6), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 3, new Vector2(19, 6), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(19, 6), false)
            }
        };
    }
    public static Dictionary<string, Animation> CreateCrowbarAnimations()
    {
        return new Dictionary<string, Animation>()
        {
            {
                "AttackRight",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 6), false)
            },
            {
                "AttackLeft",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 6), false, true, false)
            },
            {
                "AttackUp",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 6), false)
            },
            {
                "AttackDown",
                new Animation(_texture, 16, 16, 3, new Vector2(16, 6), false)
            }
        };
    }
    
    public static ProjectileShot CreateLaserShot(Weapon weapon, int aimDirection)
    {
        return new ProjectileShot(_texture, new Rectangle(19 * 16, 5 * 16, 16, 16), weapon, aimDirection);
    }
}