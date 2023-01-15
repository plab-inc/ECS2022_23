using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Sound;
using ECS2022_23.Core.World;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Characters;

public class Player : Character
{
    private bool _invincible;
    private bool _shieldBreak;
    
    public float EP;
    public float Level;
    public float Armor;
    
    public bool ImmuneToWater = false;

    public DeathCause DeathCause;
    public List<Item> Items = new();
    public Weapon Weapon { get; set; }
    public Trinket Trinket { get; set; }
    public Room Room { get; set; }
    
    public bool Invincible
    {
        get => _invincible;
        set
        {
            _invincible = value;
            if (_invincible)
            {
                switch (_shieldBreak)
                {
                    case true: AnimationManager.StartColorChange(Color.White, Color.Aqua);
                        break;
                    case false: AnimationManager.StartColorChange(Color.White, new Color(236, 86, 113, 255));
                        break;
                }
            }
            else AnimationManager.StopColorChange();
        }
    }
    public Player(Texture2D texture, Dictionary<AnimationType, Animation> animations) : base(Vector2.Zero,texture, animations)
    {
        DamageSound = SoundLoader.PlayerDamageSound;
        
        Velocity = 3f;
        HP = 3;
        Armor = 2;
        Strength = 5;
        
        EP = 0;
        Level = 1;
    }
    public Player(Texture2D texture, Dictionary<AnimationType, Animation> animations, float ep, float level) : base(Vector2.Zero,texture, animations)
    {
        DamageSound = SoundLoader.PlayerDamageSound;
        
        Velocity = 3f;
        HP = 3;
        Armor = 2;
        Strength = 5;

        EP = ep;
        Level = level;
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
                DeathCause = DeathCause.Water;
                Kill();
            }
        }
        
        LevelUp();
        Weapon?.SetPosition(this);
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
    public void Moves(Vector2 direction)
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
    public override bool IsInWater(Rectangle movedBody)
    {
        return Stage.WaterLayer.Any(rectangle => rectangle.Contains(movedBody));
    }
    public override bool Collides(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();
        var rect = new Rectangle(newPoint, new Point(Texture.Width, Texture.Height));
        
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

        foreach (var rectangle in Stage.GroundLayer)
        {
            if (rectangle.Contains(feet))
            {
                feetOnGround = true;
            }
        }

        if (!feetOnGround) return false;

        foreach (var rectangle in Room.GetRectanglesRelativeToWorld("Interactables","Locker"))
        {
            if (rectangle.Contains(feet) || rectangle.Contains(armHitBoxLeft) || rectangle.Contains(armHitBoxRight))
            {
                return false;
            }
        }
        
        foreach (var rectangle in Stage.GroundLayer)
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
    
    public void TakesDamage(float damagePoints, Entity entity)
    {
        if (Invincible) return;

        _shieldBreak = Armor > 0;

        Armor -= damagePoints;

        if (_shieldBreak)
        {
            SoundManager.Play(SoundLoader.ShieldBreakSound);
        }

        if (Armor <= 0)
        {
            HP += Armor;
            Armor = 0;
            SetAnimation(AnimationType.Hurt);
            SoundManager.Play(DamageSound);
        }
           
        if (!IsAlive())
        {
            DeathCause = Helper.Transform.EntityToDeathCause(entity);
            SetAnimation(AnimationType.Death);
        }

        Invincible = true;
    }

    private void LevelUp()
    {
        if (25 <= EP)
        {
            EP -= 25;
            Strength += 1+Level*0.25f;
            Level++;
            SoundManager.Play(SoundLoader.LevelUpSound);
        }
    }

    public void Aims(Direction aimDirection)
    {
        AimDirection = aimDirection;
    }

}