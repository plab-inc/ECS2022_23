using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class Locker : Inventory
{
    public Locker(int rowCount, int colCount) : base(rowCount, colCount)
    {
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(Game1.ScreenWidth/2-100-Width, Game1.ScreenHeight/2-Height/2, Width, Height);
        CreateRows();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawText(spriteBatch);
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        var text = UiLoader.CreateTextElement("Saved Items");
        text.DestinationRec =
            new Rectangle(DestinationRec.X, DestinationRec.Y - Height/RowCount/2, Width, Height);
        text.Scale = new Vector2(Scale/2, Scale/2);
        text.Draw(spriteBatch);
    }
}