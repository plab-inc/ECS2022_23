using System.Collections.Generic;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui;
using ECS2022_23.Core.Ui.InventoryManagement;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ECS2022_23.Core.Manager;

public static class LockerManager
{
    private static Pocket _pocket;
    private static bool _lockerIsActive;
    
    private static Texture2D _spriteSheet;
    private static Rectangle _sourceRecOfArrow = new Rectangle(9*16, 3*16, 16, 16);
    private static Point _scale = new Point(6, 6);

    public static Locker Locker { get; set; }
    private static List<Item> _itemsInLocker = new List<Item>();

    public static void Init(List<Item> itemsInLocker)
    {
        Locker = new Locker(3, 2);
        _pocket = _pocket = new Pocket(3, 3);
        _spriteSheet = UiLoader.SpriteSheet;
        _lockerIsActive = true;

        _itemsInLocker = itemsInLocker;

        foreach (var item in itemsInLocker)
        {
            Locker.AddItem(item);
        }
    }
    
    public static void LoadLocker(Locker locker)
    {
        Locker = locker;
    }

    public static void AddToPocket(Item item)
    {
        _pocket.AddItem(item);
        _itemsInLocker.Remove(item);
    }

    public static void RemoveFromPocket(Item item)
    {
        _pocket.RemoveItem(item);
        _itemsInLocker.Add(item);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_spriteSheet, new Rectangle(Game1.ScreenWidth/2-(16*_scale.X/2), 
            Game1.ScreenHeight/2-(16*_scale.Y/2), 
            16*_scale.X, 16*_scale.Y), _sourceRecOfArrow, Color.White);
        Locker.Draw(spriteBatch);
        _pocket.Draw(spriteBatch);
    }

    public static void HandleInput(Keys actionKey)
    {
        SwitchActiveInventory(actionKey);
        switch (actionKey)
        {
            case Keys.Right: MoveIndex(1);
                break;
            case Keys.Left: MoveIndex(-1);
                break;
            case Keys.Enter:
                if (_lockerIsActive)
                {
                    TransferSelectedItem(Locker, _pocket);
                }
                else
                {
                    TransferSelectedItem(_pocket, Locker);
                }
                break;
        }
    }

    private static void MoveIndex(int i)
    {
        switch (_lockerIsActive)
        {
          case true:
              MoveIndex(Locker, i);
              break;
          case false:
              MoveIndex(_pocket, i);
              break;
        }
    }

    private static void MoveIndex(Inventory inventory, int i)
    {
        switch (i)
        {
            case > 0:
                inventory.IncreaseIndex();
                break;
            case < 0:
                inventory.DecreaseIndex();
                break;
        }
    }

    private static void TransferSelectedItem(Inventory fromInventory, Inventory toInventory)
    {
        var toTransfer = fromInventory.GetSelectedItem();
        
        if (toTransfer == null) return;

        switch (toTransfer)
        {
            case Key: return;
            case Weapon weapon:
                if (SwitchBothWeapons(fromInventory, toInventory, weapon)) return;
                break;
        }

        switch (fromInventory)
        {
            case Locker locker:
                if(InventoryManager.AddItem(toTransfer)) locker.RemoveItem(toTransfer);
                break;
            case Pocket:
                if (Locker.AddItem(toTransfer)) InventoryManager.RemoveItem(toTransfer);
                break;
        }
    }

    private static bool SwitchBothWeapons(Inventory fromInventory, Inventory toInventory, Item toTransfer)
    {
        if (!Locker.WeaponLimitReached() || !_pocket.WeaponLimitReached()) return false;
        
        var toSwitch = toInventory.GetWeapon();
        
        switch (fromInventory)
        {
            case Locker locker:
                InventoryManager.RemoveItem(toSwitch);
                if (InventoryManager.AddItem(toTransfer))
                {
                    locker.RemoveItem(toTransfer);
                    locker.AddItem(toSwitch);
                }
                break;
            case Pocket:
                Locker.RemoveItem(toSwitch);
                if (Locker.AddItem(toTransfer))
                {
                    InventoryManager.RemoveItem(toTransfer);
                    InventoryManager.AddItem(toSwitch);
                }
                break;
        }
        return true;
    }

    private static void SwitchActiveInventory(Keys actionKey)
    {
        switch (actionKey)
        {
            case Keys.Right:
            {
                if (_lockerIsActive && Locker.IsAtLastIndex()) _lockerIsActive = false;
                break;
            }
            case Keys.Left:
            {
                if (!_lockerIsActive && _pocket.IsAtFirstIndex()) _lockerIsActive = true;
                break;
            }
        }
    }
    
}