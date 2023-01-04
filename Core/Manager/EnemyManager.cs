using System;
using System.Collections.Generic;
using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class EnemyManager
{
    private static List<Enemy> Enemies = new();
    private static List<Enemy> _enemyTypes = new();
    private static Enemy _keyEnemy;
    public static Player Player { set; get;}
    public static Level Level { set; get; }

    static EnemyManager()
    {

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

    public static bool EnemyDropsKey(Enemy enemy)
    { 
        if (_keyEnemy == null) return false;
        return enemy.Equals(_keyEnemy);
    }
    
    public static void KillEnemies()
    {
        Enemies.Clear();
    }

    public static void SpawnEnemies()
    {
        bool skipFirst = true;
        foreach (var room in Level.Rooms)
        {
            if (skipFirst)
            {
                skipFirst = false;
                continue;
            }

            if (room.Spawns != null && room.Spawns.Count > 0)
            {
                Enemy en = GetRandomEnemy();
                en.Position = room.GetRandomSpawnPos(en);
                AddEnemy(en);
                CombatManager.AddEnemy(en);
            }
        }
        ChooseEnemyForKey();
    }

    private static Enemy GetRandomEnemy()
    {
        Random rand = new Random();
        switch (rand.Next(0, 4))
        {
            case 0: return new Walker(Level);
            case 1: return new Chaser(Level, Player);
            case 2: return new Turret(Level, Player);
            case 3: return new Gunner(Level, Player);
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
        Debug.WriteLine("Enemy:" + Enemies[0].ActivationRectangle + "Player: " + Player.Position);
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

    private static void ChooseEnemyForKey()
    {
        var random = new Random();
        //Zum Testen dropped der erste Enemy den Schlüssel, damit man ihn nicht extra suchen muss, sonst Enemies.Count als Grenze setzen
        //var randomInt = random.Next(Enemies.Count);
        var randomInt = random.Next(1);

        if (randomInt >= 0 && randomInt < Enemies.Count)
        {
            _keyEnemy = Enemies[randomInt];
        }
    }

}