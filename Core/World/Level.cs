using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.World;
public class Level
{
    public List<Room> Rooms;
    public List<Rectangle> GroundLayer;
    
    public Level(List<Room> rooms, List<Rectangle> groundLayer)
    {
        Rooms = rooms;
        GroundLayer = groundLayer;
    }
    
    public Room StartRoom
    {
        get
        {
            return Rooms.First(x => x.RoomMapName.Contains("start"));
        }
    }
    
    public Rectangle Background {
        
        get
        {
            var background = new Rectangle();

            foreach (var rectangle in GroundLayer)
            {
                background = Rectangle.Union(rectangle, background);
            }
            background.Inflate(50,50);

            return background;
        }
    }
    

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var room in Rooms)
        {
            room.Draw(spriteBatch);
        }
    }
}