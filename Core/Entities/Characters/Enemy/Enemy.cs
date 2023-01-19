using System.Collections.Generic;
using ECS2022_23.Core.Behaviors;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.Enemy;

public class Enemy : Character
{
    public Vector2 AimVector;
    public Behavior Behavior;

    public Color Color = Color.White;
    public float EpReward;
    public bool IsActive;
    public bool IsBoss;
    public float ItemSpawnRate;


    public Enemy(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation.Animation> animations, Behavior behavior,
        Stage stage) : base(spawn, texture, animations)
    {
        Behavior = behavior;
        Behavior.Owner = this;
        Stage = stage;
    }

    public void Update(GameTime gameTime)
    {
        SetAnimation(AnimationType.WalkDown);
        if (IsActive)
            Act();
        else
            IsActive = Activate();

        if (!IsAlive()) SetAnimation(AnimationType.Death);
        AnimationManager.Update(gameTime);
    }

    private void Act()
    {
        Position += Behavior.Move(Position, Velocity);
        Behavior.Attack();
    }

    private bool Activate()
    {
        if (HP < MaxHP)
            return true;

        var vec = new Vector3(Position.X, Position.Y, 0);
        return EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Contains ||
               EnemyManager.Player.ActivationSphere.Contains(vec) == ContainmentType.Intersects;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsBoss)
            AnimationManager.Draw(spriteBatch, Position, new Vector2(1.5f, 1.5f));
        else
            AnimationManager.Draw(spriteBatch, Position, Color);
    }
}