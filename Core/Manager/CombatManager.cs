using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Sound;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using ECS2022_23.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class CombatManager
{
    public static List<Enemy> _activeEnemies = new List<Enemy>();
    private static List<ProjectileShot> _activeShotsByPlayer = new List<ProjectileShot>();
    private static List<ProjectileShot> _activeShotsByEnemy = new List<ProjectileShot>();
    private static Timer _damageCooldown;

    public static void Init()
    {
        _activeEnemies = new List<Enemy>();
        _activeShotsByPlayer = new List<ProjectileShot>();
        _activeShotsByEnemy = new List<ProjectileShot>();
        _damageCooldown = new Timer(2f);
    }
    
    public static void Update(GameTime gameTime, Player player)
    {
        if (player.Invincible)
        {
            _damageCooldown.Update(gameTime);
            if (_damageCooldown.LimitReached())
            {
                player.Invincible = false;
            }
        }
        
        CheckShotPlayerCollision(player);
        
        if (!player.Invincible)
        {
            CheckEnemyCollision(player);
        }
      
        if (player.IsAttacking)
        {
            foreach (var enemy in _activeEnemies)
            {
                PlayerAttack(player, enemy);
            }

            player.IsAttacking = false;
        }

        foreach (var enemy in _activeEnemies)
        {
            if (enemy.IsAlive())
            {
                CheckShotEnemyCollision(enemy, player);
            }
            if (enemy.IsAttacking && enemy.IsAlive())
            {
                EnemyAttack(enemy, player);
            }
        }
        
        foreach (var shot in _activeShotsByPlayer)
        {
            shot.Update(gameTime);
        }
        foreach (var shot in _activeShotsByEnemy)
        {
            shot.Update(gameTime);
        }
        _activeShotsByPlayer.RemoveAll(shot => shot.HitTarget || !shot.IsInAir());
        _activeShotsByEnemy.RemoveAll(shot => shot.HitTarget || !shot.IsInAir());
        foreach (var enemy in _activeEnemies.Where(enemy => !enemy.IsAlive()))
        {
            enemy.OnDeath();
        }
        
        _activeEnemies.RemoveAll(enemy => !enemy.IsAlive());
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shot in _activeShotsByPlayer)
        {
            shot.Draw(spriteBatch);
        }
        foreach (var shot in _activeShotsByEnemy)
        {
            shot.Draw(spriteBatch);
        }
    }
    
    private static void PlayerAttack(Player attacker, Enemy defender)
    {
        if (!defender.IsAlive()) return;
        if (!EntitiesCollide(attacker, defender) && !WeaponCollide(attacker, defender)) return;

        if (attacker.Weapon == null)
        {
            defender.HP -= attacker.Strength;
        }
        else
        {
            defender.HP -= (attacker.Strength + attacker.Weapon.DamagePoints);
        }
        defender.SetAnimation(AnimationType.Hurt);

        if (defender.HP > 0) return;
        EnemyDies(defender, attacker);
    }
    
    private static void EnemyAttack(Enemy attacker, Player player)
    {
        if (!player.IsAlive()) return;
        
        if (EntitiesCollide(attacker, player))
        {
            player.TakesDamage(attacker.Strength, attacker);
        }
        attacker.IsAttacking = false;
    }

    private static bool EntitiesCollide(Entity attacker, Entity defender) 
    {
        return attacker.IntersectPixels(defender);
    }

    private static bool WeaponCollide(Player attacker, Entity defender)
    {
        if (attacker.Weapon == null) return false;
        var figureRect = attacker.Rectangle;
        var weaponRect = attacker.Weapon.Rectangle;
        Rectangle attackRect; 
        
        switch (attacker.AimDirection)
        {
            case Direction.Right:
                attackRect = new Rectangle(figureRect.X + figureRect.Width, figureRect.Y, weaponRect.Width, weaponRect.Height);
                break;
            case Direction.Left:
                attackRect = new Rectangle(figureRect.X - weaponRect.Width, figureRect.Y, weaponRect.Width, weaponRect.Height);
                break;
            case Direction.Up:
                attackRect = new Rectangle(figureRect.X, figureRect.Y - weaponRect.Width, weaponRect.Width, weaponRect.Height);
                break;
            case Direction.Down:
                attackRect = new Rectangle(figureRect.X, figureRect.Y + figureRect.Height, weaponRect.Width, weaponRect.Height);
                break;
            default: return false;
        }
        return defender.IntersectPixels(attacker.Weapon);
    }
    
    public static void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }

    public static void Shoot(Player player)
    {
        var shot = ItemLoader.CreateLaserShot(player.Weapon, player.AimDirection);
        shot.Stage = player.Stage;
        _activeShotsByPlayer.Add(shot);
    }

    public static void Shoot(Enemy enemy)
    {
        var shot = ItemLoader.CreateLaserShot(enemy);
        shot.Stage = enemy.Stage;
        _activeShotsByEnemy.Add(shot);
    }

    public static void Shoot(Vector2 position, Vector2 direction, Stage stage)
    {
        var shot = ItemLoader.CreateLaserShot(position, direction);
        shot.Stage = stage;
        _activeShotsByEnemy.Add(shot);
    }

    private static void CheckShotEnemyCollision(Enemy enemy, Player player)
    {
        foreach (var projectileShot in _activeShotsByPlayer.Where(projectileShot => projectileShot.IntersectPixels(enemy) &&
                                                                            projectileShot.DamageOrigin == (int)DamageOrigin.Player))
        {
            enemy.HP -= projectileShot.DamagePoints + player.Strength;
            enemy.SetAnimation(AnimationType.Hurt);
            projectileShot.HitTarget = true;
            if (enemy.HP > 0) continue;
            EnemyDies(enemy, player);
            return;
        }
    }

    private static void CheckShotPlayerCollision(Player player)
    {
        foreach (var projectileShot in _activeShotsByEnemy.Where(projectileShot => projectileShot.IntersectPixels(player) && projectileShot.DamageOrigin == DamageOrigin.Enemy))
        {
            player.TakesDamage(projectileShot.DamagePoints, projectileShot);
            projectileShot.HitTarget = true;
            return;
        }
    }
  
    public static void EnemyDies(Enemy enemy, Player player)
    {
        enemy.SetAnimation(AnimationType.Death);
        SoundManager.Play(enemy.DeathSound);
        ItemManager.DropLoot(enemy);
        EnemyManager.RemoveEnemy(enemy);
        player.EP += enemy.EpReward;
    }

    private static void CheckEnemyCollision(Player player)
    {
        foreach (var enemy in _activeEnemies)
        {
            if (EntitiesCollide(player, enemy))
            {
                player.TakesDamage(enemy.Strength, enemy);
                return;
            }
        }
    }
}