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
        Behavior.Owner = this;
        
        Velocity = 1.55f;
        HP = 8;
        Strength = 1;
        EpReward = 1;
        SpriteHeight = 16;
        SpriteWidth = 16;
        DeathSound = SoundLoader.BlobDeathSound;
        Color = Color.Green;
    }

    public override void Attack()
    {
        
    }
}