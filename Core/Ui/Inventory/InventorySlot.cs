using System;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.Inventory;
[Serializable]
public class InventorySlot
{
    public int Index;
    public Item Item;
    public int ItemCount = 0;
    
    public bool IsSelected;
    public bool IsActive = false;
    public bool IsUsed = false;
    
    public Rectangle DestinationRec;
    
    private UiText _text;
    private int _scale;
    private Rectangle _selectedSourceRec = new Rectangle(9*16, 4*16, 16, 16);
    private Rectangle _frameSourceRec = new Rectangle(8 * 16, 4 * 16, 16, 16);
    private Texture2D _activeTexture;
    private Texture2D _spriteSheet;
    
    public InventorySlot(Rectangle dest, int scale, int index)
    {
        DestinationRec = dest;
        _activeTexture = UiLoader.CreateColorTexture(new Color(192, 91, 255, 255));
        _spriteSheet = UiLoader.SpriteSheet;
        _text = UiLoader.CreateTextElement("");
        _text.DestinationRec = new Rectangle(DestinationRec.X+DestinationRec.Width/8, DestinationRec.Y, DestinationRec.Width / 2,
            DestinationRec.Height / 2);
        _scale = scale;
        _text.Scale = new Vector2(_scale/2, _scale/2);
        Index = index;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            spriteBatch.Draw(_activeTexture, DestinationRec, Color.White);
        }
        spriteBatch.Draw(_spriteSheet, DestinationRec, _frameSourceRec, Color.White);
        if (IsUsed)
        {
            spriteBatch.Draw(Item.Texture, DestinationRec, Item.SourceRect, Color.White);

            _text.Text = "" + ItemCount;
            _text.Draw(spriteBatch);
        }
        if (IsSelected)
        {
            spriteBatch.Draw(_spriteSheet, DestinationRec, _selectedSourceRec, Color.White);
        }
    }

    public void AddItem(Item item, int count)
    {
        Item = item;
        ItemCount = count;
        IsUsed = true;
    }

    public void RemoveItem()
    {
        Item = null;
        IsUsed = false;
        IsActive = false;
        ItemCount = 0;
    }
}