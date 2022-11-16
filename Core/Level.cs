using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core;
public class Level
{
    public List<Room> Rooms;
    public List<Rectangle> CollisionLayer;

    public Level(List<Room> rooms, List<Rectangle> collisionLayer)
    {
        Rooms = rooms;
        CollisionLayer = collisionLayer;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var room in Rooms)
        {
            room.Draw(spriteBatch);
        }
    }
}