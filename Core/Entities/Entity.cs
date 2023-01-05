using System;
using System.Collections.Generic;
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
    
    protected Dictionary<AnimationType, Animation> Animations;
    protected readonly AnimationManager AnimationManager = new();
    protected int SpriteWidth = 16;
    protected int SpriteHeight = 16;

    public Color[] EntityTextureData { get; }
    public Rectangle Rectangle => new((int) Position.X,(int) Position.Y, SpriteWidth, SpriteHeight);

    protected Entity(Vector2 spawn, Texture2D texture)
    {
        Texture = texture;
        Position = spawn;
        
        EntityTextureData = new Color[texture.Width * texture.Height];
        texture.GetData(EntityTextureData);
    }
    
    protected Entity(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations) : this(spawn, texture)
    {
        Animations = animations;
    }
    
    public void AddAnimation(AnimationType name, Animation animation)
    {
        if (Animations == null)
        {
            Animations = new Dictionary<AnimationType, Animation>();
        }
        Animations.Add(name, animation);
    }
    
    public void SetAnimation(AnimationType name)
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
    
    public bool IntersectPixels(Rectangle rectangleB, Color[] dataB)
    {
        // Find the bounds of the rectangle intersection
        int top = Math.Max(Rectangle.Top, rectangleB.Top);
        int bottom = Math.Min(Rectangle.Bottom, rectangleB.Bottom);
        int left = Math.Max(Rectangle.Left, rectangleB.Left);
        int right = Math.Min(Rectangle.Right, rectangleB.Right);

        // Check every point within the intersection bounds
        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                // Get the color of both pixels at this point
                Color colorA = EntityTextureData[(x - Rectangle.Left) +
                                                 (y - Rectangle.Top) * Rectangle.Width];
                Color colorB = dataB[(x - rectangleB.Left) +
                                     (y - rectangleB.Top) * rectangleB.Width];

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