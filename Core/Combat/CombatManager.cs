using System.Collections.Generic;
using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.enemy;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Combat;

public static class CombatManager
{
    private static Rectangle currentRec;
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
        if (CheckCollision(attacker, defender))
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
                //TODO remove dead entity from game / list / make unvisible
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
                currentRec = attackRect;
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Left:
                attackRect = new Rectangle(figureRect.X - figureRect.Width, figureRect.Y, figureRect.Width, figureRect.Height);
                currentRec = attackRect;
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Up:
                attackRect = new Rectangle(figureRect.X, figureRect.Y - figureRect.Height, figureRect.Width, figureRect.Height);
                currentRec = attackRect;
                return attackRect.Intersects(defenderRect);
            case (int) Direction.Down:
                attackRect = new Rectangle(figureRect.X, figureRect.Y + figureRect.Height, figureRect.Width, figureRect.Height);
                currentRec = attackRect;
                return attackRect.Intersects(defenderRect);
            default: return false;
        }
    }

    public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        var recTexture = new Texture2D(graphicsDevice, 1, 1);
        recTexture.SetData(new Color[] { Color.Green });
        
        spriteBatch.Draw(recTexture, currentRec, Color.White);
    }
}