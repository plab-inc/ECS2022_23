using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public abstract class TargetingBehavior : Behavior
{
    protected Character Target {get; }

    protected TargetingBehavior(Character target)
    {
        Target = target;
    }

    protected void Aim(Character target)
    {
        Owner.AimVector = Vector2.Normalize(target.Position - Owner.Position);
    }

}