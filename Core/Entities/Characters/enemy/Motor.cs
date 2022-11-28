using ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy;

public abstract class Motor
{
    private Pathfinding path;
    private Level Level;
    public Enemy _enemy;

    public Motor(Level level)
    {
        Level = level;
    }

    public void setEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
    
   public abstract Vector2 Move(Vector2 position, int velocity);

}