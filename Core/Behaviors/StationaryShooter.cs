using ECS2022_23.Core.Behaviors;
using ECS2022_23.Core.Manager;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class  StationaryShooter : TargetingBehavior
{
    
    private int _attackDelay;

    public StationaryShooter(Character target) : base(target)
    {
        
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim(Target);
        return Vector2.Zero;
    }

    public override void Attack()
    {
        if (++_attackDelay >= 75)
        {
            _attackDelay = 0;
            CombatManager.Shoot(Owner);
        }
    }
}