using System.Linq;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class TrinketSlot : Inventory
{
    public TrinketSlot() : base(1,1)
    {
        Scale = 4;
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(0+PixelSize+Width+16, Game1.ScreenHeight-Height, Width, Height);
        CreateRows();
    }
    
    public override bool AddItem(Item item)
    {
        if (item == null) return false;
        var row = InventoryRows.First();
        var slot = row.Slots.First();
        slot.AddItem(item, 1);

        return true;
    }
}