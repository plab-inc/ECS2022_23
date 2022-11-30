using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public abstract class Item : Entity
{
    public Rectangle SourceRect { get; }
    protected Item(Vector2 spawn,Texture2D texture, Rectangle sourceRect) : base(spawn, texture)
    {
        SourceRect = sourceRect;
    }

    public virtual void Use()
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
}