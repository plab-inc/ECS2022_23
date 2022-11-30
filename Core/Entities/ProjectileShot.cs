using ECS2022_23.Core.Entities.Items;
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

    public ProjectileShot(Texture2D texture2D, Rectangle sourceRect, Weapon weapon, int aimDirection) : base(weapon.Position, texture2D)
    {
        SourceRectangle = sourceRect;
        Weapon = weapon;
        AimDirection = aimDirection;
        DamagePoints = weapon.DamagePoints;
        _endOfRange = UpdateVectors(_endOfRange, Weapon.Range);
    }

    public override void Update(GameTime gameTime)
    { 
        UpdateShotPosition();
        //AnimationManager.Update(gameTime);
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
        //AnimationManager.Draw(spriteBatch, Position);
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
    private void UpdateShotPosition()
    {
        var speed = 2f;
        Position = UpdateVectors(Position, speed);
    }

    private Vector2 UpdateVectors(Vector2 result, float toAdd)
    {
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
}