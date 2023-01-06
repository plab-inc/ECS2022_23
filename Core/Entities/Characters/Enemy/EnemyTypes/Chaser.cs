using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Chaser : Enemy
{
    
    public Chaser(Stage stage, Character target) : base(Vector2.Zero, UiLoader.SpriteSheet,
        AnimationLoader.CreateZombieEnemyAnimations(), new Chase(target), stage)
    {
        Velocity = 1.5f;
        HP = 10;
        XpReward = 2;
        MoneyReward = 1;
        SpriteHeight = 14;
        SpriteWidth = 14;
        Behavior.SetEnemy(this);
        DeathSound = SoundLoader.BlobDeathSound;
        Strength = 1;
    }

    public override void Attack()
    {
        
    }
}