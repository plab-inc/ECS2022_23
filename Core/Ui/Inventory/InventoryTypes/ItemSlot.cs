using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Ui.Inventory.InventoryTypes;

public class ItemSlot : Inventory
{
    public ItemSlot(InventoryType type) : base(1, 1)
    {
        Scale = 4;
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        if (type == InventoryType.TrinketSlot)
            DestinationRec = new Rectangle(0 + PixelSize + Width + 16, Game1.ScreenHeight - Height, Width, Height);
        else if (type == InventoryType.WeaponSlot)
            DestinationRec = new Rectangle(0 + PixelSize, Game1.ScreenHeight - Height, Width, Height);
        CreateRows();
    }

    public override bool AddItem(Item item)
    {
        if (item == null) return false;
        var row = InventoryRows[0];
        var slot = row.Slots[0];
        slot.AddItem(item, 1);
        return true;
    }
}