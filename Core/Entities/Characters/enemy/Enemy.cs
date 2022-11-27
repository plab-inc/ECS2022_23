using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    private Motor _motor;
    public Rectangle ActivationRectangle;
    private bool _isActive=false;

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

    // Updates Enemy when it is active. Checks for Activation if it isn't active.
    public override void Update(GameTime gameTime)
    {
        if (_isActive)
        {
           Act();
        }
        else
        {
            _isActive = Activate();
        }
    }

    // Resolves Enemy Behavior like Movement and Attack.
    private void Act()
    {
        // Movement
        Position = _motor.Move(Position, (int) Velocity);
        
        // Check for Attack
    }


    public bool Activate()
    {
        // When the Player enters the Activation Radius the Enemy becomes active
        return false;
    }

    public bool CollidesWithWall()
    {
        // Checks if the Enemy would collide with a Wall
        return false;
    }

    public bool CollideWithPlayer()
    {
        // Checks if the Enemy collides with the player.
        return false;
    }
}