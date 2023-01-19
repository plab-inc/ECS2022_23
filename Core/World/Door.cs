using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.World;

public class Door
{
    private readonly int _x;
    private readonly int _y;

    public Direction Direction;
    public Room Room;

    public Door(Room room, Direction direction, int x, int y)
    {
        Room = room;
        Direction = direction;
        _x = x;
        _y = y;
    }

    public Point Position => new(_x, _y);
}