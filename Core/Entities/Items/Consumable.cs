using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Consumable : Item
{
    public float HealMultiplier { get; set; }
    public float XpPoints { get; set; }
    public float ArmorPoints { get; set; }
    public float DamageMultiplier { get; set; }
    public float Duration { get; set; }
    public Consumable(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public override bool Use(Player player)
    {
        var maxHp = player.MaxHP;
        player.HP += HealMultiplier*maxHp;
        if (player.HP > maxHp)
        {
            player.HP = maxHp;
        }
        player.Ep += XpPoints;
        player.Armor += ArmorPoints;
        return true;
    }

}