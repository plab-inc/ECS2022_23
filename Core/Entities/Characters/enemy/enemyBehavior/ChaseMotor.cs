using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class ChaseMotor : Motor
{
    private Vector2 _targetPosition;
    
    public ChaseMotor(Level level, Vector2 targetPosition) : base(level)
    {
        _targetPosition = targetPosition;
    }

    public override Vector2 Move(Vector2 position, int velocity)
    {
        Vector2 vec = Vector2.Zero;

        // This is just a test implementation. Will be reworked later.
        if (position.X <= _targetPosition.X)
        {
            vec.X += velocity;
            // Prevents overshooting of the target. 
            if (position.X > _targetPosition.X)
                vec.X = _targetPosition.X;
        }
        else if (position.X > _targetPosition.X)
        {
            vec.X -= velocity;
            if (position.X < _targetPosition.X)
                vec.X = _targetPosition.X;
        }

        if (position.Y <= _targetPosition.Y)
        {
            vec.Y += velocity;
            if (position.Y > _targetPosition.Y)
            {
                vec.Y = _targetPosition.Y;
            }
        }
        else if (position.Y > _targetPosition.Y)
        {
            vec.Y -= velocity;
            if (position.Y < _targetPosition.Y)
            {
                vec.Y = _targetPosition.Y;
            }
        }
        return vec;
    }
    
}