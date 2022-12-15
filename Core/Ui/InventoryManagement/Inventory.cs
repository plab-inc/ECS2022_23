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
    private int pixelSize = 16;
    private int _scale;
    private int width;
    private int height;
    private int _rowCount;
    private int _colCount;
    private Rectangle DestinationRec;
    private List<InventoryRow> inventoryRows = new List<InventoryRow>();
    private Texture2D background;

    public Inventory(int rowCount, int colCount)
    {
        _rowCount = rowCount;
        _colCount = colCount;
        _scale = 5;
        width = pixelSize * _rowCount * _scale;
        height = pixelSize * _colCount * _scale;
        DestinationRec = new Rectangle(0, 0, width, height);
        background = UiLoader.CreateColorTexture(Color.DarkGray);
        CreateRows();
    }

    public void CreateRows()
    {
        for (var i = 0; i < _rowCount; i++)
        {
               inventoryRows.Add(new InventoryRow(new Rectangle(DestinationRec.X,DestinationRec.Y+i*height/_colCount, width, 
                   height/_rowCount), 
                   _colCount, UiLoader.CreateColorTexture(Color.Coral), pixelSize*_scale, _scale));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, DestinationRec, Color.White);
        foreach (var row in inventoryRows)
        {
            row.Draw(spriteBatch);
        }
    }
    
    public void AddItem(Item item)
    {
        if (item == null) return;
        
        foreach (var row in inventoryRows)
        {
            var slot = row.FindItem(item);
           
            if (slot != null)
            {
                slot.count++;
                return;
            }

            slot = row.GetFreeSlot();
            if (slot == null) continue;
            slot.AddItem(item, 1);
            return;
        }
    }

}