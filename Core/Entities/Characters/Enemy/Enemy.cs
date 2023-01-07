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
    protected Behavior Behavior;
    private bool _isActive;
    private BoundingSphere _activationSphere;
    protected float ActivationRadius;
    protected Color Color = Color.White;
    public Vector2 AimVector;
    protected bool IsBoss;
    
    public Vector2 OriginalSpawn;
    public Room OriginalRoom;
    
    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations, Behavior behavior, Stage stage) : base(spawn, texture, animations)
    {
        Behavior = behavior;
        ActivationRadius = 125;
        Stage = stage;
    }
    
    public override void Update(GameTime gameTime)
    {
        SetAnimation(AnimationType.WalkDown);
        SetActivationRadius();
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
       return _activationSphere.Contains(vec) == ContainmentType.Contains|| _activationSphere.Contains(vec) == ContainmentType.Intersects;
   }

   public Vector2 ReturnToSpawn()
   {
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

    private void SetActivationRadius()
    {
        Vector3 vec = new Vector3(Position.X, Position.Y, 0);
        _activationSphere = new BoundingSphere(vec, ActivationRadius);
    }

    
    
}