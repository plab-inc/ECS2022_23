using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory;

[Serializable]
public class InventoryRow
{
    private Rectangle _destinationRec;
    private InventoryTypes.Inventory _inventory;
    private int _scale;
    private int _slotCount;
    private int _slotSize;
    public List<InventorySlot> Slots = new();

    public InventoryRow(Rectangle destRec, int slotCount, int slotSize, InventoryTypes.Inventory inventory)
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
            Slots.Add(new InventorySlot(
                new Rectangle(_destinationRec.X + i * _slotSize, _destinationRec.Y, _slotSize, _slotSize), _scale,
                _inventory.SlotCount++));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var slot in Slots) slot.Draw(spriteBatch);
    }

    public InventorySlot GetFreeSlot()
    {
        return Slots.Find(slot => slot.IsUsed == false);
    }

    public InventorySlot FindSlotWithItem(Item item)
    {
        foreach (var slot in Slots)
        {
            if (slot.Item == null) continue;
            var itemInSlot = slot.Item;
            if (itemInSlot.ItemType == item.ItemType || itemInSlot.ItemType == item.ItemType) return slot;
        }

        return null;
    }
}