using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using ECS2022_23.Core.Ui.InventoryManagement.InventoryTypes;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class InventoryManager
{
    private static Player _player;
    
    private static ToolBar _toolBar;
    private static WeaponSlot _weaponSlot;
    private static TrinketSlot _trinketSlot;
    
    public static void Init(Player player)
    {
        _toolBar = new ToolBar(1, 9);
        _weaponSlot = new WeaponSlot();
        _trinketSlot = new TrinketSlot();
        _player = player;
        
        if (player.Items != null)
        {
            foreach (var item in player.Items)
            {
                AddItem(item);
            }
        }

        if (player.Weapon != null)
        {
            AddItem(player.Weapon);
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _toolBar.Draw(spriteBatch);
        _weaponSlot.Draw(spriteBatch);
        _trinketSlot.Draw(spriteBatch);
    }
    
    public static void UseItemAtIndex(Player player, int index)
    {
        var item = _toolBar.GetItemAtIndex(index);
        if (item == null) return;
        UseItem(player, item);
    }

    private static void UseItem(Player player, Item item)
    {
        switch (item) 
        {
            case Trinket trinket:
                UseTrinket(player, trinket);
                return;
        }
    
        if (player.UseItem(item))
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
                _toolBar.AddItem(item);
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
                if(_toolBar.ItemIsActive(trinket)) {
                    UseTrinket(_player, trinket);
                }
                break;
            case Weapon:
                if(_weaponSlot.RemoveItem(item)) SetPlayerWeapon(null);
                break;
        }
        LockerManager.RemoveFromPocket(item);
        _toolBar.RemoveItem(item);
        _player.Items.Remove(item);
    }

    private static void UseTrinket(Player player, Trinket trinket)
    {
        _toolBar.SwitchActiveState(trinket);
        if (trinket.ItemType == player.Trinket.ItemType)
        {
            trinket.Unequip(player);
            _trinketSlot.RemoveItem(trinket);
        }
        else
        {
            _trinketSlot.AddItem(trinket);
            player.UseItem(trinket);
        }
    }

    private static void SetPlayerWeapon(Weapon weapon)
    {
        _player.Weapon = weapon;
    }
}