using System.Collections.Generic;
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
    
    public static Dictionary<string, Animation> CreateBlobEnemyAnimations()
    {
        return new Dictionary<string, Animation>()
        {
            {
                "Walk",
                new Animation(_texture, 16, 16, 2, new Vector2(10, 1), true)
            },
            {
                "WalkRight",
                new Animation(_texture, 16, 16, 2, new Vector2(10, 1), true, true, false)
            },
            {
                "WalkLeft",
                new Animation(_texture, 16, 16, 2, new Vector2(10, 1), true)
            },
            {
                "Attack",
                new Animation(_texture, 16, 16, 5, new Vector2(10, 1), false)
            },
            {
                "Hurt",
                new Animation(_texture, 16, 16, 1, new Vector2(15, 1), false)
            },
            {
                "Death",
                new Animation(_texture, 16, 16, 1, new Vector2(14, 1), true)
            },
            {
                "Default",
                new Animation(_texture, 16, 16, 3, new Vector2(10, 1), true)
            }
        };
    }
    
    public static Dictionary<string, Animation> CreateEyeEnemyAnimations()
    {
        return new Dictionary<string, Animation>()
        {
            {
                "Walk",
                new Animation(_texture, 16, 16, 4, new Vector2(10, 2), true)
            },
            {
                "WalkRight",
                new Animation(_texture, 16, 16, 4, new Vector2(10, 2), true, true, false)
            },
            {
                "WalkLeft",
                new Animation(_texture, 16, 16, 4, new Vector2(10, 2), true)
            },
            {
                "Attack",
                new Animation(_texture, 16, 16, 2, new Vector2(12, 2), false)
            },
            {
                "Hurt",
                new Animation(_texture, 16, 16, 1, new Vector2(14, 2), false)
            },
            {
                "Death",
                new Animation(_texture, 16, 16, 1, new Vector2(15, 2), true)
            },
            {
                "Default",
                new Animation(_texture, 16, 16, 4, new Vector2(10, 2), true)
            }
        };
    }

    private static Dictionary<string, Animation> CreateSwordAnimations()
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

    private static Dictionary<string, Animation> CreatePhaserAnimations()
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

    private static Dictionary<string, Animation> CreateKnifeAnimations()
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

    private static Dictionary<string, Animation> CreateStickAnimations()
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

    private static Dictionary<string, Animation> CreateCrowbarAnimations()
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

    public static Weapon CreateSwordWeapon(Vector2 position)
    {
        return new Weapon(position, _texture, CreateSwordAnimations(), new Rectangle(13 * 16, 6 * 16, 16, 16));
    }
    public static Weapon CreatePhaserWeapon(Vector2 position)
    {
        return new Weapon(position, _texture, CreatePhaserAnimations(), new Rectangle(16 * 16, 5 * 16, 16, 16), WeaponType.RANGE, 32);
    }
    
    public static Weapon CreateKnifeWeapon(Vector2 position)
    {
        return new Weapon(position, _texture, CreateKnifeAnimations(), new Rectangle(13 * 16, 5 * 16, 16, 16));
    }
    
    public static Weapon CreateCrowbarWeapon(Vector2 position)
    {
        return new Weapon(position, _texture, CreateCrowbarAnimations(), new Rectangle(16 * 16, 6 * 16, 16, 16));
    }
    
    public static Weapon CreateStickWeapon(Vector2 position)
    {
        return new Weapon(position, _texture, CreateStickAnimations(), new Rectangle(19 * 16, 6 * 16, 16, 16));
    }
    public static Consumable CreateHealthPotion(Vector2 position)
    {
        return new Consumable(position, _texture, new Rectangle(19*16, 4*16, 16,16));
    }
    
    public static Consumable CreateArmorPotion(Vector2 position)
    {
        return new Consumable(position, _texture, new Rectangle(20*16, 4*16, 16,16));
    }
    
    public static Currency CreateCoin(Vector2 position)
    {
        return new Currency(position, _texture, new Rectangle(17*16, 4*16, 16,16));
    }
    
    public static Currency CreateKey(Vector2 position)
    {
        return new Currency(position, _texture, new Rectangle(18*16, 4*16, 16,16));
    }
}