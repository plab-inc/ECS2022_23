using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class ToolBar : Inventory
{
    public ToolBar(int rowCount, int colCount) : base(rowCount, colCount)
    {
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(Game1.ScreenWidth/2-Width/2, Game1.ScreenHeight-Height, Width, Height);
        CreateRows();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawText(spriteBatch);
        DrawNumbers(spriteBatch);
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        var text = UiLoader.CreateTextElement("Toolbar");
        text.Scale = new Vector2(Scale / 2, Scale / 2);
        text.DestinationRec =  new Rectangle(DestinationRec.X, DestinationRec.Y-Height/RowCount/2, Width, Height);
        text.Draw(spriteBatch);
    }
    
    private void DrawNumbers(SpriteBatch spriteBatch)
    {
        foreach (var row in InventoryRows)
        {
            foreach (var slot in row.Slots)
            {
                var number = slot.Index + 1;
                var text = UiLoader.CreateTextElement(""+number);
                text.Scale = new Vector2(Scale / 2, Scale / 2);
                text.DestinationRec =  new Rectangle(slot.DestinationRec.X+slot.DestinationRec.Width/2, slot.DestinationRec.Y, slot.DestinationRec.Width, slot.DestinationRec.Height);
                text.Draw(spriteBatch);
            }
        }
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