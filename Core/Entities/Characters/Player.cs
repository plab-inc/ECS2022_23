using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Sound;
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
    public List<Item> Items;
    public Weapon Weapon { get; set; }
    public Trinket Trinket { get; set; }
    public Room Room { get; set; }
    public bool ImmuneToWater = false;
    public bool Invincible = false;
    private Input _input;

    public Player(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations) : base(spawn, texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
        Strength = 5;
    }
    public Player(Texture2D texture, Dictionary<AnimationType, Animation> animations) : base(Vector2.Zero,texture, animations)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
        Strength = 5;
    }
    
    public override void Update(GameTime gameTime)
    {
        if (IsAttacking && AnimationManager.AnimationFinished)
        {
            IsAttacking = false;
        }
        _input.Move(); 
        _input.Aim();
        Weapon.SetPosition(this);
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
        if(IsAttacking) return;
        if (Weapon != null)
        {
            Weapon.SetAnimationDirection(AimDirection);
            if (Weapon.WeaponType == WeaponType.Range)
            {
                CombatManager.Shoot(this);
                SoundManager.Play(Weapon.AttackSound);
            }
        }

        switch (AimDirection)
        {
            case (int) Direction.Right:
                SetAnimation(AnimationType.AttackRight);
                break;
            case (int)Direction.Left:
                SetAnimation(AnimationType.AttackLeft);
                break;
            case (int)Direction.Up:
                SetAnimation(AnimationType.AttackUp);
                break;
            case (int)Direction.Down:
                SetAnimation(AnimationType.AttackDown);
                break;
            case (int)Direction.None:
                SetAnimation(AnimationType.AttackRight);
                break;
            default:
                SetAnimation(AnimationType.AttackRight);
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

    public void AddItem(Item item)
    {
        Items ??= new List<Item>();
        Items.Add(item);
    }

    public bool UseItem(Item item)
    {
        if (Items.Count <= 0) return false;
        if (item.GetType() == typeof(Trinket))
        {
            return item.Use(this);
        } 
        if (item.Use(this))
        {
            Items.Remove(item);
            return true;
        }

        return false;
    }
    
    public void TakesDamage(float damagePoints)
    {
        if (Invincible) return;
        
        Armor -= damagePoints;
        if (Armor < 0)
        {
            HP += Armor;
            Armor = 0;
            Invincible = true;
            SetAnimation(AnimationType.Hurt);
        }
           
        if (!IsAlive())
        {
            Invincible = true;
            SetAnimation(AnimationType.Death);
        }
    }
}