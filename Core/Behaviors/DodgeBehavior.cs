using ECS2022_23.Core.Behaviors;
using ECS2022_23.Core.Manager;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public class  Dodger : TargetingBehavior
{
    private int _delay;
    
    public Dodger(Character target) : base(target)
    {
       
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim(Target);
        
        if (State is (int)EnemyStates.Initial or (int)EnemyStates.Attack)
        {
            return Vector2.Zero;
        }

        if (State == (int) EnemyStates.Move)
        {
            Vector2 direction = -Vector2.Normalize(Target.Position - position) * velocity;
            if (Owner.Collides(direction))
                return direction;
        }
        
        return Vector2.Zero;
    }

    public override void Attack()
    {
        if(!Owner.IsActive)
            return;
        
        if (Vector2.Distance(Owner.Position, Target.Position)>50)
        {
            if (++_delay > 50)
            {
                _delay = 0;
                CombatManager.Shoot(Owner);
            }
        }
        else
        {
            State = (int)EnemyStates.Move;
        }
    }
}