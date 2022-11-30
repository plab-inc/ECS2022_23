using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Trinket : Item
{
    public float DamageMultiplier { get; set; } = 2f;
    public float XpMultiplier { get; set; } = 2f;
    public float ArmorMultiplier { get; set; } = 2f;
    public Trinket(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}