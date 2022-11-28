using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public abstract class Character : Entity
{
    public float HP;
    public float Strength;
    public float Velocity;
    protected int SpriteWidth;
    public int AimDirection;
    public bool IsAlive = true;
    public bool IsAttacking { get; set; }
    
    protected Character(Vector2 spawn, Texture2D texture) : base(spawn, texture)
    {
    }
    
    protected Character(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations) : base(spawn, texture, animations)
    {
        
    }
    
    public virtual void Attack()
    {
        
    }

    public virtual void Move()
    {
        
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Animations == null)
        {
            base.Draw(spriteBatch);
        }
        else
        {
            AnimationManager.Draw(spriteBatch, Position);
        }
    }
}