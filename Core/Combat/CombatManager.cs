using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Combat;

public static class CombatManager
{
    private static List<Enemy> _activeEnemies = new List<Enemy>();
    private static List<ProjectileShot> _activeShots = new List<ProjectileShot>();
    public static void Update(GameTime gameTime, Player player)
    {
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
            //muss noch mit mehreren enemies getestet werden (gleichzeitige Angriffe z.B.)
            if (enemy.IsAlive)
            {
                CheckShotEnemyCollision(enemy, player);
            }
            if (enemy.IsAttacking && enemy.IsAlive)
            {
                EnemyAttack(enemy, player);
            }
        }
        
        foreach (var shot in _activeShots)
        {
            shot.Update(gameTime);
        }
        _activeShots.RemoveAll(shot => !shot.IsWithinRange() || shot.HitTarget);
        _activeEnemies.RemoveAll(enemy => !enemy.IsAlive);
    }
    private static void PlayerAttack(Player attacker, Enemy defender)
    {
        if (!defender.IsAlive) return;
        if (!EntitiesCollide(attacker, defender) && !WeaponCollide(attacker, defender)) return;
        defender.HP -= (attacker.Strength + attacker.Weapon.DamagePoints);
        defender.SetAnimation("Hurt");

        if (defender.HP > 0) return;
        EnemyDies(defender, attacker);
    }
    
    private static void EnemyAttack(Character attacker, Player defender)
    {
        if (!defender.IsAlive) return;
        if (EntitiesCollide(attacker, defender))
        {
            var armor = defender.Armor;
            armor -= attacker.Strength;
            if (armor < 0)
            {
                defender.Armor = 0;
                defender.HP += armor;
                defender.SetAnimation("Hurt");
            }
            else
            {
                defender.Armor = armor;
            }
           
            if (defender.HP <= 0)
            {
                defender.SetAnimation("Death");
                defender.IsAlive = false;
            }
        }
        attacker.IsAttacking = false;
    }

    private static bool EntitiesCollide(Entity attacker, Entity defender) 
    {
        var figureRect = attacker.Rectangle;
        var defenderRect = defender.Rectangle;

        return figureRect.Intersects(defenderRect);
    }

    private static bool WeaponCollide(Player attacker, Entity defender)
    {
        var figureRect = attacker.Rectangle;
        var defenderRect = defender.Rectangle;
        var weaponRect = attacker.Weapon.Rectangle;
        Rectangle attackRect; 
        
        switch (attacker.AimDirection)
        {
            case (int) Direction.Right:
                attackRect = new Rectangle(figureRect.X + figureRect.Width, figureRect.Y, weaponRect.Width, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Left:
                attackRect = new Rectangle(figureRect.X - weaponRect.Width, figureRect.Y, weaponRect.Width, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Up:
                attackRect = new Rectangle(figureRect.X, figureRect.Y - weaponRect.Width, weaponRect.Width, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Down:
                attackRect = new Rectangle(figureRect.X, figureRect.Y + figureRect.Height, weaponRect.Width, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            default: return false;
        }
    }
    
    public static void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }

    public static void Shoot(Player player)
    {
        var shot = AnimationLoader.CreateLaserShot(player.Weapon, player.AimDirection);
        _activeShots.Add(shot);
    }
    private static void CheckShotEnemyCollision(Enemy enemy, Player player)
    {
        foreach (var shot in _activeShots.Where(shot => shot.Rectangle.Intersects(enemy.Rectangle)))
        {
            enemy.HP -= shot.DamagePoints + player.Strength;
            enemy.SetAnimation("Hurt");
            shot.HitTarget = true;
            if (enemy.HP > 0) continue;
            EnemyDies(enemy, player);
            return;
        }
    }
  
    private static void EnemyDies(Enemy enemy, Player player)
    {
        enemy.SetAnimation("Death");
        ItemManager.DropRandomLoot(enemy.Position);
        player.Money += enemy.MoneyReward;
        player.XpToNextLevel += enemy.XpReward;
        enemy.IsAlive = false;
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var shot in _activeShots)
        {
            shot.Draw(spriteBatch);
        }
    }
}