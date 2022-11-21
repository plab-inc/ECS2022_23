using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using TiledCS;

namespace MonoGameLevelGenerator.Core;

public class Door
{
    public Room room;
    
    public Direction Direction;
    public Point marker;
    public int x;
    public int y;


    public Door(Room room, Point marker, Direction direction, int x, int y)
    {
        this.room = room;
        this.marker = marker;
        this.Direction = direction;
        this.x = x;
        this.y = y;
    }

    public Point ToPoint()
    {
        return new Point(x, y);
    }

    public Rectangle ToRectangle()
    {
        return new Rectangle(x, y, 16, 16);
    }
}