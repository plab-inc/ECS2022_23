using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Currency : Item
{
    public float Value { get; set; } = 10;
    
    public Currency(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}