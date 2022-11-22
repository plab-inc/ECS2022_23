using System;
using ECS2022_23.Core.entities.characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.ui;

public class UiManager
{
    private UiPanel _panel;
    private int _preHeartCount;
    private float _preMoneyCount;
    private float _preXpCount;
    private bool _hasChanged;
    public void AddPanel(UiPanel panel)
    {
        _panel = panel;
    }

    public void Update(GameTime gameTime, Player player)
    {
        //TODO ggf später Observer einbauen für Updates, falls es mehr panels werden, die verwaltet werden müssen
        _hasChanged = false;
        UpdateHearts(player);
        UpdateMoneyText(player);
        UpdateXpText(player);
        if (_hasChanged)
        {
            _panel.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _panel.Draw(spriteBatch);
    }
    private void UpdateHearts(Player player)
    {
        var heartCount = (int) player.HP;
        if (heartCount == _preHeartCount) return;
        var index = _panel.GetIndexFromLabel(UiLabels.HpIcon);
        if (index < 0) return;

        if (heartCount > 0)
        {
          
            var change = heartCount - _preHeartCount;

            if (change > 0)
            {
                for (int i = 1; i <= change; i++)
                {
                    _panel.InsertAtIndex(UiLoader.CreateHeart(), index+i);
                }
            } else if (change < 0)
            {
                change *= -1;
                for (int i = 1; i <= change; i++)
                {
                    _panel.RemoveAtIndex(index+i,UiLabels.Heart);
                }
            }
            _preHeartCount = heartCount;
        } else 
        {
            _preHeartCount = 0;
            _panel.RemoveAll(HasHeartLabel);
        }

        _hasChanged = true;
    }
    private void UpdateMoneyText(Player player)
    {
        var money = player.Money;
        if (Math.Abs(_preMoneyCount - money) == 0) return;
        var index = _panel.GetIndexFromLabel(UiLabels.CoinText);
        if (index < 0) return;
        UiText uiText = (UiText) _panel.GetComponentAtIndex(index);
        if (uiText == null) return;
        uiText.Text = player.Money <= 0 ? "0.00" : $"{money:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
        _preMoneyCount = money;
        _hasChanged = true;
    }
    
    private void UpdateXpText(Player player)
    {
        var xp = player.XpToNextLevel;
        if (Math.Abs(_preXpCount - xp) == 0) return;
        var index = _panel.GetIndexFromLabel(UiLabels.XpText);
        if (index < 0) return;
        UiText uiText = (UiText) _panel.GetComponentAtIndex(index);
        if (uiText == null) return;
        uiText.Text = player.XpToNextLevel <= 0 ? "0.00" : $"{xp:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
        _preXpCount = xp;
        _hasChanged = true;
    }
    
    private static bool HasHeartLabel(Component component)
    {
        return component.UiLabel == UiLabels.Heart;
    }

}