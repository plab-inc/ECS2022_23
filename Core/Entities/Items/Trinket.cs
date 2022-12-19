using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Trinket : Item
{
    public float DamageMultiplier { get; set; }
    public float XpMultiplier { get; set; }
    public float ArmorMultiplier { get; set; }
    public Trinket(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
    
    public override void Use(Player player)
    {
        player.Strength *= DamageMultiplier;
        player.XpToNextLevel *= XpMultiplier;
        player.Armor *= ArmorMultiplier;
        player.Trinket = this;
        player.ImmuneToWater = true;
    }
}