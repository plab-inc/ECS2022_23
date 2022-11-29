using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Weapon : Item
{
    public float DamagePoints;
    public float Range { get; }
    public WeaponType WeaponType;

    public Weapon(Vector2 spawn, Texture2D texture, Vector2 startPos, Dictionary<string, Animation> animations) : base(spawn, texture)
    {
        Animations = animations;
    }
    public Weapon(Texture2D texture, Vector2 startPos, Dictionary<string, Animation> animations, WeaponType type, float range) : base(Vector2.Zero, texture)
    {
        Animations = animations;
        Range = range;
        DamagePoints = 2;
        WeaponType = type;
    }
    
    public override void Update(GameTime gameTime)
    {
        AnimationManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position);
    }
    
}

public enum WeaponType
{
    CLOSE,
    RANGE
}