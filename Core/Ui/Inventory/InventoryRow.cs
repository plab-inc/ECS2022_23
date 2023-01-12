using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

[Serializable]
public class InventoryRow
{
    private Inventory _inventory;
    public List<InventorySlot> Slots = new List<InventorySlot>();
    private Rectangle _destinationRec;
    private int _slotCount;
    private int _slotSize;
    private int _scale;
    public InventoryRow(Rectangle destRec, int slotCount, int slotSize, Inventory inventory)
    {
        _destinationRec = destRec;
        _slotCount = slotCount;
        _slotSize = slotSize;
        _scale = inventory.Scale;
        _inventory = inventory;
        CreateSlots();
    }

    private void CreateSlots()
    {
        for (var i = 0; i < _slotCount; i++)
        {
            Slots.Add(new InventorySlot(new Rectangle(_destinationRec.X+i*_slotSize, _destinationRec.Y, _slotSize, _slotSize), _scale, _inventory.SlotCount++));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var slot in Slots)
        {
            slot.Draw(spriteBatch);
        }
    }

    public InventorySlot GetFreeSlot()
    {
        return Slots.Find(slot => slot.IsUsed == false);
    }

    public InventorySlot FindSlotWithItem(Item item)
    {
        foreach (var slot in Slots)
        {
            if(slot.Item == null) continue;
            var itemInSlot = slot.Item;
            if (itemInSlot.itemType == item.itemType || itemInSlot.Equals(item))
            {
                return slot;
            }
        }

        return null;
    }
}