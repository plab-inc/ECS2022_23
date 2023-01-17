using ECS2022_23.Core.Behaviors;
using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class Boss : TargetingBehavior
{
    public Boss(Character target) : base(target)
    {
        
    }
    
    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim(Target);
        Vector2 direction = Vector2.Normalize(Target.Position - position) * velocity;
        if(Owner.Collides(direction))
            return direction;
        
        return Vector2.Zero;
    }
    
    
    
}