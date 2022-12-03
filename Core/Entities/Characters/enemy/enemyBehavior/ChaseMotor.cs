using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class ChaseMotor : Motor
{
    private Entity _target;
    
    public ChaseMotor(Level level, Entity target) : base(level)
    {
        _target = target;
    }

    public override Vector2 Move(Vector2 position, int velocity)
    {
        Vector2 vec = Vector2.Zero;

        // This is just a test implementation. Will be reworked later.
        if (vec.X <= _target.Position.X)
        {
            vec.X += velocity;
            // Prevents overshooting of the target. 
            if (vec.X > _target.Position.X)
                vec.X = _target.Position.X;
        }
        else if (vec.X > _target.Position.X)
        {
            vec.X -= velocity;
            if (vec.X < _target.Position.X)
                vec.X = _target.Position.X;
        }

        if (vec.Y <= _target.Position.Y)
        {
            vec.Y += velocity;
            if (vec.Y > _target.Position.Y)
            {
                vec.Y = _target.Position.Y;
            }
        }
        else if (vec.Y > _target.Position.Y)
        {
            vec.Y -= velocity;
            if (vec.Y < _target.Position.Y)
            {
                vec.Y = _target.Position.Y;
            }
        }
        return vec;
    }
    
}