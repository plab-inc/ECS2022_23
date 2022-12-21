using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

public class Pocket : Inventory
{
    public Pocket(int rowCount, int colCount) : base(rowCount, colCount)
    {
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(Game1.ScreenWidth/2-Width/2, Game1.ScreenHeight/2-Height/2, Width, Height);
        CreateRows();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawText(spriteBatch);
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        var text = UiLoader.CreateTextElement("Inventory");
        text.DestinationRec =
            new Rectangle(DestinationRec.X, DestinationRec.Y - Height/ColCount/2, Width, Height);
        text.Scale = new Vector2(Scale/2, Scale/2);
        text.Draw(spriteBatch);
        var text2 = UiLoader.CreateTextElement("Press I to close");
        text2.DestinationRec =
            new Rectangle(DestinationRec.X + Width, DestinationRec.Y, Width, Height);
        text2.Scale = new Vector2(Scale/2, Scale/2);
        text2.Draw(spriteBatch);
        var text3= UiLoader.CreateTextElement("Press Enter to use");
        text3.DestinationRec =
            new Rectangle(DestinationRec.X + Width, DestinationRec.Y+Height/ColCount/2, Width, Height);
        text3.Scale = new Vector2(Scale/2, Scale/2);
        text3.Draw(spriteBatch);
        var text4= UiLoader.CreateTextElement("Choose with arrow keys");
        text4.DestinationRec =
            new Rectangle(DestinationRec.X + Width, DestinationRec.Y+Height/ColCount*2/2, Width, Height);
        text4.Scale = new Vector2(Scale/2, Scale/2);
        text4.Draw(spriteBatch);
    }
}