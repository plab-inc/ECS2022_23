using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Motor
{
    private Pathfinding path;
    protected Enemy Enemy;
    
    
    public void SetEnemy(Enemy enemy)
    {
        Enemy = enemy;
    }
    
    public abstract Vector2 Move(Vector2 position, float velocity);

}