using System.Collections.Generic;
using ECS2022_23.Core.animations;
using ECS2022_23.Core.entities.items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ECS2022_23.Core.entities.characters;

public class Player : Character
{
    private Weapon _weapon;
    public Player(Texture2D texture) : base(texture)
    {
        Velocity = 0.5f;
        HP = 10;
        pixelWidth = 16;
    }
    
    public Player(Texture2D texture, Dictionary<string, Animation> animations) : base(texture, animations)
    {
        Velocity = 0.5f;
        HP = 10;
        pixelWidth = 16;
    }

    public override void Move()
    {
        var velocity = new Vector2();
        var speed = 3f;
        var animation = "Default";

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            velocity.Y = -speed;
            animation = "WalkUp";
        } else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            velocity.Y = speed;
            animation = "WalkDown";
        } else if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            velocity.X = -speed;
            animation = "WalkLeft";
        } else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            velocity.X = speed;
            animation = "WalkRight";
        } else if (Keyboard.GetState().IsKeyDown(Keys.X))
        {
            Attack();
        } 
        
        bool canMove = true;
        
        if (canMove)
        {
            Position += velocity;
            SetAnimation(animation);
        }
    }

    public override void Update(GameTime gameTime)
    {
        Move();
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
            _weapon.Position = new Vector2(Position.X+pixelWidth, Position.Y);
            _weapon.SetAnimation("Attack");
        }
      
        SetAnimation("AttackRight");
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
}