using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;

public class Blob : Enemy
{
   public Blob(Stage stage) : base(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateBlobEnemyAnimations(), new RandomBehavior(), stage)
   {
       Velocity = 1f;
        HP = 10;
        Strength = 1;
        EpReward = 1;

        Color = Color.Cyan;
        DeathSound = SoundLoader.BlobDeathSound;
        ItemSpawnRate = 10f;
    }
}