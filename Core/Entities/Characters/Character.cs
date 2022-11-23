using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public abstract class Character : Entity
{
    public float HP;
    public float Strength;
    public float Velocity;
    protected int SpriteWidth;

    public virtual void Attack()
    {
        
    }

    public virtual void Move()
    {
        
    }
    protected Character(Texture2D texture) : base(texture)
    {
    }
    
    protected Character(Texture2D texture, Dictionary<string, Animation> animations) : base(texture, animations)
    {
    }
}