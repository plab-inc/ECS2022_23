using System.Collections.Generic;
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
    public List<Item> Items;
    private Weapon _weapon;
    private Input _input;
    
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
            _weapon.Position = new Vector2(Position.X+SpriteWidth, Position.Y);
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