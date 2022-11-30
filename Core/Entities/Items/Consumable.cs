using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Consumable : Item
{
    public float HealPoints { get; set; } = 10;
    public float XpPoints { get; set; } = 5;
    public float DamageMultiplier { get; set; } = 1f;
    public float Duration { get; set; } = 10f;
    public Consumable(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}