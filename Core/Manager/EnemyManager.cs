using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.Entities.Characters.enemy.EnemyTypes;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public static class EnemyManager
{
    private static List<Enemy> Enemies = new();
    private static List<Enemy> _enemyTypes = new();
    public static Player Player { set; get;}
    public static Level Level { set; get; }
    

    static EnemyManager()
    {
        _enemyTypes.Add(new Walker(null));
        _enemyTypes.Add(new Chaser(null,(Player)null));
    }

    private static void AddEnemy(Enemy e)
    {
        Enemies.Add(e);
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.RemoveAt(Enemies.IndexOf(enemy));
        }
    }

    public static void SpawnEnemies()
    {
        Random rand = new Random();
        foreach (var room in Level.Rooms)
        {
            if (room.Spawns != null && room.Spawns.Count > 0)
            {
                Enemy en = GetRandomEnemy();
                en.Position = room.GetRandomSpawnPos(en);
                AddEnemy(en);
                CombatManager.AddEnemy(en);
            }
        }
    }

    private static Enemy GetRandomEnemy()
    {
        Random rand = new Random();
        // rand.Next(0, EnemyTypes.Count)
        switch (0)
        {
            case 0: return new Walker(Level, AnimationLoader.CreateZombieEnemyAnimations());
            case 1: return new Chaser(Level, Player);
        }
        return new Walker(Level);
    }

    public static void CheckEnemyStatus()
    {
        foreach (var enemy in Enemies)
        {
            if (!enemy.IsAlive())
            {
                RemoveEnemy(enemy);
            }
        }
    }

    public static void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies)
        {
            enemy.Update(gameTime);
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var en in Enemies)
        {
            en.Draw(spriteBatch);
        }
    }

}