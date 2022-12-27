using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public class Key : Item
{
    public Key(Vector2 spawn, Texture2D texture, Rectangle sourceRect) : base(spawn, texture, sourceRect)
    {
    }

    public override void Update(GameTime gameTime)
    {
        
    }
    
    public override bool Use(Player player)
    {
        Debug.WriteLine("Tried to use key!");
        return false;
    }
    
    
}