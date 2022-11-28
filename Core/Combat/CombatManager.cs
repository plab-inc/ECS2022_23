using System.Collections.Generic;
using System.Linq;
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
        if (!CheckCollision(attacker, defender)) return;
        defender.HP -= attacker.Strength;
        defender.SetAnimation("Hurt");

        if (!(defender.HP <= 0)) return;
        defender.SetAnimation("Death");
        attacker.Money += defender.MoneyReward;
        attacker.XpToNextLevel += defender.XpReward;
        defender.IsAlive = false;
        //TODO remove dead entity from game / entity-list / make invisible 
    }
    
    private static void EnemyAttack(Enemy attacker, Player defender)
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

    private static bool CheckCollision(Character attacker, Character defender) 
    {
        var figureRect = attacker.Rectangle;
        var defenderRect = defender.Rectangle;
        Rectangle attackRect; 

        if (figureRect.Intersects(defenderRect)) return true;
       
        switch (attacker.AimDirection)
        {
            case (int) Direction.Right:
                attackRect = new Rectangle(figureRect.X + figureRect.Width, figureRect.Y, figureRect.Width, figureRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Left:
                attackRect = new Rectangle(figureRect.X - figureRect.Width, figureRect.Y, figureRect.Width, figureRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Up:
                attackRect = new Rectangle(figureRect.X, figureRect.Y - figureRect.Height, figureRect.Width, figureRect.Height);
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Down:
                attackRect = new Rectangle(figureRect.X, figureRect.Y + figureRect.Height, figureRect.Width, figureRect.Height);
                return attackRect.Intersects(defenderRect);
            default: return false;
        }
    }
    
    public static void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }
}