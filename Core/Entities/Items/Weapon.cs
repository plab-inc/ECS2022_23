using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Loader;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Weapon : Item
{
    public readonly SoundEffect AttackSound;
    public readonly float DamagePoints;
    public readonly WeaponType WeaponType = WeaponType.Close;
    public Direction AimDirection;

    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations,
        Rectangle sourceRect, ItemType itemType, float damagePoints) : base(spawn, texture, sourceRect, itemType)
    {
        DamagePoints = damagePoints;
        Animations = animations;
    }

    public Weapon(Vector2 spawn, Texture2D texture, Dictionary<AnimationType, Animation> animations,
        Rectangle sourceRect, ItemType itemType, float damagePoints, WeaponType type) : base(spawn, texture, sourceRect,
        itemType)
    {
        DamagePoints = damagePoints;
        Animations = animations;
        WeaponType = type;
        AttackSound = SoundLoader.LaserSound;
    }

    public override void Update(GameTime gameTime)
    {
        AnimationManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        AnimationManager.Draw(spriteBatch, Position);
    }

    public void DrawIcon(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public void SetAnimationDirection(Direction direction)
    {
        AimDirection = direction;
        switch (direction)
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
            case (int) Direction.None:
                SetAnimation(AnimationType.AttackRight);
                break;
            default:
                SetAnimation(AnimationType.AttackRight);
                break;
        }
    }

    public void SetPosition(Player player)
    {
        var direction = player.AimDirection;
        var playerPos = player.Position;
        var width = player.Rectangle.Width;
        if (!AnimationManager.AnimationFinished) direction = AimDirection;
        switch (direction)
        {
            case Direction.Right:
                Position = new Vector2(playerPos.X + width, playerPos.Y);
                break;
            case Direction.Left:
                Position = new Vector2(playerPos.X - width, playerPos.Y);
                break;
            case Direction.Up:
                Position = new Vector2(playerPos.X, playerPos.Y - width);
                break;
            case Direction.Down:
                Position = new Vector2(playerPos.X, playerPos.Y + width);
                break;
            case Direction.None:
                Position = new Vector2(playerPos.X + width, playerPos.Y);
                break;
            default:
                Position = new Vector2(playerPos.X + width, playerPos.Y);
                break;
        }
    }
}