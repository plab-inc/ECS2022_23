using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Manager;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities;

public abstract class Entity
{
    public Texture2D Texture; 
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int) Position.X,(int) Position.Y, SpriteWidth, SpriteHeight);
    
    protected Dictionary<AnimationType, Animation> Animations;
    protected AnimationManager AnimationManager = new();

    private Color[] TextureData { get; }

    public int SpriteWidth = 16;
    public int SpriteHeight = 16;
    public int DeathCause {get; set;}

    protected Entity(Vector2 spawn, Texture2D texture)
    {
        Texture = texture;
        Position = spawn;
        
        TextureData = new Color[texture.Width * texture.Height];
        texture.GetData(TextureData);
    }
    
    protected Entity(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations) : this(spawn, texture)
    {
        Animations = animations;
    }

    public void SetAnimation(AnimationType type)
    {
        if (Animations.ContainsKey(type))
        {
            AnimationManager.Play(Animations?[type]);
        }
    }
    public virtual void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }
    
    public bool IntersectPixels(Entity entity)
    {
        // Find the bounds of the rectangle intersection
        int top = Math.Max(Rectangle.Top, entity.Rectangle.Top);
        int bottom = Math.Min(Rectangle.Bottom, entity.Rectangle.Bottom);
        int left = Math.Max(Rectangle.Left, entity.Rectangle.Left);
        int right = Math.Min(Rectangle.Right, entity.Rectangle.Right);

        // Check every point within the intersection bounds
        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                // Get the color of both pixels at this point
                Color colorA = TextureData[(x - Rectangle.Left) +
                                                 (y - Rectangle.Top) * Rectangle.Width];
                Color colorB = entity.TextureData[(x - entity.Rectangle.Left) +
                                     (y - entity.Rectangle.Top) * entity.Rectangle.Width];

                // If both pixels are not completely transparent,
                if (colorA.A != 0 && colorB.A != 0)
                {
                    // then an intersection has been found
                    return true;
                }
            }
        }
        
        

        // No intersection found
        return false;
    }
}