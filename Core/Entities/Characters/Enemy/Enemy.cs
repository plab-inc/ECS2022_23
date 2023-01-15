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
    public Vector2 OriginalSpawn;
    public Room OriginalRoom;
    public float ItemSpawnRate;

    protected Color Color = Color.White;
    protected Behavior Behavior;
    protected bool IsActive;
    

    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Behavior behavior, Stage stage) : base(spawn, texture, animations)
    {
        Behavior = behavior;
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
        if (!OriginalRoom.Rectangle.Intersects(EnemyManager.Player.Rectangle))
        {
            Position += ReturnToSpawn();
        }
        else
        {
            Position += Behavior.Move(Position, Velocity);
        }
        Attack();
    }

   private bool Activate()
   {
       if (HP < MaxHP)
           return true;
       
       Vector3 vec = new Vector3(EnemyManager.Player.Position.X, EnemyManager.Player.Position.Y, 0);
       return EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Contains ||
              EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Intersects;
   }

   private Vector2 ReturnToSpawn()
   {
       if (Position == OriginalSpawn)
       {
           IsActive = false;
           return Vector2.Zero;
       }

       Vector2 direction = Vector2.Normalize(OriginalSpawn - Position) * Velocity;
        direction.Floor();
        if (Collides(direction))
            return direction;
        return Vector2.Zero;
   }

   public override void Draw(SpriteBatch spriteBatch)
    {
        if(IsBoss)
            AnimationManager.Draw(spriteBatch, Position, new Vector2(1.5f,1.5f));
        else    
            AnimationManager.Draw(spriteBatch, Position, Color);
    }
}