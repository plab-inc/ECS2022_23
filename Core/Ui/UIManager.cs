using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public static class UiManager
{
    public static UiPanel StatsPanel { get; set; }
    public static UiPanel ItemPanel { get; set; }
    private static int _preHeartCount;
    private static Weapon _preWeapon;
    private static int _preItemCount;
    private static bool _statsHaveChanged;
    private static bool _itemsHaveChanged;
    
    public static void Update(Player player)
    {
        _statsHaveChanged = false;
        _itemsHaveChanged = false;
        
        UpdateStats(player);
        if (_statsHaveChanged)
        {
            StatsPanel.Update();
        }

        UpdateInventory(player);
        if (!_itemsHaveChanged) return;
        ItemPanel.Update();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        StatsPanel.Draw(spriteBatch);
        ItemPanel.Draw(spriteBatch);
    }
    private static void UpdateHearts(Player player)
    {
        var heartCount = (int) player.HP;
        if (heartCount == _preHeartCount) return;
        var index = StatsPanel.GetIndexFromLabel(UiLabels.HpIcon);
        if (index < 0) return;

        if (heartCount > 0)
        {
          
            var change = heartCount - _preHeartCount;

            if (change > 0)
            {
                for (int i = 1; i <= change; i++)
                {
                    StatsPanel.InsertAtIndex(UiLoader.CreateHeart(), index+i);
                }
            } else if (change < 0)
            {
                change *= -1;
                for (int i = 1; i <= change; i++)
                {
                    StatsPanel.RemoveAtIndex(index+i,UiLabels.Heart);
                }
            }
            _preHeartCount = heartCount;
        } else 
        {
            _preHeartCount = 0;
            StatsPanel.RemoveAll( component => component.UiLabel == UiLabels.Heart);
        }

        _statsHaveChanged = true;
    }

    private static void UpdateText(UiPanel panel, UiLabels label, float stats)
    {
        UiText uiText = (UiText) panel.GetComponentByLabel(label);
        if (uiText == null) return;
        if (uiText.Text == $"{stats:0.##}") return;
       
        uiText.Text = stats <= 0 ? "0" : $"{stats:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
        _statsHaveChanged = true;
    }

    private static bool HasHeartLabel(Component component)
    {
        return component.UiLabel == UiLabels.Heart;
    }

    private static void UpdateInventory(Player player)
    {
      UpdateItems(player);
      UpdateWeapon(player);
    }

    private static void UpdateStats(Player player)
    {
        UpdateHearts(player);
        UpdateText(StatsPanel, UiLabels.CoinText, player.Money);
        UpdateText(StatsPanel, UiLabels.XpText, player.XpToNextLevel);
    }

    /*
    private static void UpdateItems(Player player)
    {
        var items = player.Items;
        if (items == null) return;
        if(items.Count == _preItemCount) return;

        var itemGroups = items
            .GroupBy(item => item.SourceRect)
            .Select(grp => grp.ToList())
            .ToList();
        ItemPanel.RemoveAll(component => component.UiLabel == UiLabels.ItemIcon);
        ItemPanel.RemoveAll(component => component.UiLabel == UiLabels.ItemText);
        foreach (var group in itemGroups)
        {
            var sourceRec = group.First().SourceRect;
            ItemPanel.Add(UiLoader.CreateIcon(sourceRec, UiLabels.ItemIcon));
            var text = UiLoader.CreateTextElement(UiLabels.ItemText);
            text.Text = ""+group.Count;
            ItemPanel.Add(text);
        }
        _itemsHaveChanged = true;
        _preItemCount = items.Count;
    }
    */
    
    private static void UpdateItems(Player player)
    {
        var items = player.Items;
        if (items == null) return;
        if(items.Count == _preItemCount) return;

        var itemGroups = items
            .GroupBy(item => item.SourceRect)
            .Select(grp => grp.ToList())
            .ToList();
        UpdateText(ItemPanel,UiLabels.HealthText, 0);
        UpdateText(ItemPanel, UiLabels.ArmorText, 0);
        foreach (var group in itemGroups)
        {
            var sourceRec = group.First().SourceRect;
            var healthItem = ItemPanel.GetComponentByLabel(UiLabels.HealthItemIcon);
            var armorItem = ItemPanel.GetComponentByLabel(UiLabels.ArmorItemIcon);
            if (sourceRec == healthItem.SourceRec)
            {
                UpdateText(ItemPanel,UiLabels.HealthText, group.Count);
            } else if (sourceRec == armorItem.SourceRec)
            {
                UpdateText(ItemPanel, UiLabels.ArmorText, group.Count);
            }
            else
            {
                var index = ItemPanel.GetIndexFromLabel(UiLabels.ArmorText);
                var delete = true;
                while (delete)
                {
                    if (!ItemPanel.RemoveAtIndex(index + 1, UiLabels.ItemIcon) &&
                        !ItemPanel.RemoveAtIndex(index + 1, UiLabels.ItemText))
                        delete = false;
                }
                ItemPanel.Add(UiLoader.CreateIcon(sourceRec, UiLabels.ItemIcon));
                var text = UiLoader.CreateTextElement(UiLabels.ItemText);
                text.Text = ""+group.Count;
                ItemPanel.Add(text);
            }
           
        }
        _itemsHaveChanged = true;
        _preItemCount = items.Count;
    }
    
    private static void UpdateWeapon(Player player) {
    
        if (player.Weapon == null || player.Weapon == _preWeapon) return;
        
        var index = ItemPanel.GetIndexFromLabel(UiLabels.WeaponIcon);
        var weapon = player.Weapon;
        if (index >= 0)
        {
            ItemPanel.RemoveAtIndex(index, UiLabels.WeaponIcon);
            ItemPanel.InsertAtIndex(UiLoader.CreateIcon(weapon.SourceRect, UiLabels.WeaponIcon), index);
        }
        else
        {
            ItemPanel.InsertAtIndex(UiLoader.CreateIcon(weapon.SourceRect, UiLabels.WeaponIcon), 0);
        }

        _preWeapon = weapon;
        _itemsHaveChanged = true;
    }

    public static Item UseItemAtIndex(int index, Player player)
    {
        if (index <= 0) return null;
        var component = ItemPanel.GetComponentAtIndex(index);
        return player.Items.Find(item => item.SourceRect == component.SourceRec);
    }
}