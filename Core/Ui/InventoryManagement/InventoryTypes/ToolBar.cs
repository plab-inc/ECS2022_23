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
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        var text = UiLoader.CreateTextElement("Toolbar");
        text.Scale = new Vector2(Scale / 2, Scale / 2);
        text.DestinationRec =  new Rectangle(DestinationRec.X, DestinationRec.Y-Height/RowCount/2, Width, Height);
        text.Draw(spriteBatch);
    }
}