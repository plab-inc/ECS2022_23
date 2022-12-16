using System.Threading;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory;

public class InventorySlot
{
    private Rectangle _destinationRec;
    public bool IsUsed = false;
    public Item Item;
    public int Count = 0;
    private Texture2D _backgroundTexture;
    private UiText _text;
    private int _scale;
    public bool Selected;
    private Texture2D _selectedTexture = UiLoader.GetSpritesheet();
    private Rectangle _selectedSourceRec = new Rectangle(9*16, 4*16, 16, 16);
    public InventorySlot(Rectangle dest, Texture2D background, int scale)
    {
        _destinationRec = dest;
        _backgroundTexture = background;
        _text = UiLoader.CreateTextElement("");
        _text.DestinationRec = new Rectangle(_destinationRec.X, _destinationRec.Y, _destinationRec.Width / 2,
            _destinationRec.Height / 2);
        _scale = scale;
        _text.Scale = new Vector2(_scale/2, _scale/2);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsUsed)
        {
            spriteBatch.Draw(Item.Texture, _destinationRec, Item.SourceRect, Color.White);
            if (Selected)
            {
                spriteBatch.Draw(_selectedTexture, _destinationRec, _selectedSourceRec, Color.White);
            }
            _text.Text = "" + Count;
            _text.Draw(spriteBatch);
        }
        else
        {
            spriteBatch.Draw(_backgroundTexture, _destinationRec, Color.White);
        }
    
    }

    public void AddItem(Item item, int count)
    {
        this.Item = item;
        this.Count = count;
        IsUsed = true;
    }
}