using System;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace ECS2022_23.Core.Entities.Characters.Enemy;

public class EnemyFactory
{
    
    
    public Enemy CreateRandomEnemy(Stage stage, Character target)
    {
        Random rand = new Random();
        // rand.Next(0,5)
        switch (rand.Next(0,2))
        {
            case 0: return CreateBlob(stage);
            case 1: return CreateChaser(stage, target);
        }
        return CreateBlob(stage);
    }
    
    public Enemy CreateBlob(Stage stage)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateBlobEnemyAnimations(),
            new RandomBehavior(), stage);
        
        en.Velocity = 1f;
        en.HP = 10;
        en.Strength = 1;
        en.EpReward = 1;

        en.Color = Color.Cyan;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 10f;

        return en;
    }

    public Enemy CreateChaser(Stage stage, Character target)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateZombieEnemyAnimations(),
            new Chase(target), stage);

        en.Velocity = 1.5f;
        en.HP = 10;
        en.Strength = 1;
        en.EpReward = 2;
        en.SpriteHeight = 14;
        en.SpriteWidth = 14;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 40f;
        
        return en;
    }

    public Enemy CreateBouncer(Stage stage)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new BounceBehavior(), stage);
        
        en.Velocity = 1.55f;
        en.HP = 8;
        en.Strength = 1;
        en.EpReward = 1;
        en.SpriteHeight = 16;
        en.SpriteWidth = 16;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.Color = Color.Green;
        en.Behavior.Owner = en;
        return en;
    }

    public Enemy CreateExploder(Stage stage)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet,
            AnimationLoader.CreateBlobEnemyAnimations(), new RandomBehavior(), stage);

        return en;
    }




}