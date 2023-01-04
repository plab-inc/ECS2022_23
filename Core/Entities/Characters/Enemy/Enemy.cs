using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.Enemy;

public abstract class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    
    protected Behavior.Behavior Behavior;
    protected bool IsActive;
    
    public Rectangle ActivationRectangle;
    protected Color Color = Color.White;

    public Vector2 AimVector;
    
    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Behavior.Behavior behavior, Level level) : base(spawn, texture, animations)
    {
        Behavior = behavior;
        Level = level;
        ActivationRectangle = Rectangle;
        ActivationRectangle.Location = Rectangle.Location;
    }
    
    public override void Update(GameTime gameTime)
    {
        SetAnimation(AnimationType.WalkDown);
        if (IsActive)
        {
           Act();
        }
        else
        {
            IsActive = Activate();
        }
        
        if(!IsAlive())
        {
            SetAnimation(AnimationType.Death);
        }
        else
        {
            //SetAnimation("Default");
        }
        AnimationManager.Update(gameTime);
    }

   private void Act()
    {
        Position += Behavior.Move(Position, Velocity);
        Attack();
    }

   private bool Activate()
    {
        if(ActivationRectangle.Contains(EnemyManager.Player.Position))
            return true;

        return false;
    }
 
    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position, Color);
    }

    public void SetActivationRectangle()
    {
        //ActivationRectangle.Inflate(50,50);
        //ActivationRectangle.Offset(-ActivationRectangle.Width/2,-ActivationRectangle.Height/2);
        //ActivationRectangle.Width = 50;
        //ActivationRectangle.Height = 50;
        //ActivationRectangle.Location -= new Point(ActivationRectangle.Width, ActivationRectangle.Height);
    }
}