using System;
using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory.InventoryTypes;

[Serializable]
public abstract class Inventory
{
    private Texture2D _backgroundTexture;
    private int _prevIndex;
    private InventoryRow _prevRow;
    protected int ColCount;
    protected Rectangle DestinationRec;
    protected int Height;
    protected List<InventoryRow> InventoryRows = new();

    protected int PixelSize = 16;
    protected int RowCount;
    public int Scale = 5;
    public int SlotCount = 0;
    protected int Width;


    protected Inventory(int rowCount, int colCount)
    {
        RowCount = rowCount;
        ColCount = colCount;
        _backgroundTexture = UiLoader.CreateColorTexture(new Color(28, 111, 255, 255));
    }

    protected void CreateRows()
    {
        for (var i = 0; i < RowCount; i++)
            InventoryRows.Add(new InventoryRow(new Rectangle(DestinationRec.X, DestinationRec.Y + i * Height / RowCount,
                Width,
                Height / RowCount), ColCount, PixelSize * Scale, this));

        if (InventoryRows.Count > 0)
        {
            _prevRow = InventoryRows[0];
            if (_prevRow.Slots.Count > 0) _prevRow.Slots[_prevIndex].IsSelected = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, DestinationRec, Color.White);
        foreach (var row in InventoryRows) row.Draw(spriteBatch);
    }

    public virtual bool AddItem(Item item)
    {
        if (item == null) return false;

        foreach (var row in InventoryRows)
        {
            var slot = row.FindSlotWithItem(item);

            if (slot != null)
            {
                slot.ItemCount++;
                return true;
            }

            slot = row.GetFreeSlot();
            if (slot == null) continue;
            slot.AddItem(item, 1);
            return true;
        }

        return false;
    }

    protected void SelectIndex(int index)
    {
        if (index < 0) index = 0;

        if (index == _prevIndex) return;
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Index == index);
            if (slot == null) continue;
            slot.IsSelected = true;
            var prevSlot = _prevRow.Slots[_prevIndex % ColCount];
            prevSlot.IsSelected = false;
            _prevIndex = index;
            _prevRow = row;
            return;
        }
    }

    public void IncreaseIndex()
    {
        SelectIndex(_prevIndex + 1);
    }

    public void DecreaseIndex()
    {
        SelectIndex(_prevIndex - 1);
    }

    public Item GetSelectedItem()
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.IsSelected);
            if (slot == null) continue;
            return slot.Item;
        }

        return null;
    }

    public virtual bool RemoveItem(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindSlotWithItem(item);

            if (slot == null) continue;
            slot.ItemCount--;
            if (slot.ItemCount <= 0) slot.RemoveItem();
            return true;
        }

        return false;
    }

    public void SwitchActiveState(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindSlotWithItem(item);
            if (slot == null) continue;
            slot.IsActive = !slot.IsActive;
            return;
        }
    }

    public bool ItemIsActive(Item item)
    {
        foreach (var row in InventoryRows)
        {
            var slot = row.FindSlotWithItem(item);
            if (slot == null) continue;
            return slot.IsActive;
        }

        return false;
    }

    public bool IsAtLastIndex()
    {
        return _prevIndex + 1 == SlotCount;
    }

    public bool IsAtFirstIndex()
    {
        return _prevIndex == 0;
    }

    public Weapon GetWeapon()
    {
        foreach (var row in InventoryRows)
        foreach (var slot in row.Slots)
        {
            if (slot.Item == null) continue;
            if (slot.Item.GetType() == typeof(Weapon)) return (Weapon) slot.Item;
        }

        return null;
    }
}