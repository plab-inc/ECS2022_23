using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Motor
{
    private Pathfinding path;
    private Level _level;
    public Enemy Enemy;
    public Vector2 TargetPosition;

    public Motor(Level? level)
    {
        _level = level;
    }

    public void SetEnemy(Enemy enemy)
    {
        Enemy = enemy;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        TargetPosition = targetPosition;
    }
    
    public abstract Vector2 Move(Vector2 position, int velocity);

}