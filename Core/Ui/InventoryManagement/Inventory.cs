using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public class Inventory
{
    private const int PixelSize = 16;
    private int _scale;
    private int _width;
    private int _height;
    private int _rowCount;
    private int _colCount;
    private Rectangle _destinationRec;
    private List<InventoryRow> _inventoryRows = new List<InventoryRow>();
    private Texture2D _backgroundTexture;
    private int _prevIndex = 0;
    private InventoryRow _prevRow;
    public Inventory(int rowCount, int colCount)
    {
        _rowCount = rowCount;
        _colCount = colCount;
        _scale = 5;
        _width = PixelSize * _rowCount * _scale;
        _height = PixelSize * _colCount * _scale;
        _destinationRec = new Rectangle(Game1.ScreenWidth/2-_width/2, Game1.ScreenHeight/2-_height/2, _width, _height);
        _backgroundTexture = UiLoader.CreateColorTexture(Color.DarkGray);
        CreateRows();
    }

    private void CreateRows()
    {
        for (var i = 0; i < _rowCount; i++)
        {
               _inventoryRows.Add(new InventoryRow(new Rectangle(_destinationRec.X,_destinationRec.Y+i*_height/_colCount, _width, 
                   _height/_rowCount), 
                   _colCount, UiLoader.CreateColorTexture(Color.Lavender), PixelSize*_scale, _scale));
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
                slot.Count++;
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
            if (row.Slots.Count <= index) continue;
            if (row.Slots[index] == null) continue;
            row.Slots[index].Selected = true;
            var prevSlot = _prevRow.Slots[_prevIndex];
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

}