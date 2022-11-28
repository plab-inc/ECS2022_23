using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class ChaseMotor : Motor
{
    private Entity _target;
    
    public ChaseMotor(Entity target, Level level, Enemy enemy) : base(level)
    {
        _target = target;
    }

    public override Vector2 Move(Vector2 position, int velocity)
    {
        // This is just a test implementation. Will be reworked later.
        if (position.X <= _target.Position.X)
        {
            position.X += velocity;
            // Prevents overshooting of the target. 
            if (position.X > _target.Position.X)
                position.X = _target.Position.X;
        }
        else if (position.X > _target.Position.X)
        {
            position.X -= velocity;
            if (position.X < _target.Position.X)
                position.X = _target.Position.X;
        }

        if (position.Y <= _target.Position.Y)
        {
            position.Y += velocity;
            if (position.Y > _target.Position.Y)
            {
                position.Y = _target.Position.Y;
            }
        }
        else if (position.Y > _target.Position.Y)
        {
            position.Y -= velocity;
            if (position.Y < _target.Position.Y)
            {
                position.Y = _target.Position.Y;
            }
        }

        return position;
    }
}