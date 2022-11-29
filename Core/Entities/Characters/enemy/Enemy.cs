using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public  class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    private Motor _motor;
    public bool IsActive = true;

    public Enemy(Vector2 spawn, Texture2D texture, Motor motor) : base(spawn, texture)
    {
        Velocity = 2f;
        HP = 10;
        SpriteWidth = 16;
        _motor = motor;
    }

    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Motor motor) : base(spawn, texture, animations)
    {
        Velocity = 3f;
        HP = 10;
        SpriteWidth = 16;
        _motor = motor;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
           Position = _motor.Move(Position, (int) Velocity);
        }

        if(!IsAlive)
        {
            SetAnimation("Death");
        }
        else
        {
            SetAnimation("Default");
        }
        AnimationManager.Update(gameTime);
    }
}