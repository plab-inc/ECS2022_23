using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.World;

public class Door
{
    public Room Room;

    public Direction Direction;
    public Point Marker;

    private int _x;
    private int _y;
    
    public Door(Room room, Point marker, Direction direction, int x, int y)
    {
        Room = room;
        Marker = marker;
        Direction = direction;
        _x = x;
        _y = y;
    }

    public Point Position => new(_x, _y);
    public Rectangle Rectangle => new(_x, _y, 16, 16);
    public Point NormalizedPosition => new(_x - Room.Position.X, _y - Room.Position.Y);
}