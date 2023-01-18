using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class Toolbar : Inventory
{
    private readonly List<UiText> _numberTexts;

    public Toolbar(int rowCount, int colCount) : base(rowCount, colCount)
    {
        Scale = 4;
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(Game1.ScreenWidth / 2 - Width / 2, Game1.ScreenHeight - Height, Width, Height);
        _numberTexts = new List<UiText>();
        CreateRows();
        foreach (var row in InventoryRows)
        foreach (var slot in row.Slots)
        {
            var number = slot.Index + 1;
            var text = UiLoader.CreateTextElement("" + number);
            text.Scale = new Vector2(Scale / 2, Scale / 2);
            text.DestinationRec = new Rectangle(slot.DestinationRec.X + slot.DestinationRec.Width / 2,
                slot.DestinationRec.Y - slot.DestinationRec.Height / 2,
                slot.DestinationRec.Width, slot.DestinationRec.Height);
            _numberTexts.Add(text);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach (var numberText in _numberTexts) numberText.Draw(spriteBatch);
    }

    public Item GetItemAtIndex(int index)
    {
        if (index < 0) return null;
        foreach (var row in InventoryRows)
        {
            var slot = row.Slots.Find(slot => slot.Index == index);
            if (slot == null) continue;
            SelectIndex(index);
            if (slot.Item != null) return slot.Item;
        }

        return null;
    }
}