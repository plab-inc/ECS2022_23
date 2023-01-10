using System;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class BounceBehavior : Behavior
{
    private Vector2 oldDirection;
    public override Vector2 Move(Vector2 position, float velocity)
    {
        Vector2 direction = oldDirection;
        if (State == (int) EnemyStates.Initial)
        {
            State = (int) EnemyStates.Move;
            direction = getRandomDirection();
            oldDirection = direction;
            return direction;
        }
        
        if (State == (int)EnemyStates.Move)
        {
            if (!Owner.Collides(oldDirection))
                return oldDirection;

            if (Owner.Collides(oldDirection))
            {
                // Bounce
                oldDirection *= -1;
                return oldDirection;
            }
        }


        return direction;
    }

    private Vector2 getRandomDirection()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        int temp = rand.Next(0, 5);
        Vector2 direction = new Vector2();
        switch (temp)
        {
            case 0 : 
                direction = new Vector2(1,1);
                break;
            case 1 :
                direction = new Vector2(1, -1);
                break;
            case 3 :
                direction = new Vector2(-1, 1);
                break;
            case 4:
                direction = new Vector2(-1, -1);
                break;
        }
        return direction;
    }


}