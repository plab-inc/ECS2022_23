using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;


namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Bouncer : Enemy
{
    public Bouncer(Stage stage) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(), new BounceBehavior(), stage)
    {
        Behavior.SetEnemy(this);
        
        Velocity = 1.5f;
        HP = 10;
        Strength = 1;
        EpReward = 2;
        SpriteHeight = 8;
        SpriteWidth = 8;
        DeathSound = SoundLoader.BlobDeathSound;
        IsActive = true;
        Color = Color.Green;
    }

    public override void Attack()
    {
        
    }
}