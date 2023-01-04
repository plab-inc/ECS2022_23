using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities;

public class ProjectileShot : Entity
{
    private Weapon Weapon { get; set; }
    private Direction AimDirection { get; set; }
    private Vector2 AimVector { get; set; }
    private Rectangle SourceRectangle { get; }
    public float DamagePoints { get; private set; }
    public bool HitTarget { get; set; }
    public Level Level { get; set; }

    public int Origin { get; set; }

    public ProjectileShot(Texture2D texture2D, Rectangle sourceRect, Weapon weapon, Direction aimDirection) : base(weapon.Position, texture2D)
    {
        SourceRectangle = sourceRect;
        Weapon = weapon;
        AimDirection = aimDirection;
        DamagePoints = weapon.DamagePoints;
        Origin = (int)DamageOrigin.Player;
    }

    public ProjectileShot(Enemy enemy, Texture2D texture2D, Rectangle sourceRect, Vector2 aimDirection) : base(enemy.Position, texture2D)
    {
        SourceRectangle = sourceRect;
        AimVector = aimDirection;
        DamagePoints = enemy.Strength;
        Origin = (int)DamageOrigin.Enemy;
    }


    public override void Update(GameTime gameTime)
    { 
        var speed = 2f;

        if (AimVector != Vector2.Zero)
        {
            Position += AimVector;
        }
        else
        {
            Position = AddToPositionPlayer(speed);
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
    }

    private Vector2 AddToPositionPlayer(float toAdd)
    {
        Vector2 result; 
        
        switch (AimDirection)
        {
            case Direction.Right:
                result = new Vector2(Position.X + toAdd, Position.Y);
                break;
            case Direction.Left:
                result = new Vector2(Position.X - toAdd, Position.Y);
                break;
            case Direction.Up:
                result = new Vector2(Position.X, Position.Y - toAdd);
                break;
            case Direction.Down:
                result = new Vector2(Position.X, Position.Y + toAdd);
                break;
            case Direction.None:
                result = new Vector2(Position.X + toAdd, Position.Y);
                break;
            default:
                result = new Vector2(Position.X + toAdd, Position.Y);
                break;
        }

        return result;
    }

    public bool Collides()
    {
        var bottom = new Point(Rectangle.Center.X, Rectangle.Bottom);
        return Level.GroundLayer.Any(rectangle => rectangle.Contains(bottom));
    }
}