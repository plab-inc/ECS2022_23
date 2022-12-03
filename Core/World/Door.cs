using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.World;

public class Door
{
    public Room Room;

    public Direction Direction;
    
    private int _x;
    private int _y;
    
    public Door(Room room, Direction direction, int x, int y)
    {
        Room = room;
        Direction = direction;
        _x = x;
        _y = y;
    }

    public Point Position => new(_x, _y);

}