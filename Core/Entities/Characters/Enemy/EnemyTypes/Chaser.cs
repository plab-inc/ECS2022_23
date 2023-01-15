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
        Behavior.SetEnemy(this);
        
        Velocity = 1.5f;
        HP = 10;
        Strength = 1;
        EpReward = 2;

        SpriteHeight = 14;
        SpriteWidth = 14;
        DeathSound = SoundLoader.BlobDeathSound;
        ItemSpawnRate = 40f;
    }
    
}