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
        Vector2 direction;
        if (State == (int) EnemyStates.Initial)
        {
            State = (int) EnemyStates.Move;
            direction = GetRandomDirection();
            if(Owner.Collides(direction))
                oldDirection = direction;
            return oldDirection;
        }
        
        
        if (Owner.Collides(oldDirection))
                return oldDirection*velocity;

        // Bounce
        oldDirection = Bounce(oldDirection);

        if (count >= 2)
        {
            count = 0;
            direction = GetRandomDirection();
            if(Owner.Collides(direction))
                oldDirection = direction;
        }

        return oldDirection*velocity;
        
    }

    private Vector2 GetRandomDirection()
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
           default: return GetRandomDirection();
        }
    }

    private Vector2 Bounce(Vector2 oldPos)
    {
        count++;
        if (oldPos == new Vector2(1, -1)) 
            return new Vector2(1, 1);
        
        if (oldPos == new Vector2(1, 1))
            return new Vector2(-1, 1);

        if (oldPos == new Vector2(-1, 1))
            return new Vector2(-1, -1);

        if (oldPos == new Vector2(-1, -1))
            return new Vector2(1, -1);
        
        return oldPos;
    }
}