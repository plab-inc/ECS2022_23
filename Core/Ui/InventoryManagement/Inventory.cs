using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public abstract class Inventory
{
    protected int PixelSize = 16;
    public int Scale = 5;
    protected int Width;
    protected int Height;
    protected int RowCount;
    protected int ColCount;
    protected Rectangle DestinationRec;
    protected List<InventoryRow> InventoryRows = new List<InventoryRow>();
    protected Texture2D BackgroundTexture;
    protected int PrevIndex = 0;
    protected InventoryRow PrevRow;
    public int SlotCount = 0;

    protected Inventory(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        BackgroundTexture = UiLoader.CreateColorTexture(Color.DarkGray);
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
            PrevRow = InventoryRows[0];
            if(PrevRow.Slots.Count > 0) PrevRow.Slots[PrevIndex].Selected = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(BackgroundTexture, DestinationRec, Color.White);
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

        if (index == PrevIndex) return;
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Index == index);
            if (slot == null) continue;
            slot.Selected = true;
            var prevSlot = PrevRow.Slots[PrevIndex%ColCount];
            prevSlot.Selected = false;
            PrevIndex = index;
            PrevRow = row;
            return;
        }
    }
    public void IncreaseIndex()
    {
        SelectIndex(PrevIndex+1);
    }
    public void DecreaseIndex()
    {
        SelectIndex(PrevIndex-1);
    }

    public Item GetSelectedItem()
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Selected == true);
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
            slot.Active = !slot.Active;
            return;
        }
    }

}