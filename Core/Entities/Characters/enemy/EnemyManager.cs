using System;
using System.Collections.Generic;
using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class EnemyManager
{
    public List<Enemy> Enemies = new();
    private Player Player;
    private Escape _escape;

    public EnemyManager(Escape escape)
    {
        _escape = escape;
    }

    public void AddEnemy(Enemy e)
    {
        Enemies.Add(e);
    }

    public void SpawnEnemies()
    {
        Random rand = new Random();
        int count = 0;
        foreach (var room in _escape._level.Rooms)
        {
            if (room.Spawns != null && room.Spawns.Count > 0)
            {
                Debug.WriteLine(room.MapName);
                //Vector2 spawnPoint = room.Spawns[rand.Next(0, room.Spawns.Count)];
                var en = new Enemy(Vector2.Zero, ContentLoader.EnemyTexture, new RandomMotor(_escape._level), _escape._level);
                en.Position = room.GetRandomSpawnPos(en);
                AddEnemy(en);
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies)
        {
            enemy.Update(gameTime);
            Debug.WriteLine(Enemies[0].Position);
        }
    }
}