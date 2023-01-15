using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;

public abstract class Behavior
{
    private Pathfinding path;
    protected Enemy Owner;
    public int State { get; set;}

    protected Behavior()
    {
        State = (int)EnemyStates.Initial;
    }

    public void SetEnemy(Enemy enemy)
    {
        Owner = enemy;
    }
    
    public abstract Vector2 Move(Vector2 position, float velocity);

}