using System.Threading;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory;

public class InventorySlot
{
    public Rectangle DestinationRec;
    public bool IsUsed = false;
    public Item item;
    public int count = 0;
    public Texture2D backgroundTexture;
    private UiText _text;
    private int _scale;
    public InventorySlot(Rectangle dest, Texture2D background, int scale)
    {
        DestinationRec = dest;
        backgroundTexture = background;
        _text = UiLoader.CreateTextElement("");
        _text.DestinationRec = new Rectangle(DestinationRec.X, DestinationRec.Y, DestinationRec.Width / 2,
            DestinationRec.Height / 2);
        _scale = scale;
        _text.Scale = new Vector2(_scale/2, _scale/2);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsUsed)
        {
            spriteBatch.Draw(item.Texture, DestinationRec, item.SourceRect, Color.White);
            _text.Text = "" + count;
            _text.Draw(spriteBatch);
        }
        else
        {
            spriteBatch.Draw(backgroundTexture, DestinationRec, Color.White);
        }
    
    }

    public void AddItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
        IsUsed = true;
    }
}