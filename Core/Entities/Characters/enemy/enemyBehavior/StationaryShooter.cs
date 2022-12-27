using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class StationaryShooter : Behavior
{
    private Character _target;

    public StationaryShooter(Character target)
    {
        _target = target;
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Aim(_target);
        return Vector2.Zero;
    }

    public void Aim(Character Target)
    {
        Owner.AimVector = Vector2.Normalize((Target.Position - Owner.Position));
    }
}