using System.Collections.Generic;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Manager;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class UmlDiagram : Item
{
    public UmlDiagram(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect,
        ItemType.UmlDiagram)
    {
    }

    public override bool Use(Player player)
    {
        var killSphere = new BoundingSphere(new Vector3(player.Position.X, player.Position.Y, 0), 150f);
        var targets = new List<Enemy>();

        foreach (var enemy in EnemyManager.Enemies)
        {
            var pos = new Vector3(enemy.Position.X, enemy.Position.Y, 0);

            if (killSphere.Contains(pos) == ContainmentType.Contains ||
                killSphere.Contains(pos) == ContainmentType.Intersects) targets.Add(enemy);
        }

        foreach (var enemy in targets)
        {
            enemy.HP = 0;
            CombatManager.EnemyDies(enemy, player);
        }

        return true;
    }
}