using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui.InventoryManagement;

public static class InventoryManager
{
    private static Inventory _inventory = new(3,3);
    public static bool show = false;
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        if (show)
        {
            _inventory.Draw(spriteBatch);
        }
        else
        {
            _inventory.SelectIndex(0);
        }
    }

    public static void UseSelectedItem(Player player)
    {
        var item = _inventory.GetSelectedItem();
        if (item == null) return;
        player.UseItem(item);
        RemoveItem(item);
    }

    public static void AddItem(Item item)
    {
        _inventory.AddItem(item);
    }
    
    public static void RemoveItem(Item item)
    {
       _inventory.RemoveItem(item);
    }

    public static void IncreaseIndex()
    {
        _inventory.IncreaseIndex();
    }
    public static void DecreaseIndex()
    {
        _inventory.DecreaseIndex();
    }
}