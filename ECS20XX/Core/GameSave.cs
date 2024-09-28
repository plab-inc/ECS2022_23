using System.Collections.Generic;
using ECS2022_23.Enums;

namespace ECS2022_23.Core;

public class GameSave
{
    public GameSave(float ep, float level)
    {
        EP = ep;
        Level = level;
        ItemsInLocker = new List<ItemType>();
    }

    public float EP { get; set; }
    public float Level { get; set; }
    public List<ItemType> ItemsInLocker { get; set; }

    public void Update(float ep, float level)
    {
        EP = ep;
        Level = level;
    }
}