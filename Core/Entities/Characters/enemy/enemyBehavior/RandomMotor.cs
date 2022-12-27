using System;
using System.Diagnostics;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class RandomMotor : Motor
{
    private int _delay=0;
    Random rand = new Random((int)DateTime.Now.Ticks);
    private int oldDirection = 0;
   
    public override Vector2 Move(Vector2 position, float velocity)
    {
        // RandomEnemy chooses a Direction and stays on it for X seconds
        _delay++;
        int newDirection=oldDirection;
        
        // This binds the speed of directional change to the FPS. Could result in some unexpected behavior should the FPS change.
        if (_delay >= 15)
        {
            _delay = 0;
            newDirection = rand.Next(0, 4);
            Debug.WriteLine(newDirection);
        }
        oldDirection = newDirection;
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
        } while (!Owner.Collides(temp) && retry<4);

        if (retry>=4)
        {
            return Vector2.Zero;
        }
        return temp;
    }
    
    
}