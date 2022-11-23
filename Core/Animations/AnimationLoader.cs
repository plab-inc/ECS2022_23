using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.animations;

public static class AnimationLoader
{
    public static Dictionary<string, Animation> LoadPlayerAnimations(ContentManager content)
    {
        return new Dictionary<string, Animation>()
        {
            {
                "WalkUp",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1, 5), true)
            },
            {
                "WalkRight",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1, 4), true)
            },
            {
                "WalkLeft",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1, 4), true, true,
                    false)
            },
            {
                "WalkDown",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 6, new Vector2(1, 3), true)
            },
            {
                "AttackRight",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(6, 6), false)
            },
            {
                "AttackLeft",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(6, 6), false, true, false)
            },
            {
                "Default",
                new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 7, new Vector2(1, 2), true)
            }
        };
    }

    public static Animation LoadBasicWeaponAnimation(ContentManager content)
    {
        return new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(13, 6), false);
    }
    public static Animation LoadCrowbarAnimation(ContentManager content)
    {
        return new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(16, 6), false);
    }
    public static Animation LoadKnifeAnimation(ContentManager content)
    {
        return new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(13, 5), false);
    }
    public static Animation LoadPhaserAnimation(ContentManager content)
    {
        return new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(16, 5), false);
    }
    public static Animation LoadStickAnimation(ContentManager content)
    {
        return new Animation(content.Load<Texture2D>("sprites/spritesheet"), 16, 16, 3, new Vector2(19, 6), false);
    }
}