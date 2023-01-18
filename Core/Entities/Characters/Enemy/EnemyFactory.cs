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
        var rand = new Random((int) DateTime.Now.Ticks);
        switch (rand.Next(0, 6))
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
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateBlobEnemyAnimations(),
            new Randomer(), stage)
        {
            Velocity = 1f,
            HP = 10,
            Strength = 1,
            EpReward = 1,
            Color = Color.Cyan,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 10f,
            DeathCause = DeathCause.Blob
        };

        return en;
    }

    public Enemy CreateChaser(Stage stage, Character target)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateZombieEnemyAnimations(),
            new Chaser(target), stage)
        {
            Velocity = 1.5f,
            HP = 10,
            Strength = 1,
            EpReward = 2,
            SpriteHeight = 14,
            SpriteWidth = 14,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 40f,
            DeathCause = DeathCause.Chaser
        };

        return en;
    }

    public Enemy CreateBouncer(Stage stage)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new Bouncer(), stage)
        {
            Velocity = 1.55f,
            HP = 8,
            Strength = 1,
            EpReward = 1,
            SpriteHeight = 16,
            SpriteWidth = 16,
            DeathSound = SoundLoader.BlobDeathSound,
            Color = Color.Green
        };

        en.Behavior.Owner = en;
        en.DeathCause = DeathCause.Bouncer;
        return en;
    }

    public Enemy CreateExploder(Stage stage)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet,
            AnimationLoader.CreateBlobEnemyAnimations(), new Exploder(), stage)
        {
            Velocity = 0.75f,
            HP = 15,
            Strength = 1,
            EpReward = 2,
            Color = Color.GreenYellow,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 20f,
            DeathCause = DeathCause.Exploder
        };

        return en;
    }

    public Enemy CreateGiantBlob(Stage stage, Character target)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet,
            AnimationLoader.CreateBlobEnemyAnimations(), new Blobber(target), stage)
        {
            IsBoss = true,
            Velocity = 0.5f,
            HP = 150,
            Strength = 2,
            EpReward = 10,
            SpriteHeight = 32,
            SpriteWidth = 32,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 100f,
            DeathCause = DeathCause.GiantBlob
        };

        return en;
    }

    public Enemy CreateGunner(Stage stage, Character target)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new Dodger(target), stage)
        {
            Velocity = 1.5f,
            HP = 15,
            Strength = 1,
            EpReward = 2,
            Color = Color.Cyan,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 60f,
            DeathCause = DeathCause.Gunner
        };

        return en;
    }

    public Enemy CreateTurret(Stage stage, Character target)
    {
        var en = new Enemy(Vector2.Zero, UiLoader.SpriteSheet, AnimationLoader.CreateEyeEnemyAnimations(),
            new Shooter(target), stage)
        {
            Velocity = 0f,
            HP = 20,
            Strength = 1,
            EpReward = 1,
            DeathSound = SoundLoader.BlobDeathSound,
            ItemSpawnRate = 30f,
            DeathCause = DeathCause.Turret
        };

        return en;
    }
}