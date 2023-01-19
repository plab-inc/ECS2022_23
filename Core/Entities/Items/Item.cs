using System;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

[Serializable]
public abstract class Item : Entity
{
    public ItemType ItemType;

    protected Item(Vector2 spawn, Texture2D texture, Rectangle sourceRect, ItemType itemType) : base(spawn, texture)
    {
        SourceRect = sourceRect;
        ItemType = itemType;
    }

    public Rectangle SourceRect { get; }

    public virtual bool Use(Player player)
    {
        return true;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRect, Color.White);
    }
}