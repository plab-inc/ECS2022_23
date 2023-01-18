using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public abstract class Behavior
{
    public Enemy Owner { get; set;}
    protected int State { get; set;}

    protected Behavior()
    {
        State = (int)EnemyStates.Initial;
    }

    public virtual void Attack()
    {
        
    }

    public virtual void OnDeath()
    {
        
    }

    public abstract Vector2 Move(Vector2 position, float velocity);

}