using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Consumable : Item
{
    public float HealPoints { get; set; } = 2;
    public float XpPoints { get; set; } = 1;
    public float DamageMultiplier { get; set; } = 1f;
    public float Duration { get; set; } = 10f;
    public Consumable(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Use(Player player)
    {
        player.HP += HealPoints;
        if (player.HP > player.MaxHP)
        {
            player.HP = player.MaxHP;
        }
        player.XpToNextLevel += XpPoints;
    }

}