using System.Collections.Generic;
using ECS2022_23.Core.animations;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core.entities.characters;

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