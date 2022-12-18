using System.Diagnostics;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public static class InventoryManager
{
    private static Pocket _pocket = new(3,3);
    private static ToolBar _toolBar = new(1,9);
    private static WeaponSlot _weaponSlot = new WeaponSlot();
    private static Weapon _prevWeapon;
    public static bool Show = false;

    public static void Update(Player player)
    {
        if (_prevWeapon != null)
        {
            if (_prevWeapon.Equals(player.Weapon))
            {
                return;
            }
        }
        _weaponSlot.AddItem(player.Weapon);
        _prevWeapon = player.Weapon;
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        if (Show)
        {
            _pocket.Draw(spriteBatch);
        }
        else
        {
            _pocket.SelectIndex(0);
        }
        _toolBar.Draw(spriteBatch);
        _weaponSlot.Draw(spriteBatch);
    }

    public static void UseSelectedItem(Player player)
    {
        var item = _pocket.GetSelectedItem();
        if (item == null) return;
        player.UseItem(item);
        RemoveItem(item);
    }

    public static void AddItem(Item item)
    {
        if (item.GetType() == typeof(Weapon))
        {
            _weaponSlot.AddItem(item);
        }
        else
        {
            _pocket.AddItem(item);
            _toolBar.AddItem(item);
        }
    }
    
    public static void RemoveItem(Item item)
    {
       _pocket.RemoveItem(item);
       _toolBar.RemoveItem(item);
    }

    public static void IncreaseIndex()
    {
        _pocket.IncreaseIndex();
    }
    public static void DecreaseIndex()
    {
        _pocket.DecreaseIndex();
    }
    
    public static void UseItemAtIndex(Player player, int index)
    {

        var item = _toolBar.GetItemAtIndex(index);
        if (item == null) return;
        player.UseItem(item);
        RemoveItem(item);
    }
}