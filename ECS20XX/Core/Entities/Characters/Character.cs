﻿using System.Collections.Generic;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public abstract class Character : Entity
{
    public Direction AimDirection;
    public SoundEffect DamageSound;
    public SoundEffect DeathSound;
    public float HP;
    public float MaxHP = 10;
    public float Strength;

    public float Velocity;

    protected Character(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation.Animation> animations) :
        base(spawn,
            texture, animations)
    {
    }

    public bool IsAttacking { get; set; }

    public virtual bool Collides(Vector2 velocity)
    {
        var newPosition = (Position + velocity).ToPoint();
        var body = new Rectangle(newPosition, new Point(Rectangle.Width, Rectangle.Height));
        var feet = new Point(body.Center.X, body.Bottom);

        if (velocity == Vector2.Zero) return true;

        foreach (var rectangle in Stage.GroundLayer)
            if (rectangle.Contains(feet) && !IsInWater(body))
                return true;
        return false;
    }

    public virtual bool IsInWater(Rectangle movedBody)
    {
        foreach (var rectangle in Stage.WaterLayer)
            if (rectangle.Intersects(movedBody))
                return true;
        return false;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Animations == null)
            base.Draw(spriteBatch);
        else
            AnimationManager.Draw(spriteBatch, Position);
    }

    public bool IsAlive()
    {
        return HP > 0;
    }

    protected void Kill(DeathCause deathCause)
    {
        if (Animations != null)
        {
            AnimationManager.StopColorChange();
            AnimationManager.Stop();
            if (deathCause == DeathCause.Water)
                SetAnimation(AnimationType.Drowning);
            else
                SetAnimation(AnimationType.Death);
        }

        HP = 0;
    }
}