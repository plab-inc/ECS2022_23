using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public abstract class Item : Entity
{
    public Rectangle SourceRect { get; }

    protected Item(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture)
    {
        SourceRect = sourceRect;
    }

    public virtual void Use()
    {

    }

    public virtual void Use(Player player)
    {

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRect, Color.White);
    }

    public void DrawIcon(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, SourceRect, Color.White);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType().BaseType != typeof(Item))
        {
            return false;
        }

        var toCompare = (Item)obj;
        return Equals(toCompare) && toCompare.Texture == this.Texture && Position == toCompare.Position;
    }

    private bool Equals(Item other)
    {
        return SourceRect.Equals(other.SourceRect);
    }

    public override int GetHashCode()
    {
        return SourceRect.GetHashCode();
    }
}