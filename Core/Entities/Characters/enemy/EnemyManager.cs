using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
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
    private List<Enemy> _enemyTypes = new();

    public EnemyManager(Level level, Player player)
    {
        _level = level;
        _player = player;
        _enemyTypes.Add(new Walker(level));
        _enemyTypes.Add(new Chaser(level,_player));
    }

    private void AddEnemy(Enemy e)
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
                CombatManager.AddEnemy(en);
            }
        }
    }

    private Enemy GetRandomEnemy()
    {
        Random rand = new Random();
        // rand.Next(0, EnemyTypes.Count)
        switch (0)
        {
            case 0: return new Walker(_level, AnimationLoader.CreateBlobEnemyAnimations());
            case 1: return new Chaser(_level, _player);
        }
        return new Walker(_level);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies)
        {
            enemy.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var en in Enemies)
        {
            en.Draw(spriteBatch);
        }
    }

}