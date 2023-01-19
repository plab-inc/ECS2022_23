using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Manager;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public class Shooter : Aimer
{
    private int _attackDelay;

    public Shooter(Character target) : base(target)
    {
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim();
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