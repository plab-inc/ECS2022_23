using System.Diagnostics;
using ECS2022_23.Core.World;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Entities.Characters.enemy.enemyBehavior;

public class Chase : Behavior
{
    private Character Target;
    public Chase(Character target)
    {
        Target = target;
    }

    public override Vector2 Move(Vector2 position, float velocity)
    {
        Vector2 direction = Target.Position - position;
        direction.Normalize();
        if(!Owner.Collides(direction * velocity))
            return direction * velocity;
        
        return Vector2.Zero;
    }
    
    /*
    public override Vector2 Move(Vector2 position, float velocity)
    {
        
        Vector2 change = Vector2.Zero;
        
        if (position.X < Target.Position.X)
        {
            change.X += velocity;
            if (position.X + change.X > Target.Position.X)
                change.X = (position.X - change.X) + Target.Position.X;
        }
        else if (position.X > Target.Position.X)
        {
            change.X -= velocity;
            if (position.X + change.X < Target.Position.X)
                change.X = (-position.X - change.X) + Target.Position.X;
        }
        if (position.Y < Target.Position.Y)
        {
            change.Y += velocity;
            if (position.Y + change.Y > Target.Position.Y)
                change.Y = (position.Y - change.Y) + Target.Position.Y;
        }
        else if (position.Y > Target.Position.Y)
        {
            change.Y -= velocity;
            if (position.Y + change.Y < Target.Position.Y)
                change.Y = (-position.Y - change.Y) + Target.Position.Y;
        }
        
        Debug.WriteLine(change);
        
        if (!Owner.Collides(change))
        {
            return change;
        }
        return change;
    }
    */
    
}