using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Combat;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Sound;
using ECS2022_23.Core.Ui.InventoryManagement;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public class Player : Character
{
    public float Armor;
    public float XpToNextLevel;
    public float Money;

    private float speed = 3;
    public List<Item> Items;
    public Weapon Weapon { get; set; }
    public Trinket Trinket { get; set; }
    public Room Room { get; set; }
    
    private Input _input;
    public bool ImmuneToWater = false;
    
        
    public Player(Vector2 spawn, Texture2D texture, Dictionary<string, Animation> animations) : base(spawn, texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
        Strength = 5;
    }
    public Player(Texture2D texture, Dictionary<string, Animation> animations) : base(Vector2.Zero,texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
        Strength = 5;
    }
    
    public override void Update(GameTime gameTime)
    {
        if (AnimationManager.AnimationFinished)
        {
            _input.Move();
            _input.Aim();
        }
        AnimationManager.Update(gameTime);
        Weapon?.Update(gameTime);
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
            Weapon?.Draw(spriteBatch);
        }
    }

    public override void Attack()
    {
        if (Weapon != null)
        {
            SetWeaponPosition();
            if (Weapon.WeaponType == WeaponType.RANGE)
            {
                CombatManager.Shoot(this);
                SoundManager.Play(Weapon.AttackSound);
            }
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
            case (int)Direction.None:
                SetAnimation("AttackRight");
                break;
            default:
                SetAnimation("AttackRight");
                break;
        }

        IsAttacking = true;
    }
    public override bool IsInWater(Rectangle body)
    {
        foreach (var rectangle in Level.WaterLayer)
        {
            if (rectangle.Contains(body))
            {
                return true;
            }
        }
        return false;
    }
    public override bool Collides(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();
        var rect = new Rectangle(newPoint, new Point(Texture.Width, Texture.Height));

        //TODO clean up
        
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

        foreach (var rectangle in Level.GroundLayer)
        {
            if (rectangle.Contains(feet))
            {
                feetOnGround = true;
            }
        }

        if (!feetOnGround) return false;

        foreach (var rectangle in Level.GroundLayer)
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
    private void SetWeaponPosition()
    {
        switch (AimDirection)
        {
            case (int) Direction.Right:
                Weapon.Position = new Vector2(Position.X + SpriteWidth, Position.Y);
                Weapon.SetAnimation("AttackRight");
                break;
            case (int)Direction.Left:
                Weapon.Position = new Vector2(Position.X - SpriteWidth, Position.Y);
                Weapon.SetAnimation("AttackLeft");
                break;
            case (int)Direction.Up:
                Weapon.Position = new Vector2(Position.X, Position.Y - SpriteWidth);
                //_weapon.SetAnimation("AttackUp");
                break;
            case (int)Direction.Down:
                Weapon.Position = new Vector2(Position.X, Position.Y + SpriteWidth);
                //_weapon.SetAnimation("AttackDown");
                break;
            case (int) Direction.None:
                Weapon.Position = new Vector2(Position.X + SpriteWidth, Position.Y);
                Weapon.SetAnimation("AttackRight");
                break;
            default:
                Weapon.Position = new Vector2(Position.X + SpriteWidth, Position.Y);
                Weapon.SetAnimation("AttackRight");
                break;
        }
    }
    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
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
        if (Items.Count <= 0) return;
        if (item.GetType() == typeof(Trinket))
        {
            item.Use(this);
        }
        else
        {
            if (!Items.Remove(item)) return;
            item.Use(this);
        }
    }
}