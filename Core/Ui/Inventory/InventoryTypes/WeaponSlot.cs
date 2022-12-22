using System;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class WeaponSlot : Inventory
{
    public WeaponSlot() : base(1, 1)
    { 
        Scale = 4;
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(0+PixelSize, Game1.ScreenHeight-Height, Width, Height);
        CreateRows();
    }

    public override void AddItem(Item item)
    {
        try
        {
            if (item == null) return;
            var row = InventoryRows[0];
            var slot = row.Slots[0];
            slot.AddItem(item, 1);
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException ||
                                   ex is NullReferenceException) 
        {
            ColCount = 1;
            RowCount = 1;
            CreateRows();
        }
     
    }
}