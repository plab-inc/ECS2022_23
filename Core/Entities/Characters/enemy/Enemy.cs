using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    
    protected Motor Motor;
    protected bool _isActive=true;
    
    protected Rectangle ActivationRectangle;
    protected Color Color = Color.White;
    
    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations, Motor motor, Level level) : base(spawn, texture, animations)
    {
        Motor = motor;
        Level = level;
        ActivationRectangle = Rectangle;
    }
    
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
            //SetAnimation("Default");
        }
        AnimationManager.Update(gameTime);
    }

   private void Act()
    {
        Position += Motor.Move(Position, Velocity);
        SetAnimation("WalkDown");
        // Check for Attack
    }

   private bool Activate()
    {
        return true;
    }
 
    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position, Color);
    }
}