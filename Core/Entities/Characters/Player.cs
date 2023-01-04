using System.Collections.Generic;
using System.Linq;
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
    public bool Invincible;
    public bool ImmuneToWater = false;
    
    public List<Item> Items;
    public Weapon Weapon { get; set; }
    public Trinket Trinket { get; set; }
    public Room Room { get; set; }
    public Player(Texture2D texture, Dictionary<AnimationType, Animation> animations) : base(Vector2.Zero,texture, animations)
    {
        Velocity = 3f;
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

        if (IsInWater(Rectangle))
        {
            if (!ImmuneToWater)
            {
                Kill();
            }
        }
        
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
            if (Invincible)
            {
                AnimationManager.StartColorChange();
            }
            else
            {
                AnimationManager.StopColorChange();
            }
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
            case Direction.Right:
                SetAnimation(AnimationType.AttackRight);
                break;
            case Direction.Left:
                SetAnimation(AnimationType.AttackLeft);
                break;
            case Direction.Up:
                SetAnimation(AnimationType.AttackUp);
                break;
            case Direction.Down:
                SetAnimation(AnimationType.AttackDown);
                break;
            case Direction.None:
                SetAnimation(AnimationType.AttackRight);
                break;
            default:
                SetAnimation(AnimationType.AttackRight);
                break;
        }

        IsAttacking = true;
    }
    public virtual void Moves(Vector2 direction)
    {
        var moveDirection = Helper.Transform.Vector2ToDirection(direction);
        
        switch (moveDirection)
        {
            case Direction.Right:
                SetAnimation(AnimationType.WalkRight);
                break;
            case Direction.Left:
                SetAnimation(AnimationType.WalkLeft);
                break;
            case Direction.Up:
                SetAnimation(AnimationType.WalkUp);
                break;
            case Direction.Down:
                SetAnimation(AnimationType.WalkDown);
                break;
            case Direction.None:
                SetAnimation(AnimationType.Default);
                break;
        }
        
        if (!Collides(direction * Velocity)) 
            return;

        Position += direction * Velocity;
        
    }
    public override bool IsInWater(Rectangle body)
    {
        return Level.WaterLayer.Any(rectangle => rectangle.Contains(body));
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

    public void Aims(Direction getAimDirection)
    {
        AimDirection = getAimDirection;
    }
}