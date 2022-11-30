using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public class UiManager
{
    public UiPanel StatsPanel { get; set; }
    public UiPanel ItemPanel { get; set; }
    private int _preHeartCount;
    private Weapon _preWeapon;
    private int _preItemCount;
    private bool _statsHaveChanged;
    private bool _itemsHaveChanged;
    
    public void Update(Player player)
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

    public void Draw(SpriteBatch spriteBatch)
    {
        StatsPanel.Draw(spriteBatch);
        ItemPanel.Draw(spriteBatch);
    }
    private void UpdateHearts(Player player)
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

    private void UpdateText(UiLabels label, float stats)
    {
        UiText uiText = (UiText) StatsPanel.GetComponentByLabel(label);
        if (uiText == null) return;
        if (uiText.Text == $"{stats:0.##}") return;
       
        uiText.Text = stats <= 0 ? "0.00" : $"{stats:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
        _statsHaveChanged = true;
    }

    private static bool HasHeartLabel(Component component)
    {
        return component.UiLabel == UiLabels.Heart;
    }

    private void UpdateInventory(Player player)
    {
      UpdateItems(player);
      UpdateWeapon(player);
    }

    private void UpdateStats(Player player)
    {
        UpdateHearts(player);
        UpdateText(UiLabels.CoinText, player.Money);
        UpdateText(UiLabels.XpText, player.XpToNextLevel);
    }

    private void UpdateItems(Player player)
    {
        var items = player.Items;
        if (items == null) return;
        if(items.Count == _preItemCount) return;
        ItemPanel.RemoveAll(component => component.UiLabel == UiLabels.ItemIcon);
        foreach (var item in items)
        {
            ItemPanel.Add(UiLoader.CreateIcon(item.SourceRect, UiLabels.ItemIcon));
        }
        _itemsHaveChanged = true;
        _preItemCount = items.Count;
    }
    
    private void UpdateWeapon(Player player) {
    
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
            ItemPanel.Add(UiLoader.CreateIcon(weapon.SourceRect, UiLabels.WeaponIcon));
        }

        _preWeapon = weapon;
        _itemsHaveChanged = true;
    }

}