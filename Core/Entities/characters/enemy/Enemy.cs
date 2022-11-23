using System.Collections.Generic;
using ECS2022_23.Core.animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.entities.characters.enemy;

public  class Enemy : Character
{
    public float XpReward;
    public float MoneyReward;
    private Motor _motor;
    public bool IsActive=true;

    public Enemy(Texture2D texture, Motor motor) : base(texture)
    {
        Velocity = 2f;
        HP = 10;
        SpriteWidth = 16;
        _motor = motor;
    }

    public Enemy(Texture2D texture, Dictionary<string, Animation> animations, Motor motor) : base(texture, animations)
    {
        Velocity = 3f;
        HP = 10;
        SpriteWidth = 16;
        _motor = motor;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
           Position = _motor.Move(Position, (int) Velocity);
        }
    }
}