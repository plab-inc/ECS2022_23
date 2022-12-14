using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class ChaseMotor : Motor
{
    
    public ChaseMotor(Level level, Vector2 targetPosition) : base(level)
    {
        TargetPosition = targetPosition;
    }

    public override Vector2 Move(Vector2 position, int velocity)
    {
        Vector2 vec = Vector2.Zero;

        // This is just a test implementation. Will be reworked later.
        if (position.X < TargetPosition.X)
        {
            vec.X += velocity;
            // Prevents overshooting of the target. 
            if (position.X > TargetPosition.X)
                vec.X = TargetPosition.X;
        }
        else if (position.X > TargetPosition.X)
        {
            vec.X -= velocity;
            if (position.X < TargetPosition.X)
                vec.X = TargetPosition.X;
        }

        if (position.Y <= TargetPosition.Y)
        {
            vec.Y += velocity;
            if (position.Y > TargetPosition.Y)
            {
                vec.Y = TargetPosition.Y;
            }
        }
        else if (position.Y > TargetPosition.Y)
        {
            vec.Y -= velocity;
            if (position.Y < TargetPosition.Y)
            {
                vec.Y = TargetPosition.Y;
            }
        }

        if (!Enemy.Collides(vec))
        {
            return vec;
        }
        return Vector2.Zero;
    }
    
}