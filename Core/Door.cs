using ECS2022_23.Enums;

namespace MonoGameLevelGenerator.Core;

public class Door
{
    public Room room;
    
    public Direction Direction;
    public int x;
    public int y;


    public Door(Room room, Direction direction, int x, int y)
    {
        this.room = room;
        Direction = direction;
        this.x = x;
        this.y = y;
    }
}