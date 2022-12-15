using System;
using System.Collections.Generic;
using System.Linq;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory;

public class InventoryRow
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    private Rectangle DestinationRec;
    private int slotCount;
    private Texture2D _texture;
    private int _slotSize;
    private int _scale;
    public InventoryRow(Rectangle destRec, int slotCount, Texture2D texture, int slotSize, int scale)
    {
        DestinationRec = destRec;
        this.slotCount = slotCount;
        _texture = texture;
        _slotSize = slotSize;
        _scale = scale;
        CreateSlots();
    }

    public void CreateSlots()
    {
        for (var i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot(new Rectangle(DestinationRec.X+i*_slotSize, DestinationRec.Y, _slotSize, _slotSize), 
                UiLoader.CreateColorTexture(Color.Cyan), _scale));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var slot in slots)
        {
            slot.Draw(spriteBatch);
        }
    }

    public InventorySlot GetFreeSlot()
    {
        return slots.Find(slot => slot.IsUsed == false);
    }

    public InventorySlot FindItem(Item item)
    {
        try
        {
            return slots.Where(slot => slot.IsUsed).FirstOrDefault(slot => slot.item.Equals(item));
        }
        catch (NullReferenceException e)
        {
            return null;
        } 
    }
}