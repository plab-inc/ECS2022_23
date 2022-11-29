using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Combat;

public static class CombatManager
{
    private static List<Enemy> _activeEnemies = new List<Enemy>();
    public static void Update(Player player)
    {
        if (player.IsAttacking)
        {
            foreach (var enemy in _activeEnemies)
            {
                PlayerAttack(player, enemy);
            }

            player.IsAttacking = false;
        }

        foreach (var enemy in _activeEnemies.Where(enemy => enemy.IsAttacking))
        {
            //muss noch mit mehreren enemies getestet werden (gleichzeitige Angriffe z.B.)
            EnemyAttack(enemy, player);
        }
    }
    private static void PlayerAttack(Player attacker, Enemy defender)
    {
        if (!defender.IsAlive) return;
        if (!CheckCollision(attacker, defender) && !CheckWeaponRange(attacker, defender)) return;
        defender.HP -= (attacker.Strength + attacker.Weapon.DamagePoints);
        defender.SetAnimation("Hurt");

        if (!(defender.HP <= 0)) return;
        defender.SetAnimation("Death");
        attacker.Money += defender.MoneyReward;
        attacker.XpToNextLevel += defender.XpReward;
        defender.IsAlive = false;
        //TODO remove dead entity from game / entity-list / make invisible 
    }
    
    private static void EnemyAttack(Character attacker, Player defender)
    {
        if (!defender.IsAlive) return;
        if (CheckCollision(attacker, defender))
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
                //TODO remove dead entity from game / entity-list / make invisible 
            }
        }
        attacker.IsAttacking = false;
    }

    private static bool CheckCollision(Entity attacker, Entity defender) 
    {
        var figureRect = attacker.Rectangle;
        var defenderRect = defender.Rectangle;

        return figureRect.Intersects(defenderRect);
    }

    private static bool CheckWeaponRange(Player attacker, Entity defender)
    {
        var figureRect = attacker.Rectangle;
        var defenderRect = defender.Rectangle;
        var weaponRect = attacker.Weapon.Rectangle;
        var weapon = attacker.Weapon;
        Rectangle attackRect; 
        
        switch (attacker.AimDirection)
        {
            case (int) Direction.Right:
                attackRect = new Rectangle(figureRect.X + figureRect.Width, figureRect.Y, (int) weapon.Range, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Left:
                attackRect = new Rectangle(figureRect.X - (int) weapon.Range, figureRect.Y, (int) weapon.Range, weaponRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Up:
                attackRect = new Rectangle(figureRect.X, figureRect.Y - (int) weapon.Range, weaponRect.Height, (int) weapon.Range);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Down:
                attackRect = new Rectangle(figureRect.X, figureRect.Y + figureRect.Height, weaponRect.Height, (int) weapon.Range);
                return attackRect.Intersects(defenderRect);
            default: return false;
        }
    }
    
    public static void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }
}