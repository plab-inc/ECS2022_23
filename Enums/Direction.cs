namespace ECS2022_23.Enums;

public enum Direction
{
    None = 0,
    Up = 1,
    Down = -1,
    Left = 4,
    Right = -4,
    UpLeft = Up + Left,
    UpRight = Up + Right,
    DownLeft = Down + Left,
    DownRight = Down + Right
}