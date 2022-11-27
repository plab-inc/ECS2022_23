using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities;

public abstract class Entity
{
    protected Texture2D Texture; 
    public Vector2 Position { get; set; }
    
    protected Dictionary<string, Animation> Animations;
    protected readonly AnimationManager AnimationManager = new();
    
    public Rectangle Rectangle => new((int) Position.X,(int) Position.Y, Texture.Width, Texture.Height);

    protected Entity(Vector2 spawn, Texture2D texture)
    {
        Texture = texture;
        Position = spawn;
    }
    
    protected Entity(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations) : this(spawn, texture)
    {
        Animations = animations;
    }
    
    public void AddAnimation(string name, Animation animation)
    {
        if (Animations == null)
        {
            Animations = new Dictionary<string, Animation>();
        }
        Animations.Add(name, animation);
    }
    
    public void SetAnimation(string name)
    {
        try
        {
            AnimationManager.Play(Animations?[name]);
        }
        catch (KeyNotFoundException e)
        {
            return;
        }
    }
    
    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}