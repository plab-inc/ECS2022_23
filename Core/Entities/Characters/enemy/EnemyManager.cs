using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.Game;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public class EnemyManager
{
    public List<Enemy> Enemies = new();
    private Player _player;
    private Level _level;
    private List<Enemy> EnemyTypes = new();

    public EnemyManager(Level level, Player player)
    {
        _level = level;
        _player = player;
        EnemyTypes.Add(new Walker(level));
        EnemyTypes.Add(new Chaser(level,_player));
    }

    public void AddEnemy(Enemy e)
    {
        Enemies.Add(e);
    }

    public void SpawnEnemies()
    {
        Random rand = new Random();
        foreach (var room in _level.Rooms)
        {
            if (room.Spawns != null && room.Spawns.Count > 0)
            {
                Enemy en = GetRandomEnemy();
                en.Position = room.GetRandomSpawnPos(en);
                AddEnemy(en);
            }
        }
    }

    private Enemy GetRandomEnemy()
    {
        Random rand = new Random();
        // rand.Next(0, EnemyTypes.Count
        switch (1)
        {
            case 0: return new Walker(_level);
            case 1: return new Chaser(_level, _player); // TODO Chaser does not get the correct Coordinates
        }
        return new Walker(_level);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies)
        {
            enemy.Update(gameTime);
        }
        Debug.WriteLine(_player.Position + " " + Enemies.First().Position);
    }
}