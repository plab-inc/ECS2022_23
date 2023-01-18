using System;
using ECS2022_23.Core.Behaviors;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy;

public class EnemyFactory
{
    
    
    public Enemy CreateRandomEnemy(Stage stage, Character target)
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        switch (rand.Next(0,6))
        {
            case 0: return CreateBlob(stage);
            case 1: return CreateChaser(stage, target);
            case 2: return CreateBouncer(stage);
            case 3: return CreateExploder(stage);
            case 4: return CreateGunner(stage, target);
            case 5: return CreateTurret(stage, target);
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
        en.DeathCause = (int)DeathCause.Blob;
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
        en.DeathCause = (int)DeathCause.Chaser;
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
        en.DeathCause = (int)DeathCause.Bouncer;
        return en;
    }

    public Enemy CreateExploder(Stage stage)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet,
            AnimationLoader.CreateBlobEnemyAnimations(), new ExploderBehavior(), stage);
        
        en.Velocity = 0.75f;
        en.HP = 15;
        en.Strength = 1;
        en.EpReward = 2;

        en.Color = Color.GreenYellow;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 20f;
        en.DeathCause = (int)DeathCause.Exploder;
        
        return en;
    }

    public Enemy CreateGiantBlob(Stage stage, Character target)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet,
            AnimationLoader.CreateBlobEnemyAnimations(), new GiantBlobBehavior(target), stage);
        
        en.IsBoss = true;
        
        en.Velocity = 0.5f;
        en.HP = 150;
        en.Strength = 2;
        en.EpReward = 10;
        
        en.SpriteHeight = 32;
        en.SpriteWidth = 32;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 100f;
        en.DeathCause = (int)DeathCause.GiantBlob;
        return en;
    }

    public Enemy CreateGunner(Stage stage, Character target)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new Dodger(target), stage);

        en.Velocity = 1.5f;
        en.HP = 15;
        en.Strength = 1;
        en.EpReward = 2;
        en.Color = Color.Cyan;
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 60f;
        en.DeathCause = (int)DeathCause.Gunner;
        return en;
    }

    public Enemy CreateTurret(Stage stage, Character target)
    {
        Enemy en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new StationaryShooter(target), stage);
        
        en.Velocity = 0f;
        en.HP = 20;
        en.Strength = 1;
        en.EpReward = 1;
        
        en.DeathSound = SoundLoader.BlobDeathSound;
        en.ItemSpawnRate = 30f;
        en.DeathCause = (int)DeathCause.Turret;
        
        return en;
    }
}