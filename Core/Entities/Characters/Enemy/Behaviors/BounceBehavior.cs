using System;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class BounceBehavior : Behavior
{
    private Vector2 oldDirection;
    private int count;
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
        
        
        if (Owner.Collides(oldDirection))
                return oldDirection;

        // Bounce
        oldDirection = Bounce(direction);

        if (count >= 2)
        {
            count = 0;
            direction = getRandomDirection();
            if(Owner.Collides(direction))
                return getRandomDirection();
        }

        return oldDirection;
        
    }

    private Vector2 getRandomDirection()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        switch (rand.Next(0, 5))
        {
            case 0 : 
                return new Vector2(1,1);
            case 1 :
                return new Vector2(1, -1);
            case 3 :
                return new Vector2(-1, 1);
            case 4:
                return new Vector2(-1, -1);
           default: return getRandomDirection();
        }
    }

    public Vector2 Bounce(Vector2 oldPos)
    {
        count++;
        if (oldPos == new Vector2(1, -1)) 
            return new Vector2(1, 1);
        
        if (oldDirection == new Vector2(1, 1))
            return new Vector2(-1, 1);

        if (oldDirection == new Vector2(-1, 1))
            return new Vector2(-1, -1);

        if (oldDirection == new Vector2(-1, -1))
            return new Vector2(1, -1);
        
        return oldPos;
    }


}