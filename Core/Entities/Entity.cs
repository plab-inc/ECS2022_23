using System;
using System.Collections.Generic;
using ECS2022_23.Core.animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLevelGenerator.Core;

public abstract class Entity
{
    protected Texture2D _texture; 
    public Vector2 Position { get; set; }
    protected Dictionary<string, Animation> Animations;
    protected readonly AnimationManager AnimationManager;
    
    public Rectangle Rectangle
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
    }

    protected Entity(Texture2D texture)
    {
        _texture = texture;
        Position = new Vector2(60, 60);
        AnimationManager = new AnimationManager();
    }
    
    protected Entity(Texture2D texture, Dictionary<string, Animation> animations) : this(texture)
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
        spriteBatch.Draw(_texture,Position,Color.White);
    }
}