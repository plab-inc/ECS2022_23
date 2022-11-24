using System.Collections.Generic;
using System.Runtime.InteropServices;
using ECS2022_23.Core.animations;
using ECS2022_23.Core.entities.items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core.entities.characters;

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

    public Player(Texture2D texture) : base(texture)
    {
        Velocity = 0.5f;
        _input = new Input(this);
        HP = 10;
        SpriteWidth = 16;
    }

    public Player(Texture2D texture, Dictionary<string, Animation> animations) : base(texture, animations)
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

    private bool Collides(Vector2 velocity)
    {
        var newPoint = (Position + velocity).ToPoint();
        var rect = new Rectangle(newPoint, new Point(_texture.Width, _texture.Height));

        //TODO clean this mess up
        
        var armHitBoxLeft =
            new Rectangle(newPoint.X + 4, newPoint.Y + _texture.Height / 2 + 2, 1, _texture.Height / 2 - 2);
        var armHitBoxRight = new Rectangle(newPoint.X + _texture.Width - 5, newPoint.Y + _texture.Height / 2 + 2, 1,
            _texture.Height / 2 - 2);

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
            _weapon.Position = new Vector2(Position.X + SpriteWidth, Position.Y);
            _weapon.SetAnimation("Attack");
        }

        SetAnimation("AttackRight");
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