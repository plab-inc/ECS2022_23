using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.Enemy;

public abstract class Enemy : Character
{
    public float EpReward;
    public Vector2 AimVector;
    public bool IsBoss;
    public float ItemSpawnRate;

    protected Color Color = Color.White;
    protected Behavior Behavior;
    protected bool IsActive;
    

    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Behavior behavior, Stage stage) : base(spawn, texture, animations)
    {
        Behavior = behavior;
        Behavior.Owner = this;
        Stage = stage;
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
        AnimationManager.Update(gameTime);
    }

   private void Act()
    {
        Position += Behavior.Move(Position, Velocity);
        Attack();
    }

   public virtual void OnDeath()
   {
       
   }

   private bool Activate()
   {
       if (HP < MaxHP)
           return true;
       
       Vector3 vec = new Vector3(Position.X, Position.Y, 0);
       return EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Contains ||
              EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Intersects;
   }
   
   public override void Draw(SpriteBatch spriteBatch)
    {
        if(IsBoss)
            AnimationManager.Draw(spriteBatch, Position, new Vector2(1.5f,1.5f));
        else    
            AnimationManager.Draw(spriteBatch, Position, Color);
    }
}