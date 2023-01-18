using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
using ECS2022_23.Enums;

namespace ECS2022_23.Core;

public class GameSave
{
    public float EP{ get; set; }
    public float Level { get; set; }
    public List<ItemType> ItemsInLocker { get; set; }

    public GameSave(float ep, float level)
    {
        EP = ep;
        Level = level;
        ItemsInLocker = new List<ItemType>();
    }

    public void Update(float ep, float level)
    {
        this.EP = ep;
        this.Level = level;
    }
}