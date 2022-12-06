using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities;

public class ProjectileShot : Entity
{
    private Weapon Weapon { get; set; }
    private int AimDirection { get; set; }
    private Rectangle SourceRectangle { get; }
    private Vector2 _endOfRange;
    public float DamagePoints { get; private set; }
    public bool HitTarget { get; set; }
    public Level Level { get; set; }

    public ProjectileShot(Texture2D texture2D, Rectangle sourceRect, Weapon weapon, int aimDirection) : base(weapon.Position, texture2D)
    {
        SourceRectangle = sourceRect;
        Weapon = weapon;
        AimDirection = aimDirection;
        DamagePoints = weapon.DamagePoints;
        _endOfRange = AddToPosition(Weapon.Range);
    }

    public override void Update(GameTime gameTime)
    { 
        var speed = 2f;
        Position = AddToPosition(speed);
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
    }
    
    public bool IsWithinRange()
    {
        switch (AimDirection)
        {
            case (int) Direction.Right:
                if (Position.X > _endOfRange.X) return false;
                break;
            case (int)Direction.Left:
                if (Position.X < _endOfRange.X) return false;
                break;
            case (int)Direction.Up:
                if (Position.Y < _endOfRange.Y) return false;
                break;
            case (int)Direction.Down:
                if (Position.Y > _endOfRange.Y) return false;
                break;
            case (int)Direction.None:
                if (Position.X > _endOfRange.X) return false;
                break;
            default:
                if (Position.X > _endOfRange.X) return false;
                break;
        }

        return true;
    }
    
    private Vector2 AddToPosition(float toAdd)
    {
        Vector2 result; 
        switch (AimDirection)
        {
            case (int) Direction.Right:
                result = new Vector2(Position.X + toAdd, Position.Y);
                break;
            case (int)Direction.Left:
                result = new Vector2(Position.X - toAdd, Position.Y);
                break;
            case (int)Direction.Up:
                result = new Vector2(Position.X, Position.Y - toAdd);
                break;
            case (int)Direction.Down:
                result = new Vector2(Position.X, Position.Y + toAdd);
                break;
            case (int)Direction.None:
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
        var onGround = false;
        var bottom = new Point(Rectangle.Center.X, Rectangle.Bottom);

        foreach (var rectangle in Level.GroundLayer)
        {
            if (rectangle.Contains(bottom))
            {
                onGround = true;
                break;
            }
        }

        return onGround;
    }
}