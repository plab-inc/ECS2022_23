using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities;

public abstract class Entity
{
    protected AnimationManager AnimationManager = new();

    protected Dictionary<AnimationType, Animation> Animations;
    public int SpriteHeight = 16;

    public int SpriteWidth = 16;
    public Texture2D Texture;

    protected Entity(Vector2 spawn, Texture2D texture)
    {
        Texture = texture;
        Position = spawn;

        TextureData = new Color[texture.Width * texture.Height];
        texture.GetData(TextureData);
    }

    protected Entity(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations) : this(spawn,
        texture)
    {
        Animations = animations;
    }

    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int) Position.X, (int) Position.Y, SpriteWidth, SpriteHeight);

    public Stage Stage { get; set; }

    private Color[] TextureData { get; }
    public DeathCause DeathCause { get; set; }

    public void SetAnimation(AnimationType type)
    {
        if (Animations.ContainsKey(type)) AnimationManager.Play(Animations?[type]);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public bool IntersectPixels(Entity entity)
    {
        // Find the bounds of the rectangle intersection
        var top = Math.Max(Rectangle.Top, entity.Rectangle.Top);
        var bottom = Math.Min(Rectangle.Bottom, entity.Rectangle.Bottom);
        var left = Math.Max(Rectangle.Left, entity.Rectangle.Left);
        var right = Math.Min(Rectangle.Right, entity.Rectangle.Right);

        // Check every point within the intersection bounds
        for (var y = top; y < bottom; y++)
        for (var x = left; x < right; x++)
        {
            // Get the color of both pixels at this point
            var colorA = TextureData[x - Rectangle.Left +
                                     (y - Rectangle.Top) * Rectangle.Width];
            var colorB = entity.TextureData[x - entity.Rectangle.Left +
                                            (y - entity.Rectangle.Top) * entity.Rectangle.Width];

            // If both pixels are not completely transparent,
            if (colorA.A != 0 && colorB.A != 0)
                // then an intersection has been found
                return true;
        }


        // No intersection found
        return false;
    }
}