using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public abstract class TargetingBehavior : Behavior
{
    protected TargetingBehavior(Character target)
    {
        Target = target;
    }

    protected Character Target { get; }

    protected void Aim()
    {
        Owner.AimVector = Vector2.Normalize(Target.Position - Owner.Position);
    }
}