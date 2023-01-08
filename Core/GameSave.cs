using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

namespace ECS2022_23.Core;

public class GameSave
{
    public float EP{ get; set; }
    public float Level { get; set; }
    public List<Item> ItemsInLocker { get; set; }

    public GameSave(float ep, float level)
    {
        EP = ep;
        Level = level;
        ItemsInLocker = new List<Item>();
    }

    public void Update(float ep, float level)
    {
        this.EP = ep;
        this.Level = level;
    }
}