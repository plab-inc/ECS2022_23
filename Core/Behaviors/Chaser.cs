using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public class Chaser : Aimer
{
    public Chaser(Character target) : base(target)
    {
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim();
        var direction = Vector2.Normalize(Target.Position - position) * velocity;
        if (Owner.Collides(direction))
            return direction;

        return Vector2.Zero;
    }
}