using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Enemy : Character
{
    // Statistics
    public float XpReward;
    public float MoneyReward;
    
    // Behavior
    public Motor Motor;
    private bool _isActive=false;
    
    // Level
    protected Rectangle ActivationRectangle;

    public Enemy(Vector2 spawn, Texture2D texture, Motor motor, Level level) : base(spawn, texture)
    {
        Motor = motor;
        SpriteWidth = 16;
        Level = level;
        ActivationRectangle = Rectangle;
    }

    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Motor motor, Level level) : base(spawn, texture, animations)
    {
        Motor = motor;
        SpriteWidth = 16;
        Level = level;
        ActivationRectangle = Rectangle;
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
        
        if(!IsAlive())
        {
            SetAnimation("Death");
        }
        else
        {
            SetAnimation("Default");
        }
        AnimationManager.Update(gameTime);
        
    }

    // Resolves Enemy Behavior like Movement and Attack.
    private void Act()
    {
        // Movement
        Position += Motor.Move(Position, (int) Velocity);
        
        // Check for Attack
    }


    public bool Activate()
    {
        // When the Player enters the Activation Radius the Enemy becomes active
        CombatManager.AddEnemy(this);
        return true;
    }
    
    public bool CollideWithPlayer()
    {
        // Checks if the Enemy collides with the player.
        return false;
    }
}