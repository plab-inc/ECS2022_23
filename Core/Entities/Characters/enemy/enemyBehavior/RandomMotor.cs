using System;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class RandomMotor : Motor
{
    int delay=0;
    Random rand = new Random((int)DateTime.Now.Ticks);
    private int LastDirection;
    
    public RandomMotor(Level level) : base(level)
    {
        
    }
    
    public override Vector2 Move(Vector2 position, int velocity)
    {
        // Enemy chooses a Direction and stays on it for X seconds
        
        delay++;
        int newDirection = LastDirection;
        
        // This binds the speed of directional change to the FPS. Could result in some unexpected behavior should the FPS change.
        if (delay >= 15)
        {
            delay = 0;
            newDirection = rand.Next(0, 4);
        }

        LastDirection = newDirection;

        Vector2 temp = Vector2.Zero;
        
        // Directions in Order: UP, DOWN, LEFT, Right
        int retry = 0;
        do
        {
            switch (newDirection)
            {
                case 0 :
                    temp = new Vector2(0, velocity);
                    break;
                case 1:
                    temp = new Vector2(0, -velocity);
                    break;
                case 2:
                    temp = new Vector2(-velocity, 0);
                    break;
                case 3:
                    temp = new Vector2(velocity, 0);
                    break;
            }

            retry++;
        } while (!_enemy.CollidesWithWall2(temp) && retry<4);

        if (retry>=4)
        {
            return Vector2.Zero;
        }
        
        return temp;
        
    }
}