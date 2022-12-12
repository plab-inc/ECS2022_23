using System.Collections.Generic;
using System.Collections.Specialized;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Weapon : Item
{
    public float DamagePoints = 5;
    public WeaponType WeaponType = WeaponType.CLOSE;

    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
        Animations = animations;
    }
    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Rectangle sourceRect, WeaponType type) : base(spawn, texture, sourceRect)
    {
        Animations = animations;
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