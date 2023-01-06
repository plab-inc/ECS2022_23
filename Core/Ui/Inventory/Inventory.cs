using System.Collections.Generic;
using System.Diagnostics;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public abstract class Inventory
{
    public int Scale = 5;
    public int SlotCount = 0;
    
    protected int PixelSize = 16;
    protected int Width;
    protected int Height;
    protected int RowCount;
    protected int ColCount;
    protected Rectangle DestinationRec;
    protected List<InventoryRow> InventoryRows = new List<InventoryRow>();
    
    private Texture2D _backgroundTexture;
    private int _prevIndex = 0;
    private InventoryRow _prevRow;
  

    protected Inventory(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        _backgroundTexture = UiLoader.CreateColorTexture(new Color(28, 111, 255, 255));
    }

    protected void CreateRows()
    {
        for (var i = 0; i < RowCount; i++)
        {
               InventoryRows.Add(new InventoryRow(new Rectangle(DestinationRec.X,DestinationRec.Y+i*Height/RowCount, Width, 
                   Height/RowCount), ColCount, PixelSize*Scale, this));
        }

        if (InventoryRows.Count > 0)
        {
            _prevRow = InventoryRows[0];
            if(_prevRow.Slots.Count > 0) _prevRow.Slots[_prevIndex].IsSelected = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, DestinationRec, Color.White);
        foreach (var row in InventoryRows)
        {
            row.Draw(spriteBatch);
        }
    }
    
    public virtual void AddItem(Item item)
    {
        if (item == null) return;
        
        foreach (var row in InventoryRows)
        {
            var slot = row.FindItem(item);
           
            if (slot != null)
            {
                slot.ItemCount++;
                return;
            }

            slot = row.GetFreeSlot();
            if (slot == null) continue;
            slot.AddItem(item, 1);
            return;
        }
    }

    public void SelectIndex(int index)
    {
        if (index < 0)
        {
            index = 0;
        }

        if (index == _prevIndex) return;
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Index == index);
            if (slot == null) continue;
            slot.IsSelected = true;
            var prevSlot = _prevRow.Slots[_prevIndex%ColCount];
            prevSlot.IsSelected = false;
            _prevIndex = index;
            _prevRow = row;
            return;
        }
    }
    public void IncreaseIndex()
    {
        SelectIndex(_prevIndex+1);
    }
    public void DecreaseIndex()
    {
        SelectIndex(_prevIndex-1);
    }

    public Item GetSelectedItem()
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.IsSelected == true);
            if (slot == null) continue;
            return slot.Item;
        }

        return null;
    }

    public void RemoveItem(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindItem(item);
            
            if (slot == null) continue;
            slot.ItemCount--;
            if (slot.ItemCount <= 0)
            { 
                slot.RemoveItem();
            }

            return;
        }
    }

    public void SwitchActiveState(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindItem(item);
            if (slot == null) continue;
            slot.IsActive = !slot.IsActive;
            return;
        }
    }

    public bool IsItemActive(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindItem(item);
            if (slot == null) continue;
            return slot.IsActive;
        }

        return false;
    }
    
    public bool LastIndex()
    {
        return _prevIndex+1 == SlotCount;
    }
    
    public bool FirstIndex()
    {
        return _prevIndex == 0;
    }
    
    
}