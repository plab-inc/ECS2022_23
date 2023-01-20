using System.Collections.Generic;
using System.IO;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Loader;

public static class AnimationLoader
{
    private static ContentManager _content;
    private static Texture2D _spritesheetPink;
    private static Texture2D _spritesheetRed;

    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        _content = content;
        _spritesheetPink = _content.Load<Texture2D>("Sprites/spritesheet");
        _spritesheetRed = _content.Load<Texture2D>("Sprites/spritesheet_red");
    }

    public static Dictionary<AnimationType, Animation.Animation> CreatePlayerAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.WalkUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 6, new Point(1, 5), true)
            },
            {
                AnimationType.WalkRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 6, new Point(1, 4), true)
            },
            {
                AnimationType.WalkLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 6, new Point(1, 4), true, true,
                    false)
            },
            {
                AnimationType.WalkDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 6, new Point(1, 3), true)
            },
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(6, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(6, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(9, 6), false)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(11, 6), false)
            },
            {
                AnimationType.Hurt,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(2, 6), false)
            },
            {
                AnimationType.Death,
                new Animation.Animation(_spritesheetPink, 16, 16, 1, new Point(4, 6), true)
            },
            {
                AnimationType.Drowning,
                new Animation.Animation(_spritesheetPink, 16, 16, 5, new Point(8, 5), false)
            },
            {
                AnimationType.Default,
                new Animation.Animation(_spritesheetPink, 16, 16, 7, new Point(1, 2), true)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateBlobEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.Walk,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(10, 1), true)
            },
            {
                AnimationType.WalkRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(10, 1), true, true, false)
            },
            {
                AnimationType.WalkLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(10, 1), true)
            },
            {
                AnimationType.WalkUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(10, 1), true, true, false)
            },
            {
                AnimationType.WalkDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(10, 1), true, true, false)
            },
            {
                AnimationType.Attack,
                new Animation.Animation(_spritesheetPink, 16, 16, 5, new Point(10, 1), false)
            },
            {
                AnimationType.Hurt,
                new Animation.Animation(_spritesheetPink, 16, 16, 1, new Point(15, 1), false)
            },
            {
                AnimationType.Death,
                new Animation.Animation(_spritesheetPink, 16, 16, 1, new Point(14, 1), true)
            },
            {
                AnimationType.Default,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(10, 1), true)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateEyeEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.Walk,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true)
            },
            {
                AnimationType.WalkRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true, true, false)
            },
            {
                AnimationType.WalkLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true)
            },
            {
                AnimationType.WalkUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true, true, false)
            },
            {
                AnimationType.WalkDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true, true, false)
            },
            {
                AnimationType.Attack,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(12, 2), false)
            },
            {
                AnimationType.Hurt,
                new Animation.Animation(_spritesheetPink, 16, 16, 1, new Point(14, 2), false)
            },
            {
                AnimationType.Death,
                new Animation.Animation(_spritesheetPink, 16, 16, 1, new Point(15, 2), true)
            },
            {
                AnimationType.Default,
                new Animation.Animation(_spritesheetPink, 16, 16, 4, new Point(10, 2), true)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateZombieEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.WalkRight,
                new Animation.Animation(_spritesheetRed, 16, 16, 2, new Point(4, 2), true)
            },
            {
                AnimationType.WalkLeft,
                new Animation.Animation(_spritesheetRed, 16, 16, 2, new Point(4, 2), true, true, false)
            },
            {
                AnimationType.WalkUp,
                new Animation.Animation(_spritesheetRed, 16, 16, 2, new Point(2, 2), true)
            },
            {
                AnimationType.WalkDown,
                new Animation.Animation(_spritesheetRed, 16, 16, 2, new Point(0, 2), true)
            },
            {
                AnimationType.Hurt,
                new Animation.Animation(_spritesheetRed, 16, 16, 1, new Point(6, 2), false)
            },
            {
                AnimationType.Default,
                new Animation.Animation(_spritesheetRed, 16, 16, 2, new Point(2, 2), true)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateSwordAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(13, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(13, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(16, 2), false, false, true)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(16, 2), false, true, true)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreatePhaserAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(16, 5), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(16, 5), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(16, 1), false, false, true)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(16, 1), false, true, false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateKnifeAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(13, 5), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(13, 5), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(18, 2), false, false, true)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(18, 2), false, true, false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateStickAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(19, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(19, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(20, 2), false, false, true)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(20, 2), false, true, false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation.Animation> CreateCrowbarAnimations()
    {
        return new Dictionary<AnimationType, Animation.Animation>
        {
            {
                AnimationType.AttackRight,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(16, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation.Animation(_spritesheetPink, 16, 16, 3, new Point(16, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(18, 1), false, false, true)
            },
            {
                AnimationType.AttackDown,
                new Animation.Animation(_spritesheetPink, 16, 16, 2, new Point(18, 1), false, true, false)
            }
        };
    }
}