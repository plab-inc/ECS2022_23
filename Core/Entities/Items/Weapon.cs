using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Loader;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Weapon : Item
{
    public float DamagePoints = 5;
    public WeaponType WeaponType = WeaponType.CLOSE;
    public SoundEffect AttackSound;

    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
        Animations = animations;
    }
    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Rectangle sourceRect, WeaponType type) : base(spawn, texture, sourceRect)
    {
        Animations = animations;
        WeaponType = type;
        AttackSound = SoundLoader.LaserSound;
    }
    
    public override void Update(GameTime gameTime)
    {
        AnimationManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position);
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(Weapon))
        {
            return false;
        }

        var toCompare = (Weapon)obj;
        return toCompare.Texture == this.Texture && this.Position == toCompare.Position 
               && DamagePoints.Equals(toCompare.DamagePoints) 
               && WeaponType == toCompare.WeaponType && Equals(AttackSound, toCompare.AttackSound);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), DamagePoints, (int)WeaponType, AttackSound);
    }
}

public enum WeaponType
{
    CLOSE,
    RANGE
}