using System;
using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public class InventoryRow
{
    private Inventory _inventory;
    public List<InventorySlot> Slots = new List<InventorySlot>();
    private Rectangle _destinationRec;
    private int _slotCount;
    private Texture2D _texture;
    private int _slotSize;
    private int _scale;
    public InventoryRow(Rectangle destRec, int slotCount, int slotSize, Inventory inventory)
    {
        _destinationRec = destRec;
        this._slotCount = slotCount;
        _texture = UiLoader.CreateColorTexture(Color.Pink);
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

    public InventorySlot FindItem(Item item)
    {
        try
        {
            return Slots.Where(slot => slot.IsUsed).FirstOrDefault(slot => slot.Item.Equals(item));
        }
        catch (NullReferenceException e)
        {
            return null;
        } 
    }
}