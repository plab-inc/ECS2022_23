using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    private Motor _motor;
    public Rectangle ActivationRectangle;
    private bool _isActive=true;
    public Level Level;

    public Enemy(Vector2 spawn, Texture2D texture, Motor motor, Level level) : base(spawn, texture)
    {
        Velocity = 1f;
        HP = 10;
        SpriteWidth = 16;
        Level = level;
        _motor = motor;
        _motor.SetEnemy(this);
        ActivationRectangle = Rectangle;
        ActivationRectangle.Inflate(25, 25);
    }

    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Motor motor, Level level) : base(spawn, texture, animations)
    {
        Velocity = 1f;
        HP = 10;
        SpriteWidth = 16;
        Level = level;
        _motor = motor;
        _motor.SetEnemy(this);
        ActivationRectangle = Rectangle;
        ActivationRectangle.Inflate(25, 25);
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

    // Resolves Enemy Behavior like Movement and Attack.
    private void Act()
    {
        // Movement
        Position += _motor.Move(Position, (int) Velocity);
        
        // Check for Attack
    }


    public bool Activate()
    {
        // When the Player enters the Activation Radius the Enemy becomes active
        return false;
    }

    public bool CollidesWithWall(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();

        foreach (var rec in Level.GroundLayer)
        {
            if (Rectangle.Intersects(rec))
            {
                return true;
            }
        }

        return false;

    }
    
    public bool CollidesWithWall2(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();
        var rect = new Rectangle(newPoint, new Point(Texture.Width, Texture.Height));

        //TODO clean this mess up
        
        var armHitBoxLeft =
            new Rectangle(newPoint.X + 4, newPoint.Y + Texture.Height / 2 + 2, 1, Texture.Height / 2 - 2);
        var armHitBoxRight = new Rectangle(newPoint.X + Texture.Width - 5, newPoint.Y + Texture.Height / 2 + 2, 1,
            Texture.Height / 2 - 2);

        if (velocity == Vector2.Zero)
        {
            return true;
        }
        
        foreach (var rectangle in Level.GroundLayer)
        {
            if (velocity.Y == 0 && velocity.X > 0)
            {
                if (rectangle.Intersects(armHitBoxRight))
                {
                    return true;
                }
            }

            if (velocity.Y == 0 && velocity.X < 0)
            {
                if (rectangle.Intersects(armHitBoxLeft))
                {
                    return true;
                }
            }

            if ((velocity.X != 0 || !(velocity.Y > 0)) && (velocity.X != 0 || !(velocity.Y < 0))) continue;
            
            if (rectangle.Intersects(armHitBoxLeft) && rectangle.Intersects(armHitBoxRight))
            {
                return true;
            }
            
           //Position = _motor.Move(Position, (int) Velocity);
        }

       
        
        return false;
    }

    public bool CollideWithPlayer()
    {
        // Checks if the Enemy collides with the player.
        return false;
    }
}