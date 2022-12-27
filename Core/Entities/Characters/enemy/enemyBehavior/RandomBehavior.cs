using System;
using System.Diagnostics;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class RandomBehavior : Behavior
{
    private int _delay=0;
    Random rand = new ((int)DateTime.Now.Ticks);
    private int oldDirection;
   
    public override Vector2 Move(Vector2 position, float velocity)
    {
        _delay++;
        int newDirection=oldDirection;
        
        if (_delay >= 15)
        {
            _delay = 0;
            newDirection = rand.Next(0, 4);
        }
        oldDirection = newDirection;
        Vector2 temp = Vector2.Zero;
        
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