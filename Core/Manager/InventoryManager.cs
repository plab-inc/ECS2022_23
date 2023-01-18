using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class InventoryManager
{
    private static Player _player;
    
    private static Toolbar _toolbar;
    private static ItemSlot _weaponSlot;
    private static ItemSlot _trinketSlot;
    
    public static void Init(Player player)
    {
        _toolbar = new Toolbar(1, 9);
        _weaponSlot = new ItemSlot(InventoryType.WeaponSlot);
        _trinketSlot = new ItemSlot(InventoryType.TrinketSlot);
        _player = player;
        
        if (player.Items != null)
        {
            foreach (var item in player.Items)
            {
                AddItem(item);
            }
        }

        if (player.Trinket != null)
        {
            var trinket = player.Trinket;
            AddItem(trinket);
            _toolbar.SwitchActiveState(trinket);
            _trinketSlot.AddItem(trinket);
            _player.UseItem(trinket);
        }
        
        if (player.Weapon != null)
        {
            AddItem(player.Weapon);
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _toolbar.Draw(spriteBatch);
        _weaponSlot.Draw(spriteBatch);
        _trinketSlot.Draw(spriteBatch);
    }
    
    public static void UseItemAtIndex(int index)
    {
        var item = _toolbar.GetItemAtIndex(index);
        if (item == null) return;
        UseItem(item);
    }

    private static void UseItem(Item item)
    {
     if(item.GetType() == typeof(Trinket)) {
         UseTrinket((Trinket) item);
         return;
     }
    
     if (_player.UseItem(item))
     {
         RemoveItem(item);
     }
    }
    
    public static bool AddItem(Item item)
    {
        if (item == null) return false;

        switch (item)
        {
            case Weapon weapon:
                _weaponSlot.AddItem(item);
                SetPlayerWeapon(weapon);
                break;
            default:
                _toolbar.AddItem(item);
                _player.Items.Add(item);
                break;
        }
        
        LockerManager.AddToPocket(item);
        return true;
    }

    public static void RemoveItem(Item item)
    {
        if (item == null) return;
        
        switch (item)
        {
            case Trinket trinket:
                if(_toolbar.ItemIsActive(trinket)) {
                    UseTrinket(trinket);
                }
                break;
            case Weapon:
                if(_weaponSlot.RemoveItem(item)) SetPlayerWeapon(null);
                break;
        }
        LockerManager.RemoveFromPocket(item);
        _toolbar.RemoveItem(item);
        _player.Items.Remove(item);
    }

    private static void UseTrinket(Trinket trinket)
    {
        _toolbar.SwitchActiveState(trinket);
        if (trinket.ItemType == _player.Trinket?.ItemType)
        {
            trinket.Unequip(_player);
            _trinketSlot.RemoveItem(trinket);
        }
        else
        {
            _trinketSlot.AddItem(trinket);
            _player.UseItem(trinket);
        }
    }

    private static void SetPlayerWeapon(Weapon weapon)
    {
        _player.Weapon = weapon;
    }
}