using System;
using System.Collections.Generic;
using System.Linq;
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
   private static Enemy _keyEnemy;
    public static Player Player { set; get;}
    public static Level Level { set; get; }
    
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
    
    public static void SpawnMultipleEnemies(int enemyLimit)
    {
        Random rand = new Random();
        List<Vector2> closedList = new List<Vector2>();
        
        foreach (var room in Level.Rooms.Skip(1))
        {
            if (room.Spawns.Count > 0)
            {
                int amount = rand.Next(1,Math.Min(room.Spawns.Count, enemyLimit));
                for (int a = 0; a < amount; a++)
                {
                    int rety=0;
                    do
                    {
                        Enemy en = GetRandomEnemy();
                        Vector2 pos = room.GetRandomSpawnPos(en);
                        if (!closedList.Contains(pos))
                        {
                            closedList.Add(pos);
                            en.Position = pos;
                            en.SetActivationRectangle();
                            AddEnemy(en);
                            CombatManager.AddEnemy(en);
                            break;
                        }
                        rety++;
                    } while (rety < 4);
                }
            }
        }
        ChooseEnemyForKey();
    }

    private static Enemy GetRandomEnemy()
    {
        Random rand = new Random();
        switch (rand.Next(0,4))
        {
            case 0: return new Walker(Level);
            case 1: return new Chaser(Level, Player);
            case 2: return new Turret(Level, Player);
            case 3: return new Gunner(Level, Player);
        }
        return new Walker(Level);
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