using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Motor
{
    private Pathfinding path;
    public Enemy Enemy;
    public Vector2 TargetPosition { set; get; }

    public Motor(Level? level)
    {
        
    }

    public void SetEnemy(Enemy enemy)
    {
        Enemy = enemy;
    }
    
    public abstract Vector2 Move(Vector2 position, float velocity);

}