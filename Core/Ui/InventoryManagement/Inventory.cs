using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public class Inventory
{
    private const int PixelSize = 16;
    public int Scale;
    private int _width;
    private int _height;
    private int _rowCount;
    private int _colCount;
    private Rectangle _destinationRec;
    private List<InventoryRow> _inventoryRows = new List<InventoryRow>();
    private Texture2D _backgroundTexture;
    private int _prevIndex = 0;
    private InventoryRow _prevRow;
    public int SlotCount = 0;
    public Inventory(int rowCount, int colCount)
    {
        _rowCount = rowCount;
        _colCount = colCount;
        Scale = 5;
        _width = PixelSize * _rowCount * Scale;
        _height = PixelSize * _colCount * Scale;
        _destinationRec = new Rectangle(Game1.ScreenWidth/2-_width/2, Game1.ScreenHeight/2-_height/2, _width, _height);
        _backgroundTexture = UiLoader.CreateColorTexture(Color.DarkGray);
        CreateRows();
    }

    private void CreateRows()
    {
        for (var i = 0; i < _rowCount; i++)
        {
               _inventoryRows.Add(new InventoryRow(new Rectangle(_destinationRec.X,_destinationRec.Y+i*_height/_colCount, _width, 
                   _height/_rowCount), _colCount, PixelSize*Scale, this));
        }

        if (_inventoryRows.Count > 0)
        {
            _prevRow = _inventoryRows[0];
            if(_prevRow.Slots.Count > 0) _prevRow.Slots[_prevIndex].Selected = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, _destinationRec, Color.White);
        foreach (var row in _inventoryRows)
        {
            row.Draw(spriteBatch);
        }
    }
    
    public void AddItem(Item item)
    {
        if (item == null) return;
        
        foreach (var row in _inventoryRows)
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
        foreach (var row in _inventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Index == index);
            if (slot == null) continue;
            slot.Selected = true;
            var prevSlot = _prevRow.Slots[_prevIndex%_colCount];
            prevSlot.Selected = false;
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
        foreach (var row in _inventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Selected == true);
            if (slot == null) continue;
            return slot.Item;
        }

        return null;
    }

    public void RemoveItem(Item item)
    {
        foreach (var row in _inventoryRows)
        {
            foreach (var slot in row.Slots)
            {
                if (slot.Item == null) continue;
                if (!slot.Item.Equals(item)) continue;
                slot.ItemCount--;
                if (slot.ItemCount <= 0)
                {
                    slot.RemoveItem();
                }
            }
        }
    }

}