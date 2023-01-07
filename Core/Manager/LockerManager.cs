using ECS2022_23.Core.Entities.Characters;
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
    private static Locker _locker;
    private static Pocket _pocket;
    private static bool _lockerIsActive;
    
    private static Texture2D _spriteSheet;
    private static Rectangle _sourceRecOfArrow = new Rectangle(9*16, 3*16, 16, 16);
    private static Point _scale = new Point(6, 6);
    public static void Init()
    {
        _locker = new Locker(3, 3);
        _pocket = _pocket = new Pocket(3, 3);
        _spriteSheet = UiLoader.SpriteSheet;
        _lockerIsActive = true;
    }

    public static void AddPlayerItems(Player player)
    {
        _pocket.ClearItems();
        
        foreach (var item in player.Items)
        {
            _pocket.AddItem(item);
        }
    }
    
    public static void LoadLocker(Locker locker)
    {
        _locker = locker;
    }

    public static void AddToPocket(Item item)
    {
        _pocket.AddItem(item);
    }

    public static void RemoveFromPocket(Item item)
    {
        _pocket.RemoveItem(item);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_spriteSheet, new Rectangle(Game1.ScreenWidth/2-(16*_scale.X/2), 
            Game1.ScreenHeight/2-(16*_scale.Y/2), 
            16*_scale.X, 16*_scale.Y), _sourceRecOfArrow, Color.White);
        _locker.Draw(spriteBatch);
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
            case Keys.Enter: Transfer();
                break;
        }
    }

    private static void MoveIndex(int i)
    {
        switch (_lockerIsActive)
        {
          case true:
              MoveIndex(_locker, i);
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

    private static void Transfer()
    {
        Item toTransfer; 
        switch (_lockerIsActive)
        {
            case true:
                toTransfer = _locker.GetSelectedItem();
                InventoryManager.AddItem(toTransfer);
                _locker.RemoveItem(toTransfer);
                break;
            case false:
                toTransfer = _pocket.GetSelectedItem();
                _locker.AddItem(toTransfer);
               InventoryManager.RemoveItem(toTransfer);
                break;
        }
    }

    private static void SwitchActiveInventory(Keys actionKey)
    {
        switch (actionKey)
        {
            case Keys.Right:
            {
                if (_lockerIsActive && _locker.IsAtLastIndex())
                {
                    _lockerIsActive = false;
                }

                break;
            }
            case Keys.Left:
            {
                if (!_lockerIsActive && _pocket.IsAtFirstIndex())
                {
                    _lockerIsActive = true;
                }

                break;
            }
        }
    }
    
}