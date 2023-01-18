using ECS2022_23.Core.Entities.Characters.Enemy.Behaviors;
using ECS2022_23.Core.Manager;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Behaviors;

public class ExploderBehavior : RandomBehavior
{
    public override void OnDeath()
    {
        CombatManager.Shoot(Owner.Position, new Vector2(0, -1), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(1, -1), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(1, 0), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(1, 1), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(0, 1), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(-1, 1), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(-1, 0), Owner.Stage);
        CombatManager.Shoot(Owner.Position, new Vector2(-1, -1), Owner.Stage);
    }
}