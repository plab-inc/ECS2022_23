using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core.Entities.Characters;

public class Player : Character
{
    public float Armor;
    public bool IsAlive;
    public float Level;
    public float XpToNextLevel;
    public float Money;

    private float speed = 3;
    public List<Item> Items;
    private Weapon _weapon;
    private Input _input;
    

    private Level level;
    
    public Player(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations) : base(spawn, texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
    }
    public Player(Texture2D texture, Dictionary<string, Animation> animations) : base(Vector2.Zero,texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
    }
    
    public void setLevel(Level level)
    {
        this.level = level;
    }

    public bool Collides(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();
        var rect = new Rectangle(newPoint, new Point(Texture.Width, Texture.Height));

        //TODO clean this mess up
        
        var armHitBoxLeft =
            new Rectangle(newPoint.X + 4, newPoint.Y + Texture.Height / 2 + 2, 1, Texture.Height / 2 - 2);
        var armHitBoxRight = new Rectangle(newPoint.X + Texture.Width - 5, newPoint.Y + Texture.Height / 2 + 2, 1,
            Texture.Height / 2 - 2);

        var feet = new Point(rect.Center.X, rect.Bottom);

        if (velocity == Vector2.Zero)
        {
            return true;
        }

        var feetOnGround = false;

        foreach (var rectangle in level.GroundLayer)
        {
            if (rectangle.Contains(feet))
            {
                feetOnGround = true;
            }
        }

        if (!feetOnGround) return false;

        foreach (var rectangle in level.GroundLayer)
        {
            if (velocity.Y == 0 && velocity.X > 0)
            {
                if (rectangle.Intersects(armHitBoxRight))
                {
                    return true;
                }
            }

            if (velocity.Y == 0 && velocity.X < 0)
            {
                if (rectangle.Intersects(armHitBoxLeft))
                {
                    return true;
                }
            }

            if ((velocity.X != 0 || !(velocity.Y > 0)) && (velocity.X != 0 || !(velocity.Y < 0))) continue;
            
            if (rectangle.Intersects(armHitBoxLeft) && rectangle.Intersects(armHitBoxRight))
            {
                return true;
            }
            
        }
        
        return false;
    }

    public override void Update(GameTime gameTime)
    {
        _input.Move();
        _input.Aim();
        AnimationManager.Update(gameTime);
        _weapon?.Update(gameTime);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Animations == null)
        {
            base.Draw(spriteBatch);
        }
        else
        {
            AnimationManager.Draw(spriteBatch, Position);
            _weapon?.Draw(spriteBatch);
        }
    }

    public override void Attack()
    {
        if (_weapon != null)
        {
            SetWeaponPosition();
        }

        switch (AimDirection)
        {
            case (int) Direction.Right:
                SetAnimation("AttackRight");
                break;
            case (int)Direction.Left:
                SetAnimation("AttackLeft");
                break;
            case (int)Direction.Up:
                SetAnimation("AttackUp");
                break;
            case (int)Direction.Down:
                SetAnimation("AttackDown");
                break;
        }
    }

    private void SetWeaponPosition()
    {
        switch (AimDirection)
        {
            case (int) Direction.Right:
                _weapon.Position = new Vector2(Position.X + SpriteWidth, Position.Y);
                _weapon.SetAnimation("AttackRight");
                break;
            case (int)Direction.Left:
                _weapon.Position = new Vector2(Position.X - SpriteWidth, Position.Y);
                _weapon.SetAnimation("AttackLeft");
                break;
            case (int)Direction.Up:
                _weapon.Position = new Vector2(Position.X, Position.Y - SpriteWidth);
                //_weapon.SetAnimation("AttackUp");
                break;
            case (int)Direction.Down:
                _weapon.Position = new Vector2(Position.X, Position.Y + SpriteWidth);
                //_weapon.SetAnimation("AttackDown");
                break;
        }
    }
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void AddItem(Item item)
    {
        Items ??= new List<Item>();
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        Items?.Remove(item);
    }

    public void UseItem(Item item)
    {
        item.Use();
    }
}