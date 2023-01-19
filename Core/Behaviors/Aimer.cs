using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public abstract class Aimer : Behavior
{
    protected Aimer(Character target)
    {
        Target = target;
    }

    protected Character Target { get; }

    protected void Aim()
    {
        Owner.AimVector = Vector2.Normalize(Target.Position - Owner.Position);
    }
}