using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public abstract class Character : Entity
{
    public float MaxHP = 10;
    public float HP;
    public float Strength;
   
    protected float Velocity;
    public Direction AimDirection;
    public SoundEffect DamageSound;
    public SoundEffect DeathSound;
    public bool IsAttacking { get; set; }

    protected Character(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations) : base(spawn, texture, animations)
    {
        
    }
    public virtual void Attack(){}
    public virtual bool Collides(Vector2 velocity)
    {
        var newPosition = (Position + velocity).ToPoint();
        var body = new Rectangle(newPosition, new Point(Rectangle.Width, Rectangle.Height));
        var feet = new Point(body.Center.X, body.Bottom);

        if (velocity == Vector2.Zero)
        {
            return true;
        }
        
        foreach (var rectangle in Stage.GroundLayer)
        {
            if (rectangle.Contains(feet) && !IsInWater(body))
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsInWater(Rectangle movedBody)
    {
        foreach (var rectangle in Stage.WaterLayer)
        {
            if (rectangle.Intersects(movedBody))
            {
                return true;
            }
        }
        return false;
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
    public bool IsAlive()
    {
        return HP>0;
    }

    protected void Kill()
    {
        if (Animations != null)
        {
            AnimationManager.StopColorChange();
            AnimationManager.Stop();
            SetAnimation(AnimationType.Death);
        }
        HP = 0;
    }

}