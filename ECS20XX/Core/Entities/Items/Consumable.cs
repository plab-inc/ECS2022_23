﻿using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Consumable : Item
{
    public Consumable(Vector2 spawn, Texture2D texture, Rectangle sourceRect, ItemType itemType) : base(spawn, texture,
        sourceRect, itemType)
    {
    }

    public float HealMultiplier { get; set; }
    public float EpReward { get; set; }
    public float ArmorPoints { get; set; }

    public override bool Use(Player player)
    {
        var maxHp = player.MaxHP;
        player.HP += HealMultiplier * maxHp;
        if (player.HP > maxHp) player.HP = maxHp;
        player.EP += EpReward;
        player.Armor += ArmorPoints;
        return true;
    }
}