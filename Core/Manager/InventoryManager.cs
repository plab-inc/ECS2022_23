using System.Collections.Generic;
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
    
    private static Weapon _prevWeapon;
    private static Trinket _prevTrinket;

    public static void Init(Player player)
    {
        _toolBar = new ToolBar(1, 9);
        _weaponSlot = new WeaponSlot();
        _trinketSlot = new TrinketSlot();
        _prevTrinket = null;
        _prevWeapon = null;
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
    
    public static void Update(Player player)
    {
        UpdateWeapon(player);
        UpdateTrinket(player);
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
            case Weapon:
                _weaponSlot.AddItem(item);
                break;
            default:
                _toolBar.AddItem(item);
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
                if(_toolBar.IsItemActive(trinket)) {
                    UseTrinket(_player, trinket);
                }
                break;
            case Weapon:
                if(_weaponSlot.RemoveItem(item)) _player.Weapon = null;
                break;
        }
        LockerManager.RemoveFromPocket(item);
        _toolBar.RemoveItem(item);
    }

    private static void UseTrinket(Player player, Trinket trinket)
    {
        _toolBar.SwitchActiveState(trinket);
        if (trinket.Equals(player.Trinket))
        {
            trinket.Unequip(player);
            _prevTrinket = trinket;
        }
        else
        {
            _trinketSlot.AddItem(trinket);
            _prevTrinket = trinket;
            player.UseItem(trinket);
        }
    }
    
    private static void UpdateWeapon(Player player)
    {
        if (player.Weapon != null)
        {
            if (_prevWeapon != null)
            {
                if (!_prevWeapon.Equals(player.Weapon))
                {
                    _weaponSlot.AddItem(player.Weapon);
                    LockerManager.AddToPocket(player.Weapon);
                    _prevWeapon = player.Weapon;
                }
            }
            else
            {
                _weaponSlot.AddItem(player.Weapon);
                LockerManager.AddToPocket(player.Weapon);
                _prevWeapon = player.Weapon;
            }
        }
        else if(_prevWeapon != null)
        {
            _weaponSlot.RemoveItem(_prevWeapon);
            LockerManager.RemoveFromPocket(_prevWeapon);
            _prevWeapon = null;
        }
    }

    private static void UpdateTrinket(Player player)
    {
        if (player.Trinket != null)
        {
            if (_prevTrinket != null)
            {
                if (!_prevTrinket.Equals(player.Trinket))
                {
                    _trinketSlot.AddItem(player.Trinket);
                    LockerManager.AddToPocket(player.Weapon);
                    _prevTrinket = player.Trinket;
                }
            }
            else
            {
                _trinketSlot.AddItem(player.Trinket);
                LockerManager.AddToPocket(player.Weapon);
                _prevTrinket = player.Trinket;
            }
        }
        else if(_prevTrinket != null)
        {
            _trinketSlot.RemoveItem(_prevTrinket);
            LockerManager.RemoveFromPocket(_prevTrinket);
            _prevTrinket = null;
        }
    }
}