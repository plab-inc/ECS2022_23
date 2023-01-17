using System;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class RandomBehavior : Behavior
{
    private int _delay;
    private int _oldDirection;
   
    public override Vector2 Move(Vector2 position, float velocity)
    {
        Owner.Attack();
        Random rand = new ((int)DateTime.Now.Ticks);
        _delay++;
        int newDirection=_oldDirection;
        
        if (_delay >= 15)
        {
            _delay = 0;
            newDirection = rand.Next(0, 4);
        }
        _oldDirection = newDirection;
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