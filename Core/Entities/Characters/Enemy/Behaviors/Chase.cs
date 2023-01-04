using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class Chase : Behavior
{
    private Character Target;
    public Chase(Character target)
    {
        Target = target;
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Vector2 direction = Vector2.Normalize(Target.Position - position) * velocity;
        if(Owner.Collides(direction))
            return direction;
        
        return Vector2.Zero;
    }
}