using ECS2022_23.Core.Behaviors;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class  StationaryShooter : TargetingBehavior
{

    public StationaryShooter(Character target) : base(target)
    {
        
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim(Target);
        return Vector2.Zero;
    }
    
}