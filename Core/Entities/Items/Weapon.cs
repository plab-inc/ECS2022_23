﻿using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Weapon : Item
{
    public float DamagePoints;
    public float Range;
    private Animation Animation { get; }

    public Weapon(Vector2 spawn, Texture2D texture, Vector2 startPos, Animation animation) : base(spawn, texture)
    {
        Animation = animation;
        AddAnimation("Attack", animation);
    }
    public Weapon(Texture2D texture, Vector2 startPos, Animation animation) : base(Vector2.Zero, texture)
    {
        Animation = animation;
        AddAnimation("Attack", animation);
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