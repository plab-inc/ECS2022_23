using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ECS2022_23.Core.Animations;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Loader;

public static class AnimationLoader
{
    private static ContentManager _content;
    private static Texture2D _texturePink;
    private static Texture2D _textureRed;
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
    public static Dictionary<AnimationType, Animation> CreatePlayerAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
               AnimationType.WalkUp,
                new Animation(_texturePink, 16, 16, 6, new Vector2(1, 5), true)
            },
            {
                AnimationType.WalkRight,
                new Animation(_texturePink, 16, 16, 6, new Vector2(1, 4), true)
            },
            {
                AnimationType.WalkLeft,
                new Animation(_texturePink, 16, 16, 6, new Vector2(1, 4), true, true,
                    false)
            },
            {
                AnimationType.WalkDown,
                new Animation(_texturePink, 16, 16, 6, new Vector2(1, 3), true)
            },
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(6, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(6, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 2, new Vector2(9, 6), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 2, new Vector2(11, 6), false)
            },
            {
                AnimationType.Hurt,
                new Animation(_texturePink, 16, 16, 2, new Vector2(2, 6), false)
            },
            {
                AnimationType.Death,
                new Animation(_texturePink, 16, 16, 1, new Vector2(4, 6), true)
            },
            {
                AnimationType.Default,
                new Animation(_texturePink, 16, 16, 7, new Vector2(1, 2), true)
            }
        };
    }
    
    public static Dictionary<AnimationType, Animation> CreateBlobEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.Walk,
                new Animation(_texturePink, 16, 16, 2, new Vector2(10, 1), true)
            },
            {
                AnimationType.WalkRight,
                new Animation(_texturePink, 16, 16, 2, new Vector2(10, 1), true, true, false)
            },
            {
                AnimationType.WalkLeft,
                new Animation(_texturePink, 16, 16, 2, new Vector2(10, 1), true)
            },
            {
                AnimationType.WalkUp,
                new Animation(_texturePink, 16, 16, 2, new Vector2(10, 1), true, true, false)
            },
            {
                AnimationType.WalkDown,
                new Animation(_texturePink, 16, 16, 2, new Vector2(10, 1), true, true, false)
            },
            {
                AnimationType.Attack,
                new Animation(_texturePink, 16, 16, 5, new Vector2(10, 1), false)
            },
            {
                AnimationType.Hurt,
                new Animation(_texturePink, 16, 16, 1, new Vector2(15, 1), false)
            },
            {
                AnimationType.Death,
                new Animation(_texturePink, 16, 16, 1, new Vector2(14, 1), true)
            },
            {
                AnimationType.Default,
                new Animation(_texturePink, 16, 16, 3, new Vector2(10, 1), true)
            }
        };
    }
    
    public static Dictionary<AnimationType, Animation> CreateEyeEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.Walk,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true)
            },
            {
                AnimationType.WalkRight,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true, true, false)
            },
            {
                AnimationType.WalkLeft,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true)
            },
            {
                AnimationType.WalkUp,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true, true, false)
            },
            {
                AnimationType.WalkDown,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true, true, false)
            },
            {
                AnimationType.Attack,
                new Animation(_texturePink, 16, 16, 2, new Vector2(12, 2), false)
            },
            {
                AnimationType.Hurt,
                new Animation(_texturePink, 16, 16, 1, new Vector2(14, 2), false)
            },
            {
                AnimationType.Death,
                new Animation(_texturePink, 16, 16, 1, new Vector2(15, 2), true)
            },
            {
                AnimationType.Default,
                new Animation(_texturePink, 16, 16, 4, new Vector2(10, 2), true)
            }
        };
    }
    public static Dictionary<AnimationType, Animation> CreateZombieEnemyAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.WalkRight,
                new Animation(_textureRed, 16, 16, 2, new Vector2(4, 2), true)
            },
            {
                AnimationType.WalkLeft,
                new Animation(_textureRed, 16, 16, 2, new Vector2(4, 2), true, true, false)
            },
            {
                AnimationType.WalkUp,
                new Animation(_textureRed, 16, 16, 2, new Vector2(2, 2), true)
            },
            {
                AnimationType.WalkDown,
                new Animation(_textureRed, 16, 16, 2, new Vector2(0, 2), true)
            },
            {
                AnimationType.Default,
                new Animation(_textureRed, 16, 16, 2, new Vector2(2, 2), true)
            },
        };
    }

    public static Dictionary<AnimationType, Animation> CreateSwordAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 6), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 6), false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation> CreatePhaserAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 5), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 5), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 5), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 5), false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation> CreateKnifeAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 5), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 5), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 5), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 3, new Vector2(13, 5), false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation> CreateStickAnimations()
    {
       return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(19, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(19, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 3, new Vector2(19, 6), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 3, new Vector2(19, 6), false)
            }
        };
    }

    public static Dictionary<AnimationType, Animation> CreateCrowbarAnimations()
    {
        return new Dictionary<AnimationType, Animation>()
        {
            {
                AnimationType.AttackRight,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 6), false)
            },
            {
                AnimationType.AttackLeft,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 6), false, true, false)
            },
            {
                AnimationType.AttackUp,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 6), false)
            },
            {
                AnimationType.AttackDown,
                new Animation(_texturePink, 16, 16, 3, new Vector2(16, 6), false)
            }
        };
    }
    
}