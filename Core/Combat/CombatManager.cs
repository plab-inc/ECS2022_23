using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Combat;

public static class CombatManager
{

    public static void Update(Player player, Enemy enemy)
    {
        if (player.IsAttacking)
        {
            Attack(player, enemy);
        } else if (enemy.IsAttacking)
        {
            Attack(enemy, player);
        }
    }

    private static void Attack(Character attacker, Character defender)
    {
        var figureRect = attacker.Rectangle;
        var attackRect = new Rectangle(figureRect.X + figureRect.Width, figureRect.Y,
            figureRect.Width, figureRect.Height);
        
        //check if close enough for attack 
        //TODO check for left up down attack with aim
        
       if (figureRect.Intersects(defender.Rectangle) || attackRect.Intersects(defender.Rectangle))
       {
           
        if (defender.GetType() == typeof(Player))
        {
            //player gets hit
            Debug.WriteLine("player gets hit");
            var player = (Player)defender;
            var armor = player.Armor;
            armor -= attacker.Strength;
            if (armor < 0)
            {
                player.Armor = 0;
                player.HP += armor;
            }
            else
            {
                player.Armor = armor;
            }
        }
        else
        {
            //enemy gets hit
            defender.HP -= attacker.Strength;
            Debug.WriteLine("enemy gets hit" + defender.HP);
        }
        
        if (defender.HP <= 0)
        {
            Debug.WriteLine("defender dies");
            //dead remove from game play death animation
        }
       }
        attacker.IsAttacking = false;
    }
}