using System;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Loader;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;

[Serializable]
public class Pocket : Inventory
{
    private UiText _text;
    public InventoryType Type;

    public Pocket(int rowCount, int colCount, InventoryType type) : base(rowCount, colCount)
    {
        Type = type;
        Width = PixelSize * ColCount * Scale;
        Height = PixelSize * RowCount * Scale;

        if (type == InventoryType.LockerInventory)
        {
            DestinationRec = new Rectangle(Game1.ScreenWidth / 2 - 100 - Width, Game1.ScreenHeight / 2 - Height / 2,
                Width, Height);
            _text = UiLoader.CreateTextElement("Locker");
        }
        else if (type == InventoryType.PocketInventory)
        {
            DestinationRec = new Rectangle(Game1.ScreenWidth / 2 + 100, Game1.ScreenHeight / 2 - Height / 2, Width,
                Height);
            _text = UiLoader.CreateTextElement("Inventory");
        }

        if (_text != null)
        {
            _text.DestinationRec =
                new Rectangle(DestinationRec.X, DestinationRec.Y - Height / RowCount / 2, Width, Height);
            _text.Scale = new Vector2(Scale / 2, Scale / 2);
        }

        CreateRows();
    }

    public bool WeaponLimitReached { get; private set; }

    public override bool AddItem(Item item)
    {
        if (item.GetType() == typeof(Weapon))
        {
            if (WeaponLimitReached)
            {
                if (Type == InventoryType.LockerInventory) return false;
                if (Type == InventoryType.PocketInventory)
                {
                    var weapon = GetWeapon();
                    RemoveItem(weapon);
                }
            }

            WeaponLimitReached = base.AddItem(item);
            return WeaponLimitReached;
        }

        return base.AddItem(item);
    }

    public override bool RemoveItem(Item item)
    {
        var removed = base.RemoveItem(item);

        if (!removed) return false;

        if (item.GetType() == typeof(Weapon)) WeaponLimitReached = false;

        return true;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _text.Draw(spriteBatch);
    }
}