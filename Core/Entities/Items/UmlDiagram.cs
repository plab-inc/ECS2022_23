using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Manager;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class UmlDiagram : Item
{
    public UmlDiagram(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect, ItemType.UmlDiagram)
    {
    }

    public override bool Use(Player player)
    {
        var killSphere =  new Rectangle(player.Position.ToPoint(),new Point(150,150));

        
        foreach (var enemy in EnemyManager.Enemies)
        {
            if (killSphere.Intersects(enemy.Rectangle))
            {
                //TODO Kill all enemies in rectangle
            }
        }

        return true;
    }
}