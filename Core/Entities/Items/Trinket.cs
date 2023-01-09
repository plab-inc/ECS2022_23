using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Trinket : Item
{
    public Trinket(Vector2 spawn, Texture2D texture, Rectangle sourceRect, ItemType itemType) : base(spawn, texture, sourceRect, itemType)
    {
    }

    public override void Update(GameTime gameTime)
    {
        return;
    }
    
    public override bool Use(Player player)
    {
        player.Trinket = this;
        player.ImmuneToWater = true;
        return true;
    }

    public void Unequip(Player player)
    {
        player.Trinket = null;
        player.ImmuneToWater = false;
    }
}