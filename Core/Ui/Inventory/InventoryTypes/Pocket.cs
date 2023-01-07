using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
public class Pocket : Inventory
{
    public Pocket(int rowCount, int colCount) : base(rowCount, colCount)
    {
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;
        DestinationRec = new Rectangle(Game1.ScreenWidth/2+100, Game1.ScreenHeight/2-Height/2, Width, Height);
        CreateRows();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawText(spriteBatch);
    }

    public override bool AddItem(Item item)
    {
        if(item.GetType() != typeof(Weapon)) return base.AddItem(item);

        var weapon = GetWeapon();
        RemoveItem(weapon);
        return base.AddItem(item);
    }
    
    private void DrawText(SpriteBatch spriteBatch)
    {
        var text = UiLoader.CreateTextElement("Inventory");
        text.DestinationRec =
            new Rectangle(DestinationRec.X, DestinationRec.Y - Height/RowCount/2, Width, Height);
        text.Scale = new Vector2(Scale/2, Scale/2);
        text.Draw(spriteBatch);
    }
    
    public bool WeaponLimitReached()
    {
        var weapon = GetWeapon();
        return weapon != null;
    }
}